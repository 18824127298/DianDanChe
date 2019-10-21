using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;

namespace Sigbit.Framework.Security
{
    public class NLSUserLock
    {
        /// <summary>
        /// 用户安全扩展记录
        /// </summary>
        private TbUserSecurity _userSecurityRec = null;

        private string _userUid = "";
        /// <summary>
        /// 用户标识
        /// </summary>
        public string UserUid
        {
            get { return _userUid; }
            set { _userUid = value; }
        }

        private SbtUser_Login_LoginResult_LockOperation _lockOperation = SbtUser_Login_LoginResult_LockOperation.None;
        /// <summary>
        /// 锁定解锁操作
        /// </summary>
        internal SbtUser_Login_LoginResult_LockOperation LockOperation
        {
            get { return _lockOperation; }
            set { _lockOperation = value; }
        }

        /// <summary>
        /// 获取一条用户记录，如果无安全扩展记录，则创建一条。
        /// </summary>
        public void Fetch()
        {
            //====== 1. 取出安全扩展记录 ==========
            _userSecurityRec = new TbUserSecurity();

            _userSecurityRec.UserUid = this.UserUid;
            if (!_userSecurityRec.Fetch(true))
            {
                //========= 2. 如果安全扩展记录不存在，则新增一条 =============
                _userSecurityRec.UserUid = this.UserUid;
                _userSecurityRec.LockStatus = "";
                _userSecurityRec.LockStatusTime = DateTimeUtil.Now;
                _userSecurityRec.WrongPasswdCnt = 0;
                _userSecurityRec.WrongPasswdLastTime = DateTimeUtil.Now;
                _userSecurityRec.ModifyTime = DateTimeUtil.Now;
                _userSecurityRec.Remarks = "";

                _userSecurityRec.Insert();
            }
        }

        /// <summary>
        /// 帐户是否已被锁定
        /// </summary>
        public bool AccountAutoLocked(out string sLoginErrorMsg)
        {
            if (_userSecurityRec.LockStatus == "")
            {
                sLoginErrorMsg = "";
                return false;
            }
            else
            {
                if (SbtSecurityConfig.LockUser.AutoUnunlockEnabled == Bool3State.False)
                {
                    sLoginErrorMsg = "帐号已被锁定，请与系统管理员联系。";
                    return true;
                }

                //======== 1. 如果帐户锁定，判断锁定的时间有没有达到设置的时长 ===========
                int nLockDuration = DateTimeUtil.SecondsAfter(_userSecurityRec.LockStatusTime, DateTimeUtil.Now);
                if (nLockDuration >= SbtSecurityConfig.LockUser.AutoUnlockMinutes * 60)
                {
                    //========== 2. 如果超过自动解锁的配置分钟数，则自动解锁 =====
                    AutoUnlockUser();
                    sLoginErrorMsg = "";
                    return false;
                }
                else
                {
                    sLoginErrorMsg = "帐号已被锁定，" + DateTimeUtil.ToTimeStrFromSecond
                            (SbtSecurityConfig.LockUser.AutoUnlockMinutes * 60 - nLockDuration) + "后将自动解锁";
                    return true;
                }
            }
        }

        /// <summary>
        /// 自动帐户解锁
        /// </summary>
        private void AutoUnlockUser()
        {
            _userSecurityRec.LockStatus = "";
            _userSecurityRec.LockStatusTime = DateTimeUtil.Now;
            _userSecurityRec.WrongPasswdCnt = 0;
            _userSecurityRec.WrongPasswdLastTime = DateTimeUtil.Now;
            _userSecurityRec.ModifyTime = DateTimeUtil.Now;

            _userSecurityRec.Update();

            this.LockOperation = SbtUser_Login_LoginResult_LockOperation.Unlock;
        }

        public void WrongPasswordBeenInputted(out string sLoginErrorMsg)
        {
            //========== 1. 如果上一次输错密码的时间和今在不在同一天，则清空密码输入次数 ==========
            if (DateTimeUtil.GetDatePart(_userSecurityRec.WrongPasswdLastTime) != DateTimeUtil.GetDatePart(DateTimeUtil.Now))
                _userSecurityRec.WrongPasswdCnt = 0;

            //=========== 2. 累加密码次数 ============
            _userSecurityRec.WrongPasswdCnt++;
            _userSecurityRec.WrongPasswdLastTime = DateTimeUtil.Now;

            sLoginErrorMsg = "您输入的密码错误，请重新输入。";

            //=========== 3. 判断是否自动锁定帐号 ==========
            if (SbtSecurityConfig.LockUser.AutoLockEnabled == Bool3State.True)
            {
                sLoginErrorMsg = "您今天的密码输入次数已达到" + _userSecurityRec.WrongPasswdCnt.ToString() + "次，"
                        + "超过" + SbtSecurityConfig.LockUser.AutoLockTimes.ToString() + "次后将自动锁定帐号。";

                if (_userSecurityRec.WrongPasswdCnt > SbtSecurityConfig.LockUser.AutoLockTimes)
                {
                    _userSecurityRec.LockStatus = "auto_locked";
                    _userSecurityRec.LockStatusTime = DateTimeUtil.Now;
                    this.LockOperation = SbtUser_Login_LoginResult_LockOperation.Lock;

                    if (SbtSecurityConfig.LockUser.AutoUnunlockEnabled == Bool3State.True)
                        sLoginErrorMsg = "登录帐号已被自动锁定，" + SbtSecurityConfig.LockUser.AutoUnlockMinutes.ToString() 
                                + "分钟后可以自动解锁。";
                    else
                        sLoginErrorMsg = "登录帐号已被自动锁定，请与系统管理员联系。";
                }
            }

            _userSecurityRec.ModifyTime = DateTimeUtil.Now;
            _userSecurityRec.Update();
        }

        public void LoginSuccess()
        {
            if (_userSecurityRec.WrongPasswdCnt != 0)
            {
                _userSecurityRec.WrongPasswdCnt = 0;
                _userSecurityRec.WrongPasswdLastTime = DateTimeUtil.Now;
                _userSecurityRec.ModifyTime = DateTimeUtil.Now;

                _userSecurityRec.Update();
            }

        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;

namespace Sigbit.Framework.Security
{
    public class NLSUserLock
    {
        /// <summary>
        /// �û���ȫ��չ��¼
        /// </summary>
        private TbUserSecurity _userSecurityRec = null;

        private string _userUid = "";
        /// <summary>
        /// �û���ʶ
        /// </summary>
        public string UserUid
        {
            get { return _userUid; }
            set { _userUid = value; }
        }

        private SbtUser_Login_LoginResult_LockOperation _lockOperation = SbtUser_Login_LoginResult_LockOperation.None;
        /// <summary>
        /// ������������
        /// </summary>
        internal SbtUser_Login_LoginResult_LockOperation LockOperation
        {
            get { return _lockOperation; }
            set { _lockOperation = value; }
        }

        /// <summary>
        /// ��ȡһ���û���¼������ް�ȫ��չ��¼���򴴽�һ����
        /// </summary>
        public void Fetch()
        {
            //====== 1. ȡ����ȫ��չ��¼ ==========
            _userSecurityRec = new TbUserSecurity();

            _userSecurityRec.UserUid = this.UserUid;
            if (!_userSecurityRec.Fetch(true))
            {
                //========= 2. �����ȫ��չ��¼�����ڣ�������һ�� =============
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
        /// �ʻ��Ƿ��ѱ�����
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
                    sLoginErrorMsg = "�ʺ��ѱ�����������ϵͳ����Ա��ϵ��";
                    return true;
                }

                //======== 1. ����ʻ��������ж�������ʱ����û�дﵽ���õ�ʱ�� ===========
                int nLockDuration = DateTimeUtil.SecondsAfter(_userSecurityRec.LockStatusTime, DateTimeUtil.Now);
                if (nLockDuration >= SbtSecurityConfig.LockUser.AutoUnlockMinutes * 60)
                {
                    //========== 2. ��������Զ����������÷����������Զ����� =====
                    AutoUnlockUser();
                    sLoginErrorMsg = "";
                    return false;
                }
                else
                {
                    sLoginErrorMsg = "�ʺ��ѱ�������" + DateTimeUtil.ToTimeStrFromSecond
                            (SbtSecurityConfig.LockUser.AutoUnlockMinutes * 60 - nLockDuration) + "���Զ�����";
                    return true;
                }
            }
        }

        /// <summary>
        /// �Զ��ʻ�����
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
            //========== 1. �����һ����������ʱ��ͽ��ڲ���ͬһ�죬���������������� ==========
            if (DateTimeUtil.GetDatePart(_userSecurityRec.WrongPasswdLastTime) != DateTimeUtil.GetDatePart(DateTimeUtil.Now))
                _userSecurityRec.WrongPasswdCnt = 0;

            //=========== 2. �ۼ�������� ============
            _userSecurityRec.WrongPasswdCnt++;
            _userSecurityRec.WrongPasswdLastTime = DateTimeUtil.Now;

            sLoginErrorMsg = "�����������������������롣";

            //=========== 3. �ж��Ƿ��Զ������ʺ� ==========
            if (SbtSecurityConfig.LockUser.AutoLockEnabled == Bool3State.True)
            {
                sLoginErrorMsg = "�������������������Ѵﵽ" + _userSecurityRec.WrongPasswdCnt.ToString() + "�Σ�"
                        + "����" + SbtSecurityConfig.LockUser.AutoLockTimes.ToString() + "�κ��Զ������ʺš�";

                if (_userSecurityRec.WrongPasswdCnt > SbtSecurityConfig.LockUser.AutoLockTimes)
                {
                    _userSecurityRec.LockStatus = "auto_locked";
                    _userSecurityRec.LockStatusTime = DateTimeUtil.Now;
                    this.LockOperation = SbtUser_Login_LoginResult_LockOperation.Lock;

                    if (SbtSecurityConfig.LockUser.AutoUnunlockEnabled == Bool3State.True)
                        sLoginErrorMsg = "��¼�ʺ��ѱ��Զ�������" + SbtSecurityConfig.LockUser.AutoUnlockMinutes.ToString() 
                                + "���Ӻ�����Զ�������";
                    else
                        sLoginErrorMsg = "��¼�ʺ��ѱ��Զ�����������ϵͳ����Ա��ϵ��";
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

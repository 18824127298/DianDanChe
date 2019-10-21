using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;

namespace Sigbit.Framework.Security
{
    /// <summary>
    /// 密码口令相关的安全配置
    /// </summary>
    public class SbtSecurityConfig_LockUser
    {
        private Bool3State _autoLockEnabled = Bool3State.Undefine;
        /// <summary>
        /// 启用自动锁定
        /// </summary>
        public Bool3State AutoLockEnabled
        {
            get
            {
                if (_autoLockEnabled == Bool3State.Undefine)
                {
                    _autoLockEnabled = TbSysParameter.GetParamterValueBool3State("security_policy", "lock_user",
                            "auto_lock_enabled", Bool3State.False);
                }
                return _autoLockEnabled;
            }
            set
            {
                _autoLockEnabled = value;
                TbSysParameter.SetParameterValueBool3State("security_policy", "lock_user",
                            "auto_lock_enabled", value);
            }
        }

        private int _autoLockTimes = -1;
        /// <summary>
        /// 超过次数后锁定
        /// </summary>
        public int AutoLockTimes
        {
            get
            {
                if (_autoLockTimes == -1)
                {
                    _autoLockTimes = TbSysParameter.GetParameterValueInt("security_policy", "lock_user",
                            "auto_lock_times", 3);
                    if (_autoLockTimes <= 0 || _autoLockTimes > 20)
                        _autoLockTimes = 3;
                }

                return _autoLockTimes;
            }
            set
            {
                _autoLockTimes = value;
                TbSysParameter.SetParameterValueInt("security_policy", "lock_user", "auto_lock_times", value);
            }
        }

        private Bool3State _autoUnunlockEnabled = Bool3State.Undefine;
        /// <summary>
        /// 启用自动解锁
        /// </summary>
        public Bool3State AutoUnunlockEnabled
        {
            get
            {
                if (_autoUnunlockEnabled == Bool3State.Undefine)
                {
                    _autoUnunlockEnabled = TbSysParameter.GetParamterValueBool3State("security_policy", "unlock_user",
                            "auto_unlock_enabled", Bool3State.False);
                }
                return _autoUnunlockEnabled;
            }
            set
            {
                _autoUnunlockEnabled = value;
                TbSysParameter.SetParameterValueBool3State("security_policy", "unlock_user",
                            "auto_unlock_enabled", value);
            }
        }

        private int _autoUnlockMinutes = -1;
        /// <summary>
        /// 超过时长后自动解锁
        /// </summary>
        public int AutoUnlockMinutes
        {
            get
            {
                if (_autoUnlockMinutes == -1)
                {
                    _autoUnlockMinutes = TbSysParameter.GetParameterValueInt("security_policy", "lock_user",
                            "auto_unlock_minutes", 5);
                    if (_autoUnlockMinutes <= 0 || _autoUnlockMinutes > 30)
                        _autoUnlockMinutes = 5;
                }

                return _autoUnlockMinutes;
            }
            set
            {
                _autoUnlockMinutes = value;
                TbSysParameter.SetParameterValueInt("security_policy", "lock_user", "auto_unlock_minutes", value);
            }
        }

    }
}

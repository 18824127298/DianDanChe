using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Framework.Security
{
    public class SbtSecurityConfig
    {
        private static SbtSecurityConfig_Password _password = null;
        /// <summary>
        /// 密码（口令）相关的安全配置
        /// </summary>
        public static SbtSecurityConfig_Password Password
        {
            get
            {
                if (_password == null)
                    _password = new SbtSecurityConfig_Password();
                return _password;
            }
        }

        private static SbtSecurityConfig_LockUser _lockUser = null;
        /// <summary>
        /// 用户锁定设置
        /// </summary>
        public static SbtSecurityConfig_LockUser LockUser
        {
            get
            {
                if (_lockUser == null)
                    _lockUser = new SbtSecurityConfig_LockUser();
                return _lockUser;
            }
        }
    }
}

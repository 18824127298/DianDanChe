using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;

namespace Sigbit.Framework.Security
{
    /// <summary>
    /// 密码口令相关的安全配置
    /// </summary>
    public class SbtSecurityConfig_Password
    {
        private int _passwordStrength = -1;
        /// <summary>
        /// 密码强度
        /// </summary>
        public int PasswordStrength
        {
            get
            {
                if (_passwordStrength == -1)
                {
                    _passwordStrength = TbSysParameter.GetParameterValueInt("security_policy", "password", 
                            "password_strengh", 0);
                    if (_passwordStrength < 0 || _passwordStrength > 4)
                        _passwordStrength = 0;
                }

                return _passwordStrength;
            }
            set
            {
                _passwordStrength = value;
                TbSysParameter.SetParameterValueInt("security_policy", "password", "password_strengh", value);
            }
        }

        private Bool3State _modiPasswordIfLowerStrength = Bool3State.Undefine;
        /// <summary>
        /// 登录后修改低强度的密码
        /// </summary>
        public Bool3State ModiPasswordIfLowerStrength
        {
            get 
            {
                if (_modiPasswordIfLowerStrength == Bool3State.Undefine)
                {
                    _modiPasswordIfLowerStrength = TbSysParameter.GetParamterValueBool3State("security_policy", "password",
                            "modi_password_if_lower_strength", Bool3State.False);
                }
                return _modiPasswordIfLowerStrength;
            }
            set 
            { 
                _modiPasswordIfLowerStrength = value;
                TbSysParameter.SetParameterValueBool3State("security_policy", "password",
                            "modi_password_if_lower_strength", value);
            }
        }
    }
}

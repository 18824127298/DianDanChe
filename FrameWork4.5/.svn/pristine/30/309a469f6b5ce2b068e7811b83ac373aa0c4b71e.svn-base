using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;
using Sigbit.Framework;

namespace Sigbit.App.Net.IBXService.Log
{
    public class CTIBXLogMessageConfig
    {
        private static CTIBXLogMessageConfig _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static CTIBXLogMessageConfig Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new CTIBXLogMessageConfig();
                return _thisInstance;
            }
        }

        private Bool3State _logMessageInDB = Bool3State.Undefine;
        /// <summary>
        /// 是否记录日志
        /// </summary>
        public bool LogMessageInDB
        {
            get
            {
                if (_logMessageInDB == Bool3State.Undefine)
                {
                    _logMessageInDB = TbSysParameter.GetParamterValueBool3State("ibx_message", "log_config",
                            "log_message_in_db", Bool3State.True);
                }

                if (_logMessageInDB == Bool3State.True)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    _logMessageInDB = Bool3State.True;
                else
                    _logMessageInDB = Bool3State.False;

                TbSysParameter.SetParameterValueBool3State("ibx_message", "log_config",
                        "log_message_in_db", _logMessageInDB);
            }
        }

        private int _logClearBeforeHours = -1;
        /// <summary>
        /// 清除指定小时数前的日志记录
        /// </summary>
        public int LogClearBeforeHours
        {
            get
            {
                if (_logClearBeforeHours != -1)
                    return _logClearBeforeHours;

                _logClearBeforeHours = TbSysParameter.GetParameterValueInt("ibx_message", "log_config",
                        "log_clear_before_hours", 24 * 7);
                if (_logClearBeforeHours < 1)
                    _logClearBeforeHours = 24 * 7;

                return _logClearBeforeHours;
            }
            set
            {
                _logClearBeforeHours = value;
                if (_logClearBeforeHours < 1)
                    _logClearBeforeHours = 24 * 7;
                TbSysParameter.SetParameterValueInt("ibx_message", "log_config",
                        "log_clear_before_hours", _logClearBeforeHours);
            }
        }
    }
}

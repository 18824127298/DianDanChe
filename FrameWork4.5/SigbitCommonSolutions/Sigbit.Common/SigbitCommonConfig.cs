using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Sigbit.Common;

namespace Sigbit.Common
{
    class SigbitCommonConfig : ConfigBase
    {
        static SigbitCommonConfig _thisInstance = null;
        /// <summary>
        /// 唯一实例 
        /// </summary>
        public static SigbitCommonConfig Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new SigbitCommonConfig();

                return _thisInstance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SigbitCommonConfig()
        {
            string sConfigFileName;
            sConfigFileName = AppPath.AppFullPath("config", 
                    "Sigbit.Common.dll.config");

            LoadFromFile(sConfigFileName);
        }

        /// <summary>
        /// 是否写日志
        /// </summary>
        public bool DebugFileEnabled
        {
            get
            {
                return GetBool("debugConfig", "debugFileEnabled");
            }
        }

        private string _debugFileDirectory = "";
        /// <summary>
        /// Debug日志的目录
        /// </summary>
        public string DebugFileDirectory
        {
            get
            {
                if (_debugFileDirectory != "")
                    return _debugFileDirectory;

                string sRet = GetString("debugConfig", "debugFileDirectory", "");
                if (sRet != "")
                {
                    Directory.CreateDirectory(sRet);
                    _debugFileDirectory = sRet;
                    return sRet;
                }

                sRet = AppPath.AppFullPath("log", "debuglog");
                Directory.CreateDirectory(sRet);
                _debugFileDirectory = sRet;
                return sRet;
            }
        }

        public bool IsLogError
        {
            get
            {
                bool bIsLogError = GetBool("debugConfig", "logError", false);
                return bIsLogError;
            }
        }

        public bool IsLogWarning
        {
            get
            {
                bool bIsLogError = GetBool("debugConfig", "logWarning", false);
                return bIsLogError;
            }
        }

        public bool IsLogDebugMessage
        {
            get
            {
                bool bIsLogError = GetBool("debugConfig", "logDebugMessage", false);
                return bIsLogError;
            }
        }

        private string _timeCostLoggerDirectory = "";
        /// <summary>
        /// 计时日志的目录
        /// </summary>
        public string TimeCostLoggerDirectory
        {
            get
            {
                if (_timeCostLoggerDirectory != "")
                    return _timeCostLoggerDirectory;

                string sRet = AppPath.AppFullPath("log", "timetune");
                Directory.CreateDirectory(sRet);
                _timeCostLoggerDirectory = sRet;
                return sRet;
            }
        }

    }
}

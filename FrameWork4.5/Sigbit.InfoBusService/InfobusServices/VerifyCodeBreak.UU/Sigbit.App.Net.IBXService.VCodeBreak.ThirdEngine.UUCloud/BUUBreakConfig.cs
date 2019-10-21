using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;

namespace Sigbit.App.Net.IBXService.VCodeBreak.ThirdEngine.UUCloud
{
    class BUUBreakConfig : ConfigBase
    {
        private static BUUBreakConfig _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static BUUBreakConfig Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new BUUBreakConfig();
                return _thisInstance;
            }
        }

        public BUUBreakConfig()
        {
            string sFileName = AppPath.AppFullPath("config", "Sigbit.App.Net.IBXService.VCodeBreak.ThirdEngine.UUCloud.config");
            LoadFromFile(sFileName);
        }

        private int _softID = -1;
        /// <summary>
        /// 软件标识
        /// </summary>
        public int SoftID
        {
            get
            {
                if (_softID == -1)
                    _softID = GetInt("softInfo", "SoftId");
                return _softID;
            }
        }

        private string _softKey = "";
        /// <summary>
        /// 软件Key
        /// </summary>
        public string SoftKey
        {
            get
            {
                if (_softKey == "")
                    _softKey = GetString("softInfo", "SoftKey");
                return _softKey;
            }
        }

        private string _userName = "";
        /// <summary>
        /// 软件Key
        /// </summary>
        public string UserName
        {
            get
            {
                if (_userName == "")
                    _userName = GetString("userInfo", "User");
                return _userName;
            }
        }

        private string _userPassword = "AKFBCD";
        /// <summary>
        /// 软件Key
        /// </summary>
        public string UserPassword
        {
            get
            {
                if (_userPassword == "AKFBCD")
                    _userPassword = GetString("userInfo", "PassWord");
                return _userPassword;
            }
        }

    }
}

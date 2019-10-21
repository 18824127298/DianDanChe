using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using Sigbit.Common;

namespace Sigbit.App.Net.WeiXinService.Client
{
    public class TWXWeiXinClientConfig : ConfigBase
    {
        private string _configFileName = "";

        private static TWXWeiXinClientConfig _instance = null;

        public static TWXWeiXinClientConfig Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TWXWeiXinClientConfig();
                return _instance;
            }
        }


        public TWXWeiXinClientConfig()
        {
            _configFileName = AppPath.AppFullPath("config",
                     "Sigbit.App.Net.WeiXinService.Client.config");

            FileInfo fileConfig = new FileInfo(_configFileName);

            fileConfig.Directory.Create();

            if (fileConfig.Exists)
            {
                LoadFromFile(_configFileName);
            }
            
        }


        private string _appID = "";
        /// <summary>
        /// 应用ID
        /// </summary>
        public string AppID
        {
            get
            {
                if (_appID == "")
                {
                    _appID = GetString("weixinConfig", "appId");
                }
                return _appID;
            }
            set
            {
                _appID = value;
                SetString("weixinConfig", "appId", value);
            }
        }


        private string _appSecret = "";
        /// <summary>
        /// 应用密钥
        /// </summary>
        public string AppSecret
        {
            get
            {
                if (_appSecret == "")
                {
                    _appSecret = GetString("weixinConfig", "appSecret");
                }
                return _appSecret;
            }
            set
            {
                _appSecret = value;
                SetString("weixinConfig", "appSecret", value);
            }
        }


        private string _token = "";
        /// <summary>
        /// 令牌
        /// </summary>
        public string Token
        {
            get
            {
                if (_token == "")
                {
                    _token = GetString("weixinConfig", "token");
                }
                return _token;
            }
            set
            {
                _token = value;
                SetString("weixinConfig", "token", value);
            }
        }


        /// <summary>
        /// 保存配置
        /// </summary>
        public void SaveConfig()
        {
            SaveToFile(_configFileName);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;

namespace Sigbit.App.Net.IBXService.DNS.Client
{
    public class IBXDnsClientConfig : ConfigBase
    {
        #region 唯一实例及构造函数
        static IBXDnsClientConfig _thisInstance = null;
        /// <summary>
        /// 唯一实例 
        /// </summary>
        public static IBXDnsClientConfig Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new IBXDnsClientConfig();

                return _thisInstance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public IBXDnsClientConfig()
        {
            string sConfigFileName;
            sConfigFileName = AppPath.AppFullPath("config",
                    "Sigbit.App.Net.IBXService.DNS.Client.config");

            LoadFromFile(sConfigFileName);
        }
        #endregion 唯一实例及构造函数

        private string _servicePageUrl = "";
        /// <summary>
        /// 服务页面地址
        /// </summary>
        public string ServicePageUrl
        {
            get
            {
                if (_servicePageUrl == "")
                {
                    _servicePageUrl = GetString("serviceConfig", "servicePage");
                    if (_servicePageUrl == "")
                        throw new Exception("服务页面缺少配置-serviceConfig-servicePage");
                }

                return _servicePageUrl;
            }
        }
    }
}

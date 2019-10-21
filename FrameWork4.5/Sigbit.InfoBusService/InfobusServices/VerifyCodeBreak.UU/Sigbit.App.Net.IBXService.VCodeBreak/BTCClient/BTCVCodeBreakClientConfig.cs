using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;

namespace Sigbit.App.Net.IBXService.VCodeBreak.BTCClient
{
    class BTCVCodeBreakClientConfig : ConfigBase
    {
        #region 唯一实例及构造函数
        static BTCVCodeBreakClientConfig _thisInstance = null;
        /// <summary>
        /// 唯一实例 
        /// </summary>
        public static BTCVCodeBreakClientConfig Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new BTCVCodeBreakClientConfig();

                return _thisInstance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BTCVCodeBreakClientConfig()
        {
            string sConfigFileName;
            sConfigFileName = AppPath.AppFullPath("config",
                    "Sigbit.App.Net.IBXService.VCodeBreak.BTCClient.config");

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

using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;

namespace Sigbit.App.Net.IBXService.DNS.Client
{
    public class IBXDnsClientConfig : ConfigBase
    {
        #region Ψһʵ�������캯��
        static IBXDnsClientConfig _thisInstance = null;
        /// <summary>
        /// Ψһʵ�� 
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
        /// ���캯��
        /// </summary>
        public IBXDnsClientConfig()
        {
            string sConfigFileName;
            sConfigFileName = AppPath.AppFullPath("config",
                    "Sigbit.App.Net.IBXService.DNS.Client.config");

            LoadFromFile(sConfigFileName);
        }
        #endregion Ψһʵ�������캯��

        private string _servicePageUrl = "";
        /// <summary>
        /// ����ҳ���ַ
        /// </summary>
        public string ServicePageUrl
        {
            get
            {
                if (_servicePageUrl == "")
                {
                    _servicePageUrl = GetString("serviceConfig", "servicePage");
                    if (_servicePageUrl == "")
                        throw new Exception("����ҳ��ȱ������-serviceConfig-servicePage");
                }

                return _servicePageUrl;
            }
        }
    }
}

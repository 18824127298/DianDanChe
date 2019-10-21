using System;
using System.Collections.Generic;
using System.Text;

using System.Net;

using Sigbit.App.Net.IBXService.Message;
using Sigbit.App.Net.IBXService.DNS.Message;

namespace Sigbit.App.Net.IBXService.DNS.Client
{
    public class IBXDnsClient
    {
        private static IBXDnsClient _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static IBXDnsClient Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new IBXDnsClient();
                return _thisInstance;
            }
        }

        public string ServiceAddressOfREQ(IBMRequestBase req)
        {
            //========== 1. 在本地缓存中定位服务地址 =============
            string sServiceAddress = IBXDnsClient_CachePool.Instance.GetServiceAddressOfREQ(req);
            if (sServiceAddress != "")
                return sServiceAddress;

            //============ 2. 如果找不到地址，则通过DNS服务寻址 ============
            IBMDnsRESP dnsRESP = GetDNSResponse(req);
            if (dnsRESP.ErrorCode == 0)
            {
                sServiceAddress = dnsRESP.ServiceUrlAddress;
            }
            else
                throw new Exception("无法寻址相应的服务地址 - " + req.TransCode);

            //========== 3. 将寻址到的地址，记到缓存中 ==========
            IBXDnsClient_CachePool.Instance.AddItem(req, sServiceAddress);

            return sServiceAddress;
        }

        private IBMDnsRESP GetDNSResponse(IBMRequestBase req)
        {
            IBMDnsRESP resp = new IBMDnsRESP();

            WebClient webClient = new WebClient();

            string sServicePage = IBXDnsClientConfig.Instance.ServicePageUrl;

            byte[] bsRequestContent = req.ToBytes();

            try
            {
                byte[] bsPageContent = webClient.UploadData(sServicePage,
                        "POST", bsRequestContent);
                resp.ReadFrom(bsPageContent);
            }
            catch (Exception ex)
            {
                resp.ErrorCode = 5455;
                resp.ErrorString = "DNS服务调用失败——" + ex.Message;
            }

            return resp;
        }
    }
}

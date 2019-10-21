using System;
using System.Collections.Generic;
using System.Text;

using System.Net;

using Sigbit.Common;
using Sigbit.App.Net.IBXService.Message;
using Sigbit.App.Net.IBXService.DNS.Client;
using Sigbit.App.Net.IBXService.Upload.Message;

namespace Sigbit.App.Net.IBXService.Cient
{
    public class IBXBusClient
    {
        private static IBXBusClient _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static IBXBusClient Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new IBXBusClient();
                return _thisInstance;
            }
        }


        private string _serviceUrl = "";

        public string ServiceUrl
        {
            get { return _serviceUrl; }
            set { _serviceUrl = value; }
        }


        public void GetResponse(IBMRequestBase req, IBMResponseBase resp)
        {
            string sServiceUrlAddress = this.ServiceUrl;

            if (sServiceUrlAddress == "")
            {
                sServiceUrlAddress = IBXDnsClient.Instance.ServiceAddressOfREQ(req);
            }

            WebClient webClient = new WebClient();
            byte[] bsRequestContent = req.ToBytes();

            try
            {
                byte[] bsPageContent = webClient.UploadData(sServiceUrlAddress,
                        "POST", bsRequestContent);
                resp.ReadFrom(bsPageContent);
            }
            catch (Exception ex)
            {
                resp.ErrorCode = 5456;
                resp.ErrorString = "服务调用失败——" + ex.Message + "\r\n访问地址：" + sServiceUrlAddress;

                DebugLogger.LogDebugMessage(resp.ErrorString);
            }
        }

        public void UploadFile(IBMRequestBase req, string sFileName, IBMUploadReceiptRESP receiptRESP)
        {
            string sServiceUrlAddress = IBXDnsClient.Instance.ServiceAddressOfREQ(req);
            string sUploadUrlAddress = GetUploadUrlOfServiceUrl(sServiceUrlAddress);

            WebClient webClient = new WebClient();
            byte[] bsRequestContent = FileUtil.ReadBytesFromFile(sFileName);

            try
            {
                byte[] bsPageContent = webClient.UploadData(sUploadUrlAddress,
                        "POST", bsRequestContent);
                receiptRESP.ReadFrom(bsPageContent);
            }
            catch (Exception ex)
            {
                receiptRESP.ErrorCode = 5444;
                receiptRESP.ErrorString = "服务调用失败——" + ex.Message + "\r\n访问地址：" + sServiceUrlAddress;

                DebugLogger.LogDebugMessage(receiptRESP.ErrorString);
            }
        }

        private string GetUploadUrlOfServiceUrl(string sServiceUrl)
        {
            string sFileExt = FileUtil.ExtractFileExt(sServiceUrl);
            string sFileWithNoExt = FileUtil.CutFileExt(sServiceUrl);

            string sRet = sFileWithNoExt + "_upload" + sFileExt;
            return sRet;
        }
    }
}

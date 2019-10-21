using System;
using System.Collections.Generic;
using System.Text;
using Sigbit.App.Net.IBXService.VCodeBreak.BTMMessage;
using System.Net;

namespace Sigbit.App.Net.IBXService.VCodeBreak.BTCClient
{
    public class BTCVCodeBreakClient
    {
        private static BTCVCodeBreakClient _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static BTCVCodeBreakClient Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new BTCVCodeBreakClient();
                return _thisInstance;
            }
        }

        public void GetResponse(SRMRequestBase req, SRMResponseBase resp)
        {
            string sServiceUrlAddress = BTCVCodeBreakClientConfig.Instance.ServicePageUrl;

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
                resp.ErrorCode = "855_service_call_fail";
                resp.ErrorString = "验证码破解服务调用失败——" + ex.Message + "\r\n访问地址：" + sServiceUrlAddress;
            }
        }
    }
}

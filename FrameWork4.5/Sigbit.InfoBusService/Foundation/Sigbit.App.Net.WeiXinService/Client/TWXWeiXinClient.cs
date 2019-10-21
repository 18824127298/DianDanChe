using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

using Sigbit.Common;

using Sigbit.App.Net.WeiXinService.Message;
using Sigbit.App.Net.WeiXinService.JSON;
using Sigbit.App.Net.WeiXinService.Event;

namespace Sigbit.App.Net.WeiXinService.Client
{
    public class TWXWeiXinClient
    {
        private static TWXWeiXinClient _instance = null;

        public static TWXWeiXinClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TWXWeiXinClient();
                return _instance;
            }
        }

        private DateTime _accessTokenTimeoutTime = DateTime.Now;

        private string _accessToken = "";

        public string GetAccessToken()
        {
            TimeSpan ts = DateTime.Now - _accessTokenTimeoutTime;

            if (ts.TotalSeconds < 0 && _accessToken != "")
            {
                return _accessToken;
            }

            //============ 获取AccessToken =========
            TWJAccessTokenReq req = new TWJAccessTokenReq();
            TWJAccessTokenResp resp = new TWJAccessTokenResp();

            resp = (TWJAccessTokenResp)TWXWeiXinClient.Instance.DealJsonRequest(req, resp);

            if (resp.ErrCode == 0)
            {
                _accessToken = resp.AccessToken;
                _accessTokenTimeoutTime = DateTime.Now.AddSeconds(resp.ExpiresIn - 200);
            }
            else
            {

            }

            return _accessToken;

        }

        /// <summary>
        /// 处理JSON请求
        /// </summary>
        /// <param name="req"></param>
        /// <param name="resp"></param>
        /// <returns></returns>
        public TWJRespBase DealJsonRequest(TWJReqBase req, TWJRespBase resp)
        {
            try
            {
                TWJRespBase ret = resp;

                string sRespInfo = "";

                string sRequestUrl = req.ToAccessUrl();


                if (sRequestUrl == "")
                    throw new Exception("请求的URL不能为空");

                if (req.AccessMethod == HttpMethod.Get)
                {
                    sRespInfo = ProcessGetRequest(sRequestUrl);

                    FileUtil.WriteStringToFile(GetLogFileName("json_result"), sRequestUrl + "\r\n" + sRespInfo);

                    ret.ParseJsonResult(sRespInfo);
                }
                else
                {
                    FileUtil.WriteStringToFile(GetLogFileName("req"), req.ToJsonString());

                    sRespInfo = ProcessPostRequest(sRequestUrl, req.ToJsonString());

                    FileUtil.WriteStringToFile(GetLogFileName("resp"), sRequestUrl + "\r\n" + sRespInfo);

                    ret.ParseJsonResult(sRespInfo);
                }

                return ret;
            }
            catch (Exception ex)
            {
                FileUtil.WriteStringToFile(GetLogFileName("json_error"), ex.Message + "\r\n" + ex.StackTrace);
            }

            return null;
        }


        private string ProcessGetRequest(string sRequestUrl)
        {
            string sRet = "";

            WebClient client = new WebClient();
            client.Encoding = Encoding.Default;

            sRet = client.DownloadString(sRequestUrl);

            return sRet;
        }

        private string ProcessPostRequest(string sRequestUrl, string sPostData)
        {
            byte[] btSendData = Encoding.UTF8.GetBytes(sPostData);

            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            client.Headers.Add("ContentLength", btSendData.Length.ToString());
            byte[] btResponseData = client.UploadData(sRequestUrl, "POST", btSendData);

            string sRet = Encoding.UTF8.GetString(btResponseData);

            return sRet;
        }



        private string GetLogFileName(string sPostFix)
        {
            string sFileName = DateTimeUtil.Now.Replace("-", "").Replace(":", "").Replace(" ", "-");
            sFileName += "-" + RandUtil.NewString(3, RandStringType.Lower) + "-" + sPostFix + ".log";

            sFileName = "c:\\temp\\" + sFileName;

            return sFileName;
        }



    }
}

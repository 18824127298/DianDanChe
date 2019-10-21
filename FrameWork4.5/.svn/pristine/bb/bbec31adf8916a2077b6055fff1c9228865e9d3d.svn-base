using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sigbit.App.Net.WeiXinService.Client;

namespace Sigbit.App.Net.WeiXinService.JSON
{
    /// <summary>
    /// 获取服务器IP
    /// </summary>
    public class TWJGetCallbackIpReq : TWJReqBase
    {
        //https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token=ACCESS_TOKEN

        public TWJGetCallbackIpReq()
        {
            ReqCode = TWJBusiCode.GetCallbackIp;
            this.AccessMethod = HttpMethod.Get;
        }


        private string _accessToken = "";
        /// <summary>
        /// 访问令牌
        /// </summary>
        public string AccessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }

        public override string ToAccessUrl()
        {
            string sRet = "https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token=" + this.AccessToken;

            return sRet;
        }

    }
}

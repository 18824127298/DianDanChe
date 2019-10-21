using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigbit.App.Net.WeiXinService.Client;

namespace Sigbit.App.Net.WeiXinService.JSON
{
    public class TWJJsApiTicketReq : TWJReqBase
    {
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
            if (this.AccessToken == "")
            {
                this.AccessToken = TWXWeiXinClient.Instance.GetAccessToken();
            }

            string sRet = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + this.AccessToken + "&type=wx_card";

            return sRet;
        }
    }
}

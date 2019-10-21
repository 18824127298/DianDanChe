using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sigbit.App.Net.WeiXinService.Client;

namespace Sigbit.App.Net.WeiXinService.JSON
{
    public class TWJAccessTokenReq : TWJReqBase
    {
        //https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET

        public TWJAccessTokenReq()
        {
            ReqCode = TWJBusiCode.AccessToken;
            AccessMethod = HttpMethod.Get;
        }

        public override string ToAccessUrl()
        {
            string sRet = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}",
               TWXWeiXinClientConfig.Instance.AppID, TWXWeiXinClientConfig.Instance.AppSecret);

            return sRet;
        }

    }
}

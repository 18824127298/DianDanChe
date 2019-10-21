using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sigbit.Common;
using Sigbit.App.Net.WeiXinService.JSON;

namespace Sigbit.App.Net.WeiXinService.User
{

    public enum TWJOAuth2ResultReqEGrantType
    {
        AuthorizationCode
    }

    public class TWJOAuth2ResultReq : TWJReqBase
    {
        public TWJOAuth2ResultReq()
        {
            this.ReqCode = TWJBusiCode.OAuthResult;
            this.AccessMethod = HttpMethod.Get;
        }

        private string _appId = "";
        /// <summary>
        /// 公众号的唯一标识
        /// </summary>
        public string AppId
        {
            get { return _appId; }
            set { _appId = value; }
        }


        private string _secret = "";
        /// <summary>
        /// 公众号的appsecret
        /// </summary>
        public string Secret
        {
            get { return _secret; }
            set { _secret = value; }
        }


        private string _code = "";
        /// <summary>
        /// 填写第一步获取的code参数
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private TWJOAuth2ResultReqEGrantType _grantType = TWJOAuth2ResultReqEGrantType.AuthorizationCode;
        /// <summary>
        /// 授权类型
        /// </summary>
        public TWJOAuth2ResultReqEGrantType GrantType
        {
            get { return _grantType; }
            set { _grantType = value; }
        }


        public override string ToAccessUrl()
        {
            string sRet = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + this.AppId
                + "&secret=" + this.Secret + "&code=" + this.Code + "&grant_type=" + GrantType.ToCodeString();

            return sRet;
        }

    }
}

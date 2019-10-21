using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Sigbit.Common;
using Sigbit.App.Net.WeiXinService.JSON;
using Sigbit.Common.Encrypt;

namespace Sigbit.App.Net.WeiXinService.User
{
    public enum TWJEScope
    {
        [SbtEnumDescString("静默获取OpenId")]
        SnsapiBase,
        [SbtEnumDescString("用户授权获取信息")]
        SnsapiUserinfo
    }

    public class TWJOAuth2RedirectReq : TWJReqBase
    {

        //https://open.weixin.qq.com/connect/oauth2/authorize?appid=APPID&redirect_uri=REDIRECT_URI&response_type=code&scope=SCOPE&state=STATE#wechat_redirect
        //若提示“该链接无法访问”，请检查参数是否填写错误，是否拥有scope参数对应的授权作用域权限。

        public TWJOAuth2RedirectReq()
        {
            this.ReqCode = TWJBusiCode.OAuthRedirect;
        }

        private string _appId = "";
        /// <summary>
        /// 公众号唯一标识
        /// </summary>
        public string AppId
        {
            get { return _appId; }
            set { _appId = value; }
        }

        public string RedirectUri { get; set; }

        private string _response_type = "code";
        /// <summary>
        /// 返回类型,默认code
        /// </summary>
        public string ResponseType
        {
            get { return _response_type; }
            set { _response_type = value; }
        }


        private TWJEScope _scope = TWJEScope.SnsapiBase;
        /// <summary>
        /// 应用授权作用域
        /// </summary>
        public TWJEScope Scope
        {
            get { return _scope; }
            set { _scope = value; }
        }

        private string _state = "";
        /// <summary>
        /// 重定向参数
        /// </summary>
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }


        public override string ToAccessUrl()
        {
            if (this.AppId == "")
                this.AppId = Client.TWXWeiXinClientConfig.Instance.AppID;

            if (this.State == "")
                this.State = RandUtil.NewString(5, RandStringType.LowerNumber);

            if (this.RedirectUri.Contains("?"))
            {
                this.RedirectUri += "&enstate=" + EncryptUtil.DesEncodeString(this.State, "ieosid");
            }
            else
            {
                this.RedirectUri += "?enstate=" + EncryptUtil.DesEncodeString(this.State, "ieosid");
            }

            string sRet = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + this.AppId
                + "&redirect_uri=" + HttpUtility.UrlEncode(this.RedirectUri)
            + "&response_type=" + this.ResponseType + "&scope="
            + this.Scope.ToCodeString() + "&state=" + this.State + "#wechat_redirect";

            return sRet;
        }


    }
}

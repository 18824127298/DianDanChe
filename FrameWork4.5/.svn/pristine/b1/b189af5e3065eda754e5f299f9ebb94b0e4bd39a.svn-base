using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sigbit.Common;
using Sigbit.App.Net.WeiXinService.JSON;
using System.Web.Script.Serialization;

namespace Sigbit.App.Net.WeiXinService.User
{

    class TWJOAuth2Result
    {

        public string access_token { get; set; }  //网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同

        public int expires_in { get; set; } //	access_token接口调用凭证超时时间，单位（秒）
        public string refresh_token { get; set; }	//用户刷新access_token
        public string openid { get; set; }	//用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID
        public string scope { get; set; }	//用户授权的作用域，使用逗号（,）分隔
        public string unionid { get; set; }	//只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。详见：获取用户个人信息（UnionID机制）
    }


    public class TWJOAuth2ResultResp : TWJRespBase
    {
        public TWJOAuth2ResultResp()
        {
        }

        //access_token	网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
        //expires_in	access_token接口调用凭证超时时间，单位（秒）
        //refresh_token	用户刷新access_token
        //openid	用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID
        //scope	用户授权的作用域，使用逗号（,）分隔
        //unionid	只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。详见：获取用户个人信息（UnionID机制）

        private string _accessToken = "";
        /// <summary>
        /// 网页授权接口调用凭证
        /// </summary>
        public string AccessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }


        private int _expiresIn = 0;
        /// <summary>
        /// 接口调用凭证超时时间(秒)
        /// </summary>
        public int ExpiresIn
        {
            get { return _expiresIn; }
            set { _expiresIn = value; }
        }


        private string _refreshToken = "";
        /// <summary>
        /// 用户刷新Token
        /// </summary>
        public string RefreshToken
        {
            get { return _refreshToken; }
            set { _refreshToken = value; }
        }

        private string _openId = "";
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string OpenId
        {
            get { return _openId; }
            set { _openId = value; }
        }


        private string _scope = "";
        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔
        /// </summary>
        public string Scope
        {
            get { return _scope; }
            set { _scope = value; }
        }

        private string _unionId = "";
        /// <summary>
        /// 全局唯一标识
        /// </summary>
        public string UnionId
        {
            get { return _unionId; }
            set { _unionId = value; }
        }


        public override void ParseJsonResult(string sJsonResultString)
        {
            base.ParseJsonResult(sJsonResultString);

            if (base.ErrCode == 0)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();

                TWJOAuth2Result result = js.Deserialize<TWJOAuth2Result>(sJsonResultString);

                this.AccessToken = result.access_token;
                this.OpenId = result.openid;
                this.ExpiresIn = result.expires_in;
                this.RefreshToken = result.refresh_token;
                this.Scope = result.scope;
                this.UnionId = result.unionid;

            }

        }

    }
}

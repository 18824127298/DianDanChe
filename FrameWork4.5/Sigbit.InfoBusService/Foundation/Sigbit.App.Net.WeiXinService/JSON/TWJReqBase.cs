using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sigbit.Common;
using System.Web;

namespace Sigbit.App.Net.WeiXinService.JSON
{
    public enum HttpMethod
    {
        Get,
        Post
    }

    /// <summary>
    /// JSON消息业务码
    /// </summary>
    public enum TWJBusiCode
    {
        None,
        [SbtEnumDescString("获取AccessToken")]
        AccessToken,
        [SbtEnumDescString("获取微信服务器IP地址")]
        GetCallbackIp,
        [SbtEnumDescString("自定义创建菜单")]
        MenuCreate,
        [SbtEnumDescString("获取用户授权重定向")]
        OAuthRedirect,
        [SbtEnumDescString("获取用户授权结果")]
        OAuthResult,
        [SbtEnumDescString("拉取用户信息")]
        SnsapiUserinfo
    }


    public abstract class TWJReqBase
    {

        private TWJBusiCode _reqCode = TWJBusiCode.None;
        /// <summary>
        /// 请求码
        /// </summary>
        protected TWJBusiCode ReqCode
        {
            get { return _reqCode; }
            set { _reqCode = value; }
        }

        public virtual string ToAccessUrl()
        {
            string sRet = "";

            return sRet;
        }

        private HttpMethod _accessMethod = HttpMethod.Get;
        /// <summary>
        /// 访问方式
        /// </summary>
        public HttpMethod AccessMethod
        {
            get { return _accessMethod; }
            set { _accessMethod = value; }
        }


        public virtual string ToJsonString()
        {
            return "";
        }

    }

}

using CheDaiBaoCommonService.Models;
using CheDaiBaoWeChatModel;
using Sigbit.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CheDaiBaoCommonService.Service
{
    public class WeiXinAuthenticationService
    {
        private readonly string appid = Configs.GetWeiXinAppId();
        private readonly string appsecret = Configs.GetWeiXinAppsecret();
        private readonly string loanurl = Configs.GetWeiXinUrlLoan();

        //判断是否通过微信进来
        public void WeiXinRenZheng(AuthorizationContext filterContext)
        {
            var code = filterContext.HttpContext.Request.QueryString["Code"];
            DebugLogger.LogDebugMessage(code + "," + loanurl);
            if (string.IsNullOrEmpty(code))
            {
                var url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=" + loanurl + "&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect", appid);
                //filterContext.HttpContext.Response.Redirect(url);
                //filterContext.HttpContext.Response.End();
                filterContext.Result = new RedirectResult(url);
            }
            else
            {
                var url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, appsecret, code);

                

                string authorization_code = WeChatBaseRequestService.RequestUrl(url);

                string openid = WeChatBaseRequestService.GetJsonValue(authorization_code, "openid");

                string apptoken = WeChatBaseRequestService.getApptoken();
                DebugLogger.LogDebugMessage(apptoken);
                url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", apptoken, openid);
                string userinfo = WeChatBaseRequestService.RequestUrl(url);
                string unionid = WeChatBaseRequestService.GetJsonValue(userinfo, "unionid");
                WeiXinModel WeiXin = new WeiXinModel();
                WeiXin.UnionId = unionid;
                WeiXin.HeadImgurl = WeChatBaseRequestService.GetJsonValue(userinfo, "headimgurl").Replace("\\", "");
                WeiXin.NickName = WeChatBaseRequestService.GetJsonValue(userinfo, "nickname");
                WeiXin.OpenId = openid;
                WeiXin.Token = apptoken;

                System.Web.HttpContext.Current.Session["WeiXin"] = WeiXin;
            }
        }

        #region 获取Token
        /// <summary>
        /// 获取Token
        /// </summary>
        public string GetToken()
        {
            string strJson = WeChatBaseRequestService.RequestUrl(string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, appsecret));
            return WeChatBaseRequestService.GetJsonValue(strJson, "access_token");
        }
        #endregion

        #region 验证Token是否过期
        /// <summary>
        /// 验证Token是否过期
        /// </summary>
        public bool TokenExpired(string access_token)
        {
            string jsonStr = WeChatBaseRequestService.RequestUrl(string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", access_token));
            if (WeChatBaseRequestService.GetJsonValue(jsonStr, "errcode") == "42001" || WeChatBaseRequestService.GetJsonValue(jsonStr, "errcode") == "40001")
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}

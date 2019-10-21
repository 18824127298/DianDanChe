using CheDaiBaoCommonService.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CheDaiBaoWeChatController.Controller
{
    public static class WxSharedController
    {
        static System.Web.Caching.Cache objCache = HttpRuntime.Cache;

        /// <summary>
        /// 获取jsapi_ticket
        /// 有效期7200秒，开发者必须在自己的服务全局缓存jsapi_ticket
        /// </summary>
        /// <returns></returns>
        public static void GetJsApiTicket()
        {
            string accessToken = string.Empty;
            accessToken = WeChatBaseRequestService.getApptoken();
            if (!string.IsNullOrEmpty(accessToken))
            {
                string url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + accessToken + "&type=jsapi";
                string resStr = WeChatBaseRequestService.RequestUrl(url, "Get");
                TicketModel model = JsonConvert.DeserializeObject<TicketModel>(resStr);
                if (!string.IsNullOrEmpty(model.ticket))
                {
                    //请求成功了
                    DateTime dt = DateTime.Now.AddSeconds(Convert.ToInt32(model.expires_in));
                    objCache.Insert("JsApiTicket", model.ticket, null, dt, System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
        }

        /// <summary>  
        /// DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time"> DateTime时间格式</param>  
        /// <returns>Unix时间戳格式</returns>  
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

    }
    public class TicketModel
    {
        public string errcode;

        public string errmsg;

        public string ticket;

        public string expires_in;
    }
}

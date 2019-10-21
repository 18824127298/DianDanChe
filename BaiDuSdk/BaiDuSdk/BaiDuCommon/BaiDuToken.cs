using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using CheDaiBaoWeChatModel;
using System.Web;
using System.Web.Caching;
using BaiDuSdk.Util;
namespace BaiDuSdk.BaiDuCommon
{
    public static class BaiDuToken
    {
        // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
        // 返回token示例
        public static String TOKEN = Configs.GetBaiduToken();

        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        private static String clientId = Configs.GetAPIKey();
        // 百度云中开通对应服务应用的 Secret Key
        private static String clientSecret = Configs.GetSecretKey();

        public static String getAccessToken()
        {
            if (HttpContext.Current.Cache["BaiDuToken"] != null)
            {
                return HttpContext.Current.Cache["BaiDuToken"].ToString();
            }
            else
            {
                String authHost = "https://aip.baidubce.com/oauth/2.0/token";
                HttpClient client = new HttpClient();
                List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
                paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
                paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
                HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
                String result = response.Content.ReadAsStringAsync().Result;
                string sAccessToken = Expansions.GetJsonValue(result, "access_token");
                string sExpiresIn = Expansions.GetJsonValue(result, "expires_in");
                HttpContext.Current.Cache.Insert("BaiDuToken", sAccessToken, null, DateTime.Now.AddSeconds(Convert.ToInt32(sExpiresIn)), Cache.NoSlidingExpiration);
                return sAccessToken;
            }
        }

    }
}

using Sigbit.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CheDaiBaoCommonService.Service
{
    public class WeChatBaseRequestService
    {

        #region 请求Url，发送数据
        /// <summary>
        /// 请求Url，发送数据
        /// </summary>
        public static string PostUrl(string url, string postData)
        {
            byte[] data = Encoding.UTF8.GetBytes(postData);

            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            Stream outstream = request.GetRequestStream();
            outstream.Write(data, 0, data.Length);
            outstream.Close();

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            return content;
        }
        #endregion

        #region 获取Json字符串某节点的值
        /// <summary>
        /// 获取Json字符串某节点的值
        /// </summary>
        public static string GetJsonValue(string jsonStr, string key)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(jsonStr))
            {
                key = "\"" + key.Trim('"') + "\"";
                int index = jsonStr.IndexOf(key) + key.Length + 1;
                if (index > key.Length + 1)
                {
                    //先截逗号，若是最后一个，截“｝”号，取最小值
                    int end = jsonStr.IndexOf(',', index);
                    if (end == -1)
                    {
                        end = jsonStr.IndexOf('}', index);
                    }

                    result = jsonStr.Substring(index, end - index);
                    result = result.Trim(new char[] { '"', ' ', '\'' }); //过滤引号或空格
                }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 根据xml字符串读节点
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xmlElementName"></param>
        /// <returns></returns>
        public string ReadXmlElement(string xml, string xmlElementName)
        {
            XElement element = XElement.Parse(xml);
            return element.Element(xmlElementName).Value;
        }

        #region 请求Url
        #region 请求Url，不发送数据
        /// <summary>
        /// 请求Url，不发送数据
        /// </summary>
        public static string RequestUrl(string url)
        {
            return RequestUrl(url, "POST");
        }
        #endregion
        #endregion

        #region 请求Url，不发送数据
        /// <summary>
        /// 请求Url，不发送数据
        /// </summary>
        public static string RequestUrl(string url, string method)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = method;
            request.ContentType = "text/html";
            request.Headers.Add("charset", "utf-8");

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            return content;
        }
        #endregion

        //微信文本格式
        public string ReplyText(string fromUserName, string toUserName, string createTime, string stext)
        {
            return string.Format(@"<xml>
                <ToUserName><![CDATA[{0}]]></ToUserName>
                <FromUserName><![CDATA[{1}]]></FromUserName>
                <CreateTime>{2}</CreateTime>
                <MsgType><![CDATA[{3}]]></MsgType>
                </xml>", fromUserName, toUserName, createTime, stext);
        }

        //微信图文格式
        public string ReplyTuwenText(string fromUserName, string toUserName,
            string createTime, string sTitle, string sDescription, string sUrl, string sPicUrl)
        {
            return string.Format(@"
                     <xml>
                     <ToUserName><![CDATA[{0}]]></ToUserName>
                     <FromUserName><![CDATA[{1}]]></FromUserName>
                     <CreateTime>{2}</CreateTime>
                     <MsgType><![CDATA[{3}]]></MsgType>
                     <ArticleCount>1</ArticleCount>
                     <Articles>
                     <item>
                     <Title><![CDATA[{4}]]></Title> 
                     <Description><![CDATA[{5}]]></Description>
                     <PicUrl><![CDATA[{6}]]></PicUrl>
                     <Url><![CDATA[{7}]]></Url>
                     </item>
                     </Articles>
                     </xml>", fromUserName, toUserName, createTime, "news", sTitle, sDescription, sPicUrl, sUrl);
        }

        //#region 拼接图文消息素材Json字符串
        ///// <summary>
        ///// 拼接图文消息素材Json字符串
        ///// </summary>
        //public static string GetArticlesJsonStr(PageBase page, string access_token, DataTable dt)
        //{
        //    StringBuilder sbArticlesJson = new StringBuilder();

        //    sbArticlesJson.Append("{\"articles\":[");
        //    int i = 0;
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        string path = page.MapPath(dr["ImgUrl"].ToString());
        //        if (!File.Exists(path))
        //        {
        //            return "{\"code\":0,\"msg\":\"要发送的图片不存在\"}";
        //        }
        //        string msg = WXApi.UploadMedia(access_token, "image", path); // 上图片返回媒体ID
        //        string media_id = Tools.GetJsonValue(msg, "media_id");
        //        sbArticlesJson.Append("{");
        //        sbArticlesJson.Append("\"thumb_media_id\":\"" + media_id + "\",");
        //        sbArticlesJson.Append("\"author\":\"" + dr["Author"].ToString() + "\",");
        //        sbArticlesJson.Append("\"title\":\"" + dr["Title"].ToString() + "\",");
        //        sbArticlesJson.Append("\"content_source_url\":\"" + dr["TextUrl"].ToString() + "\",");
        //        sbArticlesJson.Append("\"content\":\"" + dr["Content"].ToString() + "\",");
        //        sbArticlesJson.Append("\"digest\":\"" + dr["Content"].ToString() + "\",");
        //            sbArticlesJson.Append("\"show_cover_pic\":\"1\"}");
        //    }
        //    sbArticlesJson.Append("]}");

        //    return sbArticlesJson.ToString();
        //}
        //#endregion

        #region 上传图文消息素材返回media_id
        /// <summary>
        /// 上传图文消息素材返回media_id
        /// </summary>
        public static string UploadNews(string access_token, string postData)
        {
            return PostUrl(string.Format("https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token={0}", access_token), postData);
        }
        #endregion

        /// <summary>
        /// 当前时间
        /// </summary>
        /// <returns></returns>
        public string GetNowTime()
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);
            long a = (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000;
            return a.ToString();
        }

        private static string apptoken = null;
        public static string getApptoken()
        {
            WeiXinAuthenticationService WeiXinBase = new WeiXinAuthenticationService();
            if (apptoken == null)
            {
                apptoken = WeiXinBase.GetToken();
            }
            if (WeiXinBase.TokenExpired(apptoken))
            {
                apptoken = WeiXinBase.GetToken();
            }
            return apptoken;
        }
    }
}


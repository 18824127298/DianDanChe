using CheDaiBaoWeChatModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CheDaiBaoWeChatServicee.Service
{
    public class FullNameVerify
    {
        public string Verify(string sFullName, string sIDNumber, int nGodId, string sTimeNow)
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("charset", "UTF-8");
            sParaTemp.Add("idcard", sIDNumber);
            sParaTemp.Add("merchantCode", Configs.GetMerchantNo());
            sParaTemp.Add("productCode", "P000001");
            sParaTemp.Add("realName", sFullName);
            sParaTemp.Add("tradeNo", nGodId + sTimeNow);
            sParaTemp.Add("tradeTime", sTimeNow);
            sParaTemp.Add("version", "1.0.0");
            StringBuilder content = new StringBuilder();
            StringBuilder sData = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in sParaTemp)
            {

                if (temp.Key == "realName")
                {
                    content.Append("&" + temp.Key + "=" + HttpUtility.UrlDecode(temp.Value));
                    sData.Append("&" + temp.Key + "=" + HttpUtility.UrlEncode(temp.Value, Encoding.UTF8));
                }
                else
                {
                    content.Append("&" + temp.Key + "=" + temp.Value);
                    sData.Append("&" + temp.Key + "=" + temp.Value);
                }
            }
            content.Append("&signkey=" + Configs.GetMiShi());
            string signSrc = content.ToString().TrimStart(new char[] { '&' });
            string signature = Md5Encrypt(signSrc);
            sParaTemp.Add("signature", signature);
            sData.Append("&signature=" + signature);
            return PostUrl("http://auth2.payadd.cn/Polymer/auth", sData.ToString().TrimStart(new char[] { '&' }));
        }

        public string SignCheck(string sRespDesc, string sRespCode)
        {
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();

            sParaTemp.Add("respDesc", sRespDesc);
            sParaTemp.Add("respCode", sRespCode);

            StringBuilder content = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in sParaTemp)
            {
                content.Append("&" + temp.Key + "=" + temp.Value);
            }
            content.Append("&signkey=" + Configs.GetMiShi());
            string signSrc = content.ToString().TrimStart(new char[] { '&' });
            return Md5Encrypt(signSrc);
        }

        #region 请求Url，发送数据
        /// <summary>
        /// 请求Url，发送数据
        /// </summary>
        public string PostUrl(string url, string postData)
        {
            byte[] data = Encoding.GetEncoding("utf-8").GetBytes(postData);

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

        private static string Md5Encrypt(string strToBeEncrypt)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] FromData = System.Text.Encoding.GetEncoding("utf-8").GetBytes(strToBeEncrypt);
            Byte[] TargetData = md5.ComputeHash(FromData);
            string Byte2String = "";
            for (int i = 0; i < TargetData.Length; i++)
            {
                Byte2String += TargetData[i].ToString("x2");
            }
            return Byte2String.ToLower();
        }


    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiandancheCommonCore
{
    public class SendHttp
    {
        # region 短信
        String username = "gzyswl";//配置短信帐号
        String passwd = "BaoStep@#2018";//配置短信密码
        /**
                 * 发送短信
                 * phone 为手机号码，多个号码以","号隔开
                 * msg为短信内容，须加上签名，例如"【微软科技】"，短信内容为utf-8编码，有特殊符号请urlencode内容
                 * needstatus是否需要推送短信值为true或false;
                 * port 端口，默认为空
                 * sendtime发送时间，为空即马上发送，格式例如："2016-12-12 12:12:12" 这样的标准格式
                 **/
        public String sendMsg(String phone, String msg, String needstatus, String port, String sendtime)
        {
           return RequestData("http://www.qybor.com:8500/shortMessage", "username=" + this.username + "&passwd=" + this.passwd + "&phone=" + phone + "&msg=" + msg + "&needstatus=" + needstatus + "&port=" + port + "&sendtime=" + sendtime);
        }

        public string RequestData(string POSTURL, string PostData)
        {
            //发送请求的数据
            WebRequest myHttpWebRequest = WebRequest.Create(POSTURL);
            myHttpWebRequest.Method = "POST";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byte1 = encoding.GetBytes(PostData);
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = byte1.Length;
            Stream newStream = myHttpWebRequest.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            newStream.Close();

            //发送成功后接收返回的XML信息
            HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            string lcHtml = string.Empty;
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, enc);
            lcHtml = streamReader.ReadToEnd();
            return lcHtml;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;

using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Runtime.Serialization;

namespace Sigbit.Lib.Translator.Microsoft
{    
    public class AdmAccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string scope { get; set; }
    }

    public class AdmAuthentication
    {
        public static readonly string DatamarketAccessUri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
        private string clientId;
        private string cientSecret;
        private string request;

        public AdmAuthentication(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.cientSecret = clientSecret;
            //If clientid or client secret has special characters, encode before sending request
            this.request = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret));
        }

        public AdmAccessToken GetAccessToken()
        {
            return HttpPost(DatamarketAccessUri, this.request);
        }

        private AdmAccessToken HttpPost(string DatamarketAccessUri, string requestDetails)
        {
            //Prepare OAuth request 
            WebRequest webRequest = WebRequest.Create(DatamarketAccessUri);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
            webRequest.ContentLength = bytes.Length;
            
            using (Stream outputStream = webRequest.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }

            using (WebResponse webResponse = webRequest.GetResponse())
            {
                Stream dataStream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);

                string s = reader.ReadToEnd();
                //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AdmAccessToken));
                //Get deserialized object from JSON stream
                //AdmAccessToken token = (AdmAccessToken)serializer.ReadObject(webResponse.GetResponseStream());

                AdmAccessToken token = ExtractAccessTokenFromJsonString(s);
                return token;
            }
        }

        /// <summary>
        /// 从Json格式的包中解析出Token
        /// </summary>
        private AdmAccessToken ExtractAccessTokenFromJsonString(string sJsonString)
        {
            if (!sJsonString.Contains("{"))
                throw new Exception("Json string format error : " + sJsonString);

            sJsonString = sJsonString.Trim().Replace("{", "").Replace("}", "");

            string[] arrParam = sJsonString.Split(',');
            AdmAccessToken token = new AdmAccessToken();

            for (int i = 0; i < arrParam.Length; i++)
            {
                string sParam = arrParam[i].Replace("\"", "");
                string sParamName = sParam.Substring(0, sParam.IndexOf(':'));
                string sParamValue = sParam.Substring(sParam.IndexOf(':') + 1);

                switch (sParamName)
                {
                    case "token_type":
                        token.token_type = sParamValue;
                        break;
                    case "access_token":
                        token.access_token = sParamValue;
                        break;
                    case "expires_in":
                        token.expires_in = sParamValue;
                        break;
                    case "scope":
                        token.scope = sParamValue;
                        break;
                }
            }

            return token;
        }
    }    
}

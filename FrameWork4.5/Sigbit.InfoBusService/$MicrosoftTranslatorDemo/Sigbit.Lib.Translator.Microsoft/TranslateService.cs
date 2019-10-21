using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Media;

namespace Sigbit.Lib.Translator.Microsoft
{
    /// <summary>
    /// 翻译语言
    /// </summary>
    public class TranslateLanguage
    {
        /// <summary>
        /// 英文
        /// </summary>
        public const string English = "en";
        /// <summary>
        /// 简体中文
        /// </summary>
        public const string SimplifiedChinese = "zh-CN";
        /// <summary>
        /// 繁体中文
        /// </summary>
        public const string TraditionalChinese = "zh-TW";
        /// <summary>
        /// 法文
        /// </summary>
        public const string French = "fr";
        /// <summary>
        /// 德文
        /// </summary>
        public const string Deutsch = "de";
        /// <summary>
        /// 西班牙文
        /// </summary>
        public const string Spanish = "es";
        /// <summary>
        /// 日文
        /// </summary>
        public const string Japanese = "ja";
        /// <summary>
        /// 韩文
        /// </summary>
        public const string Korean = "ko";
    }

    /// <summary>
    /// 朗读文件的格式
    /// </summary>
    public enum SpeakResultFormat
    {
        /// <summary>
        /// wav格式
        /// </summary>
        WAV,
        /// <summary>
        /// mp3格式
        /// </summary>
        MP3
    }

    /// <summary>
    /// 朗读文件的质量
    /// </summary>
    public enum SpeakResultQuality
    {
        /// <summary>
        /// 最高质量
        /// </summary>
        MaxQuality,
        /// <summary>
        /// 最小体积
        /// </summary>
        MinSize
    }

    public class TranslateService
    {

        private string clientId = "";
        private string clientSecret = "";
        private AdmAccessToken accessToken = null;
        private DateTime datetimeGetTonken;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sClientId">微软服务参数</param>
        /// <param name="sClientSecret">微软服务参数</param>
        public TranslateService(string sClientId, string sClientSecret)
        {
            clientId = sClientId;
            clientSecret = sClientSecret;
        }

        /// <summary>
        /// 获取新的token
        /// </summary>
        private void AuthenNewAccessToken()
        {
            AdmAuthentication auth = new AdmAuthentication(clientId, clientSecret);
            datetimeGetTonken = DateTime.Now;
            accessToken = auth.GetAccessToken();

            if (accessToken.access_token == "")
                throw new Exception("Authentication new access token fail");
        }

        /// <summary>
        /// 获取token
        /// </summary>
        public string GetAccessToken()
        {
            if (accessToken == null)
                AuthenNewAccessToken();

            TimeSpan timeSpan = DateTime.Now - datetimeGetTonken;
            if ((int)timeSpan.TotalSeconds > Convert.ToInt32(accessToken.expires_in) * 0.8)
                AuthenNewAccessToken();

            return "Bearer " + accessToken.access_token;
        }

        /// <summary>
        /// 检测语言类型
        /// </summary>
        public string DetectLanguage(string sText)
        {
            string textToDetect = sText;
            //Keep appId parameter blank as we are sending access token in authorization header.
            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Detect?text=" + textToDetect;

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Authorization", GetAccessToken());
            WebResponse response = null;
            string languageDetected = "";
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);

                    languageDetected = ExtractResponseValue(reader.ReadToEnd());
                }
            }

            catch
            {
                throw;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }

            return languageDetected;
        }

        /// <summary>
        /// 翻译文本
        /// </summary>
        /// <param name="sText">翻译的内容</param>
        /// <param name="sFromLanguage">来源语言</param>
        /// <param name="sToLanguage">目标语言</param>
        /// <returns></returns>
        public string Translate(string sText, string sFromLanguage, string sToLanguage)
        {
            string textToDetect = sText;

            //Keep appId parameter blank as we are sending access token in authorization header.
            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" 
                        + textToDetect + "&from=" + sFromLanguage + "&to=" + sToLanguage;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Authorization", GetAccessToken());
            WebResponse response = null;
            string translateResult = "";
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);

                    translateResult = ExtractResponseValue(reader.ReadToEnd());
                }
            }

            catch
            {
                throw;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }

            return translateResult;
        }

        /// <summary>
        /// 朗读文本
        /// </summary>
        /// <remarks>默认WAV格式和最好质量</remarks>
        public bool Speak(string sText, string sLanguage, string sSaveFileName)
        {
            return Speak(sText, sLanguage, sSaveFileName, SpeakResultFormat.WAV, SpeakResultQuality.MaxQuality);
        }


        /// <summary>
        /// 朗读文本
        /// </summary>
        /// <param name="sText">朗读的内容</param>
        /// <param name="sLanguage">朗读的语言</param>
        /// <param name="sSaveFileName">保存文件的全路径</param>
        /// <param name="format">语音格式</param>
        /// <param name="quality">语音质量</param>
        /// <remarks>最大保存10M文件</remarks>
        public bool Speak(string sText, string sLanguage,string sSaveFileName, SpeakResultFormat format, SpeakResultQuality quality)
        {
            string sFileFormat = "audio/mp3";
            if (format == SpeakResultFormat.WAV)
                sFileFormat = "audio/wav";

            string sFileQuality = "MinSize";
            if (quality == SpeakResultQuality.MaxQuality)
                sFileQuality = "MaxQuality";

            //Keep appId parameter blank as we are sending access token in authorization header.
            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Speak?text="
                        + sText + "&language=" + sLanguage + "&format=" + sFileFormat + "&options=" + sFileQuality;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Authorization", GetAccessToken());
            WebResponse response = null;
            string translateResult = "";
            try
            {
                response = httpWebRequest.GetResponse();
                Stream stream = response.GetResponseStream();

                //MemoryStream memStream = new MemoryStream();
                //memStream.Seek(0, SeekOrigin.Begin);

                //BinaryReader reader = new BinaryReader(stream);

                BinaryReader reader = new BinaryReader(stream);
                byte[] buffer = reader.ReadBytes(10485760);
                //byte[] buffer = new byte[4096];
                //int nIndex = 0;
                //while (true)
                //{
                //    buffer[nIndex] = stream.Read
                //    if ((buffer[nIndex] == -1) || (nIndex >= buffer.Length))
                //        break;
                //}

                if (File.Exists(sSaveFileName))
                    File.Delete(sSaveFileName);

                FileStream writer = new FileStream(sSaveFileName, FileMode.CreateNew, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(writer);

                bw.Write(buffer);
                bw.Close();
                writer.Close();

                return true;
            }

            catch
            {
                throw;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }

            return false;
        }

        /// <summary>
        /// 解析出应答的值
        /// </summary>
        /// <remarks>前后可能带有xml的tag</remarks>
        private string ExtractResponseValue(string sResponseContent)
        {
            //<string xmlns="http://schemas.microsoft.com/2003/10/Serialization/">en</string>

            string sString = sResponseContent.Trim();

            while (true)
            {
                int nIndex = sString.IndexOf('<');
                if (nIndex < 0)
                    break;

                int nIndexLast = sString.IndexOf('>', nIndex);
                if (nIndexLast < 0)
                    break;

                sString = sString.Substring(0, nIndex) + sString.Substring(nIndexLast + 1);
            }

            return sString;
        }
    }
}

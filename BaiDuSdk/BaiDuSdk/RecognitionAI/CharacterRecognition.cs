using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Web;
using BaiDuSdk.Util;
using BaiDuSdk.BaiDuCommon;
using CheDaiBaoWeChatModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using BaiDuSdk.Model;
using Newtonsoft.Json.Linq;

namespace BaiDuSdk.RecognitionAI
{
    public class CharacterRecognition
    {
        // 身份证识别
        //public static string Idcard(string sPicture)
        //{
        //    string strbaser64 = PictureUtil.FileToBase64(sPicture); // 图片的base64编码
        //    string token = BaiDuToken.getAccessToken();
        //    string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/idcard?access_token=" + token;
        //    Encoding encoding = Encoding.Default;
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
        //    request.Method = "post";
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    request.KeepAlive = true;
        //    String str = "id_card_side=front&image=" + HttpUtility.UrlEncode(strbaser64);
        //    byte[] buffer = encoding.GetBytes(str);
        //    request.ContentLength = buffer.Length;
        //    request.GetRequestStream().Write(buffer, 0, buffer.Length);
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
        //    string result = reader.ReadToEnd();
        //    string newcontent = Unicode2String(result);
        //    return result;
        //}

        //public static string Unicode2String(string source)
        //{
        //    return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
        //                 source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        //}


        private static String clientId = Configs.GetAPIKey();
        private static String clientSecret = Configs.GetSecretKey();

        public static IdCardNumber IdcardDistinguish(string sPicture, string idCardSide)
        {
            var client = new Baidu.Aip.Ocr.Ocr(clientId, clientSecret);
            client.Timeout = 60000;  // 修改超时时间
            using (FileStream fs = new FileStream(sPicture, FileMode.Open, FileAccess.Read))
            {
                byte[] byteArray = new byte[fs.Length];
                fs.Read(byteArray, 0, byteArray.Length);

                // 调用身份证识别，可能会抛出网络等异常，请使用try/catch捕获
                //var result = client.Idcard(byteArray, idCardSide);
                //Console.WriteLine(result);
                // 如果有可选参数
                var options = new Dictionary<string, object>{
                      {"detect_direction", "true"},
                      {"detect_risk", "false"}
                 };
                // 带参数调用身份证识别
                var result = client.Idcard(byteArray, idCardSide, options);

                IdCardNumber idCardNumber = JsonConvert.DeserializeObject<IdCardNumber>(result.ToString());

                return idCardNumber;

            }

        }

        public static BankCardNumber BankcardDistinguish(string sBankCardPath)
        {
            var client = new Baidu.Aip.Ocr.Ocr(clientId, clientSecret);
            client.Timeout = 60000;  // 修改超时时间
            var image = File.ReadAllBytes(sBankCardPath);
            var result = client.Bankcard(image);

            BankCardNumber bankcardNumber = JsonConvert.DeserializeObject<BankCardNumber>(result.ToString());
            return bankcardNumber;
        }



        public static string ReadImg(string img)
        {
            return Convert.ToBase64String(File.ReadAllBytes(img));
        }

        public static FaceContrastResult FaceContrast(string image1, string image2)
        {
            var client = new Baidu.Aip.Face.Face("PXCG8yzMy9x6dff10jirQb6x", "vGz2ceiC3zrMb3EgAZ2VFcW57INgoFnH");
            client.Timeout = 60000;
            var faces = new JArray
             {
                 new JObject
                 {
                     {"image", ReadImg(image1)},
                     {"image_type", "BASE64"},
                     {"face_type", "LIVE"},
                     {"quality_control", "LOW"},
                     {"liveness_control", "NONE"},
                 },
                 new JObject
                 {
                     {"image", ReadImg(image2)},
                     {"image_type", "BASE64"},
                     {"face_type", "LIVE"},
                     {"quality_control", "LOW"},
                     {"liveness_control", "NONE"},
                 }
             };
            var result = client.Match(faces);
            FaceContrastResult faceContrastResult = JsonConvert.DeserializeObject<FaceContrastResult>(result.ToString());
            return faceContrastResult;
        }

        public static PersonVerify PersonVerify(string image, string imageType, string idCardNumber, string name)
        {
            var client = new Baidu.Aip.Face.Face(clientId, clientSecret);
            client.Timeout = 60000;  // 修改超时时间

            // 调用身份验证，可能会抛出网络等异常，请使用try/catch捕获
            //var result = client.PersonVerify(image, imageType, idCardNumber, name);
            //Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
	                       {"quality_control", "NORMAL"},
	                       {"liveness_control", "LOW"}
	                   };
            // 带参数调用身份验证
            var result = client.PersonVerify(image, imageType, idCardNumber, name, options);
            PersonVerify personVerify = JsonConvert.DeserializeObject<PersonVerify>(result.ToString());
            return personVerify;
        }
    }
}

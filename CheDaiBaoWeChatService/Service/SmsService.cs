using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Security;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using CheDaiBaoWeChatModel;
using Yibai.Api.Domain;
using Newtonsoft.Json;
using Yibai.Api.Util;
using Yibai.Api.Common;
using CheDaiBaoCommonService.Caching;
using CheDaiBaoCommonService.Expansion;
using CheDaiBaoWeChatController.Interface;
namespace CheDaiBaoWeChatService.Service
{
    public class SmsService
    {
        private readonly MemoryCacheManager _memoryCacheManager;
        private readonly OperaService _operaService;
        public SmsService()
        {
            _memoryCacheManager = new MemoryCacheManager();
            _operaService = new OperaService();
        }

        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="sendPhoneCodeType"></param>
        /// <param name="ip"></param>
        /// <param name="SendSmsType"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public SendMobileResultModel SendMobileCodeOfSms(string mobileNumber)
        {
            string cacheKey = mobileNumber;
            if (_memoryCacheManager.IsSet(cacheKey))
            {
                MobileCodeInfoModel sesstionValue = _memoryCacheManager.Get<MobileCodeInfoModel>(cacheKey);
                string sPhone = mobileNumber;
                string sContent = "你的验证码是" + sesstionValue.MobileCode + "【车1号】";

                QiyebaoSms qiyebaoSms = new QiyebaoSms();
                if (!qiyebaoSms.SendSms(sPhone, sContent))
                {
                    return new SendMobileResultModel()
                    {
                        Result = false,
                        Message = "验证码发送失败,请重试",
                        WaitOfSecond = 0
                    };
                }

                return new SendMobileResultModel()
                {
                    Result = true,
                    Message = string.Format(
               "验证码已经发送到手机号为{0},注意查收,验证码有效时间为{1}分钟{2}",
               mobileNumber.EncruptionSectionMobile(), Configs.GetMobileCodeExpires() / 60,
               Configs.GetIsSendSms() ? "" : "验证码：" + sesstionValue.MobileCode),
                    WaitOfSecond = Configs.GetMobileCodeExpires()
                };
            }

            string code = DateTime.Now.Ticks.ToString().Substring(14);

            if (Configs.GetIsSendSms())
            {

                string sPhone = mobileNumber;
                string sContent = "你的验证码是" + code + "【车1号】";

                QiyebaoSms qiyebaoSms = new QiyebaoSms();
                if (!qiyebaoSms.SendSms(sPhone, sContent))
                {
                    return new SendMobileResultModel()
                    {
                        Result = false,
                        Message = "验证码发送失败,请重试",
                        WaitOfSecond = 0
                    };
                }

            }
            var cacheValue = new MobileCodeInfoModel()
            {
                MobileCode = code,
                MobileCodeExpires = DateTime.Now.AddSeconds(Configs.GetMobileCodeExpires()),
                MobileNumber = mobileNumber
            };

            _memoryCacheManager.Set(cacheKey, cacheValue, Configs.GetMobileCodeExpires());

            string message = string.Format(
                "验证码已经发送到手机号为{0},注意查收,验证码有效时间为{1}分钟{2}",
                mobileNumber.EncruptionSectionMobile(), Configs.GetMobileCodeExpires() / 60,
                Configs.GetIsSendSms() ? "" : "验证码：" + code);

            return new SendMobileResultModel()
            {
                Result = true,
                Message = message,
                WaitOfSecond = Configs.GetMobileCodeExpires()
            };
        }

        //public bool sendsms(string sPhone, string sContent)
        //{
        //    try
        //    {
        //        List<SmsSubmit> smsSubmits = new List<SmsSubmit>();
        //        SmsSubmit smsSubmit1 = new SmsSubmit(sPhone, sContent);
        //        smsSubmits.Add(smsSubmit1);
        //        String apikey = "f75306c4a60147689b9e630aa35db3c3";
        //        string sjon = JsonConvert.SerializeObject(smsSubmits).Replace('\"', '\'');
        //        List<SmsSubmitJson> smsSubmitJsonList = new List<SmsSubmitJson>();
        //        SmsSubmitJson smsSubmitJson = new SmsSubmitJson(apikey, sjon);
        //        string sresult = JsonConvert.SerializeObject(smsSubmitJson).Replace('\'', '"').Replace("\"[", "[").Replace("]\"", "]");

        //         HttpUtils.PostJson("https://sms.100sms.cn/api/sms/batchSubmit", sresult);
        //         return true;
        //    }
        //    catch (YibaiApiException e)
        //    {
        //        Console.WriteLine("YibaiApiException, code: " + e.code + ", message: " + e.message);
        //        return false;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return false;
        //    }
        //}

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">原内容</param>
        /// <returns>加密后内容</returns>
        public static string MD5Encrypt(string source)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(source));

            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }


        /// <summary>
        /// 验证手机号码
        /// </summary>
        /// <param name="mobileCode"></param>
        /// <param name="sendPhoneCodeType"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="isRemoveCache">是否移除验证码缓存</param>
        /// <returns></returns>
        public bool ValidateMobileCode(
            string mobileCode,
            string mobileNumber,
            bool isRemoveCache = true)
        {
            mobileNumber = mobileNumber.Trim();
            string cacheKey = mobileNumber;
            if (!_memoryCacheManager.IsSet(cacheKey))
            {
                return false;
            }

            MobileCodeInfoModel sesstionValue = _memoryCacheManager.Get<MobileCodeInfoModel>(cacheKey);

            if (sesstionValue.MobileCode.ToLower() != mobileCode.ToLower())
            {
                return false;
            }

            if (sesstionValue.MobileNumber != mobileNumber)
            {
                return false;
            }

            if (isRemoveCache)
            {
                _memoryCacheManager.Remove(cacheKey);
            }
            return true;
        }
    }
}

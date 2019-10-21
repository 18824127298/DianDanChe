using CheDaiBaoCommonController.Controllers;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CheDaiBaoWeChatController.Controllers
{
    public class FLSendCodeController : BaseCommonController
    {
        [HttpPost]
        #region 短信验证码
        public ActionResult FLSendCode(string msisdn)
        {
            string mobileNumber = msisdn;

            SendMobileResultModel sendMobileResult = new SendMobileResultModel()
            {
                Result = false,
                Message = "出现问题",
                WaitOfSecond = 0
            };
            try
            {
                SmsService smsService = new SmsService();
                sendMobileResult = smsService.SendMobileCodeOfSms(mobileNumber);

                return Json(sendMobileResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                sendMobileResult.Message = ex.Message;
                return Json(sendMobileResult, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
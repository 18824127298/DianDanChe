using CheDaiBaoCommonService.Service;
using Sigbit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatService.Interface
{
    public class WechatPushMessage
    {

        //还款成功通知
        public string RepaymentSuccess(string sName, string sOpenId, string keyword1, string keyword2)
        {
            string apptoken = WeChatBaseRequestService.getApptoken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", apptoken);
            string sContent = "{\"touser\":\"" + sOpenId + "\",\"template_id\":\"fpNDIbYFKkXHK_gQ7C_gKv7PSUc7sUqJMv_u8QxZkA\",\"url\":\"\",\"topcolor\":\"#FF0000\"," +
                "\"data\":{\"first\": {\"value\":\"尊敬的" + sName + "，您在车一号融资租赁业务已扣款成功\",\"color\":\"#173177\"},\"remark\": {\"value\":\"感谢您的支持。\",\"color\":\"#173177\"}," +
                "\"keyword1\": {\"value\":\"" + keyword1 + "\",\"color\":\"#173177\"},\"keyword2\": {\"value\":\"" + keyword2 + "\",\"color\":\"#173177\"}}}";
            string userinfo = WeChatBaseRequestService.PostUrl(url, sContent);
            DebugLogger.LogDebugMessage(userinfo);
            return userinfo;
        }


        //分期还款提醒
        public string InstallationReminder(string sName, string sOpenId, string sMonth, string keyword1, string keyword2,
             string keyword3, string keyword4)
        {
            string apptoken = WeChatBaseRequestService.getApptoken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", apptoken);

            string sContent = "{\"touser\":\"" + sOpenId + "\",\"template_id\":\"DriyIRlHcAg35rf_IJ9QO9wzGp5FUZhnrVIR2lrVFS0\",\"url\":\"\",\"topcolor\":\"#FF0000\"," +
                "\"data\":{\"first\": {\"value\":\"尊敬的" + sName + "，您" + sMonth + "月份应还款：\",\"color\":\"#173177\"},\"remark\": {\"value\":\"如已还款本信息仅供参考\",\"color\":\"#173177\"}," +
                "\"keyword1\": {\"value\":\"" + keyword1 + "\",\"color\":\"#173177\"},\"keyword2\": {\"value\":\"" + keyword2 + "\",\"color\":\"#173177\"}," +
                "\"keyword3\": {\"value\":\"" + keyword3 + "\",\"color\":\"#173177\"},\"keyword4\": {\"value\":\"" + keyword4 + "\",\"color\":\"#173177\"}}}";
            string userinfo = WeChatBaseRequestService.PostUrl(url, sContent);
            DebugLogger.LogDebugMessage(userinfo);
            return userinfo;
        }

        //逾期还款提醒
        public string OverdueReminder(string sName, string sOpenId, string keyword1, string keyword2,
             string keyword3, string keyword4, string keyword5)
        {
            string apptoken = WeChatBaseRequestService.getApptoken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", apptoken);

            string sContent = "{\"touser\":\"" + sOpenId + "\",\"template_id\":\"PbgSGResIH5EfveWmtxuqFVAucPWlUzmwkWIPXLElus\",\"url\":\"\",\"topcolor\":\"#FF0000\"," +
                "\"data\":{\"first\": {\"value\":\"尊敬的" + sName + "，您在广州翼速融资租赁有限公司车1号【客服热线020-89851216】的电单车融资租赁业务已逾期\",\"color\":\"#173177\"},\"remark\": {\"value\":\"为避免产生逾期违约金费用，请尽快还款\",\"color\":\"#173177\"}," +
                "\"keyword1\": {\"value\":\"" + keyword1 + "\",\"color\":\"#173177\"},\"keyword2\": {\"value\":\"" + keyword2 + "\",\"color\":\"#173177\"}," +
                "\"keyword3\": {\"value\":\"" + keyword3 + "\",\"color\":\"#173177\"},\"keyword4\": {\"value\":\"" + keyword4 + "\",\"color\":\"#173177\"}," +
                "\"keyword5\": {\"value\":\"" + keyword5 + "\",\"color\":\"#173177\"}}}";
            string userinfo = WeChatBaseRequestService.PostUrl(url, sContent);
            DebugLogger.LogDebugMessage(userinfo);
            return userinfo;
        }

        //支付成功提醒
        public string SuccessfulReminder(string sName, string sOpenId, string keyword1, string keyword2, string keyword3)
        {
            string apptoken = WeChatBaseRequestService.getApptoken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", apptoken);

            string sContent = "{\"touser\":\"" + sOpenId + "\",\"template_id\":\"Ad4XyeOIfS_qOxKQN-ugRmPB3mnI7Az-m2ZfO1ZUwAY\",\"url\":\"\",\"topcolor\":\"#FF0000\"," +
                "\"data\":{\"first\": {\"value\":\"尊敬的" + sName + "，您已成功支付融资租赁费\",\"color\":\"#173177\"},\"remark\": {\"value\":\"感谢您的支持。\",\"color\":\"#173177\"}," +
                "\"keyword1\": {\"value\":\"" + keyword1 + "\",\"color\":\"#173177\"},\"keyword2\": {\"value\":\"" + keyword2 + "\",\"color\":\"#173177\"}," +
                "\"keyword3\": {\"value\":\"" + keyword3 + "\",\"color\":\"#173177\"}}}";
            string userinfo = WeChatBaseRequestService.PostUrl(url, sContent);
            DebugLogger.LogDebugMessage(userinfo);
            return userinfo;
        }


        //客户回款提醒
        public string CustomerReminder(string sOpenId, string keyword1, string keyword2, string keyword3)
        {
            string apptoken = WeChatBaseRequestService.getApptoken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", apptoken);

            string sContent = "{\"touser\":\"" + sOpenId + "\",\"template_id\":\"3oX1bSjKFyqvcDWKqFuDS0DSV6JoWM1Ksn839_nHAWw\",\"url\":\"\",\"topcolor\":\"#FF0000\"," +
                "\"data\":{\"first\": {\"value\":\"您好，客户已支付融资租赁费\",\"color\":\"#173177\"},\"remark\": {\"value\":\"请您及时登录系统查看是否到账核实。\",\"color\":\"#173177\"}," +
                "\"keyword1\": {\"value\":\"" + keyword1 + "\",\"color\":\"#173177\"},\"keyword2\": {\"value\":\"" + keyword2 + "\",\"color\":\"#173177\"}," +
                "\"keyword3\": {\"value\":\"" + keyword3 + "\",\"color\":\"#173177\"}}}";
            string userinfo = WeChatBaseRequestService.PostUrl(url, sContent);
            DebugLogger.LogDebugMessage(userinfo);
            return userinfo; 
        }


        //客户融资租赁审核结果通知
        public string NotificationResult(string sOpenId, string keyword1, string keyword2, string keyword3)
        {
            string apptoken = WeChatBaseRequestService.getApptoken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", apptoken);
            string sContent = "";
            if (keyword3 == "通过")
            {
                sContent = "{\"touser\":\"" + sOpenId + "\",\"template_id\":\"7qKoAaPtSqEqu4bPVTPrTFm6A_WF4fHWQoG3b2847zg\",\"url\":\"\",\"topcolor\":\"#FF0000\"," +
                    "\"data\":{\"first\": {\"value\":\"您好，您申请的融资租赁结果已出来\",\"color\":\"#173177\"},\"remark\": {\"value\":\"恭喜您，已通过审批，请于下个工作日登录公众号查看账单。\",\"color\":\"#173177\"}," +
                    "\"keyword1\": {\"value\":\"" + keyword1 + "\",\"color\":\"#173177\"},\"keyword2\": {\"value\":\"" + keyword2 + "\",\"color\":\"#173177\"}," +
                    "\"keyword3\": {\"value\":\"" + keyword3 + "\",\"color\":\"#173177\"}}}";
            }
            else
            {
                sContent = "{\"touser\":\"" + sOpenId + "\",\"template_id\":\"7qKoAaPtSqEqu4bPVTPrTFm6A_WF4fHWQoG3b2847zg\",\"url\":\"\",\"topcolor\":\"#FF0000\"," +
                   "\"data\":{\"first\": {\"value\":\"您好，您申请的融资租赁结果已出来\",\"color\":\"#173177\"},\"remark\": {\"value\":\"很抱歉，您申请的融资租赁业务未获批\",\"color\":\"#173177\"}," +
                   "\"keyword1\": {\"value\":\"" + keyword1 + "\",\"color\":\"#173177\"},\"keyword2\": {\"value\":\"" + keyword2 + "\",\"color\":\"#173177\"}," +
                   "\"keyword3\": {\"value\":\"" + keyword3 + "\",\"color\":\"#173177\"}}}";
            }
            string userinfo = WeChatBaseRequestService.PostUrl(url, sContent);
            DebugLogger.LogDebugMessage(userinfo);
            return userinfo;
        }



        //融资租赁审核处理提醒
        public string AuditProcessingReminder(string sOpenId, string keyword1, string keyword2, string keyword3)
        {
            string apptoken = WeChatBaseRequestService.getApptoken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", apptoken);

            string sContent = "{\"touser\":\"" + sOpenId + "\",\"template_id\":\"0ta44ft9YmsdOQILD88VM3-TA4A7Xxrh-dBBIZup-6E\",\"url\":\"\",\"topcolor\":\"#FF0000\"," +
                "\"data\":{\"first\": {\"value\":\"您好，有新客户申请融资租赁\",\"color\":\"#173177\"},\"remark\": {\"value\":\"请您及时审核。\",\"color\":\"#173177\"}," +
                "\"keyword1\": {\"value\":\"" + keyword1 + "\",\"color\":\"#173177\"},\"keyword2\": {\"value\":\"" + keyword2 + "\",\"color\":\"#173177\"}," +
                "\"keyword3\": {\"value\":\"" + keyword3 + "\",\"color\":\"#173177\"}}}";

            string userinfo = WeChatBaseRequestService.PostUrl(url, sContent);
            DebugLogger.LogDebugMessage(userinfo);
            return userinfo;
        }
    }
}

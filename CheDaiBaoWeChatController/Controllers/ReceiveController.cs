using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using CheDaiBaoCommonService.Service;
using Sigbit.Common;
using CheDaiBaoCommonController.Controllers;

namespace CommonMainSiteController.Controllers
{
    public class ReceiveController : BaseCommonController
    {
        public string Token = "chedaibao123";
        //[HttpGet]
        //public ActionResult Index(string signature, string timestamp, string nonce, string echostr)
        //{
        //    if (string.IsNullOrEmpty(Request.QueryString["echoStr"])) { Response.End(); }

        //    string echoStr = Request.QueryString["echoStr"].ToString();
        //    if (CheckSignature())
        //    {
        //        if (!string.IsNullOrEmpty(echoStr))
        //        {
        //            return Content(echostr);
        //        }
        //    }
        //    return Content("err");
        //}

        [HttpPost]
        public ActionResult Index(string signature, string timestamp, string nonce)
        {

            if (!CheckSignature())
            {
                return Content("");
            }
            WeChatBaseRequestService WeChatRequest = new WeChatBaseRequestService();
            Stream requestStream = System.Web.HttpContext.Current.Request.InputStream;
            byte[] requestByte = new byte[requestStream.Length];
            requestStream.Read(requestByte, 0, (int)requestStream.Length);
            string requestStr = Encoding.UTF8.GetString(requestByte);
            DebugLogger.LogDebugMessage(requestStr);
            string resxml = string.Empty;
            if (!string.IsNullOrEmpty(requestStr))
            {
                string xmltem = string.Empty;
                string toUserName = WeChatRequest.ReadXmlElement(requestStr, "ToUserName");
                string fromUserName = WeChatRequest.ReadXmlElement(requestStr, "FromUserName");
                string createTime = WeChatRequest.ReadXmlElement(requestStr, "CreateTime");
                string msgType = WeChatRequest.ReadXmlElement(requestStr, "MsgType");
                string Event = "";
                string eventKey = "";
                string sJson = "";
                if (msgType == "event")
                {
                    Event = WeChatRequest.ReadXmlElement(requestStr, "Event");
                    eventKey = WeChatRequest.ReadXmlElement(requestStr, "EventKey");
                }
                if (msgType == "text")
                {
                    string xml = string.Empty;
                    xml = WeChatRequest.ReplyText(fromUserName, toUserName, WeChatRequest.GetNowTime(), "transfer_customer_service");
                    return Content(xml);
                }
                if (Event == "subscribe")
                {
                    sJson = @"";
                    //WeChatBaseRequestService.PostUrl("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + WeChatBaseRequestService.getApptoken(), sJson);
                }
                if (Event == "SCAN")
                {
                    sJson = @"红海‘薪’时代
企事业员工的专属借款
满足员工消费需求，结婚、装修、购车、教育、旅游…
给员工最暖的福利和诚挚的帮助！
圆您的消费梦！
红海‘红薪宝’让员工借款更安心！
<a href='http://a1.rabbitpre.com/m/JZnFvMu6Y'>更多详情点我直达。</a>";
                    //WeChatBaseRequestService.PostUrl("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + WeChatBaseRequestService.getApptoken(), sJson);
                }
                DebugLogger.LogDebugMessage(Event + "," + eventKey + "," + fromUserName);
                if (Event == "CLICK")
                {
                    if (eventKey == "33")
                    {
                        sJson = "客官么么哒，如有任何问题，请在输入框内输入，我会立刻回答您哟！哦对了，人家的私人微信号“FLJF01”，朋友圈经常爆料~如遇紧急咨询，请拨打客服热线： 400-837-2223";
                        //WeChatBaseRequestService.PostUrl("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + WeChatBaseRequestService.getApptoken(), sJson);
                    }
                }
                resxml = "<xml><ToUserName><![CDATA[" + fromUserName + "]]></ToUserName><FromUserName><![CDATA[" + toUserName + "]]></FromUserName><CreateTime>" + WeChatRequest.GetNowTime() + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + sJson + "]]></Content><FuncFlag>0</FuncFlag></xml>";
            }

            return Content(resxml);
        }

        private bool CheckSignature()
        {
            string signature = Request.QueryString["signature"].ToString();
            string timestamp = Request.QueryString["timestamp"].ToString();
            string nonce = Request.QueryString["nonce"].ToString();
            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);　　 //字典排序  
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

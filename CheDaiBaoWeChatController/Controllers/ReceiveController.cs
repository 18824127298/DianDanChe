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
using CheDaiBaoWeChatService.Service;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatModel;

namespace CommonMainSiteController.Controllers
{
    public class ReceiveController : BaseCommonController
    {
        public string Token = "cheyihao123";
        [HttpGet]
        public ActionResult Index(string signature, string timestamp, string nonce, string echostr)
        {
            if (string.IsNullOrEmpty(Request.QueryString["echoStr"])) { Response.End(); }

            string echoStr = Request.QueryString["echoStr"].ToString();
            if (CheckSignature())
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    return Content(echostr);
                }
            }
            return Content("err");
        }

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
            if (!string.IsNullOrEmpty(requestStr))
            {
                string xmltem = string.Empty;
                string toUserName = WeChatRequest.ReadXmlElement(requestStr, "ToUserName");
                string fromUserName = WeChatRequest.ReadXmlElement(requestStr, "FromUserName");
                string createTime = WeChatRequest.ReadXmlElement(requestStr, "CreateTime");
                string msgType = WeChatRequest.ReadXmlElement(requestStr, "MsgType");
                string Event = "";
                string eventKey = "";
                if (msgType == "event")
                {
                    Event = WeChatRequest.ReadXmlElement(requestStr, "Event");
                    eventKey = WeChatRequest.ReadXmlElement(requestStr, "EventKey");
                }
                MemberService memberService = new MemberService();
                Member member = new Member();
                string apptoken = WeChatBaseRequestService.getApptoken();

                string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", apptoken, fromUserName);
                string userinfo = WeChatBaseRequestService.RequestUrl(url);
                string unionid = WeChatBaseRequestService.GetJsonValue(userinfo, "unionid");
                List<Member> memberList = memberService.Search(new Member { IsValid = true }).Where(o => o.OpenId == fromUserName).ToList();
                if (memberList.Count > 0)
                {
                    member = memberList.First();
                    if (!string.IsNullOrEmpty(member.Phone))
                    {
                        return Content("");
                    }
                }
                else
                {
                    member.OpenId = fromUserName;
                    int newMemberId = memberService.Insert(member);
                    member = memberService.GetById(newMemberId);
                }
                if (Event == "unsubscribe")
                {
                    string sJson = "{\"touser\": \"" + fromUserName + "\" ,\"msgtype\":\"text\",\"text\":{\"content\":\"车1号!\"}}";
                    WeChatBaseRequestService.PostUrl("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + WeChatBaseRequestService.getApptoken(), sJson);
                }
                if (Event == "subscribe")
                {
                    string stext = @"欢迎您关注车1号";
                    string sJson = "{\"touser\": \"" + fromUserName + "\" ,\"msgtype\":\"text\",\"text\":{\"content\":\"" + stext + "\"}}";
                    WeChatBaseRequestService.PostUrl("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + WeChatBaseRequestService.getApptoken(), sJson);

                    if (eventKey != "")
                    {
                        int nId = ConvertUtil.ToInt(eventKey.Split('_')[1]);
                        if (nId > 0)
                        {
                            if (member.Id != nId)
                            {
                                Member recommendMember = memberService.GetById(nId);
                                member.RecommendId = nId;
                                memberService.Update(member);
                                GetGodBouns(member.Id, member.OpenId);
                                sJson = "{\"touser\": \"" + recommendMember.OpenId + "\" ,\"msgtype\":\"text\",\"text\":{\"content\":\"亲，感谢您为我们推荐客户\"}}";
                                WeChatBaseRequestService.PostUrl("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + WeChatBaseRequestService.getApptoken(), sJson);
                            }
                        }
                    }
                }
                if (Event == "SCAN")
                {
                    string stext = @"欢迎您关注车1号";

                    string sJson = "{\"touser\": \"" + fromUserName + "\" ,\"msgtype\":\"text\",\"text\":{\"content\":\"" + stext + "\"}}";
                    WeChatBaseRequestService.PostUrl("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + WeChatBaseRequestService.getApptoken(), sJson);

                    if (eventKey != "")
                    {
                        int nId = ConvertUtil.ToInt(eventKey);
                        if (nId > 0)
                        {
                            if (member.Id != nId)
                            {
                                Member recommendMember = memberService.GetById(nId);
                                member.RecommendId = nId;
                                memberService.Update(member);
                                GetGodBouns(member.Id, member.OpenId);
                                sJson = "{\"touser\": \"" + recommendMember.OpenId + "\" ,\"msgtype\":\"text\",\"text\":{\"content\":\"亲，感谢您为我们推荐客户\"}}";
                                WeChatBaseRequestService.PostUrl("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + WeChatBaseRequestService.getApptoken(), sJson);
                            }
                        }
                    }
                }
                //    if (msgType == "text")
                //    {
                //        string xml = string.Empty;
                //        xml = WeChatRequest.ReplyText(fromUserName, toUserName, WeChatRequest.GetNowTime(), "transfer_customer_service");
                //        return Content(xml);
                //    }
                //    if (Event == "CLICK")
                //    {
                //        if (eventKey == "33")
                //        {
                //            DateTime DateTime1 = Convert.ToDateTime("2016-07-13");
                //            TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
                //            TimeSpan ts2 = new TimeSpan(DateTime1.Ticks);
                //            TimeSpan ts = ts1.Subtract(ts2).Duration();
                //            string sJson = "";
                //            if (ts.Hours % 3 == 1)
                //                sJson = "{\"touser\": \"" + fromUserName + "\" ,\"msgtype\":\"text\",\"text\":{\"content\":\"客官么么哒，我是小丰利，最可爱最温柔的小丰利，帮您越赚越多的小丰利~~以后在您赚大钱、走上人生巅峰的道路上，提供理财建议的繁重劳动就交给我啦!如有问题请在输入框内输入，小丰利立刻回答您呦！哦对了，人家的私人微信号是“FLJF01”，朋友圈经常爆料~如遇紧急咨询，请拨打客服热线：400 837 2223\"}}";
                //            else if (ts.Hours % 3 == 2)
                //                sJson = "{\"touser\": \"" + fromUserName + "\" ,\"msgtype\":\"text\",\"text\":{\"content\":\"客官么么哒，我是小丰利，最可爱最温柔的小丰利，帮您越赚越多的小丰利~~以后在您赚大钱、走上人生巅峰的道路上，提供理财建议的繁重劳动就交给我啦!如有问题请在输入框内输入，小丰利立刻回答您呦！哦对了，人家的私人微信号是“FLJF03”，朋友圈经常爆料~如遇紧急咨询，请拨打客服热线：400 837 2223\"}}";
                //            else
                //                sJson = "{\"touser\": \"" + fromUserName + "\" ,\"msgtype\":\"text\",\"text\":{\"content\":\"客官么么哒，我是小丰利，最可爱最温柔的小丰利，帮您越赚越多的小丰利~~以后在您赚大钱、走上人生巅峰的道路上，提供理财建议的繁重劳动就交给我啦!如有问题请在输入框内输入，小丰利立刻回答您呦！哦对了，人家的私人微信号是“FLJF04”，朋友圈经常爆料~如遇紧急咨询，请拨打客服热线：400 837 2223\"}}";
                //            WeChatBaseRequestService.PostUrl("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + WeChatBaseRequestService.getApptoken(), sJson);
                //        }
                //    }
            }
            return Content("");
        }

        public void GetGodBouns(int nMemberId, string sOpenId)
        {
            GodBounsService godBounsService = new GodBounsService();
            GodBouns godbouns = godBounsService.Search(new GodBouns() { IsValid = true }).Where(o => o.MemberId == nMemberId && o.OpenId == sOpenId).FirstOrDefault();
            if (godbouns == null)
            {
                godBounsService.Insert(new GodBouns()
                {
                    BounsAmount = 30,
                    BounsStatus = BounsStatus.未使用,
                    BounsType = BounsType.注册优惠券,
                    ExpireTime = DateTime.Now.AddMonths(1),
                    ConvertRate = 1,
                    IsActive = true,
                    LeftAmount = 30,
                    LimitAmount = 300,
                    MemberId = nMemberId,
                    Name = "加油优惠券",
                    OpenId = sOpenId
                });
            }
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

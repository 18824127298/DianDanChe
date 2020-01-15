using CheDaiBaoCommonController.Controllers;
using CheDaiBaoCommonService.Models;
using CheDaiBaoCommonService.Service;
using CheDaiBaoWeChatModel;
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
    public class YouKaRulesController : BaseCommonController
    {
        public ActionResult ActivityRules()
        {
            return View();
        }
        public ActionResult Erweima()
        {
            MemberService memberService = new MemberService();
            WeiXinModel WeiXin = (WeiXinModel)System.Web.HttpContext.Current.Session["WeiXin"];
            if (!memberService.GetAll().ToList().Where(o => o.OpenId == WeiXin.OpenId).Any())
                return RedirectToAction("Create","Member");
            else
            {
                Member member = memberService.GetAll().ToList().Where(o => o.OpenId == WeiXin.OpenId).First();
                string sJson = "{\"expire_seconds\": 604800, \"action_name\": \"QR_SCENE\", \"action_info\": {\"scene\": {\"scene_id\": " + member.Id + "}}}";
                string sUrl = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + WeChatBaseRequestService.getApptoken();
                memberService.Update(new Member()
                {
                    Id = member.Id,
                    OpenId = WeiXin.OpenId
                });
                string result = WeChatBaseRequestService.PostUrl(sUrl, sJson);
                string sTicket = WeChatBaseRequestService.GetJsonValue(result, "ticket");
                return RedirectToAction("QRCode", "YouKaRules", new { sTicket = sTicket, sName = WeiXin.NickName });
            }
        }

        public ActionResult QRCode(string sTicket, string sName)
        {
            ViewBag.Name = sName;
            ViewBag.Ticket = sTicket;
            return View();
        }

        public ActionResult GodBouns()
        {
            MemberService memberService = new MemberService();
            GodBounsService godBounsService = new GodBounsService();
            WeiXinModel weixin = (WeiXinModel)System.Web.HttpContext.Current.Session["WeiXin"];
            Member member = memberService.GetAll().ToList().Where(o => o.OpenId == weixin.OpenId).First();
            var godBounsList = godBounsService.Search(new GodBouns { IsValid = true, MemberId = member.Id }).Where(o => (o.BounsType == BounsType.注册优惠券 || o.BounsType == BounsType.推荐优惠券) && o.BounsStatus != BounsStatus.未激活).OrderBy(o => o.CreateTime).OrderBy(o => o.BounsStatus.Value).ToList();

            return View(godBounsList);
        }
    }
}

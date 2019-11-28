using CheDaiBaoCommonController.Controllers;
using CheDaiBaoCommonController.Model;
using CheDaiBaoCommonService.Models;
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
    public class MemberController : BaseCommonController
    {
        public ActionResult Create()
        {
            MemberService memberService = new MemberService();
            WeiXinModel WeiXin = (WeiXinModel)System.Web.HttpContext.Current.Session["WeiXin"];
            Member member = memberService.Search(new Member() { IsValid = true }).Where(o => o.OpenId == WeiXin.OpenId).FirstOrDefault();
            if (member == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AllGasStation", "YouKa");
            }
        }

        [HttpPost]
        public ActionResult Create(string Phone, string Code)
        {
            MemberService memberService = new MemberService();
            WeiXinModel WeiXin = (WeiXinModel)System.Web.HttpContext.Current.Session["WeiXin"];
            Member member = memberService.Search(new Member() { IsValid = true }).Where(o => o.OpenId == WeiXin.OpenId).FirstOrDefault();
            OperaService operaService = new OperaService();
            Member model = new Member();
            if (member != null)
            {
                return RedirectToAction("AllGasStation", "YouKa");
            }

            try
            {
                model.Code = Code;
                model.Phone = Phone;
                model.OpenId = WeiXin.OpenId;
                Member newMember = memberService.Registration(model);
                operaService.Insert(new Opera()
                {
                    BorrowerId = newMember.Id,
                    OperaType = OperaType.注册,
                    RelationId = newMember.Id,
                    Remark = string.Format("注册成功！用户名：{0}", newMember.Phone),
                    ClientAddress = Request.UserHostAddress
                });
                return RedirectToAction("AllGasStation", "YouKa");
            }
            catch (Exception ex)
            {
                if (ex.Message == "已经有相同的用户名" || ex.Message == "已经有相同的手机号码")
                {
                    Member oldgod = memberService.Search(new Member() { IsValid = true }).Where(o => o.Phone == model.Phone).FirstOrDefault();
                    memberService.Update(new Member()
                    {
                        Id = oldgod.Id,
                        OpenId = WeiXin.OpenId
                    });
                    return RedirectToAction("AllGasStation", "YouKa");
                }
                else
                {
                    Notification(new MessageResultModels(ex.Message, NotifyEnum.Error));
                    return View(model);
                }
            }
        }


        public ActionResult Index()
        {
            MemberService memberService = new MemberService();
            WeiXinModel WeiXin = (WeiXinModel)System.Web.HttpContext.Current.Session["WeiXin"];
            Member member = memberService.Search(new Member() { IsValid = true }).Where(o => o.OpenId == WeiXin.OpenId).FirstOrDefault();
            if (member == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                ViewBag.HeadImgurl = WeiXin.HeadImgurl;
                ViewBag.NickName = WeiXin.NickName;
                ViewBag.Phone = member.Phone;
                return View();
            }
        }
    }
}

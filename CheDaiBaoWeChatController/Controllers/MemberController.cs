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
            Member member = memberService.Search(new Member() { IsValid = true }).Where(o => o.OpenId == WeiXin.OpenId && !string.IsNullOrEmpty(o.Phone)).FirstOrDefault();
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
            try
            {
                if (member != null)
                {
                    if (string.IsNullOrEmpty(member.Phone))
                    {
                        model.Code = Code;
                        model.Phone = Phone;
                        model.OpenId = WeiXin.OpenId;
                        Member newMember = memberService.Updateration(model, member);
                        operaService.Insert(new Opera()
                        {
                            BorrowerId = member.Id,
                            OperaType = OperaType.注册,
                            RelationId = member.Id,
                            Remark = string.Format("注册成功！用户名：{0}", newMember.Phone),
                            ClientAddress = Request.UserHostAddress
                        });
                        if (member.RecommendId > 0)
                        {
                            Member recommendMember = memberService.GetById(member.RecommendId.Value);
                            GodBounsService godBounsService = new GodBounsService();
                            godBounsService.Insert(new GodBouns()
                            {
                                BounsAmount = 30,
                                BounsStatus = BounsStatus.未激活,
                                BounsType = BounsType.推荐优惠券,
                                ExpireTime = DateTime.Now.AddMonths(1),
                                ConvertRate = 1,
                                IsActive = true,
                                LeftAmount = 30,
                                LimitAmount = 300,
                                MemberId = recommendMember.Id,
                                Name = "加油优惠券",
                                OpenId = recommendMember.OpenId,
                                RelationId = member.Id
                            });
                        }
                        return RedirectToAction("AllGasStation", "YouKa");
                    }
                    else
                    {
                        return RedirectToAction("AllGasStation", "YouKa");
                    }
                }
                else
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

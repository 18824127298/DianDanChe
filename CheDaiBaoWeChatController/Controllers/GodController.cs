using CheDaiBaoCommonController.Controllers;
using CheDaiBaoCommonController.Model;
using CheDaiBaoCommonService.Models;
using CheDaiBaoCommonService.Service;
using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using CheDaiBaoWeChatServicee.Service;
using Sigbit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CheDaiBaoWeChatController.Controllers
{
    public class GodController : BaseCommonController
    {
        BorrowerService borrowerService = new BorrowerService();
        public ActionResult Index()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower == null)
            {
                return RedirectToAction("Create", "GodService");
            }
            else
            {
                FundsFlowService fundsflowService = new FundsFlowService();
                FundsFlow payfundsflow = fundsflowService.Search(new FundsFlow() { IsValid = true }).Where(o => o.PayGodId == borrower.Id && o.FeeType == FeeType.押金 && o.IsFreeze == false).FirstOrDefault();

                if (payfundsflow == null)
                {
                    ViewBag.Amount = 0;
                }
                else
                {
                    FundsFlow incomefundsflow = fundsflowService.Search(new FundsFlow() { IsValid = true }).Where(o => o.IncomeGodId == borrower.Id && o.FeeType == FeeType.押金 && o.IsFreeze == false).FirstOrDefault();
                    if (incomefundsflow == null)
                        ViewBag.Amount = payfundsflow.Amount;
                    else
                    {
                        decimal? dAmount = payfundsflow.Amount - incomefundsflow.Amount;
                        ViewBag.Amount = dAmount;
                    }
                }
                ViewBag.HeadImgurl = borrower.WeiXin.HeadImgurl;
                ViewBag.NickName = borrower.WeiXin.NickName;
                ViewBag.Phone = borrower.Phone;
                ViewBag.IsSalesman = borrower.IsSalesman;
                return View();
            }
        }
        public ActionResult Files()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            ViewBag.HeadImgurl = borrower.WeiXin.HeadImgurl;
            ViewBag.NickName = borrower.WeiXin.NickName;
            if (borrower == null)
            {
                return RedirectToAction("Create", "GodService");
            }
            else
            {
                ViewBag.Phone = borrower.Phone;
                ViewBag.FullName = String.IsNullOrEmpty(borrower.FullName) == true ? "" : borrower.FullName;
                ViewBag.IDNumber = String.IsNullOrEmpty(borrower.IDNumber) == true ? "" : borrower.IDNumber;
                return View();
            }
        }

        public ActionResult GodFileUpdate(string FullName, string IDNumber)
        {
            Borrower oldborrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            string sTimeNow = DateTime.Now.ToString("yyyyMMddHHmmss");
            FullNameVerify fnv = new FullNameVerify();
            string sResult = fnv.Verify(FullName, IDNumber, oldborrower.Id, sTimeNow);
            string sRespDesc = sResult.Split('&')[0].ToString().Split('=')[1].ToString();
            string sRespCode = sResult.Split('&')[2].ToString().Split('=')[1].ToString();
            string sSignature = sResult.Split('&')[1].ToString().Split('=')[1].ToString();
            if (sRespCode == "0000")
            {
                if (sSignature.ToLower() == fnv.SignCheck(sRespDesc, sRespCode))
                {
                    oldborrower.FullName = EncryptionService.AESEncrypt(new Regex(@"\s").Replace(FullName, ""));
                    oldborrower.IDNumber = EncryptionService.AESEncrypt(IDNumber);
                    oldborrower.IsIDNumber = 1;
                    borrowerService.Update(oldborrower);

                    OperaService operaService = new OperaService();
                    operaService.Insert(new Opera()
                    {
                        BorrowerId = oldborrower.Id,
                        OperaType = OperaType.实名认证,
                        RelationId = oldborrower.Id,
                        Remark = string.Format("{0}实名认证成功。真实姓名：{1}", oldborrower.Phone, oldborrower.FullName),
                        ClientAddress = Request.UserHostAddress
                    });

                    Notification(new MessageResultModels("实名认证提交成功", NotifyEnum.Success));
                    return RedirectToAction("Index", "God", null);
                }
                else
                {
                    Notification(new MessageResultModels("认证失败", NotifyEnum.Info));
                    return RedirectToAction("Index", "God", null);
                }
            }
            else
            {
                Notification(new MessageResultModels(sRespDesc, NotifyEnum.Info));
                return RedirectToAction("Index", "God", null);
            }

        }

        public ActionResult RemoveWeChatBindings()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            borrower.UnionId = "";
            borrowerService.Update(borrower);
            return RedirectToAction("Create", "GodService");
        }

        public ActionResult LoanApplyList()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            LoanApplyService loanapplyService = new LoanApplyService();
            List<LoanApply> loanapplyList = loanapplyService.GetLoanApplyListBySalesmanId(borrower.Id);
            ViewBag.LoanapplyList = loanapplyList;
            return View();
        }

        public ActionResult AllLoanList()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            LoanApplyService loanapplyService = new LoanApplyService();
            List<LoanApply> loanapplyList = loanapplyService.GetAllLoanApplyListBySalesmanId(borrower.Id);
            ViewBag.LoanapplyList = loanapplyList;
            return View();
        }

        public ActionResult IDPhoto()
        {
            LoanApplyService loanapplyService = new LoanApplyService();
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower.IsSalesman == true)
            {
                if (Session["BorrowerPhone"] != null)
                {
                    string sPhone = Session["BorrowerPhone"].ToString();
                    borrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == sPhone).FirstOrDefault();
                }
                else
                {
                    Notification(new MessageResultModels("手机号信息过期，请重新填写", NotifyEnum.Warning));
                    return RedirectToAction("GetPhone", "GodService", new { returnUrl = Request.Url.ToString() });
                }
            }
            ViewBag.Phone = borrower.Phone;
            LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.RepaymentStatus != CreditStatus.撤单).OrderByDescending(o => o.CreateTime).FirstOrDefault();
            if (loanApply == null)
            {
                Notification(new MessageResultModels("请先申请融资租赁", NotifyEnum.Warning));
                return RedirectToAction("Index", "God");
            }
            LoanFileService loanfileService = new LoanFileService();
            LoanFile loanfile = loanfileService.Search(new LoanFile() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.LoanApplyId == loanApply.Id).OrderByDescending(o => o.CreateTime.Value).FirstOrDefault();
            if (loanfile == null)
                return View();
            else
            {
                if (loanfile.IdCardInformationId == null && string.IsNullOrEmpty(loanfile.ZhengFilePath) && string.IsNullOrEmpty(loanfile.FanFilePath))
                {
                    return View();
                }
                else if (!string.IsNullOrEmpty(loanfile.IdCardInformationId.ToString()) && !string.IsNullOrEmpty(loanfile.ZhengFilePath) && !string.IsNullOrEmpty(loanfile.FanFilePath))
                {
                    string ZhengFilePath = "http://ddc.che01.com/PictureUpload/image/" + loanfile.ZhengFilePath.Substring(loanfile.ZhengFilePath.Length - 34, 34).Replace("\\", "/");
                    ViewBag.ZhengFilePath = ZhengFilePath;
                    string FanFilePath = "http://ddc.che01.com/PictureUpload/image/" + loanfile.FanFilePath.Substring(loanfile.FanFilePath.Length - 34, 34).Replace("\\", "/");
                    ViewBag.FanFilePath = FanFilePath;
                    return View();
                }
                else
                {
                    return View();
                }
            }
        }


        public ActionResult BankCardPhoto()
        {
            LoanApplyService loanapplyService = new LoanApplyService();
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower.IsSalesman == true)
            {
                if (Session["BorrowerPhone"] != null)
                {
                    string sPhone = Session["BorrowerPhone"].ToString();
                    borrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == sPhone).FirstOrDefault();
                }
                else
                {
                    Notification(new MessageResultModels("手机号信息过期，请重新填写", NotifyEnum.Warning));
                    return RedirectToAction("GetPhone", "GodService", new { returnUrl = Request.Url.ToString() });
                }
            }
            ViewBag.Phone = borrower.Phone;
            LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.RepaymentStatus != CreditStatus.撤单).OrderByDescending(o => o.CreateTime).FirstOrDefault();
            if (loanApply == null)
            {
                Notification(new MessageResultModels("请先申请融资租赁", NotifyEnum.Warning));
                return RedirectToAction("Index", "God");
            }
            LoanFileService loanfileService = new LoanFileService();
            LoanFile loanfile = loanfileService.Search(new LoanFile() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id).OrderByDescending(o => o.CreateTime.Value).FirstOrDefault();
            if (loanfile == null)
                return View();
            else
            {
                if (loanfile.BankCardId == null && string.IsNullOrEmpty(loanfile.BankCardPath))
                {
                    return View();
                }
                else if (!string.IsNullOrEmpty(loanfile.BankCardId.ToString()) && !string.IsNullOrEmpty(loanfile.BankCardPath))
                {
                    string BankCardPath = "http://ddc.che01.com/PictureUpload/image/" + loanfile.BankCardPath.Substring(loanfile.BankCardPath.Length - 34, 34).Replace("\\", "/");
                    ViewBag.BankCardPath = BankCardPath;
                    return View();
                }
                else
                {
                    return View();
                }
            }

        }

        public ActionResult MaterialPhoto()
        {
            try
            {
                LoanApplyService loanapplyService = new LoanApplyService();
                Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
                if (borrower.IsSalesman == true)
                {
                    if (Session["BorrowerPhone"] != null)
                    {
                        string sPhone = Session["BorrowerPhone"].ToString();
                        borrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == sPhone).FirstOrDefault();
                    }
                    else
                    {
                        Notification(new MessageResultModels("手机号信息过期，请重新填写", NotifyEnum.Warning));
                        return RedirectToAction("GetPhone", "GodService", new { returnUrl = Request.Url.ToString() });
                    }
                }
                ViewBag.Phone = borrower.Phone;
                LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && (o.RepaymentStatus == CreditStatus.未审核 || o.RepaymentStatus == CreditStatus.还款中)).OrderByDescending(o => o.CreateTime).FirstOrDefault();
                if (loanApply == null)
                {
                    ViewBag.IsBusinessFile = false;
                    return View();
                }
                else
                {
                    BusinessFileService businessFileService = new BusinessFileService();
                    List<BusinessFile> businessFileList = businessFileService.Search(new BusinessFile() { IsValid = true }).Where(o => o.RelationId == loanApply.Id && o.BorrowerId == borrower.Id && (o.Sort == 1 || o.Sort == 2)).ToList();

                    if (businessFileList.Count > 0)
                    {
                        ViewBag.IsBusinessFile = true;
                        foreach (BusinessFile bf in businessFileList)
                        {
                            bf.WeChatPath = VIVPHOTO(bf.WeChatPath);
                        }
                        ViewBag.businessFileList = businessFileList;
                        return View();
                    }
                    else
                    {
                        ViewBag.IsBusinessFile = false;
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.IsBusinessFile = false;
                return View();
            }
        }

        public string VIVPHOTO(string spath)
        {
            if (!string.IsNullOrEmpty(spath))
            {
                int i = spath.IndexOf("=");
                int j = spath.IndexOf("&");
                return spath.Replace((spath.Substring(i + 1)).Substring(0, j - i - 1), WeChatBaseRequestService.getApptoken());
            }
            else
            {
                return spath;
            }
        }

        [HttpPost]
        public string Refund()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            FundsFlowService fundsflowService = new FundsFlowService();
            FundsFlow payfundsflow = fundsflowService.Search(new FundsFlow() { IsValid = true }).Where(o => o.PayGodId == borrower.Id && o.FeeType == FeeType.押金 && o.IsFreeze == false).FirstOrDefault();

            if (payfundsflow == null)
            {
                return "0";
            }
            else
            {
                decimal? dAmount = 0;
                FundsFlow incomefundsflow = fundsflowService.Search(new FundsFlow() { IsValid = true }).Where(o => o.IncomeGodId == borrower.Id && o.FeeType == FeeType.押金 && o.IsFreeze == false).FirstOrDefault();
                if (incomefundsflow == null)
                    dAmount = payfundsflow.Amount;
                else
                {
                    dAmount = payfundsflow.Amount - incomefundsflow.Amount;
                }
                if (dAmount == 0)
                {
                    return "0";
                }
                else
                {
                    RefundService refundService = new RefundService();
                    refundService.Insert(new Refund()
                    {
                        Amount = dAmount,
                        BorrowerId = borrower.Id,
                        ReChargeId = payfundsflow.RelationId,
                        FundsFlowId = payfundsflow.Id
                    });
                    payfundsflow.IsFreeze = true;
                    fundsflowService.Update(payfundsflow);
                    return "1";
                }
            }

        }
    }
}

using CheDaiBaoCommonController.Controllers;
using CheDaiBaoCommonController.Model;
using CheDaiBaoCommonService.Models;
using CheDaiBaoWeChatController.Interface;
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
    public class LoanController : BaseCommonController
    {
        private readonly CarTypeService cartypeService;
        private readonly LoanApplyService loanapplyService;
        private readonly BorrowerService borrowerService;

        public LoanController()
        {
            this.cartypeService = new CarTypeService();
            this.loanapplyService = new LoanApplyService();
            this.borrowerService = new BorrowerService();
        }
        public ActionResult LoanIndex(string state)
        {
            if (state == "123")
            {
                return RedirectToAction("AllGasStation", "YouKa");
            }

            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower == null)
            {
                return RedirectToAction("Create", "GodService");
            }
            ViewBag.Phone = borrower.Phone;
            ViewBag.IsSalesman = borrower.IsSalesman;

            LoanApplyService loanapplyService = new LoanApplyService();
            LoanApply loanapply = loanapplyService.Search(new LoanApply { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.RepaymentStatus == CreditStatus.还款中).FirstOrDefault();
            string sLoanType = "0";
            if (loanapply != null)
            {
                if (loanapply.LoanType == LoanType.租客)
                {
                    sLoanType = "1";
                }
                else
                {
                    sLoanType = "2";
                }
            }
            ViewBag.LoanType = sLoanType;
            return View();
        }

        public ActionResult Finance()
        {
            //CarType cartype = new CarType();
            //if (Id != null)
            //{
            //    cartype = cartypeService.GetById(Id.Value);
            //}

            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower.IsSalesman == true)
            {
                if (Session["BorrowerPhone"] != null)
                {
                    ViewBag.BorrowerPhone = Session["BorrowerPhone"].ToString();
                }

                return View();
            }
            else
            {
                Notification(new MessageResultModels("您不是业务员，没有权限申请融资租赁", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex");
            }
        }
        public ActionResult BrandSelection()
        {
            List<CarType> cartypeList = cartypeService.GetAll();
            return View(cartypeList);
        }
        [HttpPost]
        public ActionResult CreateLoan(LoanApply model)
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            Session["BorrowerPhone"] = model.CreditPhone;
            if (borrower.IsSalesman == true)
            {
                Borrower userBorrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == model.CreditPhone).FirstOrDefault();
                if (userBorrower == null)
                {
                    Borrower newborrower = new Borrower();
                    newborrower.Guid = Guid.NewGuid().ToString();
                    newborrower.Aliases = model.CreditPhone;
                    newborrower.Phone = model.CreditPhone;
                    newborrower.IsValidatePhone = 1;
                    newborrower.CustomerServiceId = 0;
                    newborrower.IsSalesman = false;
                    int NewBorrowId = borrowerService.Insert(newborrower);
                    userBorrower = borrowerService.GetById(NewBorrowId);
                }
                else if (userBorrower.IsRejected == true)
                {
                    Notification(new MessageResultModels("抱歉，您已经被拒绝审批过，无法进行再次申请", NotifyEnum.Warning));
                    return RedirectToAction("LoanList", "Loan");
                }
                LoanApply loanApply = loanapplyService.Search(new LoanApply { IsValid = true }).Where(o => o.CreditPhone == model.CreditPhone && (o.RepaymentStatus == CreditStatus.还款中 || o.RepaymentStatus == CreditStatus.未审核)).FirstOrDefault();
                if (loanApply != null)
                {
                    Notification(new MessageResultModels("您已经申请过了，请耐心等待", NotifyEnum.Warning));
                    return RedirectToAction("LoanList", "Loan");
                }

                model.RepaymentStatus = CreditStatus.未审核;
                model.BorrowerId = userBorrower.Id;
                model.SalesmanId = borrower.Id;

                loanapplyService.Insert(model);
                Notification(new MessageResultModels("基本资料提交成功！", NotifyEnum.Success));
                return RedirectToAction("LoanList", "Loan");
            }
            else
            {
                Notification(new MessageResultModels("您不是业务员，没有权限申请融资租赁", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex");
            }
        }

        [HttpPost]
        public ActionResult EssentialInformation(LoanApply model)
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            Session["BorrowerPhone"] = model.CreditPhone;
            if (borrower.IsSalesman == true)
            {
                Borrower userBorrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == model.CreditPhone).FirstOrDefault();
                if (userBorrower == null)
                {
                    Notification(new MessageResultModels("找不到该用户", NotifyEnum.Warning));
                    return RedirectToAction("LoanIndex", "Loan");
                }
                if (borrower.Phone == "18824127298")
                {
                    LoanApply loanApply = loanapplyService.Search(new LoanApply { IsValid = true }).Where(o => o.CreditPhone == model.CreditPhone && (o.RepaymentStatus == CreditStatus.未审核 || o.RepaymentStatus == CreditStatus.还款中)).FirstOrDefault();
                    if (loanApply == null)
                    {
                        Notification(new MessageResultModels("您没有待审核的融资租赁", NotifyEnum.Warning));
                        return RedirectToAction("LoanIndex", "Loan");
                    }
                    model.Id = loanApply.Id;
                    model.Step = 6;

                }
                else
                {
                    LoanApply loanApply = loanapplyService.Search(new LoanApply { IsValid = true }).Where(o => o.CreditPhone == model.CreditPhone && o.RepaymentStatus == CreditStatus.未审核).FirstOrDefault();
                    if (loanApply == null)
                    {
                        Notification(new MessageResultModels("您没有待审核的融资租赁", NotifyEnum.Warning));
                        return RedirectToAction("LoanIndex", "Loan");
                    }
                    model.Id = loanApply.Id;
                    model.Step = 6;

                }
                loanapplyService.Update(model);
                Notification(new MessageResultModels("资料提交成功！", NotifyEnum.Success));
                return RedirectToAction("UploadMultipleSheets", "PictureUpload");
            }
            else
            {
                Notification(new MessageResultModels("您不是业务员，没有权限申请融资租赁", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex");
            }
        }

        public ActionResult InsertContacts()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower.IsSalesman == true)
            {
                if (Session["BorrowerPhone"] == null)
                {
                    Notification(new MessageResultModels("手机号信息过期，请重新填写", NotifyEnum.Warning));
                    return RedirectToAction("GetPhone", "GodService", new { returnUrl = Request.Url.ToString() });
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult InsertContacts(string sContacts, string Phones, string IsKnowStages)
        {
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
                    return Json(new
                    {
                        Result = "0",
                        Message = "手机号信息过期，请重新填写"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            LoanApply loanApply = loanapplyService.Search(new LoanApply { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && (o.RepaymentStatus == CreditStatus.还款中 || o.RepaymentStatus == CreditStatus.未审核)).OrderByDescending(o => o.CreateTime).FirstOrDefault();
            if (loanApply == null)
            {
                return Json(new
                {
                    Result = "1",
                    Message = "请先申请融资租赁"
                }, JsonRequestBehavior.AllowGet);
            }
            ContactsService contactsService = new ContactsService();
            for (int i = 0; i < sContacts.Split(',').Length; i++)
            {
                string sContact = sContacts.Split(',')[i];
                string sPhone = Phones.Split(',')[i];
                string sIsKnowStages = IsKnowStages.Split(',')[i];
                if (sPhone == "")
                {
                    return Json(new
                    {
                        Result = "2",
                        Message = "手机号不能有为空"
                    }, JsonRequestBehavior.AllowGet);
                }
                Contacts model = new Contacts();
                model.Contact = sContact;
                model.Phone = sPhone;
                model.BorrowerId = borrower.Id;
                model.LoanapplyId = loanApply.Id;
                model.IsKnowStages = Convert.ToBoolean(sIsKnowStages == "0" ? false : true);

                contactsService.Insert(model);
            }
            loanApply.Step = 5;
            loanapplyService.Update(loanApply);

            return Json(new
            {
                Result = "3",
                Message = "新增联系人成功"
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoanList()
        {
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
            LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id).FirstOrDefault();
            if (loanApply == null)
            {
                Notification(new MessageResultModels("您还没有申请融资租赁", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex");
            }
            else
            {
                bool bsf = false;
                bool byh = false;
                bool bbc = false;
                LoanFileService loanfileService = new LoanFileService();
                LoanFile loanfile = loanfileService.Search(new LoanFile() { IsValid = true }).Where(o => o.LoanApplyId == loanApply.Id).FirstOrDefault();
                if (loanfile != null)
                {
                    if (!string.IsNullOrEmpty(loanfile.ZhengFilePath) && !string.IsNullOrEmpty(loanfile.FanFilePath))
                    {
                        bsf = true;
                    }
                    if (!string.IsNullOrEmpty(loanfile.BankCardPath))
                    {
                        byh = true;
                    }
                }
                BusinessFileService businessfileService = new BusinessFileService();
                List<BusinessFile> businessfileList = businessfileService.Search(new BusinessFile() { IsValid = true }).Where(o => o.RelationId == loanApply.Id).ToList();
                if (businessfileList.Count > 0)
                {
                    bbc = true;
                }
                ViewBag.bsf = bsf;
                ViewBag.byh = byh;
                ViewBag.bbc = bbc;
                ViewBag.IsIDNumber = borrower.IsIDNumber;
                return View(loanApply);
            }
        }

        public ActionResult LoanPay()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            BorrowService borrowService = new BorrowService();
            Borrow borrow = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.RepaymentDate <= DateTime.Now.AddDays(30) && o.UnTotalInterest > 0).OrderBy(o => o.Stages).FirstOrDefault();
            return View(borrow);
        }

        public ActionResult CarType()
        {
            CarType carType = new CarType();
            List<CarType> cartypeList = cartypeService.GetAll();
            return View(cartypeList);
        }

        [HttpPost]
        public string InputPhone(string Phone)
        {
            Borrower borrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Aliases == Phone).FirstOrDefault();
            if (borrower == null)
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }

        public ActionResult ModifyContactMode()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            string sphone = borrower.Phone;
            if (borrower.IsSalesman == true)
            {
                string Phone = Session["BorrowerPhone"].ToString();
                if (!string.IsNullOrEmpty(Phone))
                {
                    borrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == Phone).FirstOrDefault();
                    if (sphone == "18824127298" || sphone == "13432898254")
                    {
                        LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && (o.RepaymentStatus == CreditStatus.未审核 || o.RepaymentStatus == CreditStatus.还款中)).FirstOrDefault();
                        if (loanApply == null)
                        {
                            Notification(new MessageResultModels("您没有申请中的融资租赁", NotifyEnum.Warning));
                            return RedirectToAction("LoanIndex");
                        }
                        else
                        {
                            return View(loanApply);
                        }
                    }
                    else
                    {
                        LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.RepaymentStatus == CreditStatus.未审核).FirstOrDefault();
                        if (loanApply == null)
                        {
                            Notification(new MessageResultModels("您没有申请中的融资租赁", NotifyEnum.Warning));
                            return RedirectToAction("LoanIndex");
                        }
                        else
                        {
                            return View(loanApply);
                        }
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                Notification(new MessageResultModels("您不是业务员，没有修改信息的权限", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex");
            }
        }

        [HttpPost]
        public ActionResult ModifyContactMode(LoanApply model)
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            string sphone = borrower.Phone;
            if (borrower.IsSalesman == true)
            {
                string sPhone = Session["BorrowerPhone"].ToString();
                borrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == sPhone).FirstOrDefault();
                if (sphone == "18824127298" || sphone == "13432898254")
                {
                    LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && (o.RepaymentStatus == CreditStatus.未审核 || o.RepaymentStatus == CreditStatus.还款中)).FirstOrDefault();
                    if (loanApply == null)
                    {
                        Notification(new MessageResultModels("您没有申请中的融资租赁", NotifyEnum.Warning));
                        return RedirectToAction("LoanIndex");
                    }
                    else
                    {
                        model.Id = loanApply.Id;
                        loanapplyService.Update(model);
                        Notification(new MessageResultModels("修改用户信息成功", NotifyEnum.Success));
                        return RedirectToAction("LoanIndex");
                    }
                }
                else
                {
                    LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.RepaymentStatus == CreditStatus.未审核).FirstOrDefault();
                    if (loanApply == null)
                    {
                        Notification(new MessageResultModels("该用户没有审核中的申请", NotifyEnum.Warning));
                        return RedirectToAction("LoanIndex");
                    }
                    else
                    {
                        model.Id = loanApply.Id;
                        loanapplyService.Update(model);
                        Notification(new MessageResultModels("修改用户信息成功", NotifyEnum.Success));
                        return RedirectToAction("LoanIndex");
                    }
                }

            }
            else
            {
                Notification(new MessageResultModels("您不是业务员，没有修改信息的权限", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex");
            }
        }


        public ActionResult EssentialInformation()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            string sPhone = "";
            if (borrower.IsSalesman == true)
            {
                if (Session["BorrowerPhone"] != null)
                {
                    sPhone = Session["BorrowerPhone"].ToString();
                }
                else
                {
                    Notification(new MessageResultModels("手机号信息过期，请重新填写", NotifyEnum.Warning));
                    return RedirectToAction("GetPhone", "GodService", new { returnUrl = Request.Url.ToString() });
                }
            }
            ViewBag.Phone = sPhone;
            return View();
        }


        public ActionResult RecruitmentMaterials()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            string sPhone = "";
            if (borrower.IsSalesman == true)
            {
                if (Session["BorrowerPhone"] != null)
                {
                    sPhone = Session["BorrowerPhone"].ToString();
                }
                else
                {
                    Notification(new MessageResultModels("手机号信息过期，请重新填写", NotifyEnum.Warning));
                    return RedirectToAction("GetPhone", "GodService", new { returnUrl = Request.Url.ToString() });
                }
            }
            ViewBag.Phone = sPhone;
            return View();
        }


        [HttpPost]
        public ActionResult RecruitmentMaterials(LoanApply model)
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            Session["BorrowerPhone"] = model.CreditPhone;
            if (borrower.IsSalesman == true)
            {
                Borrower userBorrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == model.CreditPhone).FirstOrDefault();
                if (userBorrower == null)
                {
                    Notification(new MessageResultModels("请先申请融资租赁", NotifyEnum.Warning));
                    return RedirectToAction("LoanIndex", "Loan");
                }
                LoanApply loanApply = loanapplyService.Search(new LoanApply { IsValid = true }).Where(o => o.CreditPhone == model.CreditPhone && o.RepaymentStatus == CreditStatus.未审核).FirstOrDefault();
                if (loanApply == null)
                {
                    Notification(new MessageResultModels("您没有待审核的融资租赁", NotifyEnum.Warning));
                    return RedirectToAction("LoanIndex", "Loan");
                }
                model.Id = loanApply.Id;
                model.Step = 8;

                loanapplyService.Update(model);
                Notification(new MessageResultModels("资料提交成功！", NotifyEnum.Success));
                return RedirectToAction("LoanList", "Loan");
            }
            else
            {
                Notification(new MessageResultModels("您不是业务员，没有权限申请融资租赁", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex");
            }

        }

        public ActionResult UpdateContacts()
        {
            string sPhone = Session["BorrowerPhone"].ToString();
            Borrower borrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == sPhone).FirstOrDefault();
            if (borrower == null)
            {
                Notification(new MessageResultModels("未找到该手机号客户", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex", "Loan");
            }
            ContactsService contactsService = new ContactsService();
            LoanApply loanApply = loanapplyService.Search(new LoanApply { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && (o.RepaymentStatus == CreditStatus.还款中 || o.RepaymentStatus == CreditStatus.未审核)).OrderByDescending(o => o.CreateTime).FirstOrDefault();
            if (loanApply == null)
            {
                Notification(new MessageResultModels("该客户未有审核中的融资租赁", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex", "Loan");
            }
            List<Contacts> contactsList = contactsService.Search(new Contacts() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.LoanapplyId == loanApply.Id).ToList();
            ViewBag.contactsList = contactsList;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateContacts(string sContacts, string Phones, string IsKnowStages)
        {
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
                    return Json(new
                    {
                        Result = "0",
                        Message = "手机号信息过期，请重新填写"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            LoanApply loanApply = loanapplyService.Search(new LoanApply { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && (o.RepaymentStatus == CreditStatus.还款中 || o.RepaymentStatus == CreditStatus.未审核)).OrderByDescending(o => o.CreateTime).FirstOrDefault();
            if (loanApply == null)
            {
                return Json(new
                {
                    Result = "1",
                    Message = "请先申请融资租赁"
                }, JsonRequestBehavior.AllowGet);
            }

            ContactsService contactsService = new ContactsService();
            List<Contacts> contactsList = contactsService.Search(new Contacts() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.LoanapplyId == loanApply.Id).ToList();
            foreach (Contacts item in contactsList)
            {
                contactsService.DeleteBySearch(item);
            }

            for (int i = 0; i < sContacts.Split(',').Length; i++)
            {
                string sContact = sContacts.Split(',')[i];
                string sPhone = Phones.Split(',')[i];
                string sIsKnowStages = IsKnowStages.Split(',')[i];
                if (sPhone == "")
                {
                    return Json(new
                    {
                        Result = "2",
                        Message = "手机号不能有为空"
                    }, JsonRequestBehavior.AllowGet);
                }
                Contacts model = new Contacts();
                model.Contact = sContact;
                model.Phone = sPhone;
                model.BorrowerId = borrower.Id;
                model.LoanapplyId = loanApply.Id;
                model.IsKnowStages = Convert.ToBoolean(sIsKnowStages == "0" ? false : true);
                contactsService.Insert(model);
            }
            return Json(new
            {
                Result = "3",
                Message = "联系人修改成功"
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ModifyRecruitmentMaterials()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            string sPhone = borrower.Phone;
            if (borrower.IsSalesman == true)
            {
                string Phone = Session["BorrowerPhone"].ToString();
                if (!string.IsNullOrEmpty(Phone))
                {
                    borrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == Phone).FirstOrDefault();
                    if (sPhone == "18824127298" || sPhone == "13763395495" || sPhone == "13432898254")
                    {
                        LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.RepaymentStatus == CreditStatus.还款中).FirstOrDefault();
                        if (loanApply == null)
                        {
                            Notification(new MessageResultModels("没有还款中的融资租赁", NotifyEnum.Warning));
                            return RedirectToAction("LoanIndex");
                        }
                        else
                        {
                            return View(loanApply);
                        }
                    }
                    else
                    {
                        LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.CustomerClassification == "B").FirstOrDefault();
                        if (loanApply == null)
                        {
                            Notification(new MessageResultModels("该客户没有融资租赁", NotifyEnum.Warning));
                            return RedirectToAction("LoanIndex");
                        }
                        else
                        {
                            return View(loanApply);
                        }
                    }
                }
                else
                {
                    return View();
                }


            }
            else
            {
                Notification(new MessageResultModels("您不是业务员，没有修改信息的权限", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex");
            }
        }

        [HttpPost]
        public ActionResult ModifyRecruitmentMaterials(LoanApply model)
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower.IsSalesman == true)
            {
                string sPhone = Session["BorrowerPhone"].ToString();
                borrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == sPhone).FirstOrDefault();
                LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && (o.RepaymentStatus == CreditStatus.还款中 || o.CustomerClassification == "B")).FirstOrDefault();
                if (loanApply == null)
                {
                    Notification(new MessageResultModels("该客户当前没有融资租赁", NotifyEnum.Warning));
                    return RedirectToAction("LoanIndex");
                }
                else
                {
                    model.Id = loanApply.Id;
                    loanapplyService.Update(model);
                    Notification(new MessageResultModels("修改用户信息成功", NotifyEnum.Success));
                    return RedirectToAction("LoanIndex");
                }


            }
            else
            {
                Notification(new MessageResultModels("您不是业务员，没有修改信息的权限", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex");
            }
        }


        public ActionResult CapriciousLoan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CapriciousLoan(CapriciousLoan clmodel)
        {
            CapriciousLoan newclmodel = new CapriciousLoan();
            CapriciousLoanService capriciousloanService = new CapriciousLoanService();
            if (string.IsNullOrEmpty(clmodel.KeHuPhone))
            {
                Notification(new MessageResultModels("请填写客户手机号", NotifyEnum.Warning));
                return View();
            }

            if (string.IsNullOrEmpty(clmodel.Phone))
            {
                Notification(new MessageResultModels("请填写业务员或者商户的手机号", NotifyEnum.Warning));
                return View();
            }

            if (string.IsNullOrEmpty(clmodel.LoginKey))
            {
                Notification(new MessageResultModels("请填写业务员密钥", NotifyEnum.Warning));
                return View();
            }

            Borrower salesman = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == clmodel.Phone && o.LoginKey == clmodel.LoginKey && (o.IsSalesman == true || o.IsMerchant == true)).FirstOrDefault();
            if (salesman == null)
            {
                Notification(new MessageResultModels("请填写正确的商户号和秘钥", NotifyEnum.Warning));
                return View();
            }
            else
            {
                newclmodel.BorrowerId = salesman.Id;
                newclmodel.KeHuPhone = clmodel.KeHuPhone;
            }
            capriciousloanService.Insert(newclmodel);

            return Redirect("http://plplps.suning.com/plplps/tweeter/regist.html?registerChannelCode=29904&channelCode=S00635&realNameChannelCode=29904");
        }

        public ActionResult RentInformation()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            string sPhone = "";
            if (borrower.IsSalesman == true)
            {
                if (Session["BorrowerPhone"] != null)
                {
                    sPhone = Session["BorrowerPhone"].ToString();
                }
                else
                {
                    Notification(new MessageResultModels("手机号信息过期，请重新填写", NotifyEnum.Warning));
                    return RedirectToAction("GetPhone", "GodService", new { returnUrl = Request.Url.ToString() });
                }
            }
            ViewBag.Phone = sPhone;
            return View();
        }

        [HttpPost]
        public ActionResult RentInformation(LoanApply model)
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            Session["BorrowerPhone"] = model.CreditPhone;
            if (borrower.IsSalesman == true)
            {
                Borrower userBorrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == model.CreditPhone).FirstOrDefault();
                if (userBorrower == null)
                {
                    Notification(new MessageResultModels("找不到该用户", NotifyEnum.Warning));
                    return RedirectToAction("LoanIndex", "Loan");
                }
                if (borrower.Phone == "18824127298")
                {
                    LoanApply loanApply = loanapplyService.Search(new LoanApply { IsValid = true }).Where(o => o.CreditPhone == model.CreditPhone && (o.RepaymentStatus == CreditStatus.未审核 || o.RepaymentStatus == CreditStatus.还款中)).FirstOrDefault();
                    if (loanApply == null)
                    {
                        Notification(new MessageResultModels("您没有待审核的融资租赁", NotifyEnum.Warning));
                        return RedirectToAction("LoanIndex", "Loan");
                    }
                    model.Id = loanApply.Id;
                    model.Step = 4;

                }
                else
                {
                    LoanApply loanApply = loanapplyService.Search(new LoanApply { IsValid = true }).Where(o => o.CreditPhone == model.CreditPhone && o.RepaymentStatus == CreditStatus.未审核).FirstOrDefault();
                    if (loanApply == null)
                    {
                        Notification(new MessageResultModels("您没有待审核的融资租赁", NotifyEnum.Warning));
                        return RedirectToAction("LoanIndex", "Loan");
                    }
                    model.Id = loanApply.Id;
                    model.Step = 4;

                }
                model.unDeposit = model.Deposit;
                loanapplyService.Update(model);
                Notification(new MessageResultModels("资料提交成功！", NotifyEnum.Success));
                return RedirectToAction("InsertContacts", "Loan");
            }
            else
            {
                Notification(new MessageResultModels("您不是业务员，没有权限申请融资租赁", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex");
            }
        }

        public ActionResult LoanStages()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            string sPhone = "";
            if (borrower.IsSalesman == true)
            {
                if (Session["BorrowerPhone"] != null)
                {
                    sPhone = Session["BorrowerPhone"].ToString();
                }
                else
                {
                    Notification(new MessageResultModels("手机号信息过期，请重新填写", NotifyEnum.Warning));
                    return RedirectToAction("GetPhone", "GodService", new { returnUrl = Request.Url.ToString() });
                }
            }
            ViewBag.Phone = sPhone;
            return View();
        }

        [HttpPost]
        public ActionResult LoanStagesSubmit(LoanApply model)
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            Session["BorrowerPhone"] = model.CreditPhone;
            if (borrower.IsSalesman == true)
            {
                Borrower userBorrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == model.CreditPhone).FirstOrDefault();
                if (userBorrower == null)
                {
                    Notification(new MessageResultModels("找不到该用户", NotifyEnum.Warning));
                    return RedirectToAction("LoanIndex", "Loan");
                }
                if (borrower.Phone == "18824127298")
                {
                    LoanApply loanApply = loanapplyService.Search(new LoanApply { IsValid = true }).Where(o => o.CreditPhone == model.CreditPhone && (o.RepaymentStatus == CreditStatus.未审核 || o.RepaymentStatus == CreditStatus.还款中)).FirstOrDefault();
                    if (loanApply == null)
                    {
                        Notification(new MessageResultModels("您没有待审核的融资租赁", NotifyEnum.Warning));
                        return RedirectToAction("LoanIndex", "Loan");
                    }
                    model.Id = loanApply.Id;
                    model.Step = 4;

                }
                else
                {
                    LoanApply loanApply = loanapplyService.Search(new LoanApply { IsValid = true }).Where(o => o.CreditPhone == model.CreditPhone && o.RepaymentStatus == CreditStatus.未审核).FirstOrDefault();
                    if (loanApply == null)
                    {
                        Notification(new MessageResultModels("您没有待审核的融资租赁", NotifyEnum.Warning));
                        return RedirectToAction("LoanIndex", "Loan");
                    }
                    model.Id = loanApply.Id;
                    model.Step = 4;

                }
                loanapplyService.Update(model);
                Notification(new MessageResultModels("资料提交成功！", NotifyEnum.Success));
                return RedirectToAction("InsertContacts", "Loan");
            }
            else
            {
                Notification(new MessageResultModels("您不是业务员，没有权限申请融资租赁", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex");
            }
        }
    }
}

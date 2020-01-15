using BaiDuSdk.Model;
using BaiDuSdk.RecognitionAI;
using CheDaiBaoCommonController.Controllers;
using CheDaiBaoCommonController.Model;
using CheDaiBaoCommonService.Models;
using CheDaiBaoWeChatController.Interface;
using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Interface;
using CheDaiBaoWeChatService.Service;
using Sigbit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WxPayApi;

namespace CheDaiBaoWeChatController.Controllers
{
    public class GodServiceController : BaseCommonController
    {

        BorrowerService borrowerService = new BorrowerService();
        DriverApplicationService driverApplicationService = new DriverApplicationService();
        public ActionResult Create()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoanIndex", "Loan");
            }
        }

        [HttpPost]
        public ActionResult Create(string Phone, string Code)
        {
            Borrower oldBorrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            OperaService operaService = new OperaService();
            Borrower model = new Borrower();
            if (oldBorrower != null)
            {
                borrowerService.Update(new Borrower()
                {
                    Id = oldBorrower.Id,
                    UnionId = oldBorrower.WeiXin.UnionId
                });
                return RedirectToAction("LoanIndex", "Loan");
            }

            WeiXinModel WeiXin = (WeiXinModel)System.Web.HttpContext.Current.Session["WeiXin"];
            try
            {

                model.Aliases = Phone;
                model.Code = Code;
                model.Phone = Phone;
                model.UnionId = WeiXin.UnionId;
                model.WeiXinId = WeiXin.OpenId;
                Borrower newBorrower = borrowerService.Registration(model);
                operaService.Insert(new Opera()
                {
                    BorrowerId = newBorrower.Id,
                    OperaType = OperaType.注册,
                    RelationId = newBorrower.Id,
                    Remark = string.Format("注册成功！用户名：{0}", newBorrower.Aliases),
                    ClientAddress = Request.UserHostAddress
                });
                return RedirectToAction("LoanIndex", "Loan");
            }
            catch (Exception ex)
            {
                if (ex.Message == "已经有相同的用户名" || ex.Message == "已经有相同的手机号码")
                {
                    Borrower oldgod = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == model.Phone).FirstOrDefault();
                    borrowerService.Update(new Borrower()
                    {
                        Id = oldgod.Id,
                        WeiXinId = WeiXin.OpenId,
                        UnionId = WeiXin.UnionId
                    });
                    return RedirectToAction("LoanIndex", "Loan");
                }
                else
                {
                    Notification(new MessageResultModels(ex.Message, NotifyEnum.Error));
                    return View(model);
                }
            }
        }

        public ActionResult CarLoanRecord()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            BorrowService borrowService = new BorrowService();
            List<Borrow> borrowList = borrowService.Search(new Borrow { IsValid = true }).Where(o => o.BorrowerId == borrower.Id).ToList();
            return View(borrowList);
        }

        public ActionResult CarIllegal()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower == null)
            {
                return RedirectToAction("Create", "GodService");
            }
            else
            {
                CarIllegalService carIlleaglService = new CarIllegalService();
                List<CarIllegal> carIllegalList = carIlleaglService.Search(new CarIllegal() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id).ToList();
                return View(carIllegalList);
            }
        }

        public ActionResult DriverApplicationCreate()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower == null)
            {
                return RedirectToAction("Create", "GodService");
            }
            else
            {
                DriverApplication driverApplication = driverApplicationService.Search(new DriverApplication() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id).FirstOrDefault();
                return View(driverApplication);
            }
        }

        [HttpPost]
        public ActionResult DriverApplicationCreate(string FullName, string Phone)
        {
            try
            {
                if (FullName == "" && Phone == "")
                {
                    throw new Exception("请填写姓名和手机号！");
                }
                else
                {
                    Borrower borrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == EncryptionService.AESEncrypt(Phone)).FirstOrDefault();
                    if (borrower == null)
                    {
                        throw new Exception("您的手机号还未注册，请先注册再申请！");
                    }
                    else
                    {
                        DriverApplication driverApplication = driverApplicationService.Search(new DriverApplication() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id).FirstOrDefault();

                        if (driverApplication == null)
                        {
                            driverApplication = new DriverApplication();
                            driverApplication.FullName = EncryptionService.AESEncrypt(FullName);
                            driverApplication.Phone = EncryptionService.AESEncrypt(Phone);
                            driverApplication.BorrowerId = borrower.Id;
                            driverApplicationService.Insert(driverApplication);
                        }
                        else
                        {
                            driverApplication.FullName = EncryptionService.AESEncrypt(FullName);
                            driverApplication.Phone = EncryptionService.AESEncrypt(Phone);
                            driverApplication.BorrowerId = borrower.Id;
                            driverApplicationService.Update(driverApplication);
                        }
                        Notification(new MessageResultModels("提交申请成功，请耐心等待！", NotifyEnum.Success));
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                Notification(new MessageResultModels(ex.Message, NotifyEnum.Error));
                if (ex.Message.Contains("未注册"))
                {
                    return RedirectToAction("Create");
                }
                return View();
            }
        }

        public ActionResult Information()
        {
            LoanApplyService loanapplyService = new LoanApplyService();
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            ViewBag.Company = borrower.Company;
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
            LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.RepaymentStatus == CreditStatus.未审核).FirstOrDefault();
            if (loanApply == null)
            {
                Notification(new MessageResultModels("您尚未有申请中的融资租赁", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex", "Loan");
            }
            else
            {
                ViewBag.SalesmanId = loanApply.SalesmanId == null ? "0" : "1";
                LoanFileService loanFileService = new LoanFileService();
                LoanFile loanfile = loanFileService.Search(new LoanFile { IsValid = true }).Where(o => o.LoanApplyId == loanApply.Id).FirstOrDefault();

                if (loanfile == null)
                {
                    Notification(new MessageResultModels("请先上传证件照", NotifyEnum.Warning));
                    return RedirectToAction("UploadBorrowerPhoto", "PictureUpload");
                }
                else
                {
                    if (!string.IsNullOrEmpty(loanfile.ZhengFilePath))
                    {
                        IdCardNumber ZhengNumber = CharacterRecognition.IdcardDistinguish(loanfile.ZhengFilePath, "front");
                        if (ZhengNumber.image_status == "non_idcard")
                        {
                            Notification(new MessageResultModels("身份证正面信息无法识别,请手动输入", NotifyEnum.Warning));
                        }
                        else
                        {
                            ViewBag.Name = ZhengNumber.words_result.姓名.words;
                            ViewBag.Birth = ZhengNumber.words_result.出生.words;
                            ViewBag.Address = ZhengNumber.words_result.住址.words;
                            ViewBag.IdCardNumber = ZhengNumber.words_result.公民身份号码.words;
                            ViewBag.Sex = ZhengNumber.words_result.性别.words;
                            ViewBag.Nation = ZhengNumber.words_result.民族.words;
                        }
                    }

                    if (!string.IsNullOrEmpty(loanfile.FanFilePath))
                    {
                        IdCardNumber FanNumber = CharacterRecognition.IdcardDistinguish(loanfile.FanFilePath, "back");
                        if (FanNumber.image_status == "non_idcard")
                        {
                            Notification(new MessageResultModels("身份证反面信息无法识别,请手动输入", NotifyEnum.Warning));
                        }
                        else
                        {
                            ViewBag.SigningOrganization = FanNumber.words_result.签发机关.words;
                            ViewBag.IssuanceDate = FanNumber.words_result.签发日期.words;
                            ViewBag.ExpirationDate = FanNumber.words_result.失效日期.words;
                        }
                    }

                    if (!string.IsNullOrEmpty(loanfile.BankCardPath))
                    {
                        BankCardNumber bankCardNumber = CharacterRecognition.BankcardDistinguish(loanfile.BankCardPath);


                        if (bankCardNumber.result == null)
                        {
                            Notification(new MessageResultModels("银行卡号码无法识别,请手动输入", NotifyEnum.Warning));
                        }
                        else
                        {
                            ViewBag.BankCardNumbe = bankCardNumber.result.bank_card_number;
                            ViewBag.BankCardType = bankCardNumber.result.bank_card_type == "1" ? "借记卡" : bankCardNumber.result.bank_card_type == "2" ? "信用卡" : "";
                            ViewBag.BankName = bankCardNumber.result.bank_name;
                        }
                    }

                    return View();
                }
            }
        }


        [HttpPost]
        public ActionResult InformationSubmit(Information information)
        {
            LoanApplyService loanapplyService = new LoanApplyService();
            IdCardInformationService idcardinformationService = new IdCardInformationService();
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            //if (borrower.Company != Company.翼速)
            //{
            //    information.CustomerClassification = "D";
            //}
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
            LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.RepaymentStatus == CreditStatus.未审核).FirstOrDefault();
            if (loanApply == null)
            {
                Notification(new MessageResultModels("您尚未有申请中的融资租赁", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex", "Loan");
            }
            loanApply.BusinessName = information.BusinessName;
            loanApply.RecruitmentName = information.RecruitmentName;

            if (information.CustomerClassification == "B")
            {
                loanApply.AuditTime = DateTime.Now;
                loanApply.Auditor = new BorrowerAuthenticationService().GetAuthenticatedBorrower().FullName;
                loanApply.RepaymentStatus = CreditStatus.审核未通过;
                loanApply.Remark = information.Remark;
                loanApply.CustomerClassification = information.CustomerClassification;
                loanapplyService.Update(loanApply);
                borrower.IsRejected = true;
                borrowerService.Update(borrower);
                //QiyebaoSms qiyebaoSms = new QiyebaoSms();
                Borrower salesman = borrowerService.GetById(loanApply.SalesmanId);
                //qiyebaoSms.SendSms(salesman.Phone, "很抱歉，客户" + borrower.Phone + "(" + borrower.FullName + ")融资租赁业务未获批【车1号】");

                WechatPushMessage wechatpushMessage = new WechatPushMessage();
                if (!string.IsNullOrEmpty(salesman.WeiXinId))
                {
                    wechatpushMessage.NotificationResult(salesman.WeiXinId, borrower.Phone, information.Name, "未获批");
                }
            }
            else
            {
                Borrower sfBorrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.IDNumber == information.IdCardNumber).FirstOrDefault();
                if (sfBorrower != null)
                {
                    if (sfBorrower.IsRejected == true)
                    {
                        loanApply.AuditTime = DateTime.Now;
                        loanApply.Auditor = new BorrowerAuthenticationService().GetAuthenticatedBorrower().FullName;
                        loanApply.AuditorRemark = "用户已被拒绝过一次";
                        loanApply.RepaymentStatus = CreditStatus.审核未通过;
                        loanApply.Remark = information.Remark;
                        loanApply.CustomerClassification = information.CustomerClassification;
                        loanapplyService.Update(loanApply);
                        borrower.IsRejected = true;
                        borrowerService.Update(borrower);
                        Notification(new MessageResultModels("抱歉，您已经被拒绝审批过，无法进行再次申请", NotifyEnum.Warning));
                        return RedirectToAction("LoanList", "Loan");
                    }
                }
            }



            LoanFileService loanfileService = new LoanFileService();
            LoanFile loanfile = loanfileService.Search(new LoanFile() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.LoanApplyId == loanApply.Id).OrderByDescending(o => o.CreateTime.Value).FirstOrDefault();
            if (loanfile == null)
            {
                Notification(new MessageResultModels("还未提交证件信息", NotifyEnum.Warning));
                return RedirectToAction("LoanList", "Loan");
            }
            else
            {
                if (DateTimeUtil.ToDateTime(information.ExpirationDate) < DateTime.Now.Date)
                {
                    loanApply.AuditTime = DateTime.Now;
                    loanApply.Auditor = new BorrowerAuthenticationService().GetAuthenticatedBorrower().FullName;
                    loanApply.AuditorRemark = "身份证已过期";
                    loanApply.RepaymentStatus = CreditStatus.审核未通过;
                    loanApply.Remark = information.Remark;
                    loanApply.CustomerClassification = information.CustomerClassification;
                    loanapplyService.Update(loanApply);
                    Notification(new MessageResultModels("身份证已过期", NotifyEnum.Warning));
                    return RedirectToAction("LoanList", "Loan");
                }

                int Age = CalculateAge(information.Birth);//根据生日计算年龄
                if (Age < 18)
                {
                    loanApply.AuditTime = DateTime.Now;
                    loanApply.Auditor = new BorrowerAuthenticationService().GetAuthenticatedBorrower().FullName;
                    loanApply.AuditorRemark = "客户未满十八周岁";
                    loanApply.RepaymentStatus = CreditStatus.审核未通过;
                    loanApply.Remark = information.Remark;
                    loanApply.CustomerClassification = information.CustomerClassification;
                    loanapplyService.Update(loanApply);
                    Notification(new MessageResultModels("客户未满十八周岁", NotifyEnum.Warning));
                    return RedirectToAction("LoanList", "Loan");
                }

                IdCardInformation idCardinformation = idcardinformationService.Search(new IdCardInformation() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id).FirstOrDefault();
                if (idCardinformation == null)
                {
                    IdCardInformation newidCardinformation = new IdCardInformation();
                    newidCardinformation.Address = information.Address;
                    newidCardinformation.Birth = information.Birth;
                    newidCardinformation.ExpirationDate = information.ExpirationDate;
                    newidCardinformation.IdCardNumber = information.IdCardNumber;
                    newidCardinformation.IssuanceDate = information.IssuanceDate;
                    newidCardinformation.Name = information.Name;
                    newidCardinformation.Nation = information.Nation;
                    newidCardinformation.Sex = information.Sex;
                    newidCardinformation.SigningOrganization = information.SigningOrganization;
                    newidCardinformation.BorrowerId = borrower.Id;
                    loanfile.IdCardInformationId = idcardinformationService.Insert(newidCardinformation);
                }
                else
                {
                    idCardinformation.Address = information.Address;
                    idCardinformation.Birth = information.Birth;
                    idCardinformation.ExpirationDate = information.ExpirationDate;
                    idCardinformation.IdCardNumber = information.IdCardNumber;
                    idCardinformation.IssuanceDate = information.IssuanceDate;
                    idCardinformation.Name = information.Name;
                    idCardinformation.Nation = information.Nation;
                    idCardinformation.Sex = information.Sex;
                    idCardinformation.SigningOrganization = information.SigningOrganization;
                    idcardinformationService.Update(idCardinformation);
                    loanfile.IdCardInformationId = idCardinformation.Id;
                }

                BankCardService bankcardSerivce = new BankCardService();
                BankCard bankCard = bankcardSerivce.Search(new BankCard() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id).OrderByDescending(o => o.CreateTime.Value).FirstOrDefault();
                if (bankCard == null)
                {
                    BankCard newbankCard = new BankCard();
                    newbankCard.BorrowerId = borrower.Id;
                    newbankCard.BankCardType = information.BankCardType;
                    newbankCard.Name = information.BankCardName;
                    newbankCard.Number = information.Number;
                    loanfile.BankCardId = bankcardSerivce.Insert(newbankCard);
                }
                else
                {
                    bankCard.BankCardType = information.BankCardType;
                    bankCard.Name = information.BankCardName;
                    bankCard.Number = information.Number;
                    bankcardSerivce.Update(bankCard);
                    loanfile.BankCardId = bankCard.Id;
                }
                loanfileService.Update(loanfile);

                borrower.FullName = information.Name;
                borrower.IDNumber = information.IdCardNumber;
                borrower.IsIDNumber = 1;
                borrowerService.Update(borrower);


                if (information.CustomerClassification != "B")
                {
                    List<string> FengKongWeiXinId = borrowerService.GetFengKong();
                    List<string> FengKongPhone = borrowerService.GetPhone();
                    string sContent = string.Format(@"有新的提单，客户手机号为{0}，姓名为{1}【车1号】", loanApply.CreditPhone, information.Name);
                    QiyebaoSms qiyebaoSms = new QiyebaoSms();
                    WechatPushMessage wechatpushMessage = new WechatPushMessage();
                    foreach (string sPhone in FengKongPhone)
                    {
                        qiyebaoSms.SendSms(sPhone, sContent);
                    }
                    foreach (string sWeiXinId in FengKongWeiXinId)
                    {
                        wechatpushMessage.AuditProcessingReminder(sWeiXinId, loanApply.CreditPhone, information.Name, DateTime.Now.ToString("yyyy年MM月dd日"));
                    }
                }
                loanApply.Remark = information.Remark;
                loanApply.CustomerClassification = information.CustomerClassification;
                loanApply.Step = 3;
                loanapplyService.Update(loanApply);
                Notification(new MessageResultModels("申请信息提交成功", NotifyEnum.Success));

                if (loanApply.LoanType == LoanType.租客)
                {
                    return RedirectToAction("RentInformation", "Loan");
                }
                else
                {
                    return RedirectToAction("LoanStages", "Loan");
                }
            }
        }




        public ActionResult IdCardInformation()
        {
            LoanApplyService loanapplyService = new LoanApplyService();
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            string sphone = borrower.Phone;
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
            LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.RepaymentStatus == CreditStatus.未审核).FirstOrDefault();
            if (loanApply == null)
            {
                if (sphone != "18824127298")
                {
                    Notification(new MessageResultModels("您尚未有申请中的融资租赁", NotifyEnum.Warning));
                    return RedirectToAction("LoanIndex", "Loan");
                }
                else
                {
                    loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && (o.RepaymentStatus == CreditStatus.未审核 || o.RepaymentStatus == CreditStatus.还款中)).FirstOrDefault();
                }
            }
            LoanFileService loanFileService = new LoanFileService();
            LoanFile loanfile = loanFileService.Search(new LoanFile { IsValid = true }).Where(o => o.LoanApplyId == loanApply.Id).FirstOrDefault();

            if (loanfile == null)
            {
                Notification(new MessageResultModels("请先上传身份证照片", NotifyEnum.Warning));
                return RedirectToAction("UploadID", "PictureUpload");
            }
            else
            {
                IdCardNumber ZhengNumber = CharacterRecognition.IdcardDistinguish(loanfile.ZhengFilePath, "front");
                IdCardNumber FanNumber = CharacterRecognition.IdcardDistinguish(loanfile.FanFilePath, "back");
                DebugLogger.LogDebugMessage(ZhengNumber.image_status + "," + FanNumber.image_status);

                if (ZhengNumber.image_status == "non_idcard")
                {
                    Notification(new MessageResultModels("身份证正面信息无法识别,请手动输入", NotifyEnum.Warning));
                }
                else
                {
                    ViewBag.Name = ZhengNumber.words_result.姓名.words;
                    ViewBag.Birth = ZhengNumber.words_result.出生.words;
                    ViewBag.Address = ZhengNumber.words_result.住址.words;
                    ViewBag.IdCardNumber = ZhengNumber.words_result.公民身份号码.words;
                    ViewBag.Sex = ZhengNumber.words_result.性别.words;
                    ViewBag.Nation = ZhengNumber.words_result.民族.words;
                }

                if (FanNumber.image_status == "non_idcard")
                {
                    Notification(new MessageResultModels("身份证反面信息无法识别,请手动输入", NotifyEnum.Warning));
                }
                else
                {
                    ViewBag.SigningOrganization = FanNumber.words_result.签发机关.words;
                    ViewBag.IssuanceDate = FanNumber.words_result.签发日期.words;
                    ViewBag.ExpirationDate = FanNumber.words_result.失效日期.words;
                }
                return View();
            }
        }

        [HttpPost]
        public ActionResult IdCardSubmit(IdCardInformation ici)
        {
            LoanApplyService loanapplyService = new LoanApplyService();
            IdCardInformationService idcardinformationService = new IdCardInformationService();
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            string sphone = borrower.Phone;
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
            LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.RepaymentStatus == CreditStatus.未审核).FirstOrDefault();
            if (sphone != "18824127298")
            {
                if (loanApply == null)
                {
                    Notification(new MessageResultModels("您尚未有申请中的融资租赁", NotifyEnum.Warning));
                    return RedirectToAction("LoanIndex", "Loan");
                }
            }
            else
            {
                loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && (o.RepaymentStatus == CreditStatus.未审核 || o.RepaymentStatus == CreditStatus.还款中)).FirstOrDefault();
            }
            LoanFileService loanfileService = new LoanFileService();
            LoanFile loanfile = loanfileService.Search(new LoanFile() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id).FirstOrDefault();
            if (loanfile == null)
            {
                Notification(new MessageResultModels("还没提交身份证照片", NotifyEnum.Warning));
                return RedirectToAction("LoanList", "Loan");
            }
            else
            {
                if (DateTimeUtil.ToDateTime(ici.ExpirationDate) < DateTime.Now.Date)
                {
                    loanApply.AuditTime = DateTime.Now;
                    loanApply.Auditor = new BorrowerAuthenticationService().GetAuthenticatedBorrower().FullName;
                    loanApply.AuditorRemark = "身份证已过期";
                    loanApply.RepaymentStatus = CreditStatus.审核未通过;
                    loanapplyService.Update(loanApply);
                    Notification(new MessageResultModels("身份证已过期", NotifyEnum.Warning));
                    return RedirectToAction("LoanList", "Loan");
                }

                int Age = CalculateAge(ici.Birth);//根据生日计算年龄
                if (Age < 18)
                {
                    loanApply.AuditTime = DateTime.Now;
                    loanApply.Auditor = new BorrowerAuthenticationService().GetAuthenticatedBorrower().FullName;
                    loanApply.AuditorRemark = "客户未满十八周岁";
                    loanApply.RepaymentStatus = CreditStatus.审核未通过;
                    loanapplyService.Update(loanApply);
                    Notification(new MessageResultModels("客户未满十八周岁", NotifyEnum.Warning));
                    return RedirectToAction("LoanList", "Loan");
                }

                IdCardInformation idCardinformation = idcardinformationService.Search(new IdCardInformation() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id).FirstOrDefault();
                if (idCardinformation == null)
                {
                    ici.BorrowerId = borrower.Id;
                    loanfile.IdCardInformationId = idcardinformationService.Insert(ici);
                }
                else
                {
                    idCardinformation.Address = ici.Address;
                    idCardinformation.Birth = ici.Birth;
                    idCardinformation.ExpirationDate = ici.ExpirationDate;
                    idCardinformation.IdCardNumber = ici.IdCardNumber;
                    idCardinformation.IssuanceDate = ici.IssuanceDate;
                    idCardinformation.Name = ici.Name;
                    idCardinformation.Nation = ici.Nation;
                    idCardinformation.Sex = ici.Sex;
                    idCardinformation.SigningOrganization = ici.SigningOrganization;
                    idcardinformationService.Update(idCardinformation);
                    loanfile.IdCardInformationId = idCardinformation.Id;
                }

                loanfileService.Update(loanfile);
                borrower.FullName = ici.Name;
                borrower.IDNumber = ici.IdCardNumber;
                borrower.IsIDNumber = 1;
                borrowerService.Update(borrower);

                Borrower sfBorrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.IDNumber == ici.IdCardNumber).FirstOrDefault();
                if (loanApply.CustomerClassification == "B")
                {
                    loanApply.AuditTime = DateTime.Now;
                    loanApply.Auditor = new BorrowerAuthenticationService().GetAuthenticatedBorrower().FullName;
                    loanApply.RepaymentStatus = CreditStatus.审核未通过;
                    loanapplyService.Update(loanApply);
                    borrower.IsRejected = true;
                    borrowerService.Update(borrower);
                    QiyebaoSms qiyebaoSms = new QiyebaoSms();
                    qiyebaoSms.SendSms(borrower.Phone, "很抱歉，客户" + borrower.Phone + "(" + borrower.FullName + ")租代购业务未获批");
                }
                else
                {
                    if (sfBorrower != null)
                    {
                        if (sfBorrower.IsRejected == true)
                        {
                            loanApply.AuditTime = DateTime.Now;
                            loanApply.Auditor = new BorrowerAuthenticationService().GetAuthenticatedBorrower().FullName;
                            loanApply.AuditorRemark = "用户已被拒绝过一次";
                            loanApply.RepaymentStatus = CreditStatus.审核未通过;
                            loanapplyService.Update(loanApply);
                            borrower.IsRejected = true;
                            borrowerService.Update(borrower);
                            Notification(new MessageResultModels("抱歉，您已经被拒绝审批过，无法进行再次申请", NotifyEnum.Warning));
                            return RedirectToAction("LoanList", "Loan");
                        }
                    }
                    List<string> FengKongWeiXinId = borrowerService.GetFengKong();
                    List<string> FengKongPhone = borrowerService.GetPhone();
                    string sContent = string.Format(@"有新的提单，客户手机号为{0}【车1号】", loanApply.CreditPhone);
                    QiyebaoSms qiyebaoSms = new QiyebaoSms();
                    WechatPushMessage wechatpushMessage = new WechatPushMessage();
                    foreach (string sPhone in FengKongPhone)
                    {
                        qiyebaoSms.SendSms(sPhone, sContent);
                    }
                    foreach (string sWeiXinId in FengKongWeiXinId)
                    {
                        wechatpushMessage.AuditProcessingReminder(sWeiXinId, loanApply.CreditPhone, borrower.FullName, DateTime.Now.ToString("yyyy年MM月dd日"));
                    }
                }

                Notification(new MessageResultModels("身份证信息提交成功", NotifyEnum.Success));
                return RedirectToAction("LoanList", "Loan");
            }
        }

        /// <summary>
        /// 根据出生日期，计算精确的年龄
        /// </summary>
        /// <param name="birthDate">生日</param>
        /// <returns></returns>
        public static int CalculateAge(string birthDay)
        {
            DateTime birthDate = DateTimeUtil.ToDateTime(birthDay);
            DateTime nowDateTime = DateTime.Now;
            int age = nowDateTime.Year - birthDate.Year;
            //再考虑月、天的因素
            if (nowDateTime.Month < birthDate.Month || (nowDateTime.Month == birthDate.Month && nowDateTime.Day < birthDate.Day))
            {
                age--;
            }
            return age;
        }

        public ActionResult BankCardInformation()
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

            LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.RepaymentStatus == CreditStatus.未审核).FirstOrDefault();
            if (loanApply == null)
            {
                Notification(new MessageResultModels("您尚未有申请中的融资租赁", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex", "Loan");
            }
            else
            {
                LoanFileService loanFileService = new LoanFileService();
                LoanFile loanfile = loanFileService.Search(new LoanFile { IsValid = true }).Where(o => o.LoanApplyId == loanApply.Id).FirstOrDefault();

                BankCardNumber bankCardNumber = CharacterRecognition.BankcardDistinguish(loanfile.BankCardPath);

                if (bankCardNumber.result == null)
                {
                    Notification(new MessageResultModels("银行卡号码无法识别,请手动输入", NotifyEnum.Warning));
                }
                else
                {
                    ViewBag.BankCardNumbe = bankCardNumber.result.bank_card_number;
                    ViewBag.BankCardType = bankCardNumber.result.bank_card_type == "1" ? "借记卡" : bankCardNumber.result.bank_card_type == "2" ? "信用卡" : "";
                    ViewBag.BankName = bankCardNumber.result.bank_name;
                }
                return View();
            }
        }

        [HttpPost]
        public ActionResult BankCardSubmit(BankCard bcn)
        {
            LoanApplyService loanapplyService = new LoanApplyService();
            BankCardService bankcardSerivce = new BankCardService();
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
            LoanFileService loanfileService = new LoanFileService();
            LoanFile loanfile = loanfileService.Search(new LoanFile() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id).FirstOrDefault();
            if (loanfile == null)
            {
                Notification(new MessageResultModels("还没提交银行卡照片", NotifyEnum.Success));
                return RedirectToAction("LoanList", "Loan");
            }
            else
            {
                BankCard bankCard = bankcardSerivce.Search(new BankCard() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id).OrderByDescending(o => o.CreateTime.Value).FirstOrDefault();
                if (bankCard == null)
                {
                    bcn.BorrowerId = borrower.Id;
                    loanfile.BankCardId = bankcardSerivce.Insert(bcn);
                    loanfileService.Update(loanfile);
                }
                else
                {
                    bankCard.BankCardType = bcn.BankCardType;
                    bankCard.Name = bcn.Name;
                    bankCard.Number = bcn.Number;
                    bankcardSerivce.Update(bankCard);
                    loanfile.IdCardInformationId = bankCard.Id;
                }
                loanfileService.Update(loanfile);
                Notification(new MessageResultModels("银行卡信息提交成功", NotifyEnum.Success));
                return RedirectToAction("LoanList", "Loan");
            }
        }

        public ActionResult GetMailList()
        {
            return View();
        }

        public ActionResult GetMyBill(int nId)
        {
            LoanApplyService loanapplyService = new LoanApplyService();
            BorrowService borrowService = new BorrowService();
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            LoanApply loanApply = loanapplyService.GetById(nId);
            List<Borrow> borrowList = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.LoanApplyId == loanApply.Id).OrderBy(o => o.Stages).ToList();
            ViewBag.Borrowlist = borrowList;
            return View();
        }

        public ActionResult Amortizationloan()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower == null)
            {
                return RedirectToAction("Create", "Borrower");
            }
            ViewBag.Phone = borrower.Phone;
            LoanApplyService loanapplyService = new LoanApplyService();
            BorrowService borrowService = new BorrowService();
            List<LoanApply> loanapplyList = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => (o.RepaymentStatus == CreditStatus.还款中 || o.RepaymentStatus == CreditStatus.还款完成) && o.BorrowerId == borrower.Id).ToList();
            if (loanapplyList.Count == 0)
            {
                ViewBag.IsLoanapply = false;
            }
            else
            {
                LoanApply loanapply = loanapplyList.Find(o => o.RepaymentStatus == CreditStatus.还款中);
                if (loanapply == null)
                {
                    ViewBag.tborrow = false;
                }
                else
                {
                    Borrow borrow = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.LoanApplyId == loanapply.Id && o.RepaymentDate >= DateTime.Now.Date && o.RepaymentPlanMode == null).OrderBy(o => o.Stages).FirstOrDefault();
                    if (borrow == null)
                    {
                        ViewBag.tborrow = false;
                    }
                    else
                    {
                        ViewBag.tborrow = true;
                        ViewBag.borrow = borrow;
                    }
                }
                List<Borrow> overdueBorrowList = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && (o.UnPrincipal + o.UnTotalInterest) > 0 && o.RepaymentDate < DateTime.Now.Date && o.LoanApplyId == loanapply.Id).ToList();
                ViewBag.OverdueBorrowlist = overdueBorrowList;
                ViewBag.LoanapplyList = loanapplyList;
                ViewBag.IsLoanapply = true;
            }
            return View();
        }

        public ActionResult InputPhone()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower.IsSalesman == true)
            {
                return View();
            }
            else
            {
                Notification(new MessageResultModels("您没有业务员权限", NotifyEnum.Warning));
                return RedirectToAction("LoanIndex", "Loan");
            }
        }

        [HttpPost]
        public string InputPhone(string Phone)
        {
            Borrower ywyborrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            LoanApplyService loanApplyService = new LoanApplyService();
            Session["BorrowerPhone"] = Phone;
            Borrower borrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == Phone).FirstOrDefault();
            if (borrower == null)
            {
                Borrower newborrower = new Borrower();
                newborrower.Guid = Guid.NewGuid().ToString();
                newborrower.Aliases = Phone;
                newborrower.Phone = Phone;
                newborrower.IsValidatePhone = 1;
                newborrower.CustomerServiceId = 0;
                newborrower.IsSalesman = false;
                int NewBorrowId = borrowerService.Insert(newborrower);

                loanApplyService.Insert(new LoanApply()
                {
                    SalesmanId = ywyborrower.Id,
                    RepaymentStatus = CreditStatus.未审核,
                    BorrowerId = NewBorrowId,
                    CreditPhone = Phone,
                    Step = 1,
                    Company = ywyborrower.Company
                });
                return "0";
            }
            else if (borrower.IsRejected == true)
            {
                return "1";
            }
            else
            {
                LoanApply loanApply = loanApplyService.Search(new LoanApply()
                {
                    CreditPhone = Phone
                }).Find(o => o.RepaymentStatus == CreditStatus.还款中 || o.RepaymentStatus == CreditStatus.未审核);
                if (loanApply != null)
                {
                    if (!string.IsNullOrEmpty(loanApply.LoanType.ToString()))
                    {
                        ViewBag.LoanType = (int)loanApply.LoanType;
                    }
                    else
                    {
                        ViewBag.LoanType = 0;
                    }
                    if (loanApply.Step == 1)
                    {
                        return "3";
                    }
                    else if (loanApply.Step == 2)
                    {
                        return "4";
                    }
                    else if (loanApply.Step == 3)
                    {
                        return "5";
                    }
                    else if (loanApply.Step == 4)
                    {
                        return "6";
                    }
                    else if (loanApply.Step == 5)
                    {
                        return "7";
                    }
                    else if (loanApply.Step == 6)
                    {
                        return "8";
                    }
                    else if (loanApply.Step == 7)
                    {
                        return "9";
                    }
                    else
                    {
                        return "2";
                    }

                }
                loanApplyService.Insert(new LoanApply()
                {
                    SalesmanId = ywyborrower.Id,
                    RepaymentStatus = CreditStatus.未审核,
                    BorrowerId = borrower.Id,
                    CreditPhone = Phone,
                    Step = 1,
                    Company = ywyborrower.Company
                });
                return "0";
            }
        }

        public ActionResult Prepayment(int nId)
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower == null)
            {
                return RedirectToAction("Create", "Borrower");
            }
            BorrowService borrowService = new BorrowService();
            Borrow borrow = borrowService.GetById(nId);
            return View(borrow);
        }

        [HttpPost]
        public ActionResult Repayment(int BorrowId, string Amount)
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower == null)
            {
                return RedirectToAction("Create", "Borrower");
            }
            BorrowService borrowService = new BorrowService();
            Borrow borrow = borrowService.GetById(BorrowId);
            decimal sumAmount = (borrow.UnPrincipal + borrow.UnTotalInterest).Value;
            if (sumAmount == ConvertUtil.ToDecimal(Amount))
            {
                return Json(new
                {
                    Result = true,
                    wxJsApiParam = wxJsApiParam(borrower, "正常还利息", sumAmount)
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Result = false,
                    wxJsApiParam = "还款金额有错"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult MentionBack(int nId)
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            LoanApplyService loanapplyService = new LoanApplyService();
            DiscountService discountService = new DiscountService();
            LoanApply loanapply = loanapplyService.GetById(nId);
            decimal dStandardDeduction = 0;
            Discount discount = discountService.Search(new Discount() { IsValid = true }).Where(o => o.LeftAmount > 0 && o.BorrowerId == borrower.Id && o.LoanApplyId == loanapply.Id && o.SecondAuditResult == true).FirstOrDefault();
            if (discount != null)
            {
                dStandardDeduction = discount.LeftAmount.Value;
            }
            BorrowService borrowService = new BorrowService();
            List<Borrow> allborrowList = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.LoanApplyId == loanapply.Id && o.BorrowerId == borrower.Id).ToList();

            List<Borrow> borrowList = allborrowList.FindAll(o => (o.UnTotalInterest + o.UnPrincipal) > 0);
            Borrow newborrow = borrowList.Find(o => o.ActualRepaymentDate == null && o.RepaymentDate >= DateTime.Now.Date && DateTime.Now.Date > o.RepaymentDate.Value.AddMonths(-1).Date);
            List<Borrow> overdueborrowlist = borrowList.FindAll(o => o.ActualRepaymentDate == null && o.OverDay > 0 && o.UnTotalInterest > 0);
            decimal overdueInterest = overdueborrowlist.Sum(o => o.UnTotalInterest).Value;

            decimal unSumPrincipal = borrowList.Sum(o => o.UnPrincipal).Value;
            decimal oneInterest = borrowList.FirstOrDefault().Interest.Value;
            decimal dServiceCharge = 0;
            decimal dSumAmount = 0;
            if (loanapply.InterestDate.Value.Date < new DateTime(2019, 07, 9))
            {
                if (loanapply.InterestDate.Value.AddDays(15) >= DateTime.Now.Date)
                {
                    dServiceCharge = 200;
                    dSumAmount = unSumPrincipal + dServiceCharge;
                }
                else if (loanapply.InterestDate.Value.AddMonths(3) >= DateTime.Now.Date)
                {
                    if (newborrow == null)
                    {
                        dServiceCharge = 200;
                    }
                    else
                    {
                        dServiceCharge = 200 + oneInterest;
                    }
                    dSumAmount = unSumPrincipal + dServiceCharge;
                }
                else
                {
                    List<Borrow> newborrowList = borrowList.FindAll(o => o.Stages <= 11);
                    if (newborrowList.Count > 0)
                    {
                        DateTime dtTime = allborrowList.Find(o => o.RepaymentDate >= DateTime.Now.Date && DateTime.Now.Date > o.RepaymentDate.Value.AddMonths(-1).Date).RepaymentDate.Value.Date;
                        if (newborrow == null)
                        {
                            if (DateTime.Now.Date.AddDays(15) > dtTime)
                            {
                                dServiceCharge = oneInterest;
                            }
                            else
                            {
                                dServiceCharge = 0;
                            }
                        }
                        else
                        {
                            if (DateTime.Now.Date.AddDays(15) > dtTime)
                            {
                                dServiceCharge = oneInterest * 2;
                            }
                            else
                            {
                                dServiceCharge = oneInterest;
                            }
                        }

                    }
                    else
                    {
                        dServiceCharge = oneInterest;
                    }
                    dSumAmount = unSumPrincipal + dServiceCharge;
                }
            }
            else if (loanapply.InterestDate.Value.Date < new DateTime(2019, 08, 27))
            {
                if (loanapply.InterestDate.Value.AddMonths(loanapply.WithinMonth) >= DateTime.Now.Date)
                {
                    if (newborrow == null)
                    {
                        dServiceCharge = 200;
                    }
                    else
                    {
                        dServiceCharge = 200 + oneInterest;
                    }
                    dSumAmount = unSumPrincipal + dServiceCharge;
                }
                else
                {
                    if (newborrow == null)
                    {
                        dServiceCharge = 100;
                    }
                    else
                    {
                        dServiceCharge = 100 + oneInterest;
                    }
                    dSumAmount = unSumPrincipal + dServiceCharge;
                }
            }
            else
            {
                if (loanapply.InterestDate.Value.AddMonths(loanapply.WithinMonth) >= DateTime.Now.Date)
                {
                    if (newborrow == null)
                    {
                        dServiceCharge = 300;
                    }
                    else
                    {
                        dServiceCharge = 300 + oneInterest;
                    }
                    dSumAmount = unSumPrincipal + dServiceCharge;
                }
                else
                {
                    if (newborrow == null)
                    {
                        dServiceCharge = 200;
                    }
                    else
                    {
                        dServiceCharge = 200 + oneInterest;
                    }
                    dSumAmount = unSumPrincipal + dServiceCharge;
                }
            }
            return Json(new
            {
                Result = true,
                unSumPrincipal = unSumPrincipal,
                dSumAmount = dSumAmount + overdueInterest - dStandardDeduction,
                StandardDeduction = dStandardDeduction,
                dServiceCharge = dServiceCharge,
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public string GetWxPayment(int nId)
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            LoanApplyService loanapplyService = new LoanApplyService();
            DiscountService discountService = new DiscountService();
            LoanApply loanapply = loanapplyService.GetById(nId);
            decimal dStandardDeduction = 0;
            Discount discount = discountService.Search(new Discount() { IsValid = true }).Where(o => o.LeftAmount > 0 && o.BorrowerId == borrower.Id && o.LoanApplyId == loanapply.Id && o.SecondAuditResult == true).FirstOrDefault();
            if (discount != null)
            {
                dStandardDeduction = discount.LeftAmount.Value;
            }
            BorrowService borrowService = new BorrowService();
            List<Borrow> allborrowList = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.LoanApplyId == loanapply.Id && o.BorrowerId == borrower.Id).ToList();

            List<Borrow> borrowList = allborrowList.FindAll(o => (o.UnTotalInterest + o.UnPrincipal) > 0);
            Borrow newborrow = borrowList.Find(o => o.ActualRepaymentDate == null && o.RepaymentDate >= DateTime.Now.Date && DateTime.Now.Date > o.RepaymentDate.Value.AddMonths(-1).Date);
            List<Borrow> overdueborrowlist = borrowList.FindAll(o => o.ActualRepaymentDate == null && o.OverDay > 0 && o.UnTotalInterest > 0);
            decimal overdueInterest = overdueborrowlist.Sum(o => o.UnTotalInterest).Value;

            decimal unSumPrincipal = borrowList.Sum(o => o.UnPrincipal).Value;
            decimal oneInterest = borrowList.FirstOrDefault().Interest.Value;

            decimal dServiceCharge = 0;
            decimal dSumAmount = 0;
            if (loanapply.InterestDate.Value.Date < new DateTime(2019, 07, 9))
            {
                if (loanapply.InterestDate.Value.AddDays(15) >= DateTime.Now.Date)
                {
                    dServiceCharge = 200;
                    dSumAmount = unSumPrincipal + dServiceCharge;
                }
                else if (loanapply.InterestDate.Value.AddMonths(3) >= DateTime.Now.Date)
                {
                    if (newborrow == null)
                    {
                        dServiceCharge = 200;
                    }
                    else
                    {
                        dServiceCharge = 200 + oneInterest;
                    }
                    dSumAmount = unSumPrincipal + dServiceCharge;
                }
                else
                {
                    List<Borrow> newborrowList = borrowList.FindAll(o => o.Stages <= 11);
                    if (newborrowList.Count > 0)
                    {
                        DateTime dtTime = allborrowList.Find(o => o.RepaymentDate >= DateTime.Now.Date && DateTime.Now.Date > o.RepaymentDate.Value.AddMonths(-1).Date).RepaymentDate.Value.Date;
                        if (newborrow == null)
                        {
                            if (DateTime.Now.Date.AddDays(15) > dtTime)
                            {
                                dServiceCharge = oneInterest;
                            }
                            else
                            {
                                dServiceCharge = 0;
                            }
                        }
                        else
                        {
                            if (DateTime.Now.Date.AddDays(15) > dtTime)
                            {
                                dServiceCharge = oneInterest * 2;
                            }
                            else
                            {
                                dServiceCharge = oneInterest;
                            }
                        }
                    }
                    else
                    {
                        dServiceCharge = oneInterest;
                    }
                    dSumAmount = unSumPrincipal + dServiceCharge;
                }
            }
            else if (loanapply.InterestDate.Value.Date < new DateTime(2019, 08, 27))
            {
                if (loanapply.InterestDate.Value.AddMonths(loanapply.WithinMonth) >= DateTime.Now.Date)
                {
                    if (newborrow == null)
                    {
                        dServiceCharge = 200;
                    }
                    else
                    {
                        dServiceCharge = 200 + oneInterest;
                    }
                    dSumAmount = unSumPrincipal + dServiceCharge;
                }
                else
                {
                    if (newborrow == null)
                    {
                        dServiceCharge = 100;
                    }
                    else
                    {
                        dServiceCharge = 100 + oneInterest;
                    }
                    dSumAmount = unSumPrincipal + dServiceCharge;
                }
            }
            else
            {
                if (loanapply.InterestDate.Value.AddMonths(loanapply.WithinMonth) >= DateTime.Now.Date)
                {
                    if (newborrow == null)
                    {
                        dServiceCharge = 300;
                    }
                    else
                    {
                        dServiceCharge = 300 + oneInterest;
                    }
                    dSumAmount = unSumPrincipal + dServiceCharge;
                }
                else
                {
                    if (newborrow == null)
                    {
                        dServiceCharge = 200;
                    }
                    else
                    {
                        dServiceCharge = 200 + oneInterest;
                    }
                    dSumAmount = unSumPrincipal + dServiceCharge;
                }
            }
            return wxJsApiParam(borrower, "提还", dSumAmount - dStandardDeduction + overdueInterest);
        }



        [HttpPost]
        public string GetYqPayment(int nId)
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            DiscountService discountService = new DiscountService();
            BorrowService borrowService = new BorrowService();
            Borrow borrow = borrowService.GetById(nId);
            decimal dStandardDeduction = 0;
            Discount discount = discountService.Search(new Discount() { IsValid = true }).Where(o => o.LeftAmount > 0 && o.BorrowerId == borrower.Id && o.LoanApplyId == borrow.LoanApplyId && o.SecondAuditResult == true).FirstOrDefault();
            if (discount != null)
            {
                dStandardDeduction = discount.LeftAmount.Value;
            }
            decimal dSumAmount = (borrow.UnPrincipal + borrow.UnTotalInterest - dStandardDeduction).Value;
            return wxJsApiParam(borrower, "逾期", dSumAmount);

        }
        /// <summary>
        /// 创建充值
        /// </summary>
        /// <param name="rechargeMoney"></param>
        /// <param name="godId"></param>
        /// <param name="orderNumber"></param>
        /// <param name="recharegeMode"></param>
        /// <param name="recharegeFee"></param>
        private void CreateRecharge(decimal rechargeMoney, int godId, string orderNumber, RechargeMode recharegeMode, decimal recharegeFee)
        {
            RechargeService rechargeService = new RechargeService();
            // 增加一条充值记录, 但是不设置审核状态
            int rechargeId = rechargeService.Insert(new Recharge()
            {
                Amount = rechargeMoney,
                BorrowerId = godId,
                RechargeMode = recharegeMode,
                OrderNumber = orderNumber,
                ActualRechargeFee = recharegeFee,
                RechargeTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Remark = "微信支付"
            });
        }


        /// <summary>
        /// 调用微信的jsapi支付
        /// </summary>
        /// <returns></returns>
        private string wxJsApiParam(Borrower borrower, string sAttach, decimal dSumAmount)
        {
            string wxJsApiParam = "";
            string openid = borrower.WeiXinId;
            Random r = new Random();
            //若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数
            JsApiPay jsApiPay = new JsApiPay();
            jsApiPay.openid = openid;
            jsApiPay.total_fee = ConvertUtil.ToInt(dSumAmount * 100);
            jsApiPay.out_trade_no = DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(0, 99999);
            jsApiPay.attach = sAttach;

            //检测是否给当前页面传递了相关参数
            if (string.IsNullOrEmpty(openid) || jsApiPay.total_fee == 0)
            {
                Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面传参出错,请返回重试" + "</span>");

            }

            //JSAPI支付预处理
            try
            {
                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
                wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数       
                //在页面上显示订单信息
                //Response.Write("<span style='color:#00CD00;font-size:20px'>订单详情：</span><br/>");
                //Response.Write("<span style='color:#00CD00;font-size:20px'>" + unifiedOrderResult.ToPrintStr() + "</span>");

            }
            catch (Exception ex)
            {
                Response.Write("<span style='color:#FF0000;font-size:20px'>" + ex.Message + "</span>");
            }
            CreateRecharge(ConvertUtil.ToDecimal(jsApiPay.total_fee), borrower.Id, jsApiPay.out_trade_no, RechargeMode.微信充值, ConvertUtil.ToDecimal(jsApiPay.total_fee * 0.003));
            return wxJsApiParam;
        }


        public ActionResult OverdueUnpaid(int nLoanApplyId)
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            BorrowService borrowService = new BorrowService();
            List<Borrow> borrowList = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && (o.UnPrincipal + o.UnTotalInterest) > 0 && o.RepaymentDate < DateTime.Now.Date && o.LoanApplyId == nLoanApplyId).OrderBy(o => o.Stages).ToList();
            ViewBag.Borrowlist = borrowList;
            return View(borrowList);
        }

        public bool IsOverdueBorrow(int nLoanApplyId)
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            BorrowService borrowService = new BorrowService();
            List<Borrow> borrowList = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && (o.UnPrincipal + o.UnTotalInterest) > 0 && o.RepaymentDate < DateTime.Now.Date && o.LoanApplyId == nLoanApplyId).ToList();
            if (borrowList.Count == 0)
                return true;
            else
                return false;
        }


        public ActionResult IsFirstOverdue(int Stages, int bId)
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            BorrowService borrowService = new BorrowService();
            Borrow borrow = borrowService.GetById(bId);
            Borrow lastborrow = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && (o.UnPrincipal + o.UnTotalInterest) > 0 && o.RepaymentDate < DateTime.Now.Date && o.LoanApplyId == borrow.LoanApplyId && o.Stages == Stages - 1).FirstOrDefault();
            if (lastborrow != null)
            {
                return Json(new
                {
                    Result = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Result = true,
                    SumAmount = borrow.UnPrincipal + borrow.UnTotalInterest
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetPhone(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public string InsertPhone(string Phone)
        {
            if (Phone.Length == 11)
            {
                Borrower borrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == Phone).FirstOrDefault();
                if (borrower == null)
                {
                    return "0";
                }
                else
                {
                    Session["BorrowerPhone"] = Phone;
                    return "1";
                }
            }
            else
            {
                List<Borrower> borrowerList = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.FullName == Phone).ToList();
                if (borrowerList.Count == 0)
                {
                    return "0";
                }
                else if (borrowerList.Count > 1)
                {
                    return "2";
                }
                else
                {
                    Session["BorrowerPhone"] = borrowerList.First().Phone;
                    return "1";
                }
            }
        }

        public ActionResult RentPhone()
        {
            return View();
        }

        [HttpPost]
        public string RentPhone(string Phone)
        {
            Borrower ywyborrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            LoanApplyService loanApplyService = new LoanApplyService();
            Session["BorrowerPhone"] = Phone;
            Borrower borrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == Phone).FirstOrDefault();
            if (borrower == null)
            {
                Borrower newborrower = new Borrower();
                newborrower.Guid = Guid.NewGuid().ToString();
                newborrower.Aliases = Phone;
                newborrower.Phone = Phone;
                newborrower.IsValidatePhone = 1;
                newborrower.CustomerServiceId = 0;
                newborrower.IsSalesman = false;
                int NewBorrowId = borrowerService.Insert(newborrower);

                loanApplyService.Insert(new LoanApply()
                {
                    SalesmanId = ywyborrower.Id,
                    RepaymentStatus = CreditStatus.未审核,
                    BorrowerId = NewBorrowId,
                    CreditPhone = Phone,
                    Step = 1,
                    Company = ywyborrower.Company,
                    LoanType = LoanType.租客
                });
                return "0";
            }
            else if (borrower.IsRejected == true)
            {
                return "1";
            }
            else
            {
                LoanApply loanApply = loanApplyService.Search(new LoanApply()
                {
                    CreditPhone = Phone
                }).Find(o => o.RepaymentStatus == CreditStatus.还款中 || o.RepaymentStatus == CreditStatus.未审核);
                if (loanApply != null)
                {
                    if (!string.IsNullOrEmpty(loanApply.LoanType.ToString()))
                    {
                        ViewBag.LoanType = (int)loanApply.LoanType;
                    }
                    else
                    {
                        ViewBag.LoanType = 0;
                    }
                    if (loanApply.Step == 1)
                    {
                        return "3";
                    }
                    else if (loanApply.Step == 2)
                    {
                        return "4";
                    }
                    else if (loanApply.Step == 3)
                    {
                        return "5";
                    }
                    else if (loanApply.Step == 4)
                    {
                        return "6";
                    }
                    else if (loanApply.Step == 5)
                    {
                        return "7";
                    }
                    else if (loanApply.Step == 6)
                    {
                        return "8";
                    }
                    else if (loanApply.Step == 7)
                    {
                        return "9";
                    }
                    else
                    {
                        return "2";
                    }

                }
                loanApplyService.Insert(new LoanApply()
                {
                    SalesmanId = ywyborrower.Id,
                    RepaymentStatus = CreditStatus.未审核,
                    BorrowerId = borrower.Id,
                    CreditPhone = Phone,
                    Step = 1,
                    Company = ywyborrower.Company,
                    LoanType = LoanType.租客
                });
                return "0";
            }
        }


        public ActionResult RentPayment()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            BorrowService borrowService = new BorrowService();
            LoanApplyService loanApplyservice = new LoanApplyService();
            LoanApply loanapply = loanApplyservice.Search(new LoanApply() { IsValid = true }).Find(o => o.RepaymentStatus == CreditStatus.还款中 && o.BorrowerId == borrower.Id);
            ViewBag.unDeposit = loanapply.unDeposit;
            Borrow borrow = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.LoanApplyId == loanapply.Id && o.RepaymentPlanMode == null).OrderBy(o => o.Stages).FirstOrDefault();
            return View(borrow);
        }

        [HttpPost]
        public ActionResult RentPayment(string Amount)
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower == null)
            {
                return RedirectToAction("Create", "Borrower");
            }
            BorrowService borrowService = new BorrowService();
            LoanApplyService loanApplyservice = new LoanApplyService();
            LoanApply loanapply = loanApplyservice.Search(new LoanApply() { IsValid = true }).Find(o => o.RepaymentStatus == CreditStatus.还款中 && o.BorrowerId == borrower.Id);
            decimal sumAmount = 0;
            Decimal? unDeposit = loanapply.unDeposit;
            Borrow borrow = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.LoanApplyId == loanapply.Id && o.RepaymentPlanMode == null).OrderBy(o => o.Stages).FirstOrDefault();
            if (unDeposit > 0)
            {
                sumAmount = loanapply.unDeposit.Value;
            }
            else
            {
                sumAmount = (borrow.UnTotalInterest + borrow.UnPrincipal).Value;
            }
            if (sumAmount == ConvertUtil.ToDecimal(Amount))
            {
                return Json(new
                {
                    Result = true,
                    wxJsApiParam = wxJsApiParam(borrower, unDeposit > 0 ? "还押金" : "还租金", sumAmount)
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    Result = false,
                    wxJsApiParam = "支付金额有错"
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
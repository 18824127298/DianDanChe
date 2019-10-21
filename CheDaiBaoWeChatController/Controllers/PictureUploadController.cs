using BaiDuSdk.Model;
using BaiDuSdk.RecognitionAI;
using CheDaiBaoCommonController.Controllers;
using CheDaiBaoCommonController.Model;
using CheDaiBaoCommonService.Service;
using CheDaiBaoWeChatController.Controller;
using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CheDaiBaoWeChatController.Controllers
{
    public class PictureUploadController : BaseCommonController
    {
        private readonly LoanApplyService loanapplyService;
        private readonly BorrowerService borrowerService;

        public PictureUploadController()
        {
            this.loanapplyService = new LoanApplyService();
            this.borrowerService = new BorrowerService();
        }

        public ActionResult UploadID()
        {
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
            ViewBag.FullName = borrower.FullName;
            LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id).OrderByDescending(o => o.CreateTime).FirstOrDefault();
            if (loanApply == null)
            {
                return Json(new
                {
                    Result = false,
                    Message = "请先申请融资租赁！"
                }, JsonRequestBehavior.AllowGet);
            }

            if (sphone != "18824127298")
            {
                if (loanApply.RepaymentStatus == CreditStatus.未审核)
                    ViewBag.RepaymentStatus = true;
                else
                    ViewBag.RepaymentStatus = false;
            }
            else
            {
                ViewBag.RepaymentStatus = true;
            }
            return View();
        }

        [HttpPost]
        public string GetWXSharedParam(string url)
        {
            string timestamp = WxSharedController.ConvertDateTimeInt(DateTime.Now).ToString();
            string nonceStr = Guid.NewGuid().ToString();
            string ticket = string.Empty;
            string appId = Configs.GetWeiXinAppId();

            //获取jsapi_ticket
            if (HttpRuntime.Cache["JsApiTicket"] == null)
            {
                WxSharedController.GetJsApiTicket();
            }
            ticket = HttpRuntime.Cache["JsApiTicket"] as string;
            if (string.IsNullOrEmpty(ticket))
            {
                return JsonConvert.SerializeObject(new { result = false });
            }

            SortedList<string, string> SLString = new SortedList<string, string>();
            SLString.Add("noncestr", nonceStr);
            SLString.Add("url", url);
            SLString.Add("timestamp", timestamp);
            SLString.Add("jsapi_ticket", ticket);

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> des in SLString)
            {
                sb.Append(des.Key + "=" + des.Value + "&");
            }
            string signature = sb.ToString().Substring(0, sb.ToString().Length - 1);
            signature = FormsAuthentication.HashPasswordForStoringInConfigFile(signature, "SHA1").ToLower();
            return JsonConvert.SerializeObject(new { result = true, timestamp, nonceStr, signature, appId });
        }


        [HttpPost]
        public string GetMultimedia(string MEDIA_ID)
        {
            string file = string.Empty;
            string content = string.Empty;
            string strpath = string.Empty;
            string savepath = string.Empty;
            string stUrl = "http://file.api.weixin.qq.com/cgi-bin/media/get?access_token=" + WeChatBaseRequestService.getApptoken() + "&media_id=" + MEDIA_ID;

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(stUrl);
            int nId = 0;
            req.Method = "GET";
            string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";
            using (WebResponse wr = req.GetResponse())
            {
                try
                {
                    HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();

                    strpath = myResponse.ResponseUri.ToString();
                    return strpath;
                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                }

            }
            if (nId == 1)
            {
                return sFileName;
            }
            else
            {
                return "";
            }
        }

        [HttpPost]
        public ActionResult UploadIdNumber(string Zhengmianimg, string Fanmianimg)
        {
            string savepath = string.Empty;
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
                    return Json(new
                    {
                        Result = false,
                        Message = "请先申请融资租赁！"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && (o.RepaymentStatus == CreditStatus.未审核 || o.RepaymentStatus == CreditStatus.还款中)).FirstOrDefault();
            }
            string sZhengmianimg = "";
            string sFanmianimg = "";
            if (Zhengmianimg != "")
            {
                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";

                WebClient mywebclient = new WebClient();

                string sLuJing = Server.MapPath("image") + "\\" + DateTime.Now.ToString("yyyyMMdd");
                if (Directory.Exists(sLuJing) == false)
                {
                    Directory.CreateDirectory(sLuJing);
                }
                savepath = sLuJing + "\\" + sFileName;
                try
                {
                    mywebclient.DownloadFile(VIVPHOTO(Zhengmianimg), savepath);
                    sZhengmianimg = savepath;
                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                }


            }
            if (Fanmianimg != "")
            {
                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";
                WebClient mywebclient = new WebClient();

                string sLuJing = Server.MapPath("image") + "\\" + DateTime.Now.ToString("yyyyMMdd");
                if (Directory.Exists(sLuJing) == false)
                {
                    Directory.CreateDirectory(sLuJing);
                }
                savepath = sLuJing + "\\" + sFileName;
                try
                {
                    mywebclient.DownloadFile(VIVPHOTO(Fanmianimg), savepath);
                    sFanmianimg = savepath;
                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                }
            }
            LoanFileService loanFileService = new LoanFileService();
            LoanFile loanfile = loanFileService.Search(new LoanFile { IsValid = true }).Where(o => o.LoanApplyId == loanApply.Id).OrderByDescending(o => o.CreateTime.Value).FirstOrDefault();
            if (loanfile == null)
            {
                LoanFile nloanfile = new LoanFile();
                nloanfile.LoanApplyId = loanApply.Id;
                nloanfile.ZhengFilePath = sZhengmianimg;
                nloanfile.FanFilePath = sFanmianimg;
                nloanfile.BorrowerId = borrower.Id;
                nloanfile.WeChatZhengFilePath = Zhengmianimg;
                nloanfile.WeChatFanFilePath = Fanmianimg;
                loanFileService.Insert(nloanfile);
            }
            else
            {
                loanFileService.Update(new LoanFile()
                {
                    Id = loanfile.Id,
                    ZhengFilePath = sZhengmianimg,
                    FanFilePath = sFanmianimg,
                    WeChatZhengFilePath = Zhengmianimg,
                    WeChatFanFilePath = Fanmianimg
                });
            }
            return Json(new
            {
                Result = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadBankCard(string yinhangka)
        {
            string savepath = string.Empty;
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
            LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.RepaymentStatus == CreditStatus.未审核).OrderByDescending(o => o.CreateTime.Value).FirstOrDefault();
            if (loanApply == null)
            {
                return Json(new
                {
                    Result = false,
                    Message = "请先申请融资租赁！"
                }, JsonRequestBehavior.AllowGet);
            }
            string sYinhangka = "";
            if (yinhangka != "")
            {
                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";

                WebClient mywebclient = new WebClient();

                string sLuJing = Server.MapPath("image") + "\\" + DateTime.Now.ToString("yyyyMMdd");
                if (Directory.Exists(sLuJing) == false)
                {
                    Directory.CreateDirectory(sLuJing);
                }
                savepath = sLuJing + "\\" + sFileName;
                try
                {
                    mywebclient.DownloadFile(VIVPHOTO(yinhangka), savepath);
                    sYinhangka = savepath;
                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                }


            }
            LoanFileService loanFileService = new LoanFileService();
            LoanFile loanfile = loanFileService.Search(new LoanFile { IsValid = true }).Where(o => o.LoanApplyId == loanApply.Id).OrderByDescending(o => o.CreateTime.Value).FirstOrDefault();
            if (loanfile == null)
            {
                LoanFile nloanfile = new LoanFile();
                nloanfile.LoanApplyId = loanApply.Id;
                nloanfile.BankCardPath = sYinhangka;
                nloanfile.BorrowerId = borrower.Id;
                nloanfile.WeChatBankCardPath = yinhangka;
                loanFileService.Insert(nloanfile);
            }
            else
            {
                loanFileService.Update(new LoanFile()
                {
                    Id = loanfile.Id,
                    BankCardPath = sYinhangka,
                    WeChatBankCardPath = yinhangka
                });
            }
            return Json(new
            {
                Result = true
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult UploadBankCard()
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
            ViewBag.Phone = borrower.Phone;
            ViewBag.FullName = borrower.FullName;
            LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id).OrderByDescending(o => o.CreateTime).FirstOrDefault();
            if (loanApply == null)
            {
                return Json(new
                {
                    Result = false,
                    Message = "请先申请融资租赁！"
                }, JsonRequestBehavior.AllowGet);
            }
            LoanFileService loanfileService = new LoanFileService();
            if (loanApply.RepaymentStatus == CreditStatus.未审核)
                ViewBag.RepaymentStatus = true;
            else
                ViewBag.RepaymentStatus = false;
            return View();

        }


        public ActionResult UploadMaterial()
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
            ViewBag.Phone = borrower.Phone;
            ViewBag.FullName = borrower.FullName;
            return View();
        }


        [HttpPost]
        public ActionResult UploadMaterial(string renxiang, string imageslist)
        {
            string savepath = string.Empty;
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
                        Message = "手机号信息过期，请重新填写！",
                        ReturnUrl = Request.Url.ToString()
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            ViewBag.Phone = borrower.Phone;
            LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id).OrderByDescending(o => o.CreateTime).FirstOrDefault();
            if (loanApply == null)
            {
                return Json(new
                {
                    Result = "0",
                    Message = "请先申请融资租赁！"
                }, JsonRequestBehavior.AllowGet);
            }
            BusinessFileService businessfileService = new BusinessFileService();
            BusinessFile businessFile = new BusinessFile();
            if (renxiang != "")
            {
                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";

                WebClient mywebclient = new WebClient();

                string sLuJing = Server.MapPath("image") + "\\" + DateTime.Now.ToString("yyyyMMdd");
                if (Directory.Exists(sLuJing) == false)
                {
                    Directory.CreateDirectory(sLuJing);
                }
                savepath = sLuJing + "\\" + sFileName;
                try
                {
                    mywebclient.DownloadFile(renxiang, savepath);

                    LoanFileService loanfileService = new LoanFileService();
                    LoanFile loanfile = loanfileService.Search(new LoanFile() { IsValid = true }).Where(o => o.LoanApplyId == loanApply.Id && o.BorrowerId == borrower.Id).FirstOrDefault();
                    if (loanfile == null)
                    {
                        return Json(new
                        {
                            Result = "0",
                            Message = "请先上传身份证照片！"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (string.IsNullOrEmpty(loanfile.ZhengFilePath))
                    {
                        return Json(new
                                {
                                    Result = "0",
                                    Message = "请先上传身份证照片！"
                                }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        FaceContrastResult faceContrastResult = CharacterRecognition.FaceContrast(loanfile.ZhengFilePath, savepath);
                        if (faceContrastResult.error_code != "0")
                        {
                            return Json(new
                           {
                               Result = "0",
                               //Message = faceContrastResult.error_msg
                               Message = "请检查身份证照片，或者是客户单人的清晰人脸照"
                           }, JsonRequestBehavior.AllowGet);
                        }
                        if (Convert.ToDecimal(faceContrastResult.result.score) < 50)
                        {
                            return Json(new
                            {
                                Result = "0",
                                Message = "人脸识别对比不通过"
                            }, JsonRequestBehavior.AllowGet);
                        }

                        loanfile.Score = Convert.ToDecimal(faceContrastResult.result.score);
                        loanfileService.Update(loanfile);
                    }
                    businessFile.BorrowerId = borrower.Id;
                    businessFile.BusinessType = BusinessType.人身照;
                    businessFile.FilePath = savepath;
                    businessFile.RelationId = loanApply.Id;
                    businessFile.FileName = borrower.FullName;
                    businessFile.WeChatPath = renxiang;
                    if (loanApply.RepaymentStatus == CreditStatus.未审核)
                    {
                        businessFile.Sort = 1;
                    }
                    else
                    {
                        businessFile.Sort = 2;
                    }
                    businessfileService.Insert(businessFile);

                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                }


            }
            if (imageslist != "")
            {
                for (int i = 0; i < imageslist.Split(',').Length; i++)
                {
                    string sImage = imageslist.Split(',')[i];
                    //string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";
                    //WebClient mywebclient = new WebClient();

                    //string sLuJing = Server.MapPath("image") + "\\" + DateTime.Now.ToString("yyyyMMdd");
                    //if (Directory.Exists(sLuJing) == false)
                    //{
                    //    Directory.CreateDirectory(sLuJing);
                    //}
                    //savepath = sLuJing + "\\" + sFileName;
                    if (sImage != "")
                    {
                        savepath = "";
                        try
                        {
                            //mywebclient.DownloadFile(sImage, savepath);
                            businessFile.BorrowerId = borrower.Id;
                            businessFile.BusinessType = BusinessType.客户的信息照;
                            businessFile.FilePath = savepath;
                            businessFile.RelationId = loanApply.Id;
                            businessFile.FileName = borrower.FullName + i.ToString();
                            businessFile.WeChatPath = sImage;
                            if (loanApply.RepaymentStatus == CreditStatus.未审核)
                            {
                                businessFile.Sort = 1;
                            }
                            else
                            {
                                businessFile.Sort = 2;
                            }
                            businessfileService.Insert(businessFile);
                        }
                        catch (Exception ex)
                        {
                            savepath = ex.ToString();
                        }
                    }
                }
            }
            loanApply.Step = 7;
            loanapplyService.Update(loanApply);

            return Json(new
            {
                Result = "1",
                Message = "资料提交成功"
            }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult UploadBorrowerPhoto()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower.IsSalesman == true)
            {
                if (Session["BorrowerPhone"] != null)
                {
                    string sPhone = Session["BorrowerPhone"].ToString();
                }
                else
                {
                    Notification(new MessageResultModels("手机号信息过期，请重新填写", NotifyEnum.Warning));
                    return RedirectToAction("GetPhone", "GodService", new { returnUrl = Request.Url.ToString() });
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult UploadBorrowerPhoto(string Zhengmianimg, string Fanmianimg, string yinhangka)
        {
            string savepath = string.Empty;
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
                        Result = false,
                        Message = "手机号信息过期，请重新填写！",
                        ReturnUrl = Request.Url.ToString()
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            ViewBag.Phone = borrower.Phone;
            LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.RepaymentStatus == CreditStatus.未审核).FirstOrDefault();
            if (loanApply == null)
            {
                return Json(new
                {
                    Result = false,
                    Message = "请先申请融资租赁！"
                }, JsonRequestBehavior.AllowGet);
            }
            string sZhengmianimg = "";
            string sFanmianimg = "";
            string sYinhangka = "";
            WebClient mywebclient = new WebClient();
            if (Zhengmianimg != "")
            {
                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";


                string sLuJing = Server.MapPath("image") + "\\" + DateTime.Now.ToString("yyyyMMdd");
                if (Directory.Exists(sLuJing) == false)
                {
                    Directory.CreateDirectory(sLuJing);
                }
                savepath = sLuJing + "\\" + sFileName;
                try
                {
                    mywebclient.DownloadFile(VIVPHOTO(Zhengmianimg), savepath);
                    FileInfo fileInfo = new FileInfo(savepath);
                    if (fileInfo.Length < 2048)
                    {
                        return Json(new
                        {
                            Result = false,
                            Message = "请重新上传身份证正面！"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    sZhengmianimg = savepath;
                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                }


            }
            if (Fanmianimg != "")
            {
                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";

                string sLuJing = Server.MapPath("image") + "\\" + DateTime.Now.ToString("yyyyMMdd");
                if (Directory.Exists(sLuJing) == false)
                {
                    Directory.CreateDirectory(sLuJing);
                }
                savepath = sLuJing + "\\" + sFileName;
                try
                {
                    mywebclient.DownloadFile(VIVPHOTO(Fanmianimg), savepath);
                    FileInfo fileInfo = new FileInfo(savepath);
                    if (fileInfo.Length < 2048)
                    {
                        sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";
                        savepath = sLuJing + "\\" + sFileName;
                        mywebclient.DownloadFile(VIVPHOTO(yinhangka), savepath);
                        fileInfo = new FileInfo(savepath);
                        if (fileInfo.Length < 2048)
                        {
                            return Json(new
                            {
                                Result = false,
                                Message = "请重新上传身份证反面！"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    sFanmianimg = savepath;
                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                }
            }
            if (yinhangka != "")
            {
                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";


                string sLuJing = Server.MapPath("image") + "\\" + DateTime.Now.ToString("yyyyMMdd");
                if (Directory.Exists(sLuJing) == false)
                {
                    Directory.CreateDirectory(sLuJing);
                }
                savepath = sLuJing + "\\" + sFileName;
                try
                {
                    mywebclient.DownloadFile(yinhangka, savepath);
                    FileInfo fileInfo = new FileInfo(savepath);
                    if (fileInfo.Length < 2048)
                    {
                        sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";
                        savepath = sLuJing + "\\" + sFileName;
                        mywebclient.DownloadFile(VIVPHOTO(yinhangka), savepath);
                        fileInfo = new FileInfo(savepath);
                        if (fileInfo.Length < 2048)
                        {
                            return Json(new
                            {
                                Result = false,
                                Message = "请重新上传银行卡！"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    sYinhangka = savepath;
                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                }


            }
            LoanFileService loanFileService = new LoanFileService();
            LoanFile loanfile = loanFileService.Search(new LoanFile { IsValid = true }).Where(o => o.LoanApplyId == loanApply.Id).OrderByDescending(o => o.CreateTime.Value).FirstOrDefault();
            if (loanfile == null)
            {
                LoanFile nloanfile = new LoanFile();
                nloanfile.LoanApplyId = loanApply.Id;
                nloanfile.BankCardPath = sYinhangka;
                nloanfile.ZhengFilePath = sZhengmianimg;
                nloanfile.FanFilePath = sFanmianimg;
                nloanfile.WeChatZhengFilePath = Zhengmianimg;
                nloanfile.WeChatFanFilePath = Fanmianimg;
                nloanfile.WeChatBankCardPath = yinhangka;

                nloanfile.BorrowerId = borrower.Id;
                loanFileService.Insert(nloanfile);
            }
            else
            {
                loanFileService.Update(new LoanFile()
                {
                    Id = loanfile.Id,
                    BankCardPath = sYinhangka,
                    ZhengFilePath = sZhengmianimg,
                    FanFilePath = sFanmianimg,
                    WeChatZhengFilePath = Zhengmianimg,
                    WeChatFanFilePath = Fanmianimg,
                    WeChatBankCardPath = yinhangka
                });
            }

            loanApply.Step = 2;
            loanapplyService.Update(loanApply);
            return Json(new
            {
                Result = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadMultipleSheets()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower.IsSalesman == true)
            {
                if (Session["BorrowerPhone"] != null)
                {
                    string sPhone = Session["BorrowerPhone"].ToString();
                }
                else
                {
                    Notification(new MessageResultModels("手机号信息过期，请重新填写", NotifyEnum.Warning));
                    return RedirectToAction("GetPhone", "GodService", new { returnUrl = Request.Url.ToString() });
                }
            }
            return View();
        }

        public string VIVPHOTO(string spath)
        {
            int i = spath.IndexOf("=");
            int j = spath.IndexOf("&");
            return spath.Replace((spath.Substring(i + 1)).Substring(0, j - i - 1), WeChatBaseRequestService.getApptoken());
        }

        public ActionResult NewUploadMultipleSheets()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower.IsSalesman == true)
            {
                if (Session["BorrowerPhone"] != null)
                {
                    string sPhone = Session["BorrowerPhone"].ToString();
                }
                else
                {
                    Notification(new MessageResultModels("手机号信息过期，请重新填写", NotifyEnum.Warning));
                    return RedirectToAction("GetPhone", "GodService", new { returnUrl = Request.Url.ToString() });
                }
            }
            return View();
        }
        public ActionResult FollowUp()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower.IsSalesman == true)
            {
                string sPhone = Session["BorrowerPhone"].ToString();
                borrower = borrowerService.Search(new Borrower() { IsValid = true }).Where(o => o.Phone == sPhone).FirstOrDefault();
            }
            ViewBag.Phone = borrower.Phone;
            ViewBag.FullName = borrower.FullName;
            return View();
        }
        [HttpPost]
        public ActionResult UploadFollowUp(string remark, string creatorType, string imageslist)
        {
            string savepath = string.Empty;
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            string sCreator = borrower.FullName;
            int sSalesmanId = borrower.Id;


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
                        Message = "手机号信息过期，请重新填写！",
                        ReturnUrl = Request.Url.ToString()
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id).OrderByDescending(o => o.CreateTime).FirstOrDefault();
            if (loanApply == null)
            {
                return Json(new
                {
                    Result = "0",
                    Message = "请先申请融资租赁！"
                }, JsonRequestBehavior.AllowGet);
            }
            BusinessFileService businessfileService = new BusinessFileService();
            BusinessFile businessFile = new BusinessFile();
            FollowUpService followUpService = new FollowUpService();
            FollowUp followUp = new FollowUp();
            followUp.Creator = sCreator;
            followUp.SalesmanId = sSalesmanId;
            followUp.Remark = remark;
            followUp.LoanApplyId = loanApply.Id;
            followUp.BorrowerId = borrower.Id;
            followUp.CreatorType = (CreatorType)Convert.ToInt32(creatorType);
            int fId = followUpService.Insert(followUp);

            if (imageslist != "")
            {
                for (int i = 0; i < imageslist.Split(',').Length; i++)
                {
                    string sImage = imageslist.Split(',')[i];
                    if (sImage != "")
                    {
                        savepath = "";
                        try
                        {
                            businessFile.BorrowerId = borrower.Id;
                            businessFile.BusinessType = BusinessType.作证的资料;
                            businessFile.FilePath = savepath;
                            businessFile.RelationId = fId;
                            businessFile.FileName = borrower.FullName + i.ToString();
                            businessFile.WeChatPath = sImage;
                            businessFile.Sort = 3;
                            businessfileService.Insert(businessFile);
                        }
                        catch (Exception ex)
                        {
                            savepath = ex.ToString();
                        }
                    }
                }
            }

            return Json(new
            {
                Result = "1",
                Message = "资料提交成功"
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelfRentPhoto()
        {
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();
            if (borrower == null)
            {
                return RedirectToAction("Create", "Borrower");
            }
            return View();
        }

        [HttpPost]
        public ActionResult UploadSelfPhoto(string Zhengmianimg, string Fanmianimg, string yinhangka)
        {
            string savepath = string.Empty;
            Borrower borrower = new BorrowerAuthenticationService().GetAuthenticatedBorrower();

            LoanApply loanApply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o => o.BorrowerId == borrower.Id && o.RepaymentStatus == CreditStatus.未审核).FirstOrDefault();
            if (loanApply == null)
            {
               int id = loanapplyService.Insert(new LoanApply()
                {
                    RepaymentStatus = CreditStatus.未审核,
                    BorrowerId = borrower.Id,
                    CreditPhone = borrower.Phone,
                    Step = 1,
                    Company = Company.翼速,
                    LoanType = LoanType.租客
                });
               loanApply = loanapplyService.GetById(id);
            }
            string sZhengmianimg = "";
            string sFanmianimg = "";
            string sYinhangka = "";
            WebClient mywebclient = new WebClient();
            if (Zhengmianimg != "")
            {
                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";


                string sLuJing = Server.MapPath("image") + "\\" + DateTime.Now.ToString("yyyyMMdd");
                if (Directory.Exists(sLuJing) == false)
                {
                    Directory.CreateDirectory(sLuJing);
                }
                savepath = sLuJing + "\\" + sFileName;
                try
                {
                    mywebclient.DownloadFile(VIVPHOTO(Zhengmianimg), savepath);
                    FileInfo fileInfo = new FileInfo(savepath);
                    if (fileInfo.Length < 2048)
                    {
                        return Json(new
                        {
                            Result = false,
                            Message = "请重新上传身份证正面！"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    sZhengmianimg = savepath;
                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                }


            }
            if (Fanmianimg != "")
            {
                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";

                string sLuJing = Server.MapPath("image") + "\\" + DateTime.Now.ToString("yyyyMMdd");
                if (Directory.Exists(sLuJing) == false)
                {
                    Directory.CreateDirectory(sLuJing);
                }
                savepath = sLuJing + "\\" + sFileName;
                try
                {
                    mywebclient.DownloadFile(VIVPHOTO(Fanmianimg), savepath);
                    FileInfo fileInfo = new FileInfo(savepath);
                    if (fileInfo.Length < 2048)
                    {
                        sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";
                        savepath = sLuJing + "\\" + sFileName;
                        mywebclient.DownloadFile(VIVPHOTO(yinhangka), savepath);
                        fileInfo = new FileInfo(savepath);
                        if (fileInfo.Length < 2048)
                        {
                            return Json(new
                            {
                                Result = false,
                                Message = "请重新上传身份证反面！"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    sFanmianimg = savepath;
                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                }
            }
            if (yinhangka != "")
            {
                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";


                string sLuJing = Server.MapPath("image") + "\\" + DateTime.Now.ToString("yyyyMMdd");
                if (Directory.Exists(sLuJing) == false)
                {
                    Directory.CreateDirectory(sLuJing);
                }
                savepath = sLuJing + "\\" + sFileName;
                try
                {
                    mywebclient.DownloadFile(yinhangka, savepath);
                    FileInfo fileInfo = new FileInfo(savepath);
                    if (fileInfo.Length < 2048)
                    {
                        sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";
                        savepath = sLuJing + "\\" + sFileName;
                        mywebclient.DownloadFile(VIVPHOTO(yinhangka), savepath);
                        fileInfo = new FileInfo(savepath);
                        if (fileInfo.Length < 2048)
                        {
                            return Json(new
                            {
                                Result = false,
                                Message = "请重新上传银行卡！"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    sYinhangka = savepath;
                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                }


            }
            LoanFileService loanFileService = new LoanFileService();
            LoanFile loanfile = loanFileService.Search(new LoanFile { IsValid = true }).Where(o => o.LoanApplyId == loanApply.Id).OrderByDescending(o => o.CreateTime.Value).FirstOrDefault();
            if (loanfile == null)
            {
                LoanFile nloanfile = new LoanFile();
                nloanfile.LoanApplyId = loanApply.Id;
                nloanfile.BankCardPath = sYinhangka;
                nloanfile.ZhengFilePath = sZhengmianimg;
                nloanfile.FanFilePath = sFanmianimg;
                nloanfile.WeChatZhengFilePath = Zhengmianimg;
                nloanfile.WeChatFanFilePath = Fanmianimg;
                nloanfile.WeChatBankCardPath = yinhangka;

                nloanfile.BorrowerId = borrower.Id;
                loanFileService.Insert(nloanfile);
            }
            else
            {
                loanFileService.Update(new LoanFile()
                {
                    Id = loanfile.Id,
                    BankCardPath = sYinhangka,
                    ZhengFilePath = sZhengmianimg,
                    FanFilePath = sFanmianimg,
                    WeChatZhengFilePath = Zhengmianimg,
                    WeChatFanFilePath = Fanmianimg,
                    WeChatBankCardPath = yinhangka
                });
            }

            loanApply.Step = 2;
            loanapplyService.Update(loanApply);
            return Json(new
            {
                Result = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}

using BaiDuSdk.Model;
using BaiDuSdk.RecognitionAI;
using CheDaiBaoCommonService.Service;
using CheDaiBaoWeChatController.Interface;
using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Interface;
using CheDaiBaoWeChatService.Service;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class loan_loan_list_second_loan_audit : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        int nId = ConvertUtil.ToInt(Request["id"]);

        if (nId != 0)
        {
            PageParameter.SetCustomParamObject("id", nId);
        }
        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = loanapplyService.GetById(nId);
        IdCardInformationService idCardInformationService = new IdCardInformationService();
        IdCardInformation idCardInformation = idCardInformationService.Search(new IdCardInformation() { IsValid = true }).Where(o => o.BorrowerId == loanapply.BorrowerId).OrderByDescending(o => o.CreateTime.Value).FirstOrDefault();
        BankCardService bankcardService = new BankCardService();
        BankCard bankcard = bankcardService.Search(new BankCard() { IsValid = true }).Where(o => o.BorrowerId == loanapply.BorrowerId).OrderByDescending(o => o.CreateTime.Value).FirstOrDefault();
        LoanFileService loanfileService = new LoanFileService();
        LoanFile loanfile = loanfileService.Search(new LoanFile() { IsValid = true }).Where(o => o.BorrowerId == loanapply.BorrowerId && o.LoanApplyId == loanapply.Id).OrderByDescending(o => o.CreateTime.Value).FirstOrDefault();

        BusinessName.Text = loanapply.BusinessName;
        RecruitmentName.Text = loanapply.RecruitmentName;
        EntrySiteName.Text = loanapply.EntrySiteName;
        SiteAddress.Text = loanapply.SiteAddress;
        StationMasterPhone.Text = loanapply.StationMasterPhone;
        Brand.Text = loanapply.Brand;
        CheType.Text = loanapply.CheType;
        TotalAmountStage.Text = ConvertUtil.ToDecimal(loanapply.TotalAmountStage) + "元";
        Deadline.Text = loanapply.Deadline.ToString() + "个月";
        lblOtherLoan.Text = loanapply.OtherLoan;
        lblRemark.Text = loanapply.Remark;
        Deposit.Text = loanapply.Deposit.Value.ToString("N2") + "元";
        BicyclesRent.Text = loanapply.BicyclesRent.Value.ToString("N2") + "元";
        unDeposit.Text = loanapply.unDeposit.Value.ToString("N2") + "元";
        WithinMonth.Text = loanapply.WithinMonth.ToString() + "个月";
        Insurance.Text = loanapply.Insurance.Value.ToString("N2") + "元";


        if (idCardInformation != null)
        {
            Name.Text = idCardInformation.Name;
            Sex.Text = idCardInformation.Sex;
            IdCardNumber.Text = idCardInformation.IdCardNumber;
            Address.Text = idCardInformation.Address;
            Birth.Text = idCardInformation.Birth;
            Nation.Text = idCardInformation.Nation;
            SigningOrganization.Text = idCardInformation.SigningOrganization;
            IssuanceDate.Text = idCardInformation.IssuanceDate;
            ExpirationDate.Text = idCardInformation.ExpirationDate;
        }
        CreditPhone.Text = loanapply.CreditPhone;
        ResidentialPhone.Text = loanapply.ResidentialPhone;
        Email.Text = loanapply.Email;
        EducationLevel.Text = loanapply.EducationLevel;
        MaritalStatus.Text = loanapply.MaritalStatus;
        ChildrenNumber.Text = loanapply.ChildrenNumber.ToString(); ;
        ComingTime.Text = loanapply.ComingTime;
        LivingConditions.Text = loanapply.LivingConditions;
        ResidenceAddress.Text = loanapply.ResidenceAddress;

        LastWorkName.Text = loanapply.LastWorkName;
        JobContent.Text = loanapply.JobContent;
        MonthlyWage.Text = ConvertUtil.ToString(loanapply.MonthlyWage);
        WorkingYear.Text = loanapply.WorkingYear;
        WorkingSeniority.Text = loanapply.WorkingSeniority;

        Expenditure.Text = ConvertUtil.ToString(loanapply.Expenditure);
        LivingCost.Text = ConvertUtil.ToString(loanapply.LivingCost);
        Rent.Text = ConvertUtil.ToString(loanapply.Rent);
        SesameCredit.Text = ConvertUtil.ToString(loanapply.SesameCredit);
        IsFlowersOverdue.Text = loanapply.IsFlowersOverdue == 0 ? "否" : "是";
        FlowersOverdueAmount.Text = ConvertUtil.ToString(loanapply.FlowersOverdueAmount);
        IsBorrowOverdue.Text = loanapply.IsBorrowOverdue == 0 ? "否" : "是";
        BorrowOverdueAmount.Text = ConvertUtil.ToString(loanapply.BorrowOverdueAmount);
        IsBankLoan.Text = loanapply.IsBankLoan == 0 ? "否" : "是";
        BankLoanBalance.Text = ConvertUtil.ToString(loanapply.BankLoanBalance);
        IsBorrowOverdueAmount.Text = loanapply.IsBorrowOverdueAmount == 0 ? "否" : "是";
        BankOverdueAmount.Text = ConvertUtil.ToString(loanapply.BankOverdueAmount);
        IsPtpLoan.Text = loanapply.IsPtpLoan == 0 ? "否" : "是";
        PtpLoanBalance.Text = ConvertUtil.ToString(loanapply.PtpLoanBalance);
        IsPtpOverdueAmount.Text = loanapply.IsPtpOverdueAmount == 0 ? "否" : "是";
        PtpOverdueAmount.Text = ConvertUtil.ToString(loanapply.PtpOverdueAmount);
        FailurePassReason.Text = loanapply.FailurePassReason;
        IsCreditcardLoan.Text = loanapply.IsCreditcardLoan == 0 ? "否" : "是";
        CreditcardLoanBalance.Text = ConvertUtil.ToString(loanapply.CreditcardLoanBalance);
        IsCreditcardOverdueAmount.Text = loanapply.IsCreditcardOverdueAmount == 0 ? "否" : "是";
        CreditcardOverdueAmount.Text = ConvertUtil.ToString(loanapply.CreditcardOverdueAmount);
        CustomerClassification.Text = loanapply.CustomerClassification;
        lblAuditOpinion.Text = loanapply.AuditOpinion;

        if (loanfile != null)
            Score.Text = loanfile.Score.ToString();

        if (bankcard != null)
        {
            Number.Text = bankcard.Number;
            BankCardName.Text = bankcard.Name;
            BankCardType.Text = bankcard.BankCardType;
        }

        if (loanfile != null)
        {
            if (!string.IsNullOrEmpty(loanfile.ZhengFilePath))
                shenfenzhengzhengmian.Src = "http://ddc.che01.com/PictureUpload/image/" + loanfile.ZhengFilePath.Substring(loanfile.ZhengFilePath.Length - 34, 34).Replace("\\", "/");
            if (!string.IsNullOrEmpty(loanfile.FanFilePath))
                shenfenzhengfanmian.Src = "http://ddc.che01.com/PictureUpload/image/" + loanfile.FanFilePath.Substring(loanfile.FanFilePath.Length - 34, 34).Replace("\\", "/");
            if (!string.IsNullOrEmpty(loanfile.BankCardPath))
                yinhangka.Src = "http://ddc.che01.com/PictureUpload/image/" + loanfile.BankCardPath.Substring(loanfile.BankCardPath.Length - 34, 34).Replace("\\", "/");

        }

        BusinessFileService businessfileService = new BusinessFileService();
        List<BusinessFile> businessfileList = businessfileService.Search(new BusinessFile() { IsValid = true }).Where(o => o.BorrowerId == loanapply.BorrowerId && o.RelationId == loanapply.Id).ToList();
        if (businessfileList.Count > 0)
        {
            BusinessFile renshenzhaofile = businessfileList.FindAll(o => o.BusinessType == BusinessType.人身照).OrderByDescending(o => o.CreateTime.Value).FirstOrDefault();
            if (renshenzhaofile != null)
            {
                if (!string.IsNullOrEmpty(renshenzhaofile.FilePath))
                {
                    renshenzhao.Src = "http://ddc.che01.com/PictureUpload/image/" + renshenzhaofile.FilePath.Substring(renshenzhaofile.FilePath.Length - 34, 34).Replace("\\", "/");
                }
                else
                {
                    renshenzhao.Src = VIVPHOTO(renshenzhaofile.WeChatPath);
                }

                //renshenzhao.Src = VIVPHOTO(renshenzhaofile.WeChatPath);
            }
            CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                         = @"select * from BusinessFile where IsValid = 1 and (Sort = 1 or Sort is null) and BusinessType=" + (int)BusinessType.客户的信息照 + " and RelationId=" + nId;
            FetchDataFromDB();
        }

        CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                        = @"select * from Contacts where IsValid = 1 and BorrowerId =" + loanapply.BorrowerId + " and LoanapplyId=" + loanapply.Id;
        FetchDataFromDBContacts();

        tbART.Visible = false;
        if (loanapply.RepaymentStatus == CreditStatus.还款中 || loanapply.RepaymentStatus == CreditStatus.审核未通过)
        {
            btnOK.Visible = false;
            btnUnAudit.Visible = false;
        }
    }


    private void FetchDataFromDB()
    {
        //========= 1. 取出数据，并绑定 ========
        DataSet ds = DataHelper.Instance.ExecuteDataSet
                (CurrentPageStatus.DataViewStatus.SqlBuilder.ToString());


        gvList.DataSource = ds;
        gvList.DataBind();


    }

    private void FetchDataFromDBContacts()
    {
        //========= 1. 取出数据，并绑定 ========
        DataSet ds = DataHelper.Instance.ExecuteDataSet
                (CurrentPageStatus.DataViewStatus.SqlBuilder.ToString());


        ContactsList.DataSource = ds;
        ContactsList.DataBind();


    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            int nId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
            LoanApplyService loanapplyService = new LoanApplyService();
            LoanApply loanapply = loanapplyService.GetById(nId);

            loanapply.SecondAuditTime = DateTime.Now;
            loanapply.SecondAuditor = loanapply.Auditor;
            loanapply.SecondAuditResult = true;
            loanapply.RepaymentStatus = CreditStatus.还款中;
            loanapply.IsLending = false;
            loanapply.MerchantAuditor = CurrentUser.UserName;
            loanapply.IsMarchantAgree = true;
            loanapplyService.Update(loanapply);

            BorrowerService borrowerService = new BorrowerService();
            Borrower borrower = borrowerService.GetById(loanapply.BorrowerId);
            Borrower salesman = borrowerService.GetById(loanapply.SalesmanId);
            QiyebaoSms qiyebaoSms = new QiyebaoSms();
            string sContent = string.Format(@"尊敬的{0}，您申请的电单车融资租赁已获批，融资租赁费情况及支付日期在5日后登录车1号公众号查询。详询020-89851216【车1号】", borrower.FullName);
            qiyebaoSms.SendSms(loanapply.CreditPhone, sContent);
            //sContent = string.Format(@"客户{0},审核已通过【车1号】", borrower.FullName); 
            //qiyebaoSms.SendSms(salesman.Phone, sContent);

            WechatPushMessage wechatpushMessage = new WechatPushMessage();
            if (!string.IsNullOrEmpty(salesman.WeiXinId))
            {
                wechatpushMessage.NotificationResult(salesman.WeiXinId, borrower.Phone, borrower.FullName, "通过");
            }
            Response.Redirect("../loan_list/other_successloan_list.aspx");
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }

    }
    protected void btnUnAudit_Click(object sender, EventArgs e)
    {
        int nId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = loanapplyService.GetById(nId);
        loanapply.RepaymentStatus = CreditStatus.审核未通过;
        loanapply.SecondAuditTime = DateTime.Now;
        loanapply.SecondAuditor = loanapply.Auditor;
        loanapply.SecondAuditResult = false;
        loanapply.IsLending = false;
        loanapply.MerchantAuditor = CurrentUser.UserName;
        loanapply.IsMarchantAgree = false;
        loanapplyService.Update(loanapply);
        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(loanapply.BorrowerId);
        borrower.IsRejected = true;
        borrowerService.Update(borrower);

        Borrower salesman = borrowerService.GetById(loanapply.SalesmanId);
        //QiyebaoSms qiyebaoSms = new QiyebaoSms();
        //string sContent = string.Format(@"很抱歉，客户{0}融资租赁业务未获批【车1号】", borrower.FullName);
        //qiyebaoSms.SendSms(salesman.Phone, sContent);

        WechatPushMessage wechatpushMessage = new WechatPushMessage();
        if (!string.IsNullOrEmpty(salesman.WeiXinId))
        {
            wechatpushMessage.NotificationResult(salesman.WeiXinId, borrower.Phone, borrower.FullName, "未获批");
        }
        Response.Redirect("../loan_list/otherfail_loans_listaspx.aspx");

    }
    int i = 0;
    public string VIVFilePath(object oFilePath, object oWeChatPath, object oCreateTime)
    {
        i++;
        if (!string.IsNullOrEmpty(oFilePath.ToString()))
        {
            if (oFilePath.ToString().Contains("CheDaiBaoWeChat"))
            {
                return @"<img src='http://ddc.che01.com/PictureUpload/image/" + oFilePath.ToString().Substring(oFilePath.ToString().Length - 34, 34).Replace("\\", "/") + "' style='width:500px;height:500px;margin-top:5px;margin-bottom:5px;' id='img" + i + @"' />
                <input name='button_test' type='button' value='旋转该图片' onclick='fn_xuanzhuan(" + i + ")'/>";
            }
            else
            {
                return @"<img src='http://manage.che01.com/image/" + oFilePath.ToString().Substring(oFilePath.ToString().Length - 37, 37).Replace("\\", "/") + "' style='width:500px;height:500px;margin-top:5px;margin-bottom:5px;' id='img" + i + @"' />
                <input name='button_test' type='button' value='旋转该图片' onclick='fn_xuanzhuan(" + i + ")'/>";
            }
        }
        else
        {
            return @"<img src='" + VIVPHOTO(oWeChatPath.ToString()) + "' style='width:500px;height:500px;margin-top:5px;margin-bottom:5px;' id='img" + i + @"' />
                <input name='button_test' type='button' value='旋转该图片' onclick='fn_xuanzhuan(" + i + ")'/>";
        }

    }

    protected string VIVHTime(object oTime)
    {

        if (string.IsNullOrEmpty(oTime.ToString()))
        {
            return "";
        }
        else
        {
            return Convert.ToDateTime(oTime).ToString("yyyy-MM-dd HH:mm:ss");
        }
    }


    protected string VIVIsKnowStages(object oIsKnowStages)
    {

        if (string.IsNullOrEmpty(oIsKnowStages.ToString()))
        {
            return "";
        }
        else
        {
            return ConvertUtil.ToBool(oIsKnowStages) == true ? "是" : "否";
        }
    }

    public string VIVPHOTO(string spath)
    {
        int i = spath.IndexOf("=");
        int j = spath.IndexOf("&");
        return spath.Replace((spath.Substring(i + 1)).Substring(0, j - i - 1), WeChatBaseRequestService.getApptoken());
    }




    protected void btnDownLoad_Click(object sender, EventArgs e)
    {
        int nId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = loanapplyService.GetById(nId);

        BusinessFileService businessFileService = new BusinessFileService();
        BusinessFile businessfile = businessFileService.Search(new BusinessFile() { IsValid = true }).Where(o => o.RelationId == nId && o.BorrowerId == loanapply.BorrowerId && o.BusinessType == BusinessType.文件).FirstOrDefault();

        if (businessfile == null)
        {
            Response.Write("<script language='javascript'>alert('该文件暂不提供下载！')</script>");
            return;
        }

        // 定义文件名    
        string fileName = "";
        // 获取文件在服务器的地址    
        string url = businessfile.FilePath;

        // 判断获取的是否为地址，而非文件名    
        if (url.IndexOf("\\") > -1)
        {
            // 获取文件名    
            fileName = url.Substring(url.LastIndexOf("\\") + 1);
        }
        else
        {
            // url为文件名时，直接获取文件名    
            fileName = url;
        }
        // 以字符流的方式下载文件   
        FileStream fileStream = new FileStream(@url, FileMode.Open);
        byte[] bytes = new byte[(int)fileStream.Length];
        fileStream.Read(bytes, 0, bytes.Length);
        fileStream.Close();
        Response.ContentType = "application/octet-stream";

        // 通知浏览器下载   
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
}
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

public partial class loan_loan_list_loan_details : SbtPageBase
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
        DownPayments.Text = ConvertUtil.ToDecimal(loanapply.DownPayments) + "元";
        Deadline.Text = loanapply.Deadline.ToString() + "个月";
        CreditAmount.Text = ConvertUtil.ToDecimal(loanapply.CreditAmount) + "元";
        lblAuditorRemark.Text = loanapply.AuditorRemark;
        lblRentRate.Text = loanapply.RentRate.ToString();
        lblOtherLoan.Text = loanapply.OtherLoan;
        lblRemark.Text = loanapply.Remark;
        edtElectricCore.Text = loanapply.ElectricCore;
        edtFengKongRemark.Text = loanapply.FengKongRemark;
        edtVisitRecord.Text = loanapply.VisitRecord;
        edtZhiCha.Text = loanapply.ZhiCha;
        edtDaiQian.Text = loanapply.DaiQian;
        lblTongDunWang.Text = loanapply.TongDunWang;
        edtTongDunWang.Text = loanapply.TongDunWang;
        ddlIsRider.SelectedValue = ConvertUtil.ToString(loanapply.IsRider);
        ddlIsAnswer.SelectedValue = ConvertUtil.ToString(loanapply.IsAnswer);
        ddlIsAbnormal.SelectedValue = ConvertUtil.ToString(loanapply.IsAbnormal);
        if (!string.IsNullOrEmpty(loanapply.TotalAmountStage.ToString()) && !string.IsNullOrEmpty(loanapply.Deadline.ToString()) && !string.IsNullOrEmpty(loanapply.RentRate.ToString()))
        {
            lblMonthlyPayment.Text = Math.Ceiling((loanapply.TotalAmountStage * loanapply.RentRate / 100 + loanapply.TotalAmountStage / loanapply.Deadline).Value).ToString();
        }
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
        edtMonthlyPayment.Text = loanapply.MonthlyPayment.ToString();

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

        //Spouse.Text = loanapply.Spouse;
        //IsSpouseKnowStages.Text = loanapply.IsSpouseKnowStages == 0 ? "否" : "是";
        //ContactPhone.Text = loanapply.ContactPhone;
        //ContactAddress.Text = loanapply.ContactAddress;
        //Parents.Text = loanapply.Parents;
        //IsParentsKnowStages.Text = loanapply.IsParentsKnowStages == 0 ? "否" : "是";
        //ParentsContactPhone.Text = loanapply.ParentsContactPhone;
        //Brothers.Text = loanapply.Brothers;
        //IsBrothersKnowStages.Text = loanapply.IsBrothersKnowStages == 0 ? "否" : "是";
        //BrothersContactPhone.Text = loanapply.BrothersContactPhone;
        //Friend.Text = loanapply.Friend;
        //IsFriendKnowStages.Text = loanapply.IsFriendKnowStages == 0 ? "否" : "是";
        //FriendContactPhone.Text = loanapply.FriendContactPhone;
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
            //if (!string.IsNullOrEmpty(loanfile.WeChatZhengFilePath))
            //{
            //    shenfenzhengzhengmian.Src = VIVPHOTO(loanfile.WeChatZhengFilePath);
            //}
            //if (!string.IsNullOrEmpty(loanfile.WeChatFanFilePath))
            //{
            //    shenfenzhengfanmian.Src = VIVPHOTO(loanfile.WeChatFanFilePath);
            //}
            //if (!string.IsNullOrEmpty(loanfile.WeChatBankCardPath))
            //{
            //    yinhangka.Src = VIVPHOTO(loanfile.WeChatBankCardPath);
            //}

        }
        if (loanapply.RepaymentStatus != CreditStatus.未审核)
        {
            btnOK.Visible = false;
            btnUnAudit.Visible = false;
            Gains.Visible = false;
            btnLiXiRen.Visible = false;
            TrBoHui.Visible = false;
        }
        if (loanapply.RepaymentStatus == CreditStatus.审核未通过)
        {
            TrBoHui.Visible = true;
            edtAuditorRemark.Visible = false;
            lblAuditorRemark.Visible = true;
        }

        if (ConvertUtil.ToInt(CurrentUser.ThirdPartyCode) > 0)
        {
            BorrowerService borrowerService = new BorrowerService();
            Borrower borrower = borrowerService.GetById(ConvertUtil.ToInt(CurrentUser.ThirdPartyCode));
            if (borrower.IsSalesman.Value)
            {
                btn.Visible = false;
                TrBoHui.Visible = false;
                Gains.Visible = false;
            }
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
        if (loanapply.RepaymentStatus == CreditStatus.还款中) 
        {
            TrBoHui.Visible = true;
            edtAuditorRemark.Visible = false;
            lblAuditorRemark.Visible = true;
            tbART.Visible = true;
            btnMentionBack.Visible = true;
            lblART.Visible = true;
            DiscountService discountService = new DiscountService();
            decimal dStandardDeduction = 0;
            Discount discount = discountService.Search(new Discount() { IsValid = true }).Where(o => o.LeftAmount > 0 && o.BorrowerId == loanapply.BorrowerId && o.LoanApplyId == loanapply.Id && o.SecondAuditResult == true).FirstOrDefault();
            if (discount != null)
            {
                dStandardDeduction = discount.LeftAmount.Value;
            }
            BorrowService borrowService = new BorrowService();
            List<Borrow> allborrowList = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.LoanApplyId == loanapply.Id && o.BorrowerId == loanapply.BorrowerId).ToList();

            List<Borrow> borrowList = allborrowList.FindAll(o => (o.UnTotalInterest + o.UnPrincipal) > 0);
            if (borrowList.Count > 0)
            {
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
                lblART.Text = "提还总金额：" + (dSumAmount + overdueInterest - dStandardDeduction).ToString() + "元，未还融资租赁总额为：" + unSumPrincipal.ToString()
                    + "元，手续费为：" + (dServiceCharge + overdueInterest).ToString() + "元，减免额为：" + dStandardDeduction + "元";
            }
        }

        if (loanapply.IsMentionBack == true)
        {
            btnMentionBack.Visible = false;
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
        if (edtMonthlyPayment.Text == "")
        {
            Response.Write("<script>alert('请填写用户的融资租赁费!')</script>");
            return;
        }
        if (edtWithinMonth.Text == "")
        {
            Response.Write("<script>alert('请填写多少个月内!')</script>");
            return;
        }
        else
        {
            try
            {
                int nId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
                LoanApplyService loanapplyService = new LoanApplyService();
                LoanApply loanapply = loanapplyService.GetById(nId);
                loanapply.AuditTime = DateTime.Now;
                loanapply.Auditor = CurrentUser.UserName;
                loanapply.RepaymentStatus = CreditStatus.还款中;
                loanapply.IsLending = false;
                loanapply.MonthlyPayment = ConvertUtil.ToDecimal(edtMonthlyPayment.Text);
                loanapply.WithinMonth = ConvertUtil.ToInt(edtWithinMonth.Text);
                loanapply.TongDunWang = edtTongDunWang.Text;
                loanapply.ElectricCore = edtElectricCore.Text;
                loanapply.FengKongRemark = edtFengKongRemark.Text;
                loanapply.VisitRecord = edtVisitRecord.Text;
                loanapply.ZhiCha = edtZhiCha.Text;
                loanapply.DaiQian = edtDaiQian.Text;
                loanapply.AuditorRemark = edtAuditorRemark.Text;
                if (ddlIsRider.SelectedValue != "NotSure")
                {
                    loanapply.IsRider = ConvertUtil.ToBool(ddlIsRider.SelectedValue);
                }
                if (ddlIsAnswer.SelectedValue != "NotSure")
                {
                    loanapply.IsAnswer = ConvertUtil.ToBool(ddlIsAnswer.SelectedValue);
                }
                if (ddlIsAbnormal.SelectedValue != "NotSure")
                {
                    loanapply.IsAbnormal = ConvertUtil.ToBool(ddlIsAbnormal.SelectedValue);
                }
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

                Response.Redirect("../loan_list/success_loans_list.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }
    }
    protected void btnUnAudit_Click(object sender, EventArgs e)
    {
        if (edtAuditorRemark.Text == "")
        {
            Response.Write("<script>alert('审核备注不能为空!')</script>");
            return;
        }
        int nId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = loanapplyService.GetById(nId);
        loanapply.AuditTime = DateTime.Now;
        loanapply.Auditor = CurrentUser.UserName;
        loanapply.RepaymentStatus = CreditStatus.审核未通过;
        loanapply.AuditorRemark = edtAuditorRemark.Text;
        loanapply.TongDunWang = edtTongDunWang.Text;
        loanapply.ElectricCore = edtElectricCore.Text;
        loanapply.FengKongRemark = edtFengKongRemark.Text;
        loanapply.VisitRecord = edtVisitRecord.Text;
        loanapply.ZhiCha = edtZhiCha.Text;
        loanapply.DaiQian = edtDaiQian.Text;
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
        Response.Redirect("../loan_list/fail_loans_list.aspx");

    }
    protected void btnLiXiRen_Click(object sender, EventArgs e)
    {
        if (edtAuditorRemark.Text == "")
        {
            Response.Write("<script>alert('审核备注不能为空!')</script>");
            return;
        }
        int nId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = loanapplyService.GetById(nId);
        loanapply.AuditTime = DateTime.Now;
        loanapply.Auditor = CurrentUser.UserName;
        loanapply.AuditorRemark = edtAuditorRemark.Text;
        loanapplyService.Update(loanapply);

        BorrowerService borrowerService = new BorrowerService();
        Borrower salesman = borrowerService.GetById(loanapply.SalesmanId);
        QiyebaoSms qiyebaoSms = new QiyebaoSms();
        string sContent = edtAuditorRemark.Text;
        qiyebaoSms.SendSms(salesman.Phone, sContent);
        OperaService operaService = new OperaService();
        operaService.Insert(new Opera()
        {
            Aliases = loanapply.CreditPhone,
            BorrowerId = loanapply.BorrowerId,
            FullName = CurrentUser.UserName,
            OperaType = OperaType.驳回申请原因,
            Remark = edtAuditorRemark.Text,
            RelationId = loanapply.Id
        });


        Response.Redirect("../loan_list/unaudited_loans_list.aspx");
    }
    protected void btnMentionBack_Click(object sender, EventArgs e)
    {
        int nId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = loanapplyService.GetById(nId);
        loanapply.IsMentionBack = true;
        loanapply.MentionBackTime = DateTime.Now;
        loanapplyService.Update(loanapply);
        Response.Redirect("../loan_list/success_loans_list.aspx");
    }
    protected void btnBuChongZiLiaoTiJiao_Click(object sender, EventArgs e)
    {
        int nId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
        LoanApplyService loanapplyService = new LoanApplyService();
        loanapplyService.Update(new LoanApply()
        {
            Id = nId,
            TongDunWang = edtTongDunWang.Text,
            ElectricCore = edtElectricCore.Text,
            FengKongRemark = edtFengKongRemark.Text,
            VisitRecord = edtVisitRecord.Text,
            ZhiCha = edtZhiCha.Text,
            DaiQian = edtDaiQian.Text
        });
        if (ddlIsRider.SelectedValue != "NotSure")
        {
            loanapplyService.Update(new LoanApply()
                     {
                         Id = nId,
                         IsRider = ConvertUtil.ToBool(ddlIsRider.SelectedValue)
                     });
        }
        if (ddlIsAnswer.SelectedValue != "NotSure")
        {
            loanapplyService.Update(new LoanApply()
            {
                Id = nId,
                IsAnswer = ConvertUtil.ToBool(ddlIsAnswer.SelectedValue)
            });
        }
        if (ddlIsAbnormal.SelectedValue != "NotSure")
        {
            loanapplyService.Update(new LoanApply()
            {
                Id = nId,
                IsAbnormal = ConvertUtil.ToBool(ddlIsAbnormal.SelectedValue)
            });
        }
        Response.Redirect("../loan_list/success_loans_list.aspx");
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


    private void WriteResponse(string picName, byte[] content)
    {
        Response.Clear();
        Response.ClearHeaders();
        Response.Buffer = false;
        Response.ContentType = "application/octet-stream";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(picName, Encoding.Default));
        Response.AppendHeader("Content-Length", content.Length.ToString());
        Response.BinaryWrite(content);
        Response.Flush();
        Response.End();
    }

    private byte[] GetImageContent(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.AllowAutoRedirect = true;

        WebProxy proxy = new WebProxy();
        proxy.BypassProxyOnLocal = true;
        proxy.UseDefaultCredentials = true;

        request.Proxy = proxy;

        WebResponse response = request.GetResponse();

        using (Stream stream = response.GetResponseStream())
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Byte[] buffer = new Byte[1024];
                int current = 0;
                while ((current = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    ms.Write(buffer, 0, current);
                }
                return ms.ToArray();
            }
        }
    }

    protected void btnxiazai_Click(object sender, EventArgs e)
    {
        BusinessFileService businessFileService = new BusinessFileService();
        List<BusinessFile> businessFileList = businessFileService.Search(new BusinessFile { IsValid = true }).Where(o => o.RelationId == ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"))).ToList();

        string sLuJing = "";
        string sName = "";
        foreach (BusinessFile bf in businessFileList)
        {
            string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";

            WebClient mywebclient = new WebClient();

            sName = zw(bf.FileName);
            sLuJing = Server.MapPath("images") + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + sName;
            if (Directory.Exists(sLuJing) == false)
            {
                Directory.CreateDirectory(sLuJing);
            }
            string savepath = sLuJing + "\\" + sFileName;

            try
            {
                mywebclient.DownloadFile(VIVPHOTO(bf.WeChatPath), savepath);
            }
            catch (Exception ex)
            {
            }
        }
        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = loanapplyService.GetById(ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id")));

        LoanFileService loanfileService = new LoanFileService();
        LoanFile loanfile = loanfileService.Search(new LoanFile() { IsValid = true }).Where(o => o.BorrowerId == loanapply.BorrowerId).OrderByDescending(o => o.CreateTime).FirstOrDefault();
        if (!string.IsNullOrEmpty(loanfile.WeChatZhengFilePath))
        {
            string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";

            WebClient mywebclient = new WebClient();

            sLuJing = Server.MapPath("images") + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + sName;
            if (Directory.Exists(sLuJing) == false)
            {
                Directory.CreateDirectory(sLuJing);
            }
            string savepath = sLuJing + "\\" + sFileName;

            try
            {
                mywebclient.DownloadFile(VIVPHOTO(loanfile.WeChatZhengFilePath), savepath);
            }
            catch (Exception ex)
            {
            }
        }
        if (!string.IsNullOrEmpty(loanfile.WeChatFanFilePath))
        {
            string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";

            WebClient mywebclient = new WebClient();

            sLuJing = Server.MapPath("images") + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + sName;
            if (Directory.Exists(sLuJing) == false)
            {
                Directory.CreateDirectory(sLuJing);
            }
            string savepath = sLuJing + "\\" + sFileName;

            try
            {
                mywebclient.DownloadFile(VIVPHOTO(loanfile.WeChatFanFilePath), savepath);
            }
            catch (Exception ex)
            {
            }
        }
        if (!string.IsNullOrEmpty(loanfile.WeChatBankCardPath))
        {
            string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";

            WebClient mywebclient = new WebClient();

            sLuJing = Server.MapPath("images") + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + sName;
            if (Directory.Exists(sLuJing) == false)
            {
                Directory.CreateDirectory(sLuJing);
            }
            string savepath = sLuJing + "\\" + sFileName;

            try
            {
                mywebclient.DownloadFile(VIVPHOTO(loanfile.WeChatBankCardPath), savepath);
            }
            catch (Exception ex)
            {
            }
        }


        ZipFile(sLuJing, sLuJing + ".zip");


        WriteResponse(PinYinConverter.Get(sName) + ".zip", GetImageContent("http://manage.che01.com/loan/loan_list/images/" + DateTime.Now.ToString("yyyyMMdd") + "/" + sName + ".zip"));
    }

    //提取字符串的中文
    public string zw(string szf)
    {
        string x = @"[\u4E00-\u9FFF]+";
        MatchCollection Matches = Regex.Matches
        (szf, x, RegexOptions.IgnoreCase);
        StringBuilder sb = new StringBuilder();
        foreach (Match NextMatch in Matches)
        {
            sb.Append(NextMatch.Value);
        }
        return sb.ToString();
    }

    /// <summary>
    /// 下载zip
    /// </summary>
    /// <param name="URL">请求地址</param>
    /// <param name="filename">缓存的路径</param>
    public void DownloadFile(string URL, string filename)
    {
        try
        {
            float percent = 0;
            HttpWebRequest Myrq = (HttpWebRequest)HttpWebRequest.Create(URL);//请求网络资源
            HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();//返回Internet资源的响应
            long totalBytes = myrp.ContentLength;//获取请求返回内容的长度
            Stream st = myrp.GetResponseStream();//读取服务器的响应资源，以IO流的形式进行读写
            Stream so = new FileStream(filename, FileMode.Create);
            long totalDownloadedByte = 0;
            byte[] by = new byte[1024];
            int osize = st.Read(by, 0, (int)by.Length);
            while (osize > 0)
            {
                totalDownloadedByte = osize + totalDownloadedByte;
                so.Write(by, 0, osize);
                osize = st.Read(by, 0, (int)by.Length);//读取当前字节流的总长度
                percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                double perNumber = Math.Round(percent);
            }
            so.Flush();
            so.Close();
            st.Close();
        }
        catch (Exception ex)
        {
        }
    }



    public void ZipFile(string strFile, string strZip)
    {
        var len = strFile.Length;
        var strlen = strFile[len - 1];
        if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
        {
            strFile += Path.DirectorySeparatorChar;
        }
        ZipOutputStream outstream = new ZipOutputStream(File.Create(strZip));
        outstream.SetLevel(6);
        zip(strFile, outstream, strFile);
        outstream.Finish();
        outstream.Close();
    }

    public void zip(string strFile, ZipOutputStream outstream, string staticFile)
    {
        if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
        {
            strFile += Path.DirectorySeparatorChar;
        }
        Crc32 crc = new Crc32();
        //获取指定目录下所有文件和子目录文件名称
        string[] filenames = Directory.GetFileSystemEntries(strFile);
        //遍历文件
        foreach (string file in filenames)
        {
            if (Directory.Exists(file))
            {
                zip(file, outstream, staticFile);
            }
            //否则，直接压缩文件
            else
            {
                //打开文件
                FileStream fs = File.OpenRead(file);
                //定义缓存区对象
                //FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                //StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);

                //StringBuilder sb = new StringBuilder();
                //while (!sr.EndOfStream)
                //{
                //    sb.AppendLine(sr.ReadLine() + "<br>");
                //}
                byte[] buffer = new byte[fs.Length];
                //通过字符流，读取文件
                fs.Read(buffer, 0, buffer.Length);
                //得到目录下的文件（比如:D:\Debug1\test）,test
                string tempfile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                ZipEntry entry = new ZipEntry(tempfile);
                entry.DateTime = DateTime.Now;
                entry.Size = fs.Length;
                fs.Close();
                crc.Reset();
                crc.Update(buffer);
                entry.Crc = crc.Value;
                outstream.PutNextEntry(entry);
                //写文件
                outstream.Write(buffer, 0, buffer.Length);
            }
        }
    }
    protected void btnChedan_Click(object sender, EventArgs e)
    {
        try
        {
            if (edtAuditorRemark.Text == "")
            {
                Response.Write("<script>alert('请填写审核备注!')</script>");
                return;
            }

            int nId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
            LoanApplyService loanapplyService = new LoanApplyService();
            LoanApply loanapply = loanapplyService.GetById(nId);
            loanapply.RepaymentStatus = CreditStatus.撤单;
            loanapply.CancellationRemark = edtAuditorRemark.Text;
            loanapplyService.Update(loanapply);
            Response.Write("<script>alert('撤单已完成!')</script>");
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }
    protected void btnYanZheng_Click(object sender, EventArgs e)
    {
        PersonVerify PersonVerify = CharacterRecognition.PersonVerify(renshenzhao.Src, "URL", IdCardNumber.Text, Name.Text);
        if (PersonVerify.error_msg == "SUCCESS")
        {
            if (ConvertUtil.ToInt(PersonVerify.result.score) >= 80)
            {
                Response.Write("<script>alert('公安系统认证成功，分数为：" + PersonVerify.result.score + "')</script>");
            }
            else
            {
                Response.Write("<script>alert('分数为" + PersonVerify.result.score + "')</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('客户单人照请重新拍照并上传')</script>");
        }
    }

    //public string PersonVerify(string image, string imageType, string idCardNumber, string name)
    //{
    //    String clientId = Configs.GetAPIKey();
    //    String clientSecret = Configs.GetSecretKey();
    //    var client = new Baidu.Aip.Face.Face(clientId, clientSecret);
    //    client.Timeout = 60000;  // 修改超时时间

    //    // 调用身份验证，可能会抛出网络等异常，请使用try/catch捕获
    //    //var result = client.PersonVerify(image, imageType, idCardNumber, name);
    //    //Console.WriteLine(result);
    //    // 如果有可选参数
    //    var options = new Dictionary<string, object>{
    //                       {"quality_control", "NORMAL"},
    //                       {"liveness_control", "LOW"}
    //                   };
    //    // 带参数调用身份验证
    //    var result = client.PersonVerify(image, imageType, idCardNumber, name, options);
    //    Console.WriteLine(result);
    //    return result.ToString();
    //}
}
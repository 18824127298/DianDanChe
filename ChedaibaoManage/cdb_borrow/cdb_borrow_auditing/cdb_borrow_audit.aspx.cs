using CheDaiBaoCommonService.Data;
using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using Sigbit.App.PTP.Notify.Sms.ChuangRui;
using Sigbit.App.PTP.Notify.Sms.Jytd;
using Sigbit.Common;
using Sigbit.Framework;
using Sigbit.Web.JavaScipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cdb_borrow_cdb_borrow_auditing_cdb_borrow_audit : SbtPageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        int nId = ConvertUtil.ToInt(Request["Id"]);
        if (nId != 0)
        {
            PageParameter.SetCustomParamObject("Id", nId);
        }
        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = new LoanApply();
        loanapply = loanapplyService.GetById(nId);
        lblName.Text = EncryptionService.AESDecrypt(loanapply.CreditName);
        lblPhone.Text = EncryptionService.AESDecrypt(loanapply.CreditPhone);
        lblDeadLine.Text = loanapply.Deadline.ToString();
        CarTypeService carTypeService = new CarTypeService();
        CarType carType = carTypeService.GetById(loanapply.CarTypeId);
        lblCarName.Text = carType.CarName;
        lblCarTitle.Text = carType.CarTitle;
        lblLiftFares.Text = carType.LiftFares.ToString();
        lblRetainage.Text = carType.Retainage.ToString();
        lblMonthPrice.Text = carType.MonthPrice.ToString();

        OperateModelService operateModelService = new OperateModelService();
        OperateModel operateModel = operateModelService.GetById(carType.OperateModelId);
        lblOperateModel.Text = operateModel.Name;

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string sAuditRemark = edtAuditRemark.Text.Trim();

        if (sAuditRemark.Contains("请在此填写审核") || sAuditRemark == "")
        {
            edtAuditRemark.Focus();
            PromptMessage("请填写审核意见");
            return;
        }

        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = new LoanApply();
        int nId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("Id"));
        loanapply = loanapplyService.GetById(nId);
        BorrowService borrowService = new BorrowService();
        Borrow borrow = new Borrow();
        using (var connection = SqlConnections.GetOpenConnection())
        {
            connection.Open();
            using (var sqltran = connection.BeginTransaction())
            {
                connection.Update(new LoanApply()
                  {
                      Id = nId,
                      IsAudit = 1,
                      AuditTime = DateTime.Now,
                      Auditor = CurrentUser.UserName,
                      AuditorRemark = sAuditRemark
                  }, sqltran);
                sqltran.Commit();
            }
            Response.Redirect("cdb_borrow_auditing.aspx");
        }
    }

    protected void btnUnAudit_Click(object sender, EventArgs e)
    {
        string sAuditRemark = edtAuditRemark.Text.Trim();

        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = new LoanApply();
        int nId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("Id"));
        loanapply = loanapplyService.GetById(nId);
        loanapplyService.Update(new LoanApply()
        {
            Id = nId,
            IsAudit = 0,
            AuditTime = DateTime.Now,
            Auditor = CurrentUser.UserName,
            AuditorRemark = sAuditRemark,
            RepaymentStatus = CreditStatus.流标
        });
        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(loanapply.BorrowerId);
        string sSmsContent = "尊敬的" + EncryptionService.AESDecrypt(borrower.FullName) + "，您在车贷宝的申请未通过审核。如有疑问，请致电400-837-2223，感谢您对车贷宝的支持，祝您生活愉快！";
        Response.Redirect("cdb_borrow_auditing.aspx");

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("cdb_borrow_auditing.aspx");
    }

    protected void PromptMessage(string sMessage)
    {
        JSMessageBox.Alert(sMessage);
    }
}
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

public partial class cdb_borrow_cdb_borrow_auditing_driver_application_audit : SbtPageBase
{

    DriverApplicationService driverApplicationService = new DriverApplicationService();
    DriverApplication driverApplication = new DriverApplication();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        int nId = ConvertUtil.ToInt(Request["Id"]);
        if (nId != 0)
        {
            PageParameter.SetCustomParamObject("Id", nId);
        }
        driverApplication = driverApplicationService.GetById(nId);
        lblName.Text = EncryptionService.AESDecrypt(driverApplication.FullName);
        lblPhone.Text = EncryptionService.AESDecrypt(driverApplication.Phone);
        lblCreateTIme.Text = driverApplication.CreateTime.Value.ToString("yyyy-MM-dd");

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

        int nId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("Id"));
        driverApplicationService.Update(new DriverApplication()
          {
              Id = nId,
              IsAudit = 1,
              AuditTime = DateTime.Now,
              Auditor = CurrentUser.UserName,
              AuditorRemark = sAuditRemark
          });
        Response.Redirect("driver_application_list.aspx");
    }


    protected void btnUnAudit_Click(object sender, EventArgs e)
    {
        string sAuditRemark = edtAuditRemark.Text.Trim();

        int nId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("Id"));
        driverApplication = driverApplicationService.GetById(nId);
        driverApplicationService.Update(new DriverApplication()
        {
            Id = nId,
            IsAudit = 0,
            AuditTime = DateTime.Now,
            Auditor = CurrentUser.UserName,
            AuditorRemark = sAuditRemark
        });
        string sSmsContent = "尊敬的" + EncryptionService.AESDecrypt(driverApplication.FullName) + "，您在车贷宝的司机申请未通过审核。如有疑问，请致电400-837-2223，感谢您对车贷宝的支持，祝您生活愉快！";
        Response.Redirect("driver_application_list.aspx");

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("driver_application_list.aspx");
    }

    protected void PromptMessage(string sMessage)
    {
        JSMessageBox.Alert(sMessage);
    }
}
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using Sigbit.Common;
using Sigbit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class loan_loan_discount_other_discount_audit : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        //========== 1. 初始化界面 ==========
        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;



        //========== 4. 取数据 ==========
        DiscountService discountService = new DiscountService();
        Discount discount = discountService.GetById(ConvertUtil.ToInt(sRecordPrimaryKey));

        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(discount.BorrowerId);


        //========== 5. 更新各控件的显示 ==========
        lblFullname.Text = borrower.FullName;
        lblAmount.Text = discount.Amount.ToString();
        lblCreator.Text = discount.Creator;
        lblCreateTime.Text = discount.CreateTime.Value.ToString("yyyy年MM月dd日");
        lblRemark.Text = discount.Remark;
    }
    protected void btnAudit_Click(object sender, EventArgs e)
    {
        SbtMessageBoxPage msgPage = new SbtMessageBoxPage(this);
        int nRecordPrimaryKey = ConvertUtil.ToInt(ViewState["rec_key"]);

        string sAuditRemark = edtAuditRemark.Text.Trim();
        if (sAuditRemark == "")
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "备注不能为空！";
            edtAuditRemark.Focus();
            return;
        }


        DiscountService discountService = new DiscountService();
        Discount discount = discountService.GetById(nRecordPrimaryKey);

        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(discount.BorrowerId);

        string sRecharegAlias = borrower.Phone;


        if (discount.IsAudit != null)
        {
            msgPage.MessageText = "该记录已经审核操作过了，无需再进行审核操作";
            msgPage.Show();
            return;
        }


        discount.Auditor = CurrentUser.UserName;
        discount.AuditRemark = sAuditRemark;
        discount.IsAudit = true;
        discount.AuditTime = DateTime.Now;

        discount.SecondAuditor = "guohua";
        discount.SecondAuditResult = true;
        discount.SecondAuditTime = DateTime.Now.AddMinutes(3);
        discountService.Update(discount);

        msgPage.MessageText = "审核操作成功";
        msgPage.ReturnUrl = this.Request.Url.AbsoluteUri;

        msgPage.Show();
    }
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("other_discount_audit.aspx");
    }
    protected void btnUnAudit_Click(object sender, EventArgs e)
    {
        SbtMessageBoxPage msgPage = new SbtMessageBoxPage(this);
        int nRecordPrimaryKey = ConvertUtil.ToInt(ViewState["rec_key"]);

        string sAuditRemark = edtAuditRemark.Text.Trim();
        if (sAuditRemark == "")
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "备注不能为空！";
            edtAuditRemark.Focus();
            return;
        }


        DiscountService discountService = new DiscountService();
        Discount discount = discountService.GetById(nRecordPrimaryKey);

        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(discount.BorrowerId);

        string sRecharegAlias = borrower.Phone;


        if (discount.IsAudit != null)
        {
            msgPage.MessageText = "该记录已经审核操作过了，无需再进行审核操作";
            msgPage.Show();
            return;
        }


        discount.Auditor = CurrentUser.UserName;
        discount.AuditRemark = sAuditRemark;
        discount.IsAudit = false;
        discount.AuditTime = DateTime.Now;

        discount.SecondAuditor = "guohua";
        discount.SecondAuditResult = false;
        discount.SecondAuditTime = DateTime.Now.AddMinutes(3);

        discountService.Update(discount);

        msgPage.MessageText = "审核操作成功";
        msgPage.ReturnUrl = this.Request.Url.AbsoluteUri;

        msgPage.Show();
    }
}
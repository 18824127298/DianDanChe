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

public partial class loan_loan_discount_discount_insert : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int nId = ConvertUtil.ToInt(Request["id"]);

        if (nId != 0)
        {
            PageParameter.SetCustomParamObject("id", nId);
        }
        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = loanapplyService.GetById(nId);
        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(loanapply.BorrowerId);
        Borrower Salesman = borrowerService.GetById(loanapply.SalesmanId);
        lblPhone.Text = borrower.Phone;
        lblName.Text = borrower.FullName;
        lblAmount.Text = loanapply.CreditAmount.Value.ToString("N2");
        lblSalesman.Text = Salesman.FullName;
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (edtAmount.Text == "")
        {
            Response.Write("<script>alert('请填写减免的金额!')</script>");
            return;
        }
        if (edtRemark.Text == "")
        {
            Response.Write("<script>alert('请填写减免的原因!')</script>");
            return;
        }
        int nId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = loanapplyService.GetById(nId);
        DiscountService discountService = new DiscountService();
        if (loanapply != null)
        {
            discountService.Insert(new Discount()
            {
                Amount = Convert.ToDecimal(edtAmount.Text),
                BorrowerId = loanapply.BorrowerId,
                LeftAmount = Convert.ToDecimal(edtAmount.Text),
                LoanApplyId = loanapply.Id,
                Creator = CurrentUser.UserName,
                IsAudit = true,
                Remark = edtRemark.Text
            });
            SbtMessageBoxPage msgPage = new SbtMessageBoxPage(this);
            msgPage.MessageText = "减免提交成功，请耐心等待后台审核";
            msgPage.ReturnUrl = this.Request.Url.AbsoluteUri;

            msgPage.Show();
        }
    }
}
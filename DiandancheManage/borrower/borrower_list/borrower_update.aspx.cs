using CheDaiBaoWeChatModel;
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

public partial class borrower_borrower_list_borrower_update : SbtPageBase
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


        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(nId);
        lblFullName.Text = borrower.FullName;
        lblPhone.Text = borrower.Aliases;
        edtFullName.Text = borrower.FullName;
        edtPhone.Text = borrower.Aliases;
        lblddlIsSalesman.Text = borrower.IsSalesman == true ? "是" : "否";
        ddlIsSalesman.SelectedValue = borrower.IsSalesman == true ? "true" : "false";
        lblWeiXinId.Text = borrower.WeiXinId;
        edtWeiXinId.Text = borrower.WeiXinId;
        lblLoginKey.Text = borrower.LoginKey;
        edtLoginKey.Text = borrower.LoginKey;
        lblIsMerchant.Text = borrower.IsMerchant == true ? "是" : "否";
        ddlIsMerchant.SelectedValue = borrower.IsMerchant == true ? "true" : "false";
        lblCompany.Text = borrower.Company == null ? "" : borrower.Company.ToString();
        ddlCompany.SelectedValue = borrower.Company == null ? "0" : ((int)borrower.Company).ToString();
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        int nId = ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id"));
        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(nId);
        borrower.Aliases = edtPhone.Text;
        borrower.Phone = edtPhone.Text;
        borrower.FullName = edtFullName.Text;
        borrower.IsSalesman = ConvertUtil.ToBool(ddlIsSalesman.SelectedValue);
        borrower.IsMerchant = ConvertUtil.ToBool(ddlIsMerchant.SelectedValue);
        borrower.WeiXinId = edtWeiXinId.Text;
        borrower.LoginKey = edtLoginKey.Text;
        borrower.Company = (Company)ConvertUtil.ToInt(ddlCompany.SelectedValue);
        borrowerService.Update(borrower);

        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = loanapplyService.Search(new LoanApply() { IsValid = true }).Where(o=>o.CreditPhone == lblPhone.Text && o.BorrowerId == borrower.Id).FirstOrDefault();
        if (loanapply != null)
        {
            loanapply.CreditPhone = edtPhone.Text;
            loanapplyService.Update(loanapply);
        }


        OperaService operaService = new OperaService();
        operaService.Insert(new Opera()
        {
            BorrowerId = borrower.Id,
            FullName = CurrentUser.RealName,
            OperaType = OperaType.后台修改用户信息,
            RelationId = ConvertUtil.ToInt(CurrentUser.UserUid),
            Remark = "修改用户信息",
        });

        Response.Redirect("../borrower_list/borrower_list.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../borrower_list/borrower_list.aspx");
    }
}
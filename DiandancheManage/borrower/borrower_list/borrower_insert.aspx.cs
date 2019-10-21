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

public partial class borrower_borrower_list_borrower_insert : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = new Borrower();
        borrower.Guid = Guid.NewGuid().ToString();
        borrower.Aliases = edtPhone.Text;
        borrower.Phone = edtPhone.Text;
        borrower.FullName = edtFullName.Text;
        borrower.LoginKey = edtLoginKey.Text;
        borrower.IsSalesman = ConvertUtil.ToBool(ddlIsSalesman.SelectedValue);
        int Id = borrowerService.Insert(borrower);

        OperaService operaService = new OperaService();
        operaService.Insert(new Opera()
        {
            BorrowerId = Id,
            FullName = CurrentUser.RealName,
            OperaType = OperaType.后台创建会员,
            RelationId = ConvertUtil.ToInt(CurrentUser.UserUid),
            Remark = "新增后台会员",
        });

        Response.Redirect("../borrower_list/borrower_list.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../borrower_list/borrower_list.aspx");
    }
}
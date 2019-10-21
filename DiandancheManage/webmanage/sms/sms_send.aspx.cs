using CheDaiBaoWeChatController.Interface;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class webmanage_sms_sms_send : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetAll().Find(o => o.Phone == edtPhone.Text);
        if (borrower != null)
        {
            QiyebaoSms qiyebaoSms = new QiyebaoSms();
            qiyebaoSms.SendSms(edtPhone.Text, edtContent.Text);
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "已发送";
        }
        else
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "找不到该客户手机号";
        }
    }
}
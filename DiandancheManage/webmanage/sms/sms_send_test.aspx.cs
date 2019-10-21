using CheDaiBaoWeChatController.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class webmanage_sms_sms_send_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        QiyebaoSms qiyebaoSms = new QiyebaoSms();
        qiyebaoSms.SendSms(edtPhone.Text, edtContent.Text);
    }
}
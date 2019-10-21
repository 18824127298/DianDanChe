using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Sigbit.Framework;
using Sigbit.Framework.Security;
using Sigbit.Web.JavaScipt;

public partial class framework_ctrlpanel_change_passwd : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //============ 1. 得到当前的用户及密码 =============
        TbUser tblUser = new TbUser();
        tblUser.UserUid = CurrentUser.UserUid;
        tblUser.Fetch();

        //========= 2. 得到输入的新旧密码 ===========
        string sInputOldPassword = edtOldPassword.Text.Trim();
        string sInputNewPassword = edtNewPassword.Text.Trim();
        string sInputNewPasswordConfirm = edtNewPasswordOK.Text.Trim();

        //======== 3. 判断输入的原密码是否正确 ============
        if (sInputOldPassword != tblUser.Password)
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "您输入的原密码不正确，请重新输入！";
            edtOldPassword.Text = "";
            edtOldPassword.Focus();
            return;
        }

        //=========== 4. 判断输入的两个新密码是否一致 ===========
        if (sInputNewPassword != sInputNewPasswordConfirm)
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "您两次输入的新密码不一致，请重新输入！";
            edtNewPassword.Text = "";
            edtNewPasswordOK.Text = "";
            edtNewPassword.Focus();
            return;
        }

        //=========== 5. 密码强度的判断 ==============
        int nNewPasswordStrength = NPSPasswordUtil.PasswordStrengthOfPassword(sInputNewPassword);
        if (nNewPasswordStrength < SbtSecurityConfig.Password.PasswordStrength)
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "密码强度未达到安全设置要求，请重新输入！";
            edtNewPassword.Text = "";
            edtNewPasswordOK.Text = "";
            edtNewPassword.Focus();
            return;
        }

        //============== 5. 进行修改密码数据操作 ==============
        if (tblUser.Password != sInputNewPassword)
        {
            tblUser.Password = sInputNewPassword;
            tblUser.Update();

            lblErrMessage.Visible = false;
        }

        SbtMessageBoxPage page = new SbtMessageBoxPage(this);
        page.ReturnUrl = "../main/mainframe.aspx";
        page.MessageText = "密码已成功修改，您的密码已由“"
                + sInputOldPassword + "”成功修改为“" + sInputNewPassword + "”"
                +"。您以后须用新的密码登陆系统，谢谢。";
        page.Show();
    }
}

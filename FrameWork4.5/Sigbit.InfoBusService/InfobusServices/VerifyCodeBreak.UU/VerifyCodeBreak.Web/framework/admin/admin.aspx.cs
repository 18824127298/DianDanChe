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

using Sigbit.Common;
using Sigbit.Framework;
using Sigbit.Framework.Security;

public partial class framework_admin_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        FormsAuthentication.SignOut();
        Session.Abandon();
    }

    protected void imgbtnAdmin_Click(object sender, ImageClickEventArgs e)
    {
        //======== 1. 得到用户名和密码 ==========
        string sUserName = edtUserName.Text.Trim();
        string sPassword = edtPassword.Text.Trim();

        //======== 2. 判断用户名和密码是否有效 =========
        string sLoginErrorMsg = "";
        bool bCanLogin = SbtUser.CheckUserLogin(sUserName, sPassword, out sLoginErrorMsg);

        if (bCanLogin)
        {
            SbtUser user = new SbtUser();
            user.FetchByUserName(sUserName);

            SigbitPrincipal principal = new SigbitPrincipal();
            principal.LoadUser(user);
            Context.User = principal;
            FormsAuthentication.RedirectFromLoginPage(principal.Identity.Name, true);

            Session["currentUserMEFTMG"] = user;

            //========= 2+. 增加密码强度的判断 ===========
            if (SbtSecurityConfig.Password.ModiPasswordIfLowerStrength == Bool3State.True)
            {
                if (NPSPasswordUtil.PasswordStrengthOfPassword(sPassword) < SbtSecurityConfig.Password.PasswordStrength)
                {
                    Response.Redirect("../security_manage/password_change_lower_strength.aspx", true);
                    return;
                }
            }

            Response.Redirect("../main/mainframe.aspx", true);
        }
        else
        {
            //Response.Write("<script>alert('密码不正确,密码区分大小写请正确填写!');</script>");
            Response.Write("<script>alert('" + sLoginErrorMsg + "');</script>");
        }
    }

    protected void imgbtnReset_Click(object sender, ImageClickEventArgs e)
    {
        edtUserName.Text = "";
        edtUserName.Focus();
        edtPassword.Text = "";
    }
}

﻿using System;
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
using Sigbit.Framework.License;
using Sigbit.Framework.Security;
using Sigbit.Framework.Patch.ScheduleEngine;
using Sigbit.Web.JavaScipt;


public partial class farmwork_admin_Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        string sSubSystem = ConvertUtil.ToString(Request["sub_sys"]);

        lblSystemName.Text = SbtLicIDFeaturesConfig.Instance.SystemFullName;
        
        FormsAuthentication.SignOut();
        if (sSubSystem != "")
        {
            SbtAppContext.CurrentSubSystem = sSubSystem;

            if (Session["currentUserMEFTMG"] != null)
                Response.Redirect("../main/mainframe.aspx", true);
        }

        
    }

    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
    {
        //======== 1. 得到用户名和密码 ==========
        string sUserName = edtUserName.Text.Trim();
        string sPassword = edtPassword.Text.Trim();

        //======== 2. 判断用户名和密码是否有效 =========
        bool bCanLogin = SbtUser.CheckUserLogin(sUserName, sPassword);

        if (bCanLogin)
        {
            SbtPageBase.ForceThemeUsing = "blue-comfort";

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
            lblLoginErrorMsg.Text = "登陆失败,可能出现以下问题:<br/>\n"
                    + "1.你还没有注册<br/>\n"
                    + "2.你的密码不正确,密码区分大小写请正确填写";
            lblLoginErrorMsg.Visible = true;
        }
    }
    protected void btnRefill_Click(object sender, ImageClickEventArgs e)
    {
        //lblLoginErrorMsg.Visible = false;
        //lblLoginErrorMsg.Text = "";
        //edtUserName.Text = "";
        //edtUserName.Focus();
        //edtPassword.Text = "";
        Response.Redirect("homepage.aspx");
    }
}

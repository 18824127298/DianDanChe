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
using Sigbit.Data;
using Sigbit.Framework;
using Sigbit.Web.WebControlUtil;

public partial class genui_DITJ_login_log_browse_modify : SbtPageBase
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
        TbLogUserLogin tbl = new TbLogUserLogin();
        tbl.LoginLogUid = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. 更新各控件的显示 ==========
        lblLoginTime.Text = tbl.LoginTime;
        lblLogoutTime.Text = tbl.LogoutTime;
        lblInSystemDuration.Text = DateTimeUtil.ToTimeStrFromSecond(tbl.InSystemDuration);
        lblUserName.Text = tbl.UserName + " (" + tbl.LoginIpAddress + ")";

        if (tbl.IsLoginSuccess == "Y")
            lblLoginResult.Text = "登录成功";
        else
        {
            lblLoginResult.Text = tbl.LoginFailDesc;
            trLogoutDesc.Visible = false;
        }

        if (tbl.HasLogout == "N")
            lblLogoutDesc.Text = "未退出";
        else
        {
            if (tbl.LogoutCause == TbLogUserLoginFLogoutCause.Exit)
                lblLogoutDesc.Text = "用户退出";
            else if (tbl.LogoutCause == TbLogUserLoginFLogoutCause.Logout)
                lblLogoutDesc.Text = "用户注销";
            else if (tbl.LogoutCause == TbLogUserLoginFLogoutCause.WinClose)
                lblLogoutDesc.Text = "关闭浏览器窗口";
            else
                lblLogoutDesc.Text = "应用异常";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("login_log_browse_list.aspx");
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbLogUserLogin tbl = new TbLogUserLogin();
            tbl.LoginLogUid = sSelectedID;
            tbl.Delete();
        }
    }
}

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
        //========== 1. ��ʼ������ ==========
        //========== 2. ȡ���������� ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 4. ȡ���� ==========
        TbLogUserLogin tbl = new TbLogUserLogin();
        tbl.LoginLogUid = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. ���¸��ؼ�����ʾ ==========
        lblLoginTime.Text = tbl.LoginTime;
        lblLogoutTime.Text = tbl.LogoutTime;
        lblInSystemDuration.Text = DateTimeUtil.ToTimeStrFromSecond(tbl.InSystemDuration);
        lblUserName.Text = tbl.UserName + " (" + tbl.LoginIpAddress + ")";

        if (tbl.IsLoginSuccess == "Y")
            lblLoginResult.Text = "��¼�ɹ�";
        else
        {
            lblLoginResult.Text = tbl.LoginFailDesc;
            trLogoutDesc.Visible = false;
        }

        if (tbl.HasLogout == "N")
            lblLogoutDesc.Text = "δ�˳�";
        else
        {
            if (tbl.LogoutCause == TbLogUserLoginFLogoutCause.Exit)
                lblLogoutDesc.Text = "�û��˳�";
            else if (tbl.LogoutCause == TbLogUserLoginFLogoutCause.Logout)
                lblLogoutDesc.Text = "�û�ע��";
            else if (tbl.LogoutCause == TbLogUserLoginFLogoutCause.WinClose)
                lblLogoutDesc.Text = "�ر����������";
            else
                lblLogoutDesc.Text = "Ӧ���쳣";
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

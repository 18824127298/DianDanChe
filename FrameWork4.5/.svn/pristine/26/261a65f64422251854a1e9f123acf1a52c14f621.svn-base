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

public partial class framework_organize_role_manage_modify : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 0. ɾ����¼ ==========
        string sDelRecords = ConvertUtil.ToString(Request["del_rec"]);
        if (sDelRecords != "")
        {
            DeleteData(sDelRecords);
            Response.Redirect("role_manage_list.aspx");
            return;
        }

        //========== 1. ��ʼ������ ==========
        //========== 2. ȡ���������� ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. �������жϺʹ��� ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "�����û���ɫ";
            btnOK.Text = "����";
            return;
        }

        //========== 4. ȡ���� ==========
        TbRole tbl = new TbRole();
        tbl.RoleUid = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. ���¸��ؼ�����ʾ ==========
        edtRoleName.Text = tbl.RoleName;
        edtRemarks.Text = tbl.Remarks;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string sRoleUid = ViewState["rec_key"].ToString();
        bool bAppendMode = false;
        if (ConvertUtil.ToString(ViewState["rec_key"]) == "")
            bAppendMode = true;

        //========== 1. ��֤���ݵ���Ч�� ==========

        //========== 1.1 �жϽ�ɫ�����Ƿ�Ϊ�� ==========
        string sRoleName = edtRoleName.Text.Trim();
        if (sRoleName == "")
        {
            lblErrMessage.Text = "������д��ɫ����";
            lblErrMessage.Visible = true;
            edtRoleName.Focus();
            return;
        }

        //========== 2. ������������ ==========
        TbRole tbl = new TbRole();
        if (bAppendMode)
        {
            tbl.RoleUid = Guid.NewGuid().ToString();
            tbl.RoleName = edtRoleName.Text.Trim();
            tbl.Creator = CurrentUser.UserName;
            tbl.CreateTime = DateTimeUtil.Now;
            tbl.Remarks = edtRemarks.Text.Trim();

            tbl.Insert();
        }

        //========== 3. ���ݸ��´��� ==========
        else
        {
            tbl.RoleUid = sRoleUid;
            tbl.Fetch();

            tbl.RoleName = edtRoleName.Text.Trim();
            tbl.Remarks = edtRemarks.Text.Trim();

            tbl.Update();
        }

        //========== 4. ���ص���ҳ�� ==========
        Response.Redirect("role_manage_list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("role_manage_list.aspx");
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbRole tbl = new TbRole();
            tbl.RoleUid = sSelectedID;
            tbl.Delete();
        }
    }
}

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
using Sigbit.Framework.Security;
using Sigbit.Web.WebControlUtil;
using Sigbit.Framework.Role;

public partial class genui_CRPN_user_modify : SbtPageBase
{
    private string GetCurrentDeptId()
    {
        if (PageParameter.StringParam[22] == "user_dept_tree_current_dept_EBNP")
            return PageParameter.StringParam[23];
        else
            return "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========= 0+. �����û���¼ =============
        string sUnlockUserUid = ConvertUtil.ToString(Request["unlock_rec"]);
        if (sUnlockUserUid != "")
        {
            UnlockUser(sUnlockUserUid);
            Response.Redirect("user_list.aspx?dpt_uid=" + GetCurrentDeptId());
            return;
        }

        //========== 0. ɾ����¼ ==========
        string sDelRecords = ConvertUtil.ToString(Request["del_rec"]);
        if (sDelRecords != "")
        {
            DeleteData(sDelRecords);
            Response.Redirect("user_list.aspx?dpt_uid=" + GetCurrentDeptId());
            return;
        }

        //========== 1. ��ʼ������ ==========
        WCUComboBox.InitComboBox(ddlbRole, SbtCodeTables.Role);
        ddlbRole.Items.Add(new ListItem("[δָ����ɫ]", ""));
        ddlbRole.SelectedValue = "";

        WCUComboBox.InitComboBox(ddlbDeptId, SbtCodeTables.DeptTree);
        ddlbDeptId.SelectedValue = GetCurrentDeptId();

        //========== 2. ȡ���������� ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. �������жϺʹ��� ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "�����û�";
            btnOK.Text = "����";
            edtPassword.Text = "";
            ddlbDeptId.Enabled = false;
            return;
        }

        //========== 4. ȡ���� ==========
        TbUser tbl = new TbUser();
        tbl.UserUid = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. ���¸��ؼ�����ʾ ==========
        edtUserName.Text = tbl.UserName;
        edtRealName.Text = tbl.RealName;
        ddlbSex.SelectedValue = tbl.Sex;
        edtTelephone.Text = tbl.Telephone;
        ddlbIsActive.SelectedValue = tbl.IsActive;
        if (tbl.IsAdmin == "Y")
            cbxIsAdmin.Checked = true;
        else
            cbxIsAdmin.Checked = false;
        edtRemarks.Text = tbl.Remarks;

        string sRoleUid = SbtRoleUserUtil.GetRoleUidOfUser(sRecordPrimaryKey);
        ddlbRole.SelectedValue = sRoleUid;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool bAppendMode = false;
        string sParamRecKey = ConvertUtil.ToString(ViewState["rec_key"]);
        if (sParamRecKey == "")
            bAppendMode = true;

        //========== 1. ��֤���ݵ���Ч�� ==========
        //========== 1.1 �ж��ʺ��Ƿ�Ϊ�� ==========
        string sUserName = edtUserName.Text.Trim();
        if (sUserName == "")
        {
            lblErrMessage.Text = "������д�ʺ�";
            lblErrMessage.Visible = true;
            edtUserName.Focus();
            return;
        }

        //========== 1.2 �ж������Ƿ�Ϊ�� ==========
        string sRealName = edtRealName.Text.Trim();
        if (sRealName == "")
        {
            lblErrMessage.Text = "������д����";
            lblErrMessage.Visible = true;
            edtRealName.Focus();
            return;
        }

        //=========== 1.3 �ʺŲ����ظ� ================
        bool bIsDupAccount = false;
        TbUser tblUserAccountDupCheck = new TbUser();
        if (tblUserAccountDupCheck.FetchByUserName(sUserName, true))
        {
            if (bAppendMode)
                bIsDupAccount = true;
            else
            {
                if (tblUserAccountDupCheck.UserUid != sParamRecKey)
                    bIsDupAccount = true;
            }
        }
        if (bIsDupAccount)
        {
            lblErrMessage.Text = "������ʺź����е��ʺ��ظ�����������ָ��";
            lblErrMessage.Visible = true;
            edtUserName.Focus();
            return;
        }

        //============ 1.4 ���루ǿ�ȡ���ȫ�ԣ� ===============
        //===== (1) �ж��Ƿ����������� ==============
        bool bInputNewPassword = false;
        if (bAppendMode)
            bInputNewPassword = true;
        else
        {
            if (edtPassword.Text.Trim() != "***** ***** ***** *****")
                bInputNewPassword = true;
        }

        if (bInputNewPassword)
        {
            //========= (2) �õ������������ǿ�� ==========
            int nPasswordStrength = NPSPasswordUtil.PasswordStrengthOfPassword(edtPassword.Text.Trim());

            //========== (3) ���ǿ�ȴﲻ��Ҫ������ʾ ============
            if (nPasswordStrength < SbtSecurityConfig.Password.PasswordStrength)
            {
                lblErrMessage.Visible = true;
                lblErrMessage.Text = "����ǿ��δ�ﵽ��ȫ����Ҫ�����������룡";
                edtPassword.Focus();
                return;
            }
        }

        //========== 2. ������������ ==========
        TbUser tbl = new TbUser();
        if (bAppendMode)
        {
            tbl.UserUid = Guid.NewGuid().ToString();
            tbl.UserName = edtUserName.Text.Trim();
            tbl.RealName = edtRealName.Text.Trim();
            tbl.Password = edtPassword.Text.Trim();
            tbl.Sex = ddlbSex.SelectedValue;
            tbl.Telephone = edtTelephone.Text.Trim();
            tbl.CreateTime = DateTimeUtil.Now;
            tbl.IsActive = ddlbIsActive.SelectedValue;
            if (cbxIsAdmin.Checked)
                tbl.IsAdmin = "Y";
            else
                tbl.IsAdmin = "N";
            tbl.Remarks = edtRemarks.Text.Trim();

            tbl.Creator = CurrentUser.UserName;
            tbl.DeptId = GetCurrentDeptId();

            tbl.Insert();
        }

        //========== 3. ���ݸ��´��� ==========
        else
        {
            tbl.UserUid = sParamRecKey;
            tbl.Fetch();

            tbl.UserName = edtUserName.Text.Trim();
            tbl.RealName = edtRealName.Text.Trim();
            if (edtPassword.Text.Trim() != "***** ***** ***** *****")
                tbl.Password = edtPassword.Text.Trim();
            tbl.Sex = ddlbSex.SelectedValue;
            tbl.Telephone = edtTelephone.Text.Trim();
            tbl.IsActive = ddlbIsActive.SelectedValue;
            if (cbxIsAdmin.Checked)
                tbl.IsAdmin = "Y";
            else
                tbl.IsAdmin = "N";
            tbl.Remarks = edtRemarks.Text.Trim();
            tbl.DeptId = ddlbDeptId.SelectedValue;

            tbl.Update();
        }

        string sRoleUid = ddlbRole.SelectedValue;
        SbtRoleUserUtil.SetRoleUidToUser(tbl.UserUid, sRoleUid);

        //========== 4. ���ص���ҳ�� ==========
        Response.Redirect("user_list.aspx?dpt_uid=" + GetCurrentDeptId());
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("user_list.aspx?dpt_uid=" + GetCurrentDeptId());
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbUser tbl = new TbUser();
            tbl.UserUid = sSelectedID;
            tbl.Delete();
        }
    }

    private void UnlockUser(string sUserUid)
    {
        TbUserSecurity tblSecurity = new TbUserSecurity();
        tblSecurity.UserUid = sUserUid;

        if (tblSecurity.Fetch(true))
        {
            tblSecurity.ModifyTime = DateTimeUtil.Now;
            tblSecurity.LockStatus = "";
            tblSecurity.LockStatusTime = DateTimeUtil.Now;
            tblSecurity.Update();
        }
    }
}

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
using Sigbit.Framework.Role;
using Sigbit.App.PTP.DBDefine.WangDai;

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

        pnlAdminArea.Visible = CurrentUser.IsAdmin;


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
        ddlbRole.Items.Insert(0, new ListItem("[δָ����ɫ]", ""));
        ddlbRole.SelectedValue = "";

        //========== 2. ȡ���������� ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. �������жϺʹ��� ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "�����û�";
            btnOK.Text = "����";
            edtPassword.Text = "";
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
        edtMobilePhone.Text = tbl.Mobilephone;
        edtEmail.Text = tbl.Email;

        ddlbIsActive.SelectedValue = tbl.IsActive;
        if (tbl.IsAdmin == "Y")
            cbxIsAdmin.Checked = true;
        else
            cbxIsAdmin.Checked = false;
        edtRemarks.Text = tbl.Remarks;

        string sRoleUid = SbtRoleUserUtil.GetRoleUidOfUser(sRecordPrimaryKey);
        ddlbRole.SelectedValue = sRoleUid;

        edtAreaName.Text = tbl.ExInfo03;
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

        //=========== 1.4 ����ϵͳ�ʺŲ����ظ� ================
        if (tblUserAccountDupCheck.ThirdPartyCode != "")
        {
            TbJudge tblWdUserAccountDupCheckGetById = new TbJudge();
            tblWdUserAccountDupCheckGetById.Id = Convert.ToInt32(tblUserAccountDupCheck.ThirdPartyCode);
            bool bIsHasAccount = tblWdUserAccountDupCheckGetById.Fetch(true);

            if (bIsHasAccount)
            {
                bool bWdIsDupAccount = false;
                TbJudge tblWdUserAccountDupCheck = new TbJudge();
                tblWdUserAccountDupCheck.Aliases = sUserName;
                if (tblWdUserAccountDupCheck.FetchByAliases())
                {
                    if (bAppendMode)
                        bWdIsDupAccount = true;
                    else
                    {
                        if (tblWdUserAccountDupCheck.Id != tblWdUserAccountDupCheckGetById.Id)
                            bWdIsDupAccount = true;
                    }
                }
                if (bWdIsDupAccount)
                {
                    lblErrMessage.Text = "������ʺź����е��ʺ��ظ�����������ָ��.(�����û���)";
                    lblErrMessage.Visible = true;
                    edtUserName.Focus();
                    return;
                }
            }
        }

        //========== 2. ������������ ==========
        TbUser tbl = new TbUser();
        TbJudge tblTbJudge = new TbJudge();

        if (bAppendMode)
        {
            // �����û�������Ӵ���
            tblTbJudge.UpdateTime = tblTbJudge.CreateTime = DateTimeUtil.Now;
            if (ddlbIsActive.SelectedValue.ToLower() == "y")
                tblTbJudge.IsValid = 1;
            else
                tblTbJudge.IsValid = 0;
            tblTbJudge.Aliases = edtUserName.Text.Trim();
            tblTbJudge.FullName = edtRealName.Text.Trim();
            tblTbJudge.Salt = Guid.NewGuid().ToString();
            tblTbJudge.LoginKey = edtPassword.Text.Trim();
            tblTbJudge.LoginKey = CreatePasswordHash(tblTbJudge.LoginKey, tblTbJudge.Salt);

            tblTbJudge.Insert();

            bool bIsAddSuccess = tblTbJudge.FetchByAliases();
            if (!bIsAddSuccess)
            {
                throw new Exception("�����û����֮���޷����»�ȡ���û������ݣ�");
            }


            tbl.ThirdPartyCode = tblTbJudge.Id.ToString();

            // ��˾�Ŀ�ܵĺ�̨�û���Ӵ���
            tbl.UserUid = Guid.NewGuid().ToString();
            tbl.UserName = edtUserName.Text.Trim();
            tbl.RealName = edtRealName.Text.Trim();
            tbl.Password = edtPassword.Text.Trim();
            tbl.Sex = ddlbSex.SelectedValue;
            tbl.Telephone = edtTelephone.Text.Trim();
            tbl.Mobilephone = edtMobilePhone.Text.Trim();
            tbl.Email = edtEmail.Text.Trim();
            tbl.CreateTime = DateTimeUtil.Now;
            tbl.IsActive = ddlbIsActive.SelectedValue;
            if (cbxIsAdmin.Checked)
                tbl.IsAdmin = "Y";
            else
                tbl.IsAdmin = "N";
            tbl.Remarks = edtRemarks.Text.Trim();

            tbl.Creator = CurrentUser.UserName;
            tbl.DeptId = GetCurrentDeptId();

            tbl.ExInfo03 = edtAreaName.Text.Trim();

            tbl.Insert();
        }

        //========== 3. ���ݸ��´��� ==========
        else
        {




            // ��˾�Ŀ�ܵĺ�̨�û���Ӵ���

            tbl.UserUid = sParamRecKey;
            tbl.Fetch();

            tbl.UserName = edtUserName.Text.Trim();
            tbl.RealName = edtRealName.Text.Trim();
            if (edtPassword.Text.Trim() != "***** ***** ***** *****")
                tbl.Password = edtPassword.Text.Trim();
            tbl.Sex = ddlbSex.SelectedValue;
            tbl.Telephone = edtTelephone.Text.Trim();
            tbl.Mobilephone = edtMobilePhone.Text.Trim();
            tbl.Email = edtEmail.Text.Trim();
            tbl.IsActive = ddlbIsActive.SelectedValue;
            if (cbxIsAdmin.Checked)
                tbl.IsAdmin = "Y";
            else
                tbl.IsAdmin = "N";
            tbl.Remarks = edtRemarks.Text.Trim();

            tbl.ExInfo03 = edtAreaName.Text.Trim();

            tbl.Update();

            // �����û�������Ӵ���
            if (tbl.ThirdPartyCode != "")
            {
                tblTbJudge.Id = Convert.ToInt32(tbl.ThirdPartyCode);
                tblTbJudge.Fetch();
                tblTbJudge.Aliases = edtUserName.Text.Trim();

                tblTbJudge.UpdateTime = DateTimeUtil.Now;
                if (ddlbIsActive.SelectedValue.ToLower() == "y")
                    tblTbJudge.IsValid = 1;
                else
                    tblTbJudge.IsValid = 0;

                tblTbJudge.FullName = edtRealName.Text.Trim();
                if (edtPassword.Text.Trim() != "***** ***** ***** *****")
                {
                    tblTbJudge.Salt = Guid.NewGuid().ToString();
                    tblTbJudge.LoginKey = edtPassword.Text.Trim();
                    tblTbJudge.LoginKey = CreatePasswordHash(tblTbJudge.LoginKey, tblTbJudge.Salt);
                    
                }
                tblTbJudge.Update();
            }
            else // ������µ��Ƿ��ܱ����û�е����� Id �Ļ������һ���û�
            {
                // �����û�������Ӵ���
                tblTbJudge.UpdateTime = tblTbJudge.CreateTime = DateTimeUtil.Now;
                if (ddlbIsActive.SelectedValue.ToLower() == "y")
                    tblTbJudge.IsValid = 1;
                else
                    tblTbJudge.IsValid = 0;
                tblTbJudge.Aliases = edtUserName.Text.Trim();
                tblTbJudge.FullName = edtRealName.Text.Trim();
                tblTbJudge.LoginKey = tbl.Password;
                tblTbJudge.Salt = Guid.NewGuid().ToString();
                tblTbJudge.LoginKey = CreatePasswordHash(tblTbJudge.LoginKey, tblTbJudge.Salt);
                tblTbJudge.Insert();

                bool bIsAddSuccess = tblTbJudge.FetchByAliases();
                if (!bIsAddSuccess)
                {
                    throw new Exception("�����û����֮���޷����»�ȡ���û������ݣ�");
                }


                tbl.ThirdPartyCode = tblTbJudge.Id.ToString();
                tbl.Update();
            }

        }

        string sRoleUid = ddlbRole.SelectedValue;
        SbtRoleUserUtil.SetRoleUidToUser(tbl.UserUid, sRoleUid);

        //========== 4. ���ص���ҳ�� ==========
        Response.Redirect("user_list.aspx?dpt_uid=" + GetCurrentDeptId());
    }
    public virtual string CreatePasswordHash(string password, string saltkey, string passwordFormat = "MD5")
    {
        if (String.IsNullOrEmpty(passwordFormat))
            passwordFormat = "MD5";
        string saltAndPassword = String.Concat(password, saltkey);
        string hashedPassword =
            FormsAuthentication.HashPasswordForStoringInConfigFile(
                saltAndPassword, passwordFormat);
        return hashedPassword;
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
            tbl.Fetch();

            TbJudge tblTbJudge = new TbJudge();
            tblTbJudge.Id = ConvertUtil.ToInt(tbl.ThirdPartyCode, 0);
            tblTbJudge.Delete();

            tbl.Delete();
        }
    }
}

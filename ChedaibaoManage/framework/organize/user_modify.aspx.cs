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


        //========== 0. 删除记录 ==========
        string sDelRecords = ConvertUtil.ToString(Request["del_rec"]);
        if (sDelRecords != "")
        {
            DeleteData(sDelRecords);
            Response.Redirect("user_list.aspx?dpt_uid=" + GetCurrentDeptId());
            return;
        }

        //========== 1. 初始化界面 ==========
        WCUComboBox.InitComboBox(ddlbRole, SbtCodeTables.Role);
        ddlbRole.Items.Insert(0, new ListItem("[未指定角色]", ""));
        ddlbRole.SelectedValue = "";

        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. 新增的判断和处理 ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "新增用户";
            btnOK.Text = "新增";
            edtPassword.Text = "";
            return;
        }

        //========== 4. 取数据 ==========
        TbUser tbl = new TbUser();
        tbl.UserUid = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. 更新各控件的显示 ==========
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

        //========== 1. 验证数据的有效性 ==========
        //========== 1.1 判断帐号是否为空 ==========
        string sUserName = edtUserName.Text.Trim();
        if (sUserName == "")
        {
            lblErrMessage.Text = "必须填写帐号";
            lblErrMessage.Visible = true;
            edtUserName.Focus();
            return;
        }

        //========== 1.2 判断姓名是否为空 ==========
        string sRealName = edtRealName.Text.Trim();
        if (sRealName == "")
        {
            lblErrMessage.Text = "必须填写姓名";
            lblErrMessage.Visible = true;
            edtRealName.Focus();
            return;
        }

        //=========== 1.3 帐号不能重复 ================
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
            lblErrMessage.Text = "输入的帐号和已有的帐号重复，必须重新指定";
            lblErrMessage.Visible = true;
            edtUserName.Focus();
            return;
        }

        //=========== 1.4 网贷系统帐号不能重复 ================
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
                    lblErrMessage.Text = "输入的帐号和已有的帐号重复，必须重新指定.(网贷用户表)";
                    lblErrMessage.Visible = true;
                    edtUserName.Focus();
                    return;
                }
            }
        }

        //========== 2. 数据新增处理 ==========
        TbUser tbl = new TbUser();
        TbJudge tblTbJudge = new TbJudge();

        if (bAppendMode)
        {
            // 网贷用户数据添加代码
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
                throw new Exception("网贷用户添加之后，无法重新获取该用户的数据！");
            }


            tbl.ThirdPartyCode = tblTbJudge.Id.ToString();

            // 公司的框架的后台用户添加代码
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

        //========== 3. 数据更新处理 ==========
        else
        {




            // 公司的框架的后台用户添加代码

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

            // 网贷用户数据添加代码
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
            else // 假如更新的是否框架表里边没有第三方 Id 的话就添加一个用户
            {
                // 网贷用户数据添加代码
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
                    throw new Exception("网贷用户添加之后，无法重新获取该用户的数据！");
                }


                tbl.ThirdPartyCode = tblTbJudge.Id.ToString();
                tbl.Update();
            }

        }

        string sRoleUid = ddlbRole.SelectedValue;
        SbtRoleUserUtil.SetRoleUidToUser(tbl.UserUid, sRoleUid);

        //========== 4. 返回到主页面 ==========
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

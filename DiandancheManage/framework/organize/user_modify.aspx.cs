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
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;

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

        edtAreaName.Text = tbl.Address;
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

        //========== 1.2 判断手机号姓名是否为空 ==========
        string sPhone = edtMobilePhone.Text.Trim();
        //if (sPhone == "")
        //{
        //    lblErrMessage.Text = "必须填写手机号";
        //    lblErrMessage.Visible = true;
        //    edtMobilePhone.Focus();
        //    return;
        //}

        string sRealName = edtRealName.Text.Trim();
        if (sRealName == "")
        {
            lblErrMessage.Text = "必须填写姓名";
            lblErrMessage.Visible = true;
            edtRealName.Focus();
            return;
        }

        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.Search(new Borrower() { IsValid = true }).Find(o => o.Phone == sPhone);
        //if (borrower == null)
        //{
        //    lblErrMessage.Text = "手机号未在公众号注册";
        //    lblErrMessage.Visible = true;
        //    edtMobilePhone.Focus();
        //    return;
        //}


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


        TbUser tblUser = new TbUser();
        tblUser.UserName = edtUserName.Text;
        tblUser.Mobilephone = edtMobilePhone.Text;
        tblUser.RealName = edtRealName.Text;
        tblUser.ThirdPartyCode = borrower == null ? "0" : borrower.Id.ToString();
        tblUser.Sex = ddlbSex.SelectedValue;
        tblUser.Address = edtAreaName.Text;
        tblUser.Telephone = edtTelephone.Text;
        tblUser.Email = edtEmail.Text;
        tblUser.IsActive = ddlbIsActive.SelectedValue;
        tblUser.IsAdmin = cbxIsAdmin.Checked == true ? "Y" : "N";
        tblUser.Remarks = edtRemarks.Text;
        tblUser.DeptId = GetCurrentDeptId();
        tblUser.CreateTime = DateTimeUtil.ToDateTimeStr(DateTime.Now);
        tblUser.Creator = CurrentUser.UserName;

        TbRoleUser tblru = new TbRoleUser();
        if (bAppendMode)
        {
            tblUser.UserUid = Guid.NewGuid().ToString();
            tblUser.Password = edtPassword.Text;
            tblru.UserUid = tblUser.UserUid;
            tblru.RoleUid = ddlbRole.SelectedValue;
            tblUser.Insert();
            tblru.Insert();
        }
        else
        {
            TbUser oldtbl = new TbUser();
            oldtbl.UserUid = sParamRecKey;
            oldtbl.Fetch();

            tblUser.UserUid = sParamRecKey;
            tblUser.Password = oldtbl.Password;
            tblru.UserUid = tblUser.UserUid;
            tblru.RoleUid = ddlbRole.SelectedValue;
            tblUser.Update();
            tblru.Update();
        }
        if (borrower != null)
        {
            borrower.FullName = edtRealName.Text;
            borrowerService.Update(borrower);
        }
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
        }
    }
}

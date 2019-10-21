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

public partial class genui_ETJJ_role_manage_modify : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 0. 删除记录 ==========
        string sDelRecords = ConvertUtil.ToString(Request["del_rec"]);
        if (sDelRecords != "")
        {
            DeleteData(sDelRecords);
            Response.Redirect("role_manage_list.aspx");
            return;
        }

        //========== 1. 初始化界面 ==========
        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. 新增的判断和处理 ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "新增用户角色";
            btnOK.Text = "新增";
            return;
        }

        //========== 4. 取数据 ==========
        TbRole tbl = new TbRole();
        tbl.RoleUid = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. 更新各控件的显示 ==========
        edtRoleName.Text = tbl.RoleName;
        edtRemarks.Text = tbl.Remarks;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string sRoleUid = ViewState["rec_key"].ToString();
        bool bAppendMode = false;
        if (ConvertUtil.ToString(ViewState["rec_key"]) == "")
            bAppendMode = true;

        //========== 1. 验证数据的有效性 ==========

        //========== 1.1 判断角色名称是否为空 ==========
        string sRoleName = edtRoleName.Text.Trim();
        if (sRoleName == "")
        {
            lblErrMessage.Text = "必须填写角色名称";
            lblErrMessage.Visible = true;
            edtRoleName.Focus();
            return;
        }

        //========== 2. 数据新增处理 ==========
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

        //========== 3. 数据更新处理 ==========
        else
        {
            tbl.RoleUid = sRoleUid;
            tbl.Fetch();

            tbl.RoleName = edtRoleName.Text.Trim();
            tbl.Remarks = edtRemarks.Text.Trim();

            tbl.Update();
        }

        //========== 4. 返回到主页面 ==========
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

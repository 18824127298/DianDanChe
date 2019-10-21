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

public partial class genui_PAIM_operation_log_browse_modify : SbtPageBase
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
        TbLogOperationAudit tbl = new TbLogOperationAudit();
        tbl.LogUid = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. 更新各控件的显示 ==========
        lblUserName.Text = tbl.UserName;
        lblProcTime.Text = tbl.ProcTime;
        lblProcClassName.Text = tbl.ProcClassName;
        lblProcSubclassName.Text = tbl.ProcSubclassName;
        lblActionName.Text = tbl.ActionName;
        memoActionDescription.Text = tbl.ActionDescription;
        lblActionDescription.Text = tbl.ActionDescription.Replace("\r", "").Replace("\n", "<br />\r\n");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("operation_log_browse_list.aspx");
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbLogOperationAudit tbl = new TbLogOperationAudit();
            tbl.LogUid = sSelectedID;
            tbl.Delete();
        }
    }

    protected void btnModifyDescription_Click(object sender, EventArgs e)
    {
        btnModifyDescription.Visible = false;
        btnSaveDescription.Visible = true;

        lblActionDescription.Visible = false;
        memoActionDescription.Visible = true;
    }

    protected void btnSaveDescription_Click(object sender, EventArgs e)
    {
        //=========== 1. 保存更新 ================
        string sParamRecKey = ConvertUtil.ToString(ViewState["rec_key"]);
        TbLogOperationAudit tblLog = new TbLogOperationAudit();
        tblLog.LogUid = sParamRecKey;
        tblLog.Fetch();

        tblLog.ActionDescription = memoActionDescription.Text.Trim();
        tblLog.ModifyTime = DateTimeUtil.Now;
        tblLog.Update();

        //=========== 2. 更新显示 ===================
        lblActionDescription.Text = tblLog.ActionDescription.Replace("\r", "").Replace("\n", "<br />\r\n");

        //========== 3. 切换显示方式 =============
        btnModifyDescription.Visible = true;
        btnSaveDescription.Visible = false;

        lblActionDescription.Visible = true;
        memoActionDescription.Visible = false;
    }
}

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

public partial class genui_WSQV_system_define_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. 初始化界面 ==========
        //========== 2. 取出查询条件 ==========
        if (PageParameter.StringParam[0] != "system_defineWSQV")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. 刷新数据显示 ==========
        //========== 3.1 系统编号 ==========
        string sSubSystemId = sqlBuilder.GetConditionValueString("sub_system_id");
        edtSubSystemId.Text = sSubSystemId;

        //========== 3.2 系统名字 ==========
        string sSubSystemName = sqlBuilder.GetConditionValueString("sub_system_name");
        edtSubSystemName.Text = sSubSystemName;
        this.NaviTabController.AppendSelfToBar();
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. 重新清空搜索条件 ==========
        if (PageParameter.StringParam[0] != "system_defineWSQV")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. 设置搜索条件 ==========
        //========== 2.1 系统编号 ==========
        string sSubSystemId = edtSubSystemId.Text.Trim();
        if (sSubSystemId != "")
            sqlBuilder.AddCondition("sub_system_id", "系统编号",
                    sSubSystemId, SQLConditionOperator.Like);

        //========== 2.2 系统名字 ==========
        string sSubSystemName = edtSubSystemName.Text.Trim();
        if (sSubSystemName != "")
            sqlBuilder.AddCondition("sub_system_name", "系统名字",
                    sSubSystemName, SQLConditionOperator.Like);

        //========== 3. 返回到数据列表页面 ==========
        Response.Redirect("system_define_list.aspx");
    }
}

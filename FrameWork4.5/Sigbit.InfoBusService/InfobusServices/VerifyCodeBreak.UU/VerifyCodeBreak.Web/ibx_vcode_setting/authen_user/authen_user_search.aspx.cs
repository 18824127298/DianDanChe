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
using Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine;

public partial class genui_EEQY_authen_user_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. 初始化界面 ==========
        //========== 2. 取出查询条件 ==========
        if (PageParameter.StringParam[0] != "authen_userEEQY")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. 刷新数据显示 ==========
        //========== 3.1 用户名 ==========
        string sAuthenUserName = sqlBuilder.GetConditionValueString("authen_user_name");
        edtAuthenUserName.Text = sAuthenUserName;

        //========== 3.2 备注 ==========
        string sRemarks = sqlBuilder.GetConditionValueString("remarks");
        edtRemarks.Text = sRemarks;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. 重新清空搜索条件 ==========
        if (PageParameter.StringParam[0] != "authen_userEEQY")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. 设置搜索条件 ==========
        //========== 2.1 用户名 ==========
        string sAuthenUserName = edtAuthenUserName.Text.Trim();
        if (sAuthenUserName != "")
            sqlBuilder.AddCondition("authen_user_name", "用户名",
                    sAuthenUserName, SQLConditionOperator.Like);

        //========== 2.2 备注 ==========
        string sRemarks = edtRemarks.Text.Trim();
        if (sRemarks != "")
            sqlBuilder.AddCondition("remarks", "备注",
                    sRemarks, SQLConditionOperator.Like);

        //========== 3. 返回到数据列表页面 ==========
        Response.Redirect("authen_user_list.aspx");
    }
}

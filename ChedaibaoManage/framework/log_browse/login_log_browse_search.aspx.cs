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

public partial class genui_DITJ_login_log_browse_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. 初始化界面 ==========
        ddlbIsLoginSuccess.Items.Clear();
        ddlbIsLoginSuccess.Items.Add(new ListItem("登录成功", "Y"));
        ddlbIsLoginSuccess.Items.Add(new ListItem("登录失败", "N"));
        ddlbIsLoginSuccess.Items.Add(new ListItem("[所有登录结果]", "ALL"));
        ddlbIsLoginSuccess.SelectedValue = "ALL";

        //========== 2. 取出查询条件 ==========
        if (PageParameter.StringParam[0] != "login_log_browseDITJ")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. 刷新数据显示 ==========
        //========== 3.1 登录时间 ==========
        string sFromDate = sqlBuilder.GetConditionValueString("login_time",
                SQLConditionOperator.GreaterEqualThan);
        sFromDate = DateTimeUtil.GetDatePart(sFromDate);
        DatePickerFrom.DateString = sFromDate;

        string sToDate = sqlBuilder.GetConditionValueString("login_time",
                SQLConditionOperator.LessEqualThan);
        sToDate = DateTimeUtil.GetDatePart(sToDate);
        DatePickerTo.DateString = sToDate;

        //========== 3.2 用户名 ==========
        string sUserName = sqlBuilder.GetConditionValueString("user_name");
        edtUserName.Text = sUserName;

        //========== 3.3 登录成功 ==========
        string sIsLoginSuccess = sqlBuilder.GetConditionValueString("is_login_success");
        if (sIsLoginSuccess != "")
            ddlbIsLoginSuccess.SelectedValue = sIsLoginSuccess;

        //========== 3.4 失败原因 ==========
        string sLoginFailDesc = sqlBuilder.GetConditionValueString("login_fail_desc");
        edtLoginFailDesc.Text = sLoginFailDesc;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. 重新清空搜索条件 ==========
        if (PageParameter.StringParam[0] != "login_log_browseDITJ")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. 设置搜索条件 ==========
        //========== 2.1 登录时间 ==========
        string sFromDate = DatePickerFrom.DateString;
        if (sFromDate != "")
            sqlBuilder.AddCondition("login_time", "登录时间",
                    DateTimeUtil.BeginTimeOfDay(sFromDate),
                    SQLConditionOperator.GreaterEqualThan,
                    "起始日期为" + DateTimeUtil.GetDatePart(sFromDate));

        string sToDate = DatePickerTo.DateString;
        if (sToDate != "")
            sqlBuilder.AddCondition("login_time", "登录时间",
                    DateTimeUtil.EndTimeOfDay(sToDate),
                    SQLConditionOperator.LessEqualThan,
                    "结束日期为" + DateTimeUtil.GetDatePart(sToDate));

        //========== 2.2 用户名 ==========
        string sUserName = edtUserName.Text.Trim();
        if (sUserName != "")
            sqlBuilder.AddCondition("user_name", "用户名",
                    sUserName, SQLConditionOperator.Like);

        //========== 2.3 登录成功 ==========
        string sIsLoginSuccess = ddlbIsLoginSuccess.SelectedValue;
        if (sIsLoginSuccess != "ALL")
            sqlBuilder.AddCondition("is_login_success", "登录结果",
                    sIsLoginSuccess, SQLConditionOperator.Equal, ddlbIsLoginSuccess.SelectedItem.Text);

        //========== 2.4 失败原因 ==========
        string sLoginFailDesc = edtLoginFailDesc.Text.Trim();
        if (sLoginFailDesc != "")
            sqlBuilder.AddCondition("login_fail_desc", "失败原因",
                    sLoginFailDesc, SQLConditionOperator.Like);

        //========== 3. 返回到数据列表页面 ==========
        Response.Redirect("login_log_browse_list.aspx");
    }
}

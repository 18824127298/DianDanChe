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

public partial class genui_PAIM_operation_log_browse_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. 初始化界面 ==========
        //========== 2. 取出查询条件 ==========
        if (PageParameter.StringParam[0] != "operation_log_browsePAIM")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. 刷新数据显示 ==========
        //========== 3.1 操作员 ==========
        string sUserName = sqlBuilder.GetConditionValueString("user_name");
        edtUserName.Text = sUserName;

        //========== 3.2 时间 ==========
        string sFromDate = sqlBuilder.GetConditionValueString("proc_time",
                SQLConditionOperator.GreaterEqualThan);
        sFromDate = DateTimeUtil.GetDatePart(sFromDate);
        DatePickerFrom.DateString = sFromDate;

        string sToDate = sqlBuilder.GetConditionValueString("proc_time",
                SQLConditionOperator.LessEqualThan);
        sToDate = DateTimeUtil.GetDatePart(sToDate);
        DatePickerTo.DateString = sToDate;

        //========== 3.3 操作类型 ==========
        string sProcClassName = sqlBuilder.GetConditionValueString("proc_class_name");
        edtProcClassName.Text = sProcClassName;

        //========== 3.4 子类型 ==========
        string sProcSubclassName = sqlBuilder.GetConditionValueString("proc_subclass_name");
        edtProcSubclassName.Text = sProcSubclassName;

        //========== 3.5 操作 ==========
        string sActionName = sqlBuilder.GetConditionValueString("action_name");
        edtActionName.Text = sActionName;

        //========== 3.6 操作描述 ==========
        string sActionDescription = sqlBuilder.GetConditionValueString("action_description");
        edtActionDescription.Text = sActionDescription;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. 重新清空搜索条件 ==========
        if (PageParameter.StringParam[0] != "operation_log_browsePAIM")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. 设置搜索条件 ==========
        //========== 2.1 操作员 ==========
        string sUserName = edtUserName.Text.Trim();
        if (sUserName != "")
            sqlBuilder.AddCondition("user_name", "操作员",
                    sUserName, SQLConditionOperator.Like);

        //========== 2.2 时间 ==========
        string sFromDate = DatePickerFrom.DateString;
        if (sFromDate != "")
            sqlBuilder.AddCondition("proc_time", "时间",
                    DateTimeUtil.BeginTimeOfDay(sFromDate),
                    SQLConditionOperator.GreaterEqualThan,
                    "起始日期为" + DateTimeUtil.GetDatePart(sFromDate));

        string sToDate = DatePickerTo.DateString;
        if (sToDate != "")
            sqlBuilder.AddCondition("proc_time", "时间",
                    DateTimeUtil.EndTimeOfDay(sToDate),
                    SQLConditionOperator.LessEqualThan,
                    "结束日期为" + DateTimeUtil.GetDatePart(sToDate));

        //========== 2.3 操作类型 ==========
        string sProcClassName = edtProcClassName.Text.Trim();
        if (sProcClassName != "")
            sqlBuilder.AddCondition("proc_class_name", "操作类型",
                    sProcClassName, SQLConditionOperator.Like);

        //========== 2.4 子类型 ==========
        string sProcSubclassName = edtProcSubclassName.Text.Trim();
        if (sProcSubclassName != "")
            sqlBuilder.AddCondition("proc_subclass_name", "子类型",
                    sProcSubclassName, SQLConditionOperator.Like);

        //========== 2.5 操作 ==========
        string sActionName = edtActionName.Text.Trim();
        if (sActionName != "")
            sqlBuilder.AddCondition("action_name", "操作",
                    sActionName, SQLConditionOperator.Like);

        //========== 2.6 操作描述 ==========
        string sActionDescription = edtActionDescription.Text.Trim();
        if (sActionDescription != "")
            sqlBuilder.AddCondition("action_description", "操作描述",
                    sActionDescription, SQLConditionOperator.Like);

        //========== 3. 返回到数据列表页面 ==========
        Response.Redirect("operation_log_browse_list.aspx");
    }
}

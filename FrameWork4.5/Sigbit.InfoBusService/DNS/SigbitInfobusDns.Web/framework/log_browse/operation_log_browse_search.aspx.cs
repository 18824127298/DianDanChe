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

        //========== 1. ��ʼ������ ==========
        //========== 2. ȡ����ѯ���� ==========
        if (PageParameter.StringParam[0] != "operation_log_browsePAIM")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. ˢ��������ʾ ==========
        //========== 3.1 ����Ա ==========
        string sUserName = sqlBuilder.GetConditionValueString("user_name");
        edtUserName.Text = sUserName;

        //========== 3.2 ʱ�� ==========
        string sFromDate = sqlBuilder.GetConditionValueString("proc_time",
                SQLConditionOperator.GreaterEqualThan);
        sFromDate = DateTimeUtil.GetDatePart(sFromDate);
        DatePickerFrom.DateString = sFromDate;

        string sToDate = sqlBuilder.GetConditionValueString("proc_time",
                SQLConditionOperator.LessEqualThan);
        sToDate = DateTimeUtil.GetDatePart(sToDate);
        DatePickerTo.DateString = sToDate;

        //========== 3.3 �������� ==========
        string sProcClassName = sqlBuilder.GetConditionValueString("proc_class_name");
        edtProcClassName.Text = sProcClassName;

        //========== 3.4 ������ ==========
        string sProcSubclassName = sqlBuilder.GetConditionValueString("proc_subclass_name");
        edtProcSubclassName.Text = sProcSubclassName;

        //========== 3.5 ���� ==========
        string sActionName = sqlBuilder.GetConditionValueString("action_name");
        edtActionName.Text = sActionName;

        //========== 3.6 �������� ==========
        string sActionDescription = sqlBuilder.GetConditionValueString("action_description");
        edtActionDescription.Text = sActionDescription;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. ��������������� ==========
        if (PageParameter.StringParam[0] != "operation_log_browsePAIM")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. ������������ ==========
        //========== 2.1 ����Ա ==========
        string sUserName = edtUserName.Text.Trim();
        if (sUserName != "")
            sqlBuilder.AddCondition("user_name", "����Ա",
                    sUserName, SQLConditionOperator.Like);

        //========== 2.2 ʱ�� ==========
        string sFromDate = DatePickerFrom.DateString;
        if (sFromDate != "")
            sqlBuilder.AddCondition("proc_time", "ʱ��",
                    DateTimeUtil.BeginTimeOfDay(sFromDate),
                    SQLConditionOperator.GreaterEqualThan,
                    "��ʼ����Ϊ" + DateTimeUtil.GetDatePart(sFromDate));

        string sToDate = DatePickerTo.DateString;
        if (sToDate != "")
            sqlBuilder.AddCondition("proc_time", "ʱ��",
                    DateTimeUtil.EndTimeOfDay(sToDate),
                    SQLConditionOperator.LessEqualThan,
                    "��������Ϊ" + DateTimeUtil.GetDatePart(sToDate));

        //========== 2.3 �������� ==========
        string sProcClassName = edtProcClassName.Text.Trim();
        if (sProcClassName != "")
            sqlBuilder.AddCondition("proc_class_name", "��������",
                    sProcClassName, SQLConditionOperator.Like);

        //========== 2.4 ������ ==========
        string sProcSubclassName = edtProcSubclassName.Text.Trim();
        if (sProcSubclassName != "")
            sqlBuilder.AddCondition("proc_subclass_name", "������",
                    sProcSubclassName, SQLConditionOperator.Like);

        //========== 2.5 ���� ==========
        string sActionName = edtActionName.Text.Trim();
        if (sActionName != "")
            sqlBuilder.AddCondition("action_name", "����",
                    sActionName, SQLConditionOperator.Like);

        //========== 2.6 �������� ==========
        string sActionDescription = edtActionDescription.Text.Trim();
        if (sActionDescription != "")
            sqlBuilder.AddCondition("action_description", "��������",
                    sActionDescription, SQLConditionOperator.Like);

        //========== 3. ���ص������б�ҳ�� ==========
        Response.Redirect("operation_log_browse_list.aspx");
    }
}

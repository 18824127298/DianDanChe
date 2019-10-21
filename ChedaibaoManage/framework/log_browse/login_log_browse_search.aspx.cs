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

        //========== 1. ��ʼ������ ==========
        ddlbIsLoginSuccess.Items.Clear();
        ddlbIsLoginSuccess.Items.Add(new ListItem("��¼�ɹ�", "Y"));
        ddlbIsLoginSuccess.Items.Add(new ListItem("��¼ʧ��", "N"));
        ddlbIsLoginSuccess.Items.Add(new ListItem("[���е�¼���]", "ALL"));
        ddlbIsLoginSuccess.SelectedValue = "ALL";

        //========== 2. ȡ����ѯ���� ==========
        if (PageParameter.StringParam[0] != "login_log_browseDITJ")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. ˢ��������ʾ ==========
        //========== 3.1 ��¼ʱ�� ==========
        string sFromDate = sqlBuilder.GetConditionValueString("login_time",
                SQLConditionOperator.GreaterEqualThan);
        sFromDate = DateTimeUtil.GetDatePart(sFromDate);
        DatePickerFrom.DateString = sFromDate;

        string sToDate = sqlBuilder.GetConditionValueString("login_time",
                SQLConditionOperator.LessEqualThan);
        sToDate = DateTimeUtil.GetDatePart(sToDate);
        DatePickerTo.DateString = sToDate;

        //========== 3.2 �û��� ==========
        string sUserName = sqlBuilder.GetConditionValueString("user_name");
        edtUserName.Text = sUserName;

        //========== 3.3 ��¼�ɹ� ==========
        string sIsLoginSuccess = sqlBuilder.GetConditionValueString("is_login_success");
        if (sIsLoginSuccess != "")
            ddlbIsLoginSuccess.SelectedValue = sIsLoginSuccess;

        //========== 3.4 ʧ��ԭ�� ==========
        string sLoginFailDesc = sqlBuilder.GetConditionValueString("login_fail_desc");
        edtLoginFailDesc.Text = sLoginFailDesc;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. ��������������� ==========
        if (PageParameter.StringParam[0] != "login_log_browseDITJ")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. ������������ ==========
        //========== 2.1 ��¼ʱ�� ==========
        string sFromDate = DatePickerFrom.DateString;
        if (sFromDate != "")
            sqlBuilder.AddCondition("login_time", "��¼ʱ��",
                    DateTimeUtil.BeginTimeOfDay(sFromDate),
                    SQLConditionOperator.GreaterEqualThan,
                    "��ʼ����Ϊ" + DateTimeUtil.GetDatePart(sFromDate));

        string sToDate = DatePickerTo.DateString;
        if (sToDate != "")
            sqlBuilder.AddCondition("login_time", "��¼ʱ��",
                    DateTimeUtil.EndTimeOfDay(sToDate),
                    SQLConditionOperator.LessEqualThan,
                    "��������Ϊ" + DateTimeUtil.GetDatePart(sToDate));

        //========== 2.2 �û��� ==========
        string sUserName = edtUserName.Text.Trim();
        if (sUserName != "")
            sqlBuilder.AddCondition("user_name", "�û���",
                    sUserName, SQLConditionOperator.Like);

        //========== 2.3 ��¼�ɹ� ==========
        string sIsLoginSuccess = ddlbIsLoginSuccess.SelectedValue;
        if (sIsLoginSuccess != "ALL")
            sqlBuilder.AddCondition("is_login_success", "��¼���",
                    sIsLoginSuccess, SQLConditionOperator.Equal, ddlbIsLoginSuccess.SelectedItem.Text);

        //========== 2.4 ʧ��ԭ�� ==========
        string sLoginFailDesc = edtLoginFailDesc.Text.Trim();
        if (sLoginFailDesc != "")
            sqlBuilder.AddCondition("login_fail_desc", "ʧ��ԭ��",
                    sLoginFailDesc, SQLConditionOperator.Like);

        //========== 3. ���ص������б�ҳ�� ==========
        Response.Redirect("login_log_browse_list.aspx");
    }
}
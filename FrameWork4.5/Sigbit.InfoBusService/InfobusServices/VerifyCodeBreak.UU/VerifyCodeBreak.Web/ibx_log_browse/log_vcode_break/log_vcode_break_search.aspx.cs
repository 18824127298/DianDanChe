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

public partial class genui_KTJQ_log_vcode_break_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. ��ʼ������ ==========
        //========== 2. ȡ����ѯ���� ==========
        if (PageParameter.StringParam[0] != "log_vcode_breakKTJQ")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. ˢ��������ʾ ==========
        //========== 3.1 ����ʱ�� ==========
        string sFromDate = sqlBuilder.GetConditionValueString("request_time",
                SQLConditionOperator.GreaterEqualThan);
        sFromDate = DateTimeUtil.GetDatePart(sFromDate);
        DatePickerFrom.DateString = sFromDate;

        string sToDate = sqlBuilder.GetConditionValueString("request_time",
                SQLConditionOperator.LessEqualThan);
        sToDate = DateTimeUtil.GetDatePart(sToDate);
        DatePickerTo.DateString = sToDate;

        //========== 3.2 ͼ���ļ� ==========
        string sImageFileName = sqlBuilder.GetConditionValueString("image_file_name");
        edtImageFileName.Text = sImageFileName;

        //========== 3.3 ��֤������ ==========
        string sVcodeId = sqlBuilder.GetConditionValueString("vcode_id");
        edtVcodeId.Text = sVcodeId;

        //========== 3.4 �ͻ��� ==========
        string sFromClientId = sqlBuilder.GetConditionValueString("from_client_id");
        edtFromClientId.Text = sFromClientId;

        //========== 3.5 ״̬ ==========
        string sCurrentStatus = sqlBuilder.GetConditionValueString("current_status");
        edtCurrentStatus.Text = sCurrentStatus;

        //========== 3.6 �ƽ��� ==========
        string sBreakResult = sqlBuilder.GetConditionValueString("break_result");
        edtBreakResult.Text = sBreakResult;

        //========== 3.7 ʧ��ԭ�� ==========
        string sFailDesc = sqlBuilder.GetConditionValueString("fail_desc");
        edtFailDesc.Text = sFailDesc;

        //========== 3.8 ��֤������ ==========
        string sBreakText = sqlBuilder.GetConditionValueString("break_text");
        edtBreakText.Text = sBreakText;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. ��������������� ==========
        if (PageParameter.StringParam[0] != "log_vcode_breakKTJQ")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. ������������ ==========
        //========== 2.1 ����ʱ�� ==========
        string sFromDate = DatePickerFrom.DateString;
        if (sFromDate != "")
            sqlBuilder.AddCondition("request_time", "����ʱ��",
                    DateTimeUtil.BeginTimeOfDay(sFromDate),
                    SQLConditionOperator.GreaterEqualThan,
                    "��ʼ����Ϊ" + DateTimeUtil.GetDatePart(sFromDate));

        string sToDate = DatePickerTo.DateString;
        if (sToDate != "")
            sqlBuilder.AddCondition("request_time", "����ʱ��",
                    DateTimeUtil.EndTimeOfDay(sToDate),
                    SQLConditionOperator.LessEqualThan,
                    "��������Ϊ" + DateTimeUtil.GetDatePart(sToDate));

        //========== 2.2 ͼ���ļ� ==========
        string sImageFileName = edtImageFileName.Text.Trim();
        if (sImageFileName != "")
            sqlBuilder.AddCondition("image_file_name", "ͼ���ļ�",
                    sImageFileName, SQLConditionOperator.Like);

        //========== 2.3 ��֤������ ==========
        string sVcodeId = edtVcodeId.Text.Trim();
        if (sVcodeId != "")
            sqlBuilder.AddCondition("vcode_id", "��֤������",
                    sVcodeId, SQLConditionOperator.Like);

        //========== 2.4 �ͻ��� ==========
        string sFromClientId = edtFromClientId.Text.Trim();
        if (sFromClientId != "")
            sqlBuilder.AddCondition("from_client_id", "�ͻ���",
                    sFromClientId, SQLConditionOperator.Like);

        //========== 2.5 ״̬ ==========
        string sCurrentStatus = edtCurrentStatus.Text.Trim();
        if (sCurrentStatus != "")
            sqlBuilder.AddCondition("current_status", "״̬",
                    sCurrentStatus, SQLConditionOperator.Like);

        //========== 2.6 �ƽ��� ==========
        string sBreakResult = edtBreakResult.Text.Trim();
        if (sBreakResult != "")
            sqlBuilder.AddCondition("break_result", "�ƽ���",
                    sBreakResult, SQLConditionOperator.Like);

        //========== 2.7 ʧ��ԭ�� ==========
        string sFailDesc = edtFailDesc.Text.Trim();
        if (sFailDesc != "")
            sqlBuilder.AddCondition("fail_desc", "ʧ��ԭ��",
                    sFailDesc, SQLConditionOperator.Like);

        //========== 2.8 ��֤������ ==========
        string sBreakText = edtBreakText.Text.Trim();
        if (sBreakText != "")
            sqlBuilder.AddCondition("break_text", "��֤������",
                    sBreakText, SQLConditionOperator.Like);

        //========== 3. ���ص������б�ҳ�� ==========
        Response.Redirect("log_vcode_break_list.aspx");
    }
}

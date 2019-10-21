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
using Sigbit.App.Net.IBXService.VoiceReg.Service.DBDefine;

public partial class genui_NVAI_log_voice_reg_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. ��ʼ������ ==========
        //========== 2. ȡ����ѯ���� ==========
        if (PageParameter.StringParam[0] != "log_voice_regNVAI")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. ˢ��������ʾ ==========
        //========== 3.1 �ϴ�ʱ�� ==========
        string sFromDate = sqlBuilder.GetConditionValueString("upload_time",
                SQLConditionOperator.GreaterEqualThan);
        sFromDate = DateTimeUtil.GetDatePart(sFromDate);
        DatePickerFrom.DateString = sFromDate;

        string sToDate = sqlBuilder.GetConditionValueString("upload_time",
                SQLConditionOperator.LessEqualThan);
        sToDate = DateTimeUtil.GetDatePart(sToDate);
        DatePickerTo.DateString = sToDate;

        //========== 3.2 ���������� ==========
        string sReqeustThirdId = sqlBuilder.GetConditionValueString("reqeust_third_id");
        edtReqeustThirdId.Text = sReqeustThirdId;

        //========== 3.3 �����ļ� ==========
        string sVoiceFileLocal = sqlBuilder.GetConditionValueString("voice_file_local");
        edtVoiceFileLocal.Text = sVoiceFileLocal;

        //========== 3.4 �ϴ��ļ� ==========
        string sVoiceFileUpload = sqlBuilder.GetConditionValueString("voice_file_upload");
        edtVoiceFileUpload.Text = sVoiceFileUpload;

        //========== 3.5 �����ļ� ==========
        string sVoiceFileForIsr = sqlBuilder.GetConditionValueString("voice_file_for_isr");
        edtVoiceFileForIsr.Text = sVoiceFileForIsr;

        //========== 3.6 �﷨ ==========
        string sGrammarId = sqlBuilder.GetConditionValueString("grammar_id");
        edtGrammarId.Text = sGrammarId;

        //========== 3.7 �ͻ��� ==========
        string sFromClientId = sqlBuilder.GetConditionValueString("from_client_id");
        edtFromClientId.Text = sFromClientId;

        //========== 3.8 ��ǰ״̬ ==========
        string sCurrentStatus = sqlBuilder.GetConditionValueString("current_status");
        edtCurrentStatus.Text = sCurrentStatus;

        //========== 3.9 �ƽ��� ==========
        string sRegResultCode = sqlBuilder.GetConditionValueString("reg_result_code");
        edtRegResultCode.Text = sRegResultCode;

        //========== 3.10 ʧ��ԭ�� ==========
        string sRegFailDesc = sqlBuilder.GetConditionValueString("reg_fail_desc");
        edtRegFailDesc.Text = sRegFailDesc;

        //========== 3.11 ʶ������ ==========
        string sRegResultText = sqlBuilder.GetConditionValueString("reg_result_text");
        edtRegResultText.Text = sRegResultText;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. ��������������� ==========
        if (PageParameter.StringParam[0] != "log_voice_regNVAI")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. ������������ ==========
        //========== 2.1 �ϴ�ʱ�� ==========
        string sFromDate = DatePickerFrom.DateString;
        if (sFromDate != "")
            sqlBuilder.AddCondition("upload_time", "�ϴ�ʱ��",
                    DateTimeUtil.BeginTimeOfDay(sFromDate),
                    SQLConditionOperator.GreaterEqualThan,
                    "��ʼ����Ϊ" + DateTimeUtil.GetDatePart(sFromDate));

        string sToDate = DatePickerTo.DateString;
        if (sToDate != "")
            sqlBuilder.AddCondition("upload_time", "�ϴ�ʱ��",
                    DateTimeUtil.EndTimeOfDay(sToDate),
                    SQLConditionOperator.LessEqualThan,
                    "��������Ϊ" + DateTimeUtil.GetDatePart(sToDate));

        //========== 2.2 ���������� ==========
        string sReqeustThirdId = edtReqeustThirdId.Text.Trim();
        if (sReqeustThirdId != "")
            sqlBuilder.AddCondition("reqeust_third_id", "����������",
                    sReqeustThirdId, SQLConditionOperator.Like);

        //========== 2.3 �����ļ� ==========
        string sVoiceFileLocal = edtVoiceFileLocal.Text.Trim();
        if (sVoiceFileLocal != "")
            sqlBuilder.AddCondition("voice_file_local", "�����ļ�",
                    sVoiceFileLocal, SQLConditionOperator.Like);

        //========== 2.4 �ϴ��ļ� ==========
        string sVoiceFileUpload = edtVoiceFileUpload.Text.Trim();
        if (sVoiceFileUpload != "")
            sqlBuilder.AddCondition("voice_file_upload", "�ϴ��ļ�",
                    sVoiceFileUpload, SQLConditionOperator.Like);

        //========== 2.5 �����ļ� ==========
        string sVoiceFileForIsr = edtVoiceFileForIsr.Text.Trim();
        if (sVoiceFileForIsr != "")
            sqlBuilder.AddCondition("voice_file_for_isr", "�����ļ�",
                    sVoiceFileForIsr, SQLConditionOperator.Like);

        //========== 2.6 �﷨ ==========
        string sGrammarId = edtGrammarId.Text.Trim();
        if (sGrammarId != "")
            sqlBuilder.AddCondition("grammar_id", "�﷨",
                    sGrammarId, SQLConditionOperator.Like);

        //========== 2.7 �ͻ��� ==========
        string sFromClientId = edtFromClientId.Text.Trim();
        if (sFromClientId != "")
            sqlBuilder.AddCondition("from_client_id", "�ͻ���",
                    sFromClientId, SQLConditionOperator.Like);

        //========== 2.8 ��ǰ״̬ ==========
        string sCurrentStatus = edtCurrentStatus.Text.Trim();
        if (sCurrentStatus != "")
            sqlBuilder.AddCondition("current_status", "��ǰ״̬",
                    sCurrentStatus, SQLConditionOperator.Like);

        //========== 2.9 �ƽ��� ==========
        string sRegResultCode = edtRegResultCode.Text.Trim();
        if (sRegResultCode != "")
            sqlBuilder.AddCondition("reg_result_code", "�ƽ���",
                    sRegResultCode, SQLConditionOperator.Like);

        //========== 2.10 ʧ��ԭ�� ==========
        string sRegFailDesc = edtRegFailDesc.Text.Trim();
        if (sRegFailDesc != "")
            sqlBuilder.AddCondition("reg_fail_desc", "ʧ��ԭ��",
                    sRegFailDesc, SQLConditionOperator.Like);

        //========== 2.11 ʶ������ ==========
        string sRegResultText = edtRegResultText.Text.Trim();
        if (sRegResultText != "")
            sqlBuilder.AddCondition("reg_result_text", "ʶ������",
                    sRegResultText, SQLConditionOperator.Like);

        //========== 3. ���ص������б�ҳ�� ==========
        Response.Redirect("log_voice_reg_list.aspx");
    }
}

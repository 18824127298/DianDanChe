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
using Sigbit.App.Net.IBXService.VoiceQAN.Service.DBDefine;

public partial class genui_FBSL_log_voice_qan_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. ��ʼ������ ==========
        WCUComboBox.InitComboBox(ddlbCurrentStatus, EnumExUtil.ToCodeTable(TbLogVoiceQanECurrentStatus.None));
        ddlbCurrentStatus.Items.Add(new ListItem("[����״̬]", "ALL"));
        ddlbCurrentStatus.SelectedValue = "ALL";

        //========== 2. ȡ����ѯ���� ==========
        if (PageParameter.StringParam[0] != "log_voice_qanFBSL")
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

        //========== 3.2 ��������ʶ ==========
        string sReqeustThirdId = sqlBuilder.GetConditionValueString("reqeust_third_id");
        edtReqeustThirdId.Text = sReqeustThirdId;

        //========== 3.3 ���������ļ� ==========
        string sVoiceFileLocal = sqlBuilder.GetConditionValueString("voice_file_local");
        edtVoiceFileLocal.Text = sVoiceFileLocal;

        //========== 3.4 �ϴ������ļ� ==========
        string sVoiceFileUpload = sqlBuilder.GetConditionValueString("voice_file_upload");
        edtVoiceFileUpload.Text = sVoiceFileUpload;

        //========== 3.5 ���������ļ� ==========
        string sVoiceFileForQan = sqlBuilder.GetConditionValueString("voice_file_for_qan");
        edtVoiceFileForQan.Text = sVoiceFileForQan;

        //========== 3.6 ״̬ ==========
        string sCurrentStatus = sqlBuilder.GetConditionValueString("current_status");
        if (sCurrentStatus != "")
            ddlbCurrentStatus.SelectedValue = sCurrentStatus;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. ��������������� ==========
        if (PageParameter.StringParam[0] != "log_voice_qanFBSL")
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

        //========== 2.2 ��������ʶ ==========
        string sReqeustThirdId = edtReqeustThirdId.Text.Trim();
        if (sReqeustThirdId != "")
            sqlBuilder.AddCondition("reqeust_third_id", "��������ʶ",
                    sReqeustThirdId, SQLConditionOperator.Like);

        //========== 2.3 ���������ļ� ==========
        string sVoiceFileLocal = edtVoiceFileLocal.Text.Trim();
        if (sVoiceFileLocal != "")
            sqlBuilder.AddCondition("voice_file_local", "���������ļ�",
                    sVoiceFileLocal, SQLConditionOperator.Like);

        //========== 2.4 �ϴ������ļ� ==========
        string sVoiceFileUpload = edtVoiceFileUpload.Text.Trim();
        if (sVoiceFileUpload != "")
            sqlBuilder.AddCondition("voice_file_upload", "�ϴ������ļ�",
                    sVoiceFileUpload, SQLConditionOperator.Like);

        //========== 2.5 ���������ļ� ==========
        string sVoiceFileForQan = edtVoiceFileForQan.Text.Trim();
        if (sVoiceFileForQan != "")
            sqlBuilder.AddCondition("voice_file_for_qan", "���������ļ�",
                    sVoiceFileForQan, SQLConditionOperator.Like);

        //========== 2.6 ״̬ ==========
        string sCurrentStatus = ddlbCurrentStatus.SelectedValue;
        if (sCurrentStatus != "ALL")
            sqlBuilder.AddCondition("current_status", "״̬", sCurrentStatus,
                    SQLConditionOperator.Equal, ddlbCurrentStatus.SelectedItem.Text);

        //========== 3. ���ص������б�ҳ�� ==========
        Response.Redirect("log_voice_qan_list.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("log_voice_qan_list.aspx");
    }
}

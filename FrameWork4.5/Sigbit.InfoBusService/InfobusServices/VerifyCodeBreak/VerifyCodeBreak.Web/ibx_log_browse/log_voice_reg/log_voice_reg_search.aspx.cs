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

        //========== 1. 初始化界面 ==========
        //========== 2. 取出查询条件 ==========
        if (PageParameter.StringParam[0] != "log_voice_regNVAI")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. 刷新数据显示 ==========
        //========== 3.1 上传时间 ==========
        string sFromDate = sqlBuilder.GetConditionValueString("upload_time",
                SQLConditionOperator.GreaterEqualThan);
        sFromDate = DateTimeUtil.GetDatePart(sFromDate);
        DatePickerFrom.DateString = sFromDate;

        string sToDate = sqlBuilder.GetConditionValueString("upload_time",
                SQLConditionOperator.LessEqualThan);
        sToDate = DateTimeUtil.GetDatePart(sToDate);
        DatePickerTo.DateString = sToDate;

        //========== 3.2 第三方编码 ==========
        string sReqeustThirdId = sqlBuilder.GetConditionValueString("reqeust_third_id");
        edtReqeustThirdId.Text = sReqeustThirdId;

        //========== 3.3 本地文件 ==========
        string sVoiceFileLocal = sqlBuilder.GetConditionValueString("voice_file_local");
        edtVoiceFileLocal.Text = sVoiceFileLocal;

        //========== 3.4 上传文件 ==========
        string sVoiceFileUpload = sqlBuilder.GetConditionValueString("voice_file_upload");
        edtVoiceFileUpload.Text = sVoiceFileUpload;

        //========== 3.5 语音文件 ==========
        string sVoiceFileForIsr = sqlBuilder.GetConditionValueString("voice_file_for_isr");
        edtVoiceFileForIsr.Text = sVoiceFileForIsr;

        //========== 3.6 语法 ==========
        string sGrammarId = sqlBuilder.GetConditionValueString("grammar_id");
        edtGrammarId.Text = sGrammarId;

        //========== 3.7 客户端 ==========
        string sFromClientId = sqlBuilder.GetConditionValueString("from_client_id");
        edtFromClientId.Text = sFromClientId;

        //========== 3.8 当前状态 ==========
        string sCurrentStatus = sqlBuilder.GetConditionValueString("current_status");
        edtCurrentStatus.Text = sCurrentStatus;

        //========== 3.9 破解结果 ==========
        string sRegResultCode = sqlBuilder.GetConditionValueString("reg_result_code");
        edtRegResultCode.Text = sRegResultCode;

        //========== 3.10 失败原因 ==========
        string sRegFailDesc = sqlBuilder.GetConditionValueString("reg_fail_desc");
        edtRegFailDesc.Text = sRegFailDesc;

        //========== 3.11 识别文字 ==========
        string sRegResultText = sqlBuilder.GetConditionValueString("reg_result_text");
        edtRegResultText.Text = sRegResultText;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. 重新清空搜索条件 ==========
        if (PageParameter.StringParam[0] != "log_voice_regNVAI")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. 设置搜索条件 ==========
        //========== 2.1 上传时间 ==========
        string sFromDate = DatePickerFrom.DateString;
        if (sFromDate != "")
            sqlBuilder.AddCondition("upload_time", "上传时间",
                    DateTimeUtil.BeginTimeOfDay(sFromDate),
                    SQLConditionOperator.GreaterEqualThan,
                    "起始日期为" + DateTimeUtil.GetDatePart(sFromDate));

        string sToDate = DatePickerTo.DateString;
        if (sToDate != "")
            sqlBuilder.AddCondition("upload_time", "上传时间",
                    DateTimeUtil.EndTimeOfDay(sToDate),
                    SQLConditionOperator.LessEqualThan,
                    "结束日期为" + DateTimeUtil.GetDatePart(sToDate));

        //========== 2.2 第三方编码 ==========
        string sReqeustThirdId = edtReqeustThirdId.Text.Trim();
        if (sReqeustThirdId != "")
            sqlBuilder.AddCondition("reqeust_third_id", "第三方编码",
                    sReqeustThirdId, SQLConditionOperator.Like);

        //========== 2.3 本地文件 ==========
        string sVoiceFileLocal = edtVoiceFileLocal.Text.Trim();
        if (sVoiceFileLocal != "")
            sqlBuilder.AddCondition("voice_file_local", "本地文件",
                    sVoiceFileLocal, SQLConditionOperator.Like);

        //========== 2.4 上传文件 ==========
        string sVoiceFileUpload = edtVoiceFileUpload.Text.Trim();
        if (sVoiceFileUpload != "")
            sqlBuilder.AddCondition("voice_file_upload", "上传文件",
                    sVoiceFileUpload, SQLConditionOperator.Like);

        //========== 2.5 语音文件 ==========
        string sVoiceFileForIsr = edtVoiceFileForIsr.Text.Trim();
        if (sVoiceFileForIsr != "")
            sqlBuilder.AddCondition("voice_file_for_isr", "语音文件",
                    sVoiceFileForIsr, SQLConditionOperator.Like);

        //========== 2.6 语法 ==========
        string sGrammarId = edtGrammarId.Text.Trim();
        if (sGrammarId != "")
            sqlBuilder.AddCondition("grammar_id", "语法",
                    sGrammarId, SQLConditionOperator.Like);

        //========== 2.7 客户端 ==========
        string sFromClientId = edtFromClientId.Text.Trim();
        if (sFromClientId != "")
            sqlBuilder.AddCondition("from_client_id", "客户端",
                    sFromClientId, SQLConditionOperator.Like);

        //========== 2.8 当前状态 ==========
        string sCurrentStatus = edtCurrentStatus.Text.Trim();
        if (sCurrentStatus != "")
            sqlBuilder.AddCondition("current_status", "当前状态",
                    sCurrentStatus, SQLConditionOperator.Like);

        //========== 2.9 破解结果 ==========
        string sRegResultCode = edtRegResultCode.Text.Trim();
        if (sRegResultCode != "")
            sqlBuilder.AddCondition("reg_result_code", "破解结果",
                    sRegResultCode, SQLConditionOperator.Like);

        //========== 2.10 失败原因 ==========
        string sRegFailDesc = edtRegFailDesc.Text.Trim();
        if (sRegFailDesc != "")
            sqlBuilder.AddCondition("reg_fail_desc", "失败原因",
                    sRegFailDesc, SQLConditionOperator.Like);

        //========== 2.11 识别文字 ==========
        string sRegResultText = edtRegResultText.Text.Trim();
        if (sRegResultText != "")
            sqlBuilder.AddCondition("reg_result_text", "识别文字",
                    sRegResultText, SQLConditionOperator.Like);

        //========== 3. 返回到数据列表页面 ==========
        Response.Redirect("log_voice_reg_list.aspx");
    }
}

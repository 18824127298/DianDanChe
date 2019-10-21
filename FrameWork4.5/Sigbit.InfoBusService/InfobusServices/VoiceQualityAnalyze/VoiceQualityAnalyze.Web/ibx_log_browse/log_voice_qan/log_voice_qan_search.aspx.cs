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

        //========== 1. 初始化界面 ==========
        WCUComboBox.InitComboBox(ddlbCurrentStatus, EnumExUtil.ToCodeTable(TbLogVoiceQanECurrentStatus.None));
        ddlbCurrentStatus.Items.Add(new ListItem("[所有状态]", "ALL"));
        ddlbCurrentStatus.SelectedValue = "ALL";

        //========== 2. 取出查询条件 ==========
        if (PageParameter.StringParam[0] != "log_voice_qanFBSL")
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

        //========== 3.2 第三方标识 ==========
        string sReqeustThirdId = sqlBuilder.GetConditionValueString("reqeust_third_id");
        edtReqeustThirdId.Text = sReqeustThirdId;

        //========== 3.3 本地语音文件 ==========
        string sVoiceFileLocal = sqlBuilder.GetConditionValueString("voice_file_local");
        edtVoiceFileLocal.Text = sVoiceFileLocal;

        //========== 3.4 上传保存文件 ==========
        string sVoiceFileUpload = sqlBuilder.GetConditionValueString("voice_file_upload");
        edtVoiceFileUpload.Text = sVoiceFileUpload;

        //========== 3.5 分析语音文件 ==========
        string sVoiceFileForQan = sqlBuilder.GetConditionValueString("voice_file_for_qan");
        edtVoiceFileForQan.Text = sVoiceFileForQan;

        //========== 3.6 状态 ==========
        string sCurrentStatus = sqlBuilder.GetConditionValueString("current_status");
        if (sCurrentStatus != "")
            ddlbCurrentStatus.SelectedValue = sCurrentStatus;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. 重新清空搜索条件 ==========
        if (PageParameter.StringParam[0] != "log_voice_qanFBSL")
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

        //========== 2.2 第三方标识 ==========
        string sReqeustThirdId = edtReqeustThirdId.Text.Trim();
        if (sReqeustThirdId != "")
            sqlBuilder.AddCondition("reqeust_third_id", "第三方标识",
                    sReqeustThirdId, SQLConditionOperator.Like);

        //========== 2.3 本地语音文件 ==========
        string sVoiceFileLocal = edtVoiceFileLocal.Text.Trim();
        if (sVoiceFileLocal != "")
            sqlBuilder.AddCondition("voice_file_local", "本地语音文件",
                    sVoiceFileLocal, SQLConditionOperator.Like);

        //========== 2.4 上传保存文件 ==========
        string sVoiceFileUpload = edtVoiceFileUpload.Text.Trim();
        if (sVoiceFileUpload != "")
            sqlBuilder.AddCondition("voice_file_upload", "上传保存文件",
                    sVoiceFileUpload, SQLConditionOperator.Like);

        //========== 2.5 分析语音文件 ==========
        string sVoiceFileForQan = edtVoiceFileForQan.Text.Trim();
        if (sVoiceFileForQan != "")
            sqlBuilder.AddCondition("voice_file_for_qan", "分析语音文件",
                    sVoiceFileForQan, SQLConditionOperator.Like);

        //========== 2.6 状态 ==========
        string sCurrentStatus = ddlbCurrentStatus.SelectedValue;
        if (sCurrentStatus != "ALL")
            sqlBuilder.AddCondition("current_status", "状态", sCurrentStatus,
                    SQLConditionOperator.Equal, ddlbCurrentStatus.SelectedItem.Text);

        //========== 3. 返回到数据列表页面 ==========
        Response.Redirect("log_voice_qan_list.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("log_voice_qan_list.aspx");
    }
}

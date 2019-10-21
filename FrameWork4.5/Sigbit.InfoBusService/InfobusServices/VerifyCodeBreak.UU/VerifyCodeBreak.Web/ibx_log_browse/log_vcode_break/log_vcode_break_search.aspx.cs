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

        //========== 1. 初始化界面 ==========
        //========== 2. 取出查询条件 ==========
        if (PageParameter.StringParam[0] != "log_vcode_breakKTJQ")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. 刷新数据显示 ==========
        //========== 3.1 请求时间 ==========
        string sFromDate = sqlBuilder.GetConditionValueString("request_time",
                SQLConditionOperator.GreaterEqualThan);
        sFromDate = DateTimeUtil.GetDatePart(sFromDate);
        DatePickerFrom.DateString = sFromDate;

        string sToDate = sqlBuilder.GetConditionValueString("request_time",
                SQLConditionOperator.LessEqualThan);
        sToDate = DateTimeUtil.GetDatePart(sToDate);
        DatePickerTo.DateString = sToDate;

        //========== 3.2 图像文件 ==========
        string sImageFileName = sqlBuilder.GetConditionValueString("image_file_name");
        edtImageFileName.Text = sImageFileName;

        //========== 3.3 验证码类型 ==========
        string sVcodeId = sqlBuilder.GetConditionValueString("vcode_id");
        edtVcodeId.Text = sVcodeId;

        //========== 3.4 客户端 ==========
        string sFromClientId = sqlBuilder.GetConditionValueString("from_client_id");
        edtFromClientId.Text = sFromClientId;

        //========== 3.5 状态 ==========
        string sCurrentStatus = sqlBuilder.GetConditionValueString("current_status");
        edtCurrentStatus.Text = sCurrentStatus;

        //========== 3.6 破解结果 ==========
        string sBreakResult = sqlBuilder.GetConditionValueString("break_result");
        edtBreakResult.Text = sBreakResult;

        //========== 3.7 失败原因 ==========
        string sFailDesc = sqlBuilder.GetConditionValueString("fail_desc");
        edtFailDesc.Text = sFailDesc;

        //========== 3.8 验证码文字 ==========
        string sBreakText = sqlBuilder.GetConditionValueString("break_text");
        edtBreakText.Text = sBreakText;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. 重新清空搜索条件 ==========
        if (PageParameter.StringParam[0] != "log_vcode_breakKTJQ")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. 设置搜索条件 ==========
        //========== 2.1 请求时间 ==========
        string sFromDate = DatePickerFrom.DateString;
        if (sFromDate != "")
            sqlBuilder.AddCondition("request_time", "请求时间",
                    DateTimeUtil.BeginTimeOfDay(sFromDate),
                    SQLConditionOperator.GreaterEqualThan,
                    "起始日期为" + DateTimeUtil.GetDatePart(sFromDate));

        string sToDate = DatePickerTo.DateString;
        if (sToDate != "")
            sqlBuilder.AddCondition("request_time", "请求时间",
                    DateTimeUtil.EndTimeOfDay(sToDate),
                    SQLConditionOperator.LessEqualThan,
                    "结束日期为" + DateTimeUtil.GetDatePart(sToDate));

        //========== 2.2 图像文件 ==========
        string sImageFileName = edtImageFileName.Text.Trim();
        if (sImageFileName != "")
            sqlBuilder.AddCondition("image_file_name", "图像文件",
                    sImageFileName, SQLConditionOperator.Like);

        //========== 2.3 验证码类型 ==========
        string sVcodeId = edtVcodeId.Text.Trim();
        if (sVcodeId != "")
            sqlBuilder.AddCondition("vcode_id", "验证码类型",
                    sVcodeId, SQLConditionOperator.Like);

        //========== 2.4 客户端 ==========
        string sFromClientId = edtFromClientId.Text.Trim();
        if (sFromClientId != "")
            sqlBuilder.AddCondition("from_client_id", "客户端",
                    sFromClientId, SQLConditionOperator.Like);

        //========== 2.5 状态 ==========
        string sCurrentStatus = edtCurrentStatus.Text.Trim();
        if (sCurrentStatus != "")
            sqlBuilder.AddCondition("current_status", "状态",
                    sCurrentStatus, SQLConditionOperator.Like);

        //========== 2.6 破解结果 ==========
        string sBreakResult = edtBreakResult.Text.Trim();
        if (sBreakResult != "")
            sqlBuilder.AddCondition("break_result", "破解结果",
                    sBreakResult, SQLConditionOperator.Like);

        //========== 2.7 失败原因 ==========
        string sFailDesc = edtFailDesc.Text.Trim();
        if (sFailDesc != "")
            sqlBuilder.AddCondition("fail_desc", "失败原因",
                    sFailDesc, SQLConditionOperator.Like);

        //========== 2.8 验证码文字 ==========
        string sBreakText = edtBreakText.Text.Trim();
        if (sBreakText != "")
            sqlBuilder.AddCondition("break_text", "验证码文字",
                    sBreakText, SQLConditionOperator.Like);

        //========== 3. 返回到数据列表页面 ==========
        Response.Redirect("log_vcode_break_list.aspx");
    }
}

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

public partial class genui_VIZP_log_tcp_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. 初始化界面 ==========

        //========== 2. 取出查询条件 ==========
        if (PageParameter.StringParam[0] != "log_tcpVIZP")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. 刷新数据显示 ==========
        //========== 3.1 命令字及消息名称 ==========
        string sCommandIdEng = sqlBuilder.GetConditionValueString("trans_code_eng");
        if (sCommandIdEng != "")
            edtCommandIdEng.Text = sCommandIdEng;

        string sCommandIdChs = sqlBuilder.GetConditionValueString("trans_code_chs");
        edtCommandIdChs.Text = sCommandIdChs;

        //========== 3.4 消息时间 ==========
        string sFromDate = sqlBuilder.GetConditionValueString("request_time",
                SQLConditionOperator.GreaterEqualThan);
        sFromDate = DateTimeUtil.GetDatePart(sFromDate);
        DatePickerFrom.DateString = sFromDate;

        string sToDate = sqlBuilder.GetConditionValueString("request_time",
                SQLConditionOperator.LessEqualThan);
        sToDate = DateTimeUtil.GetDatePart(sToDate);
        DatePickerTo.DateString = sToDate;

        //========== 3.5 请求数据包 ==========
        string sRequestPacket = sqlBuilder.GetConditionValueString("request_packet");
        edtRequestPacket.Text = sRequestPacket;

        //========== 3.6 响应数据包 ==========
        string sResponsePacket = sqlBuilder.GetConditionValueString("response_packet");
        edtResponsePacket.Text = sResponsePacket;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. 重新清空搜索条件 ==========
        if (PageParameter.StringParam[0] != "log_tcpVIZP")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. 设置搜索条件 ==========
        //========== 2.1 命令字及消息名称 ==========
        string sCommandIdEng = edtCommandIdEng.Text.Trim();
        if (sCommandIdEng != "")
            sqlBuilder.AddCondition("trans_code_eng", "交易码",
                    sCommandIdEng, SQLConditionOperator.Like);

        string sCommandIdChs = edtCommandIdChs.Text.Trim();
        if (sCommandIdChs != "")
            sqlBuilder.AddCondition("trans_code_chs", "消息名称",
                    sCommandIdChs, SQLConditionOperator.Like);

        //========== 2.4 发送时间 ==========
        string sFromDate = DatePickerFrom.DateString;
        if (sFromDate != "")
            sqlBuilder.AddCondition("request_time", "消息时间",
                    DateTimeUtil.BeginTimeOfDay(sFromDate),
                    SQLConditionOperator.GreaterEqualThan,
                    "起始日期为" + DateTimeUtil.GetDatePart(sFromDate));

        string sToDate = DatePickerTo.DateString;
        if (sToDate != "")
            sqlBuilder.AddCondition("request_time", "消息时间",
                    DateTimeUtil.EndTimeOfDay(sToDate),
                    SQLConditionOperator.LessEqualThan,
                    "结束日期为" + DateTimeUtil.GetDatePart(sToDate));

        //========== 2.5 请求数据包 ==========
        string sRequestPacket = edtRequestPacket.Text.Trim();
        if (sRequestPacket != "")
            sqlBuilder.AddCondition("request_packet", "请求数据包", sRequestPacket,
                    SQLConditionOperator.Like);

        //========== 2.6 响应数据包 ==========
        string sResponsePacket = edtResponsePacket.Text.Trim();
        if (sResponsePacket != "")
            sqlBuilder.AddCondition("response_packet", "响应数据包", sResponsePacket,
                    SQLConditionOperator.Like);

        //========== 3. 返回到数据列表页面 ==========
        Response.Redirect("log_tcp_list.aspx");
    }
}

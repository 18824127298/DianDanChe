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

        //========== 1. ��ʼ������ ==========

        //========== 2. ȡ����ѯ���� ==========
        if (PageParameter.StringParam[0] != "log_tcpVIZP")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. ˢ��������ʾ ==========
        //========== 3.1 �����ּ���Ϣ���� ==========
        string sCommandIdEng = sqlBuilder.GetConditionValueString("trans_code_eng");
        if (sCommandIdEng != "")
            edtCommandIdEng.Text = sCommandIdEng;

        string sCommandIdChs = sqlBuilder.GetConditionValueString("trans_code_chs");
        edtCommandIdChs.Text = sCommandIdChs;

        //========== 3.4 ��Ϣʱ�� ==========
        string sFromDate = sqlBuilder.GetConditionValueString("request_time",
                SQLConditionOperator.GreaterEqualThan);
        sFromDate = DateTimeUtil.GetDatePart(sFromDate);
        DatePickerFrom.DateString = sFromDate;

        string sToDate = sqlBuilder.GetConditionValueString("request_time",
                SQLConditionOperator.LessEqualThan);
        sToDate = DateTimeUtil.GetDatePart(sToDate);
        DatePickerTo.DateString = sToDate;

        //========== 3.5 �������ݰ� ==========
        string sRequestPacket = sqlBuilder.GetConditionValueString("request_packet");
        edtRequestPacket.Text = sRequestPacket;

        //========== 3.6 ��Ӧ���ݰ� ==========
        string sResponsePacket = sqlBuilder.GetConditionValueString("response_packet");
        edtResponsePacket.Text = sResponsePacket;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. ��������������� ==========
        if (PageParameter.StringParam[0] != "log_tcpVIZP")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. ������������ ==========
        //========== 2.1 �����ּ���Ϣ���� ==========
        string sCommandIdEng = edtCommandIdEng.Text.Trim();
        if (sCommandIdEng != "")
            sqlBuilder.AddCondition("trans_code_eng", "������",
                    sCommandIdEng, SQLConditionOperator.Like);

        string sCommandIdChs = edtCommandIdChs.Text.Trim();
        if (sCommandIdChs != "")
            sqlBuilder.AddCondition("trans_code_chs", "��Ϣ����",
                    sCommandIdChs, SQLConditionOperator.Like);

        //========== 2.4 ����ʱ�� ==========
        string sFromDate = DatePickerFrom.DateString;
        if (sFromDate != "")
            sqlBuilder.AddCondition("request_time", "��Ϣʱ��",
                    DateTimeUtil.BeginTimeOfDay(sFromDate),
                    SQLConditionOperator.GreaterEqualThan,
                    "��ʼ����Ϊ" + DateTimeUtil.GetDatePart(sFromDate));

        string sToDate = DatePickerTo.DateString;
        if (sToDate != "")
            sqlBuilder.AddCondition("request_time", "��Ϣʱ��",
                    DateTimeUtil.EndTimeOfDay(sToDate),
                    SQLConditionOperator.LessEqualThan,
                    "��������Ϊ" + DateTimeUtil.GetDatePart(sToDate));

        //========== 2.5 �������ݰ� ==========
        string sRequestPacket = edtRequestPacket.Text.Trim();
        if (sRequestPacket != "")
            sqlBuilder.AddCondition("request_packet", "�������ݰ�", sRequestPacket,
                    SQLConditionOperator.Like);

        //========== 2.6 ��Ӧ���ݰ� ==========
        string sResponsePacket = edtResponsePacket.Text.Trim();
        if (sResponsePacket != "")
            sqlBuilder.AddCondition("response_packet", "��Ӧ���ݰ�", sResponsePacket,
                    SQLConditionOperator.Like);

        //========== 3. ���ص������б�ҳ�� ==========
        Response.Redirect("log_tcp_list.aspx");
    }
}

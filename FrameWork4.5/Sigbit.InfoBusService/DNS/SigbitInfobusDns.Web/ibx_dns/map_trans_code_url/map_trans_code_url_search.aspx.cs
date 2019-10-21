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
using Sigbit.App.Net.IBXService.DNS.Service.DBDefine;

public partial class genui_HUTL_map_trans_code_url_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. ��ʼ������ ==========
        WCUComboBox.InitComboBox(ddlbServiceId, QDBDnsPools.PoolService.ServiceCodeTable);
        ddlbServiceId.Items.Add(new ListItem("[����Ѱַ����]", "ALL"));
        ddlbServiceId.SelectedValue = "ALL";

        WCUComboBox.InitComboBox(ddlbTransCode, QDBDnsPools.PoolTransCode.TransCodeCodeTable);
        ddlbTransCode.Items.Add(new ListItem("[���н�����]", "ALL"));
        ddlbTransCode.SelectedValue = "ALL";

        WCUComboBox.InitComboBox(ddlbUrlAddressUid, QDBDnsPools.PoolUrlAddress.UrlAddressCodeTable);
        ddlbUrlAddressUid.Items.Add(new ListItem("[���з����ַ]", "ALL"));
        ddlbUrlAddressUid.SelectedValue = "ALL";

        //========== 2. ȡ����ѯ���� ==========
        if (PageParameter.StringParam[0] != "map_trans_code_urlHUTL")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. ˢ��������ʾ ==========
        //========== 3.1 Ѱַ���� ==========
        string sServiceId = sqlBuilder.GetConditionValueString("service_id");
        if (sServiceId != "")
            ddlbServiceId.SelectedValue = sServiceId;

        //========== 3.2 ������ ==========
        string sTransCode = sqlBuilder.GetConditionValueString("trans_code");
        if (sTransCode != "")
            ddlbTransCode.SelectedValue = sTransCode;

        //========== 3.3 �����ַ ==========
        string sUrlAddressUid = sqlBuilder.GetConditionValueString("url_address_uid");
        if (sUrlAddressUid != "")
            ddlbUrlAddressUid.SelectedValue = sUrlAddressUid;

        //========== 3.4 �ͻ��˱�ʶ/�汾/ϵͳ ==========
        string sFromClientId = sqlBuilder.GetConditionValueString("from_client_id");
        edtFromClientId.Text = sFromClientId;

        //========== 3.5 �ͻ��˰汾 ==========
        string sFromClientVersion = sqlBuilder.GetConditionValueString("from_client_version");
        edtFromClientVersion.Text = sFromClientVersion;

        //========== 3.6 �ͻ���ϵͳ ==========
        string sFromSystem = sqlBuilder.GetConditionValueString("from_system");
        edtFromSystem.Text = sFromSystem;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. ��������������� ==========
        if (PageParameter.StringParam[0] != "map_trans_code_urlHUTL")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. ������������ ==========
        //========== 2.1 Ѱַ���� ==========
        string sServiceId = ddlbServiceId.SelectedValue;
        if (sServiceId != "ALL")
            sqlBuilder.AddCondition("service_id", "Ѱַ����", sServiceId,
                    SQLConditionOperator.Equal, ddlbServiceId.SelectedItem.Text);

        //========== 2.2 ������ ==========
        string sTransCode = ddlbTransCode.SelectedValue;
        if (sTransCode != "ALL")
            sqlBuilder.AddCondition("trans_code", "������", sTransCode,
                    SQLConditionOperator.Equal, ddlbTransCode.SelectedItem.Text);

        //========== 2.3 �����ַ ==========
        string sUrlAddressUid = ddlbUrlAddressUid.SelectedValue;
        if (sUrlAddressUid != "ALL")
            sqlBuilder.AddCondition("url_address_uid", "�����ַ", sUrlAddressUid,
                    SQLConditionOperator.Equal, ddlbUrlAddressUid.SelectedItem.Text);

        //========== 2.4 �ͻ��˱�ʶ/�汾/ϵͳ ==========
        string sFromClientId = edtFromClientId.Text.Trim();
        if (sFromClientId != "")
            sqlBuilder.AddCondition("from_client_id", "�ͻ��˱�ʶ",
                    sFromClientId, SQLConditionOperator.Like);

        //========== 2.5 �ͻ��˰汾 ==========
        string sFromClientVersion = edtFromClientVersion.Text.Trim();
        if (sFromClientVersion != "")
            sqlBuilder.AddCondition("from_client_version", "�ͻ��˰汾",
                    sFromClientVersion, SQLConditionOperator.Like);

        //========== 2.6 �ͻ���ϵͳ ==========
        string sFromSystem = edtFromSystem.Text.Trim();
        if (sFromSystem != "")
            sqlBuilder.AddCondition("from_system", "�ͻ���ϵͳ",
                    sFromSystem, SQLConditionOperator.Like);

        //========== 3. ���ص������б�ҳ�� ==========
        Response.Redirect("map_trans_code_url_list.aspx");
    }
}

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

        //========== 1. 初始化界面 ==========
        WCUComboBox.InitComboBox(ddlbServiceId, QDBDnsPools.PoolService.ServiceCodeTable);
        ddlbServiceId.Items.Add(new ListItem("[所有寻址服务]", "ALL"));
        ddlbServiceId.SelectedValue = "ALL";

        WCUComboBox.InitComboBox(ddlbTransCode, QDBDnsPools.PoolTransCode.TransCodeCodeTable);
        ddlbTransCode.Items.Add(new ListItem("[所有交易码]", "ALL"));
        ddlbTransCode.SelectedValue = "ALL";

        WCUComboBox.InitComboBox(ddlbUrlAddressUid, QDBDnsPools.PoolUrlAddress.UrlAddressCodeTable);
        ddlbUrlAddressUid.Items.Add(new ListItem("[所有服务地址]", "ALL"));
        ddlbUrlAddressUid.SelectedValue = "ALL";

        //========== 2. 取出查询条件 ==========
        if (PageParameter.StringParam[0] != "map_trans_code_urlHUTL")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. 刷新数据显示 ==========
        //========== 3.1 寻址服务 ==========
        string sServiceId = sqlBuilder.GetConditionValueString("service_id");
        if (sServiceId != "")
            ddlbServiceId.SelectedValue = sServiceId;

        //========== 3.2 交易码 ==========
        string sTransCode = sqlBuilder.GetConditionValueString("trans_code");
        if (sTransCode != "")
            ddlbTransCode.SelectedValue = sTransCode;

        //========== 3.3 服务地址 ==========
        string sUrlAddressUid = sqlBuilder.GetConditionValueString("url_address_uid");
        if (sUrlAddressUid != "")
            ddlbUrlAddressUid.SelectedValue = sUrlAddressUid;

        //========== 3.4 客户端标识/版本/系统 ==========
        string sFromClientId = sqlBuilder.GetConditionValueString("from_client_id");
        edtFromClientId.Text = sFromClientId;

        //========== 3.5 客户端版本 ==========
        string sFromClientVersion = sqlBuilder.GetConditionValueString("from_client_version");
        edtFromClientVersion.Text = sFromClientVersion;

        //========== 3.6 客户端系统 ==========
        string sFromSystem = sqlBuilder.GetConditionValueString("from_system");
        edtFromSystem.Text = sFromSystem;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. 重新清空搜索条件 ==========
        if (PageParameter.StringParam[0] != "map_trans_code_urlHUTL")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. 设置搜索条件 ==========
        //========== 2.1 寻址服务 ==========
        string sServiceId = ddlbServiceId.SelectedValue;
        if (sServiceId != "ALL")
            sqlBuilder.AddCondition("service_id", "寻址服务", sServiceId,
                    SQLConditionOperator.Equal, ddlbServiceId.SelectedItem.Text);

        //========== 2.2 交易码 ==========
        string sTransCode = ddlbTransCode.SelectedValue;
        if (sTransCode != "ALL")
            sqlBuilder.AddCondition("trans_code", "交易码", sTransCode,
                    SQLConditionOperator.Equal, ddlbTransCode.SelectedItem.Text);

        //========== 2.3 服务地址 ==========
        string sUrlAddressUid = ddlbUrlAddressUid.SelectedValue;
        if (sUrlAddressUid != "ALL")
            sqlBuilder.AddCondition("url_address_uid", "服务地址", sUrlAddressUid,
                    SQLConditionOperator.Equal, ddlbUrlAddressUid.SelectedItem.Text);

        //========== 2.4 客户端标识/版本/系统 ==========
        string sFromClientId = edtFromClientId.Text.Trim();
        if (sFromClientId != "")
            sqlBuilder.AddCondition("from_client_id", "客户端标识",
                    sFromClientId, SQLConditionOperator.Like);

        //========== 2.5 客户端版本 ==========
        string sFromClientVersion = edtFromClientVersion.Text.Trim();
        if (sFromClientVersion != "")
            sqlBuilder.AddCondition("from_client_version", "客户端版本",
                    sFromClientVersion, SQLConditionOperator.Like);

        //========== 2.6 客户端系统 ==========
        string sFromSystem = edtFromSystem.Text.Trim();
        if (sFromSystem != "")
            sqlBuilder.AddCondition("from_system", "客户端系统",
                    sFromSystem, SQLConditionOperator.Like);

        //========== 3. 返回到数据列表页面 ==========
        Response.Redirect("map_trans_code_url_list.aspx");
    }
}

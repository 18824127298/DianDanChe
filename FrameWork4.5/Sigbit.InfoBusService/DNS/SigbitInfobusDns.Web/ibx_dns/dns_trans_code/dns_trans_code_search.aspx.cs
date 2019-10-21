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

public partial class genui_LNWX_dns_trans_code_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. 初始化界面 ==========
        //WCUComboBox.InitComboBox(ddlbServiceId, DBDefineCodeTables.ServiceId);
        //ddlbServiceId.Items.Add(new ListItem("[所有寻址服务]", "ALL"));
        //ddlbServiceId.SelectedValue = "ALL";

        //========== 2. 取出查询条件 ==========
        if (PageParameter.StringParam[0] != "dns_trans_codeLNWX")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. 刷新数据显示 ==========
        //========== 3.1 交易码 ==========
        string sTransCode = sqlBuilder.GetConditionValueString("trans_code");
        edtTransCode.Text = sTransCode;

        //========== 3.2 名称 ==========
        string sTransCodeName = sqlBuilder.GetConditionValueString("trans_code_name");
        edtTransCodeName.Text = sTransCodeName;

        //========== 3.3 寻址服务 ==========
        string sServiceId = sqlBuilder.GetConditionValueString("service_id");
        if (sServiceId != "")
            ddlbServiceId.SelectedValue = sServiceId;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. 重新清空搜索条件 ==========
        if (PageParameter.StringParam[0] != "dns_trans_codeLNWX")
            return;
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. 设置搜索条件 ==========
        //========== 2.1 交易码 ==========
        string sTransCode = edtTransCode.Text.Trim();
        if (sTransCode != "")
            sqlBuilder.AddCondition("trans_code", "交易码",
                    sTransCode, SQLConditionOperator.Like);

        //========== 2.2 名称 ==========
        string sTransCodeName = edtTransCodeName.Text.Trim();
        if (sTransCodeName != "")
            sqlBuilder.AddCondition("trans_code_name", "名称",
                    sTransCodeName, SQLConditionOperator.Like);

        //========== 2.3 寻址服务 ==========
        string sServiceId = ddlbServiceId.SelectedValue;
        if (sServiceId != "ALL")
            sqlBuilder.AddCondition("service_id", "寻址服务", sServiceId,
                    SQLConditionOperator.Equal, ddlbServiceId.SelectedItem.Text);

        //========== 3. 返回到数据列表页面 ==========
        Response.Redirect("dns_trans_code_list.aspx");
    }
}

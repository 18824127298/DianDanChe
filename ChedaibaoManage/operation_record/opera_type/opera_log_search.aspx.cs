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
using Sigbit.App.PTP.DBDefine;

public partial class genui_UZAY_opera_log_search : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //========== 1. 初始化界面 ==========
        //========== 2. 取出查询条件 ==========
        if (PageParameter.StringParam[0] != "opera_logUZAY")
            return;

        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];

        //========== 3. 刷新数据显示 ==========
        //========== 3.1 用户 ==========
        //string nGodid = sqlBuilder.GetConditionValueString("GodId");
        //edtGodid.Text = nGodid;

        ////========== 3.2 备注 ==========
        //string sRemark = sqlBuilder.GetConditionValueString("Remark");
        //edtRemark.Text = sRemark;

    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //========== 1. 重新清空搜索条件 ==========
        if (PageParameter.StringParam[0] != "opera_logUZAY")
            return;
        int nTypeId = ConvertUtil.ToInt(Request["type_id"]);
        SQLBuilder sqlBuilder = (SQLBuilder)PageParameter.ObjParam[0];
        sqlBuilder.ClearConditions();

        //========== 2. 设置搜索条件 ==========
        //========== 2.1 用户 ==========
        int nGodid = ConvertUtil.ToInt(ddlbGodName.SelectedValue);
        if (nGodid != 0)
            sqlBuilder.AddCondition("GodId", "用户",
                    nGodid, SQLConditionOperator.Equal);

        //========== 2.2 备注 ==========
        string sRemark = edtRemark.Text.Trim();
        if (sRemark != "")
            sqlBuilder.AddCondition("Remark", "备注",
                    sRemark, SQLConditionOperator.Like);

        //========== 3. 返回到数据列表页面 ==========
        Response.Redirect("opera_log_list.aspx?type_id=" + nTypeId);
    }

    protected void btnHuoQu_Click(object sender, EventArgs e)
    {
        string sGodName = edtGodid.Text.Trim();

        string sSQL = string.Format("select * from God where Aliases like '%{0}%'", sGodName);
        DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

        ddlbGodName.DataSource = ds.Tables[0].DefaultView;
        ddlbGodName.DataTextField = "Aliases";
        ddlbGodName.DataValueField = "Id";
        ddlbGodName.DataBind();
        ddlbGodName.Items.Insert(0, "请选择");
        ddlbGodName.SelectedValue = "请选择";
    }
}

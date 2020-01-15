using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class youka_supplier_supplier_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //========== 0. 绑定DataGrid和PageControl的关系 ===========
        gridViewPager.GridViewControl = gvList;
        gridViewPager.PageIndexChanged
            = new module_WebUserControl.PageIndexChangedHandler(gvList__PageIndexChanged);

        //========= 1. 如果首次进来，则取出前次的状态 ===========
        if (!IsPostBack)
        {
            CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                       = @"select * from Supplier where IsValid = 1";

            if (CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql == "")
            {
                CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("CreateTime");
            }
            else
            {
                gvList.PageSize = CurrentPageStatus.DataViewStatus.PageSize;
                gvList.PageIndex = CurrentPageStatus.DataViewStatus.CurrentPageIndex;
            }

            CurrentPageStatus.DataViewStatus.SqlBuilder.ClearFixConditions();

            FetchDataFromDB();
            gridViewPager.ShowPageInfo();

            NaviTabController.ShowTabBar();
        }
    }

    private void gvList__PageIndexChanged(int nNewPageIndex)
    {
        gvList.PageIndex = nNewPageIndex;
        FetchDataFromDB();
    }

    private void FetchDataFromDB()
    {
        //========= 1. 取出数据，并绑定 ========
        DataSet ds = DataHelper.Instance.ExecuteDataSet
                (CurrentPageStatus.DataViewStatus.SqlBuilder.ToString());


        gvList.DataSource = ds;
        gvList.DataBind();

        //========== 2. 保存当前状态 ==============
        CurrentPageStatus.DataViewStatus.PageSize = gvList.PageSize;
        CurrentPageStatus.DataViewStatus.CurrentPageIndex = gvList.PageIndex;
    }

    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField(e.SortExpression);
        gridViewPager.RefreshGridView();
    }

    protected void btnClearCondition_Click(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = @"select * from Supplier where IsValid = 1 ";
        if (edtCreditName.Text.Trim() != "")
        {
            sql += " and FullName = " + StringUtil.QuotedToDBStr(edtCreditName.Text);
        }

        if (edtCreditPhone.Text.Trim() != "")
        {
            sql += " and Phone= " + StringUtil.QuotedToDBStr(edtCreditPhone.Text);
        }
        CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql = sql;
        FetchDataFromDB();
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        Response.Redirect("../supplier/supplier_insert.aspx");
    }

    protected string Recharge(object oId)
    {
        int nId = ConvertUtil.ToInt(oId);
        string sRet = "<a href=\"../supplier/supplier_recharge.aspx?Id=" + oId.ToString() + "\"><image src='../../images/menu_icon/erp.gif' title='充值' /></a>";
        return sRet;
    }

    protected string Name(object oName, object oId)
    {
        string sRet = "<a href=\"../supplier/supplier_update.aspx?id=" + oId.ToString() + "\">" + oName.ToString() + "</a>";
        return sRet;
    }

    protected string RechargeList(object oId)
    {
        int nId = ConvertUtil.ToInt(oId);
        string sRet = "<a href=\"../supplier/supplier_recharge_list.aspx?Id=" + oId.ToString() + "\"><image src='../../images/menu_icon/erp.gif' title='记录' /></a>";
        return sRet;
    }
}
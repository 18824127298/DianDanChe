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

public partial class youka_gasstation_gasstation_list : SbtPageBase
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
                       = @"select g.*, s.Name as SupplierName from GasStation g join Supplier s on g.SupplierId = s.Id where g.IsValid = 1";
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

    protected void Insert_Click(object sender, EventArgs e)
    {
        Response.Redirect("../gasstation/gasstation_insert.aspx");
    }
    protected string VIVYouHui(object oId)
    {
        int nId = ConvertUtil.ToInt(oId);
        string sRet = "<a href=\"../gasstationlevel/gasstationlevel_list.aspx?Id=" + oId.ToString() + "\"><image src='../../images/menu_icon/erp.gif' title='查看' /></a>";
        return sRet;
    }

    protected string VIVPeiZhi(object oId)
    {
        int nId = ConvertUtil.ToInt(oId);
        string sRet = "<a href=\"../oilgun/oilgun_list.aspx?Id=" + oId.ToString() + "\"><image src='../../images/menu_icon/erp.gif' title='查看' /></a>";
        return sRet;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (CountryMarkPrice.Text == "")
        {
            Response.Write("<script language='javascript'>alert('国标价不能为空！')</script>");
            return;
        }
        if (NewCountryPrice.Text == "")
        {
            Response.Write("<script language='javascript'>alert('新的国标价不能为空！')</script>");
            return;
        }
        if (CountryPointTime.DateTimeString == "")
        {
            Response.Write("<script language='javascript'>alert('国标价时间点不能为空！')</script>");
            return;
        }
        string sql = @"update OilGun set CountryMarkPrice = " + CountryMarkPrice.Text + @", NewCountryPrice = " + NewCountryPrice.Text
            + @", CountryPointTime = " + StringUtil.QuotedToDBStr(CountryPointTime.DateTimeString) + @" where IsValid= 1 and  OilNumber = " + StringUtil.QuotedToDBStr(ddlOilNumber.SelectedValue);
        DataHelper.Instance.ExecuteNonQuery(sql);
        Response.Write("<script language='javascript'>alert('已修改成功！')</script>");
        return;
    }
}
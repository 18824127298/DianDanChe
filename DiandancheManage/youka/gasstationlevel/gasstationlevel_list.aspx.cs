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

public partial class youka_gasstationlevel_gasstationlevel_list : SbtPageBase
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
            int nId = ConvertUtil.ToInt(Request["id"]);

            if (nId != 0)
            {
                PageParameter.SetCustomParamObject("id", nId);
            }
        
            CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                       = @"select g.Name, gl.* from GasStationLevel gl join GasStation g on gl.GasStationId = g.Id where gl.IsValid = 1 and g.Id =" + nId;
            if (CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql == "")
            {
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
        Response.Redirect("../gasstationlevel/gasstationlevel_insert.aspx?Id=" + ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id")));
    }

    protected string Amount(object oAmount)
    {
        decimal dAmount = ConvertUtil.ToDecimal(oAmount);
        string sRet = dAmount.ToString("N2");
        return sRet;
    }


    protected string Name(object oName, object oId)
    {
        int nId = ConvertUtil.ToInt(oId);
        string sRet = "<a href=\"../gasstationlevel/gasstationlevel_update.aspx?id=" + oId.ToString() + "&lId=" + ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id")) + "\">" + oName.ToString() + "</a>";
        return sRet;
    }

}
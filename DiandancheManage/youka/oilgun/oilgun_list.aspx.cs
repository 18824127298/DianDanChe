using CheDaiBaoCommonService.Data;
using CheDaiBaoWeChatModel;
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

public partial class youka_oilgun_oilgun_list : SbtPageBase
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
                       = @"select g.Id as gId, g.Name,o.OilNumber,o.Amount,o.NewAmount, o.CountryMarkPrice, o.NewCountryPrice, o.PointTime, o.CountryPointTime
 from OilGun o join GasStation g on o.GasStationId = g.Id where o.IsValid= 1 and g.Id = " + nId + @" group by g.Id, 
 g.Name,o.OilNumber,o.Amount,o.NewAmount, o.CountryMarkPrice, o.NewCountryPrice, o.PointTime, o.CountryPointTime";
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
        Response.Redirect("../oilgun/oilgun_insert.aspx?id=" + ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id")));
    }

    protected string Amount(object oAmount)
    {
        decimal dAmount = ConvertUtil.ToDecimal(oAmount);
        string sRet = dAmount.ToString("N2");
        return sRet;
    }
    protected string Name(object oName, object gId, object OilNumber)
    {
        string sRet = "<a href=\"../oilgun/oilgun_update.aspx?id=" + gId.ToString() + "&lId=" + ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id")) + "&oilne=" + OilNumber.ToString().Split('#')[0] + "&oilnr=" + OilNumber.ToString().Split('#')[1] + "\">" + oName.ToString() + "</a>";
        return sRet;
    }

    protected string GunNumber(object gId, object OilNumber)
    {
        string sql = "select GunNumber from OilGun where IsValid= 1 and GasStationId = " + ConvertUtil.ToInt(gId) + " and OilNumber= " + StringUtil.QuotedToDBStr(OilNumber);
        List<Int32?> lstGunNumber = SqlConnections.GetOpenConnection().Query<Int32?>(sql).ToList();
        string GunNumber = "";
        foreach (Int32? sGunNumber in lstGunNumber)
        {
            if (string.IsNullOrEmpty(sGunNumber.ToString()))
            {
                return "";
            }
            GunNumber += sGunNumber.ToString() + ",";
        }
        return GunNumber.TrimEnd(',');
    }


}
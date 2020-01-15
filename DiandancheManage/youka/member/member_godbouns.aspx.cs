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

public partial class youka_member_member_godbouns : SbtPageBase
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
                       = @"select g.Name as gname, g.CreateTime, g.BounsAmount,  g.BounsStatus,  m.Phone, mr.Phone as RecommendPhone from GodBouns g left join Member m 
                           on g.MemberId = m.Id left join Member mr on m.RecommendId = mr.Id where g.IsValid= 1 and g.IsValid= 1";
            if (CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql == "")
            {
            }
            else
            {
                gvList.PageSize = CurrentPageStatus.DataViewStatus.PageSize;
                gvList.PageIndex = CurrentPageStatus.DataViewStatus.CurrentPageIndex;
            }


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


    protected string VIVBounsStatus(object oBounsStatus)
    {
        int nBounsStatus = ConvertUtil.ToInt(oBounsStatus);
        return ((BounsStatus)nBounsStatus).ToString();
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = @"select g.Name as gname, g.CreateTime, g.BounsAmount,  g.BounsStatus,  m.Phone, mr.Phone as RecommendPhone from GodBouns g left join Member m 
                           on g.MemberId = m.Id left join Member mr on m.RecommendId = mr.Id where g.IsValid= 1 and g.IsValid= 1";
        if (edtPhone.Text != "")
        {
            sql += " and m.Phone = " + StringUtil.QuotedToDBStr(edtPhone.Text);
        }

        CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql = sql;
        FetchDataFromDB();
        gridViewPager.ShowPageInfo();

        NaviTabController.ShowTabBar();
    }
}
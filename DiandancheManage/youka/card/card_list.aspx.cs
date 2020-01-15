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

public partial class youka_card_card_list : SbtPageBase
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
                       = @"select c.*, a.Name as AgentName,m.FullName as memberName, s.Number as SupplierNumber from Card c left join Agent a on c.AgentId = a.Id 
left join Member m on c.MemberId = m.Id left join Supplier s on c.SupplierId = s.Id
 where c.IsValid = 1";
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
        Response.Redirect("../card/card_insert.aspx");
    }

    protected string VIVCardStatus(object oCardStatus)
    {
        int nCardStatus = ConvertUtil.ToInt(oCardStatus);

        return ((CardStatus)nCardStatus).ToString();
    }

    protected string VIVIsRecharge(object oIsRecharge)
    {
        Boolean? bIsFree = ConvertUtil.ToBool(oIsRecharge);
        if (bIsFree == true)
            return "可充值";
        else
            return "不可充";
    }

    protected string VIVCardBrand(object oCardBrand)
    {
        int nCardBrand = ConvertUtil.ToInt(oCardBrand);

        return ((CardBrand)nCardBrand).ToString();
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (CardNumber.Text == "")
        {
            Response.Write("<script language='javascript'>alert('卡号不能为空！')</script>");
            return;
        }
        string sql = @"select c.*, a.Name as AgentName,m.FullName as memberName, s.Number as SupplierNumber from Card c left join Agent a on c.AgentId = a.Id 
left join Member m on c.MemberId = m.Id left join Supplier s on c.SupplierId = s.Id
 where c.IsValid = 1 and c.CardNumber=" + StringUtil.QuotedToDBStr(CardNumber.Text);
        CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql = sql;
        FetchDataFromDB();
    }
}
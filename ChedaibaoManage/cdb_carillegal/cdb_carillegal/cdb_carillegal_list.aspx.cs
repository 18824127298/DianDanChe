using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using Sigbit.App.PTP.Web;
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

public partial class cdb_carillegal_cdb_carillegal_cdb_carillegal_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //========== 0. 绑定DataGrid和PageControl的关系 ===========
        gridViewPager.GridViewControl = gvList;
        gridViewPager.PageIndexChanged
            = new module_WebUserControl.PageIndexChangedHandler(gvList__PageIndexChanged);

        if (IsPostBack)
            return;

        //========= 1. 如果首次进来，则取出前次的状态 ===========
        if (!IsPostBack)
        {
            CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                    = "select * from CarIllegal where IsValid= 1";
            FetchDataFromDB();
            gridViewPager.ShowPageInfo();
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

    protected string VIVBorrowerName(object oBorrowId)
    {
        BorrowerService borrowerService = new BorrowerService();
        return EncryptionService.AESDecrypt(borrowerService.Search(new Borrower() { IsValid = 1 }).Find(o => o.Id == Convert.ToInt32(oBorrowId)).FullName);
    }

    protected string VIVBorrowerPhone(object oBorrowId)
    {
        BorrowerService borrowerService = new BorrowerService();
        return EncryptionService.AESDecrypt(borrowerService.Search(new Borrower() { IsValid = 1 }).Find(o => o.Id == Convert.ToInt32(oBorrowId)).Phone);
    }

    protected string VIVMoney(object oMoney)
    {
        return VIVUtil.VIVMoney(oMoney.ToString());
    }

    protected string VIVDay(string sDateTime)
    {
        string sRet = DateTimeUtil.GetDatePart(VIVUtil.VIVDateTime(sDateTime));

        return sRet;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("carillegal_create.aspx");
    }
}
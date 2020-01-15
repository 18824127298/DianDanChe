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

public partial class caiwu_caiwu_borrower_borrower_weihuan_list : SbtPageBase
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
                       = @"select SUM(b.Principal) as Principal, br.FullName, br.Phone, l.InterestDate from Borrow b join Borrower br
on b.BorrowerId= br.Id join LoanApply l on b.LoanApplyId= l.Id join Borrower brs on l.SalesmanId= brs.Id 
and b.IsValid= 1 and brs.Company = 0 and l.RepaymentStatus = 5 group by br.FullName, br.Phone,l.InterestDate";

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


        int nRecordCnt = ds.Tables[0].Rows.Count;

        double fSummaryOfAmount = 0;


        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataRow dRow = ds.Tables[0].Rows[i];
            double fAmount = ConvertUtil.ToFloat(dRow["Principal"]);
            fSummaryOfAmount += fAmount;
        }

        lblAmount.Text = fSummaryOfAmount.ToString("#0");
    }

    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField(e.SortExpression);
        gridViewPager.RefreshGridView();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = @"select SUM(b.Principal) as Principal, br.FullName, br.Phone,l.InterestDate from Borrow b join Borrower br
on b.BorrowerId= br.Id join LoanApply l on b.LoanApplyId= l.Id join Borrower brs on l.SalesmanId= brs.Id 
and b.IsValid= 1 and brs.Company = " + StringUtil.QuotedToDBStr(ddlCompany.SelectedValue);
        if (dpApplyToTime.DateTimeString != "")
        {
            sql += " and (Convert(varchar(100),b.ActualRepaymentDate,23) >=" + StringUtil.QuotedToDBStr(dpApplyToTime.DateTimeString) + " or b.ActualRepaymentDate is null) and Convert(varchar(100), l.InterestDate ,23) <=" + StringUtil.QuotedToDBStr(dpApplyToTime.DateTimeString);
        }
        sql += " group by br.FullName, br.Phone,l.InterestDate order by l.InterestDate";
        CurrentPageStatus.DataViewStatus.SqlBuilder.ClearFixConditions();
        CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql = sql;
        FetchDataFromDB();
    }
}
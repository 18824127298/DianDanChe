using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
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

public partial class loan_loan_discount_repayments_loan_list : SbtPageBase
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
                      = @"select l.Id, l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,l.TotalAmountStage,
l.Deadline, l.CustomerClassification, SUM(d.Amount) as Amount, SUM(d.LeftAmount) as Leftamount, d.Remark, d.CreateTime
from LoanApply l join IdCardInformation i on l.BorrowerId= i.BorrowerId 
join Borrower b on b.Id = l.SalesmanId left join Discount d on d.LoanApplyId= l.Id 
 where l.IsValid= 1 and i.IsValid= 1 and d.IsValid= 1 and d.Amount>0 and d.SecondAuditResult = 1 group by
l.Id, l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,
l.TotalAmountStage,l.Deadline, l.CustomerClassification,d.Remark, d.CreateTime";

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
    protected string VIVOper(object oId)
    {
        string sRet = "<a href=\"../loan_list/borrower_borrow_list.aspx?Id=" + oId.ToString() + "\"><image src='../../images/menu_icon/erp.gif' title='查看' /></a>";

        return sRet;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = @"select l.Id, l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,l.TotalAmountStage,
l.Deadline, l.CustomerClassification, SUM(d.Amount) as Amount, SUM(d.LeftAmount) as Leftamount,d.Remark, d.CreateTime
from LoanApply l join IdCardInformation i on l.BorrowerId= i.BorrowerId 
join Borrower b on b.Id = l.SalesmanId left join Discount d on d.LoanApplyId= l.Id
 where l.IsValid= 1 and i.IsValid= 1 and d.IsValid= 1 and d.Amount>0  and d.SecondAuditResult = 1 ";
        if (edtCreditName.Text.Trim() != "")
        {
            sql += " and i.Name = " + StringUtil.QuotedToDBStr(edtCreditName.Text);
        }

        if (edtCreditPhone.Text.Trim() != "")
        {
            sql += " and l.CreditPhone = " + StringUtil.QuotedToDBStr(edtCreditPhone.Text);
        }
        if (dpApplyFromTime.DateTimeString != "")
        {
            sql += " and l.CreateTime >= " + StringUtil.QuotedToDBStr(DateTimeUtil.BeginTimeOfDay(dpApplyFromTime.DateTimeString));
        }

        if (dpApplyToTime.DateTimeString != "")
        {
            sql += " and l.CreateTime <= " + StringUtil.QuotedToDBStr(DateTimeUtil.EndTimeOfDay(dpApplyToTime.DateTimeString));
        }
        sql += @" group by l.Id, l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,
l.TotalAmountStage,l.Deadline, l.CustomerClassification,d.Remark, d.CreateTime";
        CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql = sql; 
        FetchDataFromDB();
    }
}
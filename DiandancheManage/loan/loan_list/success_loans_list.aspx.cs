using CheDaiBaoWeChatModel;
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

public partial class loan_loan_list_success_loans_list : SbtPageBase
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
                      = @"select l.Auditor, l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,
         l.TotalAmountStage, l.Deadline, l.CustomerClassification, l.AuditTime,l.Id,l.MonthlyPayment,l.LoanType from LoanApply l join IdCardInformation i on l.BorrowerId= i.BorrowerId join Borrower b on b.Id = l.SalesmanId
 where l.IsValid= 1 and l.RepaymentStatus = 5 and i.IsValid= 1";

            if (ConvertUtil.ToInt(CurrentUser.ThirdPartyCode) > 0)
            {
                BorrowerService borrowerService = new BorrowerService();
                Borrower borrower = borrowerService.GetById(ConvertUtil.ToInt(CurrentUser.ThirdPartyCode));
                if (borrower.IsSalesman.Value)
                {
                    CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql += " and l.SalesmanId = " + borrower.Id;
                }
            }
            if (CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql == "")
            {
            }
            else
            {
                gvList.PageSize = CurrentPageStatus.DataViewStatus.PageSize;
                gvList.PageIndex = CurrentPageStatus.DataViewStatus.CurrentPageIndex;
            }

            if (Session["successApplyFromTime"] != null)
            {
                dpApplyFromTime.DateTimeString = Session["successApplyFromTime"].ToString();
                CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql += " and l.AuditTime >= " + StringUtil.QuotedToDBStr(DateTimeUtil.BeginTimeOfDay(dpApplyFromTime.DateTimeString));
            }
            if (Session["successApplyToTime"] != null)
            {
                dpApplyToTime.DateTimeString = Session["successApplyToTime"].ToString();
                CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql += " and l.AuditTime <= " + StringUtil.QuotedToDBStr(DateTimeUtil.EndTimeOfDay(dpApplyToTime.DateTimeString));
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
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("l.CreateTime");
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField("l.CreateTime");
        //========= 1. 取出数据，并绑定 ========
        DataSet ds = DataHelper.Instance.ExecuteDataSet
                (CurrentPageStatus.DataViewStatus.SqlBuilder.ToString());

        gvList.DataSource = ds;
        gvList.DataBind();

        //========== 2. 保存当前状态 ==============
        CurrentPageStatus.DataViewStatus.PageSize = gvList.PageSize;
        CurrentPageStatus.DataViewStatus.CurrentPageIndex = gvList.PageIndex;

        ////============ 3. 搜索条件的描述 ==========
        //SQLBuilder currentSQLBuilder = CurrentPageStatus.DataViewStatus.SqlBuilder;
        //if (currentSQLBuilder.GetConditionCount() != 0)
        //{
        //    divSearchCondition.Visible = true;
        //    lblConditionDesc.Text = currentSQLBuilder.GetConditionDescription();
        //}
        //else
        //    divSearchCondition.Visible = false;
    }

    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField(e.SortExpression);
        gridViewPager.RefreshGridView();
    }

    protected void btnClearCondition_Click(object sender, EventArgs e)
    {
        Session["successApplyFromTime"] = null;
        Session["successApplyToTime"] = null;
        Response.Redirect("../loan_list/success_loans_list.aspx");
    }
    protected string VIVOper(object oId)
    {
        string sRet = "<a href=\"../loan_list/borrower_borrow_list.aspx?Id=" + oId.ToString() + "\"><image src='../../images/menu_icon/erp.gif' title='查看' /></a>";

        return sRet;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = @"select l.Auditor, l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,
         l.TotalAmountStage, l.Deadline, l.CustomerClassification, l.AuditTime,l.Id,l.MonthlyPayment, l.LoanType from LoanApply l join IdCardInformation i on l.BorrowerId= i.BorrowerId join Borrower b on b.Id = l.SalesmanId
 where l.IsValid= 1 and l.RepaymentStatus = 5 and i.IsValid= 1 ";
        if (edtCreditName.Text.Trim() != "")
        {
            sql += " and i.Name like '%" + edtCreditName.Text + "%'";
        }

        if (edtCreditPhone.Text.Trim() != "")
        {
            sql += " and l.CreditPhone = " + StringUtil.QuotedToDBStr(edtCreditPhone.Text);
        }
        if (dpApplyFromTime.DateTimeString != "")
        {
            sql += " and l.AuditTime >= " + StringUtil.QuotedToDBStr(DateTimeUtil.BeginTimeOfDay(dpApplyFromTime.DateTimeString));
            Session["successApplyFromTime"] = dpApplyFromTime.DateTimeString;
        }

        if (dpApplyToTime.DateTimeString != "")
        {
            sql += " and l.AuditTime <= " + StringUtil.QuotedToDBStr(DateTimeUtil.EndTimeOfDay(dpApplyToTime.DateTimeString));
            Session["successApplyToTime"] = dpApplyToTime.DateTimeString;
        }

        if (ddlCompany.SelectedValue != "All")
        {
            if (ddlCompany.SelectedValue == "0")
            {
                sql += " and (l.Company = 0 or l.Company is null)";
            }
            else
            {
                sql += " and l.Company = " + StringUtil.QuotedToDBStr(ddlCompany.SelectedValue);
            }
        }
        if (ddlIsRider.SelectedValue != "All")
        {
            sql += " and l.IsRider = " + StringUtil.QuotedToDBStr(ddlIsRider.SelectedValue);
        }
        if (ddlIsAnswer.SelectedValue != "All")
        {
            sql += " and l.IsAnswer = " + StringUtil.QuotedToDBStr(ddlIsAnswer.SelectedValue);
        }
        if (ddlIsAbnormal.SelectedValue != "All")
        {
            sql += " and l.IsAbnormal = " + StringUtil.QuotedToDBStr(ddlIsAbnormal.SelectedValue);
        }
        if (ConvertUtil.ToInt(CurrentUser.ThirdPartyCode) > 0)
        {
            BorrowerService borrowerService = new BorrowerService();
            Borrower borrower = borrowerService.GetById(ConvertUtil.ToInt(CurrentUser.ThirdPartyCode));
            if (borrower.IsSalesman.Value)
            {
                sql += " and l.SalesmanId = " + borrower.Id;
            }
        }
        CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql = sql;
        FetchDataFromDB();
    }


    public string VIVinformation(object oId)
    {
        int nId = ConvertUtil.ToInt(oId);
        string sRet = "<a href=\"../loan_list/other_information_list.aspx?Id=" + oId.ToString() + "\"><image src='../../images/menu_icon/erp.gif' title='查看' /></a>";
        return sRet;
    }

    protected string VIVFollowUp(object oId)
    {
        int nId = ConvertUtil.ToInt(oId);
        string sRet = "<a href=\"../../salesman/salesman_list/borrow_follow_list.aspx?Id=" + oId.ToString() + "\"><image src='../../images/menu_icon/erp.gif' title='查看' /></a>";
        return sRet;
    }

    protected string VIVNavigateUrl(object oId, object oLoanType)
    {
        int nId = ConvertUtil.ToInt(oId);
        if (ConvertUtil.ToInt(oLoanType) == (int)LoanType.租客)
        {
            return "rent_details.aspx?id=" + nId.ToString();
        }
        else
        {
            return "loan_details.aspx?id=" + nId.ToString();
        }
    }
}
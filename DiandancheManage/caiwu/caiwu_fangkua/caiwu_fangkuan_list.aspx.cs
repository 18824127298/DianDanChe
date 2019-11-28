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

public partial class caiwu_caiwu_fangkua_caiwu_fangkuan_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Action"] != null)
        {
            Response.ContentType = "text/plain";
            string Action = Request.QueryString["Action"];
            switch (Action)
            {
                case "Lending":
                    Response.Write(Lending(Request.QueryString["Id"]));
                    break;
            }
            Response.End();
        }
        //========== 0. 绑定DataGrid和PageControl的关系 ===========
        gridViewPager.GridViewControl = gvList;
        gridViewPager.PageIndexChanged
            = new module_WebUserControl.PageIndexChangedHandler(gvList__PageIndexChanged);

        //========= 1. 如果首次进来，则取出前次的状态 ===========
        if (!IsPostBack)
        {
            CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                       = @"select l.Auditor, l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,
l.TotalAmountStage, l.Deadline, l.CustomerClassification, l.AuditTime,l.Id from LoanApply l join IdCardInformation i on l.BorrowerId= i.BorrowerId join Borrower b on b.Id = l.SalesmanId
 where l.IsValid= 1 and l.RepaymentStatus = 5 and i.IsValid= 1 and (l.IsLending is null or l.IsLending = 0) and (l.Company = 0 or l.Company is null)";

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

    protected void btnClearCondition_Click(object sender, EventArgs e)
    {

    }


    public string Lending(string Id)
    {
        try
        {
            int nId = ConvertUtil.ToInt(Id);
            LoanApplyService loanapplyService = new LoanApplyService();
            LoanApply loanapply = loanapplyService.GetById(nId);
            loanapply.InterestDate = DateTime.Now.Date.AddDays(-1);
            loanapply.ExpectedRepayment = DateTime.Now.Date.AddDays(-1).AddMonths(ConvertUtil.ToInt(loanapply.Deadline));
            loanapply.IsLending = true;
            loanapply.LendingDate = DateTime.Now.Date;
            loanapply.BatchDate = DateTime.Now.Date.AddDays(-1);
            loanapplyService.Update(loanapply);

            BorrowService borrowService = new BorrowService();
            Borrow borrow = new Borrow();
            int avgPrincipal = Convert.ToInt32(Math.Floor(loanapply.TotalAmountStage.Value / loanapply.Deadline.Value).ToString());
            decimal Interest = loanapply.MonthlyPayment.Value - avgPrincipal;
            for (int i = 0; i < loanapply.Deadline; i++)
            {
                borrow.BorrowerId = loanapply.BorrowerId;
                borrow.LoanApplyId = loanapply.Id;
                borrow.RepaymentDate = DateTime.Now.AddDays(-1).AddMonths(i + 1).Date;
                borrow.Stages = i + 1;
                borrow.TotalPeriod = loanapply.Deadline.Value;
                borrow.OverInterest = 0;
                borrow.OverDay = 0;
                borrow.BreachAmount = 0;
                if (i + 1 == loanapply.Deadline)
                {
                    borrow.Principal = loanapply.TotalAmountStage.Value - avgPrincipal * (loanapply.Deadline - 1) + 1;
                    borrow.UnPrincipal = loanapply.TotalAmountStage.Value - avgPrincipal * (loanapply.Deadline - 1) + 1;
                    borrow.Interest = loanapply.MonthlyPayment.Value - borrow.Principal;
                    borrow.UnTotalInterest = loanapply.MonthlyPayment.Value - borrow.UnPrincipal;
                }
                else
                {
                    borrow.Principal = avgPrincipal;
                    borrow.UnPrincipal = avgPrincipal;
                    borrow.Interest = Interest;
                    borrow.UnTotalInterest = Interest;
                }
                borrowService.Insert(borrow);
            }
            FundsFlowService fundsflowService = new FundsFlowService();
            fundsflowService.Insert(new FundsFlow()
            {
                Amount = loanapply.TotalAmountStage,
                IncomeGodId = 5,
                FeeType = FeeType.平台打款,
                IsComputing = true,
                PayGodId = 2,
                LoanApplyId = loanapply.Id,
                IsFreeze = false,
                Remark = "打款"
            });

            return "放款已完成";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = @"select l.Auditor, l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,
l.TotalAmountStage, l.Deadline, l.CustomerClassification, l.AuditTime,l.Id from LoanApply l join IdCardInformation i on l.BorrowerId= i.BorrowerId join Borrower b on b.Id = l.SalesmanId
 where l.IsValid= 1 and l.RepaymentStatus = 5 and i.IsValid= 1 and (l.IsLending is null or l.IsLending = 0)";
        if (edtCreditName.Text.Trim() != "")
        {
            sql += " and i.Name like '%" + edtCreditName.Text + "%'";
        }

        if (edtCreditPhone.Text.Trim() != "")
        {
            sql += " and l.CreditPhone = " + StringUtil.QuotedToDBStr(edtCreditPhone.Text);
        }

        if (ddlCompany.SelectedValue == "0")
        {
            sql += " and (l.Company = 0 or l.Company is null)";
        }
        else
        {
            sql += " and l.Company = " + StringUtil.QuotedToDBStr(ddlCompany.SelectedValue);
        }
        CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql = sql;
        FetchDataFromDB();
    }
}
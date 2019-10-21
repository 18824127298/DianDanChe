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

public partial class caiwu_caiwu_fangkua_success_fangkuan_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //========== 0. 绑定DataGrid和PageControl的关系 ===========
        gridViewPager.GridViewControl = gvList;
        gridViewPager.PageIndexChanged
            = new module_WebUserControl.PageIndexChangedHandler(gvList__PageIndexChanged);

        if (Request.QueryString["Action"] != null)
        {
            Response.ContentType = "text/plain";
            string Action = Request.QueryString["Action"];
            switch (Action)
            {
                case "chedan":
                    Response.Write(chedan(Request.QueryString["Id"], Request.QueryString["CancellationRemark"]));
                    break;
            }
            Response.End();
        }

        //========= 1. 如果首次进来，则取出前次的状态 ===========
        if (!IsPostBack)
        {
            CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                       = @"select l.Auditor, l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,
l.TotalAmountStage, l.Deadline, l.CustomerClassification, l.AuditTime,l.Id, l.LendingDate from LoanApply l join IdCardInformation i on l.BorrowerId= i.BorrowerId join Borrower b on b.Id = l.SalesmanId
 where l.IsValid= 1 and (l.RepaymentStatus = 5 or l.RepaymentStatus = 6) and i.IsValid= 1 and l.IsLending = 1";

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

        int nRecordCnt = ds.Tables[0].Rows.Count;

        double fSummaryOfAmount = 0;


        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataRow dRow = ds.Tables[0].Rows[i];

            double fAmount = ConvertUtil.ToFloat(dRow["TotalAmountStage"]);
            fSummaryOfAmount += fAmount;

        }

        lblSummaryInfo.Text = string.Format("总共<b>{0}</b>笔，总金额一共<b>{1}</b>元；",
            nRecordCnt, fSummaryOfAmount.ToString("0.00"));


    }

    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField(e.SortExpression);
        gridViewPager.RefreshGridView();
    }

    protected void btnClearCondition_Click(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = @"select l.Auditor, l.CreditPhone, i.Name, b.FullName,l.BusinessName, l.RecruitmentName,
l.TotalAmountStage, l.Deadline, l.CustomerClassification, l.AuditTime,l.Id,l.LendingDate from LoanApply l join IdCardInformation i on l.BorrowerId= i.BorrowerId join Borrower b on b.Id = l.SalesmanId
 where l.IsValid= 1 and l.RepaymentStatus = 5 and i.IsValid= 1 and l.IsLending = 1";
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
            sql += " and l.LendingDate >= " + StringUtil.QuotedToDBStr(DateTimeUtil.BeginTimeOfDay(dpApplyFromTime.DateTimeString));
        }

        if (dpApplyToTime.DateTimeString != "")
        {
            sql += " and l.LendingDate <= " + StringUtil.QuotedToDBStr(DateTimeUtil.EndTimeOfDay(dpApplyToTime.DateTimeString));
        }
        CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql = sql;
        FetchDataFromDB();
    }
    protected string VIVHTime(object oTime)
    {

        if (string.IsNullOrEmpty(oTime.ToString()))
        {
            return "";
        }
        else
        {
            return Convert.ToDateTime(oTime).ToString("yyyy-MM-dd");
        }
    }

    public string chedan(string Id, string CancellationRemark)
    {
        try
        {
            int nId = ConvertUtil.ToInt(Id);
            LoanApplyService loanapplyService = new LoanApplyService();
            LoanApply loanapply = loanapplyService.GetById(nId);
            loanapply.RepaymentStatus = CreditStatus.撤单;
            loanapply.CancellationRemark = CancellationRemark;
            loanapplyService.Update(loanapply);
            FundsFlowService fundsflowService = new FundsFlowService();
            FundsFlow fundsflow = fundsflowService.Search(new FundsFlow() { IsValid = true }).Where(o => o.FeeType == FeeType.平台打款 && o.LoanApplyId == loanapply.Id && o.Amount == loanapply.TotalAmountStage).FirstOrDefault();
            if (fundsflow != null)
            {
                fundsflow.IsFreeze = true;
                fundsflow.IsComputing = false;
                fundsflowService.Update(fundsflow);
            }
            BorrowService borrowService = new BorrowService();
            List<Borrow> borrowList = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.LoanApplyId == loanapply.Id).ToList();
            foreach (Borrow borrow in borrowList)
            {
                borrowService.DeleteBySearch(borrow);
            }
            return "撤单已完成";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
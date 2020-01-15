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

public partial class caiwu_caiwu_borrower_rent_jieqing : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Action"] != null)
        {
            Response.ContentType = "text/plain";
            string Action = Request.QueryString["Action"];
            switch (Action)
            {
                case "Settle":
                    Response.Write(Settle(Request.QueryString["Id"]));
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
 where l.IsValid= 1 and l.RepaymentStatus = 5 and i.IsValid= 1 and l.IsLending = 1 and l.Company = 1";

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


    public string Settle(string Id)
    {
        try
        {
            int nId = ConvertUtil.ToInt(Id);
            LoanApplyService loanapplyService = new LoanApplyService();
            LoanApply loanapply = loanapplyService.GetById(nId);
            loanapply.RepaymentStatus = CreditStatus.还款完成;
            loanapply.ClosingDate = DateTime.Now;
            loanapply.RepaymentPlanMode = RepaymentPlanMode.提前还款;
            loanapplyService.Update(loanapply);

            BorrowService borrowService = new BorrowService();
            List<Borrow> borrowList = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.LoanApplyId == loanapply.Id && o.BorrowerId == loanapply.BorrowerId && (o.UnTotalInterest + o.UnPrincipal) > 0).ToList();
            foreach (Borrow borrow in borrowList)
            {
                borrow.ActualRepaymentDate = DateTime.Now;
                borrow.RepaymentPlanMode = RepaymentPlanMode.提前还款;
                borrow.UnPrincipal = 0;
                borrow.UnTotalInterest = 0;
                borrowService.Update(borrow);
            }

            return "结清已完成";
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
 where l.IsValid= 1 and l.RepaymentStatus = 5 and i.IsValid= 1 and l.IsLending = 1 and l.Company = 1";
        if (edtCreditName.Text.Trim() != "")
        {
            sql += " and i.Name like '%" + edtCreditName.Text + "%'";
        }

        if (edtCreditPhone.Text.Trim() != "")
        {
            sql += " and l.CreditPhone = " + StringUtil.QuotedToDBStr(edtCreditPhone.Text);
        }

        CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql = sql;
        FetchDataFromDB();
    }
}
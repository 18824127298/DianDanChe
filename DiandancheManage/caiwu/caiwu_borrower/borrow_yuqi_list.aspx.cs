using CheDaiBaoCommonService.Data;
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

public partial class caiwu_caiwu_borrower_borrow_yuqi_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Action"] != null)
        {
            Response.ContentType = "text/plain";
            string Action = Request.QueryString["Action"];
            switch (Action)
            {
                case "SingleTransaction":
                    Response.Write(SingleTransaction(Request.QueryString["bId"]));
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
            int nId = ConvertUtil.ToInt(Request["id"]);

            if (nId != 0)
            {
                PageParameter.SetCustomParamObject("id", nId);
            }
            CurrentPageStatus.DataViewStatus.SqlBuilder.NonConditionSql
                       = @"select b.Id, b.UnPrincipal, b.UnTotalInterest, br.FullName, l.SalesmanId, br.Phone,
l.Brand, b.Stages, b.TotalPeriod, l.CreditAmount, l.Id as lId from Borrow b join 
Borrower br on b.BorrowerId = br.Id join LoanApply l on b.LoanApplyId= l.Id
where b.IsValid = 1 and b.UnPrincipal>0
and CONVERT(varchar(100), b.RepaymentDate, 111) < CONVERT(varchar(100), GETDATE(), 111) and l.Id =" + nId;

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

        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = loanapplyService.GetById(ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id")));
        FundsFlowService fundsflowService = new FundsFlowService();
        lblBalance.Text = fundsflowService.GetAmountByBorrowerId(loanapply.BorrowerId).ToString();
        //========== 2. 保存当前状态 ==============
        CurrentPageStatus.DataViewStatus.PageSize = gvList.PageSize;
        CurrentPageStatus.DataViewStatus.CurrentPageIndex = gvList.PageIndex;
    }

    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        CurrentPageStatus.DataViewStatus.SqlBuilder.PushSortField(e.SortExpression);
        gridViewPager.RefreshGridView();
    }

    public string VIVSalesmanName(object SalesmanId)
    {
        BorrowerService borrowerService = new BorrowerService();
        return borrowerService.GetById(Convert.ToInt32(SalesmanId)).FullName;
    }

    public string SingleTransaction(string bId)
    {
        try
        {
            DiscountService discountService = new DiscountService();

            BorrowService borrowService = new BorrowService();
            Borrow borrow = borrowService.GetById(ConvertUtil.ToInt(bId));

            BorrowerService borrowerService = new BorrowerService();
            Borrower borrower = borrowerService.GetById(borrow.BorrowerId);

            LoanApplyService loanapplyService = new LoanApplyService();
            LoanApply loanapply = loanapplyService.GetById(borrow.LoanApplyId);

            FundsFlowService fundsflowService = new FundsFlowService();
            decimal balance = fundsflowService.GetAmountByBorrowerId(borrower.Id);
            if (balance < ConvertUtil.ToDecimal(lblBalance.Text))
            {
                return "支付金额不能大于用户的余额";
            }
            using (var connection = SqlConnections.GetOpenConnection())
            {
                connection.Open();
                using (var sqltran = connection.BeginTransaction())
                {
                    Recharge recharge = connection.Search<Recharge>(new Recharge() { IsValid = true }, sqltran).Where(o => o.IsAudit == true && o.BorrowerId == borrower.Id).OrderByDescending(o => o.CreateTime).FirstOrDefault();
                    if (recharge == null)
                    {
                        return "找不到用户的充值订单";
                    }

                    int fscId = 0;
                    decimal dStandardDeduction = 0;
                    Discount discount = connection.Search<Discount>(new Discount() { IsValid = true }, sqltran).Where(o => o.LeftAmount > 0 && o.BorrowerId == recharge.BorrowerId && o.LoanApplyId == loanapply.Id).FirstOrDefault();
                    if (discount != null)
                    {
                        dStandardDeduction = discount.LeftAmount.Value;
                    }
                    DebugLogger.LogDebugMessage("实际金额为：" + (borrow.UnPrincipal + borrow.UnTotalInterest - dStandardDeduction).ToString());
                    fscId = borrowService.OverdueRepayment(borrow, loanapply, recharge.Id, dStandardDeduction, sqltran);
                    if (dStandardDeduction > 0)
                    {
                        connection.Update(new Discount()
                        {
                            Id = discount.Id,
                            LeftAmount = 0,
                            RelationId = fscId
                        }, sqltran);
                    }
                    sqltran.Commit();
                }
                connection.Close();

                return "支付已成功";
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
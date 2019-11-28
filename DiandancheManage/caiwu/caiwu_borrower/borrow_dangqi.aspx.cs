using CheDaiBaoCommonService.Data;
using CheDaiBaoWeChatController.Interface;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using Sigbit.Common;
using Sigbit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class caiwu_caiwu_borrower_borrow_dangqi : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        int nId = ConvertUtil.ToInt(Request["id"]);

        if (nId != 0)
        {
            PageParameter.SetCustomParamObject("id", nId);
        }
        LoanApplyService loanapplyService = new LoanApplyService();
        DiscountService discountService = new DiscountService();
        LoanApply loanapply = loanapplyService.GetById(nId);

        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(loanapply.BorrowerId);
        Borrower Salesman = borrowerService.GetById(loanapply.SalesmanId);
        lblPhone.Text = borrower.Phone;
        lblName.Text = borrower.FullName;
        lblAmount.Text = ConvertUtil.ToString(loanapply.TotalAmountStage);
        lblSalesman.Text = Salesman.FullName;
        decimal dStandardDeduction = 0;
        Discount discount = discountService.Search(new Discount() { IsValid = true }).Where(o => o.LeftAmount > 0 && o.BorrowerId == loanapply.BorrowerId && o.LoanApplyId == loanapply.Id).FirstOrDefault();
        if (discount != null)
        {
            dStandardDeduction = discount.LeftAmount.Value;
        }
        BorrowService borrowService = new BorrowService();
        Borrow newborrow = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.LoanApplyId == loanapply.Id && o.BorrowerId == loanapply.BorrowerId && (o.UnTotalInterest + o.UnPrincipal) > 0 && o.ActualRepaymentDate == null && o.RepaymentDate >= DateTime.Now.Date && DateTime.Now.Date > o.RepaymentDate.Value.AddMonths(-1).Date).FirstOrDefault();
        if (newborrow != null)
        {
            lblunSumPrincipal.Text = newborrow.UnPrincipal.ToString();
            lblInterest.Text = newborrow.UnTotalInterest.ToString();
            lblStandardDeduction.Text = dStandardDeduction.ToString();
            edtSumAmount.Text = (newborrow.UnPrincipal + newborrow.UnTotalInterest - dStandardDeduction).ToString();
        }

        FundsFlowService fundsflowService = new FundsFlowService();
        lblBalance.Text = fundsflowService.GetAmountByBorrowerId(borrower.Id).ToString();
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        LoanApplyService loanapplyService = new LoanApplyService();
        DiscountService discountService = new DiscountService();
        LoanApply loanapply = loanapplyService.GetById(ConvertUtil.ToInt(PageParameter.GetCustomParamObject("id")));

        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(loanapply.BorrowerId);

        FundsFlowService fundsflowService = new FundsFlowService();
        decimal balance = fundsflowService.GetAmountByBorrowerId(borrower.Id);
        if (balance != ConvertUtil.ToDecimal(edtSumAmount.Text))
        {
            Response.Write("<script>alert('支付金额与用户的余额不等')</script>");
            return;
        }
        using (var connection = SqlConnections.GetOpenConnection())
        {
            connection.Open();
            using (var sqltran = connection.BeginTransaction())
            {
                Recharge recharge = connection.Search<Recharge>(new Recharge() { IsValid = true }, sqltran).Where(o => o.IsAudit == true && o.BorrowerId == borrower.Id).OrderByDescending(o => o.CreateTime).FirstOrDefault();
                if (recharge == null)
                {
                    Response.Write("<script>alert('找不到用户的充值订单')</script>");
                    return;
                }

                if (loanapply != null)
                {
                    int fscId = 0;
                    decimal dStandardDeduction = 0;
                    Discount discount = connection.Search<Discount>(new Discount() { IsValid = true }, sqltran).Where(o => o.LeftAmount > 0 && o.BorrowerId == recharge.BorrowerId && o.LoanApplyId == loanapply.Id).FirstOrDefault();
                    if (discount != null)
                    {
                        dStandardDeduction = discount.LeftAmount.Value;
                    }
                    List<Borrow> borrowList = connection.Search<Borrow>(new Borrow() { IsValid = true }, sqltran).Where(o => o.LoanApplyId == loanapply.Id && o.BorrowerId == recharge.BorrowerId && (o.UnTotalInterest + o.UnPrincipal) > 0).ToList();
                    Borrow newborrow = borrowList.Find(o => o.ActualRepaymentDate == null && o.RepaymentDate >= DateTime.Now.Date && DateTime.Now.Date > o.RepaymentDate.Value.AddMonths(-1).Date);
                    DebugLogger.LogDebugMessage("实际金额为：" + (newborrow.UnPrincipal + newborrow.UnTotalInterest - dStandardDeduction).ToString());
                    if (ConvertUtil.ToDecimal(edtSumAmount.Text) == newborrow.UnPrincipal + newborrow.UnTotalInterest - dStandardDeduction)
                    {
                        BorrowService borrowService = new BorrowService();
                        if (newborrow == null)
                        {
                            newborrow = borrowList.FindAll(o => o.LoanApplyId == loanapply.Id && o.RepaymentDate >= DateTime.Now.Date && o.RepaymentPlanMode == null).OrderBy(o => o.Stages).FirstOrDefault();
                        }
                        fscId = borrowService.NormalRepayment(newborrow, loanapply, recharge.Id, dStandardDeduction, sqltran);
                        if (dStandardDeduction > 0)
                        {
                            connection.Update(new Discount()
                            {
                                Id = discount.Id,
                                LeftAmount = 0,
                                RelationId = fscId
                            }, sqltran);
                        }
                        Response.Write("<script>alert('支付操作完成')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('金额不对等')</script>");
                    }
                }
                sqltran.Commit();
            }
            connection.Close();
        }

        RechargeService rechargeService = new RechargeService();
        Recharge newrecharge = rechargeService.Search(new Recharge() { IsValid = true }).Where(o => o.IsAudit == true && o.BorrowerId == borrower.Id).OrderByDescending(o => o.CreateTime).FirstOrDefault();

        QiyebaoSms qiyebaoSms = new QiyebaoSms();
        string sContent = string.Format("恭喜您，本次融资租赁费支付成功，金额为{0}元，订单号{1}【车1号】", edtSumAmount.Text, newrecharge.OrderNumber);
        qiyebaoSms.SendSms(borrower.Phone, sContent);
    }
}
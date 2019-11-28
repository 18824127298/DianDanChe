using CheDaiBaoCommonService.Data;
using CheDaiBaoWeChatModel;
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

public partial class caiwu_caiwu_borrower_loan_tihuan : SbtPageBase
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
        List<Borrow> borrowList = borrowService.Search(new Borrow() { IsValid = true }).Where(o => o.LoanApplyId == loanapply.Id && o.BorrowerId == loanapply.BorrowerId && (o.UnTotalInterest + o.UnPrincipal) > 0).ToList();
        Borrow newborrow = borrowList.Find(o => o.ActualRepaymentDate == null && o.RepaymentDate >= DateTime.Now.Date && DateTime.Now.Date > o.RepaymentDate.Value.AddMonths(-1).Date);
        List<Borrow> overdueborrowlist = borrowList.FindAll(o => o.ActualRepaymentDate == null && o.OverDay > 0);
        decimal overdueInterest = overdueborrowlist.Sum(o => o.UnTotalInterest).Value;

        decimal unSumPrincipal = borrowList.Sum(o => o.UnPrincipal).Value;
        decimal oneInterest = borrowList.FirstOrDefault().Interest.Value;
        decimal dServiceCharge = 0;
        decimal dSumAmount = 0;
        if (loanapply.InterestDate.Value.Date < new DateTime(2019, 07, 6))
        {
            if (loanapply.InterestDate.Value.AddDays(15) >= DateTime.Now.Date)
            {
                dServiceCharge = 200;
                dSumAmount = unSumPrincipal + dServiceCharge - dStandardDeduction;
            }
            else if (loanapply.InterestDate.Value.AddMonths(3) >= DateTime.Now.Date)
            {
                if (newborrow == null)
                {
                    dServiceCharge = 200;
                }
                else
                {
                    dServiceCharge = 200 + oneInterest;
                }
                dSumAmount = unSumPrincipal + dServiceCharge - dStandardDeduction;
            }
            else
            {
                List<Borrow> newborrowList = borrowList.FindAll(o => o.Stages <= 11);
                if (newborrowList.Count > 0)
                {
                    DateTime dtTime = borrowList.Find(o => o.RepaymentDate >= DateTime.Now.Date && DateTime.Now.Date > o.RepaymentDate.Value.AddMonths(-1).Date).RepaymentDate.Value.Date;
                    if (newborrow == null)
                    {
                        if (DateTime.Now.Date.AddDays(15) > dtTime)
                        {
                            dServiceCharge = oneInterest;
                        }
                        else
                        {
                            dServiceCharge = 0;
                        }
                    }
                    else
                    {
                        if (DateTime.Now.Date.AddDays(15) > dtTime)
                        {
                            dServiceCharge = oneInterest * 2;
                        }
                        else
                        {
                            dServiceCharge = oneInterest;
                        }
                    }
                }
                else
                {
                    dServiceCharge = oneInterest;
                }
                dSumAmount = unSumPrincipal + dServiceCharge - dStandardDeduction;
            }
        }
        else if (loanapply.InterestDate.Value.Date < new DateTime(2019, 08, 27))
        {
            if (loanapply.InterestDate.Value.AddMonths(loanapply.WithinMonth) >= DateTime.Now.Date)
            {
                if (newborrow == null)
                {
                    dServiceCharge = 200;
                }
                else
                {
                    dServiceCharge = 200 + oneInterest;
                }
            }
            else
            {
                if (newborrow == null)
                {
                    dServiceCharge = 100;
                }
                else
                {
                    dServiceCharge = 100 + oneInterest;
                }
            }
            dSumAmount = unSumPrincipal + dServiceCharge - dStandardDeduction;
        }
        else
        {
            if (loanapply.InterestDate.Value.AddMonths(loanapply.WithinMonth) >= DateTime.Now.Date)
            {
                if (newborrow == null)
                {
                    dServiceCharge = 300;
                }
                else
                {
                    dServiceCharge = 300 + oneInterest;
                }
            }
            else
            {
                if (newborrow == null)
                {
                    dServiceCharge = 200;
                }
                else
                {
                    dServiceCharge = 200 + oneInterest;
                }
            }
            dSumAmount = unSumPrincipal + dServiceCharge - dStandardDeduction;
        }
        lblunSumPrincipal.Text = unSumPrincipal.ToString();
        lblServiceCharge.Text = dServiceCharge.ToString();
        lblStandardDeduction.Text = dStandardDeduction.ToString();
        edtSumAmount.Text = (dSumAmount + overdueInterest).ToString();
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
        if (balance < ConvertUtil.ToDecimal(edtSumAmount.Text))
        {
            Response.Write("<script>alert('支付金额不能大于用户的余额')</script>");
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
                    List<Borrow> overdueborrowlist = borrowList.FindAll(o => o.ActualRepaymentDate == null && o.OverDay > 0 && o.UnTotalInterest > 0);
                    decimal overdueInterest = overdueborrowlist.Sum(o => o.UnTotalInterest).Value;

                    decimal unSumPrincipal = borrowList.Sum(o => o.UnPrincipal).Value;
                    decimal oneInterest = borrowList.FirstOrDefault().Interest.Value;
                    decimal dServiceCharge = 0;
                    decimal dSumAmount = 0;
                    if (loanapply.InterestDate.Value.Date < new DateTime(2019, 07, 6))
                    {
                        if (loanapply.InterestDate.Value.AddDays(15) >= DateTime.Now.Date)
                        {
                            dServiceCharge = 200;
                            dSumAmount = unSumPrincipal + dServiceCharge;
                        }
                        else if (loanapply.InterestDate.Value.AddMonths(3) >= DateTime.Now.Date)
                        {
                            if (newborrow == null)
                            {
                                dServiceCharge = 200;
                            }
                            else
                            {
                                dServiceCharge = 200 + oneInterest;
                            }
                            dSumAmount = unSumPrincipal + dServiceCharge;
                        }
                        else
                        {
                            List<Borrow> newborrowList = borrowList.FindAll(o => o.Stages <= 11);
                            if (newborrowList.Count > 0)
                            {
                                DateTime dtTime = borrowList.Find(o => o.RepaymentDate >= DateTime.Now.Date && DateTime.Now.Date > o.RepaymentDate.Value.AddMonths(-1).Date).RepaymentDate.Value.Date;
                                if (newborrow == null)
                                {
                                    if (DateTime.Now.Date.AddDays(15) > dtTime)
                                    {
                                        dServiceCharge = oneInterest;
                                    }
                                    else
                                    {
                                        dServiceCharge = 0;
                                    }
                                }
                                else
                                {
                                    if (DateTime.Now.Date.AddDays(15) > dtTime)
                                    {
                                        dServiceCharge = oneInterest * 2;
                                    }
                                    else
                                    {
                                        dServiceCharge = oneInterest;
                                    }
                                }
                            }
                            else
                            {
                                dServiceCharge = oneInterest;
                            }
                            dSumAmount = unSumPrincipal + dServiceCharge;
                        }
                    }
                    else if (loanapply.InterestDate.Value.Date < new DateTime(2019, 08, 27))
                    {
                        if (loanapply.InterestDate.Value.AddMonths(loanapply.WithinMonth) >= DateTime.Now.Date)
                        {
                            if (newborrow == null)
                            {
                                dServiceCharge = 200;
                            }
                            else
                            {
                                dServiceCharge = 200 + oneInterest;
                            }
                            dSumAmount = unSumPrincipal + dServiceCharge;
                        }
                        else
                        {
                            if (newborrow == null)
                            {
                                dServiceCharge = 100;
                            }
                            else
                            {
                                dServiceCharge = 100 + oneInterest;
                            }
                            dSumAmount = unSumPrincipal + dServiceCharge;
                        }
                    }
                    else
                    {
                        if (loanapply.InterestDate.Value.AddMonths(loanapply.WithinMonth) >= DateTime.Now.Date)
                        {
                            if (newborrow == null)
                            {
                                dServiceCharge = 300;
                            }
                            else
                            {
                                dServiceCharge = 300 + oneInterest;
                            }
                            dSumAmount = unSumPrincipal + dServiceCharge;
                        }
                        else
                        {
                            if (newborrow == null)
                            {
                                dServiceCharge = 200;
                            }
                            else
                            {
                                dServiceCharge = 200 + oneInterest;
                            }
                            dSumAmount = unSumPrincipal + dServiceCharge;
                        }
                    }
                    DebugLogger.LogDebugMessage("实际金额为：" + (dSumAmount - dStandardDeduction).ToString());
                    if (Convert.ToDecimal(edtSumAmount.Text) == dSumAmount - dStandardDeduction + overdueInterest)
                    {
                        BorrowService borrowService = new BorrowService();
                        fscId = borrowService.AdvanceRepayment(unSumPrincipal, dServiceCharge, borrowList, loanapply, borrower.Id, recharge.Id, dStandardDeduction, overdueborrowlist, overdueInterest, sqltran);
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
    }
}

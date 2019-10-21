using CheDaiBaoCommonService.Data;
using CheDaiBaoWeChatController.Interface;
using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Interface;
using Sigbit.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatService.Service
{
    public partial class BorrowService
    {

        public void Repayment(int dAmount, int nRechargeId, string sRepaymentType, SqlTransaction sqlTransation = null)
        {
            SqlConnection sqlConnection = sqlTransation == null ? SqlConnections.GetOpenConnection() : sqlTransation.Connection;

            DebugLogger.LogDebugMessage("金额为" + dAmount.ToString() + "充值的Id" + nRechargeId.ToString() + "类型：" + sRepaymentType);

            Recharge recharge = sqlConnection.GetById<Recharge>(nRechargeId, sqlTransation);
            LoanApply loanapply = sqlConnection.Search<LoanApply>(new LoanApply() { IsValid = true }, sqlTransation).Where(o => o.BorrowerId == recharge.BorrowerId && o.RepaymentStatus == CreditStatus.还款中).FirstOrDefault();
            if (loanapply != null)
            {
                int fscId = 0;
                decimal dStandardDeduction = 0;
                Discount discount = sqlConnection.Search<Discount>(new Discount() { IsValid = true }, sqlTransation).Where(o => o.LeftAmount > 0 && o.BorrowerId == recharge.BorrowerId && o.LoanApplyId == loanapply.Id && o.SecondAuditResult == true).FirstOrDefault();
                if (discount != null)
                {
                    dStandardDeduction = discount.LeftAmount.Value;
                }
                List<Borrow> allborrowList = sqlConnection.Search<Borrow>(new Borrow() { IsValid = true }, sqlTransation).Where(o => o.LoanApplyId == loanapply.Id && o.BorrowerId == recharge.BorrowerId).ToList();
                List<Borrow> borrowList = allborrowList.FindAll(o => (o.UnTotalInterest + o.UnPrincipal) > 0);
                Borrow newborrow = borrowList.Find(o => o.ActualRepaymentDate == null && o.RepaymentDate >= DateTime.Now.Date && DateTime.Now.Date > o.RepaymentDate.Value.AddMonths(-1).Date);
                #region 提还
                if (sRepaymentType == "提还")
                {
                    List<Borrow> overdueborrowlist = borrowList.FindAll(o => o.ActualRepaymentDate == null && o.OverDay > 0);
                    decimal overdueInterest = overdueborrowlist.Sum(o => o.UnTotalInterest).Value;

                    decimal unSumPrincipal = borrowList.Sum(o => o.UnPrincipal).Value;
                    decimal oneInterest = borrowList.FirstOrDefault().Interest.Value;
                    decimal dServiceCharge = 0;
                    decimal dSumAmount = 0;
                    if (loanapply.InterestDate.Value.Date < new DateTime(2019, 07, 9))
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
                                DateTime dtTime = allborrowList.Find(o => o.RepaymentDate >= DateTime.Now.Date && DateTime.Now.Date > o.RepaymentDate.Value.AddMonths(-1).Date).RepaymentDate.Value.Date;
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
                        dSumAmount = unSumPrincipal + dServiceCharge;
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
                        dSumAmount = unSumPrincipal + dServiceCharge;
                    }
                    DebugLogger.LogDebugMessage("实际金额为：" + ((dSumAmount - dStandardDeduction + overdueInterest) * 100).ToString());
                    if (dAmount == (dSumAmount - dStandardDeduction + overdueInterest) * 100)
                    {
                        fscId = AdvanceRepayment(unSumPrincipal, dServiceCharge, borrowList, loanapply, recharge.BorrowerId, nRechargeId, dStandardDeduction, overdueborrowlist, overdueInterest, sqlTransation);
                    }
                }
                #endregion

                #region 正常还利息
                if (sRepaymentType == "正常还利息")
                {
                    if (newborrow == null)
                    {
                        newborrow = borrowList.FindAll(o => o.LoanApplyId == loanapply.Id && o.RepaymentDate >= DateTime.Now.Date && o.RepaymentPlanMode == null).OrderBy(o => o.Stages).FirstOrDefault();
                    }
                    DebugLogger.LogDebugMessage("实际金额为：" + (newborrow.UnPrincipal + newborrow.UnTotalInterest - dStandardDeduction).ToString());
                    if (dAmount == (newborrow.UnPrincipal + newborrow.UnTotalInterest - dStandardDeduction) * 100)
                    {
                        fscId = NormalRepayment(newborrow, loanapply, nRechargeId, dStandardDeduction, sqlTransation);
                    }
                }
                #endregion

                #region 逾期
                if (sRepaymentType == "逾期")
                {
                    Borrow overBorrow = borrowList.OrderBy(o => o.RepaymentDate).ThenBy(o => o.Stages).First();
                    DebugLogger.LogDebugMessage("实际金额为：" + (overBorrow.UnPrincipal + overBorrow.UnTotalInterest - dStandardDeduction).ToString());
                    if (dAmount == (overBorrow.UnPrincipal + overBorrow.UnTotalInterest - dStandardDeduction) * 100)
                    {
                        fscId = OverdueRepayment(overBorrow, loanapply, nRechargeId, dStandardDeduction, sqlTransation);
                    }
                }

                if (dStandardDeduction > 0)
                {
                    sqlConnection.Update(new Discount()
                    {
                        Id = discount.Id,
                        LeftAmount = 0,
                        RelationId = fscId
                    }, sqlTransation);
                }
                #endregion

                #region 还租金
                if (sRepaymentType == "还租金")
                {
                    decimal? unDeposit = loanapply.unDeposit;
                    Borrow borrow = borrowList.FindAll(o => o.LoanApplyId == loanapply.Id && o.RepaymentPlanMode == null).OrderBy(o => o.Stages).FirstOrDefault();
                    DebugLogger.LogDebugMessage("实际金额为：" + (borrow.UnTotalInterest + unDeposit - dStandardDeduction).ToString());
                    if (dAmount == (borrow.UnTotalInterest + unDeposit - dStandardDeduction) * 100)
                    {
                        fscId = RentRepayment(borrow, loanapply, nRechargeId, dStandardDeduction, sqlTransation);
                    }
                }

                if (dStandardDeduction > 0)
                {
                    sqlConnection.Update(new Discount()
                    {
                        Id = discount.Id,
                        LeftAmount = 0,
                        RelationId = fscId
                    }, sqlTransation);
                }
                #endregion
            }
        }

        public int AdvanceRepayment(decimal unSumPrincipal, decimal dServiceCharge, List<Borrow> borrowList, LoanApply loanapply, int nBorrowId, int nRechargeId, decimal dStandardDeduction, List<Borrow> overdueborrowlist, decimal overdueInterest, SqlTransaction sqlTransation = null)
        {
            SqlConnection sqlConnection = sqlTransation == null ? SqlConnections.GetOpenConnection() : sqlTransation.Connection;

            int fId = sqlConnection.Insert(new FundsFlow()
            {
                Amount = unSumPrincipal,
                IncomeGodId = 2,
                FeeType = FeeType.本金,
                IsComputing = true,
                PayGodId = nBorrowId,
                RelationId = nRechargeId,
                LoanApplyId = loanapply.Id,
                IsFreeze = false,
                Remark = "提还"
            }, sqlTransation);
            int fscId = sqlConnection.Insert(new FundsFlow()
             {
                 Amount = dServiceCharge - dStandardDeduction + overdueInterest,
                 IncomeGodId = 2,
                 FeeType = FeeType.提前还款违约金,
                 IsComputing = true,
                 PayGodId = nBorrowId,
                 RelationId = nRechargeId,
                 LoanApplyId = loanapply.Id,
                 IsFreeze = false,
                 Remark = "提还违约金"
             }, sqlTransation);
            loanapply.RepaymentStatus = CreditStatus.还款完成;
            loanapply.ClosingDate = DateTime.Now;
            loanapply.RepaymentPlanMode = RepaymentPlanMode.提前还款;
            loanapply.BreachAmount = dServiceCharge;
            sqlConnection.Update<LoanApply>(loanapply, sqlTransation);

            foreach (Borrow oborrow in overdueborrowlist)
            {
                oborrow.ActualRepaymentDate = DateTime.Now;
                oborrow.FundsFlowId = fId;
                oborrow.RepaymentPlanMode = RepaymentPlanMode.逾期还款;
                oborrow.UnPrincipal = 0;
                oborrow.UnTotalInterest = 0;
                sqlConnection.Update<Borrow>(oborrow, sqlTransation);

            }
            foreach (Borrow borrow in borrowList)
            {
                borrow.ActualRepaymentDate = DateTime.Now;
                borrow.FundsFlowId = fId;
                borrow.RepaymentPlanMode = RepaymentPlanMode.提前还款;
                borrow.UnPrincipal = 0;
                borrow.UnTotalInterest = 0;
                sqlConnection.Update<Borrow>(borrow, sqlTransation);
            }
            return fscId;
        }

        public int NormalRepayment(Borrow borrow, LoanApply loanapply, int nRechargeId, decimal dStandardDeduction, SqlTransaction sqlTransation = null)
        {
            SqlConnection sqlConnection = sqlTransation == null ? SqlConnections.GetOpenConnection() : sqlTransation.Connection;

            int fId = sqlConnection.Insert(new FundsFlow()
            {
                Amount = borrow.UnPrincipal,
                IncomeGodId = 2,
                FeeType = FeeType.本金,
                IsComputing = true,
                PayGodId = borrow.BorrowerId,
                RelationId = nRechargeId,
                LoanApplyId = loanapply.Id,
                IsFreeze = false,
                Remark = borrow.Id.ToString()
            }, sqlTransation);
            int fscId = sqlConnection.Insert(new FundsFlow()
            {
                Amount = borrow.UnTotalInterest - dStandardDeduction,
                IncomeGodId = 2,
                FeeType = FeeType.利息,
                IsComputing = true,
                PayGodId = borrow.BorrowerId,
                RelationId = nRechargeId,
                LoanApplyId = loanapply.Id,
                IsFreeze = false,
                Remark = borrow.Id.ToString()
            }, sqlTransation);
            borrow.ActualRepaymentDate = DateTime.Now;
            borrow.FundsFlowId = fId;
            borrow.RepaymentPlanMode = RepaymentPlanMode.正常还款;
            borrow.UnTotalInterest = 0;
            borrow.UnPrincipal = 0;
            sqlConnection.Update<Borrow>(borrow, sqlTransation);

            if (borrow.Stages == loanapply.Deadline)
            {
                loanapply.RepaymentStatus = CreditStatus.还款完成;
                loanapply.ClosingDate = DateTime.Now;
                loanapply.RepaymentPlanMode = RepaymentPlanMode.正常还款;
                sqlConnection.Update<LoanApply>(loanapply, sqlTransation);
            }

            return fscId;
        }

        public int OverdueRepayment(Borrow overBorrow, LoanApply loanapply, int nRechargeId, decimal dStandardDeduction, SqlTransaction sqlTransation = null)
        {
            SqlConnection sqlConnection = sqlTransation == null ? SqlConnections.GetOpenConnection() : sqlTransation.Connection;

            int fId = sqlConnection.Insert(new FundsFlow()
            {
                Amount = overBorrow.UnPrincipal,
                IncomeGodId = 2,
                FeeType = FeeType.本金,
                IsComputing = true,
                PayGodId = overBorrow.BorrowerId,
                RelationId = nRechargeId,
                LoanApplyId = loanapply.Id,
                IsFreeze = false,
                Remark = overBorrow.Id.ToString()
            }, sqlTransation);
            int fscId = sqlConnection.Insert(new FundsFlow()
            {
                Amount = overBorrow.UnTotalInterest - dStandardDeduction,
                IncomeGodId = 2,
                FeeType = FeeType.逾期借款利息,
                IsComputing = true,
                PayGodId = overBorrow.BorrowerId,
                RelationId = nRechargeId,
                LoanApplyId = loanapply.Id,
                IsFreeze = false,
                Remark = overBorrow.Id.ToString()
            }, sqlTransation);
            overBorrow.ActualRepaymentDate = DateTime.Now;
            overBorrow.FundsFlowId = fId;
            overBorrow.RepaymentPlanMode = RepaymentPlanMode.逾期还款;
            overBorrow.UnTotalInterest = 0;
            overBorrow.UnPrincipal = 0;
            sqlConnection.Update<Borrow>(overBorrow, sqlTransation);

            if (overBorrow.Stages == loanapply.Deadline)
            {
                loanapply.RepaymentStatus = CreditStatus.还款完成;
                loanapply.ClosingDate = DateTime.Now;
                loanapply.RepaymentPlanMode = RepaymentPlanMode.逾期还款;
                sqlConnection.Update<LoanApply>(loanapply, sqlTransation);
            }

            return fscId;
        }

        public int RentRepayment(Borrow borrow, LoanApply loanapply, int nRechargeId, decimal dStandardDeduction, SqlTransaction sqlTransation = null)
        {
            SqlConnection sqlConnection = sqlTransation == null ? SqlConnections.GetOpenConnection() : sqlTransation.Connection;

            int fId = sqlConnection.Insert(new FundsFlow()
            {
                Amount = borrow.UnTotalInterest - dStandardDeduction,
                IncomeGodId = 2,
                FeeType = FeeType.租金,
                IsComputing = true,
                PayGodId = borrow.BorrowerId,
                RelationId = nRechargeId,
                LoanApplyId = loanapply.Id,
                IsFreeze = false,
                Remark = borrow.Id.ToString()
            }, sqlTransation);
            if (loanapply.unDeposit > 0)
            {
                int fscId = sqlConnection.Insert(new FundsFlow()
                {
                    Amount = loanapply.unDeposit,
                    IncomeGodId = 2,
                    FeeType = FeeType.押金,
                    IsComputing = true,
                    PayGodId = loanapply.BorrowerId,
                    RelationId = nRechargeId,
                    LoanApplyId = loanapply.Id,
                    IsFreeze = false,
                    Remark = loanapply.Id.ToString()
                }, sqlTransation);

                loanapply.unDeposit = 0;
                sqlConnection.Update<LoanApply>(loanapply, sqlTransation);
            }
            borrow.ActualRepaymentDate = DateTime.Now;
            borrow.FundsFlowId = fId;
            borrow.RepaymentPlanMode = RepaymentPlanMode.正常还款;
            borrow.UnTotalInterest = 0;
            borrow.UnPrincipal = 0;
            sqlConnection.Update<Borrow>(borrow, sqlTransation);

            return fId;
        }

        public void Batch(DateTime nowDateTime)
        {
            try
            {
                using (var connection = SqlConnections.GetOpenConnection())
                {
                    connection.Open();
                    using (var sqltran = connection.BeginTransaction())
                    {
                        List<LoanApply> loanapplyList = connection.Query<LoanApply>(
                        "select * from LoanApply where IsValid= 1 and RepaymentStatus= @RepaymentStatus and BatchDate<@BatchDate",
                        new LoanApply { RepaymentStatus = CreditStatus.还款中, BatchDate = nowDateTime.Date }, sqltran).ToList();


                        foreach (var item in loanapplyList)
                        {
                            int i = 1;
                            List<Borrow> borrowList = connection.Search(new Borrow() { IsValid = true }, sqltran).Where(o => o.LoanApplyId == item.Id && (o.UnPrincipal + o.UnTotalInterest) > 0 && o.RepaymentDate.Value.Date < nowDateTime.Date).OrderBy(o => o.RepaymentDate).ThenBy(t => t.Stages).ToList();
                            foreach (var borrow in borrowList)
                            {
                                int nDays = DateDiffOfDay(borrow.RepaymentDate.Value.Date, nowDateTime.Date);
                                if (i == 1)
                                {
                                    if (nDays <= 10)
                                    {
                                        connection.Update(new Borrow()
                                        {
                                            Id = borrow.Id,
                                            OverDay = nDays
                                        }, sqltran);
                                    }
                                    else if (nDays > 10 && nDays <= 30)
                                    {
                                        connection.Update(new Borrow()
                                        {
                                            Id = borrow.Id,
                                            OverDay = nDays,
                                            OverInterest = 30,
                                            UnTotalInterest = borrow.Interest + 30,
                                        }, sqltran);
                                    }
                                    else if (nDays > 30 && nDays <= 60)
                                    {
                                        connection.Update(new Borrow()
                                        {
                                            Id = borrow.Id,
                                            OverDay = nDays,
                                            OverInterest = 110,
                                            UnTotalInterest = borrow.Interest + 110,
                                        }, sqltran);
                                    }
                                    else if (nDays > 60 && nDays <= 90)
                                    {
                                        connection.Update(new Borrow()
                                        {
                                            Id = borrow.Id,
                                            OverDay = nDays,
                                            OverInterest = 240,
                                            UnTotalInterest = borrow.Interest + 240,
                                        }, sqltran);
                                    }
                                    else
                                    {
                                        connection.Update(new Borrow()
                                        {
                                            Id = borrow.Id,
                                            OverDay = nDays,
                                            OverInterest = 370,
                                            UnTotalInterest = borrow.Interest + 370,
                                        }, sqltran);
                                    }
                                }
                                else
                                {
                                    connection.Update(new Borrow()
                                    {
                                        Id = borrow.Id,
                                        OverDay = nDays
                                    }, sqltran);
                                }
                                i++;

                            }
                            connection.Update(new LoanApply()
                            {
                                Id = item.Id,
                                BatchDate = DateTime.Now
                            }, sqltran);
                        }

                        sqltran.Commit();
                    }

                }
            }
            catch (Exception ex)
            {
                DebugLogger.LogDebugMessage(ex.Message);
            }

        }

        private int DateDiffOfDay(DateTime stratDateTime, DateTime endDateTime)
        {
            return new TimeSpan(endDateTime.Date.Ticks).Subtract(new TimeSpan(stratDateTime.Date.Ticks)).Days;
        }


        public void SmsRepaymentReminder(DateTime nowDateTime)
        {

            WechatPushMessage wechatpushMessage = new WechatPushMessage();
            //BorrowerService borrowerService = new BorrowerService();
            //Borrower borrower = borrowerService.GetById(1);
            //if (!string.IsNullOrEmpty(borrower.WeiXinId))
            //{
            //    wechatpushMessage.InstallationReminder(borrower.FullName, borrower.WeiXinId, "", "电单", "期", "5", "12");
            //}
            List<Borrow> borrowList = SqlConnections.GetOpenConnection().Query<Borrow, LoanApply, Borrow>(
            @"select * from Borrow b join LoanApply l on b.LoanApplyId = l.Id 
where b.IsValid= 1 and b.RepaymentPlanMode is null and l.RepaymentStatus= @RepaymentStatus and (b.UnPrincipal+ b.UnTotalInterest)>0",
            (borrow, loanapply) =>
            {
                borrow.LoanApply = loanapply;
                return borrow;
            }, new { RepaymentStatus = CreditStatus.还款中 }).ToList();

            #region 提前
            List<Borrow> tqBorrowList = borrowList.FindAll(o =>  o.RepaymentDate.Value.AddDays(-3).Date == nowDateTime.Date).ToList();

            foreach (var item in tqBorrowList)
            {
                string sContent = string.Format(@"尊敬的客户，您的电单车融资租赁本月应还金额为{0}元，还款日为{1}，请登录车1号公众号进行还款。详询020-89851216【车1号】", (item.UnPrincipal + item.UnTotalInterest).Value.ToString("N0"), item.RepaymentDate.Value.ToString("yyyy年MM月dd日"));
                QiyebaoSms qiyebaoSms = new QiyebaoSms();
                qiyebaoSms.SendSms(item.LoanApply.CreditPhone, sContent);

                BorrowerService borrowerService = new BorrowerService();
                Borrower borrower = borrowerService.GetById(item.BorrowerId);
                if (!string.IsNullOrEmpty(borrower.WeiXinId))
                {
                    wechatpushMessage.InstallationReminder(borrower.FullName, borrower.WeiXinId, item.RepaymentDate.Value.Month.ToString(), "电单车", "第" + item.Stages + "期/总" + item.TotalPeriod + "期", (item.UnPrincipal + item.UnTotalInterest).Value.ToString("N0"), item.RepaymentDate.Value.ToString("yyyy-MM-dd"));
                }

            }
            #endregion

            #region 当天
            List<Borrow> dtBorrowList = borrowList.FindAll(o => o.RepaymentDate.Value.Date == nowDateTime.Date).ToList();

            foreach (var item in dtBorrowList)
            {
                string sContent = string.Format(@"尊敬的客户，您的电单车融资租赁业务本月应还金额为{0}元，请务必在今日登录车1号公众号进行还款！详询020-89851216 【车1号】", (item.UnPrincipal + item.UnTotalInterest).Value.ToString("N0"));
                QiyebaoSms qiyebaoSms = new QiyebaoSms();
                qiyebaoSms.SendSms(item.LoanApply.CreditPhone, sContent);

                BorrowerService borrowerService = new BorrowerService();
                Borrower borrower = borrowerService.GetById(item.BorrowerId);
                if (!string.IsNullOrEmpty(borrower.WeiXinId))
                {
                    wechatpushMessage.InstallationReminder(borrower.FullName, borrower.WeiXinId, item.RepaymentDate.Value.Month.ToString(), "电单车", "第" + item.Stages + "期/总" + item.TotalPeriod + "期", (item.UnPrincipal + item.UnTotalInterest).Value.ToString("N0"), item.RepaymentDate.Value.ToString("yyyy-MM-dd"));
                }
            }
            #endregion

            #region 逾期
            List<Borrow> yqBorrowList = borrowList.FindAll(o => o.RepaymentDate.Value.AddDays(10).Date == nowDateTime.Date
                || o.RepaymentDate.Value.AddDays(30).Date == nowDateTime.Date || o.RepaymentDate.Value.AddDays(60).Date == nowDateTime.Date).ToList();

            foreach (var item in yqBorrowList)
            {
                string sContent = string.Format(@"尊敬的客户，您的电动车融资租赁业务已逾期{0}天，逾期将产生征信出现污点并面临诉讼风险，请您及时进行还款【车1号】", item.OverDay);
                QiyebaoSms qiyebaoSms = new QiyebaoSms();
                qiyebaoSms.SendSms(item.LoanApply.CreditPhone, sContent);

                BorrowerService borrowerService = new BorrowerService();
                Borrower borrower = borrowerService.GetById(item.BorrowerId);
                if (!string.IsNullOrEmpty(borrower.WeiXinId))
                {
                    wechatpushMessage.OverdueReminder(borrower.FullName, borrower.WeiXinId, item.RepaymentDate.Value.ToString("yyyy年MM月dd日"), item.OverDay.ToString(), (item.Principal + item.Interest).Value.ToString("N0") + "元", item.OverInterest.Value.ToString("N0") + "元", (item.UnPrincipal + item.UnTotalInterest).Value.ToString("N0") + "元");
                }
            }
            #endregion
        }
    }
}

using CheDaiBaoCommonService.Data;
using CheDaiBaoWeChatController.Interface;
using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Interface;
using Sigbit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatService.Service
{
    public partial class RechargeService
    {
        private static readonly object syncObject = new object();

        public void RechargeCompare(string OutTradeNo, string TransactionId, string Openid, int TotalFee, string BankType, string Attach)
        {
            RechargeService rechargeService = new RechargeService();
            BorrowerService borrowerService = new BorrowerService();
            Borrower borrower = borrowerService.GetAll().Find(o => o.WeiXinId == Openid);
            if (borrower == null)
            {
                throw new Exception("用户不存在");
            }
            Recharge recharge = rechargeService.Search(new Recharge()
            {
                OrderNumber = OutTradeNo,
                BorrowerId = borrower.Id,
                RechargeMode = RechargeMode.微信充值
            }).SingleOrDefault();

            if (recharge == null)
            {
                throw new Exception("无此充值订单号");
            }

            decimal decFactMoney = Convert.ToDecimal(TotalFee);

            // 将充值记录的金额修改为实际支付金额
            if (recharge.Amount.Value != decFactMoney)
            {
                throw new Exception("订单金额对应不上");
            }

            if (recharge.IsAudit == null)
            {
                Recharge newrecharge = rechargeService.Search(new Recharge()
                {
                    OrderNumber = OutTradeNo,
                    BorrowerId = borrower.Id,
                    RechargeMode = RechargeMode.微信充值,
                    TransactionId = TransactionId
                }).SingleOrDefault();
                if (newrecharge == null)
                {
                    rechargeService.AuditRechargeIsSuccess(recharge.Id, TransactionId, BankType, Attach, TotalFee);

                    QiyebaoSms qiyebaoSms = new QiyebaoSms();
                    string sContent = string.Format("恭喜您，本次融资租赁费支付成功，金额为{0}元，订单号{1}【车1号】", TotalFee / 100, TransactionId);
                    qiyebaoSms.SendSms(borrower.Phone, sContent);
                    WechatPushMessage wechatpushMessage = new WechatPushMessage();
                    //wechatpushMessage.SuccessfulReminder(borrower.FullName, borrower.WeiXinId, recharge.CreateTime.Value.ToString("yyyy年MM月dd日"), TotalFee / 100 + "元");

                    //sContent = string.Format("客户{0}已经支付融资租赁费，金额{1}元【车1号】", borrower.FullName, TotalFee / 100);
                    LoanApplyService loanapplyService = new LoanApplyService();
                    LoanApply loanapply = loanapplyService.Search(new LoanApply()
                    {
                        IsValid = true,
                        BorrowerId = borrower.Id
                    }).OrderByDescending(o => o.CreateTime).SingleOrDefault();


                    //qiyebaoSms.SendSms("13763395495", sContent);
                    //qiyebaoSms.SendSms("18665573095", sContent);
                    Borrower salesman = borrowerService.GetById(loanapply.SalesmanId);
                    //qiyebaoSms.SendSms(salesman.Phone, sContent);
                    if (loanapply.RepaymentStatus == CreditStatus.还款完成)
                    {
                        sContent = string.Format("尊敬的{0}客户您好，您在广州翼速融资租赁有限公司办理的电单车融资租赁业务已全部结清，感谢您对我们工作的支持。详询020-89851216【车1号】", borrower.FullName);
                        qiyebaoSms.SendSms(borrower.Phone, sContent);
                    }
                    if (!string.IsNullOrEmpty(salesman.WeiXinId))
                    {
                        wechatpushMessage.CustomerReminder(salesman.WeiXinId, borrower.FullName, (TotalFee / 100).ToString("N0") + "元", DateTime.Now.ToString("yyyy年MM月dd日"));
                    }
                    wechatpushMessage.CustomerReminder("oAh7kw5FFUFl4WagxnRqBCgB9ad8", borrower.FullName, (TotalFee / 100).ToString("N0") + "元", DateTime.Now.ToString("yyyy年MM月dd日"));
                    wechatpushMessage.CustomerReminder("oAh7kw5BX2e9UmVc-ahGQ02j8Qt8", borrower.FullName, (TotalFee / 100).ToString("N0") + "元", DateTime.Now.ToString("yyyy年MM月dd日"));
                }
                else
                {
                    throw new Exception("已有该微信支付订单交易");
                }
            }
        }

        /// <summary>
        /// 审核支付订单
        /// </summary>
        /// <param name="rechargeId"></param>
        /// <param name="judgeId"></param>
        public void AuditRechargeIsSuccess(int rechargeId, string TransactionId, string BankType, string Attach, int TotalFee)
        {
            lock (syncObject)
            {
                using (var connection = SqlConnections.GetOpenConnection())
                {
                    connection.Open();
                    using (var sqltran = connection.BeginTransaction())
                    {
                        Recharge recharge = connection.GetById<Recharge>(rechargeId, sqltran);
                        if (recharge.IsAudit == null)
                        {
                            FeeType feeType = recharge.RechargeMode == RechargeMode.后台充值 ? FeeType.后台充值 : FeeType.充值;
                            recharge.IsAudit = true;

                            recharge.Auditor = "微信";
                            recharge.AuditTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            recharge.AuditRemark = "微信支付回调";
                            recharge.TransactionId = TransactionId;
                            recharge.BankCardCode = BankType;

                            connection.Update(recharge, sqltran);

                            connection.Insert(new FundsFlow()
                            {
                                Amount = recharge.Amount / 100,
                                FeeType = feeType,
                                IncomeGodId = recharge.BorrowerId,
                                IsFreeze = false,
                                PayGodId = 3,
                                Remark = feeType + "",
                                IsComputing = true,
                                RelationId = rechargeId
                            }, sqltran);

                            if (recharge.ActualRechargeFee.Value > 0)
                            {
                                connection.Insert(new FundsFlow()
                                {
                                    Amount = recharge.ActualRechargeFee.Value / 100,
                                    FeeType = FeeType.第三方收取的充值手续费,
                                    IncomeGodId = 3,
                                    PayGodId = 2,
                                    IsComputing = true,
                                    IsFreeze = false,
                                    RelationId = recharge.Id,
                                    Remark = string.Format("支付给{0}的充值手续费", recharge.RechargeMode)
                                }, sqltran);
                            }

                            BorrowService borrowService = new BorrowService();
                            borrowService.Repayment(TotalFee, rechargeId, Attach, sqltran);
                        }
                        sqltran.Commit();
                    }
                    connection.Close();
                }

            }

        }

        public List<Recharge> MonthlyRepayment()
        {
            return
            SqlConnections.GetOpenConnection().Query<Recharge>(@"
            select SUM(Amount) as Amount,convert(varchar(7),AuditTime,120) AS syear, Month(AuditTime) AS nmonth 
 from Recharge where IsValid= 1 and IsAudit = 1 group by convert(varchar(7),AuditTime,120), Month(AuditTime)").ToList();
        }
    }
}

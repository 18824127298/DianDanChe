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
using WxPayApi;

public partial class caiwu_caiwu_zhidan_deposit_refund_audit : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        //========== 1. 初始化界面 ==========
        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. 新增的判断和处理 ==========


        //========== 4. 取数据 ==========
        LoanApplyService loanapplyService = new LoanApplyService();
        LoanApply loanapply = loanapplyService.GetById(ConvertUtil.ToInt(sRecordPrimaryKey));

        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(loanapply.BorrowerId);


        //========== 5. 更新各控件的显示 ==========
        lblFullname.Text = borrower.FullName;
        edtAmount.Text = loanapply.Deposit.Value.ToString("N0");
    }

    protected void btnAudit_Click(object sender, EventArgs e)
    {
        SbtMessageBoxPage msgPage = new SbtMessageBoxPage(this);
        msgPage.ReturnUrl = "~/caiwu/caiwu_zhidan/deposit_refund_list.aspx";
        int nRecordPrimaryKey = ConvertUtil.ToInt(ViewState["rec_key"]);

        string sRemark = edtRemark.Text.Trim();
        if (sRemark == "")
        {
            msgPage.MessageText = "备注不能为空";
            msgPage.Show();
            return;
        }

        LoanApplyService loanapplyService = new LoanApplyService();
        BorrowerService borrowerService = new BorrowerService();
        LoanApply loanapply = loanapplyService.GetById(ConvertUtil.ToInt(ConvertUtil.ToInt(nRecordPrimaryKey)));
        Borrower borrower = borrowerService.GetById(loanapply.BorrowerId);
        FundsFlowService fundsflowService = new FundsFlowService();
        FundsFlow payfundsflow = fundsflowService.Search(new FundsFlow() { IsValid = true }).Where(o => o.PayGodId == borrower.Id && o.FeeType == FeeType.押金 && o.IsFreeze == false).FirstOrDefault();

        if (payfundsflow == null)
        {
            msgPage.MessageText = "该用户没有押金资金流";
            msgPage.Show();
            return;
        }
        else
        {
            decimal? dAmount = 0;
            FundsFlow incomefundsflow = fundsflowService.Search(new FundsFlow() { IsValid = true }).Where(o => o.IncomeGodId == borrower.Id && o.FeeType == FeeType.押金 && o.IsFreeze == false).FirstOrDefault();
            if (incomefundsflow == null)
            {
                dAmount = payfundsflow.Amount;
            }
            else
            {
                dAmount = payfundsflow.Amount - incomefundsflow.Amount;
            }
            if (dAmount == 0)
            {
                msgPage.MessageText = "该用户已经押金退款，请勿重复退款";
                msgPage.Show();
                return;
            }
            else
            {
                RefundService refundService = new RefundService();

                string sRecharegAlias = borrower.Phone;


                RechargeService rechargeService = new RechargeService();
                Recharge recharge = rechargeService.GetById(payfundsflow.RelationId);
                if (recharge == null)
                {
                    msgPage.MessageText = "找不到该笔充值记录";
                    msgPage.Show();
                    return;
                }
                if (recharge.IsAudit != true)
                {
                    msgPage.MessageText = "该充值订单未审核或者审核不通过";
                    msgPage.Show();
                    return;
                }

                try
                {
                    Random r = new Random();
                    string out_refund_no = DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(0, 99999);

                    string result = WxPayApi.Refund.Run(recharge.TransactionId, recharge.OrderNumber, recharge.Amount.Value.ToString("N0"), (ConvertUtil.ToDecimal(edtAmount.Text) * 100).ToString("N0"), out_refund_no);

                    DebugLogger.LogDebugMessage("单号" + out_refund_no + "押金回退结果" + result);


                    WxPayData data = new WxPayData();
                    data.FromXml(result);
                    if (data.GetValue("return_code").ToString() == "SUCCESS")
                    {
                        CheDaiBaoWeChatModel.Models.Refund refund = new CheDaiBaoWeChatModel.Models.Refund();
                        refund.Amount = dAmount;
                        refund.BorrowerId = borrower.Id;
                        refund.ReChargeId = payfundsflow.RelationId;
                        refund.FundsFlowId = payfundsflow.Id;
                        refund.Auditor = CurrentUser.RealName;
                        refund.AuditRemark = sRemark;
                        refund.IsAudit = true;
                        refund.AuditTime = DateTime.Now;
                        refund.OutRefundNo = data.GetValue("out_refund_no").ToString();
                        refund.RefundId = data.GetValue("refund_id").ToString();
                        refund.RefundTime = DateTime.Now;

                        refundService.Insert(refund);

                        fundsflowService.Insert(new FundsFlow()
                        {
                            Amount = refund.Amount,
                            FeeType = FeeType.押金回退,
                            IncomeGodId = recharge.BorrowerId,
                            IsFreeze = false,
                            PayGodId = 2,
                            Remark = "退回用户押金",
                            IsComputing = true,
                            RelationId = refund.Id
                        });

                        loanapply.unDeposit = -1;
                        loanapplyService.Update(loanapply);

                        OperaService operaService = new OperaService();
                        operaService.Insert(new Opera()
                        {
                            OperaType = OperaType.押金回退操作,
                            BorrowerId = borrower.Id,
                            RelationId = nRecordPrimaryKey,
                            Remark = sRemark,
                            ClientAddress = Request.UserHostAddress
                        });

                        SbtAppLogger.LogAction("押金退回审核通过", string.Format("退回用户{0},退回金额{1},退回时间{2},操作备注:{3};",
                            borrower.Aliases, refund.Amount.Value.ToString("0.00"), refund.AuditTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), sRemark));


                        msgPage.MessageText = "押金退回审核通过";
                        msgPage.Show();
                    }
                    else
                    {
                        msgPage.MessageText = "押金退回审核未通过" + data.GetValue("err_code_des").ToString();
                        msgPage.Show();
                    }

                }
                catch (WxPayException ex)
                {
                    msgPage.MessageText = ex.ToString();
                    msgPage.Show();
                    return;
                }
                catch (Exception ex)
                {
                    msgPage.MessageText = ex.ToString();
                    msgPage.Show();
                    return;
                }
            }
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("deposit_refund_list.aspx");
    }

}
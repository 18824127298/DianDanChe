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
        RefundService refundService = new RefundService();
        CheDaiBaoWeChatModel.Models.Refund refund = refundService.GetById(ConvertUtil.ToInt(sRecordPrimaryKey));

        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(refund.BorrowerId);


        //========== 5. 更新各控件的显示 ==========
        lblFullname.Text = borrower.FullName;
        lblAmount.Text = refund.Amount.Value.ToString("N0");
    }

    protected void btnAudit_Click(object sender, EventArgs e)
    {
        SbtMessageBoxPage msgPage = new SbtMessageBoxPage(this);
        msgPage.ReturnUrl = "~/caiwu/caiwu_zhidan/deposit_refund_list.aspx";
        int nRecordPrimaryKey = ConvertUtil.ToInt(ViewState["rec_key"]);

        string sRemark = edtRemark.Text.Trim();
        if (sRemark == "")
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "备注不能为空！";
            edtRemark.Focus();
            return;
        }
        RefundService refundService = new RefundService();
        CheDaiBaoWeChatModel.Models.Refund refund = refundService.GetById(ConvertUtil.ToInt(nRecordPrimaryKey));

        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(refund.BorrowerId);

        string sRecharegAlias = borrower.Phone;


        //if (refund.IsAudit != null)
        //{
        //    msgPage.MessageText = "该记录已经审核操作过了，无需再进行审核操作";
        //    msgPage.Show();
        //    return;
        //}

        RechargeService rechargeService = new RechargeService();
        Recharge recharge = rechargeService.GetById(refund.ReChargeId);
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

            string result = WxPayApi.Refund.Run(recharge.TransactionId, recharge.OrderNumber, recharge.Amount.Value.ToString("N0"), (refund.Amount * 100).Value.ToString("N0"), out_refund_no);

            DebugLogger.LogDebugMessage("单号" + out_refund_no + "押金回退结果" + result);


            WxPayData data = new WxPayData();
            data.FromXml(result);
            if (data.GetValue("return_code").ToString() == "SUCCESS")
            {
                refund.Auditor = CurrentUser.RealName;
                refund.AuditRemark = sRemark;
                refund.IsAudit = true;
                refund.AuditTime = DateTime.Now;
                refund.OutRefundNo = data.GetValue("out_refund_no").ToString();
                refund.RefundId = data.GetValue("refund_id").ToString();
                refund.RefundTime = DateTime.Now;

                refundService.Update(refund);

                FundsFlowService fundsflowService = new FundsFlowService();

                fundsflowService.Update(new FundsFlow()
                {
                    Id = refund.FundsFlowId,
                    IsFreeze = false
                });
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
                refund.Auditor = CurrentUser.RealName;
                refund.AuditRemark = sRemark;
                refund.IsAudit = false;
                refund.AuditTime = DateTime.Now;
                refund.OutRefundNo = out_refund_no;
                refund.Reason = data.GetValue("err_code_des").ToString();

                refundService.Update(refund);

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

    protected void btnUnAudit_Click(object sender, EventArgs e)
    {
        SbtMessageBoxPage msgPage = new SbtMessageBoxPage(this);
        msgPage.ReturnUrl = "~/caiwu/caiwu_zhidan/deposit_refund_list.aspx";

        int nRecordPrimaryKey = ConvertUtil.ToInt(ViewState["rec_key"]);

        string sRemark = edtRemark.Text.Trim();
        if (sRemark == "")
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "请填写不通过原因！";
            edtRemark.Focus();
            return;
        }

        RechargeService rechargeService = new RechargeService();
        Recharge recharge = rechargeService.GetById(nRecordPrimaryKey);


        BorrowerService borrowerService = new BorrowerService();
        Borrower borrower = borrowerService.GetById(recharge.BorrowerId);

        string sRecharegAlias = borrower.Phone;

        recharge.Auditor = CurrentUser.RealName;
        recharge.AuditRemark = sRemark;
        recharge.IsAudit = false;
        recharge.AuditTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        rechargeService.Update(recharge);




        SbtAppLogger.LogAction("充值审核不通过", string.Format("充值用户{0},充值金额{1},充值时间{2},不通过原因:{3};",
              borrower.Aliases, recharge.Amount.Value.ToString("0.00"), recharge.RechargeTime, sRemark));

        NaviTabController.ClearBar();
        //========== 4. 返回到主页面 ==========
        msgPage.MessageText = "充值审核不通过操作完成";
        msgPage.Show();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("deposit_refund_list.aspx");
    }

}
using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using JiaYouWxPayApi;
using Sigbit.Common;
using Sigbit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class youka_pay_deposit_refund_audit : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Action"] != null)
        {
            Response.ContentType = "text/plain";
            string Action = Request.QueryString["Action"];
            switch (Action)
            {
                case "Save":
                    Response.Write(Save(Request.QueryString["Id"], Request.QueryString["Amount"], Request.QueryString["Remark"]));
                    break;
            }
            Response.End();
        }

        if (IsPostBack)
            return;
        //========== 1. 初始化界面 ==========
        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. 新增的判断和处理 ==========


        //========== 4. 取数据 ==========
        PaymentFormService paymentFormService = new PaymentFormService();
        PaymentForm paymentForm = paymentFormService.GetById(ConvertUtil.ToInt(sRecordPrimaryKey));

        MemberService memberService = new MemberService();
        Member member = memberService.GetById(paymentForm.MemberId.Value);


        //========== 5. 更新各控件的显示 ==========
        lblPhone.Text = member.Phone;
        lblAmount.Text = paymentForm.ActualAmount.Value.ToString("N2");
        vid.Value = sRecordPrimaryKey;
    }
    public string Save(string sId, string sAmount, string sRemark)
    {
        if (sRemark == "")
        {
            return "备注不能为空！";
        }
        PaymentFormService paymentFormService = new PaymentFormService();
        PaymentForm paymentForm = paymentFormService.GetById(ConvertUtil.ToInt(sId));

        TimeSpan midTime = DateTime.Now - paymentForm.CreateTime.Value;
        if (midTime.TotalMinutes > 30)
        {
            return "距离支付时间大于三十分钟，已无法退款";
        }
        MemberService memberService = new MemberService();
        Member member = memberService.GetById(paymentForm.MemberId.Value);

        string sRecharegAlias = member.Phone;

        if (paymentForm.IsAudit != true)
        {
            return "该充值订单未审核或者审核不通过";
        }

        if (ConvertUtil.ToDecimal(sAmount) != paymentForm.ActualAmount)
        {
            return "退款金额与实际支付金额不对等！";
        }
        try
        {
            Random r = new Random();
            string out_refund_no = DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(0, 99999);

            string result = JiaYouWxPayApi.Refund.Run(paymentForm.TransactionId, paymentForm.OrderNumber, (paymentForm.ActualAmount * 100).Value.ToString("N0").Replace(",", ""), (paymentForm.ActualAmount * 100).Value.ToString("N0").Replace(",", ""), out_refund_no);

            DebugLogger.LogDebugMessage("单号" + out_refund_no + "押金回退结果" + result);


            WxPayData data = new WxPayData();
            data.FromXml(result);
            if (data.GetValue("return_code").ToString() == "SUCCESS")
            {
                paymentForm.RefundAuditor = CurrentUser.RealName;
                paymentForm.RefundRemark = sRemark;
                paymentForm.IsAudit = false;
                paymentForm.IsRefundAudit = true;
                paymentForm.OutRefundNo = data.GetValue("out_refund_no").ToString();
                paymentForm.RefundId = data.GetValue("refund_id").ToString();
                paymentForm.RefundTime = DateTime.Now;
                paymentForm.RefundAmount = ConvertUtil.ToDecimal(sAmount);

                paymentFormService.Update(paymentForm); 

                GasStationService gasstationService = new GasStationService();
                GasStation gasstation = gasstationService.GetById(paymentForm.GasStationId.Value);
                SupplierService supplierService = new SupplierService();
                Supplier supplier = supplierService.GetById(gasstation.SupplierId.Value);
                SupplierFundsFlowService spplierFundsFlowService = new SupplierFundsFlowService();
                int nRelationId = spplierFundsFlowService.Insert(new SupplierFundsFlow()
                {
                    Amount = paymentForm.ActualAmount,
                    FeeType = FeeType.退费,
                    IncomeSupplierId = supplier.Id,
                    PaySupplierId = 1,
                    IsComputing = true,
                    IsFreeze = false,
                    Remark = "后台退费"
                });


                supplier.Balance += paymentForm.ActualAmount;
                supplierService.Update(supplier);

                SbtAppLogger.LogAction("押金退回审核通过", string.Format("退回用户{0},退回金额{1},退回时间{2},操作备注:{3};",
                    member.Phone, ConvertUtil.ToDecimal(sAmount), paymentForm.RefundTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), sRemark));

                return "退款操作成功";
            }
            else
            {
                paymentForm.RefundTime = DateTime.Now;
                paymentForm.RefundAmount = ConvertUtil.ToDecimal(sAmount);
                paymentForm.RefundAuditor = CurrentUser.RealName;
                paymentForm.RefundRemark = sRemark;
                paymentForm.IsRefundAudit = false;
                paymentForm.OutRefundNo = out_refund_no;
                paymentForm.Reason = data.GetValue("err_code_des").ToString();

                paymentFormService.Update(paymentForm);

                return "押金退回审核未通过" + data.GetValue("err_code_des").ToString();
            }

        }
        catch (WxPayException ex)
        {
            return ex.ToString();
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("gasstation_success_list.aspx");
    }
}
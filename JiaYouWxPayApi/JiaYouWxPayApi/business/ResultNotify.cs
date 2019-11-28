using CheDaiBaoWeChatService.Service;
using Sigbit.Common;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JiaYouWxPayApi
{
    /// <summary>
    /// 支付结果通知回调处理类
    /// 负责接收微信支付后台发送的支付结果并对订单有效性进行验证，将验证结果反馈给微信支付后台
    /// </summary>
    public class ResultNotify : Notify
    {
        public ResultNotify(Page page)
            : base(page)
        {
        }

        public override void ProcessNotify()
        {
            WxPayData notifyData = GetNotifyData();

            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                DebugLogger.LogDebugMessage(this.GetType().ToString() + "The Pay result is error : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }

            string transaction_id = notifyData.GetValue("transaction_id").ToString();

            //查询订单，判断订单真实性
            if (!QueryOrder(transaction_id))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                DebugLogger.LogDebugMessage(this.GetType().ToString() + "order query failure : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }
            //查询订单成功
            else
            {
                WxPayData res = new WxPayData();

                //跟本地订单作比较
                try
                {
                    if (notifyData.GetValue("attach").ToString() == "加油")
                    {
                        PaymentFormService paymentFormService = new PaymentFormService();
                        paymentFormService.PaymentFormCompare(notifyData.GetValue("out_trade_no").ToString(), notifyData.GetValue("transaction_id").ToString(),
                            notifyData.GetValue("openid").ToString(), ConvertUtil.ToInt(notifyData.GetValue("total_fee")));
                        res.SetValue("return_code", "SUCCESS");
                        res.SetValue("return_msg", "OK");
                        DebugLogger.LogDebugMessage(this.GetType().ToString() + "order query success : " + res.ToXml());
                        page.Response.Write(res.ToXml());
                    }
                    else
                    {
                        RechargeService rechargeService = new RechargeService();
                        rechargeService.RechargeCompare(notifyData.GetValue("out_trade_no").ToString(), notifyData.GetValue("transaction_id").ToString(),
                            notifyData.GetValue("openid").ToString(), ConvertUtil.ToInt(notifyData.GetValue("total_fee")), notifyData.GetValue("bank_type").ToString(), notifyData.GetValue("attach").ToString());
                        res.SetValue("return_code", "SUCCESS");
                        res.SetValue("return_msg", "OK");
                        DebugLogger.LogDebugMessage(this.GetType().ToString() + "order query success : " + res.ToXml());
                        page.Response.Write(res.ToXml());
                    }
                }
                catch (Exception ex)
                {
                    res.SetValue("return_code", "FAIL");
                    res.SetValue("return_msg", ex.Message);
                    DebugLogger.LogDebugMessage(this.GetType().ToString() + "order query failure : " + ex.Message);
                    page.Response.Write(res.ToXml());
                }
                finally
                {
                    page.Response.End();
                }
            }
        }

        //查询订单
        private bool QueryOrder(string transaction_id)
        {
            WxPayData req = new WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayData res = JiaYouWxPayApi.OrderQuery(req);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
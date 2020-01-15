using CheDaiBaoCommonService.Data;
using CheDaiBaoCommonService.Expansion;
using CheDaiBaoCommonService.Service;
using CheDaiBaoWeChatController.Interface;
using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatService.Service
{
    public partial class PaymentFormService
    {
        private static readonly object syncObject = new object();

        public void PaymentFormCompare(string OutTradeNo, string TransactionId, string Openid, int TotalFee)
        {
            PaymentFormService paymentFormService = new PaymentFormService();
            MemberService memberService = new MemberService();
            Member member = memberService.GetAll().Find(o => o.OpenId == Openid);
            if (member == null)
            {
                throw new Exception("用户不存在");
            }
            PaymentForm paymentForm = paymentFormService.Search(new PaymentForm()
            {
                OrderNumber = OutTradeNo,
                MemberId = member.Id
            }).SingleOrDefault();

            if (paymentForm == null)
            {
                throw new Exception("无此支付订单号");
            }

            decimal decFactMoney = Convert.ToDecimal(TotalFee);

            if (paymentForm.ActualAmount.Value * 100 != decFactMoney)
            {
                throw new Exception("订单金额对应不上");
            }

            if (paymentForm.IsAudit == null)
            {
                PaymentForm newpaymentForm = paymentFormService.Search(new PaymentForm()
                {
                    TransactionId = TransactionId
                }).SingleOrDefault();
                if (newpaymentForm == null)
                {
                    paymentFormService.AuditRechargeIsSuccess(paymentForm.Id, TransactionId);

                    GodBounsService godbounsService = new GodBounsService();
                    GodBouns godbouns = godbounsService.Search(new GodBouns() { IsValid = true }).Where(o => o.RelationId == paymentForm.MemberId && o.BounsStatus == BounsStatus.未激活 && o.BounsType == BounsType.推荐优惠券 && o.MemberId == member.RecommendId).FirstOrDefault();
                    if (godbouns != null)
                    {
                        godbouns.BounsStatus = BounsStatus.未使用;
                        godbouns.ExpireTime = DateTime.Now.AddMonths(1);
                        godbounsService.Update(godbouns);
                    }

                    if (paymentForm.GodbounsAmount > 0)
                    {
                        GodBounsRecordService godbounsRecordService = new GodBounsRecordService();
                        GodBounsRecord godbounsRecord = godbounsRecordService.Search(new GodBounsRecord() { IsValid = true }).Where
                        (o => o.MemberId == member.Id && o.RelationId == paymentForm.Id && o.UseAmount == paymentForm.GodbounsAmount && o.UseType == BounsUseType.支付).FirstOrDefault();
                        godbounsRecord.IsEffective = true;
                        godbounsRecordService.Update(godbounsRecord);

                        GodBouns memberGodBouns = godbounsService.GetById(godbounsRecord.BounsId.Value);
                        memberGodBouns.LeftAmount = 0;
                        memberGodBouns.BounsStatus = BounsStatus.已用完;
                        godbounsService.Update(memberGodBouns);
                    }
                    //QiyebaoSms qiyebaoSms = new QiyebaoSms();
                    //string sContent = string.Format("恭喜您，本次融资租赁费支付成功，金额为{0}元，订单号{1}【车1号】", paymentForm.ActualAmount.Value.ToString("N2"), TransactionId);
                    //qiyebaoSms.SendSms(member.Phone, sContent);

                    //***返回值JSON字符串***
                    //成功：{"msg":"ok","ret":0,"data":"xxxxxxx_xxxxxxxx_xxxxxxxx","serverExecutedTime":5}
                    //失败：{"msg":"错误描述","ret":非0,"data":"null","serverExecutedTime":5}
                    GasStationService gasStationService = new GasStationService();
                    GasStation gasStation = gasStationService.GetById(paymentForm.GasStationId.Value);
                    OilGunService oilgunService = new OilGunService();
                    OilGun oilgun = oilgunService.GetById(paymentForm.OilGunId.Value);

                    string sResultprint = print(gasStation.PrinterNumber, paymentForm.OrderNumber, paymentForm.PayTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), Expansions.EncruptionSectionMobile(member.Phone), oilgun.GunNumber.ToString(), oilgun.OilNumber, paymentForm.RiseNumber.Value.ToString("N2"), paymentForm.GasStationAmount.Value.ToString("N2"), gasStation.Name);
                    string ret = WeChatBaseRequestService.GetJsonValue(sResultprint, "ret");
                    if (ret == "0")
                    {
                        paymentForm.IsPrint = true;
                        paymentFormService.Update(paymentForm);
                        WechatPushMessage wechatpushMessage = new WechatPushMessage();
                        wechatpushMessage.OilPaymentReminder(Openid, paymentForm.OrderNumber, paymentForm.ActualAmount.Value.ToString("N2") + "元", gasStation.Name, paymentForm.CreateTime.Value.ToString("yyyy年MM月dd日 HH:mm:ss"));
                    }

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
        public void AuditRechargeIsSuccess(int paymentFormId, string TransactionId)
        {
            lock (syncObject)
            {
                using (var connection = SqlConnections.GetOpenConnection())
                {
                    connection.Open();
                    using (var sqltran = connection.BeginTransaction())
                    {
                        PaymentForm paymentForm = connection.GetById<PaymentForm>(paymentFormId, sqltran);
                        if (paymentForm.IsAudit == null)
                        {
                            paymentForm.IsAudit = true;

                            paymentForm.AuditTime = DateTime.Now;
                            paymentForm.TransactionId = TransactionId;
                            connection.Update(paymentForm, sqltran);
                        }
                        GasStation gasStation = connection.GetById<GasStation>(paymentForm.GasStationId.Value, sqltran);
                        Member member = connection.GetById<Member>(paymentForm.MemberId.Value, sqltran);
                        Supplier supplier = connection.GetById<Supplier>(gasStation.SupplierId.Value, sqltran);

                        connection.Insert(new SupplierFundsFlow()
                        {
                            Amount = paymentForm.SupplierAmount.Value,
                            FeeType = FeeType.支付,
                            IncomeSupplierId = 1,
                            PaySupplierId = supplier.Id,
                            IsComputing = true,
                            IsFreeze = false,
                            RelationId = paymentForm.Id,
                            Remark = "供应商支出"
                        }, sqltran);

                        supplier.Balance = supplier.Balance - paymentForm.SupplierAmount.Value;
                        connection.Update<Supplier>(supplier, sqltran);
                        sqltran.Commit();
                    }
                    connection.Close();
                }

            }

        }


        public List<PaymentForm> GetPaymentFormByMemberId(int memberId, Boolean? IsAudit = null, SqlTransaction sqltran = null)
        {
            if (IsAudit == null)
            {
                return SqlConnections.GetOpenConnection().Query<PaymentForm, GasStation, OilGun, PaymentForm>(
                    @"
                select * from PaymentForm p join GasStation g on p.GasStationId = g.Id
join OilGun o on p.OilGunId = o.Id where p.IsValid= 1 and p.MemberId = @MemberId",
                    (paymentForm, gasStation, oilGun) =>
                    {
                        paymentForm.GasStation = gasStation;
                        paymentForm.OilGun = oilGun;
                        return paymentForm;
                    },
                    new { MemberId = memberId }, sqltran).ToList();
            }
            else if (IsAudit == true)
            {
                return SqlConnections.GetOpenConnection().Query<PaymentForm, GasStation, OilGun, PaymentForm>(
                    @"
                select * from PaymentForm p join GasStation g on p.GasStationId = g.Id
join OilGun o on p.OilGunId = o.Id where p.IsValid= 1 and p.MemberId = @MemberId and p.IsAudit = @IsAudit",
                    (paymentForm, gasStation, oilGun) =>
                    {
                        paymentForm.GasStation = gasStation;
                        paymentForm.OilGun = oilGun;
                        return paymentForm;
                    },
                    new { MemberId = memberId, IsAudit = IsAudit }, sqltran).ToList();
            }
            else
            {
                return SqlConnections.GetOpenConnection().Query<PaymentForm, GasStation, OilGun, PaymentForm>(
                    @"
                select * from PaymentForm p join GasStation g on p.GasStationId = g.Id
join OilGun o on p.OilGunId = o.Id where p.IsValid= 1 and p.MemberId = @MemberId and (p.IsAudit is null or p.IsAudit = 0)",
                    (paymentForm, gasStation, oilGun) =>
                    {
                        paymentForm.GasStation = gasStation;
                        paymentForm.OilGun = oilGun;
                        return paymentForm;
                    },
                    new { MemberId = memberId }, sqltran).ToList();
            }
        }


        public PaymentForm GetPaymentFormById(int Id, SqlTransaction sqltran = null)
        {
            return SqlConnections.GetOpenConnection().Query<PaymentForm, GasStation, OilGun, PaymentForm>(
                @"
                select * from PaymentForm p join GasStation g on p.GasStationId = g.Id
join OilGun o on p.OilGunId = o.Id where p.IsValid= 1 and p.Id = @Id",
                (paymentForm, gasStation, oilGun) =>
                {
                    paymentForm.GasStation = gasStation;
                    paymentForm.OilGun = oilGun;
                    return paymentForm;
                },
                new { Id = Id }, sqltran).Single();
        }
        //**********************请先填打印机编号和KEY，再测试**************************
        public static string USER = "357454@qq.com";  //*必填*：登录管理后台的账号名
        public static string UKEY = "zEfpmArLngwTyzVV";//*必填*: 注册账号后生成的UKEY

        public static string URL = "http://api.feieyun.cn/Api/Open/";//不需要修改

        //方法1
        private static string print(string SN, string sOrderNumber, string PayTime, string Phone, string GunNumber, string OilNumber, string RiseNumber, string Amount, string GasStationName)
        {
            //标签说明：
            //单标签: 
            //"<BR>"为换行,"<CUT>"为切刀指令(主动切纸,仅限切刀打印机使用才有效果)
            //"<LOGO>"为打印LOGO指令(前提是预先在机器内置LOGO图片),"<PLUGIN>"为钱箱或者外置音响指令
            //成对标签：
            //"<CB></CB>"为居中放大一倍,"<B></B>"为放大一倍,"<C></C>"为居中,<L></L>字体变高一倍
            //<W></W>字体变宽一倍,"<QR></QR>"为二维码,"<BOLD></BOLD>"为字体加粗,"<RIGHT></RIGHT>"为右对齐

            //拼凑订单内容时可参考如下格式
            string orderInfo;
            orderInfo = "<C>" + GasStationName + "</C><BR>";//标题字体如需居中放大,就需要用标签套上
            orderInfo += "<C>（加油站存根）</C><BR>";
            orderInfo += "--------------------------------<BR>";
            orderInfo += "<BOLD>流水号　" + sOrderNumber + "</BOLD><BR>";
            orderInfo += "--------------------------------<BR>";
            orderInfo += "交易时间　" + PayTime + "<BR>";
            orderInfo += "电话　　　" + Phone + "<BR>";
            orderInfo += "来源　　　车1号<BR>";
            orderInfo += "油枪　　　" + GunNumber + "号枪<BR>";
            orderInfo += "油号　　　" + OilNumber + "<BR>";
            orderInfo += "升数　　　" + RiseNumber + "升<BR>";
            orderInfo += "--------------------------------<BR>";
            orderInfo += "<B>金额：" + Amount + "元</B><BR>";
            orderInfo += "--------------------------------<BR>";

            orderInfo = Uri.EscapeDataString(orderInfo);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(URL);
            req.Method = "POST";
            UTF8Encoding encoding = new UTF8Encoding();

            string postData = "sn=" + SN;
            postData += ("&content=" + orderInfo);
            postData += ("&times=" + "1");//默认1联

            int itime = DateTimeToStamp(System.DateTime.Now);//时间戳秒数
            string stime = itime.ToString();
            string sig = sha1(USER, UKEY, stime);

            //公共参数
            postData += ("&user=" + USER);
            postData += ("&stime=" + stime);
            postData += ("&sig=" + sig);
            postData += ("&apiname=" + "Open_printMsg");

            byte[] data = encoding.GetBytes(postData);

            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;
            Stream resStream = req.GetRequestStream();

            resStream.Write(data, 0, data.Length);
            resStream.Close();

            HttpWebResponse response;
            string strResult;
            try
            {
                response = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                strResult = reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                strResult = response.StatusCode.ToString();//错误信息
            }

            response.Close();
            req.Abort();
            //服务器返回的JSON字符串，建议要当做日志记录起来
            return strResult;

        }

        //签名USER,UKEY,STIME
        public static string sha1(string user, string ukey, string stime)
        {
            var buffer = Encoding.UTF8.GetBytes(user + ukey + stime);
            var data = SHA1.Create().ComputeHash(buffer);

            var sb = new StringBuilder();
            foreach (var t in data)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString().ToLower();

        }


        private static int DateTimeToStamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); return (int)(time - startTime).TotalSeconds;
        }


    }
}

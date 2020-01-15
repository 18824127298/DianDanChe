using CheDaiBaoCommonController.Controllers;
using CheDaiBaoCommonService.Models;
using CheDaiBaoWeChatController.Controller;
using CheDaiBaoWeChatModel;
using CheDaiBaoWeChatModel.Models;
using CheDaiBaoWeChatService.Service;
using JiaYouWxPayApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CheDaiBaoWeChatController.Controllers
{
    public class YouKaController : BaseCommonController
    {
        public ActionResult AllGasStation()
        {
            MemberService memberService = new MemberService();
            WeiXinModel WeiXin = (WeiXinModel)System.Web.HttpContext.Current.Session["WeiXin"];
            Member member = memberService.Search(new Member() { IsValid = true }).Where(o => o.OpenId == WeiXin.OpenId && !string.IsNullOrEmpty(o.Phone)).FirstOrDefault();
            if (member != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Create", "Member");
            }
        }

        [HttpPost]
        public string GetWXSharedParam(string url)
        {
            string timestamp = WxSharedController.ConvertDateTimeInt(DateTime.Now).ToString();
            string nonceStr = Guid.NewGuid().ToString();
            string ticket = string.Empty;
            string appId = Configs.GetWeiXinAppId();

            //获取jsapi_ticket
            if (HttpRuntime.Cache["JsApiTicket"] == null)
            {
                WxSharedController.GetJsApiTicket();
            }
            ticket = HttpRuntime.Cache["JsApiTicket"] as string;
            if (string.IsNullOrEmpty(ticket))
            {
                return JsonConvert.SerializeObject(new { result = false });
            }

            SortedList<string, string> SLString = new SortedList<string, string>();
            SLString.Add("noncestr", nonceStr);
            SLString.Add("url", url);
            SLString.Add("timestamp", timestamp);
            SLString.Add("jsapi_ticket", ticket);

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> des in SLString)
            {
                sb.Append(des.Key + "=" + des.Value + "&");
            }
            string signature = sb.ToString().Substring(0, sb.ToString().Length - 1);
            signature = FormsAuthentication.HashPasswordForStoringInConfigFile(signature, "SHA1").ToLower();
            return JsonConvert.SerializeObject(new { result = true, timestamp, nonceStr, signature, appId });
        }


        [HttpPost]
        public ActionResult GetGasStation(string latitude, string longitude, string juli, string youhao, string youxian, string pinpai)
        {
            OilGunService oilGunService = new OilGunService();
            MemberService memberService = new MemberService();
            WeiXinModel WeiXin = (WeiXinModel)System.Web.HttpContext.Current.Session["WeiXin"];
            Member member = memberService.Search(new Member() { IsValid = true }).Where(o => o.OpenId == WeiXin.OpenId).FirstOrDefault();
            GasStationService gasStationService = new GasStationService();
            GasStationLevelService gasStationLevelService = new GasStationLevelService();
            if (youhao == "1")
                youhao = "92#汽油";
            if (youhao == "2")
                youhao = "95#汽油";
            if (youhao == "3")
                youhao = "98#汽油";
            if (youhao == "4")
                youhao = "0#柴油";
            List<GasStation> gasStationList = gasStationService.GetByYouhao(youhao);
            double djuli = 0;
            if (juli == "1")
                djuli = 5000;
            if (juli == "2")
                djuli = 10000;
            if (juli == "3")
                djuli = 20000;
            if (juli == "4")
                djuli = 30000;
            if (juli == "5")
                djuli = 50000;
            if (juli == "6")
                djuli = 200000;


            if (member != null)
            {
                List<GasStation> newgasStationList = new List<GasStation>();
                foreach (GasStation gs in gasStationList)
                {
                    gs.Distance = GetDistance(Convert.ToDouble(latitude), Convert.ToDouble(longitude), Convert.ToDouble(gs.Dimension), Convert.ToDouble(gs.Longitude));
                    if (gs.Distance < djuli)
                    {
                        GasStationLevel gsl = gasStationLevelService.Search(new GasStationLevel() { IsValid = true }).Where(o => o.MemberLevel == member.MemberLevel && o.GasStationId == gs.Id).Single();
                        if (gsl.ReductionTime > DateTime.Now)
                            gs.Reduction = gsl.Reduction.Value;
                        else
                            gs.Reduction = gsl.NewReduction.Value;
                        newgasStationList.Add(gs);
                    }
                }
                if (youxian == "1")
                {
                    newgasStationList = newgasStationList.OrderBy(o => o.Distance).ToList();
                }
                if (youxian == "2")
                {
                    newgasStationList = newgasStationList.OrderBy(o => o.Amount + o.Reduction).ToList();
                }
                return Json(newgasStationList);
            }
            else
            {
                return RedirectToAction("Create", "Member");
            }
        }

        private const double EARTH_RADIUS = 6378137;
        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }


        public ActionResult FillBill(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult GetOilNumber(string Id)
        {
            OilGunService oilgunService = new OilGunService();
            List<string> lstOilGun = oilgunService.GetOilNumber(Convert.ToInt32(Id));
            List<OilGun> oilgunList = new List<OilGun>();
            foreach (string oilNumber in lstOilGun)
            {
                OilGun oilgun = new OilGun();
                oilgun.OilNumber = oilNumber;
                oilgunList.Add(oilgun);
            }
            return Json(oilgunList);
        }

        [HttpPost]
        public ActionResult GetOilName(string Id, string OilNumber)
        {
            OilGunService oilgunService = new OilGunService();
            List<OilGun> oilNameList = oilgunService.Search(new OilGun() { IsValid = true }).Where(o => o.GasStationId == Convert.ToInt32(Id) && o.OilNumber == OilNumber).ToList();
            return Json(oilNameList);
        }

        //[HttpPost]
        //public ActionResult GetOilType(string Id, string OilNumber)
        //{
        //    OilGunService oilgunService = new OilGunService();
        //    List<int> lstOilType = oilgunService.GetOilType(Convert.ToInt32(Id),Convert.ToInt32(OilNumber));
        //    List<OilGun> oilTypeList = new List<OilGun>();
        //     foreach (int oilType in lstOilType)
        //    {
        //        OilGun oilgun = new OilGun();
        //        oilgun.OilType = oilType;
        //        oilTypeList.Add(oilgun);
        //    }
        //    return Json(oilTypeList);
        //}

        [HttpPost]
        public ActionResult GetActualAmount(string Id, string OilNumber, string GunNumber, string Amount)
        {
            MemberService memberService = new MemberService();
            WeiXinModel WeiXin = (WeiXinModel)System.Web.HttpContext.Current.Session["WeiXin"];
            Member member = memberService.Search(new Member() { IsValid = true }).Where(o => o.OpenId == WeiXin.OpenId).FirstOrDefault();
            if (member != null)
            {
                PaymentFormService paymentformService = new PaymentFormService();
                List<PaymentForm> paymentformList = paymentformService.Search(new PaymentForm() { IsValid = true }).Where(o => o.MemberId == member.Id && o.CreateTime.Value.Date == DateTime.Now.Date && o.IsAudit == true).ToList();
                if (paymentformList.Count >= member.Count)
                {
                    return Json(new
                    {
                        Count = 1
                    }, JsonRequestBehavior.AllowGet);
                }

                OilGunService oilgunService = new OilGunService();
                OilGun oilgun = oilgunService.Search(new OilGun() { IsValid = true }).Where(o => o.GasStationId == Convert.ToInt32(Id) && o.OilNumber == OilNumber && o.GunNumber == Convert.ToInt32(GunNumber)).Single();

                decimal godbounsAmount = 0;
                GodBouns godbouns = new GodBouns();
                if (Convert.ToDecimal(Amount) >= 300)
                {
                    GodBounsService godbounsService = new GodBounsService();
                    godbouns = godbounsService.Search(new GodBouns() { IsValid = true }).Where(o => o.MemberId == member.Id && o.BounsStatus == BounsStatus.未使用).FirstOrDefault();
                    if (godbouns != null)
                    {
                        godbounsAmount = godbouns.BounsAmount.Value;
                    }
                }

                GasStationLevelService gasStationLevelService = new GasStationLevelService();
                GasStationLevel gasStationLevel = gasStationLevelService.Search(new GasStationLevel() { IsValid = true }).Where(o => o.GasStationId == Convert.ToInt32(Id) && o.MemberLevel == member.MemberLevel).FirstOrDefault();
                if (gasStationLevel != null)
                {
                    GasStationService gasStationService = new GasStationService();
                    GasStation gasStation = gasStationService.GetById(Convert.ToInt32(Id));
                    SupplierService supplierService = new SupplierService();
                    Supplier supplier = supplierService.GetById(gasStation.SupplierId.Value);


                    Random r = new Random();
                    if (DateTime.Now < oilgun.PointTime)
                    {
                        decimal dTotalNationalPrice = 0;

                        if (DateTime.Now < oilgun.CountryPointTime)
                        {
                            dTotalNationalPrice = oilgun.CountryMarkPrice.Value * Convert.ToDecimal(Amount) / (oilgun.CountryMarkPrice.Value - oilgun.Amount.Value);
                            decimal? RiseNumber = Convert.ToDecimal(Amount) / (oilgun.CountryMarkPrice.Value - oilgun.Amount.Value);
                            decimal? ActualAmount = 0;
                            if (DateTime.Now < gasStationLevel.ReductionTime)
                                ActualAmount = Convert.ToDecimal((RiseNumber * (oilgun.CountryMarkPrice.Value - oilgun.Amount.Value - gasStationLevel.Reduction)).Value.ToString("N2")) - godbounsAmount;
                            else
                                ActualAmount = Convert.ToDecimal((RiseNumber * (oilgun.CountryMarkPrice.Value - oilgun.Amount.Value - gasStationLevel.NewReduction)).Value.ToString("N2")) - godbounsAmount;

                            string orderNumber = "CYH-" + DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(0, 99999);

                            decimal dSupplierAmount = 0;
                            if (DateTime.Now < supplier.ConcessionalPointTime)
                                dSupplierAmount = (oilgun.CountryMarkPrice.Value - oilgun.Amount.Value - supplier.Concessional.Value) * RiseNumber.Value;
                            else
                                dSupplierAmount = (oilgun.CountryMarkPrice.Value - oilgun.Amount.Value - supplier.NewConcessional.Value) * RiseNumber.Value;

                            int pId = CreatePayForm(Convert.ToInt32(Id), member.Id, Convert.ToDecimal(Amount), ActualAmount.Value, orderNumber, ActualAmount.Value * Convert.ToDecimal(0.003), RiseNumber.Value, oilgun.Id, dTotalNationalPrice, godbounsAmount, dSupplierAmount);
                            if (godbouns != null)
                            {
                                GodBounsRecordService godbounsRecordService = new GodBounsRecordService();
                                godbounsRecordService.Insert(new GodBounsRecord()
                                {
                                    BounsId = godbouns.Id,
                                    MemberId = member.Id,
                                    RelationId = pId,
                                    UseAmount = godbounsAmount,
                                    UseType = BounsUseType.支付
                                });
                            }
                            return Json(new
                            {
                                Count = 2,
                                pId = pId,
                                ActualAmount = ActualAmount,
                                GodbounsAmount = godbounsAmount
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            dTotalNationalPrice = oilgun.NewCountryPrice.Value * Convert.ToDecimal(Amount) / (oilgun.NewCountryPrice.Value - oilgun.Amount.Value);
                            decimal? RiseNumber = Convert.ToDecimal(Amount) / (oilgun.NewCountryPrice.Value - oilgun.Amount.Value);
                            decimal? ActualAmount = 0;
                            if (DateTime.Now < gasStationLevel.ReductionTime)
                                ActualAmount = Convert.ToDecimal((RiseNumber * (oilgun.NewCountryPrice.Value - oilgun.Amount.Value - gasStationLevel.Reduction)).Value.ToString("N2")) - godbounsAmount;
                            else
                                ActualAmount = Convert.ToDecimal((RiseNumber * (oilgun.NewCountryPrice.Value - oilgun.Amount.Value - gasStationLevel.NewReduction)).Value.ToString("N2")) - godbounsAmount;

                            string orderNumber = "CYH-" + DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(0, 99999);

                            decimal dSupplierAmount = 0;
                            if (DateTime.Now < supplier.ConcessionalPointTime)
                                dSupplierAmount = (oilgun.NewCountryPrice.Value - oilgun.Amount.Value - supplier.Concessional.Value) * RiseNumber.Value;
                            else
                                dSupplierAmount = (oilgun.NewCountryPrice.Value - oilgun.Amount.Value - supplier.NewConcessional.Value) * RiseNumber.Value;

                            int pId = CreatePayForm(Convert.ToInt32(Id), member.Id, Convert.ToDecimal(Amount), ActualAmount.Value, orderNumber, ActualAmount.Value * Convert.ToDecimal(0.003), RiseNumber.Value, oilgun.Id, dTotalNationalPrice, godbounsAmount, dSupplierAmount);
                            if (godbouns != null)
                            {
                                GodBounsRecordService godbounsRecordService = new GodBounsRecordService();
                                godbounsRecordService.Insert(new GodBounsRecord()
                                {
                                    BounsId = godbouns.Id,
                                    MemberId = member.Id,
                                    RelationId = pId,
                                    UseAmount = godbounsAmount,
                                    UseType = BounsUseType.支付
                                });
                            }
                            return Json(new
                            {
                                Count = 2,
                                pId = pId,
                                ActualAmount = ActualAmount,
                                GodbounsAmount = godbounsAmount
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        decimal dTotalNationalPrice = 0;
                        if (DateTime.Now < oilgun.CountryPointTime)
                        {
                            dTotalNationalPrice = oilgun.CountryMarkPrice.Value * Convert.ToDecimal(Amount) / (oilgun.CountryMarkPrice.Value - oilgun.NewAmount.Value);
                            decimal? RiseNumber = Convert.ToDecimal(Amount) / (oilgun.CountryMarkPrice.Value - oilgun.NewAmount.Value);
                            decimal? ActualAmount = 0;
                            if (DateTime.Now < gasStationLevel.ReductionTime)
                                ActualAmount = Convert.ToDecimal((RiseNumber * (oilgun.CountryMarkPrice.Value - oilgun.NewAmount.Value - gasStationLevel.Reduction)).Value.ToString("N2")) - godbounsAmount;
                            else
                                ActualAmount = Convert.ToDecimal((RiseNumber * (oilgun.CountryMarkPrice.Value - oilgun.NewAmount.Value - gasStationLevel.NewReduction)).Value.ToString("N2")) - godbounsAmount;

                            string orderNumber = "CYH-" + DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(0, 99999);



                            decimal dSupplierAmount = 0;
                            if (DateTime.Now < supplier.ConcessionalPointTime)
                                dSupplierAmount = (oilgun.CountryMarkPrice.Value - oilgun.NewAmount.Value - supplier.Concessional.Value) * RiseNumber.Value;
                            else
                                dSupplierAmount = (oilgun.CountryMarkPrice.Value - oilgun.NewAmount.Value - supplier.NewConcessional.Value) * RiseNumber.Value;


                            int pId = CreatePayForm(Convert.ToInt32(Id), member.Id, Convert.ToDecimal(Amount), ActualAmount.Value, orderNumber, ActualAmount.Value * Convert.ToDecimal(0.003), RiseNumber.Value, oilgun.Id, dTotalNationalPrice, godbounsAmount, dSupplierAmount);
                            if (godbouns != null)
                            {
                                GodBounsRecordService godbounsRecordService = new GodBounsRecordService();
                                godbounsRecordService.Insert(new GodBounsRecord()
                                {
                                    BounsId = godbouns.Id,
                                    MemberId = member.Id,
                                    RelationId = pId,
                                    UseAmount = godbounsAmount,
                                    UseType = BounsUseType.支付,
                                    IsEffective = false
                                });
                            }
                            return Json(new
                            {
                                Count = 2,
                                pId = pId,
                                ActualAmount = ActualAmount,
                                GodbounsAmount = godbounsAmount
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            dTotalNationalPrice = oilgun.NewCountryPrice.Value * Convert.ToDecimal(Amount) / (oilgun.NewCountryPrice.Value - oilgun.NewAmount.Value);
                            decimal? RiseNumber = Convert.ToDecimal(Amount) / (oilgun.NewCountryPrice.Value - oilgun.NewAmount.Value);
                            decimal? ActualAmount = 0;
                            if (DateTime.Now < gasStationLevel.ReductionTime)
                                ActualAmount = Convert.ToDecimal((RiseNumber * (oilgun.NewCountryPrice.Value - oilgun.NewAmount.Value - gasStationLevel.Reduction)).Value.ToString("N2")) - godbounsAmount;
                            else
                                ActualAmount = Convert.ToDecimal((RiseNumber * (oilgun.NewCountryPrice.Value - oilgun.NewAmount.Value - gasStationLevel.NewReduction)).Value.ToString("N2")) - godbounsAmount;

                            string orderNumber = "CYH-" + DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(0, 99999);



                            decimal dSupplierAmount = 0;
                            if (DateTime.Now < supplier.ConcessionalPointTime)
                                dSupplierAmount = (oilgun.NewCountryPrice.Value - oilgun.NewAmount.Value - supplier.Concessional.Value) * RiseNumber.Value;
                            else
                                dSupplierAmount = (oilgun.NewCountryPrice.Value - oilgun.NewAmount.Value - supplier.NewConcessional.Value) * RiseNumber.Value;


                            int pId = CreatePayForm(Convert.ToInt32(Id), member.Id, Convert.ToDecimal(Amount), ActualAmount.Value, orderNumber, ActualAmount.Value * Convert.ToDecimal(0.003), RiseNumber.Value, oilgun.Id, dTotalNationalPrice, godbounsAmount, dSupplierAmount);
                            if (godbouns != null)
                            {
                                GodBounsRecordService godbounsRecordService = new GodBounsRecordService();
                                godbounsRecordService.Insert(new GodBounsRecord()
                                {
                                    BounsId = godbouns.Id,
                                    MemberId = member.Id,
                                    RelationId = pId,
                                    UseAmount = godbounsAmount,
                                    UseType = BounsUseType.支付,
                                    IsEffective = false
                                });
                            }
                            return Json(new
                            {
                                Count = 2,
                                pId = pId,
                                ActualAmount = ActualAmount,
                                GodbounsAmount = godbounsAmount
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
                    return RedirectToAction("AllGasStation");
                }
            }
            else
            {
                return RedirectToAction("Create", "Member");
            }
        }

        private int CreatePayForm(int gasStationId, int memberId, decimal gasStationAmount, decimal actualAmount, string orderNumber, decimal recharegeFee, decimal riseNumber, int oilgunId, decimal dTotalNationalPrice, decimal godbounsAmount, decimal dSupplierAmount)
        {
            PaymentFormService paymentFormService = new PaymentFormService();
            int pId = paymentFormService.Insert(new PaymentForm()
            {
                MemberId = memberId,
                GasStationId = gasStationId,
                TotalNationalPrice = dTotalNationalPrice,
                GasStationAmount = gasStationAmount,
                ActualAmount = actualAmount,
                OrderNumber = orderNumber,
                PayTime = DateTime.Now,
                ServiceFee = recharegeFee,
                RiseNumber = riseNumber,
                OilGunId = oilgunId,
                GodbounsAmount = godbounsAmount,
                PayMode = PayMode.微信公众号支付,
                SupplierAmount = dSupplierAmount
            });
            return pId;
        }

        [HttpPost]
        public string GetWxPayment(int pId, string ActualAmount)
        {
            MemberService memberService = new MemberService();
            WeiXinModel WeiXin = (WeiXinModel)System.Web.HttpContext.Current.Session["WeiXin"];
            Member member = memberService.Search(new Member() { IsValid = true }).Where(o => o.OpenId == WeiXin.OpenId).FirstOrDefault();
            if (member != null)
            {
                PaymentFormService paymentFormService = new PaymentFormService();
                PaymentForm paymentForm = paymentFormService.GetById(pId);
                if (paymentForm.ActualAmount == Convert.ToDecimal(ActualAmount))
                {
                    return wxJsApiParam(member, "加油", paymentForm);
                }
                else
                {
                    return "1";
                }
            }
            else
            {
                return "0";
            }
        }
        /// <summary>
        /// 调用微信的jsapi支付
        /// </summary>
        /// <returns></returns>
        private string wxJsApiParam(Member member, string sAttach, PaymentForm paymentForm)
        {
            string wxJsApiParam = "";
            string openid = member.OpenId;
            Random r = new Random();
            //若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数
            JsApiPay jsApiPay = new JsApiPay();
            jsApiPay.openid = openid;
            jsApiPay.total_fee = Convert.ToInt32(paymentForm.ActualAmount * 100);
            jsApiPay.out_trade_no = paymentForm.OrderNumber;
            jsApiPay.attach = sAttach;

            //检测是否给当前页面传递了相关参数
            if (string.IsNullOrEmpty(openid) || jsApiPay.total_fee == 0)
            {
                Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面传参出错,请返回重试" + "</span>");

            }

            //JSAPI支付预处理
            try
            {
                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
                wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数       
                //在页面上显示订单信息
                //Response.Write("<span style='color:#00CD00;font-size:20px'>订单详情：</span><br/>");
                //Response.Write("<span style='color:#00CD00;font-size:20px'>" + unifiedOrderResult.ToPrintStr() + "</span>");

            }
            catch (Exception ex)
            {
                Response.Write("<span style='color:#FF0000;font-size:20px'>" + ex.Message + "</span>");
            }
            return wxJsApiParam;
        }


        public ActionResult OrdersRecord()
        {
            MemberService memberService = new MemberService();
            WeiXinModel WeiXin = (WeiXinModel)System.Web.HttpContext.Current.Session["WeiXin"];
            Member member = memberService.Search(new Member() { IsValid = true }).Where(o => o.OpenId == WeiXin.OpenId).FirstOrDefault();
            if (member != null)
            {
                ViewBag.HeadImgurl = WeiXin.HeadImgurl;
                ViewBag.NickName = WeiXin.NickName;
                ViewBag.Phone = member.Phone;
                PaymentFormService paymentformService = new PaymentFormService();
                List<PaymentForm> allPaymentFormList = paymentformService.GetPaymentFormByMemberId(member.Id);
                List<PaymentForm> paidFormList = paymentformService.GetPaymentFormByMemberId(member.Id, true);
                List<PaymentForm> unpaidFormList = paymentformService.GetPaymentFormByMemberId(member.Id, false);
                ViewBag.allPaymentFormList = allPaymentFormList;
                ViewBag.paidFormList = paidFormList;
                ViewBag.unpaidFormList = unpaidFormList;
                return View();
            }
            else
            {
                return RedirectToAction("Create", "Member");
            }
        }

        public ActionResult OrdersRecordDetails(int Id)
        {
            PaymentFormService paymentformService = new PaymentFormService();
            PaymentForm paymentForm = paymentformService.GetPaymentFormById(Id);
            return View(paymentForm);
        }

    }
}

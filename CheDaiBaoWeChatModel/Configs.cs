using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel
{
    public class Configs
    {
        #region 杂项

        /// <summary>
        /// MD5 秘钥
        /// </summary>
        private static string SiteMd5Key;
        public static string GetSiteMd5Key()
        {
            if (string.IsNullOrEmpty(SiteMd5Key))
            {
                return ConfigurationManager.AppSettings["SiteMd5Key"];
            }
            return SiteMd5Key;
        }

        /// <summary>
        /// 记住用户名的天数
        /// </summary>
        private static int _rememberDays;
        public static int GetRememberDays()
        {
            if (_rememberDays == 0)
            {
                _rememberDays = Convert.ToInt32(ConfigurationManager.AppSettings["RememberDays"]);
            }
            return _rememberDays;
        }

        /// <summary>
        /// 记住用户名的分钟
        /// </summary>
        private static int _RememberMinutes;
        public static int GetRememberMinutes()
        {
            if (_RememberMinutes == 0)
            {
                _RememberMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["RememberMinutes"]);
            }
            return _RememberMinutes;
        }


        /// <summary>
        /// 默认推荐类型
        /// </summary>
        private static int defaultRecommendType;
        public static int GetDefaultRecommendType()
        {
            if (defaultRecommendType == 0)
            {
                defaultRecommendType = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultRecommendType"]);
            }
            return defaultRecommendType;
        }

        /// <summary>
        /// 修改密码过期时间(单位：秒)
        /// </summary>
        private static int _changePwdTime;
        public static int GetChangePwdTime()
        {
            if (_changePwdTime == 0)
            {
                _changePwdTime = Convert.ToInt32(ConfigurationManager.AppSettings["ChangePwdTime"]);
            }
            return _changePwdTime;
        }

        /// <summary>
        /// 文件网站
        /// </summary>
        private static string FileSite;
        public static string GetFileSite()
        {
            if (string.IsNullOrEmpty(FileSite))
            {
                FileSite = ConfigurationManager.AppSettings["FileSite"].ToString();
            }
            return FileSite;
        }

        /// <summary>
        /// 图片保存地址
        /// </summary>
        private static string FilePath;
        public static string GetFilePath()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                FilePath = ConfigurationManager.AppSettings["FilePath"].ToString();
            }
            return FilePath;
        }


        /// <summary>
        /// 是否发送手机验证码
        /// </summary>
        private static bool? IsSendSms;
        public static bool GetIsSendSms()
        {
            if (!IsSendSms.HasValue)
            {
                IsSendSms = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSendSms"].ToString());
            }
            return IsSendSms.Value;
        }

        /// <summary>
        /// 短信验证码有效秒数
        /// </summary>
        private static int? MobileCodeExpires;
        public static int GetMobileCodeExpires()
        {
            if (!MobileCodeExpires.HasValue)
            {
                MobileCodeExpires = Convert.ToInt32(ConfigurationManager.AppSettings["MobileCodeExpires"].ToString());
            }
            return MobileCodeExpires.Value;
        }

        public static void ReMobileCodeExpires()
        {
            MobileCodeExpires = null;
            IsSendSms = null;
            YTXAccount = null;
            YTXToken = null;
            YTXAppid = null;
        }


        /// <summary>
        /// 手机正则
        /// </summary>
        private static string MobileRegex;
        public static string GetMobileRegex()
        {
            if (string.IsNullOrEmpty(MobileRegex))
            {
                MobileRegex = ConfigurationManager.AppSettings["MobileRegex"].ToString();
            }
            return MobileRegex;
        }


        /// <summary>
        /// 电子邮箱证码
        /// </summary>
        private static string EmailRegex;
        public static string GetEmailRegex()
        {
            if (string.IsNullOrEmpty(EmailRegex))
            {
                EmailRegex = ConfigurationManager.AppSettings["EmailRegex"].ToString();
            }
            return EmailRegex;
        }


        /// <summary>
        /// 平台Id
        /// </summary>
        private static int? SiteId;
        public static int GetSiteId()
        {
            if (!SiteId.HasValue)
            {
                SiteId = Convert.ToInt32(ConfigurationManager.AppSettings["SiteId"].ToString());
            }
            return SiteId.Value;
        }


        /// <summary>
        /// 分页记录条数
        /// </summary>
        private static int? PerPageSize;
        public static int GetPerPageSize()
        {
            if (!PerPageSize.HasValue)
            {
                PerPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PerPageSize"]);
            }
            return PerPageSize.Value;
        }

        /// <summary>
        /// 密钥
        /// </summary>
        private static string EncryptKey;
        public static string GetEncryptKey()
        {
            if (string.IsNullOrEmpty(EncryptKey))
            {
                EncryptKey = ConfigurationManager.AppSettings["EncryptKey"].ToString();
            }
            return EncryptKey;
        }

        /// <summary>
        /// 是否竞标中计息
        /// </summary>
        private static bool? IsNowInterest;
        public static bool GetIsNowInterest()
        {
            if (!IsNowInterest.HasValue)
            {
                IsNowInterest = Convert.ToBoolean(ConfigurationManager.AppSettings["IsNowInterest"]);
            }
            return IsNowInterest.Value;
        }

        /// <summary>
        /// MoonCake版本的后台
        /// </summary>
        private static string MoonCakeUrl;
        public static string GetMoonCakeUrl()
        {
            if (string.IsNullOrEmpty(MoonCakeUrl))
            {
                MoonCakeUrl = ConfigurationManager.AppSettings["MoonCakeUrl"];
            }
            return MoonCakeUrl;
        }

        /// <summary>
        /// FLAT版本的后台
        /// </summary>
        private static string FLATUrl;
        public static string GetFLATUrl()
        {
            if (string.IsNullOrEmpty(FLATUrl))
            {
                FLATUrl = ConfigurationManager.AppSettings["FLATUrl"];
            }
            return FLATUrl;
        }


        /// <summary>
        /// 合同物理路径
        /// </summary>
        private static string CompactMapPath;
        public static string GetCompactMapPath()
        {
            if (CompactMapPath == null)
            {
                CompactMapPath = ConfigurationManager.AppSettings["CompactMapPath"];
            }
            return CompactMapPath;
        }

        /// <summary>
        ///  允许访问IP地址，如果为空则所有都可以访问 
        /// </summary>
        private static string EffectiveIP;
        public static string GetEffectiveIP()
        {
            if (string.IsNullOrEmpty(EffectiveIP))
            {
                EffectiveIP = ConfigurationManager.AppSettings["EffectiveIP"];
            }
            return EffectiveIP;
        }
        #endregion

        #region 体验标

        /// <summary>
        /// 体验标借款人
        /// </summary>
        private static int? ExperienceCreditGodId;
        public static int GetExperienceCreditGodId()
        {
            if (!ExperienceCreditGodId.HasValue)
            {
                ExperienceCreditGodId = Convert.ToInt32(ConfigurationManager.AppSettings["ExperienceCreditGodId"]);
            }
            return ExperienceCreditGodId.Value;
        }


        /// <summary>
        /// 体验标担保公司Id
        /// </summary>
        private static int? ExperienceGuaranteeGodId;
        public static int GetExperienceGuaranteeGodId()
        {
            if (!ExperienceGuaranteeGodId.HasValue)
            {
                ExperienceGuaranteeGodId = Convert.ToInt32(ConfigurationManager.AppSettings["ExperienceGuaranteeGodId"]);
            }
            return ExperienceGuaranteeGodId.Value;
        }

        #endregion

        #region 债权转让配置
        /// <summary>
        /// 是否债权转让
        /// </summary>
        private static bool? IsAssignment;
        public static bool GetIsAssignment()
        {
            if (!IsAssignment.HasValue)
            {
                IsAssignment = Convert.ToBoolean(ConfigurationManager.AppSettings["IsAssignment"]);
            }
            return IsAssignment.Value;
        }

        /// <summary>
        /// 多少天后才能债权转让
        /// </summary>
        private static int? CreditAssignmentOfClaimsOfDay;
        public static int GetCreditAssignmentOfClaimsOfDay()
        {
            if (!CreditAssignmentOfClaimsOfDay.HasValue)
            {
                CreditAssignmentOfClaimsOfDay = Convert.ToInt32(ConfigurationManager.AppSettings["CreditAssignmentOfClaimsOfDay"]);
            }
            return CreditAssignmentOfClaimsOfDay.Value;
        }

        /// <summary>
        /// 债权转让收益比例  0到1之间表示转让人收益利率*X    大于1表示收益比例-X    如果是0就忽略
        /// </summary>
        private static decimal? CreditAssignmentOfClaimsRate;
        public static decimal GetCreditAssignmentOfClaimsRate()
        {
            if (!CreditAssignmentOfClaimsRate.HasValue)
            {
                CreditAssignmentOfClaimsRate = Convert.ToDecimal(ConfigurationManager.AppSettings["CreditAssignmentOfClaimsRate"]);
            }
            return CreditAssignmentOfClaimsRate.Value;
        }

        /// <summary>
        /// 最低债权转让金额
        /// </summary>
        private static decimal? MinAssignmentAmount;
        public static decimal GetMinAssignmentAmount()
        {
            if (!MinAssignmentAmount.HasValue)
            {
                MinAssignmentAmount = Convert.ToDecimal(ConfigurationManager.AppSettings["MinAssignmentAmount"]);
            }
            return MinAssignmentAmount.Value;
        }
        #endregion

        #region  各种费用配置
        /// <summary>
        /// 取现手续费
        /// </summary>
        private static decimal? WithdrawalFee;
        public static decimal GetWithdrawalFee()
        {
            if (!WithdrawalFee.HasValue)
            {
                WithdrawalFee = Convert.ToDecimal(ConfigurationManager.AppSettings["WithdrawalFee"].ToString());
            }
            return WithdrawalFee.Value;
        }

        /// <summary>
        /// 提现低于WithdrawalAmount  则收取提现手续费 
        /// </summary>
        private static decimal? WithdrawalAmount;
        public static decimal GetWithdrawalAmount()
        {
            if (!WithdrawalAmount.HasValue)
            {
                WithdrawalAmount = Convert.ToDecimal(ConfigurationManager.AppSettings["WithdrawalAmount"].ToString());
            }
            return WithdrawalAmount.Value;
        }

        /// <summary>
        /// 最少提现金额
        /// </summary>
        private static decimal? MinimumWithdrawals;
        public static decimal GetMinimumWithdrawals()
        {
            if (!MinimumWithdrawals.HasValue)
            {
                MinimumWithdrawals = Convert.ToDecimal(ConfigurationManager.AppSettings["MinimumWithdrawals"].ToString());
            }
            return MinimumWithdrawals.Value;
        }

        /// <summary>
        /// 注册奖金
        /// </summary>
        private static decimal? SignUpBonus;
        public static decimal GetSignUpBonus()
        {
            if (!SignUpBonus.HasValue)
            {
                SignUpBonus = Convert.ToDecimal(ConfigurationManager.AppSettings["SignUpBonus"].ToString());
            }
            return SignUpBonus.Value;
        }

        /// <summary>
        /// 首次投资推荐人奖励
        /// </summary>
        private static decimal? FirstBidReward;
        public static decimal GetFirstBidReward()
        {
            if (!FirstBidReward.HasValue)
            {
                FirstBidReward = Convert.ToDecimal(ConfigurationManager.AppSettings["FirstBidReward"].ToString());
            }
            return FirstBidReward.Value;
        }

        /// <summary>
        /// 投资人居间服务费
        /// </summary>
        private static decimal? BidFeeRate;
        public static decimal GetBidFeeRate()
        {
            if (!BidFeeRate.HasValue)
            {
                BidFeeRate = Convert.ToDecimal(ConfigurationManager.AppSettings["BidFeeRate"].ToString());
            }
            return BidFeeRate.Value;
        }

        /// <summary>
        /// 自动投资比例
        /// </summary>
        private static decimal? AutomaticBid;
        public static decimal GetAutomaticBid()
        {
            if (!AutomaticBid.HasValue)
            {
                AutomaticBid = Convert.ToDecimal(ConfigurationManager.AppSettings["AutomaticBid"].ToString());
            }
            return AutomaticBid.Value;
        }

        /// <summary>
        /// 逾期利率上浮率
        /// </summary>
        private static decimal? OverTimeCreditRate;
        public static decimal GetOverTimeCreditRate()
        {
            if (!OverTimeCreditRate.HasValue)
            {
                OverTimeCreditRate = Convert.ToDecimal(ConfigurationManager.AppSettings["OverTimeCreditRate"].ToString());
            }
            return OverTimeCreditRate.Value;
        }


        /// <summary>
        /// 逾期给担保公司的管理法比例
        /// </summary>
        private static decimal? GuaranteeOverTimeRate;
        public static decimal GetGuaranteeOverTimeRate()
        {
            if (!GuaranteeOverTimeRate.HasValue)
            {
                GuaranteeOverTimeRate = Convert.ToDecimal(ConfigurationManager.AppSettings["GuaranteeOverTimeRate"].ToString());
            }
            return GuaranteeOverTimeRate.Value;
        }

        /// <summary>
        /// 是否允许空转
        /// </summary>
        private static bool? IsCashWithdrawal;
        public static bool GetIsCashWithdrawal()
        {
            if (!IsCashWithdrawal.HasValue)
            {
                IsCashWithdrawal = Convert.ToBoolean(ConfigurationManager.AppSettings["IsCashWithdrawal"].ToString());
            }
            return IsCashWithdrawal.Value;
        }

        /// <summary>
        /// 提前还款违约金
        /// </summary>
        private static decimal? PrepaymentRate;
        public static decimal GetPrepaymentRate()
        {
            if (!PrepaymentRate.HasValue)
            {
                PrepaymentRate = Convert.ToDecimal(ConfigurationManager.AppSettings["PrepaymentRate"].ToString());
            }
            return PrepaymentRate.Value;
        }
        #endregion

        #region 网站个性化配置
        private static string SiteTitle;
        public static string GetSiteTitle()
        {
            if (string.IsNullOrEmpty(SiteTitle))
            {
                SiteTitle = ConfigurationManager.AppSettings["SiteTitle"].ToString();
            }
            return SiteTitle;
        }

        /// <summary>
        /// 公司名简称
        /// </summary>
        private static string SiteName;
        public static string GetSiteName()
        {
            if (string.IsNullOrEmpty(SiteName))
            {
                SiteName = ConfigurationManager.AppSettings["SiteName"].ToString();
            }
            return SiteName;
        }

        /// <summary>
        /// 公司网站名
        /// </summary>
        private static string SiteInternetName;
        public static string GetSiteInternetName()
        {
            if (string.IsNullOrEmpty(SiteInternetName))
            {
                SiteInternetName = ConfigurationManager.AppSettings["SiteInternetName"].ToString();
            }
            return SiteInternetName;
        }

        /// <summary>
        /// 公司全称
        /// </summary>
        private static string SiteFullName;
        public static string GetSiteFullName()
        {
            if (string.IsNullOrEmpty(SiteFullName))
            {
                SiteFullName = ConfigurationManager.AppSettings["SiteFullName"].ToString();
            }
            return SiteFullName;
        }

        /// <summary>
        /// 公司电话
        /// </summary>
        private static string SitePhone;
        public static string GetSitePhone()
        {
            if (string.IsNullOrEmpty(SitePhone))
            {
                SitePhone = ConfigurationManager.AppSettings["SitePhone"].ToString();
            }
            return SitePhone;
        }

        /// <summary>
        /// 公司网址
        /// </summary>
        private static string SiteUrl;
        public static string GetSiteUrl()
        {
            if (string.IsNullOrEmpty(SiteUrl))
            {
                SiteUrl = ConfigurationManager.AppSettings["SiteUrl"].ToString();
            }
            return SiteUrl;
        }

        /// <summary>
        /// 手机网站
        /// </summary>
        private static string SiteMobileUrl;
        public static string GetSiteMobileUrl()
        {
            if (string.IsNullOrEmpty(SiteMobileUrl))
            {
                SiteMobileUrl = ConfigurationManager.AppSettings["SiteMobileUrl"].ToString();
            }
            return SiteMobileUrl;
        }

        /// <summary>
        /// 地址
        /// </summary>
        private static string Address;
        public static string GetAddress()
        {
            if (string.IsNullOrEmpty(Address))
            {
                Address = ConfigurationManager.AppSettings["Address"].ToString();
            }
            return Address;
        }

        /// <summary>
        /// QQ
        /// </summary>
        private static string QQ;
        public static string GetQQ()
        {
            if (string.IsNullOrEmpty(QQ))
            {
                QQ = ConfigurationManager.AppSettings["QQ"].ToString();
            }
            return QQ;
        }
        #endregion

        #region 多行配置

        #region  云通讯
        private static string YTXAccount;
        public static string GetYTXAccount()
        {
            if (string.IsNullOrEmpty(YTXAccount))
            {
                YTXAccount = ConfigurationManager.AppSettings["YTXAccount"].ToString();
            }
            return YTXAccount;
        }

        private static string YTXToken;
        public static string GetYTXToken()
        {
            if (string.IsNullOrEmpty(YTXToken))
            {
                YTXToken = ConfigurationManager.AppSettings["YTXToken"].ToString();
            }
            return YTXToken;
        }

        private static string YTXAppid;
        public static string GetYTXAppid()
        {
            if (string.IsNullOrEmpty(YTXAppid))
            {
                YTXAppid = ConfigurationManager.AppSettings["YTXAppid"].ToString();
            }
            return YTXAppid;
        }
        #endregion

        #region 通联支付参数
        /// <summary>
        /// 提交地址
        /// </summary>
        private static string TongLianServerUrl;
        public static string GetTongLianServerUrl()
        {
            if (string.IsNullOrEmpty(TongLianServerUrl))
            {
                TongLianServerUrl = ConfigurationManager.AppSettings["TongLianServerUrl"].ToString();
            }
            return TongLianServerUrl;
        }

        /// <summary>
        /// 通联直接回调页面
        /// </summary>
        private static string TongLianPickupUrl;
        public static string GetTongLianPickupUrl()
        {
            if (string.IsNullOrEmpty(TongLianPickupUrl))
            {
                TongLianPickupUrl = ConfigurationManager.AppSettings["TongLianPickupUrl"].ToString();
            }
            return TongLianPickupUrl;
        }

        /// <summary>
        /// 通联异步回调页面
        /// </summary>
        private static string TongLianRreceiveUrl;
        public static string GetTongLianRreceiveUrl()
        {
            if (string.IsNullOrEmpty(TongLianRreceiveUrl))
            {
                TongLianRreceiveUrl = ConfigurationManager.AppSettings["TongLianRreceiveUrl"].ToString();
            }
            return TongLianRreceiveUrl;
        }

        /// <summary>
        /// 通联商户Id
        /// </summary>
        private static string TongLianMerchantId;
        public static string GetTongLianMerchantId()
        {
            if (string.IsNullOrEmpty(TongLianMerchantId))
            {
                TongLianMerchantId = ConfigurationManager.AppSettings["TongLianMerchantId"].ToString();
            }
            return TongLianMerchantId;
        }

        /// <summary>
        /// 通联Key
        /// </summary>
        private static string TongLianKey;
        public static string GetTongLianKey()
        {
            if (string.IsNullOrEmpty(TongLianKey))
            {
                TongLianKey = ConfigurationManager.AppSettings["TongLianKey"].ToString();
            }
            return TongLianKey;
        }

        /// <summary>
        /// 通联私钥
        /// </summary>
        private static string TongLianPathCer;
        public static string GetTongLianPathCer()
        {
            if (string.IsNullOrEmpty(TongLianPathCer))
            {
                TongLianPathCer = ConfigurationManager.AppSettings["TongLianPathCer"].ToString();
            }
            return TongLianPathCer;
        }
        #endregion

        #region 宝付支付参数
        /// <summary>
        /// 宝付充值手续费利率
        /// </summary>
        private static decimal? BaoFuRechargeRate;
        public static decimal GetBaoFuRechargeRate()
        {
            if (!BaoFuRechargeRate.HasValue)
            {
                BaoFuRechargeRate = Convert.ToDecimal(ConfigurationManager.AppSettings["BaoFuRechargeRate"].ToString());
            }
            return BaoFuRechargeRate.Value;
        }

        /// <summary>
        /// 宝付商户号
        /// </summary>
        private static string merchantID;
        public static string GetMerchantID()
        {
            if (string.IsNullOrEmpty(merchantID))
            {
                merchantID = ConfigurationManager.AppSettings["MerchantID"].ToString();
            }
            return merchantID;
        }

        /// <summary>
        /// 宝付终端编号
        /// </summary>
        private static string TerminalID;
        public static string GetTerminalID()
        {
            if (string.IsNullOrEmpty(TerminalID))
            {
                TerminalID = ConfigurationManager.AppSettings["TerminalID"].ToString();
            }
            return TerminalID;
        }

        private static string md5key;
        /// <summary>
        /// 宝付密钥
        /// </summary>
        public static string GetMd5key()
        {
            if (string.IsNullOrEmpty(md5key))
            {
                md5key = ConfigurationManager.AppSettings["Md5key"].ToString();
            }
            return md5key;
        }

        #endregion

        #region 汇付宝
        /// <summary>
        /// 汇付宝充值手续费利率
        /// </summary>
        private static decimal? HuiFuTongRechargeRate;
        public static decimal GetHuiFuTongRechargeRate()
        {
            if (!HuiFuTongRechargeRate.HasValue)
            {
                HuiFuTongRechargeRate = Convert.ToDecimal(ConfigurationManager.AppSettings["HuiFuTongRechargeRate"].ToString());
            }
            return HuiFuTongRechargeRate.Value;
        }

        /// <summary>
        /// 汇付通商户Id
        /// </summary>
        private static string huiFuTongMerchartId;
        public static string GetHuiFuTongMerchantId()
        {
            if (string.IsNullOrEmpty(huiFuTongMerchartId))
            {
                huiFuTongMerchartId = ConfigurationManager.AppSettings["HuiFuTongMerchartId"].ToString();
            }
            return huiFuTongMerchartId;
        }


        private static string huiFuTongmd5key;
        /// <summary>
        /// 宝付密钥
        /// </summary>
        public static string GetHuiFuTongMd5key()
        {
            if (string.IsNullOrEmpty(huiFuTongmd5key))
            {
                huiFuTongmd5key = ConfigurationManager.AppSettings["HuiFuTongMd5key"].ToString();
            }
            return huiFuTongmd5key;
        }

        #endregion

        #region 微信相关
        private static string WeiXinToken;
        public static string GetWeiXinToken()
        {
            if (string.IsNullOrEmpty(WeiXinToken))
            {
                WeiXinToken = ConfigurationManager.AppSettings["WeiXinToken"].ToString();
            }
            return WeiXinToken;
        }

        private static string WeiXinAppId;
        public static string GetWeiXinAppId()
        {
            if (string.IsNullOrEmpty(WeiXinAppId))
            {
                WeiXinAppId = ConfigurationManager.AppSettings["WeiXinAppId"].ToString();
            }
            return WeiXinAppId;
        }

        private static string WeiXinAppsecret;
        public static string GetWeiXinAppsecret()
        {
            if (string.IsNullOrEmpty(WeiXinAppsecret))
            {
                WeiXinAppsecret = ConfigurationManager.AppSettings["WeiXinAppsecret"].ToString();
            }
            return WeiXinAppsecret;
        }

        //-----ceshidaimaduan-----
        private static string WeiXinUrlLoan;
        public static string GetWeiXinUrlLoan()
        {
            if (string.IsNullOrEmpty(WeiXinUrlLoan))
            {
                WeiXinUrlLoan = ConfigurationManager.AppSettings["WeiXinLoanUrl"].ToString();
            }
            return WeiXinUrlLoan;
        }
        #endregion

        #region 短信配置
        /// <summary>
        /// 流标通知
        /// </summary>
        private static int LiuBiaoTongZhiSmsId;
        public static int GetLiuBiaoTongZhiSmsId()
        {
            if (LiuBiaoTongZhiSmsId == 0)
            {
                LiuBiaoTongZhiSmsId = Convert.ToInt32(ConfigurationManager.AppSettings["LiuBiaoTongZhiSmsId"]);
            }
            return LiuBiaoTongZhiSmsId;
        }

        /// <summary>
        /// 回款通知
        /// </summary>
        private static int HuiKuanXinXiSmsId;
        public static int GetHuiKuanXinXiSmsId()
        {
            if (HuiKuanXinXiSmsId == 0)
            {
                HuiKuanXinXiSmsId = Convert.ToInt32(ConfigurationManager.AppSettings["HuiKuanXinXiSmsId"]);
            }
            return HuiKuanXinXiSmsId;
        }

        /// <summary>
        /// 验证码
        /// </summary>
        private static int YanZhengMaSmsId;
        public static int GetYanZhengMaSmsId()
        {
            if (YanZhengMaSmsId == 0)
            {
                YanZhengMaSmsId = Convert.ToInt32(ConfigurationManager.AppSettings["YanZhengMaSmsId"]);
            }
            return YanZhengMaSmsId;
        }

        /// <summary>
        /// 银行卡验证成功
        /// </summary>
        private static int YinHangKaChengGongSmsId;
        public static int GetYinHangKaChengGongSmsId()
        {
            if (YinHangKaChengGongSmsId == 0)
            {
                YinHangKaChengGongSmsId = Convert.ToInt32(ConfigurationManager.AppSettings["YinHangKaChengGongSmsId"]);
            }
            return YinHangKaChengGongSmsId;
        }

        /// <summary>
        /// 银行卡验证失败
        /// </summary>
        private static int YinHangKaShiBaiSmsId;
        public static int GetYinHangKaShiBaiSmsId()
        {
            if (YinHangKaShiBaiSmsId == 0)
            {
                YinHangKaShiBaiSmsId = Convert.ToInt32(ConfigurationManager.AppSettings["YinHangKaShiBaiSmsId"]);
            }
            return YinHangKaShiBaiSmsId;
        }
        #endregion

        #region 首页新闻的文章类型

        private static int articleType1;
        public static int GetArticleType1()
        {
            if (articleType1 == 0)
            {
                articleType1 = Convert.ToInt32(ConfigurationManager.AppSettings["TopNewsArticleType1"]);
            }
            return articleType1;
        }

        private static int articleType2;
        public static int GetArticleType2()
        {
            if (articleType2 == 0)
            {
                articleType2 = Convert.ToInt32(ConfigurationManager.AppSettings["TopNewsArticleType2"]);
            }
            return articleType2;
        }

        private static int articleType3;
        public static int GetArticleType3()
        {
            if (articleType3 == 0)
            {
                articleType3 = Convert.ToInt32(ConfigurationManager.AppSettings["TopNewsArticleType3"]);
            }
            return articleType3;
        }

        #endregion

        #region 邮箱配置
        /// <summary>
        /// 发件人邮箱地址
        /// </summary>
        private static string FromEmail;
        public static string GetFromEmail()
        {
            if (string.IsNullOrEmpty(FromEmail))
            {
                FromEmail = ConfigurationManager.AppSettings["FromEmail"];
            }
            return FromEmail;
        }

        /// <summary>
        /// 发件人用户名
        /// </summary>
        private static string EmailUserName;
        public static string GetEmailUserName()
        {
            if (string.IsNullOrEmpty(EmailUserName))
            {
                EmailUserName = ConfigurationManager.AppSettings["EmailUserName"];
            }
            return EmailUserName;
        }

        /// <summary>
        /// 发件人密码
        /// </summary>
        private static string EmailPassWord;
        public static string GetEmailPassWord()
        {
            if (string.IsNullOrEmpty(EmailPassWord))
            {
                EmailPassWord = ConfigurationManager.AppSettings["EmailPassWord"];
            }
            return EmailPassWord;
        }

        /// <summary>
        /// 发件人EmailHost
        /// </summary>
        private static string EmailHost;
        public static string GetEmailHost()
        {
            if (string.IsNullOrEmpty(EmailHost))
            {
                EmailHost = ConfigurationManager.AppSettings["EmailHost"];
            }
            return EmailHost;
        }


        /// <summary>
        ///  异常上传消息通知邮件地址
        /// </summary>
        private static string PliploadToEmail;
        public static string GetPliploadToEmail()
        {
            if (string.IsNullOrEmpty(PliploadToEmail))
            {
                PliploadToEmail = ConfigurationManager.AppSettings["PliploadToEmail"];
            }
            return PliploadToEmail;
        }
        /// <summary>
        /// 邮件的有效时效
        /// </summary>
        private static int EmailCodeExpires;
        public static int GetEmailCodeExpires()
        {
            if (EmailCodeExpires == 0)
            {
                EmailCodeExpires = Convert.ToInt32(ConfigurationManager.AppSettings["EmailCodeExpires"]);
            }
            return EmailCodeExpires;
        }
        #endregion

        #endregion

        #region 体验金配置相关
        /// <summary>
        /// 首次充值赠送的体验金
        /// </summary>
        private static decimal? FirstRechargeGiftAmount;
        public static decimal GetFirstRechargeGiftAmount()
        {
            if (!FirstRechargeGiftAmount.HasValue)
            {
                FirstRechargeGiftAmount = Convert.ToDecimal(ConfigurationManager.AppSettings["FirstRechargeGiftAmount"].ToString());
            }
            return FirstRechargeGiftAmount.Value;
        }

        /// <summary>
        /// 首次充值赠送体验金有效天数
        /// </summary>
        private static int? FirstRechargeGiftTerm;
        public static int GetFirstRechargeGiftTerm()
        {
            if (!FirstRechargeGiftTerm.HasValue)
            {
                FirstRechargeGiftTerm = Convert.ToInt32(ConfigurationManager.AppSettings["FirstRechargeGiftTerm"].ToString());
            }
            return FirstRechargeGiftTerm.Value;
        }

        /// <summary>
        /// 充值送体验金比例
        /// </summary>
        private static decimal? GiftProportion;
        public static decimal GetGiftProportion()
        {
            if (!GiftProportion.HasValue)
            {
                GiftProportion = Convert.ToDecimal(ConfigurationManager.AppSettings["GiftProportion"].ToString());
            }
            return GiftProportion.Value;
        }

        /// <summary>
        /// 充值送体验金比例有效天数
        /// </summary>
        private static int? GiftProTerm;
        public static int GetGiftProTerm()
        {
            if (!GiftProTerm.HasValue)
            {
                GiftProTerm = Convert.ToInt32(ConfigurationManager.AppSettings["GiftProTerm"].ToString());
            }
            return GiftProTerm.Value;
        }


        /// <summary>
        /// 注册送体验金
        /// </summary>
        private static decimal? RegistrationGiftAmount;
        public static decimal GetRegistrationGiftAmount()
        {
            if (!RegistrationGiftAmount.HasValue)
            {
                RegistrationGiftAmount = Convert.ToDecimal(ConfigurationManager.AppSettings["RegistrationGiftAmount"].ToString());
            }
            return RegistrationGiftAmount.Value;
        }

        /// <summary>
        /// 注册送体验金有效天数
        /// </summary>
        private static int? RegistrationGiftAmountTerm;
        public static int GetRegistrationGiftAmountTerm()
        {
            if (!RegistrationGiftAmountTerm.HasValue)
            {
                RegistrationGiftAmountTerm = Convert.ToInt32(ConfigurationManager.AppSettings["RegistrationGiftAmountTerm"].ToString());
            }
            return RegistrationGiftAmountTerm.Value;
        }
        #endregion

        private static string UploadDomain;
        public static string GetUploadDomain()
        {
            if (UploadDomain == null)
            {
                UploadDomain = ConfigurationManager.AppSettings["UploadDomain"];
            }
            return UploadDomain;
        }


        #region 连连支付
        private static string LianLianMd5Key;
        public static string GetLianLianMd5Key()
        {
            if (string.IsNullOrEmpty(LianLianMd5Key))
            {
                LianLianMd5Key = ConfigurationManager.AppSettings["LianLianMd5Key"].ToString();
            }
            return LianLianMd5Key;
        }

        private static string LianLianMerchantId;
        public static string GetLianLianMerchantId()
        {
            if (string.IsNullOrEmpty(LianLianMerchantId))
            {
                LianLianMerchantId = ConfigurationManager.AppSettings["LianLianMerchantId"].ToString();
            }
            return LianLianMerchantId;
        }

        private static string LianLianYTPUBKEY;
        public static string GetLianLianYTPUBKEY()
        {
            if (string.IsNullOrEmpty(LianLianYTPUBKEY))
            {
                LianLianYTPUBKEY = ConfigurationManager.AppSettings["YT_PUB_KEY"].ToString();
            }
            return LianLianYTPUBKEY;
        }

        private static string LianLianTRADERPRIKEY;
        public static string GetLianLianTRADERPRIKEY()
        {
            if (string.IsNullOrEmpty(LianLianTRADERPRIKEY))
            {
                LianLianTRADERPRIKEY = ConfigurationManager.AppSettings["TRADER_PRI_KEY"].ToString();
            }
            return LianLianTRADERPRIKEY;
        }
        #endregion


        #region 富友支付
        private static string MchntNo;
        public static string GetFuYouMchntNo()
        {
            if (string.IsNullOrEmpty(MchntNo))
            {
                MchntNo = ConfigurationManager.AppSettings["MchntNo"].ToString();
            }
            return MchntNo;
        }

        private static string MchntKey;
        public static string GetFuYouMchntKey()
        {
            if (string.IsNullOrEmpty(MchntKey))
            {
                MchntKey = ConfigurationManager.AppSettings["MchntKey"].ToString();
            }
            return MchntKey;
        }

        private static string SendOrderURL;
        public static string GetFuYouSendOrderURL()
        {
            if (string.IsNullOrEmpty(SendOrderURL))
            {
                SendOrderURL = ConfigurationManager.AppSettings["SendOrderURL"].ToString();
            }
            return SendOrderURL;
        }


        private static string WechatMchntKey;
        public static string GetFuYouWeChatMchntKey()
        {
            if (string.IsNullOrEmpty(WechatMchntKey))
            {
                WechatMchntKey = ConfigurationManager.AppSettings["WechatMchntKey"].ToString();
            }
            return WechatMchntKey;
        }

        private static string SendOrderWeChatURL;
        public static string GetSendOrderWeChatURL()
        {
            if (string.IsNullOrEmpty(SendOrderWeChatURL))
            {
                SendOrderWeChatURL = ConfigurationManager.AppSettings["SendOrderWeChatURL"].ToString();
            }
            return SendOrderWeChatURL;
        }
        #endregion

        #region 深圳信用宝
        private static string MerchantNo;
        public static string GetMerchantNo()
        {
            if (MerchantNo == null)
            {
                MerchantNo = ConfigurationManager.AppSettings["MerchantNo"];
            }
            return MerchantNo;
        }

        private static string MiShi;
        public static string GetMiShi()
        {
            if (MiShi == null)
            {
                MiShi = ConfigurationManager.AppSettings["MiShi"];
            }
            return MiShi;
        }
        #endregion

        #region 创瑞短信参数
        private static string SmsName;
        public static string GetSmsName()
        {
            if (SmsName == null)
            {
                SmsName = ConfigurationManager.AppSettings["SmsName"];
            }
            return SmsName;
        }

        private static string CRKey;
        public static string GetCRKey()
        {
            if (CRKey == null)
            {
                CRKey = ConfigurationManager.AppSettings["CRKey"];
            }
            return CRKey;
        }
        #endregion


        #region 百度相关
        private static string BaiduToken;
        public static string GetBaiduToken()
        {
            if (string.IsNullOrEmpty(BaiduToken))
            {
                BaiduToken = ConfigurationManager.AppSettings["BaiduToken"].ToString();
            }
            return BaiduToken;
        }

        private static string APIKey;
        public static string GetAPIKey()
        {
            if (string.IsNullOrEmpty(APIKey))
            {
                APIKey = ConfigurationManager.AppSettings["APIKey"].ToString();
            }
            return APIKey;
        }

        private static string SecretKey;
        public static string GetSecretKey()
        {
            if (string.IsNullOrEmpty(SecretKey))
            {
                SecretKey = ConfigurationManager.AppSettings["SecretKey"].ToString();
            }
            return SecretKey;
        }

        #endregion
    }
}
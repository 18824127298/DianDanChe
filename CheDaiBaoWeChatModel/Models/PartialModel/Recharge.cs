

using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CheDaiBaoCommonService.Data;

namespace CheDaiBaoWeChatModel.Models
{
    /// <summary>
    ///  
    /// </summary>
    [Serializable]
    public partial class Recharge : BaseModel
    {

        /// <summary>
        /// 充值金额 
        /// </summary>         
        [OriginalField]
        [Display(Name = "充值金额")]
        public Decimal? Amount { get; set; }




        /// <summary>
        /// 用户Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "用户Id")]
        public int BorrowerId { get; set; }




        /// <summary>
        /// 充值接口（通联支付=1） 
        /// </summary>         
        [OriginalField]
        [Display(Name = "充值接口")]
        public RechargeMode? RechargeMode { get; set; }




        /// <summary>
        /// 审核状态 
        /// </summary>         
        [OriginalField]
        [Display(Name = "审核状态")]
        public Boolean? IsAudit { get; set; }


        /// <summary>
        /// 充值订单号 
        /// </summary>         
        [OriginalField]
        [Display(Name = "充值订单号")]
        public String OrderNumber { get; set; }

        /// <summary>
        /// 备注 
        /// </summary>         
        [OriginalField]
        [Display(Name = "备注")]
        public String Remark { get; set; }


        /// <summary>
        /// 第三方手续的充值手续费 
        /// </summary>         
        [OriginalField]
        [Display(Name = "第三方手续的充值手续费")]
        public Decimal? ActualRechargeFee { get; set; }


        /// <summary>
        /// 银行名称
        /// </summary>         
        [OriginalField]
        [Display(Name = "银行名称")]
        public String BankCardName { get; set; }

        /// <summary>
        /// 银行帐号
        /// </summary>         
        [OriginalField]
        [Display(Name = "银行帐号")]
        public String BankCardNumber { get; set; }


        /// <summary>
        /// 银行类型
        /// </summary>         
        [OriginalField]
        [Display(Name = "银行类型")]
        public String BankCardCode { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// </summary>         
        [OriginalField]
        [Display(Name = "微信支付订单号")]
        public String TransactionId { get; set; }

        /// <summary>
        /// 创建人 
        /// </summary>         
        [OriginalField]
        [Display(Name = "创建人")]
        public String Creator { get; set; }


        /// <summary>
        /// 充值时间 
        /// </summary>         
        [OriginalField]
        [Display(Name = "充值时间")]
        public String RechargeTime { get; set; }


        /// <summary>
        /// 充值备注 
        /// </summary>         
        [OriginalField]
        [Display(Name = "充值备注")]
        public String RechargeRemark { get; set; }


        /// <summary>
        /// 审核人 
        /// </summary>         
        [OriginalField]
        [Display(Name = "审核人")]
        public String Auditor { get; set; }

        /// <summary>
        /// 审核时间 
        /// </summary>         
        [OriginalField]
        [Display(Name = "审核时间")]
        public String AuditTime { get; set; }


        /// <summary>
        /// 审核备注 
        /// </summary>         
        [OriginalField]
        [Display(Name = "审核备注")]
        public String AuditRemark { get; set; }



    }
}

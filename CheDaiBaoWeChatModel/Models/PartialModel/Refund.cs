using CheDaiBaoCommonService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
    [Serializable]
    public partial class Refund : BaseModel
    {

        /// <summary>
        /// 金额 
        /// </summary>         
        [OriginalField]
        [Display(Name = "金额")]
        public Decimal? Amount { get; set; }


        /// <summary>
        /// 用户Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "用户Id")]
        public int BorrowerId { get; set; }

        /// <summary>
        /// 审核结果
        /// </summary>         
        [OriginalField]
        [Display(Name = "审核结果")]
        public Boolean? IsAudit { get; set; }

        /// <summary>
        /// 商户退款单号
        /// </summary>         
        [OriginalField]
        [Display(Name = "商户退款单号")]
        public string OutRefundNo { get; set; }

        /// <summary>
        /// 微信退款单号
        /// </summary>         
        [OriginalField]
        [Display(Name = "微信退款单号")]
        public string RefundId { get; set; }

        /// <summary>
        /// 退款时间
        /// </summary>         
        [OriginalField]
        [Display(Name = "退款时间")]
        public DateTime? RefundTime { get; set; }

        /// <summary>
        /// 资金流Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "资金流Id")]
        public int FundsFlowId { get; set; }

        /// <summary>
        /// 充值Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "充值Id")]
        public int ReChargeId { get; set; }

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
        public DateTime? AuditTime { get; set; }


        /// <summary>
        /// 审核备注 
        /// </summary>         
        [OriginalField]
        [Display(Name = "审核备注")]
        public String AuditRemark { get; set; }

        
        /// <summary>
        /// 原因 
        /// </summary>         
        [OriginalField]
        [Display(Name = "原因")]
        public String Reason { get; set; }
        
    }
}

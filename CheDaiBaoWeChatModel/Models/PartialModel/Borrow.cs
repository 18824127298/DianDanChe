using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using CheDaiBaoCommonService.Data;
using CheDaiBaoWeChatModel;

namespace CheDaiBaoWeChatModel.Models
{
    /// <summary>
    ///  
    /// </summary>
    [Serializable]
    public partial class Borrow : BaseModel
    {
 
        /// <summary>
        /// 期次(其中0代表立即还) 
        /// </summary>         
        [OriginalField]
        [Display(Name = "期次")]
        public int Stages { get; set; }
 
        /// <summary>
        ///  还款 
        /// </summary>         
        [OriginalField]
        [Display(Name = "资金流水Id")]
        public int FundsFlowId { get; set; }

        /// <summary>
        ///类型(正常还款=1,提前还款=2,逾期还款=3)
        /// </summary>         
        [OriginalField]
        [Display(Name = "类型")]
        public RepaymentPlanMode? RepaymentPlanMode { get; set; }


        /// <summary>
        /// 应还款时间 
        /// </summary>         
        [OriginalField]
        [Display(Name = "应还款时间")]
        public DateTime? RepaymentDate { get; set; }


        /// <summary>
        /// 实际还款时间 
        /// </summary>         
        [OriginalField]
        [Display(Name = "实际还款时间")]
        public DateTime? ActualRepaymentDate { get; set; }


        /// <summary>
        ///本金
        /// </summary>         
        [OriginalField]
        [Display(Name = "本金")]
        public Decimal? Principal { get; set; }


        /// <summary>
        /// 未还本金
        /// </summary>         
        [OriginalField]
        [Display(Name = "未还本金")]
        public Decimal? UnPrincipal { get; set; }


        /// <summary>
        /// 利息 
        /// </summary>         
        [OriginalField]
        [Display(Name = "利息")]
        public Decimal? Interest { get; set; }


        /// <summary>
        /// 逾期利息 
        /// </summary>         
        [OriginalField]
        [Display(Name = "逾期利息")]
        public Decimal? OverInterest { get; set; }


        /// <summary>
        /// 提前还款违约金 
        /// </summary>         
        [OriginalField]
        [Display(Name = "提前还款违约金")]
        public Decimal? BreachAmount { get; set; }



        /// <summary>
        /// 逾期天数 
        /// </summary>         
        [OriginalField]
        [Display(Name = "逾期天数")]
        public int OverDay { get; set; }



        /// <summary>
        /// 债权人 
        /// </summary>         
        [OriginalField]
        [Display(Name = "借款人")]
        public int BorrowerId { get; set; }

        /// <summary>
        /// 借款编号
        /// </summary>         
        [OriginalField]
        [Display(Name = "借款Id")]
        public int LoanApplyId { get; set; }

        /// <summary>
        /// 未还利息
        /// </summary>         
        [OriginalField]
        [Display(Name = "未还利息")]
        public Decimal? UnTotalInterest { get; set; }


        /// <summary>
        /// 总期次
        /// </summary>         
        [OriginalField]
        [Display(Name = "总期次")]
        public int TotalPeriod { get; set; }
    }
}

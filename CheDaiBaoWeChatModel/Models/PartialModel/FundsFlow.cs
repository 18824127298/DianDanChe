

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
    public partial class FundsFlow : BaseModel
    {

        /// <summary>
        /// 备注 
        /// </summary>         
        [OriginalField]
        [Display(Name = "备注")]
        public String Remark { get; set; }




        /// <summary>
        /// 金额 
        /// </summary>         
        [OriginalField]
        [Display(Name = "金额")]
        public Decimal? Amount { get; set; }




        /// <summary>
        ///  收入用户 
        /// </summary>         
        [OriginalField]
        [Display(Name = "收入用户")]
        public int IncomeGodId { get; set; }




        /// <summary>
        /// 支付用户
        /// </summary>         
        [OriginalField]
        [Display(Name = "支付用户")]
        public int PayGodId { get; set; }




        /// <summary>
        /// 是否冻结 
        /// </summary>         
        [OriginalField]
        [Display(Name = "是否冻结")]
        public Boolean? IsFreeze { get; set; }




        /// <summary>
        /// 业务类型 
        /// </summary>         
        [OriginalField]
        [Display(Name = "业务类型")]
        public FeeType? FeeType { get; set; }

        /// <summary>
        /// 关联Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "关联Id")]
        public int RelationId { get; set; }

        /// <summary>
        /// 是否是有效的资金流水
        /// </summary>         
        [OriginalField]
        [Display(Name = "是否是有效的资金流水")]
        public Boolean? IsComputing { get; set; }


        /// <summary>
        /// 借贷Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "借贷Id")]
        public Int32? LoanApplyId { get; set; }

    }
}

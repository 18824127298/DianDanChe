using CheDaiBaoCommonService.Data;
using CheDaiBaoWeChatModel.Validators;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
    [Serializable]
    [Validator(typeof(BorrowerValidator))]
    public partial class SeparateAccounts : BaseModel
    {
        /// <summary>
        /// 总金额 
        /// </summary>         
        [OriginalField]
        [Display(Name = "总金额")]
        public Decimal? Amount { get; set; }

        /// <summary>
        /// 商户的金额
        /// </summary>         
        [OriginalField]
        [Display(Name = "商户的金额")]
        public Decimal? PartnersAmount { get; set; }


        /// <summary>
        /// 我们的金额 
        /// </summary>         
        [OriginalField]
        [Display(Name = "我们的金额")]
        public Decimal? WeAmount { get; set; }


        /// <summary>
        /// 充值的Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "充值的Id")]
        public Int32? ReChargeId { get; set; }

        /// <summary>
        /// 客户的Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "客户的Id")]
        public Int32? BorrowerId { get; set; }


        /// <summary>
        /// 所属的商家 
        /// </summary>         
        [OriginalField]
        [Display(Name = "所属的商家")]
        public Company Company { get; set; }


        /// <summary>
        /// 业务员Id
        /// </summary>         
        [OriginalField]
        [Display(Name = "业务员Id")]
        public Int32? SalesmanId { get; set; }

        /// <summary>
        /// 关联的Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "关联的Id")]
        public Int32? RelationId { get; set; }


        /// <summary>
        /// 分账的时间 
        /// </summary>         
        [OriginalField]
        [Display(Name = "分账的时间")]
        public DateTime? ArrivalTime { get; set; }

    }
}


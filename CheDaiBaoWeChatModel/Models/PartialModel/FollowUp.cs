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
    [Validator(typeof(CarValidator))]
    public partial class FollowUp : BaseModel
    {
        /// <summary>
        /// 备注 
        /// </summary>         
        [OriginalField]
        [Display(Name = "备注")]
        public String Remark { get; set; }

        /// <summary>
        /// 跟进者
        /// </summary>         
        [OriginalField]
        [Display(Name = "跟进者")]
        public String Creator { get; set; }

        /// <summary>
        /// 关联的Id
        /// </summary>         
        [OriginalField]
        [Display(Name = "关联的Id")]
        public Int32? RelationId { get; set; }

        /// <summary>
        /// 客户的Id
        /// </summary>         
        [OriginalField]
        [Display(Name = "客户的Id")]
        public Int32? BorrowerId { get; set; }


        /// <summary>
        /// 业务员Id
        /// </summary>         
        [OriginalField]
        [Display(Name = "业务员Id")]
        public Int32? SalesmanId { get; set; }


        /// <summary>
        /// 贷款的Id
        /// </summary>         
        [OriginalField]
        [Display(Name = "贷款的Id")]
        public Int32? LoanApplyId { get; set; }


        /// <summary>
        /// 类型
        /// </summary>         
        [OriginalField]
        [Display(Name = "类型")]
        public CreatorType CreatorType { get; set; }
    }
}

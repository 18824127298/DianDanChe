using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

using FluentValidation.Attributes;
using CheDaiBaoCommonService.Data;

namespace CheDaiBaoWeChatModel.Models
{
    /// <summary>
    ///  用户赠金使用记录
    /// </summary>
    [Serializable]
    public partial class GodBounsRecord : BaseModel
    {
        /// <summary>
        /// 奖金标识 
        /// </summary>         
        [OriginalField]
        [Display(Name = "奖金标识")]
        public Int32? BounsId { get; set; }


        /// <summary>
        /// 用户标识 
        /// </summary>         
        [OriginalField]
        [Display(Name = "用户标识")]
        public Int32? MemberId { get; set; }



        /// <summary>
        /// 使用金额 
        /// </summary>         
        [OriginalField]
        [Display(Name = "使用金额")]
        public Decimal? UseAmount { get; set; }


        /// <summary>
        /// 关联的标识 
        /// </summary>         
        [OriginalField]
        [Display(Name = "关联的标识")]
        public Int32? RelationId { get; set; }


        /// <summary>
        /// 使用类型 
        /// </summary>         
        [OriginalField]
        [Display(Name = "使用类型")]
        public BounsUseType? UseType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>         
        [OriginalField]
        [Display(Name = "备注")]
        public String Remarks { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>         
        [OriginalField]
        [Display(Name = "是否有效")]
        public Boolean? IsEffective { get; set; }

    }
}

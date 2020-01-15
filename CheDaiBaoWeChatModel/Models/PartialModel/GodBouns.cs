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
    ///  用户赠金
    /// </summary>
    [Serializable]
    public partial class GodBouns : BaseModel
    {
        /// <summary>
        /// 名称
        /// </summary>         
        [OriginalField]
        [Display(Name = "名称")]
        public String Name { get; set; }


        /// <summary>
        /// 用户标识 
        /// </summary>         
        [OriginalField]
        [Display(Name = "用户标识")]
        public Int32? MemberId { get; set; }

        /// <summary>
        /// 奖金类型 
        /// </summary>      
        [OriginalField]
        [Display(Name = "奖金类型")]
        public BounsType? BounsType { get; set; }

        /// <summary>
        /// 奖金金额 
        /// </summary>         
        [OriginalField]
        [Display(Name = "奖金金额")]
        public Decimal? BounsAmount { get; set; }

        /// <summary>
        /// 剩余金额 
        /// </summary>         
        [OriginalField]
        [Display(Name = "剩余金额")]
        public Decimal? LeftAmount { get; set; }


        /// <summary>
        ///过期时间
        /// </summary>         
        [OriginalField]
        [Display(Name = "过期时间")]
        public DateTime? ExpireTime { get; set; }

        /// <summary>
        /// 奖金状态 
        /// </summary>         
        [OriginalField]
        [Display(Name = "奖金状态")]
        public BounsStatus? BounsStatus { get; set; }


        /// <summary>
        /// 抵扣率 
        /// </summary>         
        [OriginalField]
        [Display(Name = "抵扣率")]
        public Decimal? ConvertRate { get; set; }


        /// <summary>
        /// 使用金额限制 
        /// </summary>         
        [OriginalField]
        [Display(Name = "使用金额限制")]
        public Int32? LimitAmount { get; set; }



        /// <summary>
        /// 用户标识 
        /// </summary>         
        [OriginalField]
        [Display(Name = "用户标识")]
        public Int32? RelationId { get; set; }


        /// <summary>
        /// 关联数额 
        /// </summary>         
        [OriginalField]
        [Display(Name = "关联数额")]
        public Decimal? RelationValue { get; set; }


        /// <summary>
        /// 是否有效
        /// </summary>         
        [OriginalField]
        [Display(Name = "是否有效")]
        public Boolean? IsActive { get; set; }

        /// <summary>
        /// 备注
        /// </summary>         
        [OriginalField]
        [Display(Name = "备注")]
        public String Remarks { get; set; }

        /// <summary>
        /// 微信Id
        /// </summary>         
        [OriginalField]
        [Display(Name = "微信Id")]
        public String OpenId { get; set; }

    }
}

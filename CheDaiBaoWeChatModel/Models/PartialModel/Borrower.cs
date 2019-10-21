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
    public partial class Borrower : BaseModel
    {
        /// <summary>
        /// 用户名 
        /// </summary>         
        [OriginalField]
        [Display(Name = "用户名")]
        public String Aliases { get; set; }

        /// <summary>
        /// Guid标识 
        /// </summary>         
        [OriginalField]
        [Display(Name = "Guid标识")]
        public String Guid { get; set; }


        /// <summary>
        /// 真实姓名 
        /// </summary>         
        [OriginalField]
        [Display(Name = "真实姓名")]
        public String FullName { get; set; }




        /// <summary>
        /// 密码 
        /// </summary>         
        [OriginalField]
        [Display(Name = "密码")]
        public String LoginKey { get; set; }




        /// <summary>
        /// 手机 
        /// </summary>         
        [OriginalField]
        [Display(Name = "手机")]
        public String Phone { get; set; }


        /// <summary>
        /// 盐 
        /// </summary>         
        [OriginalField]
        [Display(Name = "盐")]
        public String Salt { get; set; }


        /// <summary>
        /// 验证码
        /// </summary>         
        [OriginalField]
        [Display(Name = "验证码")]
        public String Code { get; set; }

        /// <summary>
        /// 验证码创建时间 
        /// </summary>         
        [OriginalField]
        [Display(Name = "验证码创建时间")]
        public DateTime? CodeCreateTime { get; set; }


        /// <summary>
        /// 盐 
        /// </summary>         
        [OriginalField]
        [Display(Name = "盐")]
        public int CodeValid { get; set; }

        /// <summary>
        /// 证件ID 
        /// </summary>         
        [OriginalField]
        [Display(Name = "证件ID")]
        public String IDNumber { get; set; }




        /// <summary>
        /// 证件类型 
        /// </summary>         
        [OriginalField]
        [Display(Name = "证件类型")]
        public IDType? IDType { get; set; }




        /// <summary>
        /// 手机验证 
        /// </summary>         
        [OriginalField]
        [Display(Name = "手机验证")]
        public int IsValidatePhone { get; set; }



        /// <summary>
        /// 证件验证 
        /// </summary>         
        [OriginalField]
        [Display(Name = "证件验证")]
        public int IsIDNumber { get; set; }





        /// <summary>
        /// 微信Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "微信编号")]
        public String WeiXinId { get; set; }


        /// <summary>
        /// 推荐人 
        /// </summary>         
        [OriginalField]
        [Display(Name = "推荐人")]
        public int RecommendBorrowerId { get; set; }


        /// <summary>
        /// 推荐时间
        /// </summary>         
        [OriginalField]
        [Display(Name = "推荐时间")]
        public DateTime? RecommendTime { get; set; }

        /// <summary>
        ///推荐人标识符
        /// </summary>         
        [OriginalField]
        [Display(Name = "推荐人标识符")]
        public String RecommendedIdentifier { get; set; }

        /// <summary>
        ///客服
        /// </summary>         
        [OriginalField]
        [Display(Name = "客服")]
        public int CustomerServiceId { get; set; }


        /// <summary>
        ///客服关联时间 
        /// </summary>         
        [OriginalField]
        [Display(Name = "客服关联时间")]
        public DateTime? CustomerServiceTime { get; set; }


        /// <summary>
        ///微信唯一标示 
        /// </summary>         
        [OriginalField]
        [Display(Name = "微信唯一标示")]
        public string UnionId { get; set; }

        /// <summary>
        ///是否为业务员 
        /// </summary>         
        [OriginalField]
        [Display(Name = "是否为业务员")]
        public Boolean? IsSalesman { get; set; }

        /// <summary>
        ///是否被拒过 
        /// </summary>         
        [OriginalField]
        [Display(Name = "是否被拒过")]
        public Boolean? IsRejected { get; set; }

        
        /// <summary>
        ///是否为商户 
        /// </summary>         
        [OriginalField]
        [Display(Name = "是否为商户")]
        public Boolean? IsMerchant { get; set; }


        /// <summary>
        ///所属的公司 
        /// </summary>         
        [OriginalField]
        [Display(Name = "所属的公司")]
        public Company? Company { get; set; }



        /// <summary>
        ///所属的角色 
        /// </summary>         
        [OriginalField]
        [Display(Name = "所属的角色")]
        public LoanType? LoanType { get; set; }
    }
}


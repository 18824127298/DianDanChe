using CheDaiBaoCommonService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace CheDaiBaoWeChatModel.Models
{
    public partial class Member : BaseModel
    {
        /// <summary>
        ///手机号
        /// </summary>         
        [OriginalField]
        [Display(Name = "手机号")]
        public string Phone { get; set; }

        /// <summary>
        ///公众号Id
        /// </summary>         
        [OriginalField]
        [Display(Name = "公众号Id")]
        public string OpenId { get; set; }

        /// <summary>
        ///客户的姓名
        /// </summary>         
        [OriginalField]
        [Display(Name = "客户的姓名")]
        public string FullName { get; set; }

        /// <summary>
        ///会员的等级
        /// </summary>         
        [OriginalField]
        [Display(Name = "会员的等级")]
        public Int32? MemberLevel { get; set; }

        /// <summary>
        ///会员的标识
        /// </summary>         
        [OriginalField]
        [Display(Name = "会员的标识")]
        public string Guid { get; set; }

        /// <summary>
        ///验证码
        /// </summary>         
        [OriginalField]
        [Display(Name = "验证码")]
        public string Code { get; set; }
    }

}
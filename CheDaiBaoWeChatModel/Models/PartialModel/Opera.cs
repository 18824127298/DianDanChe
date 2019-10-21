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
    public partial class Opera : BaseModel
    {
        /// <summary>
        /// 操作类型（登录=1，实名认证=2，手机验证=3，充值。。） 
        /// </summary>         
        [OriginalField]
        [Display(Name = "操作类型")]
        public OperaType? OperaType { get; set; }




        /// <summary>
        /// 备注 
        /// </summary>         
        [OriginalField]
        [Display(Name = "备注")]
        public String Remark { get; set; }




        /// <summary>
        /// 关联Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "关联Id")]
        public Int32? RelationId { get; set; }




        /// <summary>
        /// 用户Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "用户Id")]
        public Int32? BorrowerId { get; set; }




        /// <summary>
        /// 客户端IP地址
        /// </summary>         
        [OriginalField]
        [Display(Name = "客户端IP地址")]
        public String ClientAddress { get; set; }

    }
}

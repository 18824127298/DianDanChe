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
    public partial class CapriciousLoan : BaseModel
    {
 
        /// <summary>
        /// 业务员或者商户Id
        /// </summary>         
        [OriginalField]
        [Display(Name = "业务员或者商户Id")]
        public int BorrowerId { get; set; }
 
        /// <summary>
        ///  客户手机号 
        /// </summary>         
        [OriginalField]
        [Display(Name = "客户手机号")]
        public string KeHuPhone { get; set; }

        /// <summary>
        ///  手机号 
        /// </summary>         
        [OriginalField]
        [Display(Name = "手机号")]
        public string Phone { get; set; }


        /// <summary>
        ///  秘钥 
        /// </summary>         
        [OriginalField]
        [Display(Name = "秘钥")]
        public string LoginKey { get; set; }
    }
}

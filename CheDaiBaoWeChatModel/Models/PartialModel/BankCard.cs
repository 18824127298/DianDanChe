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
    public partial class BankCard : BaseModel
    {


        /// <summary>
        /// 银行名称 
        /// </summary>         
        [OriginalField]
        [Display(Name = "银行名称")]
        public String Name { get; set; }




        /// <summary>
        /// 卡号 
        /// </summary>         
        [OriginalField]
        [Display(Name = "卡号")]
        public String Number { get; set; }





        /// <summary>
        /// 用户Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "用户Id")]
        public int BorrowerId { get; set; }




        /// <summary>
        /// 分行名称 
        /// </summary>         
        [OriginalField]
        [Display(Name = "分行名称")]
        public String Subbranch { get; set; }


        /// <summary>
        /// 银行卡类型 
        /// </summary>         
        [OriginalField]
        [Display(Name = "银行卡类型")]
        public String BankCardType { get; set; }
    }
}

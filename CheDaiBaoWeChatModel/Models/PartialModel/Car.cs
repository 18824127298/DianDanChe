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
    public partial class Car : BaseModel
    {
        /// <summary>
        /// 借款人标识 
        /// </summary>         
        [OriginalField]
        [Display(Name = "借款人标识")]
        public Int32? BorrowerId{get; set;}

        /// <summary>
        /// 车系
        /// </summary>         
        [OriginalField]
        [Display(Name = "车系")]
        public String CarSystem{get;set;}

        /// <summary>
        /// 车牌号
        /// </summary>         
        [OriginalField]
        [Display(Name = "车牌号")]
        public String CarNumber{get;set;}

        /// <summary>
        /// 发动机号码 
        /// </summary>         
        [OriginalField]
        [Display(Name = "发动机号码")]
        public String EngineNumber { get; set; }

        /// <summary>
        /// 车身架号码 
        /// </summary>         
        [OriginalField]
        [Display(Name = "车身架号码")]
        public String BodyRackNumber { get; set; }

        /// <summary>
        /// 图片的地址 
        /// </summary>         
        [OriginalField]
        [Display(Name = "图片的地址")]
        public String ImageUrl { get; set; }

        /// <summary>
        /// 备注 
        /// </summary>         
        [OriginalField]
        [Display(Name = "备注")]
        public String Remark { get; set; }

    }
}

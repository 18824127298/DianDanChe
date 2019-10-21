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
    public partial class CarType : BaseModel
    {

        /// <summary>
        /// 车名 
        /// </summary>         
        [OriginalField]
        [Display(Name = "车名")]
        public String CarName { get; set; }

        /// <summary>
        /// 车标题  
        /// </summary>         
        [OriginalField]
        [Display(Name = "车标题")]
        public String CarTitle { get; set; }


        /// <summary>
        /// 运营的类型 
        /// </summary>         
        [OriginalField]
        [Display(Name = "运营的类型")]
        public int OperateModelId { get; set; }


        /// <summary>
        /// 提车费 
        /// </summary>         
        [OriginalField]
        [Display(Name = "提车费")]
        public Decimal? LiftFares { get; set; }

        /// <summary>
        /// 尾款 
        /// </summary>         
        [OriginalField]
        [Display(Name = "尾款")]
        public Decimal? Retainage { get; set; } 

        /// <summary>
        /// 月供 
        /// </summary>         
        [OriginalField]
        [Display(Name = "月供")]
        public Decimal? MonthPrice { get; set; }

        /// <summary>
        /// 图片的地址 
        /// </summary>         
        [OriginalField]
        [Display(Name = "图片的地址")]
        public String ImageUrl { get; set; }

        /// <summary>
        /// 借款的期次 
        /// </summary>         
        [OriginalField]
        [Display(Name = "借款的期次")]
        public  int Stages { get; set; }
    }
}

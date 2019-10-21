using CheDaiBaoCommonService.Data;
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
    public partial class CarIllegal : BaseModel
    {
        /// <summary>
        /// 借款人标识 
        /// </summary>         
        [OriginalField]
        [Display(Name = "借款人标识")]
        public int BorrowerId { get; set; }
        /// <summary>
        /// 车辆Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "车辆Id")]
        public int CarId { get; set; }
        /// <summary>
        /// 车牌 
        /// </summary>         
        [OriginalField]
        [Display(Name = "车牌")]
        public String LicensePlate { get; set; }
        /// <summary>
        /// 违章的地点 
        /// </summary>         
        [OriginalField]
        [Display(Name = "违章的地点")]
        public String IllegalAddress { get; set; }
        /// <summary>
        /// 违章的时间 
        /// </summary>         
        [OriginalField]
        [Display(Name = "违章的时间")]
        public DateTime? IllegalTime { get; set; }
        /// <summary>
        /// 违规的标题 
        /// </summary>         
        [OriginalField]
        [Display(Name = "违规的标题")]
        public String IllegalTitle { get; set; }
        /// <summary>
        /// 违规的描述 
        /// </summary>         
        [OriginalField]
        [Display(Name = "违规的描述")]
        public String IllegalDescribe { get; set; }
        /// <summary>
        /// 罚款的金额 
        /// </summary>         
        [OriginalField]
        [Display(Name = "罚款的金额")]
        public Decimal? FinePrice { get; set; }
        /// <summary>
        /// 手续费 
        /// </summary>         
        [OriginalField]
        [Display(Name = "手续费")]
        public Decimal? AroundFee { get; set; }
        /// <summary>
        /// 分数 
        /// </summary>         
        [OriginalField]
        [Display(Name = "分数")]
        public int Points { get; set; }
        /// <summary>
        /// 处理的状态 
        /// </summary>         
        [OriginalField]
        [Display(Name = "处理的状态")]
        public int ProcessingState { get; set; }
        /// <summary>
        /// 操作人 
        /// </summary>         
        [OriginalField]
        [Display(Name = "操作人")]
        public int JudgeId { get; set; }
        /// <summary>
        /// 处理的时间 
        /// </summary>         
        [OriginalField]
        [Display(Name = "处理的时间")]
        public DateTime? StateTime { get; set; }
        /// <summary>
        /// 备注 
        /// </summary>         
        [OriginalField]
        [Display(Name = "备注")]
        public String Remark { get; set; }

    }
}

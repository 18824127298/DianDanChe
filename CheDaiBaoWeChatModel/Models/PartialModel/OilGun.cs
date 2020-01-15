using CheDaiBaoCommonService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
    public partial class OilGun : BaseModel
    {
        /// <summary>
        ///站点的Id
        /// </summary>         
        [OriginalField]
        [Display(Name = "站点的Id")]
        public Int32? GasStationId { get; set; }

        /// <summary>
        ///商品
        /// </summary>         
        [OriginalField]
        [Display(Name = "商品")]
        public string OilNumber { get; set; }


        ///// <summary>
        /////油号
        ///// </summary>         
        //[OriginalField]
        //[Display(Name = "油号")]
        //public Int32? OilType { get; set; }

        /// <summary>
        ///枪号
        /// </summary>         
        [OriginalField]
        [Display(Name = "枪号")]
        public Int32? GunNumber { get; set; }

        /// <summary>
        ///旧的减免额
        /// </summary>         
        [OriginalField]
        [Display(Name = "旧的减免额")]
        public Decimal? Amount { get; set; }

        /// <summary>
        ///时间点
        /// </summary>         
        [OriginalField]
        [Display(Name = "时间点")]
        public DateTime? PointTime { get; set; }

        /// <summary>
        ///新的减免额
        /// </summary>         
        [OriginalField]
        [Display(Name = "新的减免额")]
        public Decimal? NewAmount { get; set; }

        
        /// <summary>
        ///国标价
        /// </summary>         
        [OriginalField]
        [Display(Name = "国标价")]
        public Decimal? CountryMarkPrice { get; set; }

        /// <summary>
        ///新的国标价
        /// </summary>         
        [OriginalField]
        [Display(Name = "新的国标价")]
        public Decimal? NewCountryPrice { get; set; }

        /// <summary>
        ///国标价时间点
        /// </summary>         
        [OriginalField]
        [Display(Name = "国标价时间点")]
        public DateTime? CountryPointTime { get; set; }
    }
}

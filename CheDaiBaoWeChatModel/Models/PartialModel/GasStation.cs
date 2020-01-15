using CheDaiBaoCommonService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
     [Serializable]
    public partial class GasStation : BaseModel
    {

        /// <summary>
        /// 加油站名字 
        /// </summary>         
        [OriginalField]
        [Display(Name = "加油站名字")]
        public String Name { get; set; }

        /// <summary>
        /// 地址名 
        /// </summary>         
        [OriginalField]
        [Display(Name = "地址名")]
         public String AddressName { get; set; }

        /// <summary>
        /// 经度 
        /// </summary>         
        [OriginalField]
        [Display(Name = "经度")]
        public Decimal? Longitude { get; set; }

        /// <summary>
        /// 纬度 
        /// </summary>         
        [OriginalField]
        [Display(Name = "纬度")]
        public Decimal? Dimension { get; set; }

        /// <summary>
        /// 打印机编号 
        /// </summary>         
        [OriginalField]
        [Display(Name = "打印机编号")]
        public string PrinterNumber { get; set; }


        /// <summary>
        /// 品牌 
        /// </summary>         
        [OriginalField]
        [Display(Name = "品牌")]
        public string Brand { get; set; }

         
        /// <summary>
        /// 供应商 
        /// </summary>         
        [OriginalField]
        [Display(Name = "供应商")]
        public Int32? SupplierId { get; set; }
         
    }
}

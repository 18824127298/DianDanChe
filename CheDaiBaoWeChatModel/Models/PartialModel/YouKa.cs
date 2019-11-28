using CheDaiBaoCommonService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
    public class YouKa : BaseModel
    {
        /// <summary>
        /// 商品的Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "商品的Id")]
        public Decimal? ProductId { get; set; }

        /// <summary>
        /// 价格 
        /// </summary>         
        [OriginalField]
        [Display(Name = "价格")]
        public Decimal? Price { get; set; }

        /// <summary>
        /// 油类 
        /// </summary>         
        [OriginalField]
        [Display(Name = "油类")]
        public String Oils { get; set; }

        /// <summary>
        /// 容量
        /// </summary>         
        [OriginalField]
        [Display(Name = "容量")]
        public Decimal? Capacity { get; set; }

        /// <summary>
        /// 是否已付款 
        /// </summary>         
        [OriginalField]
        [Display(Name = "是否已付款")]
        public String IsPaid { get; set; }

    }
}

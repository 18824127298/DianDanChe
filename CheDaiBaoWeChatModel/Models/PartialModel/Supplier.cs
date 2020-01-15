using CheDaiBaoCommonService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{

    public class Supplier : BaseModel
    {
        /// <summary>
        /// 名字
        /// </summary>         
        [OriginalField]
        [Display(Name = "名字")]
        public string Name { get; set; }

        /// <summary>
        /// 手机号 
        /// </summary>         
        [OriginalField]
        [Display(Name = "手机号")]
        public string Phone { get; set; }

        /// <summary>
        /// 余额 
        /// </summary>         
        [OriginalField]
        [Display(Name = "余额")]
        public Decimal? Balance { get; set; }

        /// <summary>
        /// 优惠价 
        /// </summary>         
        [OriginalField]
        [Display(Name = "优惠价")]
        public Decimal? Concessional { get; set; }

        /// <summary>
        /// 优惠价 
        /// </summary>         
        [OriginalField]
        [Display(Name = "优惠价")]
        public Decimal? NewConcessional { get; set; }
        
        /// <summary>
        /// 时间点 
        /// </summary>         
        [OriginalField]
        [Display(Name = "时间点")]
        public DateTime? ConcessionalPointTime { get; set; }

        /// <summary>
        /// 主账号 
        /// </summary>         
        [OriginalField]
        [Display(Name = "主账号")]
        public string Number { get; set; }
    }
}

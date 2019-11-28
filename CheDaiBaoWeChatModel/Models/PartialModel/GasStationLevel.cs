using CheDaiBaoCommonService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
    public partial class GasStationLevel : BaseModel
    {

        /// <summary>
        /// 加油站Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "加油站Id")]
        public Int32? GasStationId { get; set; }

        /// <summary>
        /// 会员的等级
        /// </summary>         
        [OriginalField]
        [Display(Name = "会员的等级")]
        public Int32? MemberLevel { get; set; }

        /// <summary>
        /// 减免额
        /// </summary>         
        [OriginalField]
        [Display(Name = "减免额")]
        public Decimal? Reduction { get; set; }
    }

}

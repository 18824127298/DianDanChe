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
    public partial class PriceReduction : BaseModel
    {
        /// <summary>
        /// 站点的Id 
        /// </summary>         
        [OriginalField]
        [Display(Name = "站点的Id")]
        public Int32? GasStationId { get; set; }

        /// <summary>
        /// 降价的金额 
        /// </summary>         
        [OriginalField]
        [Display(Name = "降价的金额")]
        public String PreferentialAmount { get; set; }

        /// <summary>
        /// 会员的等级 
        /// </summary>         
        [OriginalField]
        [Display(Name = "会员的等级")]
        public Int32? MemberLevel { get; set; }
    }
}

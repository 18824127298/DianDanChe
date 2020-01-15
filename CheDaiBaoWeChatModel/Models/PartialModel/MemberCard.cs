using CheDaiBaoCommonService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
    public partial class MemberCard : BaseModel
    {
        /// <summary>
        ///会员的Id
        /// </summary>         
        [OriginalField]
        [Display(Name = "会员的Id")]
        public Int32? MemberId { get; set; }

        /// <summary>
        ///卡号
        /// </summary>         
        [OriginalField]
        [Display(Name = "卡号")]
        public string CardNumber { get; set; }

        /// <summary>
        ///折扣
        /// </summary>         
        [OriginalField]
        [Display(Name = "折扣")]
        public Decimal? Discount { get; set; }

        /// <summary>
        ///卡等级
        /// </summary>         
        [OriginalField]
        [Display(Name = "卡等级")]
        public Int32? CardLevel { get; set; }

        /// <summary>
        ///卡的代理商
        /// </summary>         
        [OriginalField]
        [Display(Name = "卡的代理商")]
        public Int32? AgentId { get; set; }

        
        /// <summary>
        ///卡所属品牌
        /// </summary>         
        [OriginalField]
        [Display(Name = "卡所属品牌")]
        public CardBrand? CardBrand { get; set; }
        
    }
}

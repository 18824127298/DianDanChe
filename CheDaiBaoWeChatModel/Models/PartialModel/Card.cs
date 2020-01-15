
using CheDaiBaoCommonService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
    public partial class Card : BaseModel
    {
        /// <summary>
        ///卡号
        /// </summary>         
        [OriginalField]
        [Display(Name = "卡号")]
        public string CardNumber { get; set; }

        /// <summary>
        ///卡折扣
        /// </summary>         
        [OriginalField]
        [Display(Name = "卡折扣")]
        public Decimal? CardDiscount { get; set; }

        /// <summary>
        ///卡等级
        /// </summary>         
        [OriginalField]
        [Display(Name = "卡等级")]
        public Int32? CardLevel { get; set; }

        /// <summary>
        ///卡提成
        /// </summary>         
        [OriginalField]
        [Display(Name = "卡提成")]
        public Int32? CardRoyalty { get; set; }

        /// <summary>
        ///会员的标识
        /// </summary>         
        [OriginalField]
        [Display(Name = "会员的标识")]
        public Int32? MemberId { get; set; }


        /// <summary>
        ///代理商标识
        /// </summary>         
        [OriginalField]
        [Display(Name = "代理商标识")]
        public Int32? AgentId { get; set; }


        /// <summary>
        ///物流公司标识
        /// </summary>         
        [OriginalField]
        [Display(Name = "物流公司标识")]
        public Int32? SupplierId { get; set; }


        /// <summary>
        ///卡片的状态
        /// </summary>         
        [OriginalField]
        [Display(Name = "卡片的状态")]
        public CardStatus? CardStatus { get; set; }


        /// <summary>
        ///车牌号
        /// </summary>         
        [OriginalField]
        [Display(Name = "车牌号")]
        public string CarNumber { get; set; }


        /// <summary>
        ///是否可以充值
        /// </summary>         
        [OriginalField]
        [Display(Name = "是否可以充值")]
        public Boolean? IsRecharge { get; set; }


        /// <summary>
        ///车主的车牌号
        /// </summary>         
        [OriginalField]
        [Display(Name = "车主的车牌号")]
        public string OwnerNumber { get; set; }


        /// <summary>
        ///所属的公司
        /// </summary>         
        [OriginalField]
        [Display(Name = "所属的公司")]
        public CardBrand? CardBrand { get; set; }
    }
}

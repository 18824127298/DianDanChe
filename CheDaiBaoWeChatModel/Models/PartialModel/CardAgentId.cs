using CheDaiBaoCommonService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{

    public partial class CardAgentId : BaseModel
    {
        /// <summary>
        ///卡号的标识
        /// </summary>         
        [OriginalField]
        [Display(Name = "卡号的标识")]
        public Int32? CardId { get; set; }

        /// <summary>
        ///下级代理商标识
        /// </summary>         
        [OriginalField]
        [Display(Name = "下级代理商标识")]
        public Int32? AgentId { get; set; }

        /// <summary>
        ///上级代理商标识
        /// </summary>         
        [OriginalField]
        [Display(Name = "上级代理商标识")]
        public Int32? RecommendAgentId { get; set; }

        /// <summary>
        /// 会员的标识
        /// </summary>         
        [OriginalField]
        [Display(Name = "会员的标识")]
        public Int32? MemberId { get; set; }
    }
}

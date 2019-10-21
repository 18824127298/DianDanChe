using CheDaiBaoCommonService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
    /// <summary>
    ///  
    /// </summary>
    [Serializable]
    public partial class Contacts : BaseModel
    {

        /// <summary>
        /// 关系
        /// </summary>         
        [OriginalField]
        [Display(Name = "关系")]
        public string Contact { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>         
        [OriginalField]
        [Display(Name = "手机号")]
        public string Phone { get; set; }


        /// <summary>
        /// 客户Id
        /// </summary>         
        [OriginalField]
        [Display(Name = "客户Id")]
        public int BorrowerId { get; set; }


        /// <summary>
        /// 借款Id
        /// </summary>         
        [OriginalField]
        [Display(Name = "借款Id")]
        public int LoanapplyId { get; set; }


        /// <summary>
        /// 是否知晓分期
        /// </summary>
        [OriginalField]
        [Display(Name = "是否知晓分期")]
        public Boolean? IsKnowStages
        {
            get;
            set;
        }
    }
}

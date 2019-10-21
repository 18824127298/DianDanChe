using CheDaiBaoCommonService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
    public partial class Discount : BaseModel
    {
 
       
        /// <summary>
        ///减免额
        /// </summary>         
        [OriginalField]
        [Display(Name = "减免额")]
        public Decimal? Amount { get; set; }


        /// <summary>
        /// 剩余减免额
        /// </summary>         
        [OriginalField]
        [Display(Name = "剩余减免额")]
        public Decimal? LeftAmount { get; set; }


      

        /// <summary>
        /// 借款人 
        /// </summary>         
        [OriginalField]
        [Display(Name = "借款人")]
        public int BorrowerId { get; set; }

        /// <summary>
        /// 借款编号
        /// </summary>         
        [OriginalField]
        [Display(Name = "借款Id")]
        public int LoanApplyId { get; set; }


         /// <summary>
        /// 关联的标识
        /// </summary>         
        [OriginalField]
        [Display(Name = "关联的Id")]
        public int RelationId { get; set; }


        /// <summary>
        /// 创建人
        /// </summary>         
        [OriginalField]
        [Display(Name = "创建人")]
        public string Creator { get; set; }

        /// <summary>
        /// 减免的原因
        /// </summary>         
        [OriginalField]
        [Display(Name = "减免的原因")]
        public string Remark { get; set; }

        /// <summary>
        /// 第一次审核结果
        /// </summary>         
        [OriginalField]
        [Display(Name = "第一次审核结果")]
        public Boolean? IsAudit { get; set; }


        /// <summary>
        /// 第一次审核的时间
        /// </summary>         
        [OriginalField]
        [Display(Name = "第一次审核的时间")]
        public DateTime? AuditTime { get; set; }

        /// <summary>
        /// 第一次审核人
        /// </summary>         
        [OriginalField]
        [Display(Name = "第一次审核人")]
        public string Auditor { get; set; }


        /// <summary>
        /// 第二次审核结果
        /// </summary>         
        [OriginalField]
        [Display(Name = "第二次审核结果")]
        public Boolean? SecondAuditResult { get; set; }


        /// <summary>
        /// 第二次审核的时间
        /// </summary>         
        [OriginalField]
        [Display(Name = "第二次审核的时间")]
        public DateTime? SecondAuditTime { get; set; }

        /// <summary>
        /// 第二次审核人
        /// </summary>         
        [OriginalField]
        [Display(Name = "第二次审核人")]
        public string SecondAuditor { get; set; }


        /// <summary>
        /// 第一次审核备注
        /// </summary>         
        [OriginalField]
        [Display(Name = "第一次审核备注")]
        public string AuditRemark { get; set; }


        /// <summary>
        /// 第二次审核备注
        /// </summary>         
        [OriginalField]
        [Display(Name = "第二次审核备注")]
        public string SecondAuditRemark { get; set; }
    }
}

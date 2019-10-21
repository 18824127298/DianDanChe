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
    public partial class DriverApplication : BaseModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>         
        [OriginalField]
        [Display(Name = "用户Id")]
        public Int32? BorrowerId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>         
        [OriginalField]
        [Display(Name = "姓名")]
        public String FullName { get; set; }




        /// <summary>
        /// 手机号
        /// </summary>         
        [OriginalField]
        [Display(Name = "手机号")]
        public String Phone { get; set; }




        /// <summary>
        /// 审核状态
        /// </summary>         
        [OriginalField]
        [Display(Name = "审核状态")]
        public Int32? IsAudit { get; set; }




        /// <summary>
        /// 审核时间
        /// </summary>         
        [OriginalField]
        [Display(Name = "审核时间")]
        public DateTime? AuditTime { get; set; }




        /// <summary>
        /// 审核人
        /// </summary>         
        [OriginalField]
        [Display(Name = "审核人")]
        public String Auditor { get; set; }



        /// <summary>
        /// 审核备注
        /// </summary>         
        [OriginalField]
        [Display(Name = "审核备注")]
        public String AuditorRemark { get; set; }
    }
}

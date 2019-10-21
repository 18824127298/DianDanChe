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
    public partial class IdCardInformation : BaseModel
    {
        /// <summary>
        /// 身份证姓名 
        /// </summary>         
        [OriginalField]
        [Display(Name = "身份证姓名")]
        public String Name { get; set; }


        /// <summary>
        /// 身份证出生
        /// </summary>         
        [OriginalField]
        [Display(Name = "身份证出生")]
        public String Birth { get; set; }

        /// <summary>
        /// 身份证地址
        /// </summary>         
        [OriginalField]
        [Display(Name = "身份证地址")]
        public String Address { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>         
        [OriginalField]
        [Display(Name = "身份证号码")]
        public String IdCardNumber { get; set; }

        /// <summary>
        /// 身份证性别 
        /// </summary>         
        [OriginalField]
        [Display(Name = "身份证性别")]
        public String Sex { get; set; }

        /// <summary>
        /// 身份证民族
        /// </summary>         
        [OriginalField]
        [Display(Name = "身份证民族")]
        public String Nation { get; set; }


        /// <summary>
        /// 身份证签发机关
        /// </summary>         
        [OriginalField]
        [Display(Name = "身份证签发机关")]
        public String SigningOrganization { get; set; }

        /// <summary>
        /// 身份证签发日期 
        /// </summary>         
        [OriginalField]
        [Display(Name = "身份证签发日期")]
        public String IssuanceDate { get; set; }

        /// <summary>
        /// 身份证失效日期
        /// </summary>         
        [OriginalField]
        [Display(Name = "身份证失效日期")]
        public String ExpirationDate { get; set; }

        /// <summary>
        /// 借款人标识
        /// </summary>         
        [OriginalField]
        [Display(Name = "借款人标识")]
        public Int32? BorrowerId { get; set; }

    }
}

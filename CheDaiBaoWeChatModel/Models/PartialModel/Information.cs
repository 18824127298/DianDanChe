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
    public partial class Information : BaseModel
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


        /// <summary>
        /// 银行名称 
        /// </summary>         
        [OriginalField]
        [Display(Name = "银行名称")]
        public String BankCardName { get; set; }

        /// <summary>
        /// 卡号 
        /// </summary>         
        [OriginalField]
        [Display(Name = "卡号")]
        public String Number { get; set; }

        /// <summary>
        /// 银行卡类型 
        /// </summary>         
        [OriginalField]
        [Display(Name = "银行卡类型")]
        public String BankCardType { get; set; }


        /// <summary>
        /// 客户分类
        /// </summary>
        [OriginalField]
        [Display(Name = "客户分类")]
        public string CustomerClassification
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        [OriginalField]
        [Display(Name = "备注")]
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 商户名称
        /// </summary>
        [OriginalField]
        [Display(Name = "商户名称")]
        public string BusinessName
        {
            get;
            set;
        }

        /// <summary>
        /// 招聘点名称
        /// </summary>
        [OriginalField]
        [Display(Name = "招聘点名称")]
        public string RecruitmentName
        {
            get;
            set;
        }
    }
}

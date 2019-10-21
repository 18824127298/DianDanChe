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
    public partial class LoanFile : BaseModel
    {
        /// <summary>
        /// 申请借款的Id
        /// </summary>
        [OriginalField]
        [Display(Name = "申请借款的Id")]
        public Int32? LoanApplyId { get; set; }

        /// <summary>
        /// 身份证正面照路径
        /// </summary>
        [OriginalField]
        [Display(Name = "身份证正面照路径")]
        public string ZhengFilePath { get; set; }


        /// <summary>
        /// 身份证反面照路径
        /// </summary>
        [OriginalField]
        [Display(Name = "身份证反面照路径")]
        public string FanFilePath { get; set; }


        /// <summary>
        /// 银行卡路径
        /// </summary>
        [OriginalField]
        [Display(Name = "银行卡路径")]
        public string BankCardPath { get; set; }


        /// <summary>
        /// 用户的标识
        /// </summary>
        [OriginalField]
        [Display(Name = "用户的标识")]
        public Int32? BorrowerId
        {
            get;
            set;
        }

        /// <summary>
        /// 身份信息关联标识
        /// </summary>
        [OriginalField]
        [Display(Name = "身份信息关联标识")]
        public Int32? IdCardInformationId
        {
            get;
            set;
        }

        /// <summary>
        /// 银行卡关联的标识
        /// </summary>
        [OriginalField]
        [Display(Name = "银行卡关联的标识")]
        public Int32? BankCardId
        {
            get;
            set;
        }


        /// <summary>
        /// 人脸识别对比分数
        /// </summary>
        [OriginalField]
        [Display(Name = "人脸识别对比分数")]
        public Decimal? Score
        {
            get;
            set;
        }


        /// <summary>
        /// 微信身份证正面照路径
        /// </summary>
        [OriginalField]
        [Display(Name = "微信身份证正面照路径")]
        public string WeChatZhengFilePath { get; set; }


        /// <summary>
        /// 微信身份证反面照路径
        /// </summary>
        [OriginalField]
        [Display(Name = "微信身份证反面照路径")]
        public string WeChatFanFilePath { get; set; }


        /// <summary>
        /// 微信银行卡路径
        /// </summary>
        [OriginalField]
        [Display(Name = "微信银行卡路径")]
        public string WeChatBankCardPath { get; set; }
    }
}

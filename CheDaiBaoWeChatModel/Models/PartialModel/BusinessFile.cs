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
    public partial class BusinessFile : BaseModel
    {

        /// <summary>
        /// 文件业务类型
        /// </summary>         
        [OriginalField]
        [Display(Name = "文件业务类型")]
        public BusinessType? BusinessType { get; set; }


        /// <summary>
        /// 文件名称 
        /// </summary>         
        [OriginalField]
        [Display(Name = "文件名称")]
        public String FileName { get; set; }


        /// <summary>
        /// 关联编号
        /// </summary>         
        [OriginalField]
        [Display(Name = "关联编号")]
        public Int32? RelationId { get; set; }

        /// <summary>
        /// 客户的标识 
        /// </summary>         
        [OriginalField]
        [Display(Name = "客户的标识")]
        public Int32? BorrowerId { get; set; }


        /// <summary>
        /// 路径 
        /// </summary>         
        [OriginalField]
        [Display(Name = "路径")]
        public String FilePath { get; set; }

        /// <summary>
        /// 排序 
        /// </summary>         
        [OriginalField]
        [Display(Name = "排序")]
        public Int32? Sort { get; set; }


        /// <summary>
        /// 微信图片地址 
        /// </summary>         
        [OriginalField]
        [Display(Name = "微信图片地址")]
        public String WeChatPath { get; set; }
    }
}

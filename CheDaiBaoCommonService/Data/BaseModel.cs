using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Data
{
    public abstract partial class BaseModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [OriginalFieldAttribute]
        [Display(Name = "编号")]
        public virtual int Id { get; set; }
        
        /// <summary>
        /// 有效否
        /// </summary>
        [OriginalFieldAttribute]
        [Display(Name = "有效否 ")]
        public virtual int IsValid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [OriginalFieldAttribute]
        [Display(Name = "创建时间 ")]
        public virtual DateTime? CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [OriginalFieldAttribute]
        [Display(Name = "修改时间 ")]
        public virtual DateTime? UpdateTime { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class OriginalFieldAttribute : Attribute
    {
    }
}

using CheDaiBaoCommonService.Data;
using CheDaiBaoWeChatModel.Validators;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
    [Serializable]
    public partial class OperateModel : BaseModel
    {
        /// <summary>
        /// 用户名 
        /// </summary>         
        [OriginalField]
        [Display(Name = "用户名")]
        public String Name { get; set; }
    }
}



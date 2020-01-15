using CheDaiBaoCommonService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
     public partial class Agent : BaseModel
     {
         /// <summary>
         ///代理商名字
         /// </summary>         
         [OriginalField]
         [Display(Name = "代理商名字")]
         public string Name { get; set; }

        /// <summary>
        ///手机号
        /// </summary>         
        [OriginalField]
        [Display(Name = "手机号")]
        public string Phone { get; set; }
    }
}

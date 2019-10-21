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
    public partial class SendMessageHistory : BaseModel
    {
        /// <summary>
        /// 手机号 
        /// </summary>         
        [OriginalField]
        [Display(Name = "手机号")]
        public String Phone { get; set; }




        /// <summary>
        /// 短信的内容 
        /// </summary>         
        [OriginalField]
        [Display(Name = "短信的内容")]
        public String SmsContent { get; set; }





        /// <summary>
        /// 创建人标识
        /// </summary>         
        [OriginalField]
        [Display(Name = "创建人标识")]
        public int Creator { get; set; }


        /// <summary>
        /// 发送的状态 
        /// </summary>         
        [OriginalField]
        [Display(Name = "发送的状态")]
        public String SendStatus { get; set; }




        /// <summary>
        /// 发送失败的原因 
        /// </summary>         
        [OriginalField]
        [Display(Name = "发送失败的原因")]
        public String SendFailMsg { get; set; }




        /// <summary>
        /// 发送的时间 
        /// </summary>         
        [OriginalField]
        [Display(Name = "发送的时间")]
        public String SendTime { get; set; }





        /// <summary>
        /// 备注 
        /// </summary>         
        [OriginalField]
        [Display(Name = "备注")]
        public String Remarks { get; set; }
    }
}

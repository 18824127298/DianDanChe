using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
    public class MobileCodeInfoModel
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string MobileCode { get; set; }

        /// <summary>
        /// 验证码过期时间
        /// </summary>
        public DateTime MobileCodeExpires { get; set; }

        /// <summary>
        /// 接受验证码的手机号码
        /// </summary>
        public string MobileNumber { get; set; }
    }
}

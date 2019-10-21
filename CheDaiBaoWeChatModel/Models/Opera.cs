using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
    /// <summary>
    /// 操作类
    /// </summary>
    public partial class Opera
    {
        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 操作人用户名
        /// </summary>
        public string Aliases { get; set; }
    }
}

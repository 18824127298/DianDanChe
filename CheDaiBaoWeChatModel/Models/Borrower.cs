using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
    public partial class Borrower
    {
        public string BankCard { get; set; }

        public int BankCardId { get; set; }

        public WeiXin WeiXin { get; set; }

    }

    public class WeiXin
    {
        public string UnionId { get; set; }

        public string HeadImgurl { get; set; }

        public string NickName { get; set; }

        public string OpenId { get; set; }

        public string Token { get; set; }

    }
}

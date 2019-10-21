using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
    public partial class LoanApply
    {
        public Decimal? OutstandingInterest { get; set; }

        public Decimal? UnPrincipal { get; set; }

        public Borrower borrower { get; set; }

        public string syear { get; set; }

        public Int32? nmonth { get; set; }

        public Int32? day { get; set; }

        public Int32? count { get; set; }
    }
}

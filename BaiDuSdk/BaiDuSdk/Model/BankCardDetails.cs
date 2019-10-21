using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiDuSdk.Model
{
    public class BankCardDetails
    {
        public string bank_card_number { get; set; }
        public string valid_dat { get; set; }
        public string bank_card_type { get; set; }
        public string bank_name { get; set; }
    }
}
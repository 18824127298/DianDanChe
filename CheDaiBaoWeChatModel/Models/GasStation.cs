using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoWeChatModel.Models
{
    public partial class GasStation
    {
        public double Distance { get; set; }
        public decimal Reduction { get; set; }
        public decimal Amount { get; set; }
        public decimal CountryMarkPrice { get; set; }
        public decimal NewAmount { get; set; }
        public decimal NewCountryPrice { get; set; }
        public DateTime PointTime { get; set; }
        public DateTime CountryPointTime { get; set; }
    }
}

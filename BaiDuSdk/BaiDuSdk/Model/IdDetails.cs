using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiDuSdk.Model
{
    public class IdDetails
    {
        public Location location { get; set; }
        public string words { get; set; }
    }

    public class Location
    {
        public string width { get; set; }
        public string top { get; set; }
        public string left { get; set; }
        public string height { get; set; }
    }
}

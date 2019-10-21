using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiDuSdk.Model
{
    public class PersonVerify
    {
        public string error_code { get; set; }
        public string error_msg { get; set; }
        public string log_id { get; set; }
        public string timestamp { get; set; }
        public string cached { get; set; }
        public PersonVerifyDetails result { get; set; }
    }

    public class PersonVerifyDetails
    {
        public string score { get; set; }
    }
}

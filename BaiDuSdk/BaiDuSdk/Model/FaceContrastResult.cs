using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiDuSdk.Model
{
    public class FaceContrastResult
    {
        public string error_code { get; set; }
        public string error_msg { get; set; }
        public string log_id { get; set; }
        public string timestamp { get; set; }
        public string cached { get; set; }
        public FaceResult result { get; set; }
    }

    public class FaceResult
    {
        public string score { get; set; }
    }
}

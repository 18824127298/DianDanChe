using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiDuSdk.Model
{
    public class IdCardNumber
    {
        public string log_id { get; set; }

        public string words_result_num { get; set; }

        public string direction { get; set; }

        public string image_status { get; set; }

        public WordsResult words_result { get; set; }
    }

}

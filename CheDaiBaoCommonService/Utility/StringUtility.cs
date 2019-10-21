using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheDaiBaoCommonService.Utility
{
    public class StringUtility
    {
        public static string HideString(string str, int start = 0, int length = 0, string speator = "*")
        {
            if (string.IsNullOrEmpty(str) || start >= str.Length)
            {
                return str;
            }
            else
            {
                if (length == 0 || length>str.Length-start)
                {
                    length = str.Length - 1;
                }
                string retStr = str.Substring(0, start);
                for (int i = 0; i < length; i++)
                {
                    retStr += speator;
                }
                return retStr + str.Substring(start + length);
            }
        }
    }
}

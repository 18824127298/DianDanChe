using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.IBXService.VCodeBreak.YunNan
{
    public class BYNBreakRequest
    {
        private string _imageFileName;
        /// <summary>
        /// 验证码图片文件路径
        /// </summary>
        public string ImageFileName
        {
            get
            {
                return _imageFileName;
            }
            set 
            {
                _imageFileName = value;
            }
        }
    }
}

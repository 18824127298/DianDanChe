using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.IBXService.VCodeBreak.ThirdEngine.UUCloud
{
    /// <summary>
    /// 验证码识别请求
    /// </summary>
    public class BUUBreakRequest
    {
        private string _imageFileName = "";
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

        private int _uuCodeType = 0;
        /// <summary>
        /// UU云的CodeType
        /// </summary>
        public int UUCodeType
        {
            get
            {
                return _uuCodeType;
            }
            set
            {
                _uuCodeType = value;
            }
        }
    }
}

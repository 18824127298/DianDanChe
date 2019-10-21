using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Net.IBXService.VCodeBreak.ThirdEngine.UUCloud
{
    /// <summary>
    /// 验证码识别请求
    /// </summary>
    public class BUUBreakRequest
    {
        public BUUBreakRequest(string sImageFileName, string sUUCodeType)
        {
            this._imageFileName = sImageFileName;
            this._uuCodeType = sUUCodeType;
        }

        public BUUBreakRequest()
        { }

        private string _imageFileName;
        /// <summary>
        /// 验证码图片文件路径
        /// </summary>
        public string ImageFileName
        {
            get
            {
                if (_imageFileName.Trim() == "")
                    throw new Exception("ImageFileName is NULL!");
                return _imageFileName;
            }
            set
            {
                _imageFileName = value;
            }
        }

        private string _uuCodeType;
        /// <summary>
        /// UU云的CodeType
        /// </summary>
        public string UUCodeType
        {
            get
            {
                if (_uuCodeType.Trim() == "")
                {
                    _uuCodeType = "1004";
                }
                return _uuCodeType;
            }
            set
            {
                try
                {
                    int nTemp = int.Parse(value.Trim());
                    _uuCodeType = value;
                }
                catch
                {
                    _uuCodeType = "1004";
                }
            }
        }
    }
}

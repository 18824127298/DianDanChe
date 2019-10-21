using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Common.MMLib.ImageFileProcess
{
    /// <summary>
    /// 获取像素的方法
    /// </summary>
    public enum ImageFileCompare_GetPixelMethod
    {
        /// <summary>
        /// 像素法
        /// </summary>
        GetPixel,
        /// <summary>
        /// 内存法
        /// </summary>
        Memory
    }

    public class ImageFileCompareConfig
    {
        private static ImageFileCompareConfig _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static ImageFileCompareConfig Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new ImageFileCompareConfig();
                return _thisInstance;
            }
        }

        private ImageFileCompare_GetPixelMethod _getPixelMethod = ImageFileCompare_GetPixelMethod.Memory;
        /// <summary>
        /// 获取像素的方法
        /// </summary>
        public ImageFileCompare_GetPixelMethod GetPixelMethod
        {
            get { return _getPixelMethod; }
            set { _getPixelMethod = value; }
        }
    }
}

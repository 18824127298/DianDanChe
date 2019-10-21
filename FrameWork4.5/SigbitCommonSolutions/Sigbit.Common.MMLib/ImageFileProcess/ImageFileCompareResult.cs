using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Common.MMLib.ImageFileProcess
{
    public class ImageFileCompareResult
    {
        private int _totalPixelCount = 0;
        /// <summary>
        /// 像素总数
        /// </summary>
        public int TotalPixelCount
        {
            get { return _totalPixelCount; }
            set { _totalPixelCount = value; }
        }

        private int _differentPixelCount = 0;
        /// <summary>
        /// 不相同的像素总数
        /// </summary>
        public int DifferentPixelCount
        {
            get { return _differentPixelCount; }
            set { _differentPixelCount = value; }
        }

        private double _differentRate = 0.0;
        /// <summary>
        /// 差异率。例如，两个图像有50%不同，则差异率的值填为50.0。
        /// </summary>
        public double DifferentRate
        {
            get { return _differentRate; }
            set { _differentRate = value; }
        }
    }
}

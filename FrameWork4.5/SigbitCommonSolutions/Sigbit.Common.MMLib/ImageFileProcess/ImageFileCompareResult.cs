using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Common.MMLib.ImageFileProcess
{
    public class ImageFileCompareResult
    {
        private int _totalPixelCount = 0;
        /// <summary>
        /// ��������
        /// </summary>
        public int TotalPixelCount
        {
            get { return _totalPixelCount; }
            set { _totalPixelCount = value; }
        }

        private int _differentPixelCount = 0;
        /// <summary>
        /// ����ͬ����������
        /// </summary>
        public int DifferentPixelCount
        {
            get { return _differentPixelCount; }
            set { _differentPixelCount = value; }
        }

        private double _differentRate = 0.0;
        /// <summary>
        /// �����ʡ����磬����ͼ����50%��ͬ��������ʵ�ֵ��Ϊ50.0��
        /// </summary>
        public double DifferentRate
        {
            get { return _differentRate; }
            set { _differentRate = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Framework.TiiChart
{
    /// <summary>
    /// 坐标轴
    /// </summary>
    public class TiiAxis
    {
        private double _originValue = 0;
        /// <summary>
        /// 原点
        /// </summary>
        public double OriginValue
        {
            get { return _originValue; }
            set 
            {
                _originValue = value;
                IsSetOrigin = true;
            }
        }

        private double _maxValue = 0;
        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue
        {
            get { return _maxValue; }
            set
            { 
                _maxValue = value;
                IsSetMax = true;
            }
        }

        private bool _isSetOrigin = false;
        /// <summary>
        /// 是否设置原点
        /// </summary>
        public bool IsSetOrigin
        {
            get { return _isSetOrigin; }
            set { _isSetOrigin = value; }
        }

        private bool _isSetMax = false;
        /// <summary>
        /// 是否设置最大值
        /// </summary>
        public bool IsSetMax
        {
            get { return _isSetMax; }
            set { _isSetMax = value; }
        }
    }
}

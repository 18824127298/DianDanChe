using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Framework.TiiChart
{
    /// <summary>
    /// 限制类
    /// </summary>
    public class TiiLimit
    {
        private double _minPercent = 0;
        /// <summary>
        /// 最小百分比
        /// </summary>
        public double MinPercent
        {
            get { return _minPercent; }
            set 
            {
                _minPercent = value;
                _isLimit = true;
            }
        }

        private string _otherName = "其它";
        /// <summary>
        /// 其它名称
        /// </summary>
        public string OtherName
        {
            get { return _otherName; }
            set { _otherName = value; }
        }

        private bool _isLimit = false;
        /// <summary>
        /// 只读,是否启用限制
        /// </summary>
        public bool IsLimit
        {
            get { return _isLimit; }
        }
    }
}

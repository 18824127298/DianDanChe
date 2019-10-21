using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Framework.TiiChart
{
    /// <summary>
    /// 3D显示类
    /// </summary>
    public class TiiView3D
    {
        private bool _is3D = true;
        /// <summary>
        /// 是否显示3D效果
        /// </summary>
        public bool Is3D
        {
            get { return _is3D; }
            set { _is3D = value; }
        }

        private int _percent3D = 10;
        /// <summary>
        /// 3D百分比，0%～100%
        /// </summary>
        public int Percent3D
        {
            get { return _percent3D; }
            set
            {
                if (value > 100)
                {
                    value = 100;
                }
                if (value < 0)
                {
                    value = 0;
                }
                _percent3D = value; 
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Framework.TiiChart
{
    /// <summary>
    /// 风格类
    /// </summary>
    public class TiiStyle
    {
        private TiiStyleSet _styleSet = TiiStyleSet.Classic;
        /// <summary>
        /// 风格集
        /// </summary>
        public TiiStyleSet StyleSet
        {
            get { return _styleSet; }
            set { _styleSet = value; }
        }
    }
}

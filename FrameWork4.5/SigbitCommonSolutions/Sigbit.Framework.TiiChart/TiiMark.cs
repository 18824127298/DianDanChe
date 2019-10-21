using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Framework.TiiChart
{
    /// <summary>
    /// 标记类
    /// </summary>
    public class TiiMark
    {
        private TiiMarkDisplayContent _markDisplayContent = TiiMarkDisplayContent.Value;
        /// <summary>
        /// 标记显示内容
        /// </summary>
        public TiiMarkDisplayContent MarkDisplayContent
        {
            get { return _markDisplayContent; }
            set { _markDisplayContent = value; }
        }

        private bool _isShowMark = true;
        /// <summary>
        /// 是否显示标记
        /// </summary>
        public bool IsShowMark
        {
            get { return _isShowMark; }
            set { _isShowMark = value; }
        }
    }
}

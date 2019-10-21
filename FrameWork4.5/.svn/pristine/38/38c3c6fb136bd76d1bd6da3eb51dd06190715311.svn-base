using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace Sigbit.Framework.NaviTab
{
    /// <summary>
    /// 装载多个Bar条
    /// </summary>
    class NVTBarContainer
    {
        private Hashtable _htBars = new Hashtable();

        private string _currentBarName = "";
        /// <summary>
        /// 当前Bar条的名称
        /// </summary>
        public string CurrentBarName
        {
            get { return _currentBarName; }
            set { _currentBarName = value; }
        }

        /// <summary>
        /// 当前的Bar条
        /// </summary>
        public NVTBar CurrentBar
        {
            get
            {
                NVTBar currentBar = (NVTBar)_htBars[this.CurrentBarName];
                if (currentBar == null)
                {
                    currentBar = new NVTBar();
                    currentBar.BarName = this.CurrentBarName;
                    _htBars[this.CurrentBarName] = currentBar;
                }

                return currentBar;
            }
        }

        /// <summary>
        /// 清空Bar条
        /// </summary>
        /// <param name="sBarName">Bar条的名称</param>
        public void ClearBar(string sBarName)
        {
            NVTBar bar = new NVTBar();
            bar.BarName = sBarName;
            _htBars[sBarName] = bar;
        }
    }
}

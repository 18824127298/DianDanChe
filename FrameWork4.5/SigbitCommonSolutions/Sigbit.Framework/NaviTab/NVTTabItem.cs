using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Sigbit.Framework.NaviTab
{
    class NVTTabItem
    {
        private string _tabTitle = "";
        /// <summary>
        /// TAB标题
        /// </summary>
        public string TabTitle
        {
            get { return _tabTitle; }
            set { _tabTitle = value; }
        }

        private string _tabURL = "";
        /// <summary>
        /// TAB链接
        /// </summary>
        public string TabURL
        {
            get { return _tabURL; }
            set
            {
                _tabURL = value;
            }
        }

        private string _parentURL = "";
        /// <summary>
        /// 父级链接
        /// </summary>
        public string ParentURL
        {
            get { return _parentURL; }
            set { _parentURL = value; }
        }

        private bool _canClose = false;
        /// <summary>
        /// 能否被关闭
        /// </summary>
        public bool CanClose
        {
            get { return _canClose; }
            set { _canClose = value; }
        }

        private string _tabCategory = "";
        /// <summary>
        /// TAB类型
        /// </summary>
        public string TabCategory
        {
            get { return _tabCategory; }
            set { _tabCategory = value; }
        }

        /// <summary>
        /// TAB的链接、名称、类型中是否包含设定键值
        /// </summary>
        /// <param name="sKey">设定键值</param>
        /// <returns>包含返回true，否则返回false</returns>
        public bool IsMatchKey(string sKey)
        {
            if (this.TabTitle.IndexOf(sKey) != -1)
                return true;

            if (this.TabURL.IndexOf(sKey) != -1)
                return true;

            if (this.TabCategory == sKey)
                return true;

            return false;
        }
    }
}

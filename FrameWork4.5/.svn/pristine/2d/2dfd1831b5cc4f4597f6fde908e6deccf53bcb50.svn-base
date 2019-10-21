using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

using Sigbit.Common;

namespace Sigbit.Framework
{
    /// <summary>
    /// 页面的数据视图状态
    /// </summary>
    public class SbtPageDataViewStatus
    {
        private int _currentPageIndex;
        /// <summary>
        /// 当前的页号
        /// </summary>
        public int CurrentPageIndex
        {
            get { return _currentPageIndex; }
            set { _currentPageIndex = value; }
        }

        private int _pageSize;
        /// <summary>
        /// 页面的大小
        /// </summary>
        public int PageSize
        {
            get 
            {
                if (_pageSize == 0)
                    return 10;
                else
                    return _pageSize; 
            }
            set { _pageSize = value; }
        }

        private SQLBuilder _sqlBuilder = null;
        /// <summary>
        /// SQL创建器
        /// </summary>
        public SQLBuilder SqlBuilder
        {
            get 
            {
                if (_sqlBuilder == null)
                    _sqlBuilder = new SQLBuilder();
                return _sqlBuilder; 
            }
        }
    }

    /// <summary>
    /// 页面的状态
    /// </summary>
    public class SbtPageStatus
    {
        public string[] StringStatus = new string[32];
        public int[] IntStatus = new int[32];
        public object[] ObjStatus = new object[8];

        private SbtPageDataViewStatus _dataViewStatus = null;
        /// <summary>
        /// 页面的数据视图状态
        /// </summary>
        public SbtPageDataViewStatus DataViewStatus
        {
            get 
            {
                if (_dataViewStatus == null)
                    _dataViewStatus = new SbtPageDataViewStatus();
                return _dataViewStatus; 
            }
        }
    }

    /// <summary>
    /// 页面状态的列表
    /// </summary>
    /// <remarks>
    /// 可以同时记录多个页面的状态。
    /// 不能无限制的进行记录。以后可以扩充容量限制，并按时间先后进行清除。
    /// </remarks>
    class SbtPageStatusList
    {
        Hashtable _htPageStatus = new Hashtable();

        public SbtPageStatus GetPageStatus(string sKey)
        {
            SbtPageStatus pageStatus = (SbtPageStatus)_htPageStatus[sKey];
            if (pageStatus == null)
            {
                pageStatus = new SbtPageStatus();
                _htPageStatus[sKey] = pageStatus;
            }

            return pageStatus;
        }
    }
}

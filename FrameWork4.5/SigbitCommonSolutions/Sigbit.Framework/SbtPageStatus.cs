using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

using Sigbit.Common;

namespace Sigbit.Framework
{
    /// <summary>
    /// ҳ���������ͼ״̬
    /// </summary>
    public class SbtPageDataViewStatus
    {
        private int _currentPageIndex;
        /// <summary>
        /// ��ǰ��ҳ��
        /// </summary>
        public int CurrentPageIndex
        {
            get { return _currentPageIndex; }
            set { _currentPageIndex = value; }
        }

        private int _pageSize;
        /// <summary>
        /// ҳ��Ĵ�С
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
        /// SQL������
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
    /// ҳ���״̬
    /// </summary>
    public class SbtPageStatus
    {
        public string[] StringStatus = new string[32];
        public int[] IntStatus = new int[32];
        public object[] ObjStatus = new object[8];

        private SbtPageDataViewStatus _dataViewStatus = null;
        /// <summary>
        /// ҳ���������ͼ״̬
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
    /// ҳ��״̬���б�
    /// </summary>
    /// <remarks>
    /// ����ͬʱ��¼���ҳ���״̬��
    /// ���������ƵĽ��м�¼���Ժ���������������ƣ�����ʱ���Ⱥ���������
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

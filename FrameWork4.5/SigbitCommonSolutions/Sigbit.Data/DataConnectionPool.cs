using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Collections;

namespace Sigbit.Data
{
    /// <summary>
    /// 连接的定位方式
    /// </summary>
    enum ConnectionLocateMethod
    {
        /// <summary>
        /// 固定段定位
        /// </summary>
        Fix,
        /// <summary>
        /// 可变段定位
        /// </summary>
        Variable
    }

    /// <summary>
    /// 数据连接段
    /// </summary>
    class DataConnectionSetion
    {
        ArrayList _arrDataHelperList = null;

        private int _sectionSize = 0;
        /// <summary>
        /// 池段的大小
        /// </summary>
        public int SectionSize
        {
            get { return _sectionSize; }
        }

        private string _dataInstanceName = "";
        /// <summary>
        /// 数据连接实例名
        /// </summary>
        public string DataInstanceName
        {
            get { return _dataInstanceName; }
        }

        private ConnectionLocateMethod _connectionLocateMeth;
        /// <summary>
        /// 空闲连接的定位方式
        /// </summary>
        public ConnectionLocateMethod ConnectionLocateMeth
        {
            get { return _connectionLocateMeth; }
        }

        int _nLastIdleIndex = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nSize">段大小</param>
        /// <param name="sDataInstanceName">数据实例名</param>
        /// <param name="locateMeth">空闲连接的定位方式</param>
        public DataConnectionSetion(int nSize, string sDataInstanceName, 
                ConnectionLocateMethod locateMeth)
        {
            _dataInstanceName = sDataInstanceName;
            _connectionLocateMeth = locateMeth;
            _arrDataHelperList = new ArrayList();
            _sectionSize = nSize;

            for (int i = 0; i < nSize; i++)
            {
                DataHelper dataHelper = new DataHelper();
                dataHelper.DataInstanceName = sDataInstanceName;

                _arrDataHelperList.Add(dataHelper);
            }
        }

        /// <summary>
        /// 按索引定位连接
        /// </summary>
        /// <param name="nIndex">下标索引</param>
        /// <returns>连接</returns>
        private DataHelper GetDataHelper(int nIndex)
        {
            if (nIndex >= _arrDataHelperList.Count)
                return null;

            return (DataHelper)_arrDataHelperList[nIndex];
        }

        /// <summary>
        /// 得到一个空闲的连接实例
        /// </summary>
        /// <returns>连接实例</returns>
        public DataHelper GetIdleConnection()
        {
            if (_connectionLocateMeth == ConnectionLocateMethod.Fix)
                return GetIdleConnection__Fix();
            else
                return GetIdleConnection__Variable();
        }

        /// <summary>
        /// 得到固定段的空闲连接实例
        /// </summary>
        /// <returns>连接实例</returns>
        private DataHelper GetIdleConnection__Fix()
        {
            if (_sectionSize == 1)
            {
                DataHelper dh = GetDataHelper(0);
                return dh;
            }

            //=========== 1. 从上一次空闲处开始找连接 =============
            for (int i = _nLastIdleIndex + 1; i < _arrDataHelperList.Count; i++)
            {
                DataHelper dh = GetDataHelper(i);
                if (!dh.IsRunningSQL)
                {
                    _nLastIdleIndex = i;
                    return dh;
                }
            }

            for (int i = 0; i <= _nLastIdleIndex; i++)
            {
                DataHelper dh = GetDataHelper(i);
                if (!dh.IsRunningSQL)
                {
                    _nLastIdleIndex = i;
                    return dh;
                }
            }

            //======== 2. 如果都没有找到连接，则找到下一个连接即可 ===========
            _nLastIdleIndex++;
            if (_nLastIdleIndex >= _sectionSize)
                _nLastIdleIndex = 0;

            DataHelper dhRet = GetDataHelper(_nLastIdleIndex);
            return dhRet;
        }

        /// <summary>
        /// 得到可变段的空闲连接实例
        /// </summary>
        /// <returns>连接实例</returns>
        private DataHelper GetIdleConnection__Variable()
        {
            if (_sectionSize == 0)
                return null;

            //============ 1. 先找一个已连接上，但又空闲的连接 ==========
            for (int i = _nLastIdleIndex + 1; i < _arrDataHelperList.Count; i++)
            {
                DataHelper dh = GetDataHelper(i);
                if (dh.IsConnected && !dh.IsRunningSQL)
                {
                    _nLastIdleIndex = i;
                    return dh;
                }
            }

            for (int i = 0; i <= _nLastIdleIndex; i++)
            {
                DataHelper dh = GetDataHelper(i);
                if (dh.IsConnected && !dh.IsRunningSQL)
                {
                    _nLastIdleIndex = i;
                    return dh;
                }
            }

            //=========== 2. 再找空闲的连接（没连接上即可） ===================
            for (int i = _nLastIdleIndex + 1; i < _arrDataHelperList.Count; i++)
            {
                DataHelper dh = GetDataHelper(i);
                if (!dh.IsRunningSQL)
                {
                    _nLastIdleIndex = i;
                    return dh;
                }
            }

            for (int i = 0; i <= _nLastIdleIndex; i++)
            {
                DataHelper dh = GetDataHelper(i);
                if (!dh.IsRunningSQL)
                {
                    _nLastIdleIndex = i;
                    return dh;
                }
            }

            return null;
        }

        /// <summary>
        /// 清除30秒未运行SQL语句的连接
        /// </summary>
        public void ClearConnections()
        {
            for (int i = 0; i < _arrDataHelperList.Count; i++)
            {
                DataHelper dh = GetDataHelper(i);
                if (dh.IsConnected && !dh.IsRunningSQL)
                {
                    TimeSpan timeSpan = DateTime.Now - dh.LastRunSQLTime;
                    if (timeSpan.TotalSeconds > 30)
                        dh.Disconnect();
                }
            }
        }

        public override string ToString()
        {
            string sRet = "";
            for (int i = 0; i < _arrDataHelperList.Count; i++)
            {
                DataHelper dh = GetDataHelper(i);

                if (dh.IsConnected)
                {
                    if (dh.IsRunningSQL)
                        sRet += "R";
                    else
                        sRet += "I";
                }
                else
                {
                    sRet += "-";
                }
            }

            return sRet;
        }
    }

    class DataConnectionPool
    {
        private int _minPoolSize = 1;
        /// <summary>
        /// 最小的池大小
        /// </summary>
        public int MinPoolSize
        {
            get { return _minPoolSize; }
        }

        private int _maxPoolSize = 1;
        /// <summary>
        /// 最大的池大小
        /// </summary>
        public int MaxPoolSize
        {
            get { return _maxPoolSize; }
        }

        private string _dataInstanceName = "";
        /// <summary>
        /// 数据连接实例名
        /// </summary>
        public string DataInstanceName
        {
            get { return _dataInstanceName; }
        }

        /// <summary>
        /// 固定的连接段
        /// </summary>
        DataConnectionSetion _sectionFix = null;

        /// <summary>
        /// 可变的连接段
        /// </summary>
        DataConnectionSetion _sectionVariable = null;

        /// <summary>
        /// 最后一次清连接的时间
        /// </summary>
        DateTime _lastClearConnectionTime = DateTime.Now;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nMinSize">最小保证大小</param>
        /// <param name="nMaxSize">最大可达大小</param>
        public DataConnectionPool(int nMinSize, int nMaxSize, string sDataInstanceName)
        {
            Debug.Assert(nMinSize >= 1);
            Debug.Assert(nMaxSize >= nMinSize);

            _minPoolSize = nMinSize;
            _maxPoolSize = nMaxSize;
            _dataInstanceName = sDataInstanceName;

            _sectionFix = new DataConnectionSetion(_minPoolSize, sDataInstanceName, 
                    ConnectionLocateMethod.Fix);
            _sectionVariable = new DataConnectionSetion(_maxPoolSize - _minPoolSize, 
                    sDataInstanceName, ConnectionLocateMethod.Variable);
        }

        /// <summary>
        /// 得到空闲的连接
        /// </summary>
        /// <returns>空闲的连接</returns>
        public DataHelper GetIdleConnection()
        {
            //========= 1. 每隔30秒清一次连接 ===========
            TimeSpan timeSpan = DateTime.Now - _lastClearConnectionTime;
            if (timeSpan.TotalSeconds > 30)
            {
                _lastClearConnectionTime = DateTime.Now;
                ClearVariableConnections();
            }

            //========= 2. 得到空闲连接 ============
            DataHelper dhFix = _sectionFix.GetIdleConnection();
            if (!dhFix.IsRunningSQL)
                return dhFix;

            DataHelper dhVariable = _sectionVariable.GetIdleConnection();
            if (dhVariable == null)
                return dhFix;
            else
                return dhVariable;
        }

        /// <summary>
        /// 清除可变段的连接
        /// </summary>
        private void ClearVariableConnections()
        {
            _sectionVariable.ClearConnections();
        }

        public override string ToString()
        {
            string sRet = _sectionFix.ToString() + " " + _sectionVariable.ToString();
            return sRet;
        }
    }
}

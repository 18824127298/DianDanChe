using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Collections;

namespace Sigbit.Data
{
    /// <summary>
    /// ���ӵĶ�λ��ʽ
    /// </summary>
    enum ConnectionLocateMethod
    {
        /// <summary>
        /// �̶��ζ�λ
        /// </summary>
        Fix,
        /// <summary>
        /// �ɱ�ζ�λ
        /// </summary>
        Variable
    }

    /// <summary>
    /// �������Ӷ�
    /// </summary>
    class DataConnectionSetion
    {
        ArrayList _arrDataHelperList = null;

        private int _sectionSize = 0;
        /// <summary>
        /// �ضεĴ�С
        /// </summary>
        public int SectionSize
        {
            get { return _sectionSize; }
        }

        private string _dataInstanceName = "";
        /// <summary>
        /// ��������ʵ����
        /// </summary>
        public string DataInstanceName
        {
            get { return _dataInstanceName; }
        }

        private ConnectionLocateMethod _connectionLocateMeth;
        /// <summary>
        /// �������ӵĶ�λ��ʽ
        /// </summary>
        public ConnectionLocateMethod ConnectionLocateMeth
        {
            get { return _connectionLocateMeth; }
        }

        int _nLastIdleIndex = 0;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="nSize">�δ�С</param>
        /// <param name="sDataInstanceName">����ʵ����</param>
        /// <param name="locateMeth">�������ӵĶ�λ��ʽ</param>
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
        /// ��������λ����
        /// </summary>
        /// <param name="nIndex">�±�����</param>
        /// <returns>����</returns>
        private DataHelper GetDataHelper(int nIndex)
        {
            if (nIndex >= _arrDataHelperList.Count)
                return null;

            return (DataHelper)_arrDataHelperList[nIndex];
        }

        /// <summary>
        /// �õ�һ�����е�����ʵ��
        /// </summary>
        /// <returns>����ʵ��</returns>
        public DataHelper GetIdleConnection()
        {
            if (_connectionLocateMeth == ConnectionLocateMethod.Fix)
                return GetIdleConnection__Fix();
            else
                return GetIdleConnection__Variable();
        }

        /// <summary>
        /// �õ��̶��εĿ�������ʵ��
        /// </summary>
        /// <returns>����ʵ��</returns>
        private DataHelper GetIdleConnection__Fix()
        {
            if (_sectionSize == 1)
            {
                DataHelper dh = GetDataHelper(0);
                return dh;
            }

            //=========== 1. ����һ�ο��д���ʼ������ =============
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

            //======== 2. �����û���ҵ����ӣ����ҵ���һ�����Ӽ��� ===========
            _nLastIdleIndex++;
            if (_nLastIdleIndex >= _sectionSize)
                _nLastIdleIndex = 0;

            DataHelper dhRet = GetDataHelper(_nLastIdleIndex);
            return dhRet;
        }

        /// <summary>
        /// �õ��ɱ�εĿ�������ʵ��
        /// </summary>
        /// <returns>����ʵ��</returns>
        private DataHelper GetIdleConnection__Variable()
        {
            if (_sectionSize == 0)
                return null;

            //============ 1. ����һ���������ϣ����ֿ��е����� ==========
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

            //=========== 2. ���ҿ��е����ӣ�û�����ϼ��ɣ� ===================
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
        /// ���30��δ����SQL��������
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
        /// ��С�ĳش�С
        /// </summary>
        public int MinPoolSize
        {
            get { return _minPoolSize; }
        }

        private int _maxPoolSize = 1;
        /// <summary>
        /// ���ĳش�С
        /// </summary>
        public int MaxPoolSize
        {
            get { return _maxPoolSize; }
        }

        private string _dataInstanceName = "";
        /// <summary>
        /// ��������ʵ����
        /// </summary>
        public string DataInstanceName
        {
            get { return _dataInstanceName; }
        }

        /// <summary>
        /// �̶������Ӷ�
        /// </summary>
        DataConnectionSetion _sectionFix = null;

        /// <summary>
        /// �ɱ�����Ӷ�
        /// </summary>
        DataConnectionSetion _sectionVariable = null;

        /// <summary>
        /// ���һ�������ӵ�ʱ��
        /// </summary>
        DateTime _lastClearConnectionTime = DateTime.Now;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="nMinSize">��С��֤��С</param>
        /// <param name="nMaxSize">���ɴ��С</param>
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
        /// �õ����е�����
        /// </summary>
        /// <returns>���е�����</returns>
        public DataHelper GetIdleConnection()
        {
            //========= 1. ÿ��30����һ������ ===========
            TimeSpan timeSpan = DateTime.Now - _lastClearConnectionTime;
            if (timeSpan.TotalSeconds > 30)
            {
                _lastClearConnectionTime = DateTime.Now;
                ClearVariableConnections();
            }

            //========= 2. �õ��������� ============
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
        /// ����ɱ�ε�����
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

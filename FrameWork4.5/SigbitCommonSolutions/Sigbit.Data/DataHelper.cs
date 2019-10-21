using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

using System.Diagnostics;
using System.IO;

using Sigbit.Common;
using Sigbit.Data.RomitSQL;

namespace Sigbit.Data
{
    /// <summary>
    /// SQL��������ʱ������
    /// </summary>
    public enum SQLMaxTimeCostType
    {
        QuickSQL,
        NormalSQL,
        SlowSQL
    }

    /// <summary>
    /// ���ݿ������
    /// </summary>
    /// <remarks>
    /// ======== ����������� (oldix) ========<br/>
    /// Ŀǰ�����ݿ�����÷��ھ�̬�����н���������WebӦ�ã����Է���Application����
    /// �У���Cache֮�С��������һ����װ�м���������������͵�Ӧ�ã�<br/>
    /// ======== �����۵����� (oldix) ========<br/>
    /// һ�������ļ���μ�¼���ļ����ݺ͸�ʽ�����֯����δ�ţ����������Ŀǰ�Ƿ�
    /// �ڡ�config/��Ŀ¼�£��ԡ����dll�ļ���+.config��ɡ���<br/>
    /// ����DataHelper�в���ConnectString�������е����Ӳ��������ִ���ʽ����Ŀǰ��Ӧ
    /// �ò�ࡣ���е�Ӧ����Ҫ��Ӧ����ָ���û������������Ϣ����ô����ʱ��ι���
    /// ������Ϣ������η�װ����ε��ã�<br/>
    /// ======== ��һ����ȷ�Ľ������ - MySQL (oldix) ��========<br/>
    /// 1. MySQL�Ĵ����У�ԭ����mySQLUtils��һ�����벻ͨ������Ϊ�������⣬ע�͵��ˣ�<br/>
    /// 2. ExecuteScalar���к���Զ��ص����ӡ��ڷ�װʱ���������к��ִ򿪵ķ�����<br/>
    /// ======== ��չ�� (oldix) :========<br/>
    /// 1. �洢���̡���̬�󶨲����Ĳ�����<br/>
    /// 2. ��������صķ�װ��<br/>
    /// 3. Oracle�ķ�װ��<br/>
    /// </remarks>
    public class DataHelper
    {
        #region ����
        /// <summary>
        /// ���ݵ�ʵ������
        /// </summary>
        private string _dataInstanceName;

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        private DBConnBase _dbConn = null;

        private bool _isRunningSQL = false;
        /// <summary>
        /// �Ƿ���������SQL���
        /// </summary>
        public bool IsRunningSQL
        {
            get { return _isRunningSQL; }
        }

        private DateTime _lastRunSQLTime = DateTime.Now;
        /// <summary>
        /// ���һ������SQL����ʱ��
        /// </summary>
        public DateTime LastRunSQLTime
        {
            get { return _lastRunSQLTime; }
            set { _lastRunSQLTime = value; }
        }

        public bool IsConnected
        {
            get
            {
                if (_dbConn == null)
                    return false;

                return _dbConn.IsConnected();
            }
        }

        private SQLMaxTimeCostType _nextSQLMaxTimeCostType = SQLMaxTimeCostType.QuickSQL;
        /// <summary>
        /// ��һ��SQL��������ʱ������
        /// </summary>
        public SQLMaxTimeCostType NextSQLMaxTimeCostType
        {
            get { return _nextSQLMaxTimeCostType; }
            set { _nextSQLMaxTimeCostType = value; }
        }

        private int _nextSQLMaxTimeCost = 0;
        /// <summary>
        /// ��һ��SQL��������ʱ���趨����λ�����룩����������˸����ԣ�
        /// �����ʱ�������趨��
        /// </summary>
        public int NextSQLMaxTimeCost
        {
            get { return _nextSQLMaxTimeCost; }
            set { _nextSQLMaxTimeCost = value; }
        }
        #endregion ����

        #region ����/��������
        /// <summary>
        /// ���캯��
        /// </summary>
        public DataHelper()
        {
            _dataInstanceName = DEFAULT_INSTANCE_NAME;
            DataConnection.Init();
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="sDataInstanceName">���õ�ʵ����</param>
        public DataHelper(string sDataInstanceName)
        {
            _dataInstanceName = sDataInstanceName;
            DataConnection.Init();
        }

        /// <summary>
        /// �ͷź���
        /// </summary>
        public void Dispose()
        {
            if (_dbConn != null)
            {
                if (_dbConn.IsConnected())
                    _dbConn.Disconnect();
                _dbConn = null;
            }
        }
        #endregion

        #region ���ݷ���
        /// <summary>
        /// �Ͽ�����
        /// </summary>
        public void Disconnect()
        {
            if (_dbConn == null)
                return;

            if (_dbConn.IsConnected())
                _dbConn.Disconnect();
        }

        /// <summary>
        /// �õ����ݿ�����
        /// </summary>
        /// <returns>���ݿ����ӻ���ʵ��</returns>
        /// <remarks>�������δ���ϣ������ϸ����ӡ�</remarks>
        private DBConnBase GetDatabase()
        {
            lock (this)
            {
                _lastRunSQLTime = DateTime.Now;

                if (_dbConn == null)
                {
                    //========= 1. ��ȡ������Ϣ ==========
                    SigbitDBType dbType = DataHelperConfig.Instance.GetDBType(_dataInstanceName);
                    string sConnectString = DataHelperConfig.Instance.GetConnectString
                            (_dataInstanceName);

                    //========== 2. ��������ʵ�� ==========
                    switch (dbType)
                    {
                        case SigbitDBType.dbMySql:
                            _dbConn = new DBConnMySql();
                            break;
                        case SigbitDBType.dbMySql5:
                            _dbConn = new DBConnMySql5();
                            break;
                        case SigbitDBType.dbMySql5GBK:
                            _dbConn = new DBConnMySql5GBK();
                            break;
                        case SigbitDBType.dbMsSql:
                            _dbConn = new DBConnMSSql();
                            break;
                        case SigbitDBType.dbODBC:
                            _dbConn = new DBConnODBC();
                            break;
                        case SigbitDBType.dbOracle:
                            _dbConn = new DBConnOracle();
                            break;
                        case SigbitDBType.dbSybase:
                            _dbConn = new DBConnSybase();
                            break;
                        case SigbitDBType.dbOleDb:
                            _dbConn = new DBConnOleDb();
                            break;
                        case SigbitDBType.dbPostgreSql:
                            _dbConn = new DBConnPostgreSql();
                            break;
                        case SigbitDBType.dbRomitSql:
                            _dbConn = new DBConnRomitSql();
                            break;
                        default:
                            Debug.Assert(dbType == SigbitDBType.dbMySql);
                            _dbConn = new DBConnMySql();
                            break;
                    }

                    //========== 3. ��ֵ���Ӵ������������ݿ� ========
                    _dbConn.ConnectString = sConnectString;
                    _dbConn.Connect();
                }

                if (!_dbConn.IsConnected())
                    _dbConn.Connect();

                return _dbConn;
            }
        }

        public SigbitDBType GetDBType()
        {
            SigbitDBType dbType = DataHelperConfig.Instance.GetDBType(_dataInstanceName);
            return dbType;
        }

        /// <summary>
        /// ִ�����ݸ������
        /// </summary>
        /// <param name="sSQL">SQL���</param>
        /// <returns>Ӱ�쵽������</returns>
        public int ExecuteNonQuery(string sSQL)
        {
            int nRet;

            try
            {
                nRet = ExecuteNonQuery__Once(sSQL);
                return nRet;
            }
            catch
            {
                SQLErrorLogWrite(sSQL, "������һ��(ExecuteNonQuery)��");
            }

            nRet = ExecuteNonQuery__Once(sSQL);
            return nRet;
        }

        private int ExecuteNonQuery__Once(string sSQL)
        {
            //========== 1. ������ʱ ==============
            Stopwatch watch = null;
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                watch = new Stopwatch();
                watch.Start();
            }

            //========= 2. ������� =============
            _isRunningSQL = true;

            int nRet;
            try
            {
                DBConnBase dbConn = GetDatabase();
                nRet = dbConn.ExecuteNonQuery(sSQL);
                _isRunningSQL = false;
            }
            catch (Exception e)
            {

                ErrorProcess(sSQL, e);

                _isRunningSQL = false;
                SQLErrorLogWrite(sSQL, e.Message);
                throw e;
            }

            //======== 3. ֹͣ��ʱ������¼��־ ===========
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                watch.Stop();
                int nEllapsedMS = (int)watch.ElapsedMilliseconds;

                SQLPerfLogWrite(sSQL, nEllapsedMS);
            }

            return nRet;
        }

        /// <summary>
        /// ����SQL��䣬���õ������
        /// </summary>
        /// <param name="sSQL">SQL���</param>
        /// <returns>��õĽ����</returns>
        public DataSet ExecuteDataSet(string sSQL)
        {
            DataSet dsResult;

            try
            {
                dsResult = ExecuteDataSet__Once(sSQL);
                return dsResult;
            }
            catch
            {
                SQLErrorLogWrite(sSQL, "������һ��(ExecuteDataSet)��");
            }

            dsResult = ExecuteDataSet__Once(sSQL);
            return dsResult;
        }

        private DataSet ExecuteDataSet__Once(string sSQL)
        {
            //========== 1. ������ʱ ==============
            Stopwatch watch = null;
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                watch = new Stopwatch();
                watch.Start();
            }

            //========= 2. ������� =============
            _isRunningSQL = true;

            DataSet ds;
            try
            {
                DBConnBase dbConn = GetDatabase();
                ds = dbConn.ExecuteDataSet(sSQL);
                _isRunningSQL = false;


            }
            catch (Exception e)
            {
                ErrorProcess(sSQL, e);

                _isRunningSQL = false;
                SQLErrorLogWrite(sSQL, e.Message);
                throw e;
            }

            //======== 3. ֹͣ��ʱ������¼��־ ===========
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                watch.Stop();
                int nEllapsedMS = (int)watch.ElapsedMilliseconds;

                SQLPerfLogWrite(sSQL, nEllapsedMS);
            }

            return ds;
        }

        /// <summary>
        /// ����SQL��䣬���õ�DataReader
        /// </summary>
        /// <param name="sSQL">SQL���</param>
        /// <returns>��õ�DataReader</returns>
        public IDataReader ExecuteDataReader(string sSQL)
        {
            IDataReader drResult;

            try
            {
                drResult = ExecuteDataReader__Once(sSQL);
                return drResult;
            }
            catch
            {
                SQLErrorLogWrite(sSQL, "������һ��(ExecuteDataReader)��");
            }

            drResult = ExecuteDataReader__Once(sSQL);
            return drResult;
        }

        private IDataReader ExecuteDataReader__Once(string sSQL)
        {
            //========== 1. ������ʱ ==============
            Stopwatch watch = null;
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                watch = new Stopwatch();
                watch.Start();
            }

            //========= 2. ������� =============
            _isRunningSQL = true;

            IDataReader dataReader;
            try
            {
                DBConnBase dbConn = GetDatabase();
                dataReader = dbConn.ExecuteDataReader(sSQL);
                _isRunningSQL = false;
            }
            catch (Exception e)
            {

                ErrorProcess(sSQL, e);

                _isRunningSQL = false;
                SQLErrorLogWrite(sSQL, e.Message);
                throw e;
            }

            //======== 3. ֹͣ��ʱ������¼��־ ===========
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                watch.Stop();
                int nEllapsedMS = (int)watch.ElapsedMilliseconds;

                SQLPerfLogWrite(sSQL, nEllapsedMS);
            }

            return dataReader;
        }

        /// <summary>
        /// ����SQL��䣬�õ����صĽ��
        /// </summary>
        /// <param name="sSQL">SQL���</param>
        /// <returns>���صĽ�����ý��Ϊ��һ�е�һ�е�ֵ</returns>
        public Object ExecuteScalar(string sSQL)
        {
            Object oRet;

            try
            {
                oRet = ExecuteScalar__Once(sSQL);
                return oRet;
            }
            catch
            {
                SQLErrorLogWrite(sSQL, "������һ��(ExecuteScalar)��");
            }

            oRet = ExecuteScalar__Once(sSQL);
            return oRet;
        }

        private Object ExecuteScalar__Once(string sSQL)
        {
            //========== 1. ������ʱ ==============
            Stopwatch watch = null;
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                watch = new Stopwatch();
                watch.Start();
            }

            //========= 2. ������� =============
            _isRunningSQL = true;

            Object obRet;
            try
            {
                DBConnBase dbConn = GetDatabase();
                obRet = dbConn.ExecuteScalar(sSQL);
                _isRunningSQL = false;
            }
            catch (Exception e)
            {
                ErrorProcess(sSQL, e); ;

                _isRunningSQL = false;
                SQLErrorLogWrite(sSQL, e.Message);
                throw e;
            }

            //======== 3. ֹͣ��ʱ������¼��־ ===========
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                watch.Stop();
                int nEllapsedMS = (int)watch.ElapsedMilliseconds;

                SQLPerfLogWrite(sSQL, nEllapsedMS);
            }

            return obRet;
        }
        #endregion

        #region �����������

        private void ErrorProcess(string sSQL, Exception ex)
        {
            try
            {

                bool bWantDisconnect = false;

                SigbitDBType eDBType = GetDBType();

                switch (eDBType)
                {
                    case SigbitDBType.dbMySql:

                        if (ex.Message.Contains("has gone away"))
                        {
                            bWantDisconnect = true;
                        }
                        else if (ex.Message.Contains("Lost connection"))
                        {
                            bWantDisconnect = true;
                        }
                        break;
                }

                if (bWantDisconnect)
                {
                    DBConnBase dbConn = GetDatabase();

                    dbConn.Disconnect();

                    SQLRunLogWrite(sSQL, "���ݿ�����ɹ���");
                }

            }
            catch (Exception e)
            {
                SQLErrorLogWrite(sSQL, e.Message);
            }

        }

        #endregion

        #region ��̬�������������ݿ���ʶ����(ָ��ʵ��������ȡʵ��)

        /// <summary>
        /// ȱʡ��ʵ����
        /// </summary>
        private const string DEFAULT_INSTANCE_NAME = "instanceDefault";

        /// <summary>
        /// ���ӳ��б�
        /// </summary>
        private static Hashtable _dataHelperPoolList = new Hashtable();

        public static string GetConnectionStatusString()
        {
            string sRet = "";

            foreach (DictionaryEntry entry in _dataHelperPoolList)
            {
                string sInstanceName = (string)entry.Key;
                DataConnectionPool connPool = (DataConnectionPool)entry.Value;

                sRet += sInstanceName + ": " + connPool.ToString() + "\r\n";
            }
            return sRet;
        }

        /// <summary>
        /// �����ݿ���ʶ���� ָ����ǰ����Ĭ��ʵ��(ͨ��Instance)ʱ��ʵ����
        /// </summary>
        public string DataInstanceName
        {
            get
            {
                return _dataInstanceName;
            }
            set
            {
                _dataInstanceName = value;
                if (_dataInstanceName == null || _dataInstanceName == "")
                    _dataInstanceName = DEFAULT_INSTANCE_NAME;
            }
        }

        /// <summary>
        /// �����ݿ���ʶ�����л�ȡĬ�ϵ����ݿ����ʵ��
        /// </summary>
        public static DataHelper Instance
        {
            get
            {
                lock (typeof(DataHelper))
                {
                    DataConnectionPool thisPool
                            = (DataConnectionPool)_dataHelperPoolList[DEFAULT_INSTANCE_NAME];
                    if (thisPool == null)
                    {
                        int nMinPoolSize = DataHelperConfig.Instance.GetMinPoolSize
                                (DEFAULT_INSTANCE_NAME);
                        int nMaxPoolSize = DataHelperConfig.Instance.GetMaxPoolSize
                                (DEFAULT_INSTANCE_NAME);
                        DataConnectionPool connPool = new DataConnectionPool(nMinPoolSize, nMaxPoolSize,
                                DEFAULT_INSTANCE_NAME);
                        _dataHelperPoolList.Add(DEFAULT_INSTANCE_NAME, connPool);
                        DataHelper dataHelper = connPool.GetIdleConnection();
                        return dataHelper;
                    }
                    else
                    {
                        return thisPool.GetIdleConnection();
                    }
                }
            }
        }

        /// <summary>
        /// �����ݿ���ʶ�����л�ȡָ��ʵ���������ݿ����ʵ��
        /// </summary>
        /// <param name="dataInstanceName">ʵ����</param>
        /// <returns>�������ʵ��</returns>
        public static DataHelper GetInstance(string dataInstanceName)
        {
            lock (typeof(DataHelper))
            {
                DataConnectionPool thisPool
                        = (DataConnectionPool)_dataHelperPoolList[dataInstanceName];
                if (thisPool == null)
                {
                    int nMinPoolSize = DataHelperConfig.Instance.GetMinPoolSize
                            (dataInstanceName);
                    int nMaxPoolSize = DataHelperConfig.Instance.GetMaxPoolSize
                            (dataInstanceName);
                    DataConnectionPool connPool = new DataConnectionPool(nMinPoolSize, nMaxPoolSize,
                            dataInstanceName);
                    _dataHelperPoolList.Add(dataInstanceName, connPool);
                    DataHelper dataHelper = connPool.GetIdleConnection();
                    return dataHelper;
                }
                else
                {
                    return thisPool.GetIdleConnection();
                }
            }
        }
        #endregion

        #region ��־��¼

        /// <summary>
        /// ���ݿ�ִ����Ϣд��
        /// </summary>
        /// <param name="sSQL">SQL���</param>
        /// <param name="sRunLog">ִ����Ϣ</param>
        private void SQLRunLogWrite(string sSQL, string sRunLog)
        {
            //========= 1. ���ļ� ==========
            string sLogDirectory = DataHelperConfig.Instance.LogDirectory;
            string sLogFileName = sLogDirectory + "\\sqlperf_"
                    + DateTimeUtil.Now.Substring(0, 10).Replace("-", "") + ".vlog";

            lock (typeof(DataHelper))
            {
                FileStream fs = File.OpenWrite(sLogFileName);
                fs.Seek(0, SeekOrigin.End);

                //====== 2. д�뿪ʼ��ʱ��Ϣ =============
                string sLine;

                sLine = "==== (" + this.DataInstanceName + ")��������Ϣ��";
                sLine += DateTime.Now.ToString() + " ";
                sLine += "====\r\n";

                byte[] bsLog = Encoding.Default.GetBytes(sLine);
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 3. д��SQL��� ==========
                bsLog = Encoding.Default.GetBytes(sSQL + "\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 4. д��ִ����Ϣ ==========
                bsLog = Encoding.Default.GetBytes("-------- ������Ϣ --------\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                bsLog = Encoding.Default.GetBytes(sRunLog + "\r\n\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 5. �ر��ļ� ==========
                fs.Flush();
                fs.Close();
            }
        }


        private void SQLErrorLogWrite(string sSQL, string sErrorMessage)
        {
            //========= 1. ���ļ� ==========
            string sLogDirectory = DataHelperConfig.Instance.LogDirectory;
            string sLogFileName = sLogDirectory + "\\sqlperf_"
                    + DateTimeUtil.Now.Substring(0, 10).Replace("-", "") + ".vlog";

            lock (typeof(DataHelper))
            {
                FileStream fs = File.OpenWrite(sLogFileName);
                fs.Seek(0, SeekOrigin.End);

                //====== 2. д�뿪ʼ��ʱ��Ϣ =============
                string sLine;

                sLine = "==== (" + this.DataInstanceName + ")��SQL������д���";
                sLine += DateTime.Now.ToString() + " ";
                sLine += "====\r\n";

                byte[] bsLog = Encoding.Default.GetBytes(sLine);
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 3. д��SQL��� ==========
                bsLog = Encoding.Default.GetBytes(sSQL + "\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 4. д�������Ϣ ==========
                bsLog = Encoding.Default.GetBytes("-------- ������Ϣ --------\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                bsLog = Encoding.Default.GetBytes(sErrorMessage + "\r\n\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 5. �ر��ļ� ==========
                fs.Flush();
                fs.Close();
            }
        }

        /// <summary>
        /// ��¼SQL���
        /// </summary>
        /// <param name="sSQL">SQL���</param>
        /// <param name="nEllapsedMS">��ʱ</param>
        private void SQLPerfLogWrite(string sSQL, int nEllapsedMS)
        {
            //=========== 0. �ж��Ƿ���Ҫ������־��¼ ==========
            Debug.Assert(DataHelperConfig.Instance.LogMethod == SQLPerfLogMethod.LogWanted
                    || DataHelperConfig.Instance.LogMethod == SQLPerfLogMethod.LogAll);
            int nMaxSQLTimeCost = GetMaxSQLTimeCost();

            bool bSQLOverTime = false;
            if (nEllapsedMS > nMaxSQLTimeCost)
                bSQLOverTime = true;

            if (DataHelperConfig.Instance.LogMethod == SQLPerfLogMethod.LogWanted
                    && !bSQLOverTime)
                return;

            //========= 1. ���ļ� ==========
            string sLogDirectory = DataHelperConfig.Instance.LogDirectory;
            string sLogFileName = sLogDirectory + "\\sqlperf_"
                    + DateTimeUtil.Now.Substring(0, 10).Replace("-", "") + ".vlog";

            lock (typeof(DataHelper))
            {
                FileStream fs = File.OpenWrite(sLogFileName);
                fs.Seek(0, SeekOrigin.End);

                //====== 2. д�뿪ʼ��ʱ��Ϣ =============
                string sLine;

                if (bSQLOverTime)
                {
                    sLine = "==== ��" + _dataInstanceName + "����actual " + nEllapsedMS.ToString() + " ms / want ";
                    sLine += nMaxSQLTimeCost.ToString() + " ms�� ";
                    sLine += DateTime.Now.ToString() + " ";
                    sLine += "====\r\n";
                }
                else
                {
                    sLine = "====[" + _dataInstanceName + "][SQL " + nEllapsedMS.ToString().PadLeft(6, '.') + " ms] ";
                    sLine += DateTime.Now.ToString() + " ";
                    sLine += "====\r\n";
                }
                byte[] bsLog = Encoding.Default.GetBytes(sLine);
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 3. д��SQL��� ==========
                bsLog = Encoding.Default.GetBytes(sSQL + "\r\n\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 5. �ر��ļ� ==========
                fs.Flush();
                fs.Close();
            }
        }

        /// <summary>
        /// �õ�SQL������е����ʱ��
        /// </summary>
        /// <returns>�Ժ���Ϊ��λ��ʱ��ֵ</returns>
        private int GetMaxSQLTimeCost()
        {
            int nRet = -1;
            if (_nextSQLMaxTimeCost > 0)
                nRet = _nextSQLMaxTimeCost;
            else
            {
                switch (_nextSQLMaxTimeCostType)
                {
                    case SQLMaxTimeCostType.QuickSQL:
                        nRet = DataHelperConfig.Instance.LogQuickSQLTime;
                        break;
                    case SQLMaxTimeCostType.NormalSQL:
                        nRet = DataHelperConfig.Instance.LogNormalSQLTime;
                        break;
                    case SQLMaxTimeCostType.SlowSQL:
                        nRet = DataHelperConfig.Instance.LogSlowSQLTime;
                        break;
                    default:
                        throw new Exception("δ֪��_nextSQLMaxTimeCostType");
                }
            }

            _nextSQLMaxTimeCost = 0;
            _nextSQLMaxTimeCostType = SQLMaxTimeCostType.QuickSQL;
            return nRet;
        }
        #endregion ��־��¼
    }
}

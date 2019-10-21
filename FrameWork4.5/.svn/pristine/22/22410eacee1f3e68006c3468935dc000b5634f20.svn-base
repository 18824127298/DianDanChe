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
    /// SQL语句的运行时长类型
    /// </summary>
    public enum SQLMaxTimeCostType
    {
        QuickSQL,
        NormalSQL,
        SlowSQL
    }

    /// <summary>
    /// 数据库访问类
    /// </summary>
    /// <remarks>
    /// ======== 待解决的问题 (oldix) ========<br/>
    /// 目前的数据库的配置放在静态变量中解决，如果是Web应用，可以放在Application变量
    /// 中，或Cache之中。但如果在一个封装中兼容两个或更多类型的应用？<br/>
    /// ======== 待讨论的问题 (oldix) ========<br/>
    /// 一、配置文件如何记录？文件内容和格式如何组织，如何存放，如何命名？目前是放
    /// 在“config/”目录下，以“组件dll文件名+.config组成”。<br/>
    /// 二、DataHelper中采用ConnectString放置所有的连接参数，这种处理方式对于目前的应
    /// 用差不多。但有的应用需要在应用中指定用户名、密码等信息，那么，此时如何管理
    /// 配置信息，类如何封装，如何调用？<br/>
    /// ======== 不一定正确的解决方法 - MySQL (oldix) ：========<br/>
    /// 1. MySQL的处理中，原来的mySQLUtils有一处编译不通过，似为编码问题，注释掉了；<br/>
    /// 2. ExecuteScalar运行后会自动关掉连接。在封装时，采用运行后又打开的方法；<br/>
    /// ======== 扩展点 (oldix) :========<br/>
    /// 1. 存储过程、动态绑定参数的操作；<br/>
    /// 2. 事务处理相关的封装；<br/>
    /// 3. Oracle的封装；<br/>
    /// </remarks>
    public class DataHelper
    {
        #region 属性
        /// <summary>
        /// 数据的实例名称
        /// </summary>
        private string _dataInstanceName;

        /// <summary>
        /// 数据库连接
        /// </summary>
        private DBConnBase _dbConn = null;

        private bool _isRunningSQL = false;
        /// <summary>
        /// 是否正在运行SQL语句
        /// </summary>
        public bool IsRunningSQL
        {
            get { return _isRunningSQL; }
        }

        private DateTime _lastRunSQLTime = DateTime.Now;
        /// <summary>
        /// 最近一次运行SQL语句的时间
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
        /// 下一条SQL语句的运行时长类型
        /// </summary>
        public SQLMaxTimeCostType NextSQLMaxTimeCostType
        {
            get { return _nextSQLMaxTimeCostType; }
            set { _nextSQLMaxTimeCostType = value; }
        }

        private int _nextSQLMaxTimeCost = 0;
        /// <summary>
        /// 下一条SQL语句的运行时长设定（单位：毫秒）。如果设置了该属性，
        /// 则忽略时长类型设定。
        /// </summary>
        public int NextSQLMaxTimeCost
        {
            get { return _nextSQLMaxTimeCost; }
            set { _nextSQLMaxTimeCost = value; }
        }
        #endregion 属性

        #region 构造/析构函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataHelper()
        {
            _dataInstanceName = DEFAULT_INSTANCE_NAME;
            DataConnection.Init();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sDataInstanceName">配置的实例名</param>
        public DataHelper(string sDataInstanceName)
        {
            _dataInstanceName = sDataInstanceName;
            DataConnection.Init();
        }

        /// <summary>
        /// 释放函数
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

        #region 数据访问
        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            if (_dbConn == null)
                return;

            if (_dbConn.IsConnected())
                _dbConn.Disconnect();
        }

        /// <summary>
        /// 得到数据库连接
        /// </summary>
        /// <returns>数据库连接基类实例</returns>
        /// <remarks>如果连接未连上，则连上该连接。</remarks>
        private DBConnBase GetDatabase()
        {
            lock (this)
            {
                _lastRunSQLTime = DateTime.Now;

                if (_dbConn == null)
                {
                    //========= 1. 获取配置信息 ==========
                    SigbitDBType dbType = DataHelperConfig.Instance.GetDBType(_dataInstanceName);
                    string sConnectString = DataHelperConfig.Instance.GetConnectString
                            (_dataInstanceName);

                    //========== 2. 创建连接实例 ==========
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

                    //========== 3. 赋值连接串，并连到数据库 ========
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
        /// 执行数据更新语句
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <returns>影响到的行数</returns>
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
                SQLErrorLogWrite(sSQL, "【重试一次(ExecuteNonQuery)】");
            }

            nRet = ExecuteNonQuery__Once(sSQL);
            return nRet;
        }

        private int ExecuteNonQuery__Once(string sSQL)
        {
            //========== 1. 启动计时 ==============
            Stopwatch watch = null;
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                watch = new Stopwatch();
                watch.Start();
            }

            //========= 2. 运行语句 =============
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

            //======== 3. 停止计时，并记录日志 ===========
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                watch.Stop();
                int nEllapsedMS = (int)watch.ElapsedMilliseconds;

                SQLPerfLogWrite(sSQL, nEllapsedMS);
            }

            return nRet;
        }

        /// <summary>
        /// 运行SQL语句，并得到结果集
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <returns>获得的结果集</returns>
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
                SQLErrorLogWrite(sSQL, "【重试一次(ExecuteDataSet)】");
            }

            dsResult = ExecuteDataSet__Once(sSQL);
            return dsResult;
        }

        private DataSet ExecuteDataSet__Once(string sSQL)
        {
            //========== 1. 启动计时 ==============
            Stopwatch watch = null;
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                watch = new Stopwatch();
                watch.Start();
            }

            //========= 2. 运行语句 =============
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

            //======== 3. 停止计时，并记录日志 ===========
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                watch.Stop();
                int nEllapsedMS = (int)watch.ElapsedMilliseconds;

                SQLPerfLogWrite(sSQL, nEllapsedMS);
            }

            return ds;
        }

        /// <summary>
        /// 运行SQL语句，并得到DataReader
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <returns>获得的DataReader</returns>
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
                SQLErrorLogWrite(sSQL, "【重试一次(ExecuteDataReader)】");
            }

            drResult = ExecuteDataReader__Once(sSQL);
            return drResult;
        }

        private IDataReader ExecuteDataReader__Once(string sSQL)
        {
            //========== 1. 启动计时 ==============
            Stopwatch watch = null;
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                watch = new Stopwatch();
                watch.Start();
            }

            //========= 2. 运行语句 =============
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

            //======== 3. 停止计时，并记录日志 ===========
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                watch.Stop();
                int nEllapsedMS = (int)watch.ElapsedMilliseconds;

                SQLPerfLogWrite(sSQL, nEllapsedMS);
            }

            return dataReader;
        }

        /// <summary>
        /// 运行SQL语句，得到返回的结果
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <returns>返回的结果。该结果为第一行第一列的值</returns>
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
                SQLErrorLogWrite(sSQL, "【重试一次(ExecuteScalar)】");
            }

            oRet = ExecuteScalar__Once(sSQL);
            return oRet;
        }

        private Object ExecuteScalar__Once(string sSQL)
        {
            //========== 1. 启动计时 ==============
            Stopwatch watch = null;
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                watch = new Stopwatch();
                watch.Start();
            }

            //========= 2. 运行语句 =============
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

            //======== 3. 停止计时，并记录日志 ===========
            if (DataHelperConfig.Instance.LogMethod != SQLPerfLogMethod.LogNone)
            {
                watch.Stop();
                int nEllapsedMS = (int)watch.ElapsedMilliseconds;

                SQLPerfLogWrite(sSQL, nEllapsedMS);
            }

            return obRet;
        }
        #endregion

        #region 错误断连处理

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

                    SQLRunLogWrite(sSQL, "数据库断连成功！");
                }

            }
            catch (Exception e)
            {
                SQLErrorLogWrite(sSQL, e.Message);
            }

        }

        #endregion

        #region 静态方法，建立数据库访问对象池(指定实例名，获取实例)

        /// <summary>
        /// 缺省的实例名
        /// </summary>
        private const string DEFAULT_INSTANCE_NAME = "instanceDefault";

        /// <summary>
        /// 连接池列表
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
        /// 对数据库访问对象池 指定当前调用默认实例(通过Instance)时的实例名
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
        /// 从数据库访问对象池中获取默认的数据库访问实例
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
        /// 从数据库访问对象池中获取指定实例名的数据库访问实例
        /// </summary>
        /// <param name="dataInstanceName">实例名</param>
        /// <returns>访问类的实例</returns>
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

        #region 日志记录

        /// <summary>
        /// 数据库执行信息写入
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <param name="sRunLog">执行信息</param>
        private void SQLRunLogWrite(string sSQL, string sRunLog)
        {
            //========= 1. 打开文件 ==========
            string sLogDirectory = DataHelperConfig.Instance.LogDirectory;
            string sLogFileName = sLogDirectory + "\\sqlperf_"
                    + DateTimeUtil.Now.Substring(0, 10).Replace("-", "") + ".vlog";

            lock (typeof(DataHelper))
            {
                FileStream fs = File.OpenWrite(sLogFileName);
                fs.Seek(0, SeekOrigin.End);

                //====== 2. 写入开始计时信息 =============
                string sLine;

                sLine = "==== (" + this.DataInstanceName + ")【运行信息】";
                sLine += DateTime.Now.ToString() + " ";
                sLine += "====\r\n";

                byte[] bsLog = Encoding.Default.GetBytes(sLine);
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 3. 写入SQL语句 ==========
                bsLog = Encoding.Default.GetBytes(sSQL + "\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 4. 写入执行信息 ==========
                bsLog = Encoding.Default.GetBytes("-------- 运行信息 --------\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                bsLog = Encoding.Default.GetBytes(sRunLog + "\r\n\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 5. 关闭文件 ==========
                fs.Flush();
                fs.Close();
            }
        }


        private void SQLErrorLogWrite(string sSQL, string sErrorMessage)
        {
            //========= 1. 打开文件 ==========
            string sLogDirectory = DataHelperConfig.Instance.LogDirectory;
            string sLogFileName = sLogDirectory + "\\sqlperf_"
                    + DateTimeUtil.Now.Substring(0, 10).Replace("-", "") + ".vlog";

            lock (typeof(DataHelper))
            {
                FileStream fs = File.OpenWrite(sLogFileName);
                fs.Seek(0, SeekOrigin.End);

                //====== 2. 写入开始计时信息 =============
                string sLine;

                sLine = "==== (" + this.DataInstanceName + ")【SQL语句运行错误】";
                sLine += DateTime.Now.ToString() + " ";
                sLine += "====\r\n";

                byte[] bsLog = Encoding.Default.GetBytes(sLine);
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 3. 写入SQL语句 ==========
                bsLog = Encoding.Default.GetBytes(sSQL + "\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 4. 写入错误信息 ==========
                bsLog = Encoding.Default.GetBytes("-------- 错误信息 --------\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                bsLog = Encoding.Default.GetBytes(sErrorMessage + "\r\n\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 5. 关闭文件 ==========
                fs.Flush();
                fs.Close();
            }
        }

        /// <summary>
        /// 记录SQL语句
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <param name="nEllapsedMS">历时</param>
        private void SQLPerfLogWrite(string sSQL, int nEllapsedMS)
        {
            //=========== 0. 判断是否需要进行日志记录 ==========
            Debug.Assert(DataHelperConfig.Instance.LogMethod == SQLPerfLogMethod.LogWanted
                    || DataHelperConfig.Instance.LogMethod == SQLPerfLogMethod.LogAll);
            int nMaxSQLTimeCost = GetMaxSQLTimeCost();

            bool bSQLOverTime = false;
            if (nEllapsedMS > nMaxSQLTimeCost)
                bSQLOverTime = true;

            if (DataHelperConfig.Instance.LogMethod == SQLPerfLogMethod.LogWanted
                    && !bSQLOverTime)
                return;

            //========= 1. 打开文件 ==========
            string sLogDirectory = DataHelperConfig.Instance.LogDirectory;
            string sLogFileName = sLogDirectory + "\\sqlperf_"
                    + DateTimeUtil.Now.Substring(0, 10).Replace("-", "") + ".vlog";

            lock (typeof(DataHelper))
            {
                FileStream fs = File.OpenWrite(sLogFileName);
                fs.Seek(0, SeekOrigin.End);

                //====== 2. 写入开始计时信息 =============
                string sLine;

                if (bSQLOverTime)
                {
                    sLine = "==== 【" + _dataInstanceName + "】【actual " + nEllapsedMS.ToString() + " ms / want ";
                    sLine += nMaxSQLTimeCost.ToString() + " ms】 ";
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

                //========== 3. 写入SQL语句 ==========
                bsLog = Encoding.Default.GetBytes(sSQL + "\r\n\r\n");
                fs.Write(bsLog, 0, bsLog.Length);

                //========== 5. 关闭文件 ==========
                fs.Flush();
                fs.Close();
            }
        }

        /// <summary>
        /// 得到SQL语句运行的最大时间
        /// </summary>
        /// <returns>以毫秒为单位的时间值</returns>
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
                        throw new Exception("未知的_nextSQLMaxTimeCostType");
                }
            }

            _nextSQLMaxTimeCost = 0;
            _nextSQLMaxTimeCostType = SQLMaxTimeCostType.QuickSQL;
            return nRet;
        }
        #endregion 日志记录
    }
}

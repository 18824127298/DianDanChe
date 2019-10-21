using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Sigbit.Common;
using Sigbit.Common.Encrypt;

namespace Sigbit.Data
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum SigbitDBType
    {
        /// <summary>
        /// Microsoft SQLServer
        /// </summary>
        dbMsSql,
        /// <summary>
        /// MySQL
        /// </summary>
        dbMySql,
        /// <summary>
        /// 
        /// </summary>
        dbMySql5,
        /// <summary>
        /// 
        /// </summary>
        dbMySql5GBK,
        /// <summary>
        /// ODBC
        /// </summary>
        dbODBC,
        /// <summary>
        /// Oracle
        /// </summary>
        dbOracle,
        /// <summary>
        /// OleDb
        /// </summary>
        dbOleDb,
        /// <summary>
        /// Sybase
        /// </summary>
        dbSybase,
        /// <summary>
        /// Postgre
        /// </summary>
        dbPostgreSql,
        /// <summary>
        /// 远程数据库
        /// </summary>
        dbRomitSql
    };

    /// <summary>
    /// 数据库日志文件的记录方式
    /// </summary>
    public enum SQLPerfLogMethod
    {
        /// <summary>
        /// 未知记录
        /// </summary>
        UnknownMethod,
        /// <summary>
        /// 不做记录
        /// </summary>
        LogNone,
        /// <summary>
        /// 只记录超时SQL语句
        /// </summary>
        LogWanted,
        /// <summary>
        /// 全记录
        /// </summary>
        LogAll
    }

    /// <summary>
    /// 数据库访问配置类
    /// </summary>
    public class DataHelperConfig : ConfigBase
    {
        private static DataHelperConfig _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static DataHelperConfig Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new DataHelperConfig();

                return _thisInstance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataHelperConfig()
        {
            string sConfigFileName;
            sConfigFileName = AppPath.AppFullPath("config", "sigbit.data.dll.config");

            LoadFromFile(sConfigFileName);
        }

        /// <summary>
        /// 得到数据库类型
        /// </summary>
        /// <param name="sInstanceName">配置的实例名</param>
        /// <returns>配置的数据库类型</returns>
        public SigbitDBType GetDBType(string sInstanceName)
        {
            string sDBType;
            sDBType = GetString(sInstanceName, "dbType");
            switch (sDBType)
            {
                case "mssql":
                    return SigbitDBType.dbMsSql;
                case "mysql":
                    return SigbitDBType.dbMySql;
                case "mysql5":
                    return SigbitDBType.dbMySql5;
                case "mysql5GBK":
                    return SigbitDBType.dbMySql5GBK;
                case "odbc":
                    return SigbitDBType.dbODBC;
                case "oracle":
                    return SigbitDBType.dbOracle;
                case "oledb":
                    return SigbitDBType.dbOleDb;
                case "sybase":
                    return SigbitDBType.dbSybase;
                case "romitsql":
                    return SigbitDBType.dbRomitSql;
                default:
                    throw new Exception("DataHelperConfig.GetDBType Error : dbType config error - " + sDBType);
            }
        }

        /// <summary>
        /// 得到数据库连接串
        /// </summary>
        /// <param name="sInstanceName">配置的实例名</param>
        /// <returns>配置的数据库类型</returns>
        public string GetConnectString(string sInstanceName)
        {
            string sConnectString = GetString(sInstanceName, "connectString");
            if (sConnectString == "")
            {
                string sEncryptString = GetString(sInstanceName, "enConnectString");
                if (sEncryptString == "")
                    throw new Exception("DataHelperConfig.GetConnectString Error : enConnectString config error.");

                sConnectString = EncryptUtil.DesDecodeString(sEncryptString, "Fa72cizdLf");
            }
            if (sConnectString == "")
                throw new Exception("DataHelperConfig.GetConnectString Error : connectString config error.");

            return sConnectString;
        }

        public int GetMinPoolSize(string sInstanceName)
        {
            int nMinPoolSize = GetInt(sInstanceName, "minPoolSize", 1);
            return nMinPoolSize;
        }

        public int GetMaxPoolSize(string sInstanceName)
        {
            int nMinPoolSize = GetInt(sInstanceName, "maxPoolSize", 1);
            return nMinPoolSize;
        }

        private SQLPerfLogMethod _logMethod = SQLPerfLogMethod.UnknownMethod;
        /// <summary>
        /// 日志的记录方式
        /// </summary>
        public SQLPerfLogMethod LogMethod
        {
            get
            {
                if (_logMethod != SQLPerfLogMethod.UnknownMethod)
                    return _logMethod;

                string sLogMethod = GetString("sqlPerfLogConfig", "logMethod", "logNone");
                switch (sLogMethod)
                {
                    case "logNone":
                        _logMethod = SQLPerfLogMethod.LogNone;
                        break;
                    case "logAll":
                        _logMethod = SQLPerfLogMethod.LogAll;
                        break;
                    case "logWanted":
                        _logMethod = SQLPerfLogMethod.LogWanted;
                        break;
                    default:
                        throw new Exception("数据库日志配置错误，未知的日志记录类型-"
                                + sLogMethod);
                }
                return _logMethod;
            }
        }

        private string _logDirectory = "";
        /// <summary>
        /// 数据库日志的目录
        /// </summary>
        public string LogDirectory
        {
            get
            {
                if (_logDirectory != "")
                    return _logDirectory;

                string sRet = AppPath.AppFullPath("log", "sqlperf");
                Directory.CreateDirectory(sRet);
                _logDirectory = sRet;
                return sRet;
            }
        }

        private int _logQuickSQLTime = -1;
        /// <summary>
        /// 短SQL语句的超时时长，建议为100 ms
        /// </summary>
        public int LogQuickSQLTime
        {
            get
            {
                if (_logQuickSQLTime != -1)
                    return _logQuickSQLTime;

                _logQuickSQLTime = GetInt("sqlPerfLogConfig", "quickSQLTime", -1);
                if (_logQuickSQLTime == -1)
                    throw new Exception("数据库日志配置错误，未配置短SQL语句的超时时长");
                return _logQuickSQLTime;
            }
        }

        private int _logNormalSQLTime = -1;
        /// <summary>
        /// 普通SQL语句的超时时长，建议为1000 ms
        /// </summary>
        public int LogNormalSQLTime
        {
            get
            {
                if (_logNormalSQLTime != -1)
                    return _logNormalSQLTime;

                _logNormalSQLTime = GetInt("sqlPerfLogConfig", "normalSQLTime", -1);
                if (_logNormalSQLTime == -1)
                    throw new Exception("数据库日志配置错误，未配置普通SQL语句的超时时长");
                return _logNormalSQLTime;
            }
        }

        private int _logSlowSQLTime = -1;
        /// <summary>
        /// 长SQL语句的超时时长，建议为3000 ms
        /// </summary>
        public int LogSlowSQLTime
        {
            get
            {
                if (_logSlowSQLTime != -1)
                    return _logSlowSQLTime;

                _logSlowSQLTime = GetInt("sqlPerfLogConfig", "slowSQLTime", -1);
                if (_logSlowSQLTime == -1)
                    throw new Exception("数据库日志配置错误，未配置长SQL语句的超时时长");
                return _logSlowSQLTime;
            }
        }
    }
}

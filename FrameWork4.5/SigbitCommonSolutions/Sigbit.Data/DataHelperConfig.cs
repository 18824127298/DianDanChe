using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Sigbit.Common;
using Sigbit.Common.Encrypt;

namespace Sigbit.Data
{
    /// <summary>
    /// ���ݿ�����
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
        /// Զ�����ݿ�
        /// </summary>
        dbRomitSql
    };

    /// <summary>
    /// ���ݿ���־�ļ��ļ�¼��ʽ
    /// </summary>
    public enum SQLPerfLogMethod
    {
        /// <summary>
        /// δ֪��¼
        /// </summary>
        UnknownMethod,
        /// <summary>
        /// ������¼
        /// </summary>
        LogNone,
        /// <summary>
        /// ֻ��¼��ʱSQL���
        /// </summary>
        LogWanted,
        /// <summary>
        /// ȫ��¼
        /// </summary>
        LogAll
    }

    /// <summary>
    /// ���ݿ����������
    /// </summary>
    public class DataHelperConfig : ConfigBase
    {
        private static DataHelperConfig _thisInstance = null;
        /// <summary>
        /// Ψһʵ��
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
        /// ���캯��
        /// </summary>
        public DataHelperConfig()
        {
            string sConfigFileName;
            sConfigFileName = AppPath.AppFullPath("config", "sigbit.data.dll.config");

            LoadFromFile(sConfigFileName);
        }

        /// <summary>
        /// �õ����ݿ�����
        /// </summary>
        /// <param name="sInstanceName">���õ�ʵ����</param>
        /// <returns>���õ����ݿ�����</returns>
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
        /// �õ����ݿ����Ӵ�
        /// </summary>
        /// <param name="sInstanceName">���õ�ʵ����</param>
        /// <returns>���õ����ݿ�����</returns>
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
        /// ��־�ļ�¼��ʽ
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
                        throw new Exception("���ݿ���־���ô���δ֪����־��¼����-"
                                + sLogMethod);
                }
                return _logMethod;
            }
        }

        private string _logDirectory = "";
        /// <summary>
        /// ���ݿ���־��Ŀ¼
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
        /// ��SQL���ĳ�ʱʱ��������Ϊ100 ms
        /// </summary>
        public int LogQuickSQLTime
        {
            get
            {
                if (_logQuickSQLTime != -1)
                    return _logQuickSQLTime;

                _logQuickSQLTime = GetInt("sqlPerfLogConfig", "quickSQLTime", -1);
                if (_logQuickSQLTime == -1)
                    throw new Exception("���ݿ���־���ô���δ���ö�SQL���ĳ�ʱʱ��");
                return _logQuickSQLTime;
            }
        }

        private int _logNormalSQLTime = -1;
        /// <summary>
        /// ��ͨSQL���ĳ�ʱʱ��������Ϊ1000 ms
        /// </summary>
        public int LogNormalSQLTime
        {
            get
            {
                if (_logNormalSQLTime != -1)
                    return _logNormalSQLTime;

                _logNormalSQLTime = GetInt("sqlPerfLogConfig", "normalSQLTime", -1);
                if (_logNormalSQLTime == -1)
                    throw new Exception("���ݿ���־���ô���δ������ͨSQL���ĳ�ʱʱ��");
                return _logNormalSQLTime;
            }
        }

        private int _logSlowSQLTime = -1;
        /// <summary>
        /// ��SQL���ĳ�ʱʱ��������Ϊ3000 ms
        /// </summary>
        public int LogSlowSQLTime
        {
            get
            {
                if (_logSlowSQLTime != -1)
                    return _logSlowSQLTime;

                _logSlowSQLTime = GetInt("sqlPerfLogConfig", "slowSQLTime", -1);
                if (_logSlowSQLTime == -1)
                    throw new Exception("���ݿ���־���ô���δ���ó�SQL���ĳ�ʱʱ��");
                return _logSlowSQLTime;
            }
        }
    }
}

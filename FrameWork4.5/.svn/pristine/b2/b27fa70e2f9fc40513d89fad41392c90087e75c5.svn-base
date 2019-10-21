using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Net;

using Sigbit.Common;

namespace Sigbit.Data.RomitSQL
{
    public class DBConnRomitSql : DBConnBase
    {

        DBConnRomitSql__Config _romitConfig = null;
        /// <summary>
        /// 配置信息
        /// </summary>
        internal DBConnRomitSql__Config RomitConfig
        {
            get 
            {
                if (_romitConfig == null)
                {
                    _romitConfig = new DBConnRomitSql__Config();
                    _romitConfig.ParseDBConnString(this.ConnectString);
                }
                return _romitConfig; 
            }
        }

        /// <summary>
        /// 判断是否已经连接上
        /// </summary>
        /// <returns>是否已经创建连接</returns>
        /// <remarks>对于RomitSQL并没有连接的概念，一概认为已连接上</remarks>
        override public bool IsConnected()
        {
            return true;
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        override public void Connect()
        {
        }

        /// <summary>
        /// 断连数据库
        /// </summary>
        override public void Disconnect()
        {
        }

        /// <summary>
        /// 运行更新语句
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <returns>影响到的行数</returns>
        override public int ExecuteNonQuery(string sSQL)
        {
            lock (this)
            {
                ROMMSQLRequest rommRequest = new ROMMSQLRequest();
                rommRequest.MessageID = ROMXMessageID.ExecuteNonQuery;
                rommRequest.SQLStatement = sSQL;

                DBConnRomitSql__Client romitClient = new DBConnRomitSql__Client();
                ROMMSQLResult rommResult = romitClient.GetSQLResult(rommRequest, this.RomitConfig);

                int nRet = rommResult.ResultAffectedRowsCount;
                return nRet;
            }
        }

        /// <summary>
        /// 运行查询语句                                                                    
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <returns>查得的结果集</returns>
        override public DataSet ExecuteDataSet(string sSQL)
        {
            lock (this)
            {
                ROMMSQLRequest rommRequest = new ROMMSQLRequest();
                rommRequest.MessageID = ROMXMessageID.ExecuteDataSet;
                rommRequest.SQLStatement = sSQL;

                DBConnRomitSql__Client romitClient = new DBConnRomitSql__Client();
                ROMMSQLResult rommResult = romitClient.GetSQLResult(rommRequest, this.RomitConfig);

                DataSet ds = rommResult.ResultDataSet;
                return ds;
            }
        }

        public override IDataReader ExecuteDataReader(string sSQL)
        {
            throw new Exception("RomitSQL不支持ExecuteDataReader");
        }

        /// <summary>
        /// 运行查询语句
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <returns>查得的一个数值</returns>
        override public object ExecuteScalar(string sSQL)
        {
            lock (this)
            {
                ROMMSQLRequest rommRequest = new ROMMSQLRequest();
                rommRequest.MessageID = ROMXMessageID.ExecuteScalar;
                rommRequest.SQLStatement = sSQL;

                DBConnRomitSql__Client romitClient = new DBConnRomitSql__Client();
                ROMMSQLResult rommResult = romitClient.GetSQLResult(rommRequest, this.RomitConfig);

                return rommResult.ResultScalarResult;
            }
        }
    }

    /// <summary>
    /// RomitSQL的数据连接配置
    /// </summary>
    class DBConnRomitSql__Config
    {
        private string _servicePageUrl = "";
        /// <summary>
        /// 服务端页面地址
        /// </summary>
        public string ServicePageUrl
        {
            get { return _servicePageUrl; }
            set { _servicePageUrl = value; }
        }

        private string _serviceDBInstanceName = "";
        /// <summary>
        /// 服务端数据库实例名
        /// </summary>
        public string ServiceDBInstanceName
        {
            get { return _serviceDBInstanceName; }
            set { _serviceDBInstanceName = value; }
        }

        /// <summary>
        /// 解析数据连接串
        /// </summary>
        /// <param name="DBConnString">数据连接串</param>
        /// <remarks>
        /// 数据链接串的形式示例：
        /// <connectString value="ServiceUrl=http://localhost:5985/WebSite/service/romit_sql_server/romit_sql_server.aspx;" />
        /// </remarks>
        public void ParseDBConnString(string sDBConnString)
        {
            //======= 1. 按";"分隔 ==================
            string[] arrKeyValue = sDBConnString.Split(';');
            for (int i = 0; i < arrKeyValue.Length; i++)
            {
                string sKeyValue = arrKeyValue[i];

                //======== 2. 得到关键字和值 ===========
                int nEqualPos = sKeyValue.IndexOf('=');
                if (nEqualPos == -1)
                    continue;

                string sKey = sKeyValue.Substring(0, nEqualPos);
                string sValue = sKeyValue.Substring(nEqualPos + 1);

                //========== 3. 解析关键字和值 ============
                if (sKey == "ServiceUrl")
                    this.ServicePageUrl = sValue;
                else if (sKey == "InstanceName")
                    this.ServiceDBInstanceName = sValue;
            }
        }
    }

    class DBConnRomitSql__Client
    {
        public ROMMSQLResult GetSQLResult(ROMMSQLRequest request, DBConnRomitSql__Config romitConfig)
        {
            ROMMSQLResult romitResponse = new ROMMSQLResult();

            WebClient webClient = new WebClient();
            string sServicePage = romitConfig.ServicePageUrl;

            byte[] bsRequestContent = request.ToBytes();

            //FileUtil.WriteBytesToFile("c:\\temp\\romit-request.xml", bsRequestContent);

            try
            {
                byte[] bsPageContent = webClient.UploadData(sServicePage,
                        "POST", bsRequestContent);
                //FileUtil.WriteBytesToFile("c:\\temp\\romit-response.xml", bsPageContent);

                romitResponse.ReadFrom(bsPageContent);

                if (romitResponse.ExceptionString != "")
                    throw new Exception("RomitSQL访问出错(远程端) - " + romitResponse.ExceptionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return romitResponse;
        }
    }
}

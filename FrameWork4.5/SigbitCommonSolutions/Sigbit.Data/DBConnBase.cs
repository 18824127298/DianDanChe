using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sigbit.Data
{
    /// <summary>
    /// 数据库访问的基类，用于继承出各类数据库访问
    /// </summary>
    abstract public class DBConnBase
    {
        private string _connectString;

        /// <summary>
        /// 数据库的连接串
        /// </summary>
        public string ConnectString
        {
            get { return _connectString; }
            set { _connectString = value; }
        }

        /// <summary>
        /// 判断是否已经连接上
        /// </summary>
        /// <returns>是否已经创建连接</returns>
        abstract public bool IsConnected();

        /// <summary>
        /// 连接数据库
        /// </summary>
        abstract public void Connect();

        /// <summary>
        /// 断连数据库
        /// </summary>
        abstract public void Disconnect();

        /// <summary>
        /// 运行更新语句
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <returns>影响到的行数</returns>
        abstract public int ExecuteNonQuery(string sSQL);

        /// <summary>
        /// 运行查询语句
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <returns>查得的结果集</returns>
        abstract public DataSet ExecuteDataSet(string sSQL);

        /// <summary>
        /// 运行查询语句，得到DataReader
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <returns>DataReader</returns>
        abstract public IDataReader ExecuteDataReader(string sSQL);

        /// <summary>
        /// 运行查询语句
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <returns>查得的一个数值</returns>
        abstract public object ExecuteScalar(string sSQL);
    }
}

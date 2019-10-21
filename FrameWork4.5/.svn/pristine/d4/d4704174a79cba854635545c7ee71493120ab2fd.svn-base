using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Sigbit.Data
{
    /// <summary>
    /// OleDb类型数据库访问封装
    /// </summary>
    /// <remarks>
    /// 主要用于Access数据库访问
    /// </remarks>
    public class DBConnOleDb:DBConnBase
    {
        private OleDbConnection  _sqlConn = null;

        /// <summary>
        /// 判断是否已经连接上
        /// </summary>
        /// <returns>是否已经创建连接</returns>
        override public bool IsConnected()
        {
            if (_sqlConn == null)
                return false;

            if (_sqlConn.State == ConnectionState.Open)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        override public void Connect()
        {
            if (IsConnected())
                Disconnect();
           
            _sqlConn = new OleDbConnection(ConnectString);
            _sqlConn.Open();
        }

        /// <summary>
        /// 断连数据库
        /// </summary>
        override public void Disconnect()
        {
            if (!IsConnected())
                return;

            _sqlConn.Close();
            _sqlConn.Dispose();
            _sqlConn = null;
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
                //============= 1. 创建Command ==========

                OleDbCommand cmd;
                cmd = new OleDbCommand(sSQL, _sqlConn);

                //=========== 2. 运行语句 =======
                int nResult = cmd.ExecuteNonQuery();

                //========== 3. 显示数据 ==========
                return nResult;
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
                //============= 1. 创建Command ==========
                OleDbCommand cmd;

                cmd = new OleDbCommand(sSQL, _sqlConn);

                //=========== 2. 填入DataSet =======
                OleDbDataAdapter adapter;
                DataSet ds;

                adapter = new OleDbDataAdapter();
                adapter.SelectCommand = cmd;
                ds = new DataSet();

                adapter.Fill(ds);

                //========== 3. 返回DataSet ==========
                return ds;
            }
        }

        public override IDataReader ExecuteDataReader(string sSQL)
        {
            //============= 1. 创建Command ==========
            OleDbCommand cmd;

            cmd = new OleDbCommand(sSQL, _sqlConn);
            OleDbDataReader dr = cmd.ExecuteReader();
            return dr;
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
                //============= 1. 创建Command ==========
                OleDbCommand cmd;
                cmd = new OleDbCommand(sSQL, _sqlConn);

                //=========== 2. 运行语句 =======
                object ret;

                ret = cmd.ExecuteScalar();

                //========== 3. 返回结果 ==========
                return ret;
            }
        }
    }
}

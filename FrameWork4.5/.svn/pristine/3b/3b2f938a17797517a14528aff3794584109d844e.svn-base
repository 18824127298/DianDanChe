using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Data.Common;

namespace Sigbit.Data
{
    public class DBConnODBC : DBConnBase
    {
        private OdbcConnection _sqlConn = null;
        /// <summary>
        /// 判断是否已经连接上
        /// </summary>
        /// <returns>是否已经创建连接</returns>
        public override bool IsConnected()
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
        public override void Connect()
        {
            if (IsConnected())
                Disconnect();
            _sqlConn = new OdbcConnection(ConnectString);
            _sqlConn.Open();
        }
        /// <summary>
        /// 断连数据库
        /// </summary>
        public override void Disconnect()
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
        public override int ExecuteNonQuery(string sSQL)
        {
            lock (this)
            {
                //============= 1. 创建Command ==========
                OdbcCommand cmd;
                cmd = new OdbcCommand(sSQL, _sqlConn);

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
        public override System.Data.DataSet ExecuteDataSet(string sSQL)
        {
            lock (this)
            {
                //============= 1. 创建Command ==========
                OdbcCommand cmd;

                cmd = new OdbcCommand(sSQL, _sqlConn);

                //=========== 2. 填入DataSet =======
                OdbcDataAdapter adapter;
                DataSet ds;

                adapter = new OdbcDataAdapter();
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
            OdbcCommand cmd;

            cmd = new OdbcCommand(sSQL, _sqlConn);
            IDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        /// <summary>
        /// 运行查询语句
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <returns>查得的一个数值</returns>
        public override object ExecuteScalar(string sSQL)
        {
            lock (this)
            {

                //============= 1. 创建Command ==========
                OdbcCommand cmd;
                cmd = new OdbcCommand(sSQL, _sqlConn);

                //=========== 2. 运行语句 =======
                object ret;

                ret = cmd.ExecuteScalar();
                ///=========== 因为运行完ExecuteScalar后，连接后自动断掉，因此加上这一句 ====
                ///========= end of 因为运行完... ==========

                //========== 3. 返回结果 ==========
                return ret;
            }
        }

       
    }
}

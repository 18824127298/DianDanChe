using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using MySQLDriverCS;

namespace Sigbit.Data
{
    /// <summary>
    /// MySQL的数据库访问封装
    /// </summary>
    public class DBConnMySql : DBConnBase
    {
        private MySQLConnection _sqlConn = null;

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

            _sqlConn = new MySQLConnection(ConnectString);
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
                MySQLCommand cmd;
                cmd = new MySQLCommand(sSQL, _sqlConn);

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
            string sLimitSQL = SelectSQLTransFromTopToLimit(sSQL);

            lock (this)
            {
                //============= 1. 创建Command ==========
                MySQLCommand cmd;

                cmd = new MySQLCommand(sLimitSQL, _sqlConn);

                //=========== 2. 填入DataSet =======
                MySQLDataAdapter adapter;
                DataSet ds;

                adapter = new MySQLDataAdapter();
                adapter.SelectCommand = cmd;
                ds = new DataSet();

                adapter.Fill(ds);

                //========== 3. 返回DataSet ==========
                return ds;
            }
        }

        /// <summary>
        /// 将含有Top的SQL语句转为含有Limit的SQL语句
        /// </summary>
        /// <param name="sTopSQL">含有Top的SQL语句</param>
        /// <returns>含有Limit的SQL语句</returns>
        private string SelectSQLTransFromTopToLimit(string sTopSQL)
        {
            //======== 1. 找到Top的位置（必须靠前） ==========
            int nTopPos = sTopSQL.ToLower().IndexOf("top");
            if (nTopPos >= 12 || nTopPos < 0)
                return sTopSQL;

            //======= 2. Top后面的5个字符之内，找到一个数字 =======
            char[] arrDigit = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int nFirstDigitPos = sTopSQL.IndexOfAny(arrDigit, nTopPos + 4, 4);
            if (nFirstDigitPos < 0)
                return sTopSQL;

            //========== 3. 找到连续的最后一位数字 ==========
            int nLastDigitPos = nFirstDigitPos;
            for (int i = nFirstDigitPos + 1; i < sTopSQL.Length; i++)
            {
                char c = sTopSQL[i];
                if (c >= '0' && c <= '9')
                    nLastDigitPos = i;
                else
                    break;
            }

            //========== 4. 将数字抽出变为整数 ===========
            string sDigitSubString = sTopSQL.Substring
                    (nFirstDigitPos, nLastDigitPos - nFirstDigitPos + 1);
            int nLimitRowCount = Convert.ToInt32(sDigitSubString);

            //========== 5. 将top N的部分从SQL语句中去掉 ==========
            string sRet = sTopSQL.Remove(nTopPos, nLastDigitPos - nTopPos + 1);
            sRet += " limit " + nLimitRowCount.ToString();

            return sRet;
        }

        public override IDataReader ExecuteDataReader(string sSQL)
        {
            string sLimitSQL = SelectSQLTransFromTopToLimit(sSQL);
            lock (this)
            {
                //============= 1. 创建Command ==========
                MySQLCommand cmd;

                cmd = new MySQLCommand(sLimitSQL, _sqlConn);
                IDataReader dr = cmd.ExecuteReader();
                return dr;
            }
        }

        /// <summary>
        /// 运行查询语句
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <returns>查得的一个数值</returns>
        override public object ExecuteScalar(string sSQL)
        {
            string sLimitSQL = SelectSQLTransFromTopToLimit(sSQL);

            //Mango 修改,在拨测系统下,这个地方会造成很大的延迟
			lock (this)
			{
	            DataSet ds = ExecuteDataSet(sSQL);
	            if (ds.Tables[0].Rows.Count > 0)
	            {
	                return ds.Tables[0].Rows[0][0].ToString();
	            }
	            else
	            {
	                return null;
	            }
			}

            //string sLimitSQL = SelectSQLTransFromTopToLimit(sSQL);

            //lock (this)
            //{
            //    //============= 1. 创建Command ==========
            //    MySQLCommand cmd;
            //    cmd = new MySQLCommand(sLimitSQL, _sqlConn);

            //    //=========== 2. 运行语句 =======
            //    object ret;

            //    ret = cmd.ExecuteScalar();
            //    ///=========== 因为运行完ExecuteScalar后，连接后自动断掉，因此加上这一句 ====
            //    _sqlConn.Open();
            //    ///========= end of 因为运行完... ==========

            //    //========== 3. 返回结果 ==========
            //    return ret;
            //}
        }

    }
}

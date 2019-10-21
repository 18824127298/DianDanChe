using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Sigbit.Data
{
    /// <summary>
    /// Microsoft SQLServer的数据库访问封装
    /// </summary>
    public class DBConnMSSql : DBConnBase
    {
        private SqlConnection _sqlConn = null;

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

            _sqlConn = new SqlConnection(ConnectString);
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
                SqlCommand cmd;
                cmd = new SqlCommand(sSQL, _sqlConn);

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
            string sTopSQL = SelectSQLTransFromLimitToTop(sSQL);

            lock (this)
            {
                //============= 1. 创建Command ==========
                SqlCommand cmd;

                cmd = new SqlCommand(sTopSQL, _sqlConn);

                //=========== 2. 填入DataSet =======
                SqlDataAdapter adapter;
                DataSet ds;

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                ds = new DataSet();

                adapter.Fill(ds);

                //========== 3. 返回DataSet ==========
                return ds;
            }
        }

        /// <summary>
        /// 将含有Limit的SQL语句换成含有Top的SQL语句
        /// </summary>
        /// <param name="sLimitSQL">含有Limit的SQL语句</param>
        /// <returns>含有Top的SQL语句</returns>
        private string SelectSQLTransFromLimitToTop(string sLimitSQL)
        {
            //======== 1. 找到Limit的位置（必须靠后） ==========
            int nLimitPos = sLimitSQL.ToLower().LastIndexOf("limit");
            if (nLimitPos < 0 || nLimitPos < sLimitSQL.Length - 10)
                return sLimitSQL;

            //======= 2. Limit后面的5个字符之内，找到一个数字 =======
            char[] arrDigit = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int nFindLen = sLimitSQL.Length - (nLimitPos + 4);
            if (nFindLen > 4)
                nFindLen = 4;
            int nFirstDigitPos = sLimitSQL.IndexOfAny(arrDigit, (nLimitPos + 4), nFindLen);
            if (nFirstDigitPos < 0)
                return sLimitSQL;

            //========== 3. 找到连续的最后一位数字 ==========
            int nLastDigitPos = nFirstDigitPos;
            for (int i = nFirstDigitPos + 1; i < sLimitSQL.Length; i++)
            {
                char c = sLimitSQL[i];
                if (c >= '0' && c <= '9')
                    nLastDigitPos = i;
                else
                    break;
            }

            //========== 4. 将数字抽出变为整数 ===========
            string sDigitSubString = sLimitSQL.Substring
                    (nFirstDigitPos, nLastDigitPos - nFirstDigitPos + 1);
            int nTopRowCount = Convert.ToInt32(sDigitSubString);

            //========== 5. 将limit N的部分从SQL语句中去掉 ==========
            string sRet = sLimitSQL.Remove(nLimitPos, nLastDigitPos - nLimitPos + 1);

            //========== 6. 找到select语句的部分 =============
            int nSelectPos = sRet.ToLower().IndexOf("select");
            if (nSelectPos < 0 || nSelectPos > 5)
                return sLimitSQL;

            //========== 7. 将select语句的部分换成select topN ============
            sRet = sRet.Substring(0, nSelectPos) + "select top " + nTopRowCount.ToString() + " "
                    + sRet.Substring(nSelectPos + 6);

            return sRet;
        }

        public override IDataReader ExecuteDataReader(string sSQL)
        {
            lock (this)
            {
                //============= 1. 创建Command ==========
                SqlCommand cmd;

                cmd = new SqlCommand(sSQL, _sqlConn);
                SqlDataReader dr = cmd.ExecuteReader();
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
            lock (this)
            {
                //============= 1. 创建Command ==========
                SqlCommand cmd;
                cmd = new SqlCommand(sSQL, _sqlConn);

                //=========== 2. 运行语句 =======
                object ret;

                ret = cmd.ExecuteScalar();

                //========== 3. 返回结果 ==========
                return ret;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using MySQLDriverCS;

namespace Sigbit.Data
{
    /// <summary>
    /// MySQL�����ݿ���ʷ�װ
    /// </summary>
    public class DBConnMySql : DBConnBase
    {
        private MySQLConnection _sqlConn = null;

        /// <summary>
        /// �ж��Ƿ��Ѿ�������
        /// </summary>
        /// <returns>�Ƿ��Ѿ���������</returns>
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
        /// �������ݿ�
        /// </summary>
        override public void Connect()
        {
            if (IsConnected())
                Disconnect();

            _sqlConn = new MySQLConnection(ConnectString);
            _sqlConn.Open();
        }

        /// <summary>
        /// �������ݿ�
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
        /// ���и������
        /// </summary>
        /// <param name="sSQL">SQL���</param>
        /// <returns>Ӱ�쵽������</returns>
        override public int ExecuteNonQuery(string sSQL)
        {
            lock (this)
            {
                //============= 1. ����Command ==========
                MySQLCommand cmd;
                cmd = new MySQLCommand(sSQL, _sqlConn);

                //=========== 2. ������� =======
                int nResult = cmd.ExecuteNonQuery();

                //========== 3. ��ʾ���� ==========
                return nResult;
            }
        }

        /// <summary>
        /// ���в�ѯ���                                                                    
        /// </summary>
        /// <param name="sSQL">SQL���</param>
        /// <returns>��õĽ����</returns>
        override public DataSet ExecuteDataSet(string sSQL)
        {
            string sLimitSQL = SelectSQLTransFromTopToLimit(sSQL);

            lock (this)
            {
                //============= 1. ����Command ==========
                MySQLCommand cmd;

                cmd = new MySQLCommand(sLimitSQL, _sqlConn);

                //=========== 2. ����DataSet =======
                MySQLDataAdapter adapter;
                DataSet ds;

                adapter = new MySQLDataAdapter();
                adapter.SelectCommand = cmd;
                ds = new DataSet();

                adapter.Fill(ds);

                //========== 3. ����DataSet ==========
                return ds;
            }
        }

        /// <summary>
        /// ������Top��SQL���תΪ����Limit��SQL���
        /// </summary>
        /// <param name="sTopSQL">����Top��SQL���</param>
        /// <returns>����Limit��SQL���</returns>
        private string SelectSQLTransFromTopToLimit(string sTopSQL)
        {
            //======== 1. �ҵ�Top��λ�ã����뿿ǰ�� ==========
            int nTopPos = sTopSQL.ToLower().IndexOf("top");
            if (nTopPos >= 12 || nTopPos < 0)
                return sTopSQL;

            //======= 2. Top�����5���ַ�֮�ڣ��ҵ�һ������ =======
            char[] arrDigit = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int nFirstDigitPos = sTopSQL.IndexOfAny(arrDigit, nTopPos + 4, 4);
            if (nFirstDigitPos < 0)
                return sTopSQL;

            //========== 3. �ҵ����������һλ���� ==========
            int nLastDigitPos = nFirstDigitPos;
            for (int i = nFirstDigitPos + 1; i < sTopSQL.Length; i++)
            {
                char c = sTopSQL[i];
                if (c >= '0' && c <= '9')
                    nLastDigitPos = i;
                else
                    break;
            }

            //========== 4. �����ֳ����Ϊ���� ===========
            string sDigitSubString = sTopSQL.Substring
                    (nFirstDigitPos, nLastDigitPos - nFirstDigitPos + 1);
            int nLimitRowCount = Convert.ToInt32(sDigitSubString);

            //========== 5. ��top N�Ĳ��ִ�SQL�����ȥ�� ==========
            string sRet = sTopSQL.Remove(nTopPos, nLastDigitPos - nTopPos + 1);
            sRet += " limit " + nLimitRowCount.ToString();

            return sRet;
        }

        public override IDataReader ExecuteDataReader(string sSQL)
        {
            string sLimitSQL = SelectSQLTransFromTopToLimit(sSQL);
            lock (this)
            {
                //============= 1. ����Command ==========
                MySQLCommand cmd;

                cmd = new MySQLCommand(sLimitSQL, _sqlConn);
                IDataReader dr = cmd.ExecuteReader();
                return dr;
            }
        }

        /// <summary>
        /// ���в�ѯ���
        /// </summary>
        /// <param name="sSQL">SQL���</param>
        /// <returns>��õ�һ����ֵ</returns>
        override public object ExecuteScalar(string sSQL)
        {
            string sLimitSQL = SelectSQLTransFromTopToLimit(sSQL);

            //Mango �޸�,�ڲ���ϵͳ��,����ط�����ɺܴ���ӳ�
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
            //    //============= 1. ����Command ==========
            //    MySQLCommand cmd;
            //    cmd = new MySQLCommand(sLimitSQL, _sqlConn);

            //    //=========== 2. ������� =======
            //    object ret;

            //    ret = cmd.ExecuteScalar();
            //    ///=========== ��Ϊ������ExecuteScalar�����Ӻ��Զ��ϵ�����˼�����һ�� ====
            //    _sqlConn.Open();
            //    ///========= end of ��Ϊ������... ==========

            //    //========== 3. ���ؽ�� ==========
            //    return ret;
            //}
        }

    }
}

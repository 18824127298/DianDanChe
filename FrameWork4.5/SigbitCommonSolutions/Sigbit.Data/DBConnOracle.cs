using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Sigbit.Common.Encrypt;

namespace Sigbit.Data
{
    /// <summary>
    /// Oracle���ݿ���ʷ�װ
    /// </summary>
    public class DBConnOracle : DBConnBase
    {
        private static bool _SKIP_ORACLE_DB_PARASER = false;
        /// <summary>
        /// ����Oracle��SQL����DataSet��ת������
        /// </summary>
        public static bool SKIP_ORACLE_DB_PARASER
        {
            get { return DBConnOracle._SKIP_ORACLE_DB_PARASER; }
            set { DBConnOracle._SKIP_ORACLE_DB_PARASER = value; }
        }


        /// <summary>
        /// Oracle  ���ݿ�Ĺ���
        /// </summary>
        /// <remarks>
        /// ��Sql����е� '' �滻Ϊ '#',������е�'#'�滻Ϊ''
        /// </remarks>
        /*
         * ���������䡣ԭ���Ĵ����н�������ĵ������м�Ӳ���#�š����˵�������2013-04-01������
insert into qc_log_answer 
( log_uid,          request_flow_id,  
  user_msisdn,      user_brand,       
  receive_time,     proc_duration,    
  ques_text,        ques_command,     
  ques_brand,       ques_keyword,     
  event_type_id,    service_class_id, 
  answer_text,      answer_node_id,   
  final_text,       final_node_id,    
  final_changed,    remarks           
) values (          
'61904a3f-7830-47c0-b9d0-67446c9361db','',
'18207222637','ȫ��ͨ',
'2013-03-28 20:33:30',0,
'[����]''��','',
'','',
'','',
'���ã������ѯ�ƶ�ҵ������ٺ����ƶ�Ӫҵ���򲦴�10086��ѯ��лл��',640,
'',0,
'N','')
         * */
        class OracleDBParser
        {
            public static string SqlParser(string sSql)
            {

                sSql = ReguLongTableName(sSql);

                if (DBConnOracle.SKIP_ORACLE_DB_PARASER)
                    return sSql;

                sSql = sSql.Trim();
                int nPos = 0;
                int nPos1 = 0, nPos2 = 0;

                //ѭ������ ''
                while (nPos < sSql.Length)
                {
                    if (sSql[nPos] == '\'')
                    {
                        if (nPos1 == 0)
                        {
                            //��һ��������Ϊ��
                            nPos1 = nPos;
                        }
                        else
                        {
                            //�ڶ���������Ϊ��
                            nPos2 = nPos;
                            //nPos++;               ��ע�͵������С�����Error��
                            //continue;             ��ע�͵������С�����Error��
                        }
                    }

                    //�����ǲ����ҵ�������������
                    if ((nPos1 != 0) && (nPos2 != 0))
                    {
                        //���������������Ƿ���
                        if (nPos2 - nPos1 == 1)
                        {
                            sSql = sSql.Insert(nPos2, "#");
                            nPos++;
                        }
                        nPos1 = nPos2 = 0;
                    }
                    nPos++;
                }

                //��������ǲ����ҵ�������������
                if ((nPos1 != 0) && (nPos2 != 0))
                {
                    //���������������Ƿ���
                    if (nPos2 - nPos1 == 1)
                    {
                        sSql = sSql.Insert(nPos2, "#");
                        nPos++;
                    }
                    nPos1 = nPos2 = 0;
                }

                return sSql;
            }

            /// <summary>
            /// ���������,�滻"#"Ϊ""
            /// </summary>
            /// <param name="ds"></param>
            public static void DatsSetParser(DataSet ds)
            {
                if (DBConnOracle.SKIP_ORACLE_DB_PARASER)
                    return;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        if (ds.Tables[0].Columns[i].DataType == typeof(string))
                        {
                            if (row[i].ToString() == "#")
                            {
                                row[i] = "";
                            }
                        }
                    }

                }
            }




        }


        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="sSQL"></param>
        public static string ReguLongTableName(string sSQL)
        {
            sSQL = sSQL.Trim();

            string[] arrSQLSegments = sSQL.Split(' ');

            if (arrSQLSegments.Length <= 2)
                return sSQL;


            bool bWantReguTable = false;   //�Ƿ���Ҫ������

            string sSQLType = arrSQLSegments[0].ToLower();

            bool bHasFetchTable = false;�� //�Ƿ�Ҫ��ȡ��

            string sFetchTableName = "";

            string sConvertTableName = "";


            bool bWantNextFetch = true;


            for (int i = 1; i < arrSQLSegments.Length; i++)
            {
                if (!bWantNextFetch)
                    break;

                string sSegmentItem = arrSQLSegments[i].Trim().ToLower();

                if (sSegmentItem == "")
                    continue;

                switch (sSQLType)
                {
                    case "insert":
                        if (sSegmentItem == "into")
                        {
                            bHasFetchTable = true;
                            continue;
                        }
                        break;
                    case "update":
                        bHasFetchTable = true;
                        break;
                    case "delete":
                        if (sSegmentItem == "from")
                        {
                            bHasFetchTable = true;
                            continue;
                        }
                        break;
                    case "select":
                        if (sSegmentItem == "from")
                        {
                            bHasFetchTable = true;
                            continue;
                        }

                        if (sSegmentItem == "join")
                        {
                            bHasFetchTable = true;
                            continue;
                        }

                        break;
                    case "create":

                        if (sSegmentItem == "table")
                        {
                            bHasFetchTable = true;
                            continue;
                        }

                        break;
                }



                if (bHasFetchTable)
                {

                    string sCurrentSegment = arrSQLSegments[i];

                    bool bHasFindSpecChar = false;

                    if (sCurrentSegment.Contains("("))
                    {
                        //insert into sbt_user(user_uid,user_name.....
                        //�˴����������������������������
                        bHasFindSpecChar = true;
                    }

                    string sRawTableName = "";
                    if (bHasFindSpecChar)
                    {
                        sRawTableName = sCurrentSegment.Split('(')[0];
                        sFetchTableName = sRawTableName;
                    }
                    else
                    {
                        sFetchTableName = sCurrentSegment;
                    }


                    if (sFetchTableName.Length > 30)
                    {
                        bWantReguTable = true;

                        sConvertTableName = ConvertLongTableName(sFetchTableName);

                        if (bHasFindSpecChar)
                        {
                            arrSQLSegments[i] = arrSQLSegments[i].Replace(sRawTableName, sConvertTableName);
                        }
                        else
                        {
                            arrSQLSegments[i] = sConvertTableName;
                        }
                    }

                    bHasFetchTable = false;

                    if (sSQLType != "select")
                    {
                        bWantNextFetch = false;   //ֻ�в�ѯ������²�Ѱ�����µı�
                    }
                }

            }

            if (bWantReguTable)
            {
                sSQL = "";
                for (int i = 0; i < arrSQLSegments.Length; i++)
                {
                    sSQL += arrSQLSegments[i] + " ";
                }
            }

            return sSQL;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sTableName"></param>
        /// <returns></returns>
        public static string ConvertLongTableName(string sTableName)
        {
            string sRet = "";

            if (sTableName.Length <= 30)
                return sTableName;


            string sFixTableName = sTableName.Substring(0, 24);

            string sDyncTableName = sTableName.Substring(24);


            string sEncryptDyncTableName = EncryptUtil.MD5String(sDyncTableName);

            sRet = sFixTableName + "_" + sEncryptDyncTableName.Substring(0, 5);

            return sRet;

        }


        public static string Get__SubmitOracleServerSQL(string sSQL)
        {
            return OracleDBParser.SqlParser(sSQL);
        }

        private OracleConnection _sqlConn = null;
        /// <summary>
        /// �ж��Ƿ��Ѿ�������
        /// </summary>
        /// <returns>�Ƿ��Ѿ���������</returns>
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
        /// �������ݿ�
        /// </summary>
        public override void Connect()
        {
            if (IsConnected())
                Disconnect();
            _sqlConn = new OracleConnection(ConnectString);
            _sqlConn.Open();
        }

        /// <summary>
        /// �������ݿ�
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
        /// ���и������
        /// </summary>
        /// <param name="sSQL">SQL���</param>
        /// <returns>Ӱ�쵽������</returns>
        public override int ExecuteNonQuery(string sSQL)
        {
            sSQL = OracleDBParser.SqlParser(sSQL);
            lock (this)
            {
                //============= 1. ����Command ==========
                OracleCommand cmd;
                cmd = new OracleCommand(sSQL, _sqlConn);

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
        public override System.Data.DataSet ExecuteDataSet(string sSQL)
        {
            sSQL = SelectSQLTransFromTopLimitToRownum(sSQL);
            sSQL = OracleDBParser.SqlParser(sSQL);

            lock (this)
            {
                //============= 1. ����Command ==========
                OracleCommand cmd;

                cmd = new OracleCommand(sSQL, _sqlConn);

                //=========== 2. ����DataSet =======
                OracleDataAdapter adapter;
                DataSet ds;

                adapter = new OracleDataAdapter();
                adapter.SelectCommand = cmd;
                ds = new DataSet();

                adapter.Fill(ds);

                //========== 3. ����DataSet ==========
                OracleDBParser.DatsSetParser(ds);
                return ds;
            }
        }

        public string SelectSQLTransFromTopLimitToRownum__OnlyForTest(string sSQL)
        {
            return SelectSQLTransFromTopLimitToRownum(sSQL);
        }

        private string SelectSQLTransFromTopLimitToRownum(string sSQL)
        {
            string sRet = SelectSQLTransFromTopLimitToRownum__Top(sSQL);
            sRet = SelectSQLTransFromTopLimitToRownum__Limit(sRet);

            return sRet;
        }

        /// <summary>
        /// ������Top��SQL���תΪ����rownum��SQL���
        /// </summary>
        /// <param name="sTopSQL">����Top��SQL���</param>
        /// <returns>����Limit��SQL���</returns>
        private string SelectSQLTransFromTopLimitToRownum__Top(string sTopSQL)
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

            //=========== 6. Ƕ��rownum���� ===============
            sRet = SelectSQLTransFromTopLimitToRownum__AddRownumPart(sRet, nLimitRowCount);
            return sRet;
        }

        /// <summary>
        /// ������Limit��SQL��任�ɺ���Top��SQL���
        /// </summary>
        /// <param name="sLimitSQL">����Limit��SQL���</param>
        /// <returns>����Top��SQL���</returns>
        private string SelectSQLTransFromTopLimitToRownum__Limit(string sLimitSQL)
        {
            //======== 1. �ҵ�Limit��λ�ã����뿿�� ==========
            int nLimitPos = sLimitSQL.ToLower().LastIndexOf("limit");
            if (nLimitPos < 0 || nLimitPos < sLimitSQL.Length - 10)
                return sLimitSQL;

            //======= 2. Limit�����5���ַ�֮�ڣ��ҵ�һ������ =======
            char[] arrDigit = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int nFindLen = sLimitSQL.Length - (nLimitPos + 4);
            if (nFindLen > 4)
                nFindLen = 4;
            int nFirstDigitPos = sLimitSQL.IndexOfAny(arrDigit, (nLimitPos + 4), nFindLen);
            if (nFirstDigitPos < 0)
                return sLimitSQL;

            //========== 3. �ҵ����������һλ���� ==========
            int nLastDigitPos = nFirstDigitPos;
            for (int i = nFirstDigitPos + 1; i < sLimitSQL.Length; i++)
            {
                char c = sLimitSQL[i];
                if (c >= '0' && c <= '9')
                    nLastDigitPos = i;
                else
                    break;
            }

            //========== 4. �����ֳ����Ϊ���� ===========
            string sDigitSubString = sLimitSQL.Substring
                    (nFirstDigitPos, nLastDigitPos - nFirstDigitPos + 1);
            int nTopRowCount = Convert.ToInt32(sDigitSubString);

            //========== 5. ��limit N�Ĳ��ִ�SQL�����ȥ�� ==========
            string sRet = sLimitSQL.Remove(nLimitPos, nLastDigitPos - nLimitPos + 1);

            //========== 6. ����rownum���� ============
            sRet = SelectSQLTransFromTopLimitToRownum__AddRownumPart(sRet, nTopRowCount);

            return sRet;
        }

        private string SelectSQLTransFromTopLimitToRownum__AddRownumPart(string sSQL, int nLimitRowCount)
        {
            string sRet = sSQL;

            //======= 1. �ҵ�β������������� =============
            string sHeadPart = sRet;
            string sTailPart = "";

            int nIndexGroupBy = sRet.IndexOf("group by");
            if (nIndexGroupBy != -1)
            {
                sHeadPart = sRet.Substring(0, nIndexGroupBy);
                sTailPart = sRet.Substring(nIndexGroupBy);
            }
            else
            {
                int nIndexOrderBy = sRet.IndexOf("order by");
                if (nIndexOrderBy != -1)
                {
                    sHeadPart = sRet.Substring(0, nIndexOrderBy);
                    sTailPart = sRet.Substring(nIndexOrderBy);
                }
            }

            //======= 2. ������û��where�Ӿ� =============
            bool bFindWhere = false;
            if (sHeadPart.IndexOf("where") != -1)
                bFindWhere = true;

            //========= 3. �γ����ķ��ؽ�� ===============
            if (bFindWhere)
                sRet = sHeadPart + " and rownum <= " + nLimitRowCount.ToString() + " " + sTailPart;
            else
                sRet = sHeadPart + " where rownum <= " + nLimitRowCount.ToString() + " " + sTailPart;

            return sRet;
        }

        public override IDataReader ExecuteDataReader(string sSQL)
        {
            throw new Exception("û�д���#�Ϳյ�ת��");
            //sSQL = SelectSQLTransFromTopLimitToRownum(sSQL);
            //sSQL = OracleDBParser.SqlParser(sSQL);

            //lock (this)
            //{
            //    //============= 1. ����Command ==========
            //    OracleCommand cmd;
            //    cmd = new OracleCommand(sSQL, _sqlConn);
            //    IDataReader dr = cmd.ExecuteReader();
            //    return dr;
            //}
        }

        /// <summary>
        /// ���в�ѯ���
        /// </summary>
        /// <param name="sSQL">SQL���</param>
        /// <returns>��õ�һ����ֵ</returns>
        public override object ExecuteScalar(string sSQL)
        {
            sSQL = OracleDBParser.SqlParser(sSQL);
            lock (this)
            {            //============= 1. ����Command ==========
                OracleCommand cmd;
                cmd = new OracleCommand(sSQL, _sqlConn);

                //=========== 2. ������� =======
                object ret;

                ret = cmd.ExecuteScalar();

                //��2015.03.20 Zick���Ϊ�յ��жϡ�
                //if (ret.ToString() == "#")
                if (ret != null && ret.ToString() == "#")
                {
                    ret = "";
                }
                ///=========== ��Ϊ������ExecuteScalar�����Ӻ��Զ��ϵ�����˼�����һ�� ====
                ///========= end of ��Ϊ������... ==========

                //========== 3. ���ؽ�� ==========
                return ret;
            }
        }
    }
}

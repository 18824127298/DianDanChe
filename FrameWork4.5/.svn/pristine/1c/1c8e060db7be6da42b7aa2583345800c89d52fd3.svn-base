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
    /// Oracle数据库访问封装
    /// </summary>
    public class DBConnOracle : DBConnBase
    {
        private static bool _SKIP_ORACLE_DB_PARASER = false;
        /// <summary>
        /// 跳过Oracle的SQL语句和DataSet的转换处理
        /// </summary>
        public static bool SKIP_ORACLE_DB_PARASER
        {
            get { return DBConnOracle._SKIP_ORACLE_DB_PARASER; }
            set { DBConnOracle._SKIP_ORACLE_DB_PARASER = value; }
        }


        /// <summary>
        /// Oracle  数据库的规整
        /// </summary>
        /// <remarks>
        /// 将Sql语句中的 '' 替换为 '#',结果集中的'#'替换为''
        /// </remarks>
        /*
         * 针对以下语句。原来的处理中江汉银后的单引号中间加不了#号。做了调整。【2013-04-01调整】
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
'18207222637','全球通',
'2013-03-28 20:33:30',0,
'[江汉]''银','',
'','',
'','',
'您好，如需查询移动业务，请光临湖北移动营业厅或拨打10086咨询。谢谢！',640,
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

                //循环查找 ''
                while (nPos < sSql.Length)
                {
                    if (sSql[nPos] == '\'')
                    {
                        if (nPos1 == 0)
                        {
                            //第一个单引号为空
                            nPos1 = nPos;
                        }
                        else
                        {
                            //第二个单引号为空
                            nPos2 = nPos;
                            //nPos++;               【注释掉这两行。湖北Error】
                            //continue;             【注释掉这两行。湖北Error】
                        }
                    }

                    //看看是不是找到了两个单引号
                    if ((nPos1 != 0) && (nPos2 != 0))
                    {
                        //看看两个单引号是否挨着
                        if (nPos2 - nPos1 == 1)
                        {
                            sSql = sSql.Insert(nPos2, "#");
                            nPos++;
                        }
                        nPos1 = nPos2 = 0;
                    }
                    nPos++;
                }

                //看看最后是不是找到了两个单引号
                if ((nPos1 != 0) && (nPos2 != 0))
                {
                    //看看两个单引号是否挨着
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
            /// 遍历结果集,替换"#"为""
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
        /// 规整长表名
        /// </summary>
        /// <param name="sSQL"></param>
        public static string ReguLongTableName(string sSQL)
        {
            sSQL = sSQL.Trim();

            string[] arrSQLSegments = sSQL.Split(' ');

            if (arrSQLSegments.Length <= 2)
                return sSQL;


            bool bWantReguTable = false;   //是否需要规整表

            string sSQLType = arrSQLSegments[0].ToLower();

            bool bHasFetchTable = false;　 //是否要获取表

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
                        //此处避免出现左括号与表名相连的情况
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
                        bWantNextFetch = false;   //只有查询的情况下才寻找余下的表
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
            _sqlConn = new OracleConnection(ConnectString);
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
            sSQL = OracleDBParser.SqlParser(sSQL);
            lock (this)
            {
                //============= 1. 创建Command ==========
                OracleCommand cmd;
                cmd = new OracleCommand(sSQL, _sqlConn);

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
            sSQL = SelectSQLTransFromTopLimitToRownum(sSQL);
            sSQL = OracleDBParser.SqlParser(sSQL);

            lock (this)
            {
                //============= 1. 创建Command ==========
                OracleCommand cmd;

                cmd = new OracleCommand(sSQL, _sqlConn);

                //=========== 2. 填入DataSet =======
                OracleDataAdapter adapter;
                DataSet ds;

                adapter = new OracleDataAdapter();
                adapter.SelectCommand = cmd;
                ds = new DataSet();

                adapter.Fill(ds);

                //========== 3. 返回DataSet ==========
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
        /// 将含有Top的SQL语句转为含有rownum的SQL语句
        /// </summary>
        /// <param name="sTopSQL">含有Top的SQL语句</param>
        /// <returns>含有Limit的SQL语句</returns>
        private string SelectSQLTransFromTopLimitToRownum__Top(string sTopSQL)
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

            //=========== 6. 嵌入rownum部分 ===============
            sRet = SelectSQLTransFromTopLimitToRownum__AddRownumPart(sRet, nLimitRowCount);
            return sRet;
        }

        /// <summary>
        /// 将含有Limit的SQL语句换成含有Top的SQL语句
        /// </summary>
        /// <param name="sLimitSQL">含有Limit的SQL语句</param>
        /// <returns>含有Top的SQL语句</returns>
        private string SelectSQLTransFromTopLimitToRownum__Limit(string sLimitSQL)
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

            //========== 6. 加入rownum部分 ============
            sRet = SelectSQLTransFromTopLimitToRownum__AddRownumPart(sRet, nTopRowCount);

            return sRet;
        }

        private string SelectSQLTransFromTopLimitToRownum__AddRownumPart(string sSQL, int nLimitRowCount)
        {
            string sRet = sSQL;

            //======= 1. 找到尾部，并拆分两半 =============
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

            //======= 2. 看看有没有where子句 =============
            bool bFindWhere = false;
            if (sHeadPart.IndexOf("where") != -1)
                bFindWhere = true;

            //========= 3. 形成最后的返回结果 ===============
            if (bFindWhere)
                sRet = sHeadPart + " and rownum <= " + nLimitRowCount.ToString() + " " + sTailPart;
            else
                sRet = sHeadPart + " where rownum <= " + nLimitRowCount.ToString() + " " + sTailPart;

            return sRet;
        }

        public override IDataReader ExecuteDataReader(string sSQL)
        {
            throw new Exception("没有处理#和空的转换");
            //sSQL = SelectSQLTransFromTopLimitToRownum(sSQL);
            //sSQL = OracleDBParser.SqlParser(sSQL);

            //lock (this)
            //{
            //    //============= 1. 创建Command ==========
            //    OracleCommand cmd;
            //    cmd = new OracleCommand(sSQL, _sqlConn);
            //    IDataReader dr = cmd.ExecuteReader();
            //    return dr;
            //}
        }

        /// <summary>
        /// 运行查询语句
        /// </summary>
        /// <param name="sSQL">SQL语句</param>
        /// <returns>查得的一个数值</returns>
        public override object ExecuteScalar(string sSQL)
        {
            sSQL = OracleDBParser.SqlParser(sSQL);
            lock (this)
            {            //============= 1. 创建Command ==========
                OracleCommand cmd;
                cmd = new OracleCommand(sSQL, _sqlConn);

                //=========== 2. 运行语句 =======
                object ret;

                ret = cmd.ExecuteScalar();

                //【2015.03.20 Zick添加为空的判断】
                //if (ret.ToString() == "#")
                if (ret != null && ret.ToString() == "#")
                {
                    ret = "";
                }
                ///=========== 因为运行完ExecuteScalar后，连接后自动断掉，因此加上这一句 ====
                ///========= end of 因为运行完... ==========

                //========== 3. 返回结果 ==========
                return ret;
            }
        }
    }
}

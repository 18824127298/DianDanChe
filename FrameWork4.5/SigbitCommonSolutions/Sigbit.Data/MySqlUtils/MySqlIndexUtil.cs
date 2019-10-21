using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using Sigbit.Common;

namespace Sigbit.Data.MySqlUtils
{
    public class MySqlIndexUtil
    {
        public static bool IndexExists(string sTableName, string sColumnName)
        {
            string[] arrColumnNames = new string[1];
            arrColumnNames[0] = sColumnName;

            bool bRet = IndexExists(sTableName, arrColumnNames);
            return bRet;
        }
        
        /// <summary>
        /// 判断一个索引是否存在
        /// </summary>
        /// <param name="sTableName">表名</param>
        /// <param name="arrColumnNames">索引的各个字段</param>
        /// <returns>索引是否存在</returns>
        /// <remarks>
        /// 1. 实现得比较仓促，可读性不好；
        /// 2. 今后，需要改为先得到一个表的若干索引，再来判断索引是否符合；
        /// 3. 假定结果按照Column_name和Seq_in_index是按顺序来排列的；
        /// </remarks>
        public static bool IndexExists(string sTableName, string[] arrColumnNames)
        {
            string sSQLIndex = "show index from " + sTableName;
            DataSet dsIndex = DataHelper.Instance.ExecuteDataSet(sSQLIndex);

            string sCurrentKeyName = "";
            bool bMatchedNow = false;

            for (int i = 0; i < dsIndex.Tables[0].Rows.Count; i++)
            {
                string sKeyName = ConvertUtil.ToString(dsIndex.Tables[0].Rows[i]["Key_name"]);
                string sColumnName = ConvertUtil.ToString(dsIndex.Tables[0].Rows[i]["Column_name"]);
                int nSeqInIndex = ConvertUtil.ToInt(dsIndex.Tables[0].Rows[i]["Seq_in_index"]);

                if (sCurrentKeyName != sKeyName)
                {
                    bMatchedNow = true;
                    sCurrentKeyName = sKeyName;
                }
                else
                {
                    if (!bMatchedNow)
                        continue;
                }

                if (nSeqInIndex > arrColumnNames.Length)
                    continue;

                if (arrColumnNames[nSeqInIndex - 1] != sColumnName)
                {
                    bMatchedNow = false;
                    continue;
                }

                if (arrColumnNames[nSeqInIndex - 1] == sColumnName)
                {
                    if (nSeqInIndex == arrColumnNames.Length)
                        return true;
                }
            }

            return false;
        }
    }
}

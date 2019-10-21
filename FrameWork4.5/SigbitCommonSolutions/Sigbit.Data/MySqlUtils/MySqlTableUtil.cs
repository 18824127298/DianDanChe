using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using Sigbit.Common;
using System.Collections;

namespace Sigbit.Data.MySqlUtils
{
    public class MySqlTableUtil
    {
        public static bool TableExists(string sTableName)
        {
            string sDatabaseName = "";

            int nPosDot = sTableName.IndexOf('.');
            if (nPosDot != -1)
            {
                sDatabaseName = sTableName.Substring(0, nPosDot);
                sTableName = sTableName.Substring(nPosDot + 1);
            }

            string sSQL = "show tables";
            if (sDatabaseName != "")
                sSQL += " from " + sDatabaseName;
            sSQL += " like " + StringUtil.QuotedToDBStr(sTableName);
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            if (ds.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        public static string[] GetTableNames(string sTableName)
        {
            string sDatabaseName = "";

            int nPosDot = sTableName.IndexOf('.');
            if (nPosDot != -1)
            {
                sDatabaseName = sTableName.Substring(0, nPosDot);
                sTableName = sTableName.Substring(nPosDot + 1);
            }

            string sSQL = "show tables";
            if (sDatabaseName != "")
                sSQL += " from " + sDatabaseName;
            sSQL += " like " + StringUtil.QuotedToDBStr(sTableName);
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            ArrayList lstTables = new ArrayList();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string sMatchName = ConvertUtil.ToString(ds.Tables[0].Rows[i][0]);
                lstTables.Add(sMatchName);
            }

            string[] arrRet = ArrayUtil.ToStringArray(lstTables);
            return arrRet;
        }
    }
}

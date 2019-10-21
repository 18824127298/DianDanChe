using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

using Sigbit.Common;

namespace Sigbit.Framework.SupervisorTool.DataAdmin
{
    public class ExecuteMultiSQL
    {
        public void ExecuteSQLString(string sSQLString)
        {
            string[] arrSQLs = ExtractSQLList(sSQLString);
        }

        /// <summary>
        /// 将一组的SQL语句串解析出分立的SQL语句
        /// </summary>
        /// <param name="sSQLString">SQL语句串</param>
        /// <returns>分立的SQL语句</returns>
        private string[] ExtractSQLList(string sSQLString)
        {
            //========= 1. 分析SQL语句到多行 ==========
            sSQLString = sSQLString.Replace("\r", "");
            string[] arrLines = sSQLString.Split('\n');

            //========== 2. 分检出每一个SQL语句 =========
            ArrayList arrAllSQLs = new ArrayList();

            string sCurrentSQL = "";
            foreach (string sLine in arrLines)
            {
                sCurrentSQL += sLine + "\r\n";

                //========== (1) 如果一行以分号结束，且为SQL语句中有偶数个单引号，则表示本语句结束 ====
                if (sLine.TrimEnd().EndsWith(";"))
                {
                    if (StringUtil.Occurs(sCurrentSQL, "'") % 2 == 0)
                    {
                        arrAllSQLs.Add(sCurrentSQL);
                        sCurrentSQL = "";
                    }
                }
            }
            if (sCurrentSQL.Trim() != "")
            {
                arrAllSQLs.Add(sCurrentSQL);
                sCurrentSQL = "";
            }

            //============ 3. 去掉每一个语句末尾的分号 =============
            for (int i = 0; i < arrAllSQLs.Count; i++)
            {
                string sSingleSQL = (string)arrAllSQLs[i];
                if (sSingleSQL.TrimEnd().EndsWith(";"))
                {
                    sSingleSQL = sSingleSQL.TrimEnd().TrimEnd(';');
                    arrAllSQLs[i] = sSingleSQL;
                }
            }

            //============ 4. 转换到字符串数组 ==========
            string[] arrRet = ArrayUtil.ToStringArray(arrAllSQLs);
            return arrRet;
        }
    }
}

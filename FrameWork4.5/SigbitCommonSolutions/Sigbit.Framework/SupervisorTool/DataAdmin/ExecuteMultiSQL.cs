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
        /// ��һ���SQL��䴮������������SQL���
        /// </summary>
        /// <param name="sSQLString">SQL��䴮</param>
        /// <returns>������SQL���</returns>
        private string[] ExtractSQLList(string sSQLString)
        {
            //========= 1. ����SQL��䵽���� ==========
            sSQLString = sSQLString.Replace("\r", "");
            string[] arrLines = sSQLString.Split('\n');

            //========== 2. �ּ��ÿһ��SQL��� =========
            ArrayList arrAllSQLs = new ArrayList();

            string sCurrentSQL = "";
            foreach (string sLine in arrLines)
            {
                sCurrentSQL += sLine + "\r\n";

                //========== (1) ���һ���ԷֺŽ�������ΪSQL�������ż���������ţ����ʾ�������� ====
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

            //============ 3. ȥ��ÿһ�����ĩβ�ķֺ� =============
            for (int i = 0; i < arrAllSQLs.Count; i++)
            {
                string sSingleSQL = (string)arrAllSQLs[i];
                if (sSingleSQL.TrimEnd().EndsWith(";"))
                {
                    sSingleSQL = sSingleSQL.TrimEnd().TrimEnd(';');
                    arrAllSQLs[i] = sSingleSQL;
                }
            }

            //============ 4. ת�����ַ������� ==========
            string[] arrRet = ArrayUtil.ToStringArray(arrAllSQLs);
            return arrRet;
        }
    }
}

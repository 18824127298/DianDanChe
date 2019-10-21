using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Data;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Web
{
    /// <summary>
    /// �����
    /// </summary>
    public class CodeTable : CodeTableBase
    {
        /// <summary>
        /// ���ݴ����SQL���������
        /// </summary>
        /// <param name="sSelect">SQL��ѯ���</param>
        public void FillBySQL(string sSelect)
        {
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSelect);
            FillByDataSet(ds);
        }

        /// <summary>
        /// ���ݴ����DataSet���
        /// </summary>
        /// <param name="ds">DataSet</param>
        public void FillByDataSet(DataSet ds)
        {
            DataRowCollection rows = ds.Tables[0].Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                string sCode = ConvertUtil.ToString(rows[i][0]);
                string sDes = ConvertUtil.ToString(rows[i][1]);
                this.AddItem(sCode, sDes);
            }
        }
    }
}

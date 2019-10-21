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
    /// 代码表
    /// </summary>
    public class CodeTable : CodeTableBase
    {
        /// <summary>
        /// 根据传入的SQL语句进行填充
        /// </summary>
        /// <param name="sSelect">SQL查询语句</param>
        public void FillBySQL(string sSelect)
        {
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSelect);
            FillByDataSet(ds);
        }

        /// <summary>
        /// 根据传入的DataSet填充
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

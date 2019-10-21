using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using Sigbit.Common;
//using Sigbit.Net.BIPPacket;

namespace Sigbit.Data.RomitSQL
{
    public class ROMMSQLResult : ROMMBase
    {
        private DataSet _resultDataSet = null;
        /// <summary>
        /// 结果集
        /// </summary>
        public DataSet ResultDataSet
        {
            get { return _resultDataSet; }
            set { _resultDataSet = value; }
        }

        private int _resultAffectedRowsCount = 0;
        /// <summary>
        /// 结果影响行数
        /// </summary>
        public int ResultAffectedRowsCount
        {
            get { return _resultAffectedRowsCount; }
            set { _resultAffectedRowsCount = value; }
        }

        private string _resultScalarResult = "";
        /// <summary>
        /// ExecuteScalar运行的结果
        /// </summary>
        public string ResultScalarResult
        {
            get { return _resultScalarResult; }
            set { _resultScalarResult = value; }
        }

        private string _exceptionString = "";
        /// <summary>
        /// 例外的表示串
        /// </summary>
        public string ExceptionString
        {
            get { return _exceptionString; }
            set { _exceptionString = value; }
        }

        protected override void SynchronizeFromProperties()
        {
            base.SynchronizeFromProperties();

            //======== 1. 结果集 ========
            if (this.ResultDataSet != null)
            {
                DataTable table = this.ResultDataSet.Tables[0];
                int nColumnCount = table.Columns.Count;
                int nRowCount = table.Rows.Count;

                //========= 1.1 列名 =============
                for (int i = 0; i < nColumnCount; i++)
                {
                    string sColumnName = table.Columns[i].ColumnName;
                    this.AddField(sColumnName);
                }

                //========= 1.2 各条记录 =========
                for (int i = 0; i < nRowCount; i++)
                {
                    for (int j = 0; j < nColumnCount; j++)
                    {
                        string sValueString = table.Rows[i][j].ToString();
                        this.SetItemString(i + 1, j, sValueString);
                    }
                }
            }

            //========= 2. 影响行数 =========
            if (this.ResultAffectedRowsCount != 0)
                AddAStringValue("result_rows_affected", this.ResultAffectedRowsCount.ToString());

            //========= 3. ExecuteScalar的运行结果 =======
            if (this.ResultScalarResult != "")
                AddAStringValue("result_scalar_object", this.ResultScalarResult);

            //======== 4. 例外的表示串 =======
            if (this.ExceptionString != "")
                AddAStringValue("exception_string", this.ExceptionString);
        }

        protected override void SynchronizeToProperties()
        {
            base.SynchronizeToProperties();

            //========= 1. 结果集 ============
            if (this.GetFieldCount() != 0)
            {
                this.ResultDataSet = new DataSet();
                this.ResultDataSet.Tables.Add();
                DataTable table = this.ResultDataSet.Tables[0];

                int nFieldCount = this.GetFieldCount();
                int nRecordCount = this.GetRecordCount();

                //=========== 1.1 字段名 ================
                for (int i = 0; i < nFieldCount; i++)
                {
                    string sFieldName = this.GetDataSet().GetFieldName(i);
                    table.Columns.Add(sFieldName);
                }

                //============ 1.2 各条记录 ============
                for (int i = 0; i < nRecordCount; i++)
                {
                    string[] arrRecordValues = new string[nFieldCount];

                    for (int j = 0; j < nFieldCount; j++)
                    {
                        string sItemValue = this.GetItemString(i + 1, j);
                        arrRecordValues[j] = sItemValue;
                    }

                    table.Rows.Add(arrRecordValues);
                }
            }

            //======== 2. 影响行数 =========
            this.ResultAffectedRowsCount
                    = ConvertUtil.ToInt(this.GetAStringValueDefault("result_rows_affected", "0"));

            //========= 3. ExecuteScalar的运行结果 =======
            this.ResultScalarResult = this.GetAStringValueDefault("result_scalar_object", "");

            //======== 4. 例外的表示串 =======
            this.ExceptionString = this.GetAStringValueDefault("exception_string", "");
        }

        //public BIPPacket ToBIPPacket()
        //{
        //    BIPPacket bipPacket = new BIPPacket();

        //    //======== 1. 结果集 ========
        //    if (this.ResultDataSet != null)
        //    {
        //        DataTable table = this.ResultDataSet.Tables[0];
        //        int nColumnCount = table.Columns.Count;
        //        int nRowCount = table.Rows.Count;

        //        //========= 1.1 列名 =============
        //        for (int i = 0; i < nColumnCount; i++)
        //        {
        //            string sColumnName = table.Columns[i].ColumnName;
        //            bipPacket.AddField(sColumnName);
        //        }

        //        //========= 1.2 各条记录 =========
        //        for (int i = 0; i < nRowCount; i++)
        //        {
        //            for (int j = 0; j < nColumnCount; j++)
        //            {
        //                string sValueString = table.Rows[i][j].ToString();
        //                bipPacket.SetItemString(i + 1, j, sValueString);
        //            }
        //        }
        //    }

        //    //========= 2. 影响行数 =========
        //    if (this.ResultAffectedRowsCount != 0)
        //        bipPacket.AddAStringValue("result_rows_affected", this.ResultAffectedRowsCount.ToString());

        //    //========= 3. ExecuteScalar的运行结果 =======
        //    if (this.ResultScalarResult != "")
        //        bipPacket.AddAStringValue("result_scalar_object", this.ResultScalarResult);

        //    //======== 4. 例外的表示串 =======
        //    if (this.ExceptionString != "")
        //        bipPacket.AddAStringValue("exception_string", this.ExceptionString);

        //    return bipPacket;
        //}
    }
}

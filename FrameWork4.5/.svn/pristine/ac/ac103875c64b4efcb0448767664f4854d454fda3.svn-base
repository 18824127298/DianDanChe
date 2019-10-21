using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Sigbit.Data;
using System.Collections;

namespace Sigbit.Framework.TiiChart
{
    /// <summary>
    /// 数据源类
    /// </summary>
    public class TiiDataSource
    {
        private DataSet _dataSet = null;
        /// <summary>
        /// 数据集
        /// </summary>
        public DataSet DataSet
        {
            get { return _dataSet; }
            set { _dataSet = value; }
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet TiiChart__GetDataSet()
        {
            if (_dataSet == null)
            {
                if (SQLStatement != "")
                {
                    _dataSet = DataHelper.Instance.ExecuteDataSet(SQLStatement);
                }
                else
                {
                    if (TableName != "")
                    {
                        SQLStatement = "select * from " + TableName;
                        _dataSet = DataHelper.Instance.ExecuteDataSet(SQLStatement);
                    }
                }
            }
            return _dataSet;
        }

        private string _sQLStatement = "";
        /// <summary>
        /// SQL语句
        /// </summary>
        public string SQLStatement
        {
            get { return _sQLStatement; }
            set { _sQLStatement = value; }
        }

        private string _tableName = "";
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        private string _nameField = "";
        /// <summary>
        /// 名称字段，默认为第一个字段
        /// </summary>
        public string NameField
        {
            get { return _nameField; }
            set { _nameField = value; }
        }

        /// <summary>
        /// 设置名称字段
        /// </summary>
        /// <param name="sFiledName">字段名称</param>
        public void SetNameField(string sFiledName)
        {
            _nameField = sFiledName;
        }

        private ArrayList _valueFieldList = new ArrayList();
        /// <summary>
        /// 数值字段，默认为第二个字段
        /// </summary>
        public ArrayList ValueFieldList
        {
            get { return _valueFieldList; }
            set { _valueFieldList = value; }
        }

        private ArrayList _captionList = new ArrayList();
        /// <summary>
        /// 图表标题
        /// </summary>
        public ArrayList CaptionList
        {
            get { return _captionList; }
            set { _captionList = value; }
        }

        /// <summary>
        /// 设置或增加数值字段，第一次为设置，第二次及以后为增加
        /// </summary>
        /// <param name="sFieldValue">字段名称</param>
        public void AddValueField(string sFieldValue)
        {
            AddValueField(sFieldValue, "");
        }

        /// <summary>
        /// 设置或增加数值字段，默认第二个字段为数值字段，第一次为设置，第二次及以后为增加
        /// </summary>
        /// <param name="sFieldValue">字段名称</param>
        /// <param name="sCaption">图表标题</param>
        public void AddValueField(string sFieldValue, string sCaption)
        {
            if (!ValueFieldList.Contains(sFieldValue))
            {
                ValueFieldList.Add(sFieldValue);
                CaptionList.Add(sCaption);
            }
        }
    }
}

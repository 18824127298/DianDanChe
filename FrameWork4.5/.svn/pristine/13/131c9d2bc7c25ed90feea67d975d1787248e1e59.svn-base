using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace Sigbit.Common
{
    /// <summary>
    /// 排序的顺序
    /// </summary>
    enum SQLSortDirection
    {
        Asc,
        Desc
    }

    /// <summary>
    /// 一个排序字段
    /// </summary>
    class SQLSortField
    {
        private string _fieldName = "";
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        private SQLSortDirection _direction;
        /// <summary>
        /// 排序的方式
        /// </summary>
        public SQLSortDirection Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
    }

    /// <summary>
    /// 排序字段列表
    /// </summary>
    class SQLSortFields : ArrayList
    {
        /// <summary>
        /// 增加一个排序字段
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        public void AddSortField(string sFieldName)
        {
            AddSortField(sFieldName, SQLSortDirection.Asc);
        }

        /// <summary>
        /// 增加一个排序字段
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="direction">方向</param>
        public void AddSortField(string sFieldName, SQLSortDirection direction)
        {
            SQLSortField sortField = new SQLSortField();
            sortField.FieldName = sFieldName;
            sortField.Direction = direction;
            Add(sortField);
        }

        public SQLSortField GetSortField(int nIndex)
        {
            return (SQLSortField)this[nIndex];
        }

        /// <summary>
        /// 将排序字段压入到原排序字段中生成新的排序字段列表
        /// </summary>
        /// <param name="sPushFieldName">字段名</param>
        /// <param name="nMaxSortFieldCount">最多的排序字段数</param>
        /// <remarks>原来如果有该字段，则排序顺序倒过来，如没有则增加</remarks>
        public void PushSortField(string sPushFieldName, int nMaxSortFieldCount)
        {
            if (sPushFieldName == "" || nMaxSortFieldCount <= 0)
                return;
            sPushFieldName = sPushFieldName.ToLower();

            //========= 1. 逐一处理原有的排序字段，判断字段相同的情况 =========
            bool bFoundPushField = false;

            for (int i = 0; i < Count; i++)
            {
                SQLSortField sortField = GetSortField(i);

                //======= 1.1 如果原有的排序字段与新增的字段相同 ====
                if (sortField.FieldName == sPushFieldName)
                {
                    bFoundPushField = true;

                    SQLSortField firstSortField = new SQLSortField();
                    firstSortField.FieldName = sPushFieldName;

                    //======= 1.2 则升序改降序，降序改升序 =========
                    if (sortField.Direction == SQLSortDirection.Asc)
                        firstSortField.Direction = SQLSortDirection.Desc;
                    else
                        firstSortField.Direction = SQLSortDirection.Asc;

                    //======== 1.3 移掉原有的排序字段，并将新的加入到最前 ======
                    this.RemoveAt(i);
                    this.Insert(0, firstSortField);
                    break;
                }
            }

            //========= 2. 如果新增的字段与原排序字段都不相同，则新增字段 ======
            if (!bFoundPushField)
            {
                SQLSortField firstSortField = new SQLSortField();
                firstSortField.FieldName = sPushFieldName;
                firstSortField.Direction = SQLSortDirection.Asc;
                this.Insert(0, firstSortField);
            }

            //========= 3. 删掉多出的字段 =========
            for (int i = Count - 1; i >= nMaxSortFieldCount; i--)
            {
                this.RemoveAt(i);
            }
        }

        /// <summary>
        /// 将排序字段压入到原排序字段中生成新的排序字段列表
        /// </summary>
        /// <param name="sPushFieldName">字段名</param>
        /// <remarks>缺省的最多排序字段数限制为3</remarks>
        public void PushSortField(string sPushFieldName)
        {
            PushSortField(sPushFieldName, 3);
        }

        /// <summary>
        /// 得到SQL语句的order by部分
        /// </summary>
        /// <returns>SQL语句的order by部分</returns>
        public override string ToString()
        {
            string sRet = "";
            for (int i = 0; i < Count; i++)
            {
                SQLSortField sortField = this.GetSortField(i);
                sRet += sortField.FieldName;

                if (sortField.Direction == SQLSortDirection.Desc)
                    sRet += " desc";

                if (i != Count - 1)
                    sRet += ", ";
            }

            return sRet;
        }
    }

    /// <summary>
    /// 提供简化构造SQL语句的方便途径
    /// </summary>
    public class SQLBuilder
    {
        #region 基本SQL语句
        private string _nonConditionSql = "";
        /// <summary>
        /// 没有任何限定条件的SQL语句，一般的格式为"select xxx, yyy from TTT"
        /// </summary>
        public string NonConditionSql
        {
            get { return _nonConditionSql; }
            set { _nonConditionSql = value; }
        }
        #endregion 基本SQL语句

        #region 排序
        private SQLSortFields _sqlSortFields = new SQLSortFields();
        /// <summary>
        /// 将排序字段压入到原排序字段中生成新的排序字段列表
        /// </summary>
        /// <param name="sPushFieldName">字段名</param>
        /// <param name="nMaxSortFieldCount">最多的排序字段数</param>
        /// <remarks>原来如果有该字段，则排序顺序倒过来，如没有则增加</remarks>
        public void PushSortField(string sPushFieldName, int nMaxSortFieldCount)
        {
            _sqlSortFields.PushSortField(sPushFieldName, nMaxSortFieldCount);
        }

        /// <summary>
        /// 将排序字段压入到原排序字段中生成新的排序字段列表
        /// </summary>
        /// <param name="sPushFieldName">字段名</param>
        /// <remarks>缺省的最多排序字段数限制为3</remarks>
        public void PushSortField(string sPushFieldName)
        {
            _sqlSortFields.PushSortField(sPushFieldName);
        }
        #endregion 排序

        #region 固定条件
        SQLConditionList _fixConditionList = new SQLConditionList();
        /// <summary>
        /// 增加固定的SQL条件
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="sFieldChineseName">字段中文名</param>
        /// <param name="objFieldValue">字段值</param>
        /// <param name="op">操作符</param>
        /// <param name="sDescription">描述</param>
        /// <param name="fieldType">字段类型</param>
        public void AddFixCondition(string sFieldName, string sFieldChineseName,
                object objFieldValue, SQLConditionOperator op,
                string sDescription, SQLConditionFieldType fieldType)
        {
            _fixConditionList.AddEntry(sFieldName, sFieldChineseName,
                    objFieldValue, op, sDescription, fieldType);
        }

        /// <summary>
        /// 增加固定的SQL条件
        /// </summary>
        public void AddFixCondition(string sFieldName, string sFieldChineseName,
                object objFieldValue, SQLConditionOperator op,
                string sDescription)
        {
            _fixConditionList.AddEntry(sFieldName, sFieldChineseName,
                    objFieldValue, op, sDescription);
        }

        /// <summary>
        /// 增加固定的SQL条件
        /// </summary>
        public void AddFixCondition(string sFieldName, string sFieldChineseName,
                object objFieldValue, SQLConditionOperator op)
        {
            _fixConditionList.AddEntry(sFieldName, sFieldChineseName,
                    objFieldValue, op);
        }

        /// <summary>
        /// 增加固定的SQL条件
        /// </summary>
        public void AddFixCondition(string sFieldName, string sFieldChineseName,
                object objFieldValue)
        {
            _fixConditionList.AddEntry(sFieldName,
                    sFieldChineseName, objFieldValue);
        }

        /// <summary>
        /// 增加固定的SQL条件
        /// </summary>
        public void AddFixCondition(string sFieldName, string sFieldChineseName)
        {
            _fixConditionList.AddEntry(sFieldName, sFieldChineseName);
        }

        /// <summary>
        /// 增加固定的SQL条件
        /// </summary>
        public void AddFixCondition(string sFieldName)
        {
            _fixConditionList.AddEntry(sFieldName);
        }

        /// <summary>
        /// 清空固定的SQL条件
        /// </summary>
        public void ClearFixConditions()
        {
            _fixConditionList.Clear();
        }

        /// <summary>
        /// 增加固定的SQL条件
        /// </summary>
        public void RemoveFixCondition(string sFieldName)
        {
            _fixConditionList.RemoveEntry(sFieldName);
        }
        #endregion 固定条件

        #region 可变条件
        SQLConditionList _conditionList = new SQLConditionList();
        /// <summary>
        /// 增加可变的SQL条件
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="sFieldChineseName">字段中文名</param>
        /// <param name="objFieldValue">字段值</param>
        /// <param name="op">操作符</param>
        /// <param name="sDescription">描述</param>
        /// <param name="fieldType">字段类型</param>
        public void AddCondition(string sFieldName, string sFieldChineseName,
                object objFieldValue, SQLConditionOperator op,
                string sDescription, SQLConditionFieldType fieldType)
        {
            _conditionList.AddEntry(sFieldName, sFieldChineseName,
                    objFieldValue, op, sDescription, fieldType);
        }

        /// <summary>
        /// 增加可变的SQL条件
        /// </summary>
        public void AddCondition(string sFieldName, string sFieldChineseName,
                object objFieldValue, SQLConditionOperator op,
                string sDescription)
        {
            _conditionList.AddEntry(sFieldName, sFieldChineseName,
                    objFieldValue, op, sDescription);
        }

        /// <summary>
        /// 增加可变的SQL条件
        /// </summary>
        public void AddCondition(string sFieldName, string sFieldChineseName,
                object objFieldValue, SQLConditionOperator op)
        {
            _conditionList.AddEntry(sFieldName, sFieldChineseName,
                    objFieldValue, op);
        }

        /// <summary>
        /// 增加可变的SQL条件
        /// </summary>
        public void AddCondition(string sFieldName, string sFieldChineseName,
                object objFieldValue)
        {
            _conditionList.AddEntry(sFieldName,
                    sFieldChineseName, objFieldValue);
        }

        /// <summary>
        /// 增加可变的SQL条件
        /// </summary>
        public void AddCondition(string sFieldName, string sFieldChineseName)
        {
            _conditionList.AddEntry(sFieldName, sFieldChineseName);
        }

        /// <summary>
        /// 增加可变的SQL条件
        /// </summary>
        public void AddCondition(string sFieldName)
        {
            _conditionList.AddEntry(sFieldName);
        }

        /// <summary>
        /// 清空条件
        /// </summary>
        public void ClearConditions()
        {
            _conditionList.Clear();
        }

        /// <summary>
        /// 移除条件
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="op">操作符</param>
        public void RemoveCondition(string sFieldName, SQLConditionOperator op)
        {
            _conditionList.RemoveEntry(sFieldName, op);
        }

        /// <summary>
        /// 移除条件
        /// </summary>
        public void RemoveCondition(string sFieldName)
        {
            _conditionList.RemoveEntry(sFieldName);
        }

        /// <summary>
        /// 取出条件的值
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="op">操作符</param>
        /// <returns>取出的值</returns>
        public object GetConditionValue(string sFieldName, SQLConditionOperator op)
        {
            return _conditionList.GetConditionValue(sFieldName, op);
        }

        /// <summary>
        /// 取出条件的值
        /// </summary>
        public object GetConditionValue(string sFieldName)
        {
            return _conditionList.GetConditionValue(sFieldName);
        }

        /// <summary>
        /// 取出条件的字符串值
        /// </summary>
        public string GetConditionValueString(string sFieldName, SQLConditionOperator op)
        {
            return _conditionList.GetConditionValueString(sFieldName, op);
        }

        /// <summary>
        /// 取出条件的字符串值
        /// </summary>
        public string GetConditionValueString(string sFieldName)
        {
            return _conditionList.GetConditionValueString(sFieldName);
        }

        /// <summary>
        /// 取出条件的整型值
        /// </summary>
        public int GetConditionValueInt(string sFieldName, SQLConditionOperator op)
        {
            return _conditionList.GetConditionValueInt(sFieldName, op);
        }

        /// <summary>
        /// 取出条件的整型值
        /// </summary>
        public int GetConditionValueInt(string sFieldName)
        {
            return _conditionList.GetConditionValueInt(sFieldName);
        }

        /// <summary>
        /// 取出搜索条件的描述
        /// </summary>
        /// <returns>搜索条件的描述</returns>
        public string GetConditionDescription()
        {
            return _conditionList.GetDescription();
        }

        /// <summary>
        /// 得到条件入口的数量
        /// </summary>
        /// <returns>条件入口的数量</returns>
        public int GetConditionCount()
        {
            return _conditionList.Count;
        }

        #endregion 可变条件

        #region 输出
        /// <summary>
        /// 得到SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        public override string ToString()
        {
            //======== 1. 不带条件的SQL语句 ==========
            string sRet = NonConditionSql;

            //=======2. 限定条件 ==========
            string sFixCondition = _fixConditionList.ToString();
            string sVariableCondition = _conditionList.ToString();

            string sAllCondition;
            if (sFixCondition != "" && sVariableCondition != "")
                sAllCondition = sFixCondition + " and " + sVariableCondition;
            else
                sAllCondition = sFixCondition + sVariableCondition;
            if (sAllCondition != "")
            {
                if (sRet.IndexOf(" where ") >= 0)
                    sRet += "\n and " + sAllCondition;
                else
                    sRet += "\n where " + sAllCondition;
            }

            //========= 3. 排序 ========
            string sSortFields = _sqlSortFields.ToString();
            if (sSortFields != "")
                sRet += "\n order by " + sSortFields;

            return sRet;
        }
        #endregion 输出
    }
}

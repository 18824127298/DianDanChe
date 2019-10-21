using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using Sigbit.Common;

namespace Sigbit.Common
{
    #region 枚举SQLConditionFieldType
    /// <summary>
    /// 条件字段的类型
    /// </summary>
    public enum SQLConditionFieldType
    {
        /// <summary>
        /// 字符串类型
        /// </summary>
        String,
        /// <summary>
        /// 布尔型
        /// </summary>
        Boolean,
        /// <summary>
        /// 整型
        /// </summary>
        Int,
        /// <summary>
        /// 浮点型
        /// </summary>
        Float,
        /// <summary>
        /// 时间类型
        /// </summary>
        DateTime
    }
    #endregion 枚举SQLConditionFieldType

    #region 枚举SQLConditionOperator
    /// <summary>
    /// 条件表达式的操作符定义
    /// </summary>
    public enum SQLConditionOperator
    {
        /// <summary>
        /// 等于，=
        /// </summary>
        Equal,	   
        /// <summary>
        /// 不等于，&lt;&gt;
        /// </summary>
        NotEqual,	
        /// <summary>
        /// 大于，&gt;
        /// </summary>
        GreaterThan,	
        /// <summary>
        /// 大于等于，&gt;=
        /// </summary>
        GreaterEqualThan,
        /// <summary>
        /// 小于，&lt;
        /// </summary>
        LessThan,
        /// <summary>
        /// 小于等于，&lt;=
        /// </summary>
        LessEqualThan,
        /// <summary>
        /// 相似，like
        /// </summary>
        Like,
        /// <summary>
        /// 相似,like "%x"
        /// </summary>
        LikeLeft,
        /// <summary>
        /// 相似，like "x%"
        /// </summary>
        LikeRight,
        /// <summary>
        /// 不相似，not (like)
        /// </summary>
        NotLike,		    
        /// <summary>
        /// 在xxx内
        /// </summary>
        In,		   
        /// <summary>
        /// 不在xxx内
        /// </summary>
        NotIn,	
        /// <summary>
        /// 用户定义的其它条件限定
        /// </summary>
        UserDefined,
        /// <summary>
        /// 系统操作，读取、移除任何一个字段
        /// </summary>
        SYSAny
    }
    #endregion 枚举SQLConditionOperator

    #region 类SQLConditionEntry
    /// <summary>
    /// 条件入口类
    /// </summary>
    class SQLConditionEntry
    {
        #region 属性
        private string _fieldName = "";
        /// <summary>
        /// 实际数据字段
        /// </summary>
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        private string _fieldChinsesName = "";
        /// <summary>
        /// 字段中文名
        /// </summary>
        public string FieldChinsesName
        {
            get { return _fieldChinsesName; }
            set { _fieldChinsesName = value; }
        }

        private SQLConditionFieldType _fieldType;	   
        /// <summary>
        /// 字段类型
        /// </summary>
        public SQLConditionFieldType FieldType
        {
            get { return _fieldType; }
            set { _fieldType = value; }
        }

        private SQLConditionOperator _operator;
        /// <summary>
        /// 操作符
        /// </summary>
        public SQLConditionOperator Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        private object _fieldValue;
        /// <summary>
        /// 值
        /// </summary>
        public object FieldValue
        {
            get { return _fieldValue; }
            set { _fieldValue = value; }
        }

        private string _description = "";
        /// <summary>
        /// 条件描述
        /// </summary>
        public string Description
        {
            get 
            { 
                //===== 1. 如果有描述，则直接返回 ======
                if (_description != "")
                    return _description;

                //====== 2. 得到字段的描述 =========
                string sFieldChineseName;
                if (_fieldChinsesName == "")
                    sFieldChineseName = _fieldName;
                else
                    sFieldChineseName = _fieldChinsesName;

                //======== 3. 得到操作符的描述 =====
                string sOpDesc = GetDescriptionOfOperator(Operator);

                //======== 4. 得到值的描述 ========
                string sValueDesc = "“" + FieldValue.ToString() + "”";

                //======== 5. 返回值 =============
                _description = sFieldChineseName + sOpDesc + sValueDesc;
                return _description;
            }
            set { _description = value; }
        }
        #endregion 属性

        #region 支持
        private string GetDescriptionOfOperator(SQLConditionOperator op)
        {
            switch (op)
            {
                case SQLConditionOperator.Equal:
                    return "为";
                case SQLConditionOperator.In:
                    return "属于";
                case SQLConditionOperator.LessEqualThan:
                    return "小于";
                case SQLConditionOperator.LessThan:
                    return "小于";
                case SQLConditionOperator.Like:
                    return "含";
                case SQLConditionOperator.LikeLeft:
                    return "后缀为";
                case SQLConditionOperator.LikeRight:
                    return "前缀为";
                case SQLConditionOperator.GreaterEqualThan:
                    return "大于";
                case SQLConditionOperator.GreaterThan:
                    return "大于";
                case SQLConditionOperator.NotEqual:
                    return "不是";
                case SQLConditionOperator.NotIn:
                    return "不属于";
                case SQLConditionOperator.NotLike:
                    return "不含";
                case SQLConditionOperator.UserDefined:
                    return "";
                default:
                    return "";
            }
        }
        #endregion 支持

        #region 构造函数
        private void InitBy(string sFieldName, string sFieldChineseName,
                object objFieldValue, SQLConditionOperator op,
                string sDescription, SQLConditionFieldType fieldType)
        {
            FieldName = sFieldName;
            FieldChinsesName = sFieldChineseName;
            FieldValue = objFieldValue;
            Operator = op;
            Description = sDescription;
            FieldType = fieldType;
        }

        private void InitBy(string sFieldName, string sFieldChineseName,
                object objFieldValue, SQLConditionOperator op,
                string sDescription)
        {
            InitBy(sFieldName, sFieldChineseName, objFieldValue,
                    op, sDescription, GetConditionFieldType(objFieldValue));
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="sFieldChineseName">字段中文名</param>
        /// <param name="objFieldValue">值</param>
        /// <param name="op">操作符</param>
        /// <param name="sDescription">描述</param>
        /// <param name="fieldType">字段类型</param>
        public SQLConditionEntry(string sFieldName, string sFieldChineseName,
                object objFieldValue, SQLConditionOperator op,
                string sDescription, SQLConditionFieldType fieldType)
        {
            InitBy(sFieldName, sFieldChineseName, objFieldValue, op, sDescription, fieldType);
        }

        public SQLConditionEntry(string sFieldName, string sFieldChineseName,
                object objFieldValue, SQLConditionOperator op,
                string sDescription)
        {
            InitBy(sFieldName, sFieldChineseName, objFieldValue, op, sDescription);
        }

        public SQLConditionEntry(string sFieldName, string sFieldChineseName,
                object objFieldValue, SQLConditionOperator op)
        {
            InitBy(sFieldName, sFieldChineseName, objFieldValue, op, "");
        }

        public SQLConditionEntry(string sFieldName, string sFieldChineseName,
                object objFieldValue)
        {
            InitBy(sFieldName, sFieldChineseName, objFieldValue, SQLConditionOperator.Equal, "");
        }

        public SQLConditionEntry(string sFieldName, string sFieldChineseName)
        {
            InitBy(sFieldName, sFieldChineseName, "", SQLConditionOperator.Equal, "");
        }

        public SQLConditionEntry(string sFieldName)
        {
            InitBy(sFieldName, "", "", SQLConditionOperator.Equal, "");
        }

        public SQLConditionEntry()
        {
            InitBy("", "", "", SQLConditionOperator.Equal, "");
        }

        /// <summary>
        /// 通过传入变量值，通过该变量的类型，得到数据库字段的类型
        /// </summary>
        /// <param name="oValue">变量对象</param>
        /// <returns>相关数据库字段的类型</returns>
        public SQLConditionFieldType GetConditionFieldType(object oValue)
        {
            System.Type t = oValue.GetType();

            if (t.Equals(typeof(System.Int16))
                || t.Equals(typeof(System.Int32))
                || t.Equals(typeof(System.Int64)))
            {
                return SQLConditionFieldType.Int;          // Int16, Int32, Int64 ==>> Int
            }
            else if (t.Equals(typeof(System.String)))
            {
                return SQLConditionFieldType.String;       // String ==>> String
            }
            else if (t.Equals(typeof(System.Boolean)))
            {
                return SQLConditionFieldType.Boolean;      // Boolean ==>> Boolean
            }
            else if (t.Equals(typeof(System.DateTime)))
            {
                return SQLConditionFieldType.DateTime;     // DateTime ==>> DateTime
            }
            else if (t.Equals(typeof(System.Decimal)))
            {
                return SQLConditionFieldType.Float;         // Decimal ==>> Float
            }
            else if (t.Equals(typeof(System.Double)))
            {
                return SQLConditionFieldType.Float;         // Decimal ==>> Float
            }
            else if (t.Equals(typeof(System.Single)))
            {
                return SQLConditionFieldType.Float;         // Single ==>> Float
            }
            else
            {
                return SQLConditionFieldType.String;       // 其它 ==>> String
            }
        }
        #endregion 构造函数

        #region 输出
        private string AddLikePercent(string sValue)
        {
            if (sValue.IndexOf("%") == -1)
                return "%" + sValue + "%";
            else
                return sValue;
        }

        private string AddLikeLeftPercent(string sValue)
        {
            if (sValue.IndexOf("%") == -1)
                return "%" + sValue;
            else
                return sValue;
        }

        private string AddLikeRightPercent(string sValue)
        {
            if (sValue.IndexOf("%") == -1)
                return sValue + "%";
            else
                return sValue;
        }

        /// <summary>
        /// 得到查询条件的SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        public override string ToString()
        {
            if (FieldValue == null)
                FieldValue = "";

            switch (FieldType)
            {
                case SQLConditionFieldType.String:
                    switch (Operator)
                    {
                        case SQLConditionOperator.Equal:
                            return FieldName + " = "
                                    + StringUtil.QuotedToDBStr(FieldValue.ToString());
                        case SQLConditionOperator.In:
                            return FieldName + " in (" + FieldValue.ToString() + ")";
                        case SQLConditionOperator.LessEqualThan:
                            return FieldName + " <= "
                                    + StringUtil.QuotedToDBStr(FieldValue.ToString());
                        case SQLConditionOperator.LessThan:
                            return FieldName + " < "
                                    + StringUtil.QuotedToDBStr(FieldValue.ToString());
                        case SQLConditionOperator.Like:
                            return FieldName + " like "
                                    + StringUtil.QuotedToDBStr(AddLikePercent(FieldValue.ToString()));
                        case SQLConditionOperator.LikeLeft:
                            return FieldName + " like "
                                    + StringUtil.QuotedToDBStr(AddLikeLeftPercent(FieldValue.ToString()));
                        case SQLConditionOperator.LikeRight:
                            return FieldName + " like "
                                    + StringUtil.QuotedToDBStr(AddLikeRightPercent(FieldValue.ToString()));
                        case SQLConditionOperator.GreaterEqualThan:
                            return FieldName + " >= "
                                    + StringUtil.QuotedToDBStr(FieldValue.ToString());
                        case SQLConditionOperator.GreaterThan:
                            return FieldName + " > "
                                    + StringUtil.QuotedToDBStr(FieldValue.ToString());
                        case SQLConditionOperator.NotEqual:
                            return FieldName + " <> "
                                    + StringUtil.QuotedToDBStr(FieldValue.ToString());
                        case SQLConditionOperator.NotIn:
                            return FieldName + " not in (" + FieldValue.ToString() + ")";
                        case SQLConditionOperator.NotLike:
                            return FieldName + " not like "
                                    + StringUtil.QuotedToDBStr(AddLikePercent(FieldValue.ToString()));
                        case SQLConditionOperator.UserDefined:
                            return FieldName + " " + FieldValue.ToString();
                        default:
                            throw new Exception("Sigbit.Common.ConditionEntry.ToString() Error:"
                                    + "Unexpected String Operator.");
                    }
                case SQLConditionFieldType.Boolean:
                    bool v = Boolean.Parse(FieldValue.ToString());
                    string flag = v ? "Y" : "N";

                    switch (Operator)
                    {
                        case SQLConditionOperator.Equal:
                            return FieldName + " = " + StringUtil.QuotedToDBStr(flag);
                        case SQLConditionOperator.NotEqual:
                            return FieldName + " <> " + StringUtil.QuotedToDBStr(flag);
                        default:
                            throw new Exception("Sigbit.Common.ConditionEntry.ToString() Error:"
                                    + "Unexpected Boolean Operator.");
                    }
                case SQLConditionFieldType.DateTime:
                    DateTime dt0;
                    dt0 = DateTime.Parse(FieldValue.ToString());
                    string sDT = DateTimeUtil.ToDateTimeStr(dt0);
                    sDT = StringUtil.QuotedToDBStr(sDT);
                    switch (Operator)
                    {
                        case SQLConditionOperator.Equal:
                            return FieldName + " = " + sDT;
                        case SQLConditionOperator.NotEqual:
                            return FieldName + " <> " + sDT;
                        case SQLConditionOperator.LessEqualThan:
                            return FieldName + " <= " + sDT;
                        case SQLConditionOperator.LessThan:
                            return FieldName + " < " + sDT;
                        case SQLConditionOperator.GreaterEqualThan:
                            return FieldName + " >= " + sDT;
                        case SQLConditionOperator.GreaterThan:
                            return FieldName + " > " + sDT;
                        default:
                            throw new Exception("Sigbit.Common.ConditionEntry.ToString() Error:"
                                    + "Unexpected DateTime Operator.");
                    }
                default:    // 缺省的都是数字型
                    switch (Operator)
                    {
                        case SQLConditionOperator.Equal:
                            return FieldName + " = " + FieldValue.ToString();
                        case SQLConditionOperator.In:
                            return FieldName + " in (" + FieldValue.ToString() + ")";
                        case SQLConditionOperator.LessEqualThan:
                            return FieldName + " <= " + FieldValue.ToString();
                        case SQLConditionOperator.LessThan:
                            return FieldName + " < " + FieldValue.ToString();
                        case SQLConditionOperator.GreaterEqualThan:
                            return FieldName + " >= " + FieldValue.ToString();
                        case SQLConditionOperator.GreaterThan:
                            return FieldName + " > " + FieldValue.ToString();
                        case SQLConditionOperator.NotEqual:
                            return FieldName + " <> " + FieldValue.ToString();
                        case SQLConditionOperator.NotIn:
                            return FieldName + " not in (" + FieldValue.ToString() + ")";
                        default:
                            throw new Exception("Sigbit.Common.ConditionEntry.ToString() Error:"
                                    + "Unexpected Numeric Operator.");
                    }
            }
        }
        #endregion 输出
    }
    #endregion 类SQLConditionEntry

    #region 类SQLConditionList
    /// <summary>
    /// 查询条件列表
    /// </summary>
    class SQLConditionList : ArrayList
    {
        #region 增加
        /// <summary>
        /// 增加一个条件
        /// </summary>
        /// <param name="entry">条件项</param>
        public void AddCondition(SQLConditionEntry entry)
        {
            Add(entry);
        }

        public void AddEntry(string sFieldName, string sFieldChineseName,
                object objFieldValue, SQLConditionOperator op,
                string sDescription, SQLConditionFieldType fieldType)
        {
            SQLConditionEntry entry = new SQLConditionEntry(sFieldName, sFieldChineseName,
                    objFieldValue, op, sDescription, fieldType);
            Add(entry);
        }

        public void AddEntry(string sFieldName, string sFieldChineseName,
                object objFieldValue, SQLConditionOperator op,
                string sDescription)
        {
            SQLConditionEntry entry = new SQLConditionEntry(sFieldName, sFieldChineseName, 
                    objFieldValue, op, sDescription);
            Add(entry);
        }

        public void AddEntry(string sFieldName, string sFieldChineseName,
                object objFieldValue, SQLConditionOperator op)
        {
            SQLConditionEntry entry = new SQLConditionEntry(sFieldName, sFieldChineseName, 
                    objFieldValue, op);
            Add(entry);
        }

        public void AddEntry(string sFieldName, string sFieldChineseName,
                object objFieldValue)
        {
            SQLConditionEntry entry = new SQLConditionEntry(sFieldName,
                    sFieldChineseName, objFieldValue);
            Add(entry);
        }

        public void AddEntry(string sFieldName, string sFieldChineseName)
        {
            SQLConditionEntry entry = new SQLConditionEntry(sFieldName, sFieldChineseName);
            Add(entry);
        }

        public void AddEntry(string sFieldName)
        {
            SQLConditionEntry entry = new SQLConditionEntry(sFieldName);
            Add(entry);
        }
        #endregion 增加

        #region 取值
        /// <summary>
        /// 取出条件的字段值
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <param name="op">操作符</param>
        /// <returns>字段值(object类型)</returns>
        public object GetConditionValue(string sFieldName, SQLConditionOperator op)
        {
            for (int i = 0; i < Count; i++)
            {
                SQLConditionEntry entry = (SQLConditionEntry)this[i];
                if (entry.FieldName == sFieldName)
                {
                    if (op == SQLConditionOperator.SYSAny)
                        return entry.FieldValue;
                    else if (entry.Operator == op)
                        return entry.FieldValue;
                }
            }
            return null;
        }

        public object GetConditionValue(string sFieldName)
        {
            return GetConditionValue(sFieldName, SQLConditionOperator.SYSAny);
        }

        public string GetConditionValueString(string sFieldName,
                SQLConditionOperator op)
        {
            object objValue = GetConditionValue(sFieldName, op);
            string sRet = ConvertUtil.ToString(objValue);
            return sRet;
        }

        public string GetConditionValueString(string sFieldName)
        {
            return GetConditionValueString(sFieldName, SQLConditionOperator.SYSAny);
        }

        public int GetConditionValueInt(string sFieldName,
                SQLConditionOperator op)
        {
            object objValue = GetConditionValue(sFieldName, op);
            int nRet = ConvertUtil.ToInt(objValue);
            return nRet;
        }

        public int GetConditionValueInt(string sFieldName)
        {
            return GetConditionValueInt(sFieldName, SQLConditionOperator.SYSAny);
        }

        #endregion 取值

        #region 描述
        public string GetDescription()
        {
            if (Count == 0)
                return "";

            string sRet = "";
            for (int i = 0; i < Count; i++)
            {
                SQLConditionEntry entry = (SQLConditionEntry)this[i];
                if (i != 0)
                    sRet += ";";
                sRet += entry.Description;
            }
            return sRet;
        }
        #endregion 描述

        #region 删除
        public void RemoveEntry(string sFieldName)
        {
            RemoveEntry(sFieldName, SQLConditionOperator.SYSAny);
        }

        public void RemoveEntry(string sFieldName, SQLConditionOperator op)
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                SQLConditionEntry entry = (SQLConditionEntry)this[i];
                if (entry.FieldName == sFieldName)
                {
                    if (op == SQLConditionOperator.SYSAny)
                        RemoveAt(i);
                    else if (entry.Operator == op)
                        RemoveAt(i);
                }
            }
        }
        #endregion 删除

        #region 输出
        /// <summary>
        /// 输出限定条件的SQL语句
        /// </summary>
        /// <returns>限定条件的SQL语句</returns>
        public override string ToString()
        {
            if (Count == 0)
                return "";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Count; i++)
            {
                SQLConditionEntry entry = (SQLConditionEntry)this[i];
                string sSQL = entry.ToString();
                if (i == 0)
                    sb.Append(sSQL);
                else
                    sb.Append(" and " + sSQL);
            }

            return sb.ToString();
        }
        #endregion 输出
    }
    #endregion 类SQLConditionList
}

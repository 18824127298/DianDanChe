using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using Sigbit.Common;

namespace Sigbit.Common
{
    #region ö��SQLConditionFieldType
    /// <summary>
    /// �����ֶε�����
    /// </summary>
    public enum SQLConditionFieldType
    {
        /// <summary>
        /// �ַ�������
        /// </summary>
        String,
        /// <summary>
        /// ������
        /// </summary>
        Boolean,
        /// <summary>
        /// ����
        /// </summary>
        Int,
        /// <summary>
        /// ������
        /// </summary>
        Float,
        /// <summary>
        /// ʱ������
        /// </summary>
        DateTime
    }
    #endregion ö��SQLConditionFieldType

    #region ö��SQLConditionOperator
    /// <summary>
    /// �������ʽ�Ĳ���������
    /// </summary>
    public enum SQLConditionOperator
    {
        /// <summary>
        /// ���ڣ�=
        /// </summary>
        Equal,	   
        /// <summary>
        /// �����ڣ�&lt;&gt;
        /// </summary>
        NotEqual,	
        /// <summary>
        /// ���ڣ�&gt;
        /// </summary>
        GreaterThan,	
        /// <summary>
        /// ���ڵ��ڣ�&gt;=
        /// </summary>
        GreaterEqualThan,
        /// <summary>
        /// С�ڣ�&lt;
        /// </summary>
        LessThan,
        /// <summary>
        /// С�ڵ��ڣ�&lt;=
        /// </summary>
        LessEqualThan,
        /// <summary>
        /// ���ƣ�like
        /// </summary>
        Like,
        /// <summary>
        /// ����,like "%x"
        /// </summary>
        LikeLeft,
        /// <summary>
        /// ���ƣ�like "x%"
        /// </summary>
        LikeRight,
        /// <summary>
        /// �����ƣ�not (like)
        /// </summary>
        NotLike,		    
        /// <summary>
        /// ��xxx��
        /// </summary>
        In,		   
        /// <summary>
        /// ����xxx��
        /// </summary>
        NotIn,	
        /// <summary>
        /// �û���������������޶�
        /// </summary>
        UserDefined,
        /// <summary>
        /// ϵͳ��������ȡ���Ƴ��κ�һ���ֶ�
        /// </summary>
        SYSAny
    }
    #endregion ö��SQLConditionOperator

    #region ��SQLConditionEntry
    /// <summary>
    /// ���������
    /// </summary>
    class SQLConditionEntry
    {
        #region ����
        private string _fieldName = "";
        /// <summary>
        /// ʵ�������ֶ�
        /// </summary>
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        private string _fieldChinsesName = "";
        /// <summary>
        /// �ֶ�������
        /// </summary>
        public string FieldChinsesName
        {
            get { return _fieldChinsesName; }
            set { _fieldChinsesName = value; }
        }

        private SQLConditionFieldType _fieldType;	   
        /// <summary>
        /// �ֶ�����
        /// </summary>
        public SQLConditionFieldType FieldType
        {
            get { return _fieldType; }
            set { _fieldType = value; }
        }

        private SQLConditionOperator _operator;
        /// <summary>
        /// ������
        /// </summary>
        public SQLConditionOperator Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        private object _fieldValue;
        /// <summary>
        /// ֵ
        /// </summary>
        public object FieldValue
        {
            get { return _fieldValue; }
            set { _fieldValue = value; }
        }

        private string _description = "";
        /// <summary>
        /// ��������
        /// </summary>
        public string Description
        {
            get 
            { 
                //===== 1. �������������ֱ�ӷ��� ======
                if (_description != "")
                    return _description;

                //====== 2. �õ��ֶε����� =========
                string sFieldChineseName;
                if (_fieldChinsesName == "")
                    sFieldChineseName = _fieldName;
                else
                    sFieldChineseName = _fieldChinsesName;

                //======== 3. �õ������������� =====
                string sOpDesc = GetDescriptionOfOperator(Operator);

                //======== 4. �õ�ֵ������ ========
                string sValueDesc = "��" + FieldValue.ToString() + "��";

                //======== 5. ����ֵ =============
                _description = sFieldChineseName + sOpDesc + sValueDesc;
                return _description;
            }
            set { _description = value; }
        }
        #endregion ����

        #region ֧��
        private string GetDescriptionOfOperator(SQLConditionOperator op)
        {
            switch (op)
            {
                case SQLConditionOperator.Equal:
                    return "Ϊ";
                case SQLConditionOperator.In:
                    return "����";
                case SQLConditionOperator.LessEqualThan:
                    return "С��";
                case SQLConditionOperator.LessThan:
                    return "С��";
                case SQLConditionOperator.Like:
                    return "��";
                case SQLConditionOperator.LikeLeft:
                    return "��׺Ϊ";
                case SQLConditionOperator.LikeRight:
                    return "ǰ׺Ϊ";
                case SQLConditionOperator.GreaterEqualThan:
                    return "����";
                case SQLConditionOperator.GreaterThan:
                    return "����";
                case SQLConditionOperator.NotEqual:
                    return "����";
                case SQLConditionOperator.NotIn:
                    return "������";
                case SQLConditionOperator.NotLike:
                    return "����";
                case SQLConditionOperator.UserDefined:
                    return "";
                default:
                    return "";
            }
        }
        #endregion ֧��

        #region ���캯��
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
        /// ���캯��
        /// </summary>
        /// <param name="sFieldName">�ֶ���</param>
        /// <param name="sFieldChineseName">�ֶ�������</param>
        /// <param name="objFieldValue">ֵ</param>
        /// <param name="op">������</param>
        /// <param name="sDescription">����</param>
        /// <param name="fieldType">�ֶ�����</param>
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
        /// ͨ���������ֵ��ͨ���ñ��������ͣ��õ����ݿ��ֶε�����
        /// </summary>
        /// <param name="oValue">��������</param>
        /// <returns>������ݿ��ֶε�����</returns>
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
                return SQLConditionFieldType.String;       // ���� ==>> String
            }
        }
        #endregion ���캯��

        #region ���
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
        /// �õ���ѯ������SQL���
        /// </summary>
        /// <returns>SQL���</returns>
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
                default:    // ȱʡ�Ķ���������
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
        #endregion ���
    }
    #endregion ��SQLConditionEntry

    #region ��SQLConditionList
    /// <summary>
    /// ��ѯ�����б�
    /// </summary>
    class SQLConditionList : ArrayList
    {
        #region ����
        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="entry">������</param>
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
        #endregion ����

        #region ȡֵ
        /// <summary>
        /// ȡ���������ֶ�ֵ
        /// </summary>
        /// <param name="sFieldName">�ֶ���</param>
        /// <param name="op">������</param>
        /// <returns>�ֶ�ֵ(object����)</returns>
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

        #endregion ȡֵ

        #region ����
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
        #endregion ����

        #region ɾ��
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
        #endregion ɾ��

        #region ���
        /// <summary>
        /// ����޶�������SQL���
        /// </summary>
        /// <returns>�޶�������SQL���</returns>
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
        #endregion ���
    }
    #endregion ��SQLConditionList
}

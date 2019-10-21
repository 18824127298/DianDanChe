using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Data.DBStruct
{
    /// <summary>
    /// �ֶ�����ö��
    /// </summary>
    public enum DataTypeDefine
    {
        /// <summary>
        /// �����ַ��ֶ�
        /// </summary>
        Char,
        /// <summary>
        /// �䳤�ַ��ֶ�
        /// </summary>
        Varchar,
        /// <summary>
        /// �����ֶ�
        /// </summary>
        Int,
        /// <summary>
        /// �ı��ֶ�
        /// </summary>
        Text,
        /// <summary>
        /// С���ֶ�
        /// </summary>
        Numeric,
        /// <summary>
        /// ������������
        /// </summary>
        Other
    }

    public class ColumnDefine
    {
        private string _columnName = "";
        /// <summary>
        /// ����
        /// </summary>
        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        private string _columnNameChs = "";
        /// <summary>
        /// ����������
        /// </summary>
        public string ColumnNameChs
        {
            get { return _columnNameChs; }
            set { _columnNameChs = value; }
        }

        private DataTypeDefine _dataType = DataTypeDefine.Other;
        /// <summary>
        /// ��������
        /// </summary>
        public DataTypeDefine DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        private string _dataTypeName = "";
        /// <summary>
        /// ���������������
        /// </summary>
        public string DataTypeName
        {
            get { return _dataTypeName; }
            set { _dataTypeName = value; }
        }

        private int _length = -1;
        /// <summary>
        /// ���ݳ���
        /// </summary>
        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }

        private int _lengthPrecision = 0;
        /// <summary>
        /// С�����ֵĳ���
        /// </summary>
        public int LengthPrecision
        {
            get { return _lengthPrecision; }
            set { _lengthPrecision = value; }
        }


        private Bool3State _canBeNull = Bool3State.Undefine;
        /// <summary>
        /// �Ƿ�����Ϊ��
        /// </summary>
        public Bool3State CanBeNull
        {
            get { return _canBeNull; }
            set { _canBeNull = value; }
        }

        private string _defaultValue = "";
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public string DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

    }
}

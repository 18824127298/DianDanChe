using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Data.FoxDBF
{
    /// <summary>
    /// �ֶζ���
    /// </summary>
    class FoxFieldDef
    {
        private string _fieldName = "";
        /// <summary>
        /// �ֶ���
        /// </summary>
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        private DBFFieldType _fieldType;
        /// <summary>
        /// �ֶ�����
        /// </summary>
        public DBFFieldType FieldType
        {
            get { return _fieldType; }
            set { _fieldType = value; }
        }

        private int _fieldLength = 0;
        /// <summary>
        /// �ֶγ���
        /// </summary>
        public int FieldLength
        {
            get { return _fieldLength; }
            set { _fieldLength = value; }
        }

        private int _fieldPoint = 0;
        /// <summary>
        /// �ֶξ���
        /// </summary>
        public int FieldPoint
        {
            get { return _fieldPoint; }
            set { _fieldPoint = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace Sigbit.Net.BIPPacket
{
    /// <summary>
    /// 字段类型
    /// </summary>
    public enum BIPFieldType
    {
        /// <summary>
        /// 数据型
        /// </summary>
        Number = 'N',       // 数值型
        /// <summary>
        /// 字符型
        /// </summary>
        Char = 'C'          // 字符型
    };

    /// <summary>
    /// 字段
    /// </summary>
    public class BIPField
    {
        string _fieldName;
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        BIPFieldType _fieldType;
        /// <summary>
        /// 字段类型
        /// </summary>
        public BIPFieldType FieldType
        {
            get { return _fieldType; }
            set { _fieldType = value; }
        }

        int _fieldLength;
        /// <summary>
        /// 字段长度
        /// </summary>
        public int FieldLength
        {
            get { return _fieldLength; }
            set { _fieldLength = value; }
        }

        int _fieldPrecision;
        /// <summary>
        /// 字段精度
        /// </summary>
        public int FieldPrecision
        {
            get { return _fieldPrecision; }
            set { _fieldPrecision = value; }
        }

        /// <summary>
        /// 按域指派
        /// </summary>
        /// <param name="srcField"></param>
        public void AssignBy(BIPField srcField)
        {
            _fieldName = srcField.FieldName;
            _fieldType = srcField.FieldType;
            _fieldLength = srcField.FieldLength;
            _fieldPrecision = srcField.FieldPrecision;
        }
    }

    /// <summary>
    /// 字段列表
    /// </summary>
    class BIPFieldList : ArrayList
    {
        bool _ignoreCase;
        /// <summary>
        /// 忽略大小写
        /// </summary>
        public bool IgnoreCase
        {
            get { return _ignoreCase; }
            set { _ignoreCase = value; }
        }

        /// <summary>
        /// 向列表中增加一个字段
        /// </summary>
        /// <param name="bipField">字段</param>
        public void AddField(BIPField bipField)
        {
            BIPField newField = new BIPField();
            newField.AssignBy(bipField);
            Add(newField);
        }

        /// <summary>
        /// 根据字段名定位字段的序号
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <returns>字段序号(从0开始)</returns>
        public int LocateFieldName(string sFieldName)
        {
            for (int i = 0; i < Count; i++)
            {
                BIPField field = (BIPField)this[i];
                if (_ignoreCase)
                {
                    if (field.FieldName.ToUpper() == sFieldName.ToUpper())
                        return i;
                }
                else
                {
                    if (field.FieldName == sFieldName)
                        return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 根据字段索引(位置)得到字段
        /// </summary>
        /// <param name="nIndex">字段索引</param>
        /// <returns>字段</returns>
        public BIPField GetField(int nIndex)
        {
            return ((BIPField)this[nIndex]);
        }
    }
}

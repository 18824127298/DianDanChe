using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace Sigbit.Data.FoxDBF
{
    class FOXFIELDList
    {
        private ArrayList _arrFields = new ArrayList();

        private Hashtable _htFieldName = new Hashtable();

        public void AddField(FOXFIELD field)
        {
            _arrFields.Add(field);
            _htFieldName[field.FieldName.ToUpper()] = _arrFields.Count - 1;
        }

        public FOXFIELD GetField(int nIndex)
        {
            return (FOXFIELD)_arrFields[nIndex];
        }

        public int FieldCount
        {
            get
            {
                return _arrFields.Count;
            }
        }

        public int GetFieldSeqOfFieldName(string sFieldName)
        {
            object value = _htFieldName[sFieldName.ToUpper()];
            if (value == null)
                throw new Exception("定位不到字段 - " + sFieldName);

            int nRet = (int)value;
            return nRet;
        }

        public FOXFIELD GetField(string sFieldName)
        {
            int nFieldSeq = GetFieldSeqOfFieldName(sFieldName);
            return GetField(nFieldSeq);
        }
    }
}

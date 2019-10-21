using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;

namespace Sigbit.Framework
{
    /// <summary>
    /// ҳ��䴫�ݲ����õ���
    /// </summary>
    /// <remarks>
    /// һ����÷��ǣ��ڴ��µ�ҳ��ǰ�����������ҳ��򿪺��ȡ������
    /// �Ժ����Ҳ�����ˡ�����Ϊ���е�ҳ�涼�Ṳ�����������
    /// </remarks>
    public class SbtPageParameter
    {
        public string[] StringParam = new string[32];
        public int[] IntParam = new int[32];
        public DataSet DataSetParam = null;
        public object[] ObjParam = new object[8];

        #region �Զ������
        /// <summary>
        /// �Զ��������
        /// </summary>
        /// <remarks>
        /// �Զ���������������ַ������б�ʶ�ʹ洢�ļ���
        /// </remarks>
        private Hashtable _htCustomParams = new Hashtable();

        /// <summary>
        /// ����Զ���Ĳ���
        /// </summary>
        public void ClearAllCustomParams()
        {
            _htCustomParams.Clear();
        }

        /// <summary>
        /// ������ض��ַ�����ͷ���Զ������
        /// </summary>
        /// <param name="sHeadString">�ض����ַ�����ͷ</param>
        public void ClearCustomParamsStartWith(string sHeadString)
        {
            ArrayList arrClearKeyList = new ArrayList();

            //========== 1. ������ϣ���ҵ����ض��ַ�����ͷ�Ĺؼ��� ==============
            foreach (DictionaryEntry entry in _htCustomParams)
            {
                string sKey = (string)entry.Key;
                if (sKey.IndexOf(sHeadString) != -1)
                    arrClearKeyList.Add(sKey);
            }

            //=========== 2. �����Щ�Զ������ ==============
            foreach (string sWantClearKey in arrClearKeyList)
                _htCustomParams.Remove(sWantClearKey);
        }

        /// <summary>
        /// �����Զ������
        /// </summary>
        /// <param name="sKey">�ؼ���</param>
        /// <param name="objValue">����ֵ</param>
        public void SetCustomParamObject(string sKey, object objValue)
        {
            _htCustomParams[sKey] = objValue;
        }

        /// <summary>
        /// �����Զ������
        /// </summary>
        /// <param name="sKey">�ؼ���</param>
        /// <param name="sValue">�ַ���ֵ</param>
        public void SetCustomParamString(string sKey, string sValue)
        {
            SetCustomParamObject(sKey, sValue);
        }

        /// <summary>
        /// �����Զ������
        /// </summary>
        /// <param name="sKey">�ؼ���</param>
        /// <param name="objValue">����ֵ</param>
        public void AddCustomParamObject(string sKey, object objValue)
        {
            if (_htCustomParams[sKey] != null)
                throw new Exception("�Զ�������������йؼ��֡�" + sKey + "��, ���Ӹ��Զ���ֵ����");

            _htCustomParams.Add(sKey, objValue);
        }

        /// <summary>
        /// �����Զ������
        /// </summary>
        /// <param name="sKey">�ؼ���</param>
        /// <param name="sValue">�ַ���ֵ</param>
        public void AddCustomParamString(string sKey, string sValue)
        {
            AddCustomParamObject(sKey, sValue);
        }

        /// <summary>
        /// ��ȡ�Զ����������δ���ã�����nullֵ
        /// </summary>
        /// <param name="sKey">�ؼ���</param>
        /// <returns>����ֵ</returns>
        public object GetCustomParamObject(string sKey)
        {
            return _htCustomParams[sKey];
        }

        /// <summary>
        /// ��ȡ�Զ����������δ���ã�����nullֵ
        /// </summary>
        /// <param name="sKey">�ؼ���</param>
        /// <returns>�ַ���ֵ</returns>
        public string GetCustomParamString(string sKey)
        {
            object obj = _htCustomParams[sKey];
            if (obj == null)
                return null;

            if (!(obj is string))
                throw new Exception("��ȡ��ֵ�����ַ���");

            return (string)obj;
        }

        /// <summary>
        /// ��ȡ�Զ����������δ���ã�����ȱʡֵ
        /// </summary>
        /// <param name="sKey">�ؼ���</param>
        /// <param name="sDefaultValue">ȱʡֵ</param>
        /// <returns>�ַ���ֵ</returns>
        public string GetCustomParamStringDefault(string sKey, string sDefaultValue)
        {
            string sRet = GetCustomParamString(sKey);
            if (sRet == null)
                return sDefaultValue;
            else
                return sRet;
        }

        #endregion �Զ������
    }
}

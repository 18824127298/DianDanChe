using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Common.WordProcess.Excel
{
    /// <summary>
    /// �ֶζ�����
    /// </summary>
    public class ExcelFieldDefList : ArrayList
    {
        #region ˽������
        Hashtable htItemByName = new Hashtable();
        Hashtable htItemByChsName = new Hashtable();

        #endregion ˽������

        #region ˽�з���

        #endregion ˽�з���

        #region ��������

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        new public ExcelFieldDefListItem this[int nIndex]
        {
            get 
            {
                return (ExcelFieldDefListItem)(base[nIndex]);
            }
        }
        #endregion ��������

        #region ��������

        /// <summary>
        /// �����ֶ�
        /// </summary>
        /// <param name="item">�ֶ�</param>
        /// <returns></returns>
        public int Add(ExcelFieldDefListItem item)
        {
            htItemByName.Add(item.FieldName, item);
            if (item.FieldChsName != "")
            {
                htItemByChsName.Add(item.FieldChsName, item);
            }
            return base.Add(item);
        }

        /// <summary>
        /// ͨ���ֶ�����ȡ�ֶ�����
        /// </summary>
        /// <param name="sFieldName">�ֶ���</param>
        /// <returns></returns>
        public ExcelFieldDefListItem GetItemByFieldName(string sFieldName)
        {
            return (ExcelFieldDefListItem)htItemByName[sFieldName];
        }

        /// <summary>
        /// ͨ���ֶ����ƻ�ȡ��������
        /// </summary>
        /// <param name="sFieldName">�ֶ���</param>
        /// <returns></returns>
        public ExcelFieldDefListItem GetItemByFieldChsName(string sFieldName)
        {
            return (ExcelFieldDefListItem)htItemByChsName[sFieldName];
        }

        #endregion ��������
    }
}

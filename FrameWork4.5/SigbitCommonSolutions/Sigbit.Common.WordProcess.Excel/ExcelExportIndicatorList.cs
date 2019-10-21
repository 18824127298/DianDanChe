using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Common.WordProcess.Excel
{
    /// <summary>
    ///  ģ���ʶ��
    /// </summary>
    public class ExcelExportIndicatorList : ArrayList
    {
        #region ˽������
        ArrayList _indicatorList = new ArrayList();
        Hashtable htIndicatorList = new Hashtable();

        #endregion ˽������

        #region ˽�з���

        #endregion ˽�з���

        #region ��������

        #endregion ��������

        #region ��������

        /// <summary>
        /// ����ģ���ʶ
        /// </summary>
        /// <param name="sIndicator">��ʶ����</param>
        /// <param name="sText">��ʶ�ı�</param>
        public void Add(string sIndicator, string sText)
        {
            if (htIndicatorList[sIndicator] == null)
            {
                ExcelExportIndicatorItem item = new ExcelExportIndicatorItem();
                item.Indicator = sIndicator;
                item.Text = sText;
                base.Add(item);
                htIndicatorList.Add(sIndicator, item);
            }
            else
            {
                throw new Exception("��ʶ�Ѿ�����:" + sIndicator);
            }
        }


        /// <summary>
        /// ��ȡȫ���ı����Զ����ֶ�
        /// </summary>
        /// <param name="sText"></param>
        /// <returns></returns>
        public static string[] GetAllIndicatorFromText(string sText)
        {
            ArrayList alIndicator = new ArrayList();
            int nPos1 = 0;
            int nPos2 = 0;
            int nOffset = 0;
            while(true)
            {
                nPos1 = sText.IndexOf("{" , nOffset);
                if (nPos1 >= 0)
                {
                    nPos2 = sText.IndexOf("}", nPos1);
                    if (nPos2 >= 0)
                    {
                        nOffset = nPos2;
                        string sIndicator = sText.Substring(nPos1 + 1, nPos2 - nPos1 - 1);
                        alIndicator.Add(sIndicator);
                        nOffset = nPos2;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            if (alIndicator.Count > 0)
            {
                return (string[])alIndicator.ToArray(typeof(string));
            }
            else
            {
                return null;
            }


        }

        /// <summary>
        /// ��ȡģ���ʶ����
        /// </summary>
        /// <param name="sIndicator">��ʶ����</param>
        /// <returns></returns>
        public ExcelExportIndicatorItem GetIndicatorByName(string sIndicator)
        {
            return (ExcelExportIndicatorItem)htIndicatorList[sIndicator];
        }

        #endregion ��������
    }
}

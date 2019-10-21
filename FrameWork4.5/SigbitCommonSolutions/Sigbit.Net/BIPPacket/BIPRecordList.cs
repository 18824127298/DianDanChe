using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Diagnostics;

namespace Sigbit.Net.BIPPacket
{
    /// <summary>
    /// ��¼��
    /// </summary>
    public class BIPRecordList : ArrayList
    {
        /// <summary>
        /// ��Ӽ�¼
        /// </summary>
        public void AddRecord()
        {
            ArrayList record = new ArrayList();
            Add(record);
        }

        /// <summary>
        /// �������ݼ��е�һ��ֵ
        /// </summary>
        /// <param name="nRecordNum">��¼��ţ���1��ʼ��</param>
        /// <param name="nFieldSeq">�ֶκţ���0��ʼ��</param>
        /// <param name="sItemValue">ֵ</param>
        /// <remarks>
        /// 1)��¼��ű������б��С�ķ�Χ֮��<br/>
        /// 2)����ֶ���ų�����ǰ��С�����Զ���չ��Ӧ���ֶοռ�<br/>
        /// </remarks>
        public void SetItemString(int nRecordNum, int nFieldSeq, string sItemValue)
        {
            Debug.Assert(nRecordNum <= Count && nRecordNum >= 1);

            ArrayList slRecord = (ArrayList)this[nRecordNum - 1];

            int nFieldCount = slRecord.Count;
            if (nFieldSeq >= nFieldCount)
            {
                for (int i = 1; i <= nFieldSeq - nFieldCount + 1; i++)
                    slRecord.Add("");
            }

            slRecord[nFieldSeq] = sItemValue;
        }

        /// <summary>
        /// �õ����ݼ��е�һ��ֵ
        /// </summary>
        /// <param name="nRecordNum">��¼���</param>
        /// <param name="nFieldSeq">�ֶκ�</param>
        /// <returns>�õ���ֵ</returns>
        /// <remarks>����ֶ���ų�����ǰ��С���򷵻�""</remarks>
        public string GetItemString(int nRecordNum, int nFieldSeq)
        {
            Debug.Assert(nRecordNum <= Count);
            ArrayList slRecord = (ArrayList)this[nRecordNum - 1];

            if (slRecord.Count <= nFieldSeq)
                return "";

            return (string)slRecord[nFieldSeq];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Diagnostics;

namespace Sigbit.Net.BIPPacket
{
    /// <summary>
    /// 记录集
    /// </summary>
    public class BIPRecordList : ArrayList
    {
        /// <summary>
        /// 添加记录
        /// </summary>
        public void AddRecord()
        {
            ArrayList record = new ArrayList();
            Add(record);
        }

        /// <summary>
        /// 设置数据集中的一个值
        /// </summary>
        /// <param name="nRecordNum">记录序号（从1开始）</param>
        /// <param name="nFieldSeq">字段号（从0开始）</param>
        /// <param name="sItemValue">值</param>
        /// <remarks>
        /// 1)记录序号必须在列表大小的范围之内<br/>
        /// 2)如果字段序号超过当前大小，则自动扩展相应的字段空间<br/>
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
        /// 得到数据集中的一个值
        /// </summary>
        /// <param name="nRecordNum">记录序号</param>
        /// <param name="nFieldSeq">字段号</param>
        /// <returns>得到的值</returns>
        /// <remarks>如果字段序号超过当前大小，则返回""</remarks>
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

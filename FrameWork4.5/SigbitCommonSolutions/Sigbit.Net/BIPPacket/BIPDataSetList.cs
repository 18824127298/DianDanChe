using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace Sigbit.Net.BIPPacket
{
    /// <summary>
    /// BIP数据集列表类
    /// </summary>
    public class BIPDataSetList : ArrayList
    {
        /// <summary>
        /// 添加数据集
        /// </summary>
        public void AddDataSet()
        {
            BIPDataSet dataSet = new BIPDataSet();
            Add(dataSet);
        }

        /// <summary>
        /// 按索引获取数据集
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        public BIPDataSet GetDataSet(int nIndex)
        {
            return (BIPDataSet)this[nIndex];
        }
    }
}

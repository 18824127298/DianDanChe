using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Data.DBStruct
{
    public class ColumnDefineList
    {

        private ArrayList _defineList = new ArrayList();

        /// <summary>
        /// 列数
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return _defineList.Count;
            }
        }

        /// <summary>
        /// 得到一个列定义
        /// </summary>
        /// <param name="nIndex">序号</param>
        /// <returns></returns>
        public ColumnDefine GetColumnDefine(int nIndex)
        {
            if (nIndex < _defineList.Count)
            {
                return (ColumnDefine)_defineList[nIndex];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 增加一列
        /// </summary>
        /// <param name="column">增加的列</param>
        public void AddColumnDefine(ColumnDefine column)
        {
            _defineList.Add(column);
        }

    }
}

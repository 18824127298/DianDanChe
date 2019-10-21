using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Common.WordProcess.Excel
{
    /// <summary>
    /// 字段定义类
    /// </summary>
    public class ExcelFieldDefList : ArrayList
    {
        #region 私有属性
        Hashtable htItemByName = new Hashtable();
        Hashtable htItemByChsName = new Hashtable();

        #endregion 私有属性

        #region 私有方法

        #endregion 私有方法

        #region 公共属性

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
        #endregion 公共属性

        #region 公共方法

        /// <summary>
        /// 增加字段
        /// </summary>
        /// <param name="item">字段</param>
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
        /// 通过字段名获取字段数据
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <returns></returns>
        public ExcelFieldDefListItem GetItemByFieldName(string sFieldName)
        {
            return (ExcelFieldDefListItem)htItemByName[sFieldName];
        }

        /// <summary>
        /// 通过字段名称获取中文名称
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <returns></returns>
        public ExcelFieldDefListItem GetItemByFieldChsName(string sFieldName)
        {
            return (ExcelFieldDefListItem)htItemByChsName[sFieldName];
        }

        #endregion 公共方法
    }
}

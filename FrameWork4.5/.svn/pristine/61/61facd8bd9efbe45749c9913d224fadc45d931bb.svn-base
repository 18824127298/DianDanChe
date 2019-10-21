using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Common.WordProcess.Excel
{
    /// <summary>
    ///  模板标识类
    /// </summary>
    public class ExcelExportIndicatorList : ArrayList
    {
        #region 私有属性
        ArrayList _indicatorList = new ArrayList();
        Hashtable htIndicatorList = new Hashtable();

        #endregion 私有属性

        #region 私有方法

        #endregion 私有方法

        #region 公共属性

        #endregion 公共属性

        #region 公共方法

        /// <summary>
        /// 增加模板标识
        /// </summary>
        /// <param name="sIndicator">标识名称</param>
        /// <param name="sText">标识文本</param>
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
                throw new Exception("标识已经存在:" + sIndicator);
            }
        }


        /// <summary>
        /// 获取全部文本的自定义字段
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
        /// 获取模板标识数据
        /// </summary>
        /// <param name="sIndicator">标识名称</param>
        /// <returns></returns>
        public ExcelExportIndicatorItem GetIndicatorByName(string sIndicator)
        {
            return (ExcelExportIndicatorItem)htIndicatorList[sIndicator];
        }

        #endregion 公共方法
    }
}

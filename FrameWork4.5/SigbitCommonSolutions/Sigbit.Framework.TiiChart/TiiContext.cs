using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Sigbit.Framework.TiiChart
{
    /// <summary>
    /// 图表内容类
    /// </summary>
    public class TiiContext
    {
        /// <summary>
        /// 保存一个图表
        /// </summary>
        /// <param name="sChartName">保存名称</param>
        /// <param name="chart">图表</param>
        public static void SaveChart(string sChartName, TiiChartBase chart)
        {
            HttpContext.Current.Session[sChartName]=chart;
        }

        /// <summary>
        /// 获取一个图表
        /// </summary>
        /// <param name="sChartName">图表名称</param>
        /// <returns>图表</returns>
        public static TiiChartBase LoadChart(string sChartName)
        {
            TiiChartBase chart = (TiiChartBase)HttpContext.Current.Session[sChartName];
            return chart;
        }
    }
}

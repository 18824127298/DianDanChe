using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Framework.TiiChart
{
    /// <summary>
    /// 风格集
    /// </summary>
    public enum TiiStyleSet
    {
        /// <summary>
        /// 蓝天
        /// </summary>
        BlueSky,
        /// <summary>
        /// 经典
        /// </summary>
        Classic,
        /// <summary>
        /// 黑暗天空
        /// </summary>
        DarkSky
    }

    /// <summary>
    /// 标记显示内容
    /// </summary>
    public enum TiiMarkDisplayContent
    {
        /// <summary>
        /// 值
        /// </summary>
        Value,
        /// <summary>
        /// 百分比
        /// </summary>
        Percent,
        /// <summary>
        /// 名称
        /// </summary>
        Name,
        /// <summary>
        /// 名称+值
        /// </summary>
        NameValue,
        /// <summary>
        /// 名称+百分比
        /// </summary>
        NamePercent
    }

    /// <summary>
    /// 直方图排列方式
    /// </summary>
    public enum TiiBarAlignMethod
    {
        /// <summary>
        /// 叠放
        /// </summary>
        Stacked,
        /// <summary>
        /// 并排
        /// </summary>
        Side
    }

    /// <summary>
    /// 面积图排列方式
    /// </summary>
    public enum TiiAreaAlignMethod
    {
        /// <summary>
        /// 叠放
        /// </summary>
        Stacked,
        /// <summary>
        /// 默认
        /// </summary>
        Default
    }
}

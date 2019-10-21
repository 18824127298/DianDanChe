using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Framework
{
    public enum SbtTaskIntervalType
    {
        Month,          // 每月的几号
        Week,           // 星期几，星期1为1，星期日为7
        Day,            // HHMMSS表示一天的具体时间
        Interval,       // 以秒为单位的间隔
        Once            // 仅运行一次，非周期运行
    }
}

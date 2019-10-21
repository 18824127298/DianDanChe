using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Framework
{
    public class SbtTaskConvert
    {
        public static SbtTaskIntervalType IntervalTypeStringToEnum(string sIntervalType)
        {
            switch (sIntervalType)
            {
                case "month":
                    return SbtTaskIntervalType.Month;
                case "week":
                    return SbtTaskIntervalType.Week;
                case "day":
                    return SbtTaskIntervalType.Day;
                case "interval":
                    return SbtTaskIntervalType.Interval;
                case "once":
                    return SbtTaskIntervalType.Once;
            }

            throw new Exception("SbtTaskConvert.IntervalTypeStringToEnum() error : "
                    + "error intervalType - " + sIntervalType);
        }

        public static string IntervalTypeEnumToString(SbtTaskIntervalType intervalType)
        {
            switch (intervalType)
            {
                case SbtTaskIntervalType.Month:
                    return "month";
                case SbtTaskIntervalType.Week:
                    return "week";
                case SbtTaskIntervalType.Day:
                    return "day";
                case SbtTaskIntervalType.Interval:
                    return "interval";
                case SbtTaskIntervalType.Once:
                    return "once";
            }

            throw new Exception("SbtTaskConvert.IntervalTypeEnumToString() error : "
                    + "error intervalType");
        }
    }
}

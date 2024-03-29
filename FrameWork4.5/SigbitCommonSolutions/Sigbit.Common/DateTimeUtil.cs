
using System;
using System.Collections;

using System.Diagnostics;
using System.Globalization;

namespace Sigbit.Common
{
    /// <summary>
    /// 对时间的处理，尤其是字符串时间格式的处理提供支持
    /// </summary>
    /// <remarks>
    /// 在DateTimeUtil中，对日期、时间统一按特定的字符串格式进行
    /// 处理，时间采用19位的字符串，编码为“yyyy-mm-dd hh:mi:ss”，
    /// 日期采用10位的字符串，编码为“yyyy-mm-dd”。
    /// </remarks>
    public sealed class DateTimeUtil
    {
        #region 日期时间支持一（格式转换）
        /// <summary>
        /// 判断给定的日期时间字符串是否符合相应的格式
        /// </summary>
        /// <param name="strDate">日期时间字符串</param>
        /// <returns>是否符合格式</returns>
        public static bool IsDateTime(string strDate)
        {
            if (strDate.Length != 19)
                return false;

            try
            {
                DateTime dt = DateTime.Parse(strDate);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 得到当前月的第一天
        /// </summary>
        /// <returns>当前月的第一天</returns>
        public static string ThisMonthFirstDate
        {
            get
            {
                DateTime dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 得到当前月的最后一天
        /// </summary>
        /// <returns>当前月的最后一天</returns>
        public static string ThisMonthLastDate
        {
            get
            {
                DateTime dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 1);
                dt = dt.AddDays(-1);
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 指定日期的该月第一天
        /// </summary>
        /// <param name="sThisDate">指定的日期</param>
        /// <returns>该月第一天</returns>
        public static string MonthFirstDate(string sThisDate)
        {
            int nYear = Year(sThisDate);
            int nMonth = Month(sThisDate);
            int nDay = 1;

            return EncodeDate(nYear, nMonth, nDay) + " 00:00:00";
        }

        /// <summary>
        /// 指定日期的该月最后一天
        /// </summary>
        /// <param name="sThisDate">指定的日期</param>
        /// <returns>该月最后一天</returns>
        public static string MonthLastDate(string sThisDate)
        {
            int nYear = Year(sThisDate);
            int nMonth = Month(sThisDate);

            nMonth++;
            if (nMonth == 13)
            {
                nYear++;
                nMonth = 1;
            }

            DateTime dt = new DateTime(nYear, nMonth, 1);
            dt = dt.AddDays(-1);
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 转换字符串类型的时间至日期时间类型
        /// </summary>
        /// <param name="strDateTime">时间字符串</param>
        /// <returns>日期时间类型的值</returns>
        /// <remarks>如果格式不正确，则返回当前的时间</remarks>
        public static DateTime ToDateTime(string strDateTime)
        {
            try
            {
                if (strDateTime.Length == 8)
                {
                    DateTime dateTime = DateTime.ParseExact(strDateTime, "yyyyMMdd", CultureInfo.CurrentCulture, DateTimeStyles.None);
                    return dateTime;
                }
                else if (strDateTime.Length == 14)
                {
                    DateTime dateTime = DateTime.ParseExact(strDateTime, "yyyyMMddHHmmss", CultureInfo.CurrentCulture, DateTimeStyles.None);
                    return dateTime;
                }
                return DateTime.Parse(strDateTime);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 转换时间类型的字符串至日期时间类型
        /// </summary>
        /// <param name="strTime">时间字符串</param>
        /// <returns>时间值</returns>
        /// <remarks>如果格式不正确，则返回的是"00:00:00"的时间值</remarks>
        public static DateTime ToTime(string strTime)
        {
            try
            {
                return DateTime.Parse(strTime);
            }
            catch
            {
                return DateTime.Parse("00:00:00");
            }
        }

        /// <summary>
        /// 将时间值转换至时间字符串
        /// </summary>
        /// <param name="time">时间值</param>
        /// <returns>时间字符串</returns>
        public static string ToTimeStr(DateTime time)
        {
            return time.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 将对象转换为指定格式的字符串
        /// </summary>
        /// <param name="time">待转换的对象</param>
        /// <param name="sFormat">指定的字符串格式</param>
        /// <returns>转换后的字符串</returns>
        /// <remarks>如果为空值，或非日期时间对象，则返回""</remarks>
        public static string ToTimeStr(object time, string sFormat)
        {
            if (time == null || time == DBNull.Value)
                return "";
            else
            {
                try
                {
                    return ((DateTime)time).ToString(sFormat);
                }
                catch
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 将对象转换为字符串，格式为"HH:mm:ss"
        /// </summary>
        /// <param name="time">待转换的对象</param>
        /// <returns>转换后的字符串</returns>
        public static string ToTimeStr(object time)
        {
            return ToTimeStr(time, "HH:mm:ss");
        }

        /// <summary>
        /// 从秒数转换为时间字符串
        /// </summary>
        /// <param name="Second">秒数</param>
        /// <returns>时间字符串</returns>
        public static string ToTimeStrFromSecond(int Second)
        {
            //=========== 1. 得到小时、分钟和秒数 ===========
            string sTimeStr = "";

            int NewSecond = 0;
            int hour = Math.DivRem(Second, 3600, out NewSecond);    //小时

            Second = NewSecond;
            NewSecond = 0;
            int minute = Math.DivRem(Second, 60, out NewSecond);    //分钟

            //============ 2. 得到返回的字符串 ============
            if (hour < 10)
                sTimeStr = sTimeStr + "0" + hour.ToString() + ":";
            else
                sTimeStr = sTimeStr + hour.ToString() + ":";

            if (minute < 10)
                sTimeStr = sTimeStr + "0" + minute.ToString() + ":";
            else
                sTimeStr = sTimeStr + minute.ToString() + ":";

            if (NewSecond < 10)
                sTimeStr = sTimeStr + "0" + NewSecond.ToString();
            else
                sTimeStr = sTimeStr + NewSecond.ToString();

            return sTimeStr;
        }

        /// <summary>
        /// 将时间字符串转换为秒数
        /// </summary>
        /// <param name="timeStr">时间字符串</param>
        /// <returns>转换后的秒数</returns>
        public static int ToSecondsFromTimeStr(string timeStr)
        {
            DateTime dt = ToTime(timeStr);
            return dt.Hour * 3600 + dt.Minute * 60 + dt.Second;
        }

        /// <summary>
        /// 将日期时间值转换为日期字符串
        /// </summary>
        /// <param name="dt">日期时间值</param>
        /// <returns>日期字符串</returns>
        public static string ToDateStr(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 将对象转换为日期字符串
        /// </summary>
        /// <param name="dt">对象</param>
        /// <param name="defaultStr">缺省字符串</param>
        /// <returns>转换后的字符串。如果为空值，则按缺省值返回</returns>
        public static string ToDateStr(object dt, string defaultStr)
        {
            if (dt == null || dt is System.DBNull)
                return defaultStr;
            else
                return ((DateTime)dt).ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 将对象转换为日期字符串
        /// </summary>
        /// <param name="dt">对象</param>
        /// <returns>转换后的字符串。如果转换不成功，则返回""</returns>
        public static string ToDateStr(object dt)
        {
            return ToDateStr(dt, "");
        }

        /// <summary>
        /// 将日期时间值转换为字符串
        /// </summary>
        /// <param name="dt">日期时间值</param>
        /// <returns>字符串(len=19)</returns>
        public static string ToDateTimeStr(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 转换时间值到带毫秒字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateTimeStrWithMilliSeconds(DateTime dt)
        {
            int nMilliSeconds = dt.Millisecond;
            return dt.ToString("yyyy-MM-dd HH:mm:ss") + "." + nMilliSeconds.ToString("000");
        }

        /// <summary>
        /// 将对象转换为日期时间字符串
        /// </summary>
        /// <param name="dt">对象</param>
        /// <param name="defaultStr">转换不成功时所取的缺省字符串</param>
        /// <returns>转换后的日期时间字符串(len=19)</returns>
        public static string ToDateTimeStr(object dt, string defaultStr)
        {
            return ToDateTimeStr(dt, defaultStr, "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 将对象转换为日期时间字符串
        /// </summary>
        /// <param name="dt">对象</param>
        /// <returns>转换后的日期时间字符串(len=19)</returns>
        /// <remarks>如果转换不成功，则返回""</remarks>
        public static string ToDateTimeStr(object dt)
        {
            return ToDateTimeStr(dt, "", "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 将对象转换为指定格式的日期时间字符串
        /// </summary>
        /// <param name="dt">对象</param>
        /// <param name="defaultStr">缺省字符串</param>
        /// <param name="sFormat">格式</param>
        /// <returns>转换后的字符串</returns>
        public static string ToDateTimeStr(object dt, string defaultStr, string sFormat)
        {
            if (dt == null || dt is System.DBNull)
                return defaultStr;
            else
                return ((DateTime)dt).ToString(sFormat);
        }


        #endregion

        #region 日期时间支持二（实用例程）

        /// <summary>
        /// 获取当前的时间
        /// </summary>
        public static string Now
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 带毫秒部分的当前时间，示例为2003.07.22 10:02:52.136
        /// </summary>
        public static string NowWithMilliSeconds
        {
            get
            {
                DateTime dtNow = DateTime.Now;
                int nMilliSeconds = dtNow.Millisecond;
                return dtNow.ToString("yyyy-MM-dd HH:mm:ss") + "." + nMilliSeconds.ToString("000");
            }
        }

        /// <summary>
        /// 获取当前的时间
        /// </summary>
        public static string Today
        {
            get
            {
                return DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 获取日期的年份
        /// </summary>
        /// <param name="dtString">日期时间串</param>
        /// <returns>年份</returns>
        public static int Year(string dtString)
        {
            int nYear;

            nYear = Convert.ToInt32(dtString.Substring(0, 4));
            Debug.Assert(nYear > 1900 && nYear < 2100);

            return nYear;
        }

        /// <summary>
        /// 获取日期的月份
        /// </summary>
        /// <param name="dtString">日期时间串</param>
        /// <returns>月份</returns>
        public static int Month(string dtString)
        {
            int nMonth;

            nMonth = Convert.ToInt32(dtString.Substring(5, 2));
            Debug.Assert(nMonth >= 1 && nMonth <= 12);

            return nMonth;
        }

        /// <summary>
        /// 获取日期中该月的第几天
        /// </summary>
        /// <param name="dtString">日期时间串</param>
        /// <returns>该月的第几天</returns>
        public static int Day(string dtString)
        {
            int nDay;

            nDay = Convert.ToInt32(dtString.Substring(8, 2));
            Debug.Assert(nDay >= 1 && nDay <= 31);

            return nDay;
        }

        /// <summary>
        /// 获取日期中的小时部分
        /// </summary>
        /// <param name="dtString">日期时间串</param>
        /// <returns>小时部分</returns>
        public static int Hour(string dtString)
        {
            int nHour;

            if (dtString.Length != 19 && dtString.Length != 23)
                return 0;

            nHour = Convert.ToInt32(dtString.Substring(11, 2));
            Debug.Assert(nHour >= 0 && nHour <= 24);

            return nHour;
        }

        /// <summary>
        /// 获取日期中的分钟部分
        /// </summary>
        /// <param name="dtString">日期时间串</param>
        /// <returns>分钟部分</returns>
        public static int Minute(string dtString)
        {
            int nMinute;

            if (dtString.Length != 19 && dtString.Length != 23)
                return 0;

            nMinute = Convert.ToInt32(dtString.Substring(14, 2));
            Debug.Assert(nMinute >= 0 && nMinute <= 60);

            return nMinute;
        }

        /// <summary>
        /// 获取日期中的秒部分
        /// </summary>
        /// <param name="dtString">日期时间串</param>
        /// <returns>秒部分</returns>
        public static int Second(string dtString)
        {
            int nSecond;

            if (dtString.Length != 19 && dtString.Length != 23)
                return 0;

            nSecond = Convert.ToInt32(dtString.Substring(17, 2));
            Debug.Assert(nSecond >= 0 && nSecond <= 60);

            return nSecond;
        }

        /// <summary>
        /// 获取表示的日期是星期几
        /// </summary>
        /// <param name="dtString">日期时间串</param>
        /// <returns>星期几</returns>
        /// <remarks>返回值从0至6；星期日为0，星期六为6</remarks>
        public static int DayOfWeek(string dtString)
        {
            DateTime dt = DateTime.Parse(dtString);
            return (int)dt.DayOfWeek;
        }

        /// <summary>
        /// 获取表示的日期是一年中的第几天
        /// </summary>
        /// <param name="dtString">日期时间串</param>
        /// <returns>一年中的第几天</returns>
        public static int DayOfYear(string dtString)
        {
            DateTime dt = DateTime.Parse(dtString);
            return dt.DayOfYear;
        }

        /// <summary>
        /// 将指定的月份数加到日期值上
        /// </summary>
        /// <param name="dtString">原来的日期</param>
        /// <param name="nOffset">指定的月份数</param>
        /// <returns>得到的日期</returns>
        public static string AddMonths(string dtString, int nOffset)
        {
            DateTime dt = DateTime.Parse(dtString);
            DateTime dtRes = dt.AddMonths(nOffset);
            return dtRes.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 将指定的天数加到日期值上
        /// </summary>
        /// <param name="dtString">原来的日期</param>
        /// <param name="offset">指定的天数</param>
        /// <returns>得到的日期</returns>
        /// <remarks>天数可以为负数</remarks>
        public static string AddDays(string dtString, int offset)
        {
            DateTime dt = DateTime.Parse(dtString);
            DateTime dtRes = dt.AddDays(offset);
            return dtRes.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 将指定的秒数加到日期值上
        /// </summary>
        /// <param name="dtString">原来的日期时间</param>
        /// <param name="offset">指定的秒数</param>
        /// <returns>得到的日期时间</returns>
        /// <remarks>秒数可以为负数</remarks>
        public static string AddSeconds(string dtString, int offset)
        {
            DateTime dt = DateTime.Parse(dtString);
            DateTime dtRes = dt.AddSeconds(offset);
            return dtRes.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 添加指定的毫秒数
        /// </summary>
        /// <param name="dtString"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static string AddMilliSeconds(string dtString, int offset)
        {
            DateTime dt = DateTime.Parse(dtString);
            DateTime dtRes = dt.AddMilliseconds(offset);

            int nMilliSeconds = dtRes.Millisecond;
            return dtRes.ToString("yyyy-MM-dd HH:mm:ss") + "." + nMilliSeconds.ToString("000");
        }

        /// <summary>
        /// 添加指定的秒数
        /// </summary>
        /// <param name="dtString">时间串</param>
        /// <param name="offset">添加的秒数</param>
        /// <returns>添加指定秒数后的时间</returns>
        public static string AddMilliSeconds(string dtString, double offset)
        {
            DateTime dt = DateTime.Parse(dtString);
            DateTime dtRes = dt.AddMilliseconds(offset);

            int nMilliSeconds = dtRes.Millisecond;
            return dtRes.ToString("yyyy-MM-dd HH:mm:ss") + "." + nMilliSeconds.ToString("000");
        }

        /// <summary>
        /// 得到两个日期时间之间相差的天数
        /// </summary>
        /// <param name="dtFromString">起始日期</param>
        /// <param name="dtToString">终止日期</param>
        /// <returns>相差的天数</returns>
        /// <remarks>
        /// 不判断时间的情况。只判断日期是否切换，即使时间相差仅数分钟，
        /// 只要切换了日期，也计算相差的天数。例如，2005-05-05 23:58:58和
        /// 2005-05-06 00:02:03之间所差的天数也是1。
        /// </remarks>
        public static int DaysAfter(string dtFromString, string dtToString)
        {
            DateTime dtFrom = DateTime.Parse(dtFromString).Date;
            DateTime dtTo = DateTime.Parse(dtToString).Date;
            TimeSpan timeSpan = dtTo - dtFrom;
            return (int)timeSpan.TotalDays;
        }

        /// <summary>
        /// 得到两个日期时间之间相差的秒数
        /// </summary>
        /// <param name="dtFromString">起始日期时间</param>
        /// <param name="dtToString">终止日期时间</param>
        /// <returns>相差的秒数</returns>
        public static int SecondsAfter(string dtFromString, string dtToString)
        {
            DateTime dtFrom = DateTime.Parse(dtFromString);
            DateTime dtTo = DateTime.Parse(dtToString);
            TimeSpan timeSpan = dtTo - dtFrom;
            return (int)timeSpan.TotalSeconds;
        }

        /// <summary>
        /// 得到两个日期时间之间相差的毫秒数
        /// </summary>
        /// <param name="dtFromString">起始日期时间</param>
        /// <param name="dtToString">终止日期时间</param>
        /// <returns>相差的毫秒数</returns>
        public static int MilliSecondsAfter(string dtFromString, string dtToString)
        {
            DateTime dtFrom = DateTime.Parse(dtFromString);
            DateTime dtTo = DateTime.Parse(dtToString);
            TimeSpan timeSpan = dtTo - dtFrom;
            return (int)timeSpan.TotalMilliseconds;
        }

        #endregion

        #region 日期时间支持三（日期和时间部分分解组合）
        /// <summary>
        /// 通过时、分、秒得到时间串
        /// </summary>
        /// <param name="nHour">小时</param>
        /// <param name="nMinute">分钟</param>
        /// <param name="nSecond">秒</param>
        /// <returns>时间部分</returns>
        public static string EncodeTime(int nHour, int nMinute, int nSecond)
        {
            string sResult = nHour.ToString("00") + ":" + nMinute.ToString("00")
                    + ":" + nSecond.ToString("00");
            return sResult;
        }

        /// <summary>
        /// 通过年、月、日得到日期部分
        /// </summary>
        /// <param name="nYear">年份</param>
        /// <param name="nMonth">月份</param>
        /// <param name="nDay">日</param>
        /// <returns>日期部分</returns>
        public static string EncodeDate(int nYear, int nMonth, int nDay)
        {
            string sResult = nYear.ToString("0000") + "-" + nMonth.ToString("00")
                    + "-" + nDay.ToString("00");
            return sResult;
        }

        /// <summary>
        /// 得到日期部分
        /// </summary>
        /// <param name="sDatetime">日期时间</param>
        /// <returns>日期部分</returns>
        public static string GetDatePart(string sDatetime)
        {
            if (sDatetime.Length < 10)
                return "";
            return sDatetime.Substring(0, 10);
        }

        /// <summary>
        /// 得到时间部分
        /// </summary>
        /// <param name="sDatetime">日期时间</param>
        /// <returns>时间部分</returns>
        public static string GetTimePart(string sDatetime)
        {
            if (sDatetime.Length == 19)
                return sDatetime.Substring(11, 8);
            else
                return "00:00:00";
        }

        /// <summary>
        /// 得到一天的起始时间
        /// </summary>
        /// <param name="sDate">日期</param>
        /// <returns>起始时间</returns>
        public static string BeginTimeOfDay(string sDate)
        {
            if (sDate.Length > 10)
                sDate = sDate.Substring(0, 10);
            return sDate + " 00:00:00";
        }

        /// <summary>
        /// 得到一天的结束时间
        /// </summary>
        /// <param name="sDate">日期</param>
        /// <returns>结束时间</returns>
        public static string EndTimeOfDay(string sDate)
        {
            if (sDate.Length > 10)
                sDate = sDate.Substring(0, 10);
            return sDate + " 23:59:59.999";
        }
        #endregion

        #region 长度14的日期时间
        /// <summary>
        /// 由19位的日期时间转到14位的日期时间
        /// </summary>
        /// <param name="sDT">19位的日期时间</param>
        /// <returns>14位的日期时间</returns>
        public static string ToDateTime14Str(string sDT)
        {
            if (sDT == "")
                return "";

            if (sDT.Length != 19)
                throw new Exception("DateTimeUtil.ToDateTime14Str() error : 参数不是19位");

            string sRet = sDT.Replace(":", "").Replace("-", "").Replace(" ", "");
            if (sRet.Length != 14)
                throw new Exception("DateTimeUtil.ToDateTime14Str() error : 返回值不是14位");

            return sRet;
        }

        /// <summary>
        /// 由14位的日期时间转到19位的日期时间
        /// </summary>
        /// <param name="sDT14">14位的日期时间</param>
        /// <returns>19位的日期时间</returns>
        public static string FromDateTime14Str(string sDT14)
        {
            if (sDT14 == "")
                return "";
            if (sDT14.Length != 14)
                throw new Exception("DateTimeUtil.FromDateTime14Str() error : 参数不是14位");

            string sRet = sDT14.Substring(0, 4) + "-" + sDT14.Substring(4, 2)
                    + "-" + sDT14.Substring(6, 2)
                    + " " + sDT14.Substring(8, 2) + ":" + sDT14.Substring(10, 2)
                    + ":" + sDT14.Substring(12, 2);
            return sRet;
        }
        #endregion 长度14的日期时间

        #region 时间戳支持  Jim 20151106

        /// <summary>
        /// 当前时间的时间戳
        /// </summary>
        /// <returns></returns>
        public static string NowTimestamp()
        {
            return ToTimestamp(DateTime.Now);
        }

        /// <summary>
        /// 得到时间戳
        /// </summary>
        /// <param name="dt">DateTime值</param>
        /// <returns>时间戳</returns>
        public static string ToTimestamp(DateTime dt)
        {
            DateTime dtStartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return ((int)(dt - dtStartTime).TotalSeconds).ToString();
        }

        /// <summary>
        /// 得到时间戳
        /// </summary>
        /// <param name="sDateTimeString">时间日期字符串</param>
        /// <returns>时间戳</returns>
        public static string ToTimestamp(string sDateTimeString)
        {
            DateTime dt = ToDateTime(sDateTimeString);
            return ToTimestamp(dt);
        }

        /// <summary>
        /// 从时间戳得到日期时间字符串
        /// </summary>
        /// <param name="sTimestamp">时间戳</param>
        /// <remarks>时间日期字符串</remarks>
        public static string FromTimestamp(string sTimestamp)
        {
            return ToDateTimeStr(DTFromTimestamp(sTimestamp));
        }

        /// <summary>
        /// 从时间戳得到日期时间
        /// </summary>
        /// <param name="sTimestamp">时间戳</param>
        /// <remarks>时间日期字符串</remarks>
        public static DateTime DTFromTimestamp(string sTimestamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            long lTime = long.Parse(sTimestamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dtStart.Add(toNow);
        }

        #endregion

        #region 输出格式  Jim 20151108

        /// <summary>
        /// 得到不包括秒数的日期时间字符串
        /// </summary>
        /// <param name="sDateTimeString">19位日期时间字符串</param>
        /// <returns>2015-11-06 23:59</returns>
        /// <remarks></remarks>
        public static string VIVTimeStringWithoutSecond(string sDateTimeString)
        {
            if (sDateTimeString.Length != 19)
                return sDateTimeString;

            return sDateTimeString.Substring(0, 16);
        }

        /// <summary>
        /// 得到不包括年份和秒数的日期时间字符串
        /// </summary>
        /// <param name="sDateTimeString">19位日期时间字符串</param>
        /// <returns>11-06 23:59</returns>
        public static string VIVTimeStringtWithoutYearSecond(string sDateTimeString)
        {
            if (sDateTimeString.Length != 19)
                return sDateTimeString;

            return sDateTimeString.Substring(5, 11);
        }

        #endregion
    }
}


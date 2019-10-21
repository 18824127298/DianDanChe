
using System;
using System.Collections;

using System.Diagnostics;
using System.Globalization;

namespace Sigbit.Common
{
    /// <summary>
    /// ��ʱ��Ĵ������������ַ���ʱ���ʽ�Ĵ����ṩ֧��
    /// </summary>
    /// <remarks>
    /// ��DateTimeUtil�У������ڡ�ʱ��ͳһ���ض����ַ�����ʽ����
    /// ������ʱ�����19λ���ַ���������Ϊ��yyyy-mm-dd hh:mi:ss����
    /// ���ڲ���10λ���ַ���������Ϊ��yyyy-mm-dd����
    /// </remarks>
    public sealed class DateTimeUtil
    {
        #region ����ʱ��֧��һ����ʽת����
        /// <summary>
        /// �жϸ���������ʱ���ַ����Ƿ������Ӧ�ĸ�ʽ
        /// </summary>
        /// <param name="strDate">����ʱ���ַ���</param>
        /// <returns>�Ƿ���ϸ�ʽ</returns>
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
        /// �õ���ǰ�µĵ�һ��
        /// </summary>
        /// <returns>��ǰ�µĵ�һ��</returns>
        public static string ThisMonthFirstDate
        {
            get
            {
                DateTime dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// �õ���ǰ�µ����һ��
        /// </summary>
        /// <returns>��ǰ�µ����һ��</returns>
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
        /// ָ�����ڵĸ��µ�һ��
        /// </summary>
        /// <param name="sThisDate">ָ��������</param>
        /// <returns>���µ�һ��</returns>
        public static string MonthFirstDate(string sThisDate)
        {
            int nYear = Year(sThisDate);
            int nMonth = Month(sThisDate);
            int nDay = 1;

            return EncodeDate(nYear, nMonth, nDay) + " 00:00:00";
        }

        /// <summary>
        /// ָ�����ڵĸ������һ��
        /// </summary>
        /// <param name="sThisDate">ָ��������</param>
        /// <returns>�������һ��</returns>
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
        /// ת���ַ������͵�ʱ��������ʱ������
        /// </summary>
        /// <param name="strDateTime">ʱ���ַ���</param>
        /// <returns>����ʱ�����͵�ֵ</returns>
        /// <remarks>�����ʽ����ȷ���򷵻ص�ǰ��ʱ��</remarks>
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
        /// ת��ʱ�����͵��ַ���������ʱ������
        /// </summary>
        /// <param name="strTime">ʱ���ַ���</param>
        /// <returns>ʱ��ֵ</returns>
        /// <remarks>�����ʽ����ȷ���򷵻ص���"00:00:00"��ʱ��ֵ</remarks>
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
        /// ��ʱ��ֵת����ʱ���ַ���
        /// </summary>
        /// <param name="time">ʱ��ֵ</param>
        /// <returns>ʱ���ַ���</returns>
        public static string ToTimeStr(DateTime time)
        {
            return time.ToString("HH:mm:ss");
        }

        /// <summary>
        /// ������ת��Ϊָ����ʽ���ַ���
        /// </summary>
        /// <param name="time">��ת���Ķ���</param>
        /// <param name="sFormat">ָ�����ַ�����ʽ</param>
        /// <returns>ת������ַ���</returns>
        /// <remarks>���Ϊ��ֵ���������ʱ������򷵻�""</remarks>
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
        /// ������ת��Ϊ�ַ�������ʽΪ"HH:mm:ss"
        /// </summary>
        /// <param name="time">��ת���Ķ���</param>
        /// <returns>ת������ַ���</returns>
        public static string ToTimeStr(object time)
        {
            return ToTimeStr(time, "HH:mm:ss");
        }

        /// <summary>
        /// ������ת��Ϊʱ���ַ���
        /// </summary>
        /// <param name="Second">����</param>
        /// <returns>ʱ���ַ���</returns>
        public static string ToTimeStrFromSecond(int Second)
        {
            //=========== 1. �õ�Сʱ�����Ӻ����� ===========
            string sTimeStr = "";

            int NewSecond = 0;
            int hour = Math.DivRem(Second, 3600, out NewSecond);    //Сʱ

            Second = NewSecond;
            NewSecond = 0;
            int minute = Math.DivRem(Second, 60, out NewSecond);    //����

            //============ 2. �õ����ص��ַ��� ============
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
        /// ��ʱ���ַ���ת��Ϊ����
        /// </summary>
        /// <param name="timeStr">ʱ���ַ���</param>
        /// <returns>ת���������</returns>
        public static int ToSecondsFromTimeStr(string timeStr)
        {
            DateTime dt = ToTime(timeStr);
            return dt.Hour * 3600 + dt.Minute * 60 + dt.Second;
        }

        /// <summary>
        /// ������ʱ��ֵת��Ϊ�����ַ���
        /// </summary>
        /// <param name="dt">����ʱ��ֵ</param>
        /// <returns>�����ַ���</returns>
        public static string ToDateStr(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// ������ת��Ϊ�����ַ���
        /// </summary>
        /// <param name="dt">����</param>
        /// <param name="defaultStr">ȱʡ�ַ���</param>
        /// <returns>ת������ַ��������Ϊ��ֵ����ȱʡֵ����</returns>
        public static string ToDateStr(object dt, string defaultStr)
        {
            if (dt == null || dt is System.DBNull)
                return defaultStr;
            else
                return ((DateTime)dt).ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// ������ת��Ϊ�����ַ���
        /// </summary>
        /// <param name="dt">����</param>
        /// <returns>ת������ַ��������ת�����ɹ����򷵻�""</returns>
        public static string ToDateStr(object dt)
        {
            return ToDateStr(dt, "");
        }

        /// <summary>
        /// ������ʱ��ֵת��Ϊ�ַ���
        /// </summary>
        /// <param name="dt">����ʱ��ֵ</param>
        /// <returns>�ַ���(len=19)</returns>
        public static string ToDateTimeStr(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// ת��ʱ��ֵ���������ַ���
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateTimeStrWithMilliSeconds(DateTime dt)
        {
            int nMilliSeconds = dt.Millisecond;
            return dt.ToString("yyyy-MM-dd HH:mm:ss") + "." + nMilliSeconds.ToString("000");
        }

        /// <summary>
        /// ������ת��Ϊ����ʱ���ַ���
        /// </summary>
        /// <param name="dt">����</param>
        /// <param name="defaultStr">ת�����ɹ�ʱ��ȡ��ȱʡ�ַ���</param>
        /// <returns>ת���������ʱ���ַ���(len=19)</returns>
        public static string ToDateTimeStr(object dt, string defaultStr)
        {
            return ToDateTimeStr(dt, defaultStr, "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// ������ת��Ϊ����ʱ���ַ���
        /// </summary>
        /// <param name="dt">����</param>
        /// <returns>ת���������ʱ���ַ���(len=19)</returns>
        /// <remarks>���ת�����ɹ����򷵻�""</remarks>
        public static string ToDateTimeStr(object dt)
        {
            return ToDateTimeStr(dt, "", "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// ������ת��Ϊָ����ʽ������ʱ���ַ���
        /// </summary>
        /// <param name="dt">����</param>
        /// <param name="defaultStr">ȱʡ�ַ���</param>
        /// <param name="sFormat">��ʽ</param>
        /// <returns>ת������ַ���</returns>
        public static string ToDateTimeStr(object dt, string defaultStr, string sFormat)
        {
            if (dt == null || dt is System.DBNull)
                return defaultStr;
            else
                return ((DateTime)dt).ToString(sFormat);
        }


        #endregion

        #region ����ʱ��֧�ֶ���ʵ�����̣�

        /// <summary>
        /// ��ȡ��ǰ��ʱ��
        /// </summary>
        public static string Now
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// �����벿�ֵĵ�ǰʱ�䣬ʾ��Ϊ2003.07.22 10:02:52.136
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
        /// ��ȡ��ǰ��ʱ��
        /// </summary>
        public static string Today
        {
            get
            {
                return DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// ��ȡ���ڵ����
        /// </summary>
        /// <param name="dtString">����ʱ�䴮</param>
        /// <returns>���</returns>
        public static int Year(string dtString)
        {
            int nYear;

            nYear = Convert.ToInt32(dtString.Substring(0, 4));
            Debug.Assert(nYear > 1900 && nYear < 2100);

            return nYear;
        }

        /// <summary>
        /// ��ȡ���ڵ��·�
        /// </summary>
        /// <param name="dtString">����ʱ�䴮</param>
        /// <returns>�·�</returns>
        public static int Month(string dtString)
        {
            int nMonth;

            nMonth = Convert.ToInt32(dtString.Substring(5, 2));
            Debug.Assert(nMonth >= 1 && nMonth <= 12);

            return nMonth;
        }

        /// <summary>
        /// ��ȡ�����и��µĵڼ���
        /// </summary>
        /// <param name="dtString">����ʱ�䴮</param>
        /// <returns>���µĵڼ���</returns>
        public static int Day(string dtString)
        {
            int nDay;

            nDay = Convert.ToInt32(dtString.Substring(8, 2));
            Debug.Assert(nDay >= 1 && nDay <= 31);

            return nDay;
        }

        /// <summary>
        /// ��ȡ�����е�Сʱ����
        /// </summary>
        /// <param name="dtString">����ʱ�䴮</param>
        /// <returns>Сʱ����</returns>
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
        /// ��ȡ�����еķ��Ӳ���
        /// </summary>
        /// <param name="dtString">����ʱ�䴮</param>
        /// <returns>���Ӳ���</returns>
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
        /// ��ȡ�����е��벿��
        /// </summary>
        /// <param name="dtString">����ʱ�䴮</param>
        /// <returns>�벿��</returns>
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
        /// ��ȡ��ʾ�����������ڼ�
        /// </summary>
        /// <param name="dtString">����ʱ�䴮</param>
        /// <returns>���ڼ�</returns>
        /// <remarks>����ֵ��0��6��������Ϊ0��������Ϊ6</remarks>
        public static int DayOfWeek(string dtString)
        {
            DateTime dt = DateTime.Parse(dtString);
            return (int)dt.DayOfWeek;
        }

        /// <summary>
        /// ��ȡ��ʾ��������һ���еĵڼ���
        /// </summary>
        /// <param name="dtString">����ʱ�䴮</param>
        /// <returns>һ���еĵڼ���</returns>
        public static int DayOfYear(string dtString)
        {
            DateTime dt = DateTime.Parse(dtString);
            return dt.DayOfYear;
        }

        /// <summary>
        /// ��ָ�����·����ӵ�����ֵ��
        /// </summary>
        /// <param name="dtString">ԭ��������</param>
        /// <param name="nOffset">ָ�����·���</param>
        /// <returns>�õ�������</returns>
        public static string AddMonths(string dtString, int nOffset)
        {
            DateTime dt = DateTime.Parse(dtString);
            DateTime dtRes = dt.AddMonths(nOffset);
            return dtRes.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// ��ָ���������ӵ�����ֵ��
        /// </summary>
        /// <param name="dtString">ԭ��������</param>
        /// <param name="offset">ָ��������</param>
        /// <returns>�õ�������</returns>
        /// <remarks>��������Ϊ����</remarks>
        public static string AddDays(string dtString, int offset)
        {
            DateTime dt = DateTime.Parse(dtString);
            DateTime dtRes = dt.AddDays(offset);
            return dtRes.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// ��ָ���������ӵ�����ֵ��
        /// </summary>
        /// <param name="dtString">ԭ��������ʱ��</param>
        /// <param name="offset">ָ��������</param>
        /// <returns>�õ�������ʱ��</returns>
        /// <remarks>��������Ϊ����</remarks>
        public static string AddSeconds(string dtString, int offset)
        {
            DateTime dt = DateTime.Parse(dtString);
            DateTime dtRes = dt.AddSeconds(offset);
            return dtRes.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// ����ָ���ĺ�����
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
        /// ����ָ��������
        /// </summary>
        /// <param name="dtString">ʱ�䴮</param>
        /// <param name="offset">���ӵ�����</param>
        /// <returns>����ָ���������ʱ��</returns>
        public static string AddMilliSeconds(string dtString, double offset)
        {
            DateTime dt = DateTime.Parse(dtString);
            DateTime dtRes = dt.AddMilliseconds(offset);

            int nMilliSeconds = dtRes.Millisecond;
            return dtRes.ToString("yyyy-MM-dd HH:mm:ss") + "." + nMilliSeconds.ToString("000");
        }

        /// <summary>
        /// �õ���������ʱ��֮����������
        /// </summary>
        /// <param name="dtFromString">��ʼ����</param>
        /// <param name="dtToString">��ֹ����</param>
        /// <returns>��������</returns>
        /// <remarks>
        /// ���ж�ʱ��������ֻ�ж������Ƿ��л�����ʹʱ�����������ӣ�
        /// ֻҪ�л������ڣ�Ҳ�����������������磬2005-05-05 23:58:58��
        /// 2005-05-06 00:02:03֮�����������Ҳ��1��
        /// </remarks>
        public static int DaysAfter(string dtFromString, string dtToString)
        {
            DateTime dtFrom = DateTime.Parse(dtFromString).Date;
            DateTime dtTo = DateTime.Parse(dtToString).Date;
            TimeSpan timeSpan = dtTo - dtFrom;
            return (int)timeSpan.TotalDays;
        }

        /// <summary>
        /// �õ���������ʱ��֮����������
        /// </summary>
        /// <param name="dtFromString">��ʼ����ʱ��</param>
        /// <param name="dtToString">��ֹ����ʱ��</param>
        /// <returns>��������</returns>
        public static int SecondsAfter(string dtFromString, string dtToString)
        {
            DateTime dtFrom = DateTime.Parse(dtFromString);
            DateTime dtTo = DateTime.Parse(dtToString);
            TimeSpan timeSpan = dtTo - dtFrom;
            return (int)timeSpan.TotalSeconds;
        }

        /// <summary>
        /// �õ���������ʱ��֮�����ĺ�����
        /// </summary>
        /// <param name="dtFromString">��ʼ����ʱ��</param>
        /// <param name="dtToString">��ֹ����ʱ��</param>
        /// <returns>���ĺ�����</returns>
        public static int MilliSecondsAfter(string dtFromString, string dtToString)
        {
            DateTime dtFrom = DateTime.Parse(dtFromString);
            DateTime dtTo = DateTime.Parse(dtToString);
            TimeSpan timeSpan = dtTo - dtFrom;
            return (int)timeSpan.TotalMilliseconds;
        }

        #endregion

        #region ����ʱ��֧���������ں�ʱ�䲿�ַֽ���ϣ�
        /// <summary>
        /// ͨ��ʱ���֡���õ�ʱ�䴮
        /// </summary>
        /// <param name="nHour">Сʱ</param>
        /// <param name="nMinute">����</param>
        /// <param name="nSecond">��</param>
        /// <returns>ʱ�䲿��</returns>
        public static string EncodeTime(int nHour, int nMinute, int nSecond)
        {
            string sResult = nHour.ToString("00") + ":" + nMinute.ToString("00")
                    + ":" + nSecond.ToString("00");
            return sResult;
        }

        /// <summary>
        /// ͨ���ꡢ�¡��յõ����ڲ���
        /// </summary>
        /// <param name="nYear">���</param>
        /// <param name="nMonth">�·�</param>
        /// <param name="nDay">��</param>
        /// <returns>���ڲ���</returns>
        public static string EncodeDate(int nYear, int nMonth, int nDay)
        {
            string sResult = nYear.ToString("0000") + "-" + nMonth.ToString("00")
                    + "-" + nDay.ToString("00");
            return sResult;
        }

        /// <summary>
        /// �õ����ڲ���
        /// </summary>
        /// <param name="sDatetime">����ʱ��</param>
        /// <returns>���ڲ���</returns>
        public static string GetDatePart(string sDatetime)
        {
            if (sDatetime.Length < 10)
                return "";
            return sDatetime.Substring(0, 10);
        }

        /// <summary>
        /// �õ�ʱ�䲿��
        /// </summary>
        /// <param name="sDatetime">����ʱ��</param>
        /// <returns>ʱ�䲿��</returns>
        public static string GetTimePart(string sDatetime)
        {
            if (sDatetime.Length == 19)
                return sDatetime.Substring(11, 8);
            else
                return "00:00:00";
        }

        /// <summary>
        /// �õ�һ�����ʼʱ��
        /// </summary>
        /// <param name="sDate">����</param>
        /// <returns>��ʼʱ��</returns>
        public static string BeginTimeOfDay(string sDate)
        {
            if (sDate.Length > 10)
                sDate = sDate.Substring(0, 10);
            return sDate + " 00:00:00";
        }

        /// <summary>
        /// �õ�һ��Ľ���ʱ��
        /// </summary>
        /// <param name="sDate">����</param>
        /// <returns>����ʱ��</returns>
        public static string EndTimeOfDay(string sDate)
        {
            if (sDate.Length > 10)
                sDate = sDate.Substring(0, 10);
            return sDate + " 23:59:59.999";
        }
        #endregion

        #region ����14������ʱ��
        /// <summary>
        /// ��19λ������ʱ��ת��14λ������ʱ��
        /// </summary>
        /// <param name="sDT">19λ������ʱ��</param>
        /// <returns>14λ������ʱ��</returns>
        public static string ToDateTime14Str(string sDT)
        {
            if (sDT == "")
                return "";

            if (sDT.Length != 19)
                throw new Exception("DateTimeUtil.ToDateTime14Str() error : ��������19λ");

            string sRet = sDT.Replace(":", "").Replace("-", "").Replace(" ", "");
            if (sRet.Length != 14)
                throw new Exception("DateTimeUtil.ToDateTime14Str() error : ����ֵ����14λ");

            return sRet;
        }

        /// <summary>
        /// ��14λ������ʱ��ת��19λ������ʱ��
        /// </summary>
        /// <param name="sDT14">14λ������ʱ��</param>
        /// <returns>19λ������ʱ��</returns>
        public static string FromDateTime14Str(string sDT14)
        {
            if (sDT14 == "")
                return "";
            if (sDT14.Length != 14)
                throw new Exception("DateTimeUtil.FromDateTime14Str() error : ��������14λ");

            string sRet = sDT14.Substring(0, 4) + "-" + sDT14.Substring(4, 2)
                    + "-" + sDT14.Substring(6, 2)
                    + " " + sDT14.Substring(8, 2) + ":" + sDT14.Substring(10, 2)
                    + ":" + sDT14.Substring(12, 2);
            return sRet;
        }
        #endregion ����14������ʱ��

        #region ʱ���֧��  Jim 20151106

        /// <summary>
        /// ��ǰʱ���ʱ���
        /// </summary>
        /// <returns></returns>
        public static string NowTimestamp()
        {
            return ToTimestamp(DateTime.Now);
        }

        /// <summary>
        /// �õ�ʱ���
        /// </summary>
        /// <param name="dt">DateTimeֵ</param>
        /// <returns>ʱ���</returns>
        public static string ToTimestamp(DateTime dt)
        {
            DateTime dtStartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return ((int)(dt - dtStartTime).TotalSeconds).ToString();
        }

        /// <summary>
        /// �õ�ʱ���
        /// </summary>
        /// <param name="sDateTimeString">ʱ�������ַ���</param>
        /// <returns>ʱ���</returns>
        public static string ToTimestamp(string sDateTimeString)
        {
            DateTime dt = ToDateTime(sDateTimeString);
            return ToTimestamp(dt);
        }

        /// <summary>
        /// ��ʱ����õ�����ʱ���ַ���
        /// </summary>
        /// <param name="sTimestamp">ʱ���</param>
        /// <remarks>ʱ�������ַ���</remarks>
        public static string FromTimestamp(string sTimestamp)
        {
            return ToDateTimeStr(DTFromTimestamp(sTimestamp));
        }

        /// <summary>
        /// ��ʱ����õ�����ʱ��
        /// </summary>
        /// <param name="sTimestamp">ʱ���</param>
        /// <remarks>ʱ�������ַ���</remarks>
        public static DateTime DTFromTimestamp(string sTimestamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            long lTime = long.Parse(sTimestamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dtStart.Add(toNow);
        }

        #endregion

        #region �����ʽ  Jim 20151108

        /// <summary>
        /// �õ�����������������ʱ���ַ���
        /// </summary>
        /// <param name="sDateTimeString">19λ����ʱ���ַ���</param>
        /// <returns>2015-11-06 23:59</returns>
        /// <remarks></remarks>
        public static string VIVTimeStringWithoutSecond(string sDateTimeString)
        {
            if (sDateTimeString.Length != 19)
                return sDateTimeString;

            return sDateTimeString.Substring(0, 16);
        }

        /// <summary>
        /// �õ���������ݺ�����������ʱ���ַ���
        /// </summary>
        /// <param name="sDateTimeString">19λ����ʱ���ַ���</param>
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

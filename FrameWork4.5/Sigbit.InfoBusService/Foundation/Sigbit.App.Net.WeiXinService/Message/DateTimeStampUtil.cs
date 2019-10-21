using System;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.App.Net.WeiXinService.Message
{
    class DateTimeStampUtil
    {
        /// <summary>
        /// 转换时间
        /// </summary>
        /// <param name="dtTime"></param>
        /// <returns></returns>
        public static int ConvertToTimeStamp(DateTime dtTime)
        {
            DateTime dtBeginTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            int nRet = (int)(dtTime - dtBeginTime).TotalSeconds;

            return nRet;
        }

        public static DateTime ConvertToDateTime(int nTimeStamp)
        {
            DateTime dtBegin = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long nTicks = long.Parse(nTimeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(nTicks);
            DateTime dtRet = dtBegin.Add(toNow);

            return dtRet;
        }

    }
}

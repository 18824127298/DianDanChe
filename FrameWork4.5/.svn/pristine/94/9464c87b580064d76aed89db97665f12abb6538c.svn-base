using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

using Sigbit.Common;

namespace Sigbit.Framework
{
    public class SbtTaskRuleItem
    {
        protected string _daemonType = "";
        protected string _daemonTypeName = "";
        protected string _daemonComponent = "";
        protected string _daemonClass = "";
        protected int _daemonPriority;
        protected SbtTaskIntervalType _intervalType;
        protected int _intervalValue;
        protected int _activeIntervalValue;
        protected int _batchSize;

        /// <summary>
        /// 后台处理的类型，主键
        /// </summary>
        public string DaemonType
        {
            get
            {
                return _daemonType;
            }
            set
            {
                _daemonType = value;
            }
        }

        /// <summary>
        /// 后台处理类型的名称
        /// </summary>
        public string DaemonTypeName
        {
            get
            {
                return _daemonTypeName;
            }
            set
            {
                _daemonTypeName = value;
            }
        }

        /// <summary>
        /// 实现组件的名称
        /// </summary>
        public string DaemonComponent
        {
            get
            {
                return _daemonComponent;
            }
            set
            {
                _daemonComponent = value;
            }
        }

        /// <summary>
        /// 后台处理的类名
        /// </summary>
        public string DaemonClass
        {
            get
            {
                return _daemonClass;
            }
            set
            {
                _daemonClass = value;
            }
        }

        /// <summary>
        /// 运行优先级，从1至9
        /// </summary>
        public int DaemonPriority
        {
            get
            {
                return _daemonPriority;
            }
            set
            {
                _daemonPriority = value;
            }
        }

        /// <summary>
        /// 后台处理的时间间隔类型
        /// </summary>
        public SbtTaskIntervalType IntervalType
        {
            get
            {
                return _intervalType;
            }
            set
            {
                _intervalType = value;
            }
        }

        /// <summary>
        /// 具体的间隔值
        /// </summary>
        public int IntervalValue
        {
            get
            {
                return _intervalValue;
            }
            set
            {
                _intervalValue = value;
            }
        }

        /// <summary>
        /// 当激活时的时间间隔
        /// </summary>
        public int ActiveIntervalValue
        {
            get
            {
                return _activeIntervalValue;
            }
            set
            {
                _activeIntervalValue = value;
            }
        }

        /// <summary>
        /// 配置的批次大小（可不配）
        /// </summary>
        public int BatchSize
        {
            get
            {
                return _batchSize;
            }
            set
            {
                _batchSize = value;
            }
        }

        public void FromTbSysDaemonType(TbSysDaemonType tbl)
        {
            _daemonType = tbl.DaemonType;
            _daemonTypeName = tbl.DaemonTypeName;
            _daemonComponent = tbl.DaemonComponent;
            _daemonClass = tbl.DaemonClass;
            _daemonPriority = tbl.DaemonPriority;
            _intervalType = SbtTaskConvert.IntervalTypeStringToEnum(tbl.IntervalType);
            _intervalValue = tbl.IntervalValue;
            _activeIntervalValue = tbl.ActiveIntervalValue;
            _batchSize = tbl.BatchSize;
        }

        /// <summary>
        /// 根据规则，得到下一个任务的时间
        /// </summary>
        /// <param name="sBaseTime">基准时间，一般是当前时间或本次任务开始的时间</param>
        /// <param name="bIsActive">任务是否处理激活状态</param>
        /// <returns>下一个任务的时间</returns>
        public string GetNextTaskTime(string sBaseTime, bool bIsActive)
        {
            if (bIsActive)
                return DateTimeUtil.AddSeconds(sBaseTime, this.ActiveIntervalValue);

            switch (this.IntervalType)
            {
                case SbtTaskIntervalType.Interval:
                    return DateTimeUtil.AddSeconds(sBaseTime, this.IntervalValue);
                case SbtTaskIntervalType.Day:
                    return GetNextTaskTime__Day(sBaseTime);
                case SbtTaskIntervalType.Month:
                    return GetNextTaskTime__Month(sBaseTime);
                case SbtTaskIntervalType.Week:
                    return GetNextTaskTime__Week(sBaseTime);
                default:
                    throw new Exception("SbtTaskRuleItem.GetNextTaskTime() : " 
                            + "unexpected IntervalType");
            }
        }

        /// <summary>
        /// 根据"每日"规则，得到下一个任务的时间
        /// </summary>
        /// <param name="sBaseTime">基准时间</param>
        /// <returns>下一个任务的时间</returns>
        private string GetNextTaskTime__Day(string sBaseTime)
        {
            //===== 1. 得到计划的时间部分 =======
            int nHHMMSS = this.IntervalValue;
            int nHour = nHHMMSS / 10000;
            int nMinute = (nHHMMSS - nHour * 10000) / 100;
            int nSecond = nHHMMSS % 100;
            Debug.Assert(nHour >= 0 && nHour <= 23);
            Debug.Assert(nMinute >= 0 && nMinute <= 59);
            Debug.Assert(nSecond >= 0 && nSecond <= 59);
            string sTimePart = DateTimeUtil.EncodeTime(nHour, nMinute, nSecond);

            //===== 2. 得到基准时间的日期部分 =======
            string sDatePart = DateTimeUtil.GetDatePart(sBaseTime);

            //======== 3. 得到计划的日期时间 ========
            string sPlanDatetime = sDatePart + " " + sTimePart;

            //====== 4. 如果计划的日期时间还早于基准时间，则往后推一天 ====
            if (sPlanDatetime.CompareTo(sBaseTime) <= 0)
                sPlanDatetime = DateTimeUtil.AddDays(sPlanDatetime, 1);

            return sPlanDatetime;
        }

        /// <summary>
        /// 根据"每月"规则，得到下一个任务的时间
        /// </summary>
        /// <param name="sBaseTime">基准时间</param>
        /// <returns>下一个任务的时间</returns>
        private string GetNextTaskTime__Month(string sBaseTime)
        {
            //======== 1. 分解日和时间 =========
            int nDayHHMMSS = this.IntervalValue;
            int nHHMMSS = nDayHHMMSS % 1000000;
            int nDay = nDayHHMMSS / 1000000;

            //===== 2. 得到计划的时间部分 =======
            int nHour = nHHMMSS / 10000;
            int nMinute = (nHHMMSS - nHour * 10000) / 100;
            int nSecond = nHHMMSS % 100;
            Debug.Assert(nHour >= 0 && nHour <= 23);
            Debug.Assert(nMinute >= 0 && nMinute <= 59);
            Debug.Assert(nSecond >= 0 && nSecond <= 59);
            string sTimePart = DateTimeUtil.EncodeTime(nHour, nMinute, nSecond);

            //===== 3. 得到基准时间的年、月 =======
            int nYear = DateTimeUtil.Year(sBaseTime);
            int nMonth = DateTimeUtil.Month(sBaseTime);

            //======== 4. 得到计划的日期时间 ========
            string sDatePart = DateTimeUtil.EncodeDate(nYear, nMonth, nDay);
            string sPlanDatetime = sDatePart + " " + sTimePart;

            //====== 5. 如果计划的日期时间还早于基准时间，则往后推一个月 ====
            if (sPlanDatetime.CompareTo(sBaseTime) <= 0)
                sPlanDatetime = DateTimeUtil.AddMonths(sPlanDatetime, 1);

            return sPlanDatetime;
        }

        /// <summary>
        /// 根据"每周"规则，得到下一个任务的时间
        /// </summary>
        /// <param name="sBaseTime">基准时间</param>
        /// <returns>下一个任务的时间</returns>
        private string GetNextTaskTime__Week(string sBaseTime)
        {
            //======== 1. 分解日和时间 =========
            int nWeekHHMMSS = this.IntervalValue;
            int nHHMMSS = nWeekHHMMSS % 1000000;
            int nWeek = nWeekHHMMSS / 1000000;

            //===== 2. 得到计划的时间部分 =======
            int nHour = nHHMMSS / 10000;
            int nMinute = (nHHMMSS - nHour * 10000) / 100;
            int nSecond = nHHMMSS % 100;
            Debug.Assert(nHour >= 0 && nHour <= 23);
            Debug.Assert(nMinute >= 0 && nMinute <= 59);
            Debug.Assert(nSecond >= 0 && nSecond <= 59);
            string sTimePart = DateTimeUtil.EncodeTime(nHour, nMinute, nSecond);

            //===== 2. 得到基准时间的日期部分 =======
            string sDatePart = DateTimeUtil.GetDatePart(sBaseTime);

            //======== 3. 得到计划的日期时间（未进行周日期调整） ========
            string sPlanDatetime = sDatePart + " " + sTimePart;

            //======= 4. 进行周的日期调整 ===========
            //======= 4.1 得到基准时间是星期几 ==========
            int nCurrentWeek = DateTimeUtil.DayOfWeek(sPlanDatetime);
            if (nCurrentWeek == 0)
                nCurrentWeek = 7;

            //======== 4.2 与计划的时间结合，算出要往后延多少天 =========
            int nDelayDays;
            if (nWeek >= nCurrentWeek)
                nDelayDays = nWeek - nCurrentWeek;
            else
                nDelayDays = 7 + nWeek - nCurrentWeek;
                
            //======== 4.3 往后延，直到结束 ===========
            sPlanDatetime = DateTimeUtil.AddDays(sPlanDatetime, nDelayDays);

            //====== 5. 如果计划的日期时间还早于基准时间，则往后推一周 ====
            if (sPlanDatetime.CompareTo(sBaseTime) <= 0)
                sPlanDatetime = DateTimeUtil.AddMonths(sPlanDatetime, 7);

            return sPlanDatetime;
        }

        /// <summary>
        /// 生成任务表的一条记录数据
        /// </summary>
        /// <param name="sBaseTime">基准时间</param>
        /// <param name="bIsActive">是否激活</param>
        /// <returns>任务表的记录数据</returns>
        public TbSysDaemonTask GenerateTbTaskRecord(string sBaseTime, bool bIsActive)
        {
            TbSysDaemonTask tbl = new TbSysDaemonTask();

            tbl.DaemonTaskUid = Guid.NewGuid().ToString();
            tbl.DaemonType = this.DaemonType;
            tbl.TaskName = this.DaemonTypeName;
            tbl.TaskParameter = "";
            tbl.PlanTime = GetNextTaskTime(sBaseTime, bIsActive);
            tbl.TaskStatus = TbSysDaemonTaskFTaskStatus.Idle;
            tbl.Paused = "N";
            tbl.TaskPriority = this.DaemonPriority;
            tbl.ActiveIntervalValue = this.ActiveIntervalValue;
            tbl.BatchSize = this.BatchSize;

            return tbl;
        }
    }
}

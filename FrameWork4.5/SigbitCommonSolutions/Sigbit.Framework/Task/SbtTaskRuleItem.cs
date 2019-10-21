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
        /// ��̨��������ͣ�����
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
        /// ��̨�������͵�����
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
        /// ʵ�����������
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
        /// ��̨���������
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
        /// �������ȼ�����1��9
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
        /// ��̨�����ʱ��������
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
        /// ����ļ��ֵ
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
        /// ������ʱ��ʱ����
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
        /// ���õ����δ�С���ɲ��䣩
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
        /// ���ݹ��򣬵õ���һ�������ʱ��
        /// </summary>
        /// <param name="sBaseTime">��׼ʱ�䣬һ���ǵ�ǰʱ��򱾴�����ʼ��ʱ��</param>
        /// <param name="bIsActive">�����Ƿ�����״̬</param>
        /// <returns>��һ�������ʱ��</returns>
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
        /// ����"ÿ��"���򣬵õ���һ�������ʱ��
        /// </summary>
        /// <param name="sBaseTime">��׼ʱ��</param>
        /// <returns>��һ�������ʱ��</returns>
        private string GetNextTaskTime__Day(string sBaseTime)
        {
            //===== 1. �õ��ƻ���ʱ�䲿�� =======
            int nHHMMSS = this.IntervalValue;
            int nHour = nHHMMSS / 10000;
            int nMinute = (nHHMMSS - nHour * 10000) / 100;
            int nSecond = nHHMMSS % 100;
            Debug.Assert(nHour >= 0 && nHour <= 23);
            Debug.Assert(nMinute >= 0 && nMinute <= 59);
            Debug.Assert(nSecond >= 0 && nSecond <= 59);
            string sTimePart = DateTimeUtil.EncodeTime(nHour, nMinute, nSecond);

            //===== 2. �õ���׼ʱ������ڲ��� =======
            string sDatePart = DateTimeUtil.GetDatePart(sBaseTime);

            //======== 3. �õ��ƻ�������ʱ�� ========
            string sPlanDatetime = sDatePart + " " + sTimePart;

            //====== 4. ����ƻ�������ʱ�仹���ڻ�׼ʱ�䣬��������һ�� ====
            if (sPlanDatetime.CompareTo(sBaseTime) <= 0)
                sPlanDatetime = DateTimeUtil.AddDays(sPlanDatetime, 1);

            return sPlanDatetime;
        }

        /// <summary>
        /// ����"ÿ��"���򣬵õ���һ�������ʱ��
        /// </summary>
        /// <param name="sBaseTime">��׼ʱ��</param>
        /// <returns>��һ�������ʱ��</returns>
        private string GetNextTaskTime__Month(string sBaseTime)
        {
            //======== 1. �ֽ��պ�ʱ�� =========
            int nDayHHMMSS = this.IntervalValue;
            int nHHMMSS = nDayHHMMSS % 1000000;
            int nDay = nDayHHMMSS / 1000000;

            //===== 2. �õ��ƻ���ʱ�䲿�� =======
            int nHour = nHHMMSS / 10000;
            int nMinute = (nHHMMSS - nHour * 10000) / 100;
            int nSecond = nHHMMSS % 100;
            Debug.Assert(nHour >= 0 && nHour <= 23);
            Debug.Assert(nMinute >= 0 && nMinute <= 59);
            Debug.Assert(nSecond >= 0 && nSecond <= 59);
            string sTimePart = DateTimeUtil.EncodeTime(nHour, nMinute, nSecond);

            //===== 3. �õ���׼ʱ����ꡢ�� =======
            int nYear = DateTimeUtil.Year(sBaseTime);
            int nMonth = DateTimeUtil.Month(sBaseTime);

            //======== 4. �õ��ƻ�������ʱ�� ========
            string sDatePart = DateTimeUtil.EncodeDate(nYear, nMonth, nDay);
            string sPlanDatetime = sDatePart + " " + sTimePart;

            //====== 5. ����ƻ�������ʱ�仹���ڻ�׼ʱ�䣬��������һ���� ====
            if (sPlanDatetime.CompareTo(sBaseTime) <= 0)
                sPlanDatetime = DateTimeUtil.AddMonths(sPlanDatetime, 1);

            return sPlanDatetime;
        }

        /// <summary>
        /// ����"ÿ��"���򣬵õ���һ�������ʱ��
        /// </summary>
        /// <param name="sBaseTime">��׼ʱ��</param>
        /// <returns>��һ�������ʱ��</returns>
        private string GetNextTaskTime__Week(string sBaseTime)
        {
            //======== 1. �ֽ��պ�ʱ�� =========
            int nWeekHHMMSS = this.IntervalValue;
            int nHHMMSS = nWeekHHMMSS % 1000000;
            int nWeek = nWeekHHMMSS / 1000000;

            //===== 2. �õ��ƻ���ʱ�䲿�� =======
            int nHour = nHHMMSS / 10000;
            int nMinute = (nHHMMSS - nHour * 10000) / 100;
            int nSecond = nHHMMSS % 100;
            Debug.Assert(nHour >= 0 && nHour <= 23);
            Debug.Assert(nMinute >= 0 && nMinute <= 59);
            Debug.Assert(nSecond >= 0 && nSecond <= 59);
            string sTimePart = DateTimeUtil.EncodeTime(nHour, nMinute, nSecond);

            //===== 2. �õ���׼ʱ������ڲ��� =======
            string sDatePart = DateTimeUtil.GetDatePart(sBaseTime);

            //======== 3. �õ��ƻ�������ʱ�䣨δ���������ڵ����� ========
            string sPlanDatetime = sDatePart + " " + sTimePart;

            //======= 4. �����ܵ����ڵ��� ===========
            //======= 4.1 �õ���׼ʱ�������ڼ� ==========
            int nCurrentWeek = DateTimeUtil.DayOfWeek(sPlanDatetime);
            if (nCurrentWeek == 0)
                nCurrentWeek = 7;

            //======== 4.2 ��ƻ���ʱ���ϣ����Ҫ�����Ӷ����� =========
            int nDelayDays;
            if (nWeek >= nCurrentWeek)
                nDelayDays = nWeek - nCurrentWeek;
            else
                nDelayDays = 7 + nWeek - nCurrentWeek;
                
            //======== 4.3 �����ӣ�ֱ������ ===========
            sPlanDatetime = DateTimeUtil.AddDays(sPlanDatetime, nDelayDays);

            //====== 5. ����ƻ�������ʱ�仹���ڻ�׼ʱ�䣬��������һ�� ====
            if (sPlanDatetime.CompareTo(sBaseTime) <= 0)
                sPlanDatetime = DateTimeUtil.AddMonths(sPlanDatetime, 7);

            return sPlanDatetime;
        }

        /// <summary>
        /// ����������һ����¼����
        /// </summary>
        /// <param name="sBaseTime">��׼ʱ��</param>
        /// <param name="bIsActive">�Ƿ񼤻�</param>
        /// <returns>�����ļ�¼����</returns>
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

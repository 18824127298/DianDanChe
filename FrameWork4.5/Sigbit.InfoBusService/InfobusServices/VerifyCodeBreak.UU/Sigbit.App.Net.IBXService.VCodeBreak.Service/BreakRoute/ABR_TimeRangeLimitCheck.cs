using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;
using Sigbit.Data;

using Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Service.BreakRoute
{
    class ABR_TimeRangeLimitCheck
    {
        private TbAuthenUser _inputAuthenUserRec = null;
        /// <summary>
        /// 授权用户记录。每日、小时、分钟的限制。
        /// </summary>
        public TbAuthenUser InputAuthenUserRec
        {
            get { return _inputAuthenUserRec; }
            set { _inputAuthenUserRec = value; }
        }

        private bool _outputIsForbidden = false;
        /// <summary>
        /// 是否禁止。达到了设定的限制。
        /// </summary>
        public bool OutputIsForbidden
        {
            get { return _outputIsForbidden; }
            set { _outputIsForbidden = value; }
        }

        private string _outputForbiddenReason = "";
        /// <summary>
        /// 限制的原因描述
        /// </summary>
        public string OutputForbiddenReason
        {
            get { return _outputForbiddenReason; }
            set { _outputForbiddenReason = value; }
        }

        public void DoLimitCheck()
        {
            string sAuthenUserName = this.InputAuthenUserRec.AuthenUserName;

            //============ 1. 每分钟的限制 ================
            if (this.InputAuthenUserRec.LimitPerMinute != 0)
            {
                int nLimitValue = this.InputAuthenUserRec.LimitPerMinute;
                string sFromTime = DateTimeUtil.AddSeconds(DateTimeUtil.Now, -60);

                if (MeetTimeRangeLimit(sAuthenUserName, sFromTime, nLimitValue))
                {
                    this.OutputIsForbidden = true;
                    this.OutputForbiddenReason = "超出了每分钟的次数限制";
                    return;
                }
            }

            //============ 2. 每小时的限制 ================
            if (this.InputAuthenUserRec.LimitPerHour != 0)
            {
                int nLimitValue = this.InputAuthenUserRec.LimitPerHour;
                string sFromTime = DateTimeUtil.AddSeconds(DateTimeUtil.Now, -60 * 60);

                if (MeetTimeRangeLimit(sAuthenUserName, sFromTime, nLimitValue))
                {
                    this.OutputIsForbidden = true;
                    this.OutputForbiddenReason = "超出了每小时的次数限制";
                    return;
                }
            }

            //============ 3. 每天的限制 ================
            if (this.InputAuthenUserRec.LimitPerDay != 0)
            {
                int nLimitValue = this.InputAuthenUserRec.LimitPerDay;
                string sFromTime = DateTimeUtil.AddSeconds(DateTimeUtil.Now, -60 * 60 * 24);

                if (MeetTimeRangeLimit(sAuthenUserName, sFromTime, nLimitValue))
                {
                    this.OutputIsForbidden = true;
                    this.OutputForbiddenReason = "超出了每天的次数限制";
                    return;
                }
            }

            //============= 4. 没超出限制 ============
            this.OutputIsForbidden = false;
        }

        private bool MeetTimeRangeLimit(string sAuthenUserName, string sFromTime, int nLimitValue)
        {
            string sSQLLimit = "select count(*) from vcb_log_vcode_break where authen_user_name = "
                    + StringUtil.QuotedToDBStr(sAuthenUserName) + " and request_time >= "
                    + StringUtil.QuotedToDBStr(sFromTime) + " and request_time <= "
                    + StringUtil.QuotedToDBStr(DateTimeUtil.NowWithMilliSeconds);
            int nCountOfRequest = ConvertUtil.ToInt(DataHelper.Instance.ExecuteScalar(sSQLLimit));

            if (nCountOfRequest >= nLimitValue)
                return true;
            else
                return false;
        }

    }
}

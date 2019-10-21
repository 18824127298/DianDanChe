using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;

using Sigbit.Common;

using Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine;
using Sigbit.App.Net.IBXService.VCodeBreak.ThirdEngine.UUCloud;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Service.BreakRoute
{
    class ABRBreakRoute
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

        private TbSysVcode _inputVCodeUsageRec = null;
        /// <summary>
        /// 破解场景记录。引擎调用、伪装/真实的时延
        /// </summary>
        public TbSysVcode InputVCodeUsageRec
        {
            get { return _inputVCodeUsageRec; }
            set { _inputVCodeUsageRec = value; }
        }

        private TbLogVcodeBreak _inputBreakLogRec = null;
        /// <summary>
        /// 破解记录。破解的结果填写，场景、算法的填写。
        /// </summary>
        public TbLogVcodeBreak InputBreakLogRec
        {
            get { return _inputBreakLogRec; }
            set { _inputBreakLogRec = value; }
        }

        private string _inputImageFileNameForBreak = "";
        /// <summary>
        /// 待破解的图像文件名
        /// </summary>
        public string InputImageFileNameForBreak
        {
            get { return _inputImageFileNameForBreak; }
            set { _inputImageFileNameForBreak = value; }
        }

        private string _outputBreakResultText = "";
        /// <summary>
        /// 破解的结果文字
        /// </summary>
        public string OutputBreakResultText
        {
            get { return _outputBreakResultText; }
            set { _outputBreakResultText = value; }
        }

        private string _outputErrorCode = "";
        /// <summary>
        /// 错误代码
        /// </summary>
        public string OutputErrorCode
        {
            get { return _outputErrorCode; }
            set { _outputErrorCode = value; }
        }

        private string _outputErrorString = "";
        /// <summary>
        /// 错误描述
        /// </summary>
        public string OutputErrorString
        {
            get { return _outputErrorString; }
            set { _outputErrorString = value; }
        }

        public void DoBreakRoute()
        {
            //========== 1. 每分、小时、每日限制的判断 =================
            ABR_TimeRangeLimitCheck abrLimitCheck = new ABR_TimeRangeLimitCheck();
            abrLimitCheck.InputAuthenUserRec = this.InputAuthenUserRec;
            abrLimitCheck.DoLimitCheck();

            if (abrLimitCheck.OutputIsForbidden)
            {
                this.OutputErrorCode = "695_limit_forbidden";
                this.OutputErrorString = abrLimitCheck.OutputForbiddenReason;
                return;
            }

            //============ 2. 调用具体的算法 ================
            this.InputBreakLogRec.VcodeId = this.InputVCodeUsageRec.VcodeId;
            this.InputBreakLogRec.AlgolId = this.InputVCodeUsageRec.AlgolId;

            //========== 2.1 手工处理的直接返回 ============
            if (this.InputVCodeUsageRec.AlgolIdE == TbSysBreakAlgolEAlgolID.Manual)
                return;

            //========= 2.2 调用自动的处理 =============
            this.InputBreakLogRec.BreakBeginTime = DateTimeUtil.NowWithMilliSeconds;

            //============== 3. 伪装的处理 ==============
            bool bCallFakeEngine = false;
            if (WillCallFakeEngine())
                bCallFakeEngine = true;

            if (bCallFakeEngine)
            {
                double fFakeDelaySeconds = NeedFakeDelaySeconds();
                if (fFakeDelaySeconds != 0)
                    Thread.Sleep((int)fFakeDelaySeconds * 1000);

                this.OutputErrorCode = "043_fail";
                this.OutputErrorString = "破解失败";
                this.OutputBreakResultText = "";

                this.InputBreakLogRec.CurrentStatusE = TbLogVcodeBreakECurrentStatus.Broken;
                this.InputBreakLogRec.BreakResultE = TbLogVcodeBreakEBreakResult.Fail;
            }
            else
            {
                //============ 4. 自动的处理 =============
                if (this.InputVCodeUsageRec.AlgolIdE == TbSysBreakAlgolEAlgolID.UUCloud)
                {
                    BUUBreakRequest uuReq = new BUUBreakRequest();
                    uuReq.UUCodeType = ConvertUtil.ToInt(this.InputVCodeUsageRec.AlgolParams);
                    uuReq.ImageFileName = this.InputImageFileNameForBreak;

                    BUUBreakResult uuResult = BUUBreakEngine.Instance.VCodeBreak(uuReq);

                    this.OutputErrorCode = uuResult.ErrorCode;
                    this.OutputErrorString = uuResult.ErrorString;
                    this.OutputBreakResultText = uuResult.BreakResultText;
                }
                else if (this.InputVCodeUsageRec.AlgolIdE == TbSysBreakAlgolEAlgolID.Random)
                {
                    this.OutputBreakResultText = RandUtil.NewString(4, RandStringType.Number);
                }
                else
                {
                    this.OutputErrorCode = "682_algol_not_support";
                    this.OutputErrorString = "不支持算法（" + this.InputVCodeUsageRec.AlgolId + "）";
                    this.OutputBreakResultText = "";
                }

                this.InputBreakLogRec.CurrentStatusE = TbLogVcodeBreakECurrentStatus.Broken;
                if (this.OutputErrorCode != "")
                    this.InputBreakLogRec.BreakResultE = TbLogVcodeBreakEBreakResult.Fail;
                else
                    this.InputBreakLogRec.BreakResultE = TbLogVcodeBreakEBreakResult.Succ;

                //============ 3. 强制时延的判断和处理 ============
                int nForceDelayMS = NeedForceDelayMilliSeconds(this.InputBreakLogRec.BreakBeginTime);
                if (nForceDelayMS != 0)
                    Thread.Sleep(nForceDelayMS);
            }
            
            this.InputBreakLogRec.BreakEndTime = DateTimeUtil.NowWithMilliSeconds;
            this.InputBreakLogRec.BreakDelay = DateTimeUtil.MilliSecondsAfter(this.InputBreakLogRec.BreakBeginTime,
                    this.InputBreakLogRec.BreakEndTime) / 1000.0;
        }

        /// <summary>
        /// 得到需要强制时延的毫秒数
        /// </summary>
        /// <param name="sBeginTime">开始时间</param>
        /// <returns>毫秒数</returns>
        private int NeedForceDelayMilliSeconds(string sBeginTime)
        {
            //========= 1. 是否设置了强制时延 ===============
            if (this.InputVCodeUsageRec.CallForceMinSec == 0)
                return 0;

            //========= 2. 得到当前的时延 ============
            int nCurrentMilliSeconds = DateTimeUtil.MilliSecondsAfter(sBeginTime, DateTimeUtil.NowWithMilliSeconds);

            //========= 3. 判断时延是否已超出 ===============
            if (nCurrentMilliSeconds >= this.InputVCodeUsageRec.CallForceMinSec * 1000)
                return 0;

            //========== 4. 未超出，得到随机的时延范围 ==============
            double fForceDelay = this.InputVCodeUsageRec.CallForceMinSec;
            if (this.InputVCodeUsageRec.CallForceMaxSec > this.InputVCodeUsageRec.CallForceMinSec)
            {
                fForceDelay = RandUtil.NewFloat(this.InputVCodeUsageRec.CallForceMinSec, 
                        this.InputVCodeUsageRec.CallForceMaxSec);
            }

            //=========== 5. 得到需要强制时延的毫秒数 ===========
            int nRet = (int)(fForceDelay * 1000 - nCurrentMilliSeconds);
            if (nRet < 0)
                nRet = 0;

            return nRet;
        }

        private bool WillCallFakeEngine()
        {
            if (this.InputVCodeUsageRec.CallRate >= 99.9)
                return false;

            double fRand = RandUtil.NewFloat(0, 100);
            if (fRand <= this.InputVCodeUsageRec.CallRate)
                return false;
            else
                return true;
        }

        private double NeedFakeDelaySeconds()
        {
            double fMin = this.InputVCodeUsageRec.CallFakeMinSec;
            double fMax = this.InputVCodeUsageRec.CallForceMaxSec;

            if (fMin == 0 && fMax == 0)
                return 0;
            else if (fMin == 0)
                return fMax;
            else if (fMax == 0)
                return fMin;

            if (fMin >= fMax)
                return fMin;

            double fRet = RandUtil.NewFloat(fMin, fMax);
            return fRet;
        }
    }
}

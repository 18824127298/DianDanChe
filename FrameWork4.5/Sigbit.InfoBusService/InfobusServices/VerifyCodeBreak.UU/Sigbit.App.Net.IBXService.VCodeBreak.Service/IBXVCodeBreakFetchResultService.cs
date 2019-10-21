using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using Sigbit.Common;
using Sigbit.Data;
using Sigbit.App.Net.IBXService.VCodeBreak.Message;
using Sigbit.App.Net.IBXService.Log;
using Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Service
{
    public class IBXVCodeBreakFetchResultService
    {
        private static IBXVCodeBreakFetchResultService _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static IBXVCodeBreakFetchResultService Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new IBXVCodeBreakFetchResultService();
                return _thisInstance;
            }
        }

        public IBMVCodeBreakResultFetchRESP DealWithRequest(IBMVCodeBreakResultFetchREQ reqMess, byte[] bsPacket)
        {
            IBMVCodeBreakResultFetchRESP fetchRESP = new IBMVCodeBreakResultFetchRESP();

            IBXLogMessage ibxLogMessage = new IBXLogMessage();
            ibxLogMessage.RequestTime = DateTime.Now;

            //========== 1. 消息的处理 ==============
            TbLogVcodeBreak tblLogBreak = FetchOneISRResultRecordAndUpdate(reqMess);

            if (tblLogBreak != null)
            {
                fetchRESP.BreakResultText = tblLogBreak.BreakText;
            }

            //=============== 2. 记录消息日志 ==============
            //=========== 2.1 生成日志记录数据 =============
            ibxLogMessage.RequestMessage = reqMess;
            ibxLogMessage.RequestPacket = bsPacket;
            ibxLogMessage.ResponseMessage = fetchRESP;

            //============ 2.2 插入日志记录 ===========
            ibxLogMessage.NoteLog();

            //============== 3. 消息响应 =============
            return fetchRESP;
        }

        /// <summary>
        /// 取出一条识别结果记录
        /// </summary>
        /// <returns>识别结果记录</returns>
        private TbLogVcodeBreak FetchOneISRResultRecordAndUpdate(IBMVCodeBreakResultFetchREQ reqMess)
        {
            string sSQL = "select * from vcb_log_vcode_break where current_status = "
                    + StringUtil.QuotedToDBStr(TbLogVcodeBreakECurrentStatus.Broken.ToCodeString())
                    + " and log_uid="  + StringUtil.QuotedToDBStr(reqMess.UploadReceipt);
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            TbLogVcodeBreak tblRet = new TbLogVcodeBreak();
            tblRet.AssignByDataRow(ds, 0);

            tblRet.CurrentStatusE = TbLogVcodeBreakECurrentStatus.ResultFetched;
            tblRet.ResultFetchTime = DateTimeUtil.NowWithMilliSeconds;
            tblRet.TotalDelay = DateTimeUtil.MilliSecondsAfter(tblRet.UploadTime, tblRet.ResultFetchTime) / 1000.0;
            tblRet.Update();

            return tblRet;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using Sigbit.Common;
using Sigbit.Data;
using Sigbit.App.Net.IBXService.VoiceReg.Message;
using Sigbit.App.Net.IBXService.Log;
using Sigbit.App.Net.IBXService.VoiceReg.Service.DBDefine;

namespace Sigbit.App.Net.IBXService.VoiceReg.Service
{
    public class IBXVoiceRegFetchResultService
    {
        private static IBXVoiceRegFetchResultService _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static IBXVoiceRegFetchResultService Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new IBXVoiceRegFetchResultService();
                return _thisInstance;
            }
        }

        public IBMVoiceRegResultFetchRESP DealWithRequest(IBMVoiceRegResultFetchREQ reqMess, byte[] bsPacket)
        {
            IBMVoiceRegResultFetchRESP fetchRESP = new IBMVoiceRegResultFetchRESP();

            IBXLogMessage ibxLogMessage = new IBXLogMessage();
            ibxLogMessage.RequestTime = DateTime.Now;

            //========== 1. 消息的处理 ==============
            TbLogVoiceIsr tblISR = FetchOneISRResultRecordAndUpdate();

            if (tblISR != null)
            {
                fetchRESP.UploadReceipt = tblISR.LogUid;
                fetchRESP.RequestThirdId = tblISR.ReqeustThirdId;
                fetchRESP.RegResultText = tblISR.RegResultText;
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
        private TbLogVoiceIsr FetchOneISRResultRecordAndUpdate()
        {
            string sSQL = "select * from vreg_log_voice_isr where current_status = 'reged' limit 1";
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            TbLogVoiceIsr tblRet = new TbLogVoiceIsr();
            tblRet.AssignByDataRow(ds, 0);

            tblRet.CurrentStatus = "result_fetched";
            tblRet.ResultFetchTime = DateTimeUtil.NowWithMilliSeconds;
            tblRet.TotalDelay = DateTimeUtil.MilliSecondsAfter(tblRet.UploadTime, tblRet.ResultFetchTime) / 1000.0;
            tblRet.Update();

            return tblRet;
        }

    }
}

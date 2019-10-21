using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.App.Net.IBXService.VoiceQAN.Message;
using Sigbit.App.Net.IBXService.Log;
using Sigbit.App.Net.IBXService.VoiceQAN.Service.DBDefine;
using Sigbit.Common;
using Sigbit.Data;
using System.Data;

namespace Sigbit.App.Net.IBXService.VoiceQAN.Service.VQANService
{
    public class IBXVoiceQANFetchResultService
    {
        private static IBXVoiceQANFetchResultService _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static IBXVoiceQANFetchResultService Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new IBXVoiceQANFetchResultService();
                return _thisInstance;
            }
        }

        public IBMVoiceQANResultFetchRESP DealWithRequest(IBMVoiceQANResultFetchREQ reqMess, byte[] bsPacket)
        {
            IBMVoiceQANResultFetchRESP fetchRESP = new IBMVoiceQANResultFetchRESP();

            IBXLogMessage ibxLogMessage = new IBXLogMessage();
            ibxLogMessage.RequestTime = DateTime.Now;

            

            //========== 1. 消息的处理 ==============
            TbLogVoiceQan tblLogVQan = FetchOneVQanResultRecordAndUpdate();

            if (tblLogVQan != null)
            {
                fetchRESP.UploadReceipt = tblLogVQan.LogUid;
                fetchRESP.RequestThirdID = tblLogVQan.RequestThirdId;
                fetchRESP.QualityValue01 = tblLogVQan.QualityValue01;
                fetchRESP.QualityValue02 = tblLogVQan.QualityValue02;
                fetchRESP.QualityValue03 = tblLogVQan.QualityValue03;
                fetchRESP.QualityValue04 = tblLogVQan.QualityValue04;
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
        /// 取出一条分析结果记录
        /// </summary>
        /// <returns>分析结果记录</returns>
        private TbLogVoiceQan FetchOneVQanResultRecordAndUpdate()
        {
            string sSQL = "select * from vqan_log_voice_qan where current_status = "
                    + StringUtil.QuotedToDBStr(TbLogVoiceQanECurrentStatus.Qaned.ToCodeString())
                    + " order by qan_end_time limit 1";
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            TbLogVoiceQan tblRet = new TbLogVoiceQan();
            tblRet.AssignByDataRow(ds, 0);

            tblRet.CurrentStatusE = TbLogVoiceQanECurrentStatus.ResultFetched;
            tblRet.ResultFetchTime = DateTimeUtil.NowWithMilliSeconds;
            tblRet.TotalDelay = DateTimeUtil.MilliSecondsAfter(tblRet.UploadTime, tblRet.ResultFetchTime) / 1000.0;
            tblRet.Update();

            return tblRet;
        }

    }
}

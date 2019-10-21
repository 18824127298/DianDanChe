using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;

using Sigbit.App.Net.IBXService.Cient;
using Sigbit.App.Net.IBXService.VoiceCOMP.Message;

namespace Sigbit.App.Net.IBXService.VoiceCOMP.Client
{
    public class IBClientIvrVoiceCompare
    {
        private static IBClientIvrVoiceCompare _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static IBClientIvrVoiceCompare Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new IBClientIvrVoiceCompare();
                return _thisInstance;
            }
        }

        public IBMVoiceCompareRESP Compare(string sStandVoice, string sVoiceFileName)
        {
            ////========= 1. 准备请求 ============
            //IBMVoiceCompareREQ req = new IBMVoiceCompareREQ();
            //req.VoiceFileName = FileUtil.ExtractFileName(sVoiceFileName);
            //req.FileAdditionalInfo = sFileAdditionalInfo;

            IBMVoiceCompareREQ req = new IBMVoiceCompareREQ();
            req.StandVoice = sStandVoice;
            req.VoiceFileName = sVoiceFileName;


            ////========== 2. 上传并得到回执 =============
            //IBMUploadReceiptRESP receiptRESP = new IBMUploadReceiptRESP();
            //IBXBusClient.Instance.UploadFile(req, sVoiceFileName, receiptRESP);

            IBMVoiceCompareRESP resp = new IBMVoiceCompareRESP();
            IBXBusClient.Instance.GetResponse(req, resp);

            return resp;

            ////============ 3. 填写回执并得到结果 ============
            //req.UploadReceipt = receiptRESP.Receipt;

            //IBMVoiceCompareResult resp = new IBMVoiceCompareResult();
            //IBXBusClient.Instance.GetResponse(req, resp);

            

        }
    }
}

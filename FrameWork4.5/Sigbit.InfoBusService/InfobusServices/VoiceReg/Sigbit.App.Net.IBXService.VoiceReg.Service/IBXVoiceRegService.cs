using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Sigbit.Common;
using Sigbit.App.Net.IBXService.VoiceReg.Message;
using Sigbit.App.Net.IBXService.Log;
using Sigbit.App.Net.IBXService.VoiceReg.Service.DBDefine;

namespace Sigbit.App.Net.IBXService.VoiceReg.Service
{
    public class IBXVoiceRegService
    {
        private static IBXVoiceRegService _thisInstance = null;
        public static IBXVoiceRegService Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new IBXVoiceRegService();
                return _thisInstance;
            }
        }

        public IBMVoiceRegRESP DealWithRequest(IBMVoiceRegREQ reqMess, byte[] bsPacket)
        {
            IBMVoiceRegRESP voiceRegRESP = new IBMVoiceRegRESP();

            IBXLogMessage ibxLogMessage = new IBXLogMessage();
            ibxLogMessage.RequestTime = DateTime.Now;

            //========== 1. 消息的处理 ==============
            try
            {
                //============ 1.1 更新相关信息 ==============
                TbLogVoiceIsr tblLogISR = new TbLogVoiceIsr();
                tblLogISR.LogUid = reqMess.UploadReceipt;
                tblLogISR.Fetch();

                tblLogISR.VoiceFileLocal = reqMess.VoiceFileName;
                tblLogISR.VoiceFileForIsr = tblLogISR.VoiceFileUpload + FileUtil.ExtractFileExt(tblLogISR.VoiceFileLocal);
                tblLogISR.GrammarId = reqMess.GrammarId;
                tblLogISR.ReqeustThirdId = reqMess.RequestThirdId;

                tblLogISR.FromSystem = reqMess.FromSystem;
                tblLogISR.FromClientId = reqMess.FromClientId;
                tblLogISR.FromClientDesc = reqMess.FromClientDesc;
                tblLogISR.CurrentStatus = "request";

                tblLogISR.RequestTime = DateTimeUtil.NowWithMilliSeconds;
                tblLogISR.Update();

                //============ 1.2 复制文件 ==================
                string sSrcFileName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\data\\voice_reg\\voice_files\\"
                        + tblLogISR.VoiceFileUpload;
                string sDestFileName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\data\\voice_reg\\voice_files\\"
                        + tblLogISR.VoiceFileForIsr;
                File.Copy(sSrcFileName, sDestFileName, true);
            }
            catch (Exception ex)
            {
                voiceRegRESP.ErrorCode = 2252;
                voiceRegRESP.ErrorString = "语音识别服务错误: " + ex.Message;
            }


            //=============== 2. 记录消息日志 ==============
            //=========== 2.1 生成日志记录数据 =============
            ibxLogMessage.RequestMessage = reqMess;
            ibxLogMessage.RequestPacket = bsPacket;
            ibxLogMessage.ResponseMessage = voiceRegRESP;

            //============ 2.2 插入日志记录 ===========
            ibxLogMessage.NoteLog();

            //============== 3. 消息响应 =============
            return voiceRegRESP;
        }
    }
}

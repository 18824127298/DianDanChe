using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Sigbit.Common;

using Sigbit.App.Net.IBXService.VoiceQAN.Message;
using Sigbit.App.Net.IBXService.Log;
using Sigbit.App.Net.IBXService.VoiceQAN.Service.DBDefine;

namespace Sigbit.App.Net.IBXService.VoiceQAN.Service.VQANService
{
    public class IBXVoiceQANService
    {
        private static IBXVoiceQANService _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static IBXVoiceQANService Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new IBXVoiceQANService();
                return _thisInstance;
            }
        }

        public IBMVoiceQANRESP DealWithRequest(IBMVoiceQANREQ reqMess, byte[] bsPacket)
        {
            IBMVoiceQANRESP breakRESP = new IBMVoiceQANRESP();

            IBXLogMessage ibxLogMessage = new IBXLogMessage();
            ibxLogMessage.RequestTime = DateTime.Now;

            //========== 1. 消息的处理 ==============
            try
            {
                GetVoiceQANRESP(reqMess, breakRESP);
            }
            catch (Exception ex)
            {
                breakRESP.ErrorCode = 2253;
                breakRESP.ErrorString = "验证码服务执行错误: " + ex.Message;
            }

            //=============== 2. 记录消息日志 ==============
            //=========== 2.1 生成日志记录数据 =============
            ibxLogMessage.RequestMessage = reqMess;
            ibxLogMessage.RequestPacket = bsPacket;
            ibxLogMessage.ResponseMessage = breakRESP;

            //============ 2.2 插入日志记录 ===========
            ibxLogMessage.NoteLog();

            //============== 3. 消息响应 =============
            return breakRESP;
        }

        internal void GetVoiceQANRESP(IBMVoiceQANREQ reqMess, IBMVoiceQANRESP breakRESP)
        {
            //============ 1. 更新相关信息 ==============
            TbLogVoiceQan tblBreak = new TbLogVoiceQan();
            tblBreak.LogUid = reqMess.UploadReceipt;
            tblBreak.Fetch();

            tblBreak.VoiceFileLocal = reqMess.VoiceFileName;
            tblBreak.VoiceFileForQan = tblBreak.VoiceFileUpload + FileUtil.ExtractFileExt(tblBreak.VoiceFileLocal);
            tblBreak.RequestThirdId = reqMess.RequestThirdId;

            tblBreak.FromSystem = reqMess.FromSystem;
            tblBreak.FromClientId = reqMess.FromClientId;
            tblBreak.FromClientDesc = reqMess.FromClientDesc;
            tblBreak.CurrentStatusE = TbLogVoiceQanECurrentStatus.Request;

            tblBreak.RequestTime = DateTimeUtil.NowWithMilliSeconds;

            if (reqMess.SyncCall)
                tblBreak.IsSyncCall = "Y";
            else
                tblBreak.IsSyncCall = "N";

            //============ 2. 复制文件 ==================
            string sSrcFileName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\data\\voice_qan\\voice_files\\"
                    + tblBreak.VoiceFileUpload;
            string sDestFileName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\data\\voice_qan\\voice_files\\"
                    + tblBreak.VoiceFileForQan;


            File.Copy(sSrcFileName, sDestFileName, true);

            //=========== 3. 如果异步调用，直接返回 ==============
            if (!reqMess.SyncCall)
            {
                tblBreak.Update();
                return;
            }

            //============== 4. 同步调用。调用相关的自动破解及结果返回处理 ===============
            tblBreak.QualityValue01 = RandOutValue(12, 25);
            tblBreak.QualityValue02 = RandOutValue(42, 58);
            tblBreak.QualityValue03 = RandOutValue(58, 85);

            tblBreak.CurrentStatusE = TbLogVoiceQanECurrentStatus.Qaned;
            tblBreak.QanBeginTime = DateTimeUtil.Now;
            tblBreak.QanEndTime = tblBreak.QanBeginTime;
            tblBreak.QanDelay = 0;

            tblBreak.Update();
        }

        private double RandOutValue(double fMinValue, double fMaxValue)
        {
            double[] arrYPValues = new double[3];

            for (int i = 0; i < arrYPValues.Length; i++)
            {
                arrYPValues[i] = RandUtil.NewFloat(fMinValue, fMaxValue);
            }

            Array.Sort(arrYPValues);

            return arrYPValues[1];
        }


    }
}

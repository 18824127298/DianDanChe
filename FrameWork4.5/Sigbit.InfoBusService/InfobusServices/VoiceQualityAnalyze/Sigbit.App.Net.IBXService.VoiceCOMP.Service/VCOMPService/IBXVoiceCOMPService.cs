using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Sigbit.Common;
using Sigbit.Data;

using Sigbit.App.Net.IBXService.Message;
using Sigbit.App.Net.IBXService.Log;
using Sigbit.App.Net.IBXService.VoiceCOMP.Message;
using Sigbit.Common.MMLib.Voice.MemoryPiece;
using Sigbit.Common.MMLib.Voice.Memory;
using Sigbit.Common.MMLib.Voice;
using Sigbit.Lib.WaveLibrary;




namespace Sigbit.App.Net.IBXService.VoiceCOMP.Service.VCOMPService
{
    public class IBXVoiceCOMPService
    {

        private Hashtable _htAllVoicePiece = new Hashtable();

        private static IBXVoiceCOMPService _instance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static IBXVoiceCOMPService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IBXVoiceCOMPService();
                return _instance;
            }
        }


        public IBMVoiceCompareRESP DealWithRequest(IBMVoiceCompareREQ reqMess, byte[] bsPacket)
        {
            IBMVoiceCompareRESP matchRESP = new IBMVoiceCompareRESP();

            IBXLogMessage ibxLogMessage = new IBXLogMessage();
            ibxLogMessage.RequestTime = DateTime.Now;

            //========== 1. 消息的处理 ==============
            try
            {

                string sStandVoice = GetStandVoiceFullPath(reqMess.StandVoice);

                if (!File.Exists(sStandVoice))
                {
                    matchRESP.MatchResult = IBMVoiceCompareMatchResult.NoStandVoice;
                    matchRESP.ErrorCode = 2456;
                    matchRESP.ErrorString = "基准音" + sStandVoice + "不存在";

                    return matchRESP;
                }

                string sMatchVoice = GetMatchVoiceFullPath(reqMess.VoiceFileName);
                if (!File.Exists(sMatchVoice))
                {
                    matchRESP.MatchResult = IBMVoiceCompareMatchResult.NoMatchVoice;
                    matchRESP.ErrorCode = 2468;
                    matchRESP.ErrorString = "比较音" + sMatchVoice + "不存在";

                    return matchRESP;
                }

                MVPVoicePiece pieceStandard = GetVoicePiece(sStandVoice);
                MVPVoicePiece pieceCompare = GetVoicePiece(sMatchVoice);

                MVCCompareRule rule = new MVCCompareRule();
                rule.CompareMethod = MVCCompareRuleMethod.Contains;


                MVCCompareResult compareResult = MVCVoiceCompareUtil.CompareVoice(pieceStandard, pieceCompare, rule);

                matchRESP.MatchRate = compareResult.CompareRate;
                matchRESP.MatchResult = IBMVoiceCompareMatchResult.Succ;
                matchRESP.PlayDelay = WaveUtil.FindMaxSilentSecond(sMatchVoice, compareResult.IndexOfSrc);

            }
            catch (Exception ex)
            {
                matchRESP.ErrorCode = 9876;
                matchRESP.ErrorString = "语音对比服务出现错误: " + ex.Message;
            }

            //=============== 2. 记录消息日志 ==============
            //=========== 2.1 生成日志记录数据 =============
            ibxLogMessage.RequestMessage = reqMess;
            ibxLogMessage.RequestPacket = bsPacket;
            ibxLogMessage.ResponseMessage = matchRESP;

            //============ 2.2 插入日志记录 ===========
            ibxLogMessage.NoteLog();


            //============== 3. 消息响应 =============
            return matchRESP;
        }


        private string GetStandVoiceFullPath(string sStandVoiceName)
        {
            string sRet = IBMVoiceCOMPConfig.Instance.VoiceRootPath + "\\standard\\" + sStandVoiceName;

            return sRet;
        }


        private string GetMatchVoiceFullPath(string sMatchVoice)
        {
            string sRet = IBMVoiceCOMPConfig.Instance.VoiceRootPath + "\\voice" + sMatchVoice;

            return sRet;
        }


        private MVPVoicePiece GetVoicePiece(string sVoiceFileName)
        {
            MVPVoicePiece pieceRet = null;

            if (_htAllVoicePiece.Contains(sVoiceFileName))
            {
                pieceRet = _htAllVoicePiece[sVoiceFileName] as MVPVoicePiece;

                return pieceRet;
            }

            pieceRet.LoadFromFile(sVoiceFileName);

            _htAllVoicePiece[sVoiceFileName] = pieceRet;

            return pieceRet;
        }

    }
}

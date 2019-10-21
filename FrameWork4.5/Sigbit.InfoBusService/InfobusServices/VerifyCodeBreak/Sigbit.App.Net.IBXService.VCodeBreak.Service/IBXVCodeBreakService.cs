using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Sigbit.Common;
using Sigbit.App.Net.IBXService.VCodeBreak.Message;
using Sigbit.App.Net.IBXService.Log;
using Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Service
{
    public class IBXVCodeBreakService
    {
        private static IBXVCodeBreakService _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static IBXVCodeBreakService Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new IBXVCodeBreakService();
                return _thisInstance;
            }
        }

        public IBMVCodeBreakRESP DealWithRequest(IBMVCodeBreakREQ reqMess, byte[] bsPacket)
        {
            IBMVCodeBreakRESP breakRESP = new IBMVCodeBreakRESP();

            IBXLogMessage ibxLogMessage = new IBXLogMessage();
            ibxLogMessage.RequestTime = DateTime.Now;

            //========== 1. 消息的处理 ==============
            try
            {
                //============ 1.1 更新相关信息 ==============
                TbLogVcodeBreak tblBreak = new TbLogVcodeBreak();
                tblBreak.LogUid = reqMess.UploadReceipt;
                tblBreak.Fetch();

                tblBreak.ImageFileLocal = reqMess.ImageFileName;
                tblBreak.ImageFileForBreak = tblBreak.ImageFileUpload + FileUtil.ExtractFileExt(tblBreak.ImageFileLocal);
                tblBreak.VcodeId = reqMess.VcodeId;
                tblBreak.RequestThirdId = reqMess.RequestThirdId;

                tblBreak.FromSystem = reqMess.FromSystem;
                tblBreak.FromClientId = reqMess.FromClientId;
                tblBreak.FromClientDesc = reqMess.FromClientDesc;
                tblBreak.CurrentStatus = "request";

                tblBreak.RequestTime = DateTimeUtil.NowWithMilliSeconds;
                tblBreak.Update();

                //============ 1.2 复制文件 ==================
                string sSrcFileName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\data\\vcode_break\\vcode_images\\"
                        + tblBreak.ImageFileUpload;
                string sDestFileName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\data\\vcode_break\\vcode_images\\"
                        + tblBreak.ImageFileForBreak;
                File.Copy(sSrcFileName, sDestFileName, true);
            }
            catch (Exception ex)
            {
                breakRESP.ErrorCode = 2253;
                breakRESP.ErrorString = "验证码服务错误: " + ex.Message;
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

    }
}



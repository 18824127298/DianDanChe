using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.App.Net.IBXService.VCodeBreak.BTMMessage;
using Sigbit.Common;
using System.IO;
using Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine;
using Sigbit.App.Net.IBXService.VCodeBreak.Message;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Service.BTMService
{
    public class BTMVCodeBreakService
    {
        private static BTMVCodeBreakService _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static BTMVCodeBreakService Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new BTMVCodeBreakService();
                return _thisInstance;
            }
        }

        public BTMVCodeBreakRESP DealWithRequest(BTMVCodeBreakREQ reqMess, byte[] bsPacket)
        {
            BTMVCodeBreakRESP breakRESP = new BTMVCodeBreakRESP();

            BTXLogMessage btxLogMessage = new BTXLogMessage();
            btxLogMessage.RequestTime = DateTime.Now;

            //========== 1. 消息的处理 ==============
            try
            {
                GetVCodeBreakRESP(reqMess, breakRESP);
            }
            catch (Exception ex)
            {
                breakRESP.ErrorCode = "993_service_execute_fail";
                breakRESP.ErrorString = "验证码服务执行错误: " + ex.Message;
            }

            //=============== 2. 记录消息日志 ==============
            //=========== 2.1 生成日志记录数据 =============
            btxLogMessage.RequestMessage = reqMess;
            btxLogMessage.RequestPacket = bsPacket;
            btxLogMessage.ResponseMessage = breakRESP;

            //============ 2.2 插入日志记录 ===========
            btxLogMessage.NoteLog();

            //============== 3. 消息响应 =============
            return breakRESP;
        }

        private void GetVCodeBreakRESP(BTMVCodeBreakREQ reqMess, BTMVCodeBreakRESP breakRESP)
        {
            //========== 1. 相当于Upload调用，创建LogBreak日志，并为破解的复用做准备 =======
            string sUploadReceipt = SaveImageAndCreateLogBreakRec(reqMess);

            //========== 2. 创建IBX请求消息 ===========
            IBMVCodeBreakREQ ibxReq = new IBMVCodeBreakREQ();
            ibxReq.AuthenUserName = reqMess.AuthenUserName;
            ibxReq.AuthenPassword = reqMess.AuthenPassword;
            ibxReq.ImageFileName = reqMess.ImageFileName;
            ibxReq.RequestThirdId = reqMess.RequestThirdID;
            ibxReq.SyncCall = true;
            ibxReq.UploadReceipt = sUploadReceipt;
            ibxReq.VCodeUsageID = reqMess.VCodeUsageID;

            //=============== 3. 调用IBXService的处理 ============
            IBMVCodeBreakRESP ibxRESP = new IBMVCodeBreakRESP();
            IBXVCodeBreakService.Instance.GetVCodeBreakRESP(ibxReq, ibxRESP);

            //============= 3. 赋值到BTM的结果消息 =========
            if (ibxRESP.ErrorCode != 0)
                breakRESP.ErrorCode = ibxRESP.ErrorCode.ToString();
            breakRESP.ErrorString = ibxRESP.ErrorString;
            breakRESP.BreakResultText = ibxRESP.BreakResultText;
        }




        private string SaveImageAndCreateLogBreakRec(BTMVCodeBreakREQ reqMess)
        {
            //=========== 1. 保存到文件中 ===================
            //=========== 1.1 得到文件名 =============
            string sFilePureName = DateTimeUtil.ToDateTime14Str(DateTimeUtil.Now) + "-" + RandUtil.NewString(4, RandStringType.Lower);
            string sDirectoryName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\data\\vcode_break\\vcode_images";

            //=========== 1.1+ 2014.05.23 在目录上增加日期，避免文件过多 ===========

            string sDirectoryNameEx = DateTime.Now.Year + "\\" + DateTime.Now.Month.ToString("00") + "\\" + DateTime.Now.Day.ToString("00");

            Directory.CreateDirectory(sDirectoryName + "\\" + sDirectoryNameEx);

            string sFileFullName = sDirectoryName + "\\" + sDirectoryNameEx + "\\" + sFilePureName;

            //=========== 1.2 保存到文件中 ================
            reqMess.SaveImageDataToFile(sFileFullName);

            //========== 2. 记录破解日志记录 =============
            TbLogVcodeBreak tblLogBreak = new TbLogVcodeBreak();
            tblLogBreak.LogUid = sDirectoryNameEx + "\\" + sFilePureName;
            tblLogBreak.CurrentStatusE = TbLogVcodeBreakECurrentStatus.Upload;
            tblLogBreak.UploadTime = DateTimeUtil.NowWithMilliSeconds;
            tblLogBreak.ImageFileUpload = sDirectoryNameEx + "\\" + sFilePureName;
            tblLogBreak.Insert();

            //======== 3. 生成回执 ================
            string sRet = sDirectoryNameEx + "\\" + sFilePureName;
            return sRet;
        }
    }
}

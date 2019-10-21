using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Sigbit.Common;
using Sigbit.App.Net.IBXService.VCodeBreak.Message;
using Sigbit.App.Net.IBXService.Log;
using Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine;
using Sigbit.App.Net.IBXService.VCodeBreak.Service.BreakRoute;

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
                GetVCodeBreakRESP(reqMess, breakRESP);
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

        internal void GetVCodeBreakRESP(IBMVCodeBreakREQ reqMess, IBMVCodeBreakRESP breakRESP)
        {
            //============ 1. 更新相关信息 ==============
            TbLogVcodeBreak tblBreak = new TbLogVcodeBreak();
            tblBreak.LogUid = reqMess.UploadReceipt;
            tblBreak.Fetch();

            tblBreak.AuthenUserName = reqMess.AuthenUserName;

            tblBreak.ImageFileLocal = reqMess.ImageFileName;
            tblBreak.ImageFileForBreak = tblBreak.ImageFileUpload + FileUtil.ExtractFileExt(tblBreak.ImageFileLocal);
            tblBreak.VcodeId = reqMess.VCodeUsageID;
            tblBreak.RequestThirdId = reqMess.RequestThirdId;

            tblBreak.FromSystem = reqMess.FromSystem;
            tblBreak.FromClientId = reqMess.FromClientId;
            tblBreak.FromClientDesc = reqMess.FromClientDesc;
            tblBreak.CurrentStatusE = TbLogVcodeBreakECurrentStatus.Request;

            tblBreak.RequestTime = DateTimeUtil.NowWithMilliSeconds;

            if (reqMess.SyncCall)
                tblBreak.IsSyncCall = "Y";
            else
                tblBreak.IsSyncCall = "N";

            //============ 2. 复制文件 ==================
            string sSrcFileName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\data\\vcode_break\\vcode_images\\"
                    + tblBreak.ImageFileUpload;
            string sDestFileName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\data\\vcode_break\\vcode_images\\"
                    + tblBreak.ImageFileForBreak;
            File.Copy(sSrcFileName, sDestFileName, true);

            //============== 3. 判断用户名、密码是否符合 ===========
            TbAuthenUser tblAuthenUser = QDBVCBreakPools.PoolAuthenUser.GetAuthenUserRecByUserName(reqMess.AuthenUserName);
            if (tblAuthenUser == null)
            {
                tblBreak.Update();

                breakRESP.ErrorCode = 3511;
                breakRESP.ErrorString = "鉴权错误，用户名不存在";
                return;
            }

            tblBreak.AuthenUserUid = tblAuthenUser.AuthenUserUid;
            
            if (tblAuthenUser.AuthenPassword != reqMess.AuthenPassword)
            {
                tblBreak.Update();

                breakRESP.ErrorCode = 3512;
                breakRESP.ErrorString = "鉴权错误，密码不符";
                return;
            }

            //=========== 3. 如果异步调用，直接返回 ==============
            //if (!reqMess.SyncCall)
            //    return;

            //============== 4. 同步调用。调用相关的自动破解及结果返回处理 ===============
            
            //========== 5. 由破解场景得到破解算法 ===============
            TbSysVcode tblVCodeUsage = QDBVCBreakPools.PoolVCodeUsage.GetVCodeUsageRec(reqMess.VCodeUsageID);
            if (tblVCodeUsage == null)
            {
                tblBreak.Update();

                breakRESP.ErrorCode = 3611;
                breakRESP.ErrorString = "定位不到请求的破解场景";
                return;
            }

            //========== 6. 调用具体的破解引擎，得到破解结果 ==========
            ABRBreakRoute abrRoute = new ABRBreakRoute();
            abrRoute.InputAuthenUserRec = tblAuthenUser;
            abrRoute.InputVCodeUsageRec = tblVCodeUsage;
            abrRoute.InputBreakLogRec = tblBreak;
            abrRoute.InputImageFileNameForBreak = sDestFileName;

            abrRoute.DoBreakRoute();

            if (abrRoute.OutputErrorCode != "")
            {
                breakRESP.ErrorCode = 7336;
                breakRESP.ErrorString = abrRoute.OutputErrorString + "(" + abrRoute.OutputErrorCode + ")";
            }
            else
            {
                breakRESP.BreakResultText = abrRoute.OutputBreakResultText;
            }

            tblBreak.BreakText = breakRESP.BreakResultText;
            tblBreak.FailDesc = breakRESP.ErrorString;
            if (breakRESP.ErrorCode != 0)
                tblBreak.FailDesc += "(" + breakRESP.ErrorCode.ToString() + ")";
            tblBreak.Update();
        }

    }
}



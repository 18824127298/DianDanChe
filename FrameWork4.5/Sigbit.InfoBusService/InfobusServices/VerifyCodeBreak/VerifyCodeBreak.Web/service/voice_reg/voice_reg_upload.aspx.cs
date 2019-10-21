using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.IO;

using Sigbit.Common;
using Sigbit.App.Net.IBXService.Log;
using Sigbit.App.Net.IBXService.Upload.Message;
using Sigbit.App.Net.IBXService.VoiceReg.Service.DBDefine;

public partial class service_voice_reg_voice_reg_upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetNoStore();
        Response.ContentEncoding = System.Text.Encoding.UTF8;

        IBXLogMessage ibxLogMessage = new IBXLogMessage();
        ibxLogMessage.RequestTime = DateTime.Now;

        //=========== 1. 读取请求, 并解析请求 ==========
        byte[] bsInput = new byte[Request.InputStream.Length];
        Request.InputStream.Read(bsInput, 0, bsInput.Length);

        //=========== 2. 保存到文件中 ===================
        //=========== 2.1 得到文件名 =============
        string sFilePureName = DateTimeUtil.ToDateTime14Str(DateTimeUtil.Now) + "-" + RandUtil.NewString(4, RandStringType.Lower);
        string sDirectoryName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\data\\voice_reg\\voice_files";
        Directory.CreateDirectory(sDirectoryName);

        string sFileFullName = sDirectoryName + "\\" + sFilePureName;

        //=========== 2.2 保存到文件中 ================
        FileUtil.WriteBytesToFile(sFileFullName, bsInput);

        //========== 3. 记录破解日志记录 =============
        TbLogVoiceIsr tblLogISR = new TbLogVoiceIsr();
        tblLogISR.LogUid = sFilePureName;
        tblLogISR.CurrentStatus = "upload";
        tblLogISR.UploadTime = DateTimeUtil.NowWithMilliSeconds;
        tblLogISR.VoiceFileUpload = sFilePureName;
        tblLogISR.Insert();

        //======== 4. 生成回执 ================
        IBMUploadReceiptRESP receiptRESP = new IBMUploadReceiptRESP();
        receiptRESP.Receipt = sFilePureName;

        //=========== 5. 记录消息日志 ===========
        ibxLogMessage.RequestPacket__Description = "upload voice file. total " + bsInput.Length.ToString()
                + " bytes. save to " + sFilePureName + ".";
        ibxLogMessage.ResponseMessage = receiptRESP;

        ibxLogMessage.NoteLog();

        string sPageAnswerContent = receiptRESP.ToString();
        Response.Write(sPageAnswerContent);

        Response.End();
    }
}

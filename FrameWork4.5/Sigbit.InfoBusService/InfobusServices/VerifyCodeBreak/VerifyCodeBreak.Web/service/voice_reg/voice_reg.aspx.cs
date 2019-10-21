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

using Sigbit.Common;
using Sigbit.App.Net.IBXService.VoiceReg.Message;
using Sigbit.App.Net.IBXService.VoiceReg.Service;

public partial class service_voice_reg_voice_reg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetNoStore();
        Response.ContentEncoding = System.Text.Encoding.UTF8;

        //=========== 1. 读取请求, 并解析请求 ==========
        byte[] bsInput = new byte[Request.InputStream.Length];
        Request.InputStream.Read(bsInput, 0, bsInput.Length);

        //FileUtil.WriteBytesToFile(GetLogFileName("entry"), bsInput);

        try
        {
            //========= 2. 解析请求 ==============
            IBMVoiceRegREQ requestMess = new IBMVoiceRegREQ();
            requestMess.ReadFrom(bsInput);

            //======== 3. 处理请求，得到DNS寻址结果 ===========
            IBMVoiceRegRESP regRESP = IBXVoiceRegService.Instance.DealWithRequest(requestMess, bsInput);

            //========= 4. 响应结果 =========
            string sPageAnswerContent = regRESP.ToString();
            Response.Write(sPageAnswerContent);
        }
        catch (Exception ex)
        {
            string sMessage = ex.Message + "\r\n" + ex.StackTrace;
            FileUtil.WriteStringToFile(GetLogFileName("error"), sMessage);
        }

        Response.End();
    }

    private string GetLogFileName(string sPostFix)
    {
        string sFileName = DateTimeUtil.Now.Replace("-", "").Replace(":", "").Replace(" ", "-");
        sFileName += "-" + RandUtil.NewString(3, RandStringType.Lower) + "-" + sPostFix + ".log";

        sFileName = "c:\\temp\\" + sFileName;

        return sFileName;
    }
}

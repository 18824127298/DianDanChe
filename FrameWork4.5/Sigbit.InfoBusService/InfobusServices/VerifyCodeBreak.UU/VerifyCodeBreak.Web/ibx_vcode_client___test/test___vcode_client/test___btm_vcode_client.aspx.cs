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
using Sigbit.Framework;
using Sigbit.App.Net.IBXService.VCodeBreak.BTMMessage;
using Sigbit.App.Net.IBXService.VCodeBreak.BTCClient;

public partial class ibx_vcode_client___test_test___vcode_client_test___vcode_client : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUploadAndBreak_Click(object sender, EventArgs e)
    {
        lblErrMessage.Visible = false;

        if (FileUploadImage.FileBytes.Length == 0)
        {
            lblErrMessage.Text = "没有指定上传的文件";
            lblErrMessage.Visible = true;
            return;
        }

        //============ 1. 将上传的文件保存起来 =================
        string sSaveAsFileName = DateTimeUtil.ToDateTime14Str(DateTimeUtil.Now)
                + RandUtil.NewString(4, RandStringType.Upper);
        string sExtFileName = FileUtil.ExtractFileExt(FileUploadImage.FileName);

        sSaveAsFileName = sSaveAsFileName + sExtFileName;
        sSaveAsFileName = "c:\\temp\\" + sSaveAsFileName;

        FileUploadImage.SaveAs(sSaveAsFileName);

        //========= 2. 准备请求 ============
        BTMVCodeBreakREQ req = new BTMVCodeBreakREQ();

        req.AuthenUserName = edtAuthenUserName.Text.Trim();
        req.AuthenPassword = edtAuthenPassword.Text.Trim();

        req.VCodeUsageID = edtVCodeUsageID.Text.Trim();
        req.RequestThirdID = "";
        req.ImageFileName = sSaveAsFileName;

        req.LoadImageDataFromFile(sSaveAsFileName);

        //============ 3. 提交请求，并得到结果 ============
        BTMVCodeBreakRESP resp = new BTMVCodeBreakRESP();
        BTCVCodeBreakClient.Instance.GetResponse(req, resp);

        if (resp.ErrorCode != "")
        {
            lblErrMessage.Text = resp.GetMessageDescription();
            lblErrMessage.Visible = true;
            return;
        }

        string sBreakResult = resp.BreakResultText;
        if (sBreakResult == "")
            lblBreakResultText.Text = "[empty.  may need fetch]";
        else
            lblBreakResultText.Text = sBreakResult;
    }
}

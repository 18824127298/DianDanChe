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

using Sigbit.App.Net.IBXService.VCodeBreak.Message;
using Sigbit.App.Net.IBXService.Upload.Message;
using Sigbit.App.Net.IBXService.Cient;

public partial class ibx_vcode_client___test_test___vcode_client_test___vcode_client : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUploadImage_Click(object sender, EventArgs e)
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

        this.PageParameter.SetCustomParamString("image_fileNameWQGW", sSaveAsFileName);

        //============= 2. 通过接口上传，并得到回执 ============
        IBMVCodeBreakREQ req = new IBMVCodeBreakREQ();

        IBMUploadReceiptRESP receiptRESP = new IBMUploadReceiptRESP();

        IBXBusClient.Instance.UploadFile(req, sSaveAsFileName, receiptRESP);
        if (receiptRESP.ErrorCode != 0)
        {
            lblErrMessage.Text = receiptRESP.GetMessageDescription();
            lblErrMessage.Visible = true;
            return;
        }
        lblUploadReceipt.Text = receiptRESP.Receipt;
    }

    protected void btnUploadAndBreak_Click(object sender, EventArgs e)
    {
        lblErrMessage.Visible = false;

        //========= 1. 准备请求 ============
        IBMVCodeBreakREQ req = new IBMVCodeBreakREQ();

        req.AuthenUserName = edtAuthenUserName.Text.Trim();
        req.AuthenPassword = edtAuthenPassword.Text.Trim();

        req.VCodeUsageID = edtVCodeUsageID.Text.Trim();
        req.RequestThirdId = "";
        req.UploadReceipt = lblUploadReceipt.Text;
        req.ImageFileName = this.PageParameter.GetCustomParamString("image_fileNameWQGW");

        req.SyncCall = cbxSyncCall.Checked;

        //============ 2. 填写回执并得到结果 ============
        IBMVCodeBreakRESP resp = new IBMVCodeBreakRESP();
        IBXBusClient.Instance.GetResponse(req, resp);

        if (resp.ErrorCode != 0)
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

    protected void btnFetchBreakResult_Click(object sender, EventArgs e)
    {
        lblErrMessage.Visible = false;

        IBMVCodeBreakResultFetchREQ req = new IBMVCodeBreakResultFetchREQ();
        req.AuthenUserName = edtAuthenUserName.Text;
        req.AuthenPassword = edtAuthenPassword.Text;
        req.UploadReceipt = lblUploadReceipt.Text;

        IBMVCodeBreakResultFetchRESP resp = new IBMVCodeBreakResultFetchRESP();

        IBXBusClient.Instance.GetResponse(req, resp);

        if (resp.ErrorCode != 0)
        {
            lblErrMessage.Text = resp.GetMessageDescription();
            lblErrMessage.Visible = true;
            return;
        }

        string sBreakResult = resp.BreakResultText;
        if (sBreakResult == "")
            lblFetchResult.Text = "[empty.  wait. or fetched.]";
        else
            lblFetchResult.Text = sBreakResult;
    }
}

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

using Sigbit.App.Net.IBXService.Upload.Message;
using Sigbit.App.Net.IBXService.Cient;
using Sigbit.App.Net.IBXService.VoiceQAN.Message;

public partial class ibx_vcode_client___test_test___vcode_client_test___vcode_client : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        edtRequestThirdID.Text = RandUtil.NewString(4, RandStringType.Number) + "-" + RandUtil.NewString(3, RandStringType.Lower)
                + "-" + RandUtil.NewString(4, RandStringType.Number);
    }

    protected void btnUploadImage_Click(object sender, EventArgs e)
    {
        lblErrMessage.Visible = false;

        if (FileUploadVoice.FileBytes.Length == 0)
        {
            lblErrMessage.Text = "没有指定上传的文件";
            lblErrMessage.Visible = true;
            return;
        }

        //============ 1. 将上传的文件保存起来 =================
        string sSaveAsFileName = DateTimeUtil.ToDateTime14Str(DateTimeUtil.Now) 
                + RandUtil.NewString(4, RandStringType.Upper);
        string sExtFileName = FileUtil.ExtractFileExt(FileUploadVoice.FileName);

        sSaveAsFileName = sSaveAsFileName + sExtFileName;
        sSaveAsFileName = "c:\\temp\\" + sSaveAsFileName;

        FileUploadVoice.SaveAs(sSaveAsFileName);

        this.PageParameter.SetCustomParamString("voice_fileNameWQGW", sSaveAsFileName);

        //============= 2. 通过接口上传，并得到回执 ============
        IBMVoiceQANREQ req = new IBMVoiceQANREQ();

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

    protected void btnVQANRequest_Click(object sender, EventArgs e)
    {
        lblErrMessage.Visible = false;

        //========= 1. 准备请求 ============
        IBMVoiceQANREQ req = new IBMVoiceQANREQ();

        req.RequestThirdId = edtRequestThirdID.Text.Trim();
        req.UploadReceipt = lblUploadReceipt.Text;
        req.VoiceFileName = this.PageParameter.GetCustomParamString("voice_fileNameWQGW");

        req.SyncCall = cbxSyncCall.Checked;

        //============ 2. 填写回执并得到结果 ============
        IBMVoiceQANRESP resp = new IBMVoiceQANRESP();
        IBXBusClient.Instance.GetResponse(req, resp);

        if (resp.ErrorCode != 0)
        {
            lblErrMessage.Text = resp.GetMessageDescription();
            lblErrMessage.Visible = true;
            return;
        }

        lblRequestRET.Text = "【" + DateTimeUtil.NowWithMilliSeconds + "】请求返回";
    }

    protected void btnFetchBreakResult_Click(object sender, EventArgs e)
    {
        lblErrMessage.Visible = false;

        IBMVoiceQANResultFetchREQ req = new IBMVoiceQANResultFetchREQ();

        IBMVoiceQANResultFetchRESP resp = new IBMVoiceQANResultFetchRESP();

        IBXBusClient.Instance.GetResponse(req, resp);

        if (resp.ErrorCode != 0)
        {
            lblErrMessage.Text = resp.GetMessageDescription();
            lblErrMessage.Visible = true;
            return;
        }

        if (resp.QualityValue01 == 0)
        {
            lblFetchResult.Text = "[empty.  wait. or fetched.]";
            return;
        }

        lblFetchResult.Text = resp.QualityValue01.ToString("0.000") + " / "
                + resp.QualityValue02.ToString("0.000") + " / "
                + resp.QualityValue03.ToString("0.000")
                + " ( " + resp.RequestThirdID + " / " + resp.UploadReceipt + " ) ";
    }
}

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;

using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Framework;
using Sigbit.Web.WebControlUtil;
using Sigbit.App.Net.IBXService.VoiceReg.Service.DBDefine;

public partial class genui_KTJQ_log_vcode_break_modify : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        btnInput.Enabled = false;

        btnRefresh_Click(sender, e);
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        btnInput.Enabled = false;

        lblGrammarId.Text = "";
        lblUploadReceipt.Text = "";
        lblRequestThirdId.Text = "";
        lblRequestTime.Text = "";
        lblLocalVoiceFileName.Text = "";
        lblVoiceFileName.Text = "";
        divPlayVoice.Text = "";
        edtRegResultText.Text = "";

        //======== 1. 查到最早的状态为request的记录 ============
        string sSQLEarliest = "select * from vreg_log_voice_isr where current_status = 'request' order by request_time limit 1";
        DataSet dsEarliest = DataHelper.Instance.ExecuteDataSet(sSQLEarliest);

        if (dsEarliest.Tables[0].Rows.Count <= 0)
        {
            lblErrMessage.Text = "未找到需要语音识别的记录，可以稍后再刷新或进入自动等待页面。";
            lblErrMessage.Visible = true;
            return;
        }

        lblErrMessage.Visible = false;
        TbLogVoiceIsr tblLogISR = new TbLogVoiceIsr();
        tblLogISR.AssignByDataRow(dsEarliest, 0);

        tblLogISR.IsrBeginTime = DateTimeUtil.NowWithMilliSeconds;
        tblLogISR.Update();

        //========== 2. 显示相关信息 ==========
        lblGrammarId.Text = tblLogISR.GrammarId;
        lblUploadReceipt.Text = tblLogISR.LogUid;
        lblRequestThirdId.Text = tblLogISR.ReqeustThirdId;
        lblRequestTime.Text = tblLogISR.RequestTime;
        lblLocalVoiceFileName.Text = tblLogISR.VoiceFileLocal;
        lblVoiceFileName.Text = tblLogISR.VoiceFileForIsr;

        ViewState["log_uid_DRDP"] = tblLogISR.LogUid;

        //========= 3. 语音播放的链接 =========
        divPlayVoice.Text = GenObjectHtml("../../data/voice_reg/voice_files/" + tblLogISR.VoiceFileForIsr);

        //lnkPlayVoice.NavigateUrl = "../../data/voice_reg/voice_files/" + tblLogISR.VoiceFileForIsr;
        edtRegResultText.Text = "";
        btnInput.Enabled = true;
    }

    protected void btnInput_Click(object sender, EventArgs e)
    {
        string sLogUid = ConvertUtil.ToString(ViewState["log_uid_DRDP"]);

        TbLogVoiceIsr tblLogISR = new TbLogVoiceIsr();
        tblLogISR.LogUid = sLogUid;
        tblLogISR.Fetch();

        string sInputText = edtRegResultText.Text.Trim();
        if (sInputText == "")
            tblLogISR.RegResultCode = "fail";
        else
        {
            tblLogISR.RegResultCode = "succ";
            tblLogISR.RegResultText = sInputText;
        }

        tblLogISR.CurrentStatus = "reged";
        tblLogISR.IsrEndTime = DateTimeUtil.NowWithMilliSeconds;
        tblLogISR.BreakDelay = DateTimeUtil.MilliSecondsAfter(tblLogISR.IsrBeginTime, tblLogISR.IsrEndTime) / 1000.0;

        tblLogISR.Update();

        lblErrMessage.Text = "已填入识别结果" + sInputText;
        lblErrMessage.Visible = true;
        btnInput.Enabled = false;

        btnRefresh_Click(sender, e);
    }

    protected void btnHaveARest_Click(object sender, EventArgs e)
    {
        Response.Redirect("have_a_rest.aspx");
    }

    protected string GenObjectHtml(string sFileName)
    {
        string sRet = "";

        sRet = @"  <object align='middle' class='OBJECT' classid='CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95'
                                height='40' id='mpStandard' style='width: 270' >
                                <param name='Filename' value='{0}'>
                                <param name='AutoStart' value='0'>
                                <param name='ShowStatusBar' value='0'>
                                <param name='ShowTracker' value='1'>
                                <param name='ShowGotoBar' value='0'>
                                <param name='ShowPositionControls' value='1'>
                                <param name='ShowCaptioning' value='0'>
                                <param name='ShowControls' value='1'>
                                <param name='ShowAudioControls' value='0'>
                                <param name='ShowDisplay' value='0'>
                            </object>";

        sRet = string.Format(sRet, sFileName);

        return sRet;
    }



    /*
                    <asp:HyperLink ID="lnkPlayVoice" runat="server" 
                        NavigateUrl="../../data/voice_reg/voice_files/20121012134700-xzlg.wav" 
                        Font-Underline="True" ForeColor="Blue">播放语音</asp:HyperLink>
     */


}

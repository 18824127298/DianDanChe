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

using Sigbit.App.Net.IBXService.VoiceQAN.Service.DBDefine;

public partial class genui_KTJQ_log_vcode_break_modify : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        btnInput.Enabled = false;
        ResetAllRBList();

        btnRefresh_Click(sender, e);
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        btnInput.Enabled = false;

        lblUploadReceipt.Text = "";
        lblRequestThirdId.Text = "";
        lblRequestTime.Text = "";
        lblLocalVoiceFileName.Text = "";
        lblVoiceFileName.Text = "";
        divPlayVoice.Text = "";

        //======== 1. 查到最早的状态为request的记录 ============
        string sSQLEarliest = "select * from vqan_log_voice_qan where current_status = 'request' order by request_time limit 1";
        DataSet dsEarliest = DataHelper.Instance.ExecuteDataSet(sSQLEarliest);

        if (dsEarliest.Tables[0].Rows.Count <= 0)
        {
            lblErrMessage.Text = "未找到需要语音识别的记录，可以稍后再刷新或进入自动等待页面。";
            lblErrMessage.Visible = true;
            return;
        }

        lblErrMessage.Visible = false;

        TbLogVoiceQan tblVQan = new TbLogVoiceQan();
        tblVQan.AssignByDataRow(dsEarliest, 0);

        tblVQan.QanBeginTime = DateTimeUtil.NowWithMilliSeconds;
        tblVQan.Update();

        //========== 2. 显示相关信息 ==========
        lblUploadReceipt.Text = tblVQan.LogUid;
        lblRequestThirdId.Text = tblVQan.RequestThirdId;
        lblRequestTime.Text = tblVQan.RequestTime;
        lblLocalVoiceFileName.Text = tblVQan.VoiceFileLocal;
        lblVoiceFileName.Text = tblVQan.VoiceFileForQan;

        ViewState["log_uid_DRDP"] = tblVQan.LogUid;

        //========= 3. 语音播放的链接 =========
        divPlayVoice.Text = GenObjectHtml("../../data/voice_qan/voice_files/" + tblVQan.VoiceFileForQan);

        ResetAllRBList();

        btnInput.Enabled = true;
    }

    protected void btnInput_Click(object sender, EventArgs e)
    {
        string sLogUid = ConvertUtil.ToString(ViewState["log_uid_DRDP"]);

        TbLogVoiceQan tblLogVQan = new TbLogVoiceQan();
        tblLogVQan.LogUid = sLogUid;
        tblLogVQan.Fetch();

        tblLogVQan.QualityValue01 = ConvertUtil.ToFloat(edtNoiseValue.Text.Trim());
        tblLogVQan.QualityValue02 = ConvertUtil.ToFloat(edtVolumeValue.Text.Trim());
        tblLogVQan.QualityValue03 = ConvertUtil.ToFloat(edtSmoothValue.Text.Trim());

        tblLogVQan.CurrentStatusE = TbLogVoiceQanECurrentStatus.Qaned;
        tblLogVQan.QanEndTime = DateTimeUtil.NowWithMilliSeconds;
        tblLogVQan.QanDelay = DateTimeUtil.MilliSecondsAfter(tblLogVQan.QanBeginTime, tblLogVQan.QanEndTime) / 1000.0;

        tblLogVQan.Update();

        lblErrMessage.Text = "【" + DateTimeUtil.NowWithMilliSeconds + "】已填入分析结果";
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

    private double GetQualityValueOfSelectedIndex(int nSelectIndex)
    {
        switch (nSelectIndex)
        {
            case 0:
                return GetQualityValueOfSelectedIndex_RandOutValue(0, 12);
            case 1:
                return GetQualityValueOfSelectedIndex_RandOutValue(12, 25);
            case 2:
                return GetQualityValueOfSelectedIndex_RandOutValue(25, 42);
            case 3:
                return GetQualityValueOfSelectedIndex_RandOutValue(42, 58);
            case 4:
                return GetQualityValueOfSelectedIndex_RandOutValue(58, 85);
            case 5:
                return GetQualityValueOfSelectedIndex_RandOutValue(85, 100);
            default:
                throw new Exception("select index of Radio Button List is out of range");
        }
    }

    private double GetQualityValueOfSelectedIndex_RandOutValue(double fMinValue, double fMaxValue)
    {
        double[] arrYPValues = new double[3];

        for (int i = 0; i < arrYPValues.Length; i++)
        {
            arrYPValues[i] = RandUtil.NewFloat(fMinValue, fMaxValue);
        }

        Array.Sort(arrYPValues);

        return arrYPValues[1];
    }

    protected void rbListNoise_SelectedIndexChanged(object sender, EventArgs e)
    {
        edtNoiseValue.Text = GetQualityValueOfSelectedIndex(rbListNoise.SelectedIndex).ToString("0.000");
    }

    protected void rbListVolume_SelectedIndexChanged(object sender, EventArgs e)
    {
        edtVolumeValue.Text = GetQualityValueOfSelectedIndex(rbListVolume.SelectedIndex).ToString("0.000");
    }

    protected void rbListSmooth_SelectedIndexChanged(object sender, EventArgs e)
    {
        edtSmoothValue.Text = GetQualityValueOfSelectedIndex(rbListSmooth.SelectedIndex).ToString("0.000");
    }

    private void ResetAllRBList()
    {
        rbListNoise.SelectedIndex = 1;
        rbListVolume.SelectedIndex = 3;
        rbListSmooth.SelectedIndex = 4;

        edtNoiseValue.Text = GetQualityValueOfSelectedIndex(rbListNoise.SelectedIndex).ToString("0.000");
        edtVolumeValue.Text = GetQualityValueOfSelectedIndex(rbListVolume.SelectedIndex).ToString("0.000");
        edtSmoothValue.Text = GetQualityValueOfSelectedIndex(rbListSmooth.SelectedIndex).ToString("0.000");
    }
}

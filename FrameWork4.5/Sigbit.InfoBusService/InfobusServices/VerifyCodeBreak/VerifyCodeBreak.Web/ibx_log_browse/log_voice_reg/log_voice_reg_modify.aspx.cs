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

using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Framework;
using Sigbit.Web.WebControlUtil;
using Sigbit.App.Net.IBXService.VoiceReg.Service.DBDefine;

public partial class genui_NVAI_log_voice_reg_modify : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        //========== 1. 初始化界面 ==========
        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 4. 取数据 ==========
        TbLogVoiceIsr tbl = new TbLogVoiceIsr();
        tbl.LogUid = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. 更新各控件的显示 ==========
        lblUploadTime.Text = tbl.UploadTime;
        lblReqeustThirdId.Text = tbl.ReqeustThirdId;
        lblVoiceFileLocal.Text = tbl.VoiceFileLocal;
        lblVoiceFileUpload.Text = tbl.VoiceFileUpload;
        lblVoiceFileForIsr.Text = tbl.VoiceFileForIsr;
        lblGrammarId.Text = tbl.GrammarId;
        lblFromSystem.Text = tbl.FromSystem;
        lblFromClientId.Text = tbl.FromClientId;
        lblFromClientDesc.Text = tbl.FromClientDesc;
        lblCurrentStatus.Text = tbl.CurrentStatus;
        lblRegResultCode.Text = tbl.RegResultCode;
        lblRegFailDesc.Text = tbl.RegFailDesc;
        lblRegResultText.Text = tbl.RegResultText;
        lblRequestTime.Text = tbl.RequestTime;
        lblIsrBeginTime.Text = tbl.IsrBeginTime;
        lblIsrEndTime.Text = tbl.IsrEndTime;
        lblResultFetchTime.Text = tbl.ResultFetchTime;
        lblBreakDelay.Text = tbl.BreakDelay.ToString();
        lblTotalDelay.Text = tbl.TotalDelay.ToString();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("log_voice_reg_list.aspx");
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbLogVoiceIsr tbl = new TbLogVoiceIsr();
            tbl.LogUid = sSelectedID;
            tbl.Delete();
        }
    }
}

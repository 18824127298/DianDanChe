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
using Sigbit.App.Net.IBXService.VoiceQAN.Service.DBDefine;

public partial class genui_FBSL_log_voice_qan_modify : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        //========== 0. 删除记录 ==========
        string sDelRecords = ConvertUtil.ToString(Request["del_rec"]);
        if (sDelRecords != "")
        {
            DeleteData(sDelRecords);
            Response.Redirect("log_voice_qan_list.aspx");
            return;
        }

        //========== 1. 初始化界面 ==========

        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. 新增的判断和处理 ==========

        //========== 4. 取数据 ==========
        TbLogVoiceQan tbl = new TbLogVoiceQan();
        tbl.LogUid = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. 更新各控件的显示 ==========
        lblUploadTime.Text = tbl.UploadTime;
        lblRequestTime.Text = tbl.RequestTime;

        if (tbl.IsSyncCall == "Y")
            lblIsSyncCall.Text = "同步调用";
        else if (tbl.IsSyncCall == "N")
            lblIsSyncCall.Text = "异步调用";
        else
            lblIsSyncCall.Text = "";
        
        lblReqeustThirdId.Text = tbl.RequestThirdId;
        lblVoiceFileLocal.Text = tbl.VoiceFileLocal;
        lblVoiceFileUpload.Text = tbl.VoiceFileUpload;
        lblVoiceFileForQan.Text = tbl.VoiceFileForQan;
        lblCurrentStatus.Text = EnumExUtil.ToDescString(tbl.CurrentStatusE);
        lblQualityValue01.Text = tbl.QualityValue01.ToString("0.000") + " / " + tbl.QualityValue02.ToString("0.000") + " / " + tbl.QualityValue03.ToString("0.000");
        lblQanBeginTime.Text = tbl.QanBeginTime;
        lblQanEndTime.Text = tbl.QanEndTime;
        lblResultFetchTime.Text = tbl.ResultFetchTime;
        lblQanDelay.Text = tbl.QanDelay.ToString("0.000");
        lblTotalDelay.Text = tbl.TotalDelay.ToString("0.000");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("log_voice_qan_list.aspx");
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbLogVoiceQan tbl = new TbLogVoiceQan();
            tbl.LogUid = sSelectedID;
            tbl.Delete();
        }
    }
}

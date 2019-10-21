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
using Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine;

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

        lblVCodeId.Text = "";
        lblUploadReceipt.Text = "";
        lblRequestThirdId.Text = "";
        lblRequestTime.Text = "";
        lblLocalImageFileName.Text = "";
        lblImageFileName.Text = "";
        trImageToBreak.Visible = false;
        edtBreakResultText.Text = "";

        //======== 1. 查到最早的状态为request的记录 ============
        string sSQLEarliest = "select * from vcb_log_vcode_break where current_status = 'request' order by request_time limit 1";
        DataSet dsEarliest = DataHelper.Instance.ExecuteDataSet(sSQLEarliest);

        if (dsEarliest.Tables[0].Rows.Count <= 0)
        {
            lblErrMessage.Text = "未找到需要语音识别的记录，可以稍后再刷新或进入自动等待页面。";
            lblErrMessage.Visible = true;
            return;
        }

        lblErrMessage.Visible = false;

        TbLogVcodeBreak tblLogBreak = new TbLogVcodeBreak();
        tblLogBreak.AssignByDataRow(dsEarliest, 0);

        tblLogBreak.BreakBeginTime = DateTimeUtil.NowWithMilliSeconds;
        tblLogBreak.Update();

        //========== 2. 显示相关信息 ==========
        lblVCodeId.Text = tblLogBreak.VcodeId;
        lblUploadReceipt.Text = tblLogBreak.LogUid;
        lblRequestThirdId.Text = tblLogBreak.RequestThirdId;
        lblRequestTime.Text = tblLogBreak.RequestTime;
        lblLocalImageFileName.Text = tblLogBreak.ImageFileLocal;
        lblImageFileName.Text = tblLogBreak.ImageFileForBreak;

        ViewState["log_uid_DRDP"] = tblLogBreak.LogUid;

        //========= 3. 图像显示 =========
        imgToBreak.ImageUrl = "../../data/vcode_break/vcode_images/" + tblLogBreak.ImageFileForBreak;
        trImageToBreak.Visible = true;

        edtBreakResultText.Text = "";
        btnInput.Enabled = true;
    }

    protected void btnInput_Click(object sender, EventArgs e)
    {
        string sLogUid = ConvertUtil.ToString(ViewState["log_uid_DRDP"]);

        TbLogVcodeBreak tblLogBreak = new TbLogVcodeBreak();
        tblLogBreak.LogUid = sLogUid;
        tblLogBreak.Fetch();

        string sInputText = edtBreakResultText.Text.Trim();
        if (sInputText == "")
            tblLogBreak.BreakResult = "fail";
        else
        {
            tblLogBreak.BreakResult = "succ";
            tblLogBreak.BreakText = sInputText;
        }

        tblLogBreak.CurrentStatus = "broken";
        tblLogBreak.BreakEndTime = DateTimeUtil.NowWithMilliSeconds;
        tblLogBreak.BreakDelay = DateTimeUtil.MilliSecondsAfter(tblLogBreak.BreakBeginTime, tblLogBreak.BreakEndTime) / 1000.0;

        tblLogBreak.Update();

        lblErrMessage.Text = "已填入识别结果" + sInputText;
        lblErrMessage.Visible = true;
        btnInput.Enabled = false;

        btnRefresh_Click(sender, e);
    }

    protected void btnHaveARest_Click(object sender, EventArgs e)
    {
        Response.Redirect("have_a_rest.aspx");
    }
}

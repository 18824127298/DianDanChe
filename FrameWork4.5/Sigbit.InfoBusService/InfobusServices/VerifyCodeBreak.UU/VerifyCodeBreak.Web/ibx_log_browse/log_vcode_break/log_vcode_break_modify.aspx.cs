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
using Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine;

public partial class genui_KTJQ_log_vcode_break_modify : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        //========== 1. 初始化界面 ==========
        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. 新增的判断和处理 ==========

        //========== 4. 取数据 ==========
        TbLogVcodeBreak tbl = new TbLogVcodeBreak();
        tbl.LogUid = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. 更新各控件的显示 ==========
        lblRequestTime.Text = tbl.RequestTime;
        lblImageFileName.Text = tbl.ImageFileForBreak;

        imgToBreak.ImageUrl = "../../data/vcode_break/vcode_images/" + tbl.ImageFileForBreak;

        lblVcodeId.Text = tbl.VcodeId;
        lblAlgolId.Text = tbl.AlgolId;
        lblFromClientId.Text = tbl.FromClientId;
        lblCurrentStatus.Text = tbl.CurrentStatus;
        lblBreakResult.Text = tbl.BreakResult;
        lblFailDesc.Text = tbl.FailDesc;
        lblBreakText.Text = tbl.BreakText;
        lblFetchTime.Text = tbl.ResultFetchTime;
        lblBreakTime.Text = tbl.BreakEndTime;
        lblBreakDelay.Text = tbl.BreakDelay.ToString();
        lblTotalDelay.Text = tbl.TotalDelay.ToString();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("log_vcode_break_list.aspx");
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbLogVcodeBreak tbl = new TbLogVcodeBreak();
            tbl.LogUid = sSelectedID;
            tbl.Delete();
        }
    }
}

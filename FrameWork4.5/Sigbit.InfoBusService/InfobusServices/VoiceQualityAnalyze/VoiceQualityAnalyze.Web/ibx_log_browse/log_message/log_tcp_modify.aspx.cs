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

using Sigbit.App.Net.IBXService.DBDefine;

public partial class genui_VIZP_log_tcp_modify : SbtPageBase
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
        TbLogMessage tbl = new TbLogMessage();
        tbl.LogUid = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. 更新各控件的显示 ==========
        lblCommandIdEng.Text = tbl.TransCodeEng;
        lblCommandIdChs.Text = tbl.TransCodeChs;

        lblRequestTime.Text = tbl.RequestTime;
        lblCallDuration.Text = tbl.CallDuration.ToString() + " ms";

        lblRequestPacket.Text = GetXmlDisplayString(tbl.RequestPacket);
        lblRequestDesc.Text = tbl.RequestDesc;

        lblResponsePacket.Text = GetXmlDisplayString(tbl.ResponsePacket);
        lblResponseDesc.Text = tbl.ResponseDesc;
    }

    private string GetXmlDisplayString(string sXmlString)
    {
        if (sXmlString.IndexOf("?") == 0)
            sXmlString = sXmlString.Substring(1);

        string sRet = HttpUtility.HtmlEncode(sXmlString);

        sRet = sRet.Replace(" ", "&nbsp;");
        sRet = sRet.Replace("\r\n", "<br />");
        sRet = sRet.Replace("\n", "<br />");

        return sRet;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("log_tcp_list.aspx");
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbLogMessage tbl = new TbLogMessage();
            tbl.LogUid = sSelectedID;
            tbl.Delete();
        }
    }
}

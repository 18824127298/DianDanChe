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
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ////======== 1. �鵽�����״̬Ϊrequest�ļ�¼ ============
        //string sSQLEarliest = "select * from vcb_log_vcode_break where current_status = 'request' order by request_time limit 1";
        //DataSet dsEarliest = DataHelper.Instance.ExecuteDataSet(sSQLEarliest);

        //if (dsEarliest.Tables[0].Rows.Count <= 0)
        //{
        //    lblErrMessage.Text = "δ�ҵ���¼";
        //    lblErrMessage.Visible = true;
        //    return;
        //}

        //lblErrMessage.Visible = false;
        //TbLogVcodeBreak tblLogBreak = new TbLogVcodeBreak();
        //tblLogBreak.AssignByDataRow(dsEarliest, 0);

        //tblLogBreak.FetchTime = DateTimeUtil.NowWithMilliSeconds;
        //tblLogBreak.FetchDelay = DateTimeUtil.MilliSecondsAfter(tblLogBreak.RequestTime, tblLogBreak.FetchTime);
        //tblLogBreak.Update();

        ////========== 2. ��ʾ�����Ϣ ==========
        //lblVCodeId.Text = tblLogBreak.VcodeId;
        //lblUploadReceipt.Text = tblLogBreak.LogUid;
        //lblRequestTime.Text = tblLogBreak.RequestTime;
        //lblImageFileName.Text = tblLogBreak.ImageFileName;

        //ViewState["log_uid_DRDP"] = tblLogBreak.LogUid;

        ////========= 3. ͼ����Ϣ����ʾ =========
        ////========= 3.1 ��ͼ���ļ�����Ϊ����չ�����ļ� ===========
        //string sCurrentFileName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\data\\vcode_break\\vcode_images\\"
        //        +tblLogBreak.LogUid;
        //string sFileExt = FileUtil.ExtractFileExt(tblLogBreak.ImageFileName);
        //string sTargetFileName = sCurrentFileName + sFileExt;

        //File.Copy(sCurrentFileName, sTargetFileName, true);

        ////=========== 3.2 ����ʾ��ͼ���ΪĿ���ͼ�� ============
        //imgToBreak.ImageUrl = "~/data/vcode_break/vcode_images/" + tblLogBreak.LogUid + sFileExt;
    }

    protected void btnInput_Click(object sender, EventArgs e)
    {
        string sLogUid = ConvertUtil.ToString(ViewState["log_uid_DRDP"]);

        TbLogVcodeBreak tblLogBreak = new TbLogVcodeBreak();
        tblLogBreak.LogUid = sLogUid;
        tblLogBreak.Fetch();

        string sInputText = edtBreakResult.Text.Trim();
        if (sInputText == "")
            tblLogBreak.BreakResult = "fail";
        else
        {
            tblLogBreak.BreakResult = "succ";
            tblLogBreak.BreakText = sInputText;
        }

        //tblLogBreak.CurrentStatus = "broken";
        //tblLogBreak.BreakTime = DateTimeUtil.NowWithMilliSeconds;
        //tblLogBreak.BreakDelay = DateTimeUtil.MilliSecondsAfter(tblLogBreak.FetchTime, tblLogBreak.BreakTime) / 1000.0;
        //tblLogBreak.TotalDelay = DateTimeUtil.MilliSecondsAfter(tblLogBreak.RequestTime, tblLogBreak.BreakTime) / 1000.0;

        tblLogBreak.Update();

        lblErrMessage.Text = "��������֤��" + sInputText;
        lblErrMessage.Visible = true;
    }
}

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

public partial class genui_LYOG_vcode_scene_modify : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        //========== 0. ɾ����¼ ==========
        string sDelRecords = ConvertUtil.ToString(Request["del_rec"]);
        if (sDelRecords != "")
        {
            DeleteData(sDelRecords);
            Response.Redirect("vcode_scene_list.aspx");
            return;
        }

        //========== 1. ��ʼ������ ==========
        WCUComboBox.InitComboBox(ddlbAlgolId, QDBVCBreakPools.PoolAlgol.CodeTableOfAll);
        ddlbAlgolId.Items.Add(new ListItem("[δָ��]", "NONE"));
        ddlbAlgolId.SelectedValue = "NONE";

        //========== 2. ȡ���������� ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. �������жϺʹ��� ==========
        if (sRecordPrimaryKey == "")
        {
            lblVcodeId.Visible = false;

            lblTitle.Text = "������֤�볡��";
            btnOK.Text = "����";
            return;
        }

        //========== 4. ȡ���� ==========
        TbSysVcode tbl = new TbSysVcode();
        tbl.VcodeId = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. ���¸��ؼ�����ʾ ==========
        edtVcodeId.Visible = false;
        lblVcodeId.Text = tbl.VcodeId;
        edtVcodeId.Text = tbl.VcodeId;
        edtVcodeName.Text = tbl.VcodeName;
        edtVcodeDesc.Text = tbl.VcodeDesc;
        ddlbAlgolId.SelectedValue = tbl.AlgolId;
        edtAlgolParams.Text = tbl.AlgolParams;
        edtCallRate.Text = tbl.CallRate.ToString();
        edtCallFakeMinSec.Text = tbl.CallFakeMinSec.ToString();
        edtCallFakeMaxSec.Text = tbl.CallFakeMaxSec.ToString();
        edtCallForceMinSec.Text = tbl.CallForceMinSec.ToString();
        edtCallForceMaxSec.Text = tbl.CallForceMaxSec.ToString();
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool bAppendMode = false;
        string sParamRecKey = ConvertUtil.ToString(ViewState["rec_key"]);
        if (sParamRecKey == "")
            bAppendMode = true;

        //========== 1. ��֤���ݵ���Ч�� ==========
        //========== 1.1 �ж���֤���ʶ�Ƿ�Ϊ�� ==========
        string sVcodeId = edtVcodeId.Text.Trim();
        if (sVcodeId == "")
        {
            lblErrMessage.Text = "������д��֤���ʶ";
            lblErrMessage.Visible = true;
            edtVcodeId.Focus();
            return;
        }

        //========== 1.2 �ж���֤�������Ƿ�Ϊ�� ==========
        string sVcodeName = edtVcodeName.Text.Trim();
        if (sVcodeName == "")
        {
            lblErrMessage.Text = "������д��֤������";
            lblErrMessage.Visible = true;
            edtVcodeName.Focus();
            return;
        }

        //========== 1.3 �ж��㷨�Ƿ�Ϊ�� ==========
        string sAlgolId = ddlbAlgolId.SelectedValue;
        if (sAlgolId == "NONE")
        {
            lblErrMessage.Text = "����ѡ���㷨";
            lblErrMessage.Visible = true;
            ddlbAlgolId.Focus();
            return;
        }

        //========== 2. ������������ ==========
        TbSysVcode tbl = new TbSysVcode();
        if (bAppendMode)
        {
            tbl.VcodeId = edtVcodeId.Text.Trim();
            tbl.VcodeName = edtVcodeName.Text.Trim();
            tbl.VcodeDesc = edtVcodeDesc.Text.Trim();
            tbl.AlgolId = ddlbAlgolId.SelectedValue;
            tbl.AlgolParams = edtAlgolParams.Text.Trim();
            tbl.CallRate = ConvertUtil.ToFloat(edtCallRate.Text.Trim());
            tbl.CallFakeMinSec = ConvertUtil.ToFloat(edtCallFakeMinSec.Text.Trim());
            tbl.CallFakeMaxSec = ConvertUtil.ToFloat(edtCallFakeMaxSec.Text.Trim());
            tbl.CallForceMinSec = ConvertUtil.ToFloat(edtCallForceMinSec.Text.Trim());
            tbl.CallForceMaxSec = ConvertUtil.ToFloat(edtCallForceMaxSec.Text.Trim());
            tbl.CreateTime = DateTimeUtil.Now;
            tbl.Creator = CurrentUser.UserName;
            tbl.ModifyTime = DateTimeUtil.Now;

            tbl.Insert();
        }

        //========== 3. ���ݸ��´��� ==========
        else
        {
            tbl.VcodeId = sParamRecKey;
            tbl.Fetch();

            //tbl.VcodeId = edtVcodeId.Text.Trim();
            tbl.VcodeName = edtVcodeName.Text.Trim();
            tbl.VcodeDesc = edtVcodeDesc.Text.Trim();
            tbl.AlgolId = ddlbAlgolId.SelectedValue;
            tbl.AlgolParams = edtAlgolParams.Text.Trim();
            tbl.CallRate = ConvertUtil.ToFloat(edtCallRate.Text.Trim());
            tbl.CallFakeMinSec = ConvertUtil.ToFloat(edtCallFakeMinSec.Text.Trim());
            tbl.CallFakeMaxSec = ConvertUtil.ToFloat(edtCallFakeMaxSec.Text.Trim());
            tbl.CallForceMinSec = ConvertUtil.ToFloat(edtCallForceMinSec.Text.Trim());
            tbl.CallForceMaxSec = ConvertUtil.ToFloat(edtCallForceMaxSec.Text.Trim());
            tbl.ModifyTime = DateTimeUtil.Now;

            tbl.Update();
        }

        //========== 4. ���ص���ҳ�� ==========
        Response.Redirect("vcode_scene_list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("vcode_scene_list.aspx");
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbSysVcode tbl = new TbSysVcode();
            tbl.VcodeId = sSelectedID;
            tbl.Delete();
        }
    }
}

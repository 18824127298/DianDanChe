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

public partial class genui_AURP_break_algol_modify : SbtPageBase
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
            Response.Redirect("break_algol_list.aspx");
            return;
        }

        //========== 1. ��ʼ������ ==========
        //========== 2. ȡ���������� ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. �������жϺʹ��� ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "������֤���ƽ��㷨";
            btnOK.Text = "����";
            lblAlgolId.Visible = false;
            return;
        }

        edtAlgolId.Visible = false;

        //========== 4. ȡ���� ==========
        TbSysBreakAlgol tbl = new TbSysBreakAlgol();
        tbl.AlgolId = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. ���¸��ؼ�����ʾ ==========
        edtAlgolId.Text = tbl.AlgolId;
        lblAlgolId.Text = tbl.AlgolId;
        edtAlgolName.Text = tbl.AlgolName;
        edtAlgolDesc.Text = tbl.AlgolDesc;
        edtAlgolData01.Text = tbl.AlgolData01;
        edtAlgolData02.Text = tbl.AlgolData02;
        edtAlgolData03.Text = tbl.AlgolData03;
        edtAlgolData04.Text = tbl.AlgolData04;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool bAppendMode = false;
        string sParamRecKey = ConvertUtil.ToString(ViewState["rec_key"]);
        if (sParamRecKey == "")
            bAppendMode = true;

        //========== 1. ��֤���ݵ���Ч�� ==========
        //========== 1.1 �ж��㷨��ʶ�Ƿ�Ϊ�� ==========
        string sAlgolId = edtAlgolId.Text.Trim();
        if (sAlgolId == "")
        {
            lblErrMessage.Text = "������д�㷨��ʶ";
            lblErrMessage.Visible = true;
            edtAlgolId.Focus();
            return;
        }

        //========== 1.2 �ж��㷨�����Ƿ�Ϊ�� ==========
        string sAlgolName = edtAlgolName.Text.Trim();
        if (sAlgolName == "")
        {
            lblErrMessage.Text = "������д�㷨����";
            lblErrMessage.Visible = true;
            edtAlgolName.Focus();
            return;
        }

        //========== 2. ������������ ==========
        TbSysBreakAlgol tbl = new TbSysBreakAlgol();
        if (bAppendMode)
        {
            tbl.AlgolId = edtAlgolId.Text.Trim();
            tbl.AlgolName = edtAlgolName.Text.Trim();
            tbl.AlgolDesc = edtAlgolDesc.Text.Trim();
            tbl.AlgolData01 = edtAlgolData01.Text.Trim();
            tbl.AlgolData02 = edtAlgolData02.Text.Trim();
            tbl.AlgolData03 = edtAlgolData03.Text.Trim();
            tbl.AlgolData04 = edtAlgolData04.Text.Trim();
            tbl.CreateTime = DateTimeUtil.Now;
            tbl.Creator = CurrentUser.UserName;
            tbl.ModifyTime = DateTimeUtil.Now;

            tbl.Insert();
        }

        //========== 3. ���ݸ��´��� ==========
        else
        {
            tbl.AlgolId = sParamRecKey;
            tbl.Fetch();

            //tbl.AlgolId = edtAlgolId.Text.Trim();
            tbl.AlgolName = edtAlgolName.Text.Trim();
            tbl.AlgolDesc = edtAlgolDesc.Text.Trim();
            tbl.AlgolData01 = edtAlgolData01.Text.Trim();
            tbl.AlgolData02 = edtAlgolData02.Text.Trim();
            tbl.AlgolData03 = edtAlgolData03.Text.Trim();
            tbl.AlgolData04 = edtAlgolData04.Text.Trim();
            tbl.ModifyTime = DateTimeUtil.Now;

            tbl.Update();
        }

        //========== 4. ���ص���ҳ�� ==========
        Response.Redirect("break_algol_list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("break_algol_list.aspx");
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbSysBreakAlgol tbl = new TbSysBreakAlgol();
            tbl.AlgolId = sSelectedID;
            tbl.Delete();
        }
    }
}

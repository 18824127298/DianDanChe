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
using Sigbit.App.Net.IBXService.DNS.Service.DBDefine;

public partial class genui_FIEI_dns_service_modify : SbtPageBase
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
            Response.Redirect("dns_service_list.aspx");
            return;
        }

        //========== 1. ��ʼ������ ==========
        //========== 2. ȡ���������� ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. �������жϺʹ��� ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "��������";
            btnOK.Text = "����";

            lblServiceID.Visible = false;

            return;
        }

        edtServiceId.Visible = false;

        //========== 4. ȡ���� ==========
        TbSysService tbl = new TbSysService();
        tbl.ServiceId = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. ���¸��ؼ�����ʾ ==========
        lblServiceID.Text = tbl.ServiceId;
        edtServiceName.Text = tbl.ServiceName;
        edtServiceDesc.Text = tbl.ServiceDesc;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool bAppendMode = false;
        string sParamRecKey = ConvertUtil.ToString(ViewState["rec_key"]);
        if (sParamRecKey == "")
            bAppendMode = true;

        //========== 1. ��֤���ݵ���Ч�� ==========
        //========== 1.1 �жϷ����ʶ�Ƿ�Ϊ�� ==========
        string sServiceId = edtServiceId.Text.Trim();
        if (bAppendMode && sServiceId == "")
        {
            lblErrMessage.Text = "������д�����ʶ";
            lblErrMessage.Visible = true;
            edtServiceId.Focus();
            return;
        }

        //========== 1.2 �ж������Ƿ�Ϊ�� ==========
        string sServiceName = edtServiceName.Text.Trim();
        if (sServiceName == "")
        {
            lblErrMessage.Text = "������д����";
            lblErrMessage.Visible = true;
            edtServiceName.Focus();
            return;
        }

        //========== 2. ������������ ==========
        TbSysService tbl = new TbSysService();
        if (bAppendMode)
        {
            tbl.ServiceId = edtServiceId.Text.Trim();
            tbl.ServiceName = edtServiceName.Text.Trim();
            tbl.ServiceDesc = edtServiceDesc.Text.Trim();
            tbl.CreateTime = DateTimeUtil.Now;
            tbl.Creator = CurrentUser.UserName;
            tbl.ModifyTime = DateTimeUtil.Now;

            tbl.Insert();
        }

        //========== 3. ���ݸ��´��� ==========
        else
        {
            tbl.ServiceId = sParamRecKey;
            tbl.Fetch();

            tbl.ServiceName = edtServiceName.Text.Trim();
            tbl.ServiceDesc = edtServiceDesc.Text.Trim();
            tbl.ModifyTime = DateTimeUtil.Now;

            tbl.Update();
        }

        //========== 4. ���ص���ҳ�� ==========
        Response.Redirect("dns_service_list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("dns_service_list.aspx");
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbSysService tbl = new TbSysService();
            tbl.ServiceId = sSelectedID;
            tbl.Delete();
        }
    }
}

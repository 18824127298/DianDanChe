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

public partial class genui_OZOA_dns_url_address_modify : SbtPageBase
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
            Response.Redirect("dns_url_address_list.aspx");
            return;
        }

        //========== 1. ��ʼ������ ==========
        WCUComboBox.InitComboBox(ddlbServiceId, QDBDnsPools.PoolService.ServiceCodeTable);
        ddlbServiceId.Items.Add(new ListItem("[δָ��]", "NONE"));
        ddlbServiceId.SelectedValue = "NONE";

        //========== 2. ȡ���������� ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. �������жϺʹ��� ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "���������ַ";
            btnOK.Text = "����";
            return;
        }

        //========== 4. ȡ���� ==========
        TbSysUrlAddress tbl = new TbSysUrlAddress();
        tbl.UrlAddressUid = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. ���¸��ؼ�����ʾ ==========
        edtUrlAddressName.Text = tbl.UrlAddressName;
        ddlbServiceId.SelectedValue = tbl.ServiceId;
        edtUrlAddressLink.Text = tbl.UrlAddressLink;
        edtUrlAddressDesc.Text = tbl.UrlAddressDesc;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool bAppendMode = false;
        string sParamRecKey = ConvertUtil.ToString(ViewState["rec_key"]);
        if (sParamRecKey == "")
            bAppendMode = true;

        //========== 1. ��֤���ݵ���Ч�� ==========
        //========== 1.1 �жϵ�ַ�����Ƿ�Ϊ�� ==========
        string sUrlAddressName = edtUrlAddressName.Text.Trim();
        if (sUrlAddressName == "")
        {
            lblErrMessage.Text = "������д��ַ����";
            lblErrMessage.Visible = true;
            edtUrlAddressName.Focus();
            return;
        }

        //========== 1.2 �ж�url��ַ�Ƿ�Ϊ�� ==========
        string sUrlAddressLink = edtUrlAddressLink.Text.Trim();
        if (sUrlAddressLink == "")
        {
            lblErrMessage.Text = "������дurl��ַ";
            lblErrMessage.Visible = true;
            edtUrlAddressLink.Focus();
            return;
        }

        //========== 1.3 �ж�service�Ƿ���ѡ�� =========
        string sServiceId = ddlbServiceId.SelectedValue;
        if (sServiceId == "NONE")
        {
            lblErrMessage.Text = "��ѡ��Ѱַ����";
            lblErrMessage.Visible = true;
            ddlbServiceId.Focus();
            return;
        }

        //========== 2. ������������ ==========
        TbSysUrlAddress tbl = new TbSysUrlAddress();
        if (bAppendMode)
        {
            tbl.UrlAddressUid = Guid.NewGuid().ToString();
            tbl.UrlAddressName = edtUrlAddressName.Text.Trim();
            tbl.ServiceId = ddlbServiceId.SelectedValue;
            tbl.UrlAddressLink = edtUrlAddressLink.Text.Trim();
            tbl.UrlAddressDesc = edtUrlAddressDesc.Text.Trim();
            tbl.CreateTime = DateTimeUtil.Now;
            tbl.ModifyTime = DateTimeUtil.Now;

            tbl.Insert();
        }

        //========== 3. ���ݸ��´��� ==========
        else
        {
            tbl.UrlAddressUid = sParamRecKey;
            tbl.Fetch();

            tbl.UrlAddressName = edtUrlAddressName.Text.Trim();
            tbl.ServiceId = ddlbServiceId.SelectedValue;
            tbl.UrlAddressLink = edtUrlAddressLink.Text.Trim();
            tbl.UrlAddressDesc = edtUrlAddressDesc.Text.Trim();
            tbl.ModifyTime = DateTimeUtil.Now;

            tbl.Update();
        }

        QDBDnsPools.ResetAll();

        //========== 4. ���ص���ҳ�� ==========
        Response.Redirect("dns_url_address_list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("dns_url_address_list.aspx");
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbSysUrlAddress tbl = new TbSysUrlAddress();
            tbl.UrlAddressUid = sSelectedID;
            tbl.Delete();
        }
    }
}

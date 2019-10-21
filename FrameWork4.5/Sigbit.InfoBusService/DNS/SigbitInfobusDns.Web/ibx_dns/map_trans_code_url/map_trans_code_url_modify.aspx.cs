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

public partial class genui_HUTL_map_trans_code_url_modify : SbtPageBase
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
            Response.Redirect("map_trans_code_url_list.aspx");
            return;
        }

        //========== 1. ��ʼ������ ==========
        WCUComboBox.InitComboBox(ddlbServiceId, QDBDnsPools.PoolService.ServiceCodeTable);
        ddlbServiceId.Items.Add(new ListItem("[δָ��]", "NONE"));
        ddlbServiceId.SelectedValue = "NONE";

        WCUComboBox.InitComboBox(ddlbTransCode, QDBDnsPools.PoolTransCode.TransCodeCodeTable);

        WCUComboBox.InitComboBox(ddlbUrlAddressUid, QDBDnsPools.PoolUrlAddress.UrlAddressCodeTable);

        //========== 2. ȡ���������� ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. �������жϺʹ��� ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "����ӳ������";
            btnOK.Text = "����";
            return;
        }

        //========== 4. ȡ���� ==========
        TbMapTransCodeUrl tbl = new TbMapTransCodeUrl();
        tbl.MapUid = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. ���¸��ؼ�����ʾ ==========
        ddlbServiceId.SelectedValue = tbl.ServiceId;
        ddlbTransCode.SelectedValue = tbl.TransCode;
        ddlbUrlAddressUid.SelectedValue = tbl.UrlAddressUid;
        edtFromClientId.Text = tbl.FromClientId;
        edtFromClientVersion.Text = tbl.FromClientVersion;
        edtFromSystem.Text = tbl.FromSystem;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool bAppendMode = false;
        string sParamRecKey = ConvertUtil.ToString(ViewState["rec_key"]);
        if (sParamRecKey == "")
            bAppendMode = true;

        //========== 1. ��֤���ݵ���Ч�� ==========
        //========== 1.1 �ж�Ѱַ�����Ƿ�Ϊ�� ==========
        string sServiceId = ddlbServiceId.Text.Trim();
        if (sServiceId == "")
        {
            lblErrMessage.Text = "������дѰַ����";
            lblErrMessage.Visible = true;
            ddlbServiceId.Focus();
            return;
        }

        //========== 1.2 �жϽ������Ƿ�Ϊ�� ==========
        string sTransCode = ddlbTransCode.Text.Trim();
        if (sTransCode == "")
        {
            lblErrMessage.Text = "������д������";
            lblErrMessage.Visible = true;
            ddlbTransCode.Focus();
            return;
        }

        //========== 1.3 �жϷ����ַ�Ƿ�Ϊ�� ==========
        string sUrlAddressUid = ddlbUrlAddressUid.Text.Trim();
        if (sUrlAddressUid == "")
        {
            lblErrMessage.Text = "������д�����ַ";
            lblErrMessage.Visible = true;
            ddlbUrlAddressUid.Focus();
            return;
        }

        //========== 2. ������������ ==========
        TbMapTransCodeUrl tbl = new TbMapTransCodeUrl();
        if (bAppendMode)
        {
            tbl.MapUid = Guid.NewGuid().ToString();
            tbl.ServiceId = ddlbServiceId.SelectedValue;
            tbl.TransCode = ddlbTransCode.SelectedValue;
            tbl.UrlAddressUid = ddlbUrlAddressUid.SelectedValue;
            tbl.FromClientId = edtFromClientId.Text.Trim();
            tbl.FromClientVersion = edtFromClientVersion.Text.Trim();
            tbl.FromSystem = edtFromSystem.Text.Trim();
            tbl.CreateTime = DateTimeUtil.Now;
            tbl.ModifyTime = DateTimeUtil.Now;

            tbl.Insert();
        }

        //========== 3. ���ݸ��´��� ==========
        else
        {
            tbl.MapUid = sParamRecKey;
            tbl.Fetch();

            tbl.ServiceId = ddlbServiceId.SelectedValue;
            tbl.TransCode = ddlbTransCode.SelectedValue;
            tbl.UrlAddressUid = ddlbUrlAddressUid.SelectedValue;
            tbl.FromClientId = edtFromClientId.Text.Trim();
            tbl.FromClientVersion = edtFromClientVersion.Text.Trim();
            tbl.FromSystem = edtFromSystem.Text.Trim();
            tbl.ModifyTime = DateTimeUtil.Now;

            tbl.Update();
        }

        //========== 4. ���ص���ҳ�� ==========
        Response.Redirect("map_trans_code_url_list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("map_trans_code_url_list.aspx");
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbMapTransCodeUrl tbl = new TbMapTransCodeUrl();
            tbl.MapUid = sSelectedID;
            tbl.Delete();
        }
    }
}

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
        //========== 0. 删除记录 ==========
        string sDelRecords = ConvertUtil.ToString(Request["del_rec"]);
        if (sDelRecords != "")
        {
            DeleteData(sDelRecords);
            Response.Redirect("map_trans_code_url_list.aspx");
            return;
        }

        //========== 1. 初始化界面 ==========
        WCUComboBox.InitComboBox(ddlbServiceId, QDBDnsPools.PoolService.ServiceCodeTable);
        ddlbServiceId.Items.Add(new ListItem("[未指定]", "NONE"));
        ddlbServiceId.SelectedValue = "NONE";

        WCUComboBox.InitComboBox(ddlbTransCode, QDBDnsPools.PoolTransCode.TransCodeCodeTable);

        WCUComboBox.InitComboBox(ddlbUrlAddressUid, QDBDnsPools.PoolUrlAddress.UrlAddressCodeTable);

        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. 新增的判断和处理 ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "新增映射配置";
            btnOK.Text = "新增";
            return;
        }

        //========== 4. 取数据 ==========
        TbMapTransCodeUrl tbl = new TbMapTransCodeUrl();
        tbl.MapUid = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. 更新各控件的显示 ==========
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

        //========== 1. 验证数据的有效性 ==========
        //========== 1.1 判断寻址服务是否为空 ==========
        string sServiceId = ddlbServiceId.Text.Trim();
        if (sServiceId == "")
        {
            lblErrMessage.Text = "必须填写寻址服务";
            lblErrMessage.Visible = true;
            ddlbServiceId.Focus();
            return;
        }

        //========== 1.2 判断交易码是否为空 ==========
        string sTransCode = ddlbTransCode.Text.Trim();
        if (sTransCode == "")
        {
            lblErrMessage.Text = "必须填写交易码";
            lblErrMessage.Visible = true;
            ddlbTransCode.Focus();
            return;
        }

        //========== 1.3 判断服务地址是否为空 ==========
        string sUrlAddressUid = ddlbUrlAddressUid.Text.Trim();
        if (sUrlAddressUid == "")
        {
            lblErrMessage.Text = "必须填写服务地址";
            lblErrMessage.Visible = true;
            ddlbUrlAddressUid.Focus();
            return;
        }

        //========== 2. 数据新增处理 ==========
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

        //========== 3. 数据更新处理 ==========
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

        //========== 4. 返回到主页面 ==========
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

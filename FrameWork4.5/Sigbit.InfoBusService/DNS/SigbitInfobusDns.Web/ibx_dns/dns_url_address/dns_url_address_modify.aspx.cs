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
        //========== 0. 删除记录 ==========
        string sDelRecords = ConvertUtil.ToString(Request["del_rec"]);
        if (sDelRecords != "")
        {
            DeleteData(sDelRecords);
            Response.Redirect("dns_url_address_list.aspx");
            return;
        }

        //========== 1. 初始化界面 ==========
        WCUComboBox.InitComboBox(ddlbServiceId, QDBDnsPools.PoolService.ServiceCodeTable);
        ddlbServiceId.Items.Add(new ListItem("[未指定]", "NONE"));
        ddlbServiceId.SelectedValue = "NONE";

        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. 新增的判断和处理 ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "新增服务地址";
            btnOK.Text = "新增";
            return;
        }

        //========== 4. 取数据 ==========
        TbSysUrlAddress tbl = new TbSysUrlAddress();
        tbl.UrlAddressUid = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. 更新各控件的显示 ==========
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

        //========== 1. 验证数据的有效性 ==========
        //========== 1.1 判断地址名称是否为空 ==========
        string sUrlAddressName = edtUrlAddressName.Text.Trim();
        if (sUrlAddressName == "")
        {
            lblErrMessage.Text = "必须填写地址名称";
            lblErrMessage.Visible = true;
            edtUrlAddressName.Focus();
            return;
        }

        //========== 1.2 判断url地址是否为空 ==========
        string sUrlAddressLink = edtUrlAddressLink.Text.Trim();
        if (sUrlAddressLink == "")
        {
            lblErrMessage.Text = "必须填写url地址";
            lblErrMessage.Visible = true;
            edtUrlAddressLink.Focus();
            return;
        }

        //========== 1.3 判断service是否已选择 =========
        string sServiceId = ddlbServiceId.SelectedValue;
        if (sServiceId == "NONE")
        {
            lblErrMessage.Text = "请选择寻址服务";
            lblErrMessage.Visible = true;
            ddlbServiceId.Focus();
            return;
        }

        //========== 2. 数据新增处理 ==========
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

        //========== 3. 数据更新处理 ==========
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

        //========== 4. 返回到主页面 ==========
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

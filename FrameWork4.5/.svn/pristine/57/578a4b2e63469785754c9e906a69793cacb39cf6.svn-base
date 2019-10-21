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

public partial class genui_LNWX_dns_trans_code_modify : SbtPageBase
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
            Response.Redirect("dns_trans_code_list.aspx");
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
            lblTransCode.Visible = false;

            lblTitle.Text = "新增服务交易码";
            btnOK.Text = "新增";
            return;
        }

        edtTransCode.Visible = false;

        //========== 4. 取数据 ==========
        TbSysTransCode tbl = new TbSysTransCode();
        tbl.TransCode = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. 更新各控件的显示 ==========
        lblTransCode.Text = tbl.TransCode;
        edtTransCodeName.Text = tbl.TransCodeName;
        ddlbServiceId.SelectedValue = tbl.ServiceId;
        edtTransCodeDesc.Text = tbl.TransCodeDesc;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool bAppendMode = false;
        string sParamRecKey = ConvertUtil.ToString(ViewState["rec_key"]);
        if (sParamRecKey == "")
            bAppendMode = true;

        //========== 1. 验证数据的有效性 ==========
        //========== 1.1 判断交易码是否为空 ==========
        string sTransCode = edtTransCode.Text.Trim();
        if (bAppendMode && sTransCode == "")
        {
            lblErrMessage.Text = "必须填写交易码";
            lblErrMessage.Visible = true;
            edtTransCode.Focus();
            return;
        }

        //========== 1.2 判断名称是否为空 ==========
        string sTransCodeName = edtTransCodeName.Text.Trim();
        if (sTransCodeName == "")
        {
            lblErrMessage.Text = "必须填写名称";
            lblErrMessage.Visible = true;
            edtTransCodeName.Focus();
            return;
        }

        //========== 1.3 判断寻址服务是否为空 ==========
        string sServiceId = ddlbServiceId.SelectedValue;
        if (sServiceId == "NONE")
        {
            lblErrMessage.Text = "必须选择寻址服务";
            lblErrMessage.Visible = true;
            ddlbServiceId.Focus();
            return;
        }

        //========== 2. 数据新增处理 ==========
        TbSysTransCode tbl = new TbSysTransCode();
        if (bAppendMode)
        {
            tbl.TransCode = edtTransCode.Text.Trim();
            tbl.TransCodeName = edtTransCodeName.Text.Trim();
            tbl.ServiceId = ddlbServiceId.SelectedValue;
            tbl.TransCodeDesc = edtTransCodeDesc.Text.Trim();
            tbl.CreateTime = DateTimeUtil.Now;
            tbl.Creator = CurrentUser.UserName;
            tbl.ModifyTime = DateTimeUtil.Now;

            tbl.Insert();
        }

        //========== 3. 数据更新处理 ==========
        else
        {
            tbl.TransCode = sParamRecKey;
            tbl.Fetch();

            tbl.TransCodeName = edtTransCodeName.Text.Trim();
            tbl.ServiceId = ddlbServiceId.SelectedValue;
            tbl.TransCodeDesc = edtTransCodeDesc.Text.Trim();
            tbl.ModifyTime = DateTimeUtil.Now;

            tbl.Update();
        }

        //========== 4. 返回到主页面 ==========
        Response.Redirect("dns_trans_code_list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("dns_trans_code_list.aspx");
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbSysTransCode tbl = new TbSysTransCode();
            tbl.TransCode = sSelectedID;
            tbl.Delete();
        }
    }
}

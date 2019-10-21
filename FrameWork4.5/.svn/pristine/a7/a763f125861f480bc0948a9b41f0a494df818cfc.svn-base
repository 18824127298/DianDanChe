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
        //========== 0. 删除记录 ==========
        string sDelRecords = ConvertUtil.ToString(Request["del_rec"]);
        if (sDelRecords != "")
        {
            DeleteData(sDelRecords);
            Response.Redirect("dns_service_list.aspx");
            return;
        }

        //========== 1. 初始化界面 ==========
        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. 新增的判断和处理 ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "新增服务";
            btnOK.Text = "新增";

            lblServiceID.Visible = false;

            return;
        }

        edtServiceId.Visible = false;

        //========== 4. 取数据 ==========
        TbSysService tbl = new TbSysService();
        tbl.ServiceId = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. 更新各控件的显示 ==========
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

        //========== 1. 验证数据的有效性 ==========
        //========== 1.1 判断服务标识是否为空 ==========
        string sServiceId = edtServiceId.Text.Trim();
        if (bAppendMode && sServiceId == "")
        {
            lblErrMessage.Text = "必须填写服务标识";
            lblErrMessage.Visible = true;
            edtServiceId.Focus();
            return;
        }

        //========== 1.2 判断名称是否为空 ==========
        string sServiceName = edtServiceName.Text.Trim();
        if (sServiceName == "")
        {
            lblErrMessage.Text = "必须填写名称";
            lblErrMessage.Visible = true;
            edtServiceName.Focus();
            return;
        }

        //========== 2. 数据新增处理 ==========
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

        //========== 3. 数据更新处理 ==========
        else
        {
            tbl.ServiceId = sParamRecKey;
            tbl.Fetch();

            tbl.ServiceName = edtServiceName.Text.Trim();
            tbl.ServiceDesc = edtServiceDesc.Text.Trim();
            tbl.ModifyTime = DateTimeUtil.Now;

            tbl.Update();
        }

        //========== 4. 返回到主页面 ==========
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

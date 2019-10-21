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
        //========== 0. 删除记录 ==========
        string sDelRecords = ConvertUtil.ToString(Request["del_rec"]);
        if (sDelRecords != "")
        {
            DeleteData(sDelRecords);
            Response.Redirect("vcode_scene_list.aspx");
            return;
        }

        //========== 1. 初始化界面 ==========
        WCUComboBox.InitComboBox(ddlbAlgolId, QDBVCBreakPools.PoolAlgol.CodeTableOfAll);
        ddlbAlgolId.Items.Add(new ListItem("[未指定]", "NONE"));
        ddlbAlgolId.SelectedValue = "NONE";

        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. 新增的判断和处理 ==========
        if (sRecordPrimaryKey == "")
        {
            lblVcodeId.Visible = false;

            lblTitle.Text = "新增验证码场景";
            btnOK.Text = "新增";
            return;
        }

        //========== 4. 取数据 ==========
        TbSysVcode tbl = new TbSysVcode();
        tbl.VcodeId = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. 更新各控件的显示 ==========
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

        //========== 1. 验证数据的有效性 ==========
        //========== 1.1 判断验证码标识是否为空 ==========
        string sVcodeId = edtVcodeId.Text.Trim();
        if (sVcodeId == "")
        {
            lblErrMessage.Text = "必须填写验证码标识";
            lblErrMessage.Visible = true;
            edtVcodeId.Focus();
            return;
        }

        //========== 1.2 判断验证码名称是否为空 ==========
        string sVcodeName = edtVcodeName.Text.Trim();
        if (sVcodeName == "")
        {
            lblErrMessage.Text = "必须填写验证码名称";
            lblErrMessage.Visible = true;
            edtVcodeName.Focus();
            return;
        }

        //========== 1.3 判断算法是否为空 ==========
        string sAlgolId = ddlbAlgolId.SelectedValue;
        if (sAlgolId == "NONE")
        {
            lblErrMessage.Text = "必须选择算法";
            lblErrMessage.Visible = true;
            ddlbAlgolId.Focus();
            return;
        }

        //========== 2. 数据新增处理 ==========
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

        //========== 3. 数据更新处理 ==========
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

        //========== 4. 返回到主页面 ==========
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

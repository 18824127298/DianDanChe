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
        //========== 0. 删除记录 ==========
        string sDelRecords = ConvertUtil.ToString(Request["del_rec"]);
        if (sDelRecords != "")
        {
            DeleteData(sDelRecords);
            Response.Redirect("break_algol_list.aspx");
            return;
        }

        //========== 1. 初始化界面 ==========
        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. 新增的判断和处理 ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "新增验证码破解算法";
            btnOK.Text = "新增";
            lblAlgolId.Visible = false;
            return;
        }

        edtAlgolId.Visible = false;

        //========== 4. 取数据 ==========
        TbSysBreakAlgol tbl = new TbSysBreakAlgol();
        tbl.AlgolId = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. 更新各控件的显示 ==========
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

        //========== 1. 验证数据的有效性 ==========
        //========== 1.1 判断算法标识是否为空 ==========
        string sAlgolId = edtAlgolId.Text.Trim();
        if (sAlgolId == "")
        {
            lblErrMessage.Text = "必须填写算法标识";
            lblErrMessage.Visible = true;
            edtAlgolId.Focus();
            return;
        }

        //========== 1.2 判断算法名称是否为空 ==========
        string sAlgolName = edtAlgolName.Text.Trim();
        if (sAlgolName == "")
        {
            lblErrMessage.Text = "必须填写算法名称";
            lblErrMessage.Visible = true;
            edtAlgolName.Focus();
            return;
        }

        //========== 2. 数据新增处理 ==========
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

        //========== 3. 数据更新处理 ==========
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

        //========== 4. 返回到主页面 ==========
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

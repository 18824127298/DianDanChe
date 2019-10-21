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

public partial class genui_EEQY_authen_user_modify : SbtPageBase
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
            Response.Redirect("authen_user_list.aspx");
            return;
        }

        //========== 1. 初始化界面 ==========
        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. 新增的判断和处理 ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "新增授权用户";
            btnOK.Text = "新增";
            return;
        }

        //========== 4. 取数据 ==========
        TbAuthenUser tbl = new TbAuthenUser();
        tbl.AuthenUserUid = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. 更新各控件的显示 ==========
        edtAuthenUserName.Text = tbl.AuthenUserName;
        edtAuthenPassword.Text = tbl.AuthenPassword;
        if (tbl.LimitPerMinute != 0)
            edtLimitPerMinute.Text = tbl.LimitPerMinute.ToString();
        
        if (tbl.LimitPerHour != 0)
            edtLimitPerHour.Text = tbl.LimitPerHour.ToString();
        
        if (tbl.LimitPerDay != 0)
            edtLimitPerDay.Text = tbl.LimitPerDay.ToString();
        edtRemarks.Text = tbl.Remarks;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool bAppendMode = false;
        string sParamRecKey = ConvertUtil.ToString(ViewState["rec_key"]);
        if (sParamRecKey == "")
            bAppendMode = true;

        //========== 1. 验证数据的有效性 ==========
        //========== 1.1 判断用户名是否为空 ==========
        string sAuthenUserName = edtAuthenUserName.Text.Trim();
        if (sAuthenUserName == "")
        {
            lblErrMessage.Text = "必须填写用户名";
            lblErrMessage.Visible = true;
            edtAuthenUserName.Focus();
            return;
        }

        //========== 2. 数据新增处理 ==========
        TbAuthenUser tbl = new TbAuthenUser();
        if (bAppendMode)
        {
            tbl.AuthenUserUid = Guid.NewGuid().ToString();
            tbl.AuthenUserName = edtAuthenUserName.Text.Trim();
            tbl.AuthenPassword = edtAuthenPassword.Text.Trim();
            tbl.LimitPerMinute = ConvertUtil.ToInt(edtLimitPerMinute.Text.Trim());
            tbl.LimitPerHour = ConvertUtil.ToInt(edtLimitPerHour.Text.Trim());
            tbl.LimitPerDay = ConvertUtil.ToInt(edtLimitPerDay.Text.Trim());
            tbl.CreateTime = DateTimeUtil.Now;
            tbl.ModifyTime = DateTimeUtil.Now;
            tbl.Remarks = edtRemarks.Text.Trim();

            tbl.Insert();
        }

        //========== 3. 数据更新处理 ==========
        else
        {
            tbl.AuthenUserUid = sParamRecKey;
            tbl.Fetch();

            tbl.AuthenUserName = edtAuthenUserName.Text.Trim();
            tbl.AuthenPassword = edtAuthenPassword.Text.Trim();
            tbl.LimitPerMinute = ConvertUtil.ToInt(edtLimitPerMinute.Text.Trim());
            tbl.LimitPerHour = ConvertUtil.ToInt(edtLimitPerHour.Text.Trim());
            tbl.LimitPerDay = ConvertUtil.ToInt(edtLimitPerDay.Text.Trim());
            tbl.ModifyTime = DateTimeUtil.Now;
            tbl.Remarks = edtRemarks.Text.Trim();

            tbl.Update();
        }

        //========== 4. 返回到主页面 ==========
        Response.Redirect("authen_user_list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("authen_user_list.aspx");
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbAuthenUser tbl = new TbAuthenUser();
            tbl.AuthenUserUid = sSelectedID;
            tbl.Delete();
        }
    }
}

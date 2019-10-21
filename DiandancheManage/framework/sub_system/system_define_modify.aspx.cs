using System;
using System.Data;
using System.IO;
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
using Sigbit.Framework.SubSystem.DBDefine;
using Sigbit.Web.WebControlUtil;
using Sigbit.Framework.SubSystem;

public partial class genui_WSQV_system_define_modify : SbtPageBase
{
    protected string GetHomepageGraphUrlRootPath()
    {
        return "~/framework/admin/images/homepage/";
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        //========== 0. 删除记录 ==========
        string sDelRecords = ConvertUtil.ToString(Request["del_rec"]);
        if (sDelRecords != "")
        {
            DeleteData(sDelRecords);
            SUSSubSystem.ResetSubSystem();
            Response.Redirect("system_define_list.aspx");
            return;
        }

        //========== 1. 初始化界面 ==========

        LoadHomepageGraphList();

        //========== 2. 取出传入条件 ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. 新增的判断和处理 ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "新增系统定义";
            btnOK.Text = "新增";

            this.NaviTabController.AppendSelfToBar();
            return;
        }

        //========== 4. 取数据 ==========
        TbSysSubSystemDefine tbl = new TbSysSubSystemDefine();
        tbl.SubSystemId = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. 更新各控件的显示 ==========
        //lblSubSystemId.Text = tbl.SubSystemId;
        edtSubSystemId.Text = tbl.SubSystemId;
        edtSubSystemName.Text = tbl.SubSystemName;
        //edtSubSystemColor.Text = tbl.SubSystemColor;
        //lblSystemColor.BackColor = System.Drawing.Color.FromName(tbl.SubSystemColor);
        //lblSystemColor.Font.Bold = true;

        tdHomeGraph.BgColor = tbl.SubSystemColor;

        hfSystemColor.Value = tbl.SubSystemColor;

        edtFullName.Text = tbl.FullName;
        edtAppTheme.Text = tbl.AppTheme;

        imgHomepageGraph.ImageUrl = GetHomepageGraphUrlRootPath() + tbl.HomepageGraph;
        ddlbHomepageGraph.SelectedValue = tbl.HomepageGraph;

        edtHomepageCaption.Text = tbl.HomepageCaption;
        edtPageTitleText.Text = tbl.PageTitleText;
        edtPageTitleImage.Text = tbl.PageTitleImage;
        edtMenuRootText.Text = tbl.MenuRootText;
        edtDisplayOrder.Text = tbl.DisplayOrder.ToString();
        edtRemarks.Text = tbl.Remarks;

        this.NaviTabController.AppendSelfToBar();
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool bAppendMode = false;
        string sParamRecKey = ConvertUtil.ToString(ViewState["rec_key"]);
        if (sParamRecKey == "")
            bAppendMode = true;

        //========== 1. 验证数据的有效性 ==========
        //========== 1.1 判断系统编号是否为空 ==========
        string sSubSystemId = edtSubSystemId.Text.Trim();
        if (sSubSystemId == "" && edtSubSystemId.Visible == true)
        {
            lblErrMessage.Text = "必须填写系统编号";
            lblErrMessage.Visible = true;
            edtSubSystemId.Focus();
            return;
        }

        //========== 1.2 判断系统名字是否为空 ==========
        string sSubSystemName = edtSubSystemName.Text.Trim();
        if (sSubSystemName == "")
        {
            lblErrMessage.Text = "必须填写系统名字";
            lblErrMessage.Visible = true;
            edtSubSystemName.Focus();
            return;
        }

        //========== 1.3 判断系统颜色是否为空 ==========
        //string sSubSystemColor = edtSubSystemColor.Text.Trim();
        //if (sSubSystemColor == "")
        //{
        //    lblErrMessage.Text = "必须填写系统颜色";
        //    lblErrMessage.Visible = true;
        //    edtSubSystemColor.Focus();
        //    return;
        //}

        //========== 1.4 判断完整名字是否为空 ==========
        string sFullName = edtFullName.Text.Trim();
        //if (sFullName == "")
        //{
        //    lblErrMessage.Text = "必须填写完整名字";
        //    lblErrMessage.Visible = true;
        //    edtFullName.Focus();
        //    return;
        //}

        //========== 1.5 判断应用主题是否为空 ==========
        string sAppTheme = edtAppTheme.Text.Trim();
        //if (sAppTheme == "")
        //{
        //    lblErrMessage.Text = "必须填写应用主题";
        //    lblErrMessage.Visible = true;
        //    edtAppTheme.Focus();
        //    return;
        //}

      

        //========== 1.7 判断首页标题是否为空 ==========
        string sHomepageCaption = edtHomepageCaption.Text.Trim();
        //if (sHomepageCaption == "")
        //{
        //    lblErrMessage.Text = "必须填写首页标题";
        //    lblErrMessage.Visible = true;
        //    edtHomepageCaption.Focus();
        //    return;
        //}

        //========== 1.8 判断页面标题文本是否为空 ==========
        string sPageTitleText = edtPageTitleText.Text.Trim();
        //if (sPageTitleText == "")
        //{
        //    lblErrMessage.Text = "必须填写页面标题文本";
        //    lblErrMessage.Visible = true;
        //    edtPageTitleText.Focus();
        //    return;
        //}

        //========== 1.9 判断页面标题图片是否为空 ==========
        string sPageTitleImage = edtPageTitleImage.Text.Trim();
        //if (sPageTitleImage == "")
        //{
        //    lblErrMessage.Text = "必须填写页面标题图片";
        //    lblErrMessage.Visible = true;
        //    edtPageTitleImage.Focus();
        //    return;
        //}

        //========== 1.10 判断文本菜单根是否为空 ==========
        string sMenuRootText = edtMenuRootText.Text.Trim();
        //if (sMenuRootText == "")
        //{
        //    lblErrMessage.Text = "必须填写文本菜单根";
        //    lblErrMessage.Visible = true;
        //    edtMenuRootText.Focus();
        //    return;
        //}

        //========== 1.11 判断显示次序是否为空 ==========
        int nDisplayOrder = ConvertUtil.ToInt(edtDisplayOrder.Text.Trim());
        //if (nDisplayOrder < 0 )
        //{
        //    lblErrMessage.Text = "必须填写显示次序";
        //    lblErrMessage.Visible = true;
        //    edtDisplayOrder.Focus();
        //    return;
        //}


        if (!PKCheck(sParamRecKey, sSubSystemId))
        {
            lblErrMessage.Text = "系统编号" + sSubSystemId + "已被使用！";
            lblErrMessage.Visible = true;
            edtSubSystemId.Focus();
            return;
        }


        //========== 2. 数据新增处理 ==========
        TbSysSubSystemDefine tbl = new TbSysSubSystemDefine();
        if (bAppendMode)
        {
            tbl.SubSystemId = edtSubSystemId.Text.Trim();
            tbl.SubSystemName = edtSubSystemName.Text.Trim();
            //tbl.SubSystemColor = edtSubSystemColor.Text.Trim();
            tbl.SubSystemColor = hfSystemColor.Value;
            tbl.FullName = edtFullName.Text.Trim();
            tbl.AppTheme = edtAppTheme.Text.Trim();
            tbl.HomepageGraph = ddlbHomepageGraph.SelectedValue;
            tbl.HomepageCaption = edtHomepageCaption.Text.Trim();
            tbl.PageTitleText = edtPageTitleText.Text.Trim();
            tbl.PageTitleImage = edtPageTitleImage.Text.Trim();
            tbl.MenuRootText = edtMenuRootText.Text.Trim();
            tbl.DisplayOrder = ConvertUtil.ToInt(edtDisplayOrder.Text.Trim(), 100);
            tbl.CreateTime = DateTimeUtil.Now;
            tbl.Creator = CurrentUser.UserName;
            tbl.ModifyTime = DateTimeUtil.Now;
            tbl.Remarks = edtRemarks.Text.Trim();

            tbl.Insert();
        }

        //========== 3. 数据更新处理 ==========
        else
        {
            tbl.SubSystemId = sParamRecKey;
            tbl.Fetch();

            tbl.NewSubSystemId_ForUpdate = edtSubSystemId.Text.Trim();
            tbl.SubSystemName = edtSubSystemName.Text.Trim();
            //tbl.SubSystemColor = edtSubSystemColor.Text.Trim();
            tbl.SubSystemColor = hfSystemColor.Value;
            tbl.FullName = edtFullName.Text.Trim();
            tbl.AppTheme = edtAppTheme.Text.Trim();
            tbl.HomepageGraph = ddlbHomepageGraph.SelectedValue;
            tbl.HomepageCaption = edtHomepageCaption.Text.Trim();
            tbl.PageTitleText = edtPageTitleText.Text.Trim();
            tbl.PageTitleImage = edtPageTitleImage.Text.Trim();
            tbl.MenuRootText = edtMenuRootText.Text.Trim();
            tbl.DisplayOrder = ConvertUtil.ToInt(edtDisplayOrder.Text.Trim());
            tbl.ModifyTime = DateTimeUtil.Now;
            tbl.Remarks = edtRemarks.Text.Trim();

            tbl.Update();
        }

        SUSSubSystem.ResetSubSystem();


        NaviTabController.RemoveSelfFromBar();

        //========== 4. 返回到主页面 ==========
        Response.Redirect("system_define_list.aspx");
    }


    protected void LoadHomepageGraphList()
    {
        ddlbHomepageGraph.Items.Clear();

        string sRootPath = MapPath(GetHomepageGraphUrlRootPath());

        DirectoryInfo dirRoot = new DirectoryInfo(sRootPath);

        FileInfo[] arrFiles = dirRoot.GetFiles();

        for (int i = 0; i < arrFiles.Length; i++)
        {
            FileInfo file = arrFiles[i];

            ddlbHomepageGraph.Items.Add(new ListItem(file.Name, file.Name));
        }
    }


    protected bool PKCheck(string sRawPK, string sNewPK)
    {
        if (sRawPK == sNewPK)
            return true;

        TbSysSubSystemDefine tblSubSystem = new TbSysSubSystemDefine();
        tblSubSystem.SubSystemId = sNewPK;

        if (tblSubSystem.Fetch(true))
        {
            return false;
        }
        return true;
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("system_define_list.aspx");
        this.NaviTabController.RemoveSelfFromBar();
    }

    private void DeleteData(string sDelRecords)
    {
        string[] arrsSelectedIDs = sDelRecords.Split(',');
        for (int i = 0; i < arrsSelectedIDs.Length; i++)
        {
            string sSelectedID = arrsSelectedIDs[i];

            TbSysSubSystemDefine tbl = new TbSysSubSystemDefine();
            tbl.SubSystemId = sSelectedID;
            tbl.Delete();
        }
    }


    protected void ddlbHomepageGraph_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lblSystemColor.BackColor = System.Drawing.Color.FromName( hfSystemColor.Value);
        //lblSystemColor.Font.Bold = true;
        tdHomeGraph.BgColor = hfSystemColor.Value;
        imgHomepageGraph.ImageUrl = GetHomepageGraphUrlRootPath() + ddlbHomepageGraph.SelectedValue;
    }
}

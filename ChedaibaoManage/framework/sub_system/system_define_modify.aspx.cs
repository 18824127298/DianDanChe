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
        //========== 0. ɾ����¼ ==========
        string sDelRecords = ConvertUtil.ToString(Request["del_rec"]);
        if (sDelRecords != "")
        {
            DeleteData(sDelRecords);
            SUSSubSystem.ResetSubSystem();
            Response.Redirect("system_define_list.aspx");
            return;
        }

        //========== 1. ��ʼ������ ==========

        LoadHomepageGraphList();

        //========== 2. ȡ���������� ==========
        string sRecordPrimaryKey = ConvertUtil.ToString(Request["rec_key"]);
        ViewState["rec_key"] = sRecordPrimaryKey;

        //========== 3. �������жϺʹ��� ==========
        if (sRecordPrimaryKey == "")
        {
            lblTitle.Text = "����ϵͳ����";
            btnOK.Text = "����";

            this.NaviTabController.AppendSelfToBar();
            return;
        }

        //========== 4. ȡ���� ==========
        TbSysSubSystemDefine tbl = new TbSysSubSystemDefine();
        tbl.SubSystemId = sRecordPrimaryKey;
        tbl.Fetch();

        //========== 5. ���¸��ؼ�����ʾ ==========
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

        //========== 1. ��֤���ݵ���Ч�� ==========
        //========== 1.1 �ж�ϵͳ����Ƿ�Ϊ�� ==========
        string sSubSystemId = edtSubSystemId.Text.Trim();
        if (sSubSystemId == "" && edtSubSystemId.Visible == true)
        {
            lblErrMessage.Text = "������дϵͳ���";
            lblErrMessage.Visible = true;
            edtSubSystemId.Focus();
            return;
        }

        //========== 1.2 �ж�ϵͳ�����Ƿ�Ϊ�� ==========
        string sSubSystemName = edtSubSystemName.Text.Trim();
        if (sSubSystemName == "")
        {
            lblErrMessage.Text = "������дϵͳ����";
            lblErrMessage.Visible = true;
            edtSubSystemName.Focus();
            return;
        }

        //========== 1.3 �ж�ϵͳ��ɫ�Ƿ�Ϊ�� ==========
        //string sSubSystemColor = edtSubSystemColor.Text.Trim();
        //if (sSubSystemColor == "")
        //{
        //    lblErrMessage.Text = "������дϵͳ��ɫ";
        //    lblErrMessage.Visible = true;
        //    edtSubSystemColor.Focus();
        //    return;
        //}

        //========== 1.4 �ж����������Ƿ�Ϊ�� ==========
        string sFullName = edtFullName.Text.Trim();
        //if (sFullName == "")
        //{
        //    lblErrMessage.Text = "������д��������";
        //    lblErrMessage.Visible = true;
        //    edtFullName.Focus();
        //    return;
        //}

        //========== 1.5 �ж�Ӧ�������Ƿ�Ϊ�� ==========
        string sAppTheme = edtAppTheme.Text.Trim();
        //if (sAppTheme == "")
        //{
        //    lblErrMessage.Text = "������дӦ������";
        //    lblErrMessage.Visible = true;
        //    edtAppTheme.Focus();
        //    return;
        //}

      

        //========== 1.7 �ж���ҳ�����Ƿ�Ϊ�� ==========
        string sHomepageCaption = edtHomepageCaption.Text.Trim();
        //if (sHomepageCaption == "")
        //{
        //    lblErrMessage.Text = "������д��ҳ����";
        //    lblErrMessage.Visible = true;
        //    edtHomepageCaption.Focus();
        //    return;
        //}

        //========== 1.8 �ж�ҳ������ı��Ƿ�Ϊ�� ==========
        string sPageTitleText = edtPageTitleText.Text.Trim();
        //if (sPageTitleText == "")
        //{
        //    lblErrMessage.Text = "������дҳ������ı�";
        //    lblErrMessage.Visible = true;
        //    edtPageTitleText.Focus();
        //    return;
        //}

        //========== 1.9 �ж�ҳ�����ͼƬ�Ƿ�Ϊ�� ==========
        string sPageTitleImage = edtPageTitleImage.Text.Trim();
        //if (sPageTitleImage == "")
        //{
        //    lblErrMessage.Text = "������дҳ�����ͼƬ";
        //    lblErrMessage.Visible = true;
        //    edtPageTitleImage.Focus();
        //    return;
        //}

        //========== 1.10 �ж��ı��˵����Ƿ�Ϊ�� ==========
        string sMenuRootText = edtMenuRootText.Text.Trim();
        //if (sMenuRootText == "")
        //{
        //    lblErrMessage.Text = "������д�ı��˵���";
        //    lblErrMessage.Visible = true;
        //    edtMenuRootText.Focus();
        //    return;
        //}

        //========== 1.11 �ж���ʾ�����Ƿ�Ϊ�� ==========
        int nDisplayOrder = ConvertUtil.ToInt(edtDisplayOrder.Text.Trim());
        //if (nDisplayOrder < 0 )
        //{
        //    lblErrMessage.Text = "������д��ʾ����";
        //    lblErrMessage.Visible = true;
        //    edtDisplayOrder.Focus();
        //    return;
        //}


        if (!PKCheck(sParamRecKey, sSubSystemId))
        {
            lblErrMessage.Text = "ϵͳ���" + sSubSystemId + "�ѱ�ʹ�ã�";
            lblErrMessage.Visible = true;
            edtSubSystemId.Focus();
            return;
        }


        //========== 2. ������������ ==========
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

        //========== 3. ���ݸ��´��� ==========
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

        //========== 4. ���ص���ҳ�� ==========
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

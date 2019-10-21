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
using Sigbit.Framework;
using Sigbit.Data;

using Sigbit.Web.WebControlUtil;
using Sigbit.Framework.SubSystem;
using Sigbit.Framework.SupervisorTool.MenuManage;

public partial class framework_menu_navigate_menu_edit : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }

        string sMenuCode = ConvertUtil.ToString(Request["mnu_uid"]);
        if (sMenuCode == "")
        {
            return;
        }


        ViewState["MenuCode"] = sMenuCode;

        TbSysMenu tlbSysMenu = new TbSysMenu();
        tlbSysMenu.MenuCode = sMenuCode;
        tlbSysMenu.Fetch();

        lblMenuName.Text = tlbSysMenu.MenuName;
        edtMenuCode.Text = tlbSysMenu.MenuCode;
        edtMenuName.Text = tlbSysMenu.MenuName;
        imgMenuIcon.ImageUrl = "~/images/menu_icon/" + tlbSysMenu.MenuIcon;
        edtMenuIcon.Text = tlbSysMenu.MenuIcon;
        edtListOrder.Text = tlbSysMenu.ListOrder.ToString();
        edtMenuLink.Text = tlbSysMenu.MenuLink;

        ckbIsActive.Checked = ConvertStringToBool(tlbSysMenu.IsActive);
        ckbIsMenuItem.Checked = ConvertStringToBool(tlbSysMenu.IsMenuItem);
        ckbIsLogItem.Checked = ConvertStringToBool(tlbSysMenu.IsLogItem);
        ckbIsRightItem.Checked = ConvertStringToBool(tlbSysMenu.IsRightItem);

        //============= 初始化子系统的ddlb ============
        WCUComboBox.InitComboBox(ddlbSubSystem, SUSSubSystem.CodeTableOfSubSystem);
        ddlbSubSystem.SelectedValue = tlbSysMenu.MenuClass;
    }

    private bool ConvertStringToBool(string sValue)
    {
        if (sValue == "Y")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private string ConvertBoolToString(bool bValue)
    {
        if (bValue)
        {
            return "Y";
        }
        else
        {
            return "N";
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string sMenuCode = ViewState["MenuCode"].ToString();
        string sNewMenuCode = edtMenuCode.Text.Trim();
        bool bUpdated = false;
        if (sMenuCode != sNewMenuCode)
        {
            bUpdated = true;
        }

        TbSysMenu tlbSysMenu = new TbSysMenu();
        tlbSysMenu.MenuCode = sMenuCode;
        tlbSysMenu.Fetch();

        tlbSysMenu.MenuName = edtMenuName.Text.Trim();  //菜单名称
        tlbSysMenu.MenuIcon = edtMenuIcon.Text.Trim();                    
        tlbSysMenu.MenuLink = edtMenuLink.Text.Trim();
        tlbSysMenu.ListOrder = ConvertUtil.ToInt(edtListOrder.Text.Trim());
        tlbSysMenu.IsActive = ConvertBoolToString(ckbIsActive.Checked);
        tlbSysMenu.IsMenuItem = ConvertBoolToString(ckbIsMenuItem.Checked);
        tlbSysMenu.IsLogItem = ConvertBoolToString(ckbIsLogItem.Checked);
        tlbSysMenu.IsRightItem = ConvertBoolToString(ckbIsRightItem.Checked);

        if (bUpdated)
        {
            MBZMenuCodeBatchChange mbzMenuBatch = new MBZMenuCodeBatchChange();
            mbzMenuBatch.OldMenuCode = sMenuCode;
            mbzMenuBatch.NewMenuCode = sNewMenuCode;

            string sErrorMsg = "";
            if (!mbzMenuBatch.DoBatchChange(out sErrorMsg))
            {
                lblErrMessage.Text = sErrorMsg;
                lblErrMessage.Visible = true;
                return;
            }
            //tlbSysMenu.Delete();
            //tlbSysMenu.MenuCode = sNewMenuCode;
            //tlbSysMenu.Insert();
        }
        else
        {
            tlbSysMenu.Update();

            //============= 2+. 变更子系统 ==============
            if (tlbSysMenu.MenuClass != ddlbSubSystem.SelectedValue)
                SUSMenuChangeSubSystemUtil.DoChange(sMenuCode, ddlbSubSystem.SelectedValue);
        }

        //========= 3. 刷新显示 =============
        TbSysMenu__Lib.Reset();

        PageParameter.SetCustomParamString("CurrentParentNode", tlbSysMenu.ParentMenuCode);

        PageParameter.StringParam[0] = "菜单已保存修改成功";
        Response.Redirect("menu_update_message.aspx");



    }

    protected void btnCreateNewMenu_Click(object sender, EventArgs e)
    {
        string sMenuCode = ConvertUtil.ToString(Request["mnu_uid"]);
        string sUrl = "menu_new.aspx?mnu_uid=" + sMenuCode;
        Response.Redirect(sUrl);

    }


    protected void btnDeleteMenu_Click(object sender, EventArgs e)
    {
        //===============1.删除检查，判断是否有下级菜单出现=================
        string sMenuCode = ViewState["MenuCode"].ToString();

        string sSQL = "select count(*) from sbt_sys_menu where parent_menu_code="
            + StringUtil.QuotedToDBStr(sMenuCode);

        int nChildMenuCnt = ConvertUtil.ToInt(DataHelper.Instance.ExecuteScalar(sSQL));
        if (nChildMenuCnt > 0)
        {
            lblDeleteMsg.Visible = true;
            lblDeleteMsg.Text = "有下级菜单，不能直接删除该菜单。"
                    + "在删除本级菜单之前，请先删除相关子菜单。";
            return;
        }

        TbSysMenu tlbSysMenu = new TbSysMenu();
        tlbSysMenu.MenuCode = sMenuCode;
        tlbSysMenu.Delete();


        //========= 3. 刷新显示 =============
        PageParameter.StringParam[0] = "已删除当前菜单“" +
            TbSysMenu__Lib.Instance.GetMenuRecordByMenuCode(sMenuCode).MenuName + "”";


        string sParentMenuCode = TbSysMenu__Lib.Instance.GetMenuRecordByMenuCode(sMenuCode).ParentMenuCode;

        PageParameter.SetCustomParamString("CurrentParentNode", sParentMenuCode);

        TbSysMenu__Lib.Reset();
        Response.Redirect("menu_update_message.aspx");
    }
}

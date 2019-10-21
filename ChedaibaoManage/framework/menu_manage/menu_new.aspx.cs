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
using Sigbit.Framework.SubSystem;


public partial class framework_menu_navigate_menu_new : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        string sMenuCode = ConvertUtil.ToString(Request["mnu_uid"]);
        if (sMenuCode == "")
        {
            lblMenuName.Text = "无";
            edtMenuCode.Text = RandUtil.NewString(5, RandStringType.Lower);
        }
        else
        {
            lblMenuName.Text = TbSysMenu__Lib.Instance.GetMenuRecordByMenuCode(sMenuCode).MenuName;
            edtMenuCode.Text = sMenuCode + "_" + RandUtil.NewString(5, RandStringType.Lower);
            edtListOrder.Text = TbSysMenu__Lib.Instance.GetMenuRecordByMenuCode(sMenuCode).ListOrder.ToString();
        }

        edtMenuName.Text = "";

        ViewState["MenuCode"] = sMenuCode;
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

    private string GetMenuClass()
    {
        SbtUser user = (SbtUser)Session["currentUserMEFTMG"];
        SbtMenuNode rootMenuNode = user.MainMenuRootNode;
        return user.MainMenuRootNode.MenuSetName;
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {

        //========== 1. 数据校验 ===========

        string sParentMenuCode = ViewState["MenuCode"].ToString();

        string sMenuCode = edtMenuCode.Text.Trim();
        if (sMenuCode == "")
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "请指定菜单编码";
            edtMenuCode.Focus();
            return;
        }

        TbSysMenu tlbSysMenu = new TbSysMenu();
        tlbSysMenu.MenuCode = sMenuCode;
        if (tlbSysMenu.Fetch(true))
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "当前菜单编码已被使用，请换用其它编码";
            edtMenuCode.Focus();
            return;
        }


        //========== 1.1 名称 =============
        string sMenuName = edtMenuName.Text.Trim();
        if (sMenuName == "")
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "请指定菜单名称";
            edtMenuName.Focus();
            return;
        }

        //=========== 1.2 显示顺序 ============
        int nListOrder = ConvertUtil.ToInt(edtListOrder.Text, 0);
        if (nListOrder < 0)
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "显示顺序不能为负数";
            edtListOrder.Focus();
            return;
        }


        tlbSysMenu.MenuCode = sMenuCode;
        tlbSysMenu.MenuName = sMenuName;
        tlbSysMenu.MenuLink = edtMenuLink.Text.Trim();
        //tlbSysMenu.MenuClass = GetMenuClass();
        tlbSysMenu.MenuClass = SUSSubSystem.CurrentSubSystemID__ForMenuEdit;
        tlbSysMenu.MenuStyle = "item";
        tlbSysMenu.MenuIcon = edtMenuIcon.Text.Trim();
        tlbSysMenu.ParentMenuCode = sParentMenuCode;

        int nLevelNum = 1;
        if (sParentMenuCode!= "")
        {
            nLevelNum += TbSysMenu__Lib.Instance.GetMenuRecordByMenuCode(sParentMenuCode).LevelNum;
        }

        tlbSysMenu.LevelNum = nLevelNum;
        tlbSysMenu.ListOrder = ConvertUtil.ToInt(edtListOrder.Text.Trim());
        tlbSysMenu.HasChild = "Y";
        tlbSysMenu.IsActive = ConvertBoolToString(ckbIsActive.Checked);
        tlbSysMenu.IsMenuItem = ConvertBoolToString(ckbIsMenuItem.Checked);
        tlbSysMenu.IsLogItem = ConvertBoolToString(ckbIsLogItem.Checked);
        tlbSysMenu.IsRightItem = ConvertBoolToString(ckbIsRightItem.Checked);
        tlbSysMenu.Insert();

        ////========= 3. 刷新显示 =============
        TbSysMenu__Lib.Reset();

        PageParameter.SetCustomParamString("CurrentParentNode", sParentMenuCode);

        PageParameter.StringParam[0] = "成功添加菜单“"
                    + tlbSysMenu.MenuName + "”";
        Response.Redirect("menu_update_message.aspx");
    }
}

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Sigbit.Common;
using Sigbit.Framework;
using Sigbit.Framework.SubSystem;
using Sigbit.Framework.SubSystem.DBDefine;

public partial class farmwork_main_function_panel_menu : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        TreeNode rootNode = tvMainMenu.Nodes[0];
        rootNode.Text = SUSSubSystem.PoolSubSystem.GetSubSystemNameBySubSystemID(SbtAppContext.CurrentSubSystem);
        if (rootNode.Text == "")
            rootNode.Text = "系统综合应用管理平台";
    

        SbtUser user = (SbtUser)Session["currentUserMEFTMG"];
        SbtMenuNode rootMenuNode = user.MainMenuRootNode;
        rootMenuNode.MenuSetName = SbtAppContext.CurrentSubSystem;
        

        ExpandNode(rootNode, rootMenuNode);
        //rootNode.ExpandAll();
        if (rootNode.ChildNodes.Count > 0)
            rootNode.ChildNodes[0].Expand();
    }

    private void ExpandNode(TreeNode tvNode, SbtMenuNode menuNode)
    {
        if (menuNode.MenuLevel != 0)
        {
            tvNode.ImageUrl = "~/images/menu_icon/" + menuNode.MenuIcon;
            tvNode.Text = menuNode.ChineseName;
            //tvNode.NavigateUrl = menuNode.UrlLink;
            tvNode.NavigateUrl = "~/framework/menu_navigate/menu_navigate.aspx?nav_mnu_cod=" + menuNode.MenuCode;
            tvNode.Target = "table_main";

           
            tvNode.Expanded = false;
            //if (menuNode.MenuLevel > 1)
            //    tvNode.Expanded = false;
            //else
            //    tvNode.Expanded = true;
        }

        SbtMenuNodeList childMenuNodes = menuNode.ChildNodes;
        if (childMenuNodes.Count > 0)
        {
            tvNode.SelectAction = TreeNodeSelectAction.Expand;
            tvNode.NavigateUrl = "";
        }

        for (int i = 0; i < childMenuNodes.Count; i++)
        {
            SbtMenuNode childMenuNode = childMenuNodes.GetNode(i);
            TreeNode childTVNode = new TreeNode();
            tvNode.ChildNodes.Add(childTVNode);
            ExpandNode(childTVNode, childMenuNode);
        }
    }
}

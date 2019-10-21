using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Sigbit.Framework;

public partial class farmwork_main_function_panel_menu : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        TreeNode rootNode = tvMainMenu.Nodes[0];

        SbtUser user = (SbtUser)Session["currentUserMEFTMG"];
        SbtMenuNode rootMenuNode = user.MainMenuRootNode;

        ExpandNode(rootNode, rootMenuNode);
    }

    private void ExpandNode(TreeNode tvNode, SbtMenuNode menuNode)
    {
        if (menuNode.MenuLevel != 0)
        {
            tvNode.ImageUrl = "~/images/menu_icon/" + menuNode.MenuIcon;
            tvNode.Text = menuNode.ChineseName;
            tvNode.NavigateUrl = menuNode.UrlLink;
            tvNode.Target = "table_main";
            tvNode.Expanded = false;
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

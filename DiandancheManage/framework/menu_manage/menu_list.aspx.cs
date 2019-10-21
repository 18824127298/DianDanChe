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

public partial class framework_menu_navigate_menu_list : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        TreeNode rootNode = tvMenuTree.Nodes[0];

        SbtUser user = (SbtUser)Session["currentUserMEFTMG"];
        SbtMenuNode rootMenuNode = user.MainMenuRootNode;

        rootMenuNode.MenuSetName = SUSSubSystem.CurrentSubSystemID__ForMenuEdit;
        string sSubSystemName = SUSSubSystem.PoolSubSystem.GetSubSystemNameBySubSystemID(SUSSubSystem.CurrentSubSystemID__ForMenuEdit);

        if (sSubSystemName != "")
            rootNode.Text = sSubSystemName;

        ExpandNode(rootNode, rootMenuNode);
    }

    private void ExpandNode(TreeNode tvNode, SbtMenuNode menuNode)
    {
        if (menuNode.MenuLevel != 0)
        {
            tvNode.ImageUrl = "~/images/menu_icon/" + menuNode.MenuIcon;
            tvNode.Text = menuNode.ChineseName;
            tvNode.NavigateUrl = "menu_edit.aspx?mnu_uid=" + menuNode.MenuCode;
            tvNode.Target = "frameMenuConfig";

            string sCurrentParentNode = ConvertUtil.ToString(
                PageParameter.GetCustomParamString("CurrentParentNode"), "");
            if (sCurrentParentNode.Contains(menuNode.MenuCode))
            {
                tvNode.Expanded = true;
            }
            else
            {
                tvNode.Expanded = false;
            }
        }

        //SbtMenuNodeList childMenuNodes = menuNode.ChildNodes;

        SbtMenuNodeList childMenuNodes = GetChildNodes(menuNode);
        if (childMenuNodes.Count > 0)
        {
            tvNode.SelectAction = TreeNodeSelectAction.Expand;
        }

        for (int i = 0; i < childMenuNodes.Count; i++)
        {
            SbtMenuNode childMenuNode = childMenuNodes.GetNode(i);
            TreeNode childTVNode = new TreeNode();
            tvNode.ChildNodes.Add(childTVNode);
            ExpandNode(childTVNode, childMenuNode);
        }
    }


    private SbtMenuNodeList GetChildNodes(SbtMenuNode menuNode)
    {

        //========== 1. 得到子节点的记录列表 ==========
        SbtMenuNodeList childNodes = new SbtMenuNodeList();
        ArrayList childList
                = TbSysMenu__Lib.Instance.GetChildRecordsByMenuCode(menuNode.MenuCode);
        if (childList == null)
            return childNodes;

        //========= 2. 循环每一个节点，赋到列表中去 ===========
        for (int i = 0; i < childList.Count; i++)
        {
            TbSysMenu menuRecord = (TbSysMenu)childList[i];

            SbtMenuNode node = AssignByTbSysMenu(menuRecord);

            //if (menuNode.MenuNavigateMethod == SbtMenuNaviMeth.PopedomOfRole)
            //{
            //    if (!node.IsRightItem)
            //        continue;
            //}
            //else
            //{
            //    if (!node.IsMenuItem)
            //        continue;

            //    if (menuNode.MenuNavigateMethod != SbtMenuNaviMeth.SystemAll)
            //    {
            //        if (!node.CurrentUser.HasPopedom(node.MenuCode))
            //            continue;
            //    }
            //}

            //===== 20080528, 菜单集的判断 ==================
            if (node.MenuSetName != menuNode.MenuSetName && menuNode.MenuSetName != "")
                continue;

            node.MenuNavigateMethod = menuNode.MenuNavigateMethod;
            //node.UserUid = this.UserUid;
            node.CurrentUser = this.CurrentUser;



            childNodes.AddNode(node);
        }

        return childNodes;

    }


    /// <summary>
    /// 通过数据库中的记录进行赋值
    /// </summary>
    /// <param name="menuRecord">数据库记录</param>
    private SbtMenuNode AssignByTbSysMenu(TbSysMenu menuRecord)
    {
        SbtMenuNode node = new SbtMenuNode();
        node.MenuCode = menuRecord.MenuCode;
        node.ChineseName = menuRecord.MenuName;
        node.UrlLink = menuRecord.MenuLink;
        switch (menuRecord.MenuStyle)
        {
            case "":
            case "item":
                node.MenuStyle = SbtMenuStyle.Item;
                break;
            default:
                node.MenuStyle = SbtMenuStyle.Tab;
                break;
        }
        node.MenuIcon = menuRecord.MenuIcon;
        node.MenuLevel = menuRecord.LevelNum;
        node.MenuSetName = menuRecord.MenuClass;

        if (menuRecord.IsLogItem == "Y")
            node.IsLogItem = true;
        else
            node.IsLogItem = false;

        if (menuRecord.IsMenuItem == "Y")
            node.IsMenuItem = true;
        else
            node.IsMenuItem = false;

        if (menuRecord.IsRightItem == "Y")
            node.IsRightItem = true;
        else
            node.IsRightItem = false;

        return node;
    }
}

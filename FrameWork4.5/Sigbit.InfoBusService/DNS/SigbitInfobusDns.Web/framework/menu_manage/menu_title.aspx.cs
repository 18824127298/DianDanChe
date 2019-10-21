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

using Sigbit.Framework;

public partial class framework_menu_manage_menu_title : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnTrimMenuOrder_Click(object sender, EventArgs e)
    {

        SbtUser user = (SbtUser)Session["currentUserMEFTMG"];
        SbtMenuNode rootMenuNode = user.MainMenuRootNode;

        TrimNodeOrder(rootMenuNode);

        TbSysMenu__Lib.Reset();
    }

    private int _currentFirstMenuOrder = 0;

    private int _currentSecondMenuOrder = 0;

    protected void TrimNodeOrder(SbtMenuNode menuNode)
    {


        TbSysMenu tlbSysMenu = TbSysMenu__Lib.Instance.GetMenuRecordByMenuCode(menuNode.MenuCode);
        if (tlbSysMenu != null)
        {

            if (menuNode.MenuLevel == 1)
            {
                _currentFirstMenuOrder += 100;
                _currentSecondMenuOrder = 0;
                tlbSysMenu.ListOrder = _currentFirstMenuOrder;
                tlbSysMenu.Update();
            }
            else
            {
                _currentSecondMenuOrder += 1;
                tlbSysMenu.ListOrder = _currentFirstMenuOrder + _currentSecondMenuOrder;
                tlbSysMenu.Update();

            }
        }

        SbtMenuNodeList childMenuNodes = GetChildNodes(menuNode);

        for (int i = 0; i < childMenuNodes.Count; i++)
        {
            SbtMenuNode childMenuNode = childMenuNodes.GetNode(i);
            //TreeNode childTVNode = new TreeNode();
            //tvNode.ChildNodes.Add(childTVNode);
            TrimNodeOrder(childMenuNode);
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

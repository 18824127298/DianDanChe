using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Diagnostics;

namespace Sigbit.Framework
{
    /// <summary>
    /// 定义菜单的显示风格
    /// </summary>
    public enum SbtMenuStyle
    {
        /// <summary>
        /// 主要的菜单显示形式。可按OutlookBar或TreeView方式显示
        /// </summary>
        Item,
        /// <summary>
        /// Tab页的显示形式
        /// </summary>
        Tab
    }

    /// <summary>
    /// 定义菜单的导航方式
    /// </summary>
    public enum SbtMenuNaviMeth
    {
        /// <summary>
        /// 按用户的权限导航。不包括菜单类型标识非item的各项。可用于用户
        /// 界面显示左边的菜单树。
        /// </summary>
        UserRightMain,
        /// <summary>
        /// 按用户的权限导航。包括所有类型的菜单项。可用于列举用户的所
        /// 有菜单。
        /// </summary>
        UserRightAll,
        /// <summary>
        /// 列举所有的item菜单
        /// </summary>
        SystemMain,
        /// <summary>
        /// 列举所有的菜单
        /// </summary>
        SystemAll,
        /// <summary>
        /// 按角色拥有的权限导航
        /// </summary>
        PopedomOfRole
    }

    /// <summary>
    /// 封装一个菜单节点的相关属性
    /// </summary>
    public class SbtMenuNode
    {
        private string _menuCode = "";
        /// <summary>
        /// 菜单编码，菜单的唯一标识
        /// </summary>
        public string MenuCode
        {
            get { return _menuCode; }
            set { _menuCode = value; }
        }

        private string _chineseName = "";
        /// <summary>
        /// 菜单的中文名，用于链接的文字
        /// </summary>
        public string ChineseName
        {
            get { return _chineseName; }
            set { _chineseName = value; }
        }

        private string _urlLink = "";
        /// <summary>
        /// 菜单的链接，用于链接的目标URL
        /// </summary>
        public string UrlLink
        {
            get { return _urlLink; }
            set { _urlLink = value; }
        }

        /// <summary>
        /// 上一级的菜单节点
        /// </summary>
        public SbtMenuNode ParentNode
        {
            get 
            {
                //========= 1. 获取本条记录 ========
                string sMenuCode = this.MenuCode;
                TbSysMenu menuRecord = TbSysMenu__Lib.Instance.GetMenuRecordByMenuCode(sMenuCode);
                Debug.Assert(menuRecord != null);

                //======= 2. 得到父节点号 ========
                string sParentCode = menuRecord.ParentMenuCode;
                if (sParentCode == "")
                    return null;

                //========== 3. 得到父节点 ==========
                TbSysMenu parentRecord 
                        = TbSysMenu__Lib.Instance.GetMenuRecordByMenuCode(sParentCode);
                SbtMenuNode retNode = new SbtMenuNode();
                retNode.AssignByTbSysMenu(parentRecord);

                retNode.MenuNavigateMethod = this.MenuNavigateMethod;
                //retNode.UserUid = this.UserUid;
                retNode.CurrentUser = this.CurrentUser;

                Debug.Assert(retNode.MenuLevel == this.MenuLevel - 1);
                return retNode;
            }
        }

        /// <summary>
        /// 子节点列表，以设定的顺序排序
        /// </summary>
        public SbtMenuNodeList ChildNodes
        {
            get 
            {
                //========== 1. 得到子节点的记录列表 ==========
                SbtMenuNodeList childNodes = new SbtMenuNodeList();
                ArrayList childList 
                        = TbSysMenu__Lib.Instance.GetChildRecordsByMenuCode(this.MenuCode);
                if (childList == null)
                    return childNodes;

                //========= 2. 循环每一个节点，赋到列表中去 ===========
                for (int i = 0; i < childList.Count; i++)
                {
                    TbSysMenu menuRecord = (TbSysMenu)childList[i];

                    SbtMenuNode node = new SbtMenuNode();
                    node.AssignByTbSysMenu(menuRecord);

                    if (this.MenuNavigateMethod == SbtMenuNaviMeth.PopedomOfRole)
                    {
                        if (!node.IsRightItem)
                            continue;
                    }
                    else
                    {
                        if (!node.IsMenuItem)
                            continue;

                        if (this.MenuNavigateMethod != SbtMenuNaviMeth.SystemAll)
                        {
                            if (!this.CurrentUser.HasPopedom(node.MenuCode))
                                continue;
                        }
                    }

                    //===== 20080528, 菜单集的判断 ==================
                    if (node.MenuSetName != this.MenuSetName && this.MenuSetName != "")
                        continue;

                    node.MenuNavigateMethod = this.MenuNavigateMethod;
                    //node.UserUid = this.UserUid;
                    node.CurrentUser = this.CurrentUser;

                    Debug.Assert(node.MenuLevel == this.MenuLevel + 1);

                    childNodes.AddNode(node);
                }

                return childNodes;
            }
        }

        private bool _isMenuItem = false;
        /// <summary>
        /// 是否菜单项
        /// </summary>
        public bool IsMenuItem
        {
            get { return _isMenuItem; }
            set { _isMenuItem = value; }
        }

        private bool _isRightItem = false;
        /// <summary>
        /// 是否权限项
        /// </summary>
        public bool IsRightItem
        {
            get { return _isRightItem; }
            set { _isRightItem = value; }
        }

        private bool _isLogItem = false;
        /// <summary>
        /// 是否日志项
        /// </summary>
        public bool IsLogItem
        {
            get { return _isLogItem; }
            set { _isLogItem = value; }
        }

        private SbtMenuStyle _menuStyle;
        /// <summary>
        /// 菜单的显示风格，目前包括item、tab等
        /// </summary>
        public SbtMenuStyle MenuStyle
        {
            get { return _menuStyle; }
            set { _menuStyle = value; }
        }

        private string _menuIcon = "";
        /// <summary>
        /// 菜单的图像文件名
        /// </summary>
        public string MenuIcon
        {
            get { return _menuIcon; }
            set { _menuIcon = value; }
        }

        private int _menuLevel;
        /// <summary>
        /// 菜单的级数
        /// </summary>
        public int MenuLevel
        {
            get { return _menuLevel; }
            set { _menuLevel = value; }
        }

        private SbtMenuNaviMeth _menuNavigateMethod;
        /// <summary>
        /// 菜单的导航方式
        /// </summary>
        public SbtMenuNaviMeth MenuNavigateMethod
        {
            get { return _menuNavigateMethod; }
            set { _menuNavigateMethod = value; }
        }

        //private string _userUid = "";
        ///// <summary>
        ///// 用户的标识。如果菜单的导航方式为按用户权限导航，则必须设置该属性的值。
        ///// 而匹配出来的子节点也会带上导航方式和用户标识。
        ///// </summary>
        //public string UserUid
        //{
        //    get { return _userUid; }
        //    set { _userUid = value; }
        //}

        private SbtUser _currentUser = null;
        /// <summary>
        /// 当前的用户
        /// </summary>
        public SbtUser CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        private string _menuSetName = "";
        /// <summary>
        /// 菜单集。一个系统可能会有多个菜单集。一个集表示同时显示的一组菜单。
        /// </summary>
        public string MenuSetName
        {
            get { return _menuSetName; }
            set { _menuSetName = value; }
        }

        /// <summary>
        /// 通过数据库中的记录进行赋值
        /// </summary>
        /// <param name="menuRecord">数据库记录</param>
        void AssignByTbSysMenu(TbSysMenu menuRecord)
        {
            this.MenuCode = menuRecord.MenuCode;
            this.ChineseName = menuRecord.MenuName;
            this.UrlLink = menuRecord.MenuLink;
            switch (menuRecord.MenuStyle)
            {
                case "":
                case "item":
                    this.MenuStyle = SbtMenuStyle.Item;
                    break;
                default:
                    Debug.Assert(menuRecord.MenuStyle == "tab");
                    this.MenuStyle = SbtMenuStyle.Tab;
                    break;
            }
            this.MenuIcon = menuRecord.MenuIcon;
            this.MenuLevel = menuRecord.LevelNum;
            this.MenuSetName = menuRecord.MenuClass;

            if (menuRecord.IsLogItem == "Y")
                this.IsLogItem = true;
            else
                this.IsLogItem = false;

            if (menuRecord.IsMenuItem == "Y")
                this.IsMenuItem = true;
            else
                this.IsMenuItem = false;

            if (menuRecord.IsRightItem == "Y")
                this.IsRightItem = true;
            else
                this.IsRightItem = false;
        }
    }

    /// <summary>
    /// 封装菜单的列表
    /// </summary>
    public class SbtMenuNodeList : ArrayList
    {
        /// <summary>
        /// 向列表中增加一个菜单节点
        /// </summary>
        /// <param name="node">菜单节点</param>
        public void AddNode(SbtMenuNode node)
        {
            Add(node);
        }

        /// <summary>
        /// 按索引得到一个菜单节点
        /// </summary>
        /// <param name="nIndex">索引</param>
        /// <returns>菜单节点</returns>
        public SbtMenuNode GetNode(int nIndex)
        {
            return (SbtMenuNode)this[nIndex];
        }
    }

    
}

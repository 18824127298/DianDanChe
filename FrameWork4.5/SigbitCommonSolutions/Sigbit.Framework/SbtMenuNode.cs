using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Diagnostics;

namespace Sigbit.Framework
{
    /// <summary>
    /// ����˵�����ʾ���
    /// </summary>
    public enum SbtMenuStyle
    {
        /// <summary>
        /// ��Ҫ�Ĳ˵���ʾ��ʽ���ɰ�OutlookBar��TreeView��ʽ��ʾ
        /// </summary>
        Item,
        /// <summary>
        /// Tabҳ����ʾ��ʽ
        /// </summary>
        Tab
    }

    /// <summary>
    /// ����˵��ĵ�����ʽ
    /// </summary>
    public enum SbtMenuNaviMeth
    {
        /// <summary>
        /// ���û���Ȩ�޵������������˵����ͱ�ʶ��item�ĸ���������û�
        /// ������ʾ��ߵĲ˵�����
        /// </summary>
        UserRightMain,
        /// <summary>
        /// ���û���Ȩ�޵����������������͵Ĳ˵���������о��û�����
        /// �в˵���
        /// </summary>
        UserRightAll,
        /// <summary>
        /// �о����е�item�˵�
        /// </summary>
        SystemMain,
        /// <summary>
        /// �о����еĲ˵�
        /// </summary>
        SystemAll,
        /// <summary>
        /// ����ɫӵ�е�Ȩ�޵���
        /// </summary>
        PopedomOfRole
    }

    /// <summary>
    /// ��װһ���˵��ڵ���������
    /// </summary>
    public class SbtMenuNode
    {
        private string _menuCode = "";
        /// <summary>
        /// �˵����룬�˵���Ψһ��ʶ
        /// </summary>
        public string MenuCode
        {
            get { return _menuCode; }
            set { _menuCode = value; }
        }

        private string _chineseName = "";
        /// <summary>
        /// �˵������������������ӵ�����
        /// </summary>
        public string ChineseName
        {
            get { return _chineseName; }
            set { _chineseName = value; }
        }

        private string _urlLink = "";
        /// <summary>
        /// �˵������ӣ��������ӵ�Ŀ��URL
        /// </summary>
        public string UrlLink
        {
            get { return _urlLink; }
            set { _urlLink = value; }
        }

        /// <summary>
        /// ��һ���Ĳ˵��ڵ�
        /// </summary>
        public SbtMenuNode ParentNode
        {
            get 
            {
                //========= 1. ��ȡ������¼ ========
                string sMenuCode = this.MenuCode;
                TbSysMenu menuRecord = TbSysMenu__Lib.Instance.GetMenuRecordByMenuCode(sMenuCode);
                Debug.Assert(menuRecord != null);

                //======= 2. �õ����ڵ�� ========
                string sParentCode = menuRecord.ParentMenuCode;
                if (sParentCode == "")
                    return null;

                //========== 3. �õ����ڵ� ==========
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
        /// �ӽڵ��б����趨��˳������
        /// </summary>
        public SbtMenuNodeList ChildNodes
        {
            get 
            {
                //========== 1. �õ��ӽڵ�ļ�¼�б� ==========
                SbtMenuNodeList childNodes = new SbtMenuNodeList();
                ArrayList childList 
                        = TbSysMenu__Lib.Instance.GetChildRecordsByMenuCode(this.MenuCode);
                if (childList == null)
                    return childNodes;

                //========= 2. ѭ��ÿһ���ڵ㣬�����б���ȥ ===========
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

                    //===== 20080528, �˵������ж� ==================
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
        /// �Ƿ�˵���
        /// </summary>
        public bool IsMenuItem
        {
            get { return _isMenuItem; }
            set { _isMenuItem = value; }
        }

        private bool _isRightItem = false;
        /// <summary>
        /// �Ƿ�Ȩ����
        /// </summary>
        public bool IsRightItem
        {
            get { return _isRightItem; }
            set { _isRightItem = value; }
        }

        private bool _isLogItem = false;
        /// <summary>
        /// �Ƿ���־��
        /// </summary>
        public bool IsLogItem
        {
            get { return _isLogItem; }
            set { _isLogItem = value; }
        }

        private SbtMenuStyle _menuStyle;
        /// <summary>
        /// �˵�����ʾ���Ŀǰ����item��tab��
        /// </summary>
        public SbtMenuStyle MenuStyle
        {
            get { return _menuStyle; }
            set { _menuStyle = value; }
        }

        private string _menuIcon = "";
        /// <summary>
        /// �˵���ͼ���ļ���
        /// </summary>
        public string MenuIcon
        {
            get { return _menuIcon; }
            set { _menuIcon = value; }
        }

        private int _menuLevel;
        /// <summary>
        /// �˵��ļ���
        /// </summary>
        public int MenuLevel
        {
            get { return _menuLevel; }
            set { _menuLevel = value; }
        }

        private SbtMenuNaviMeth _menuNavigateMethod;
        /// <summary>
        /// �˵��ĵ�����ʽ
        /// </summary>
        public SbtMenuNaviMeth MenuNavigateMethod
        {
            get { return _menuNavigateMethod; }
            set { _menuNavigateMethod = value; }
        }

        //private string _userUid = "";
        ///// <summary>
        ///// �û��ı�ʶ������˵��ĵ�����ʽΪ���û�Ȩ�޵�������������ø����Ե�ֵ��
        ///// ��ƥ��������ӽڵ�Ҳ����ϵ�����ʽ���û���ʶ��
        ///// </summary>
        //public string UserUid
        //{
        //    get { return _userUid; }
        //    set { _userUid = value; }
        //}

        private SbtUser _currentUser = null;
        /// <summary>
        /// ��ǰ���û�
        /// </summary>
        public SbtUser CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        private string _menuSetName = "";
        /// <summary>
        /// �˵�����һ��ϵͳ���ܻ��ж���˵�����һ������ʾͬʱ��ʾ��һ��˵���
        /// </summary>
        public string MenuSetName
        {
            get { return _menuSetName; }
            set { _menuSetName = value; }
        }

        /// <summary>
        /// ͨ�����ݿ��еļ�¼���и�ֵ
        /// </summary>
        /// <param name="menuRecord">���ݿ��¼</param>
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
    /// ��װ�˵����б�
    /// </summary>
    public class SbtMenuNodeList : ArrayList
    {
        /// <summary>
        /// ���б�������һ���˵��ڵ�
        /// </summary>
        /// <param name="node">�˵��ڵ�</param>
        public void AddNode(SbtMenuNode node)
        {
            Add(node);
        }

        /// <summary>
        /// �������õ�һ���˵��ڵ�
        /// </summary>
        /// <param name="nIndex">����</param>
        /// <returns>�˵��ڵ�</returns>
        public SbtMenuNode GetNode(int nIndex)
        {
            return (SbtMenuNode)this[nIndex];
        }
    }

    
}

using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;

namespace Sigbit.Framework
{
    /// <summary>
    /// 菜单记录库
    /// </summary>
    /// <remarks>
    /// 以多种速查库的方式组织全部菜单数据，便于快速查找、定位，并为其
    /// 它类的实现提供支持。
    /// </remarks>
    public class TbSysMenu__Lib
    {
       
        Hashtable _htParentMenuCode = new Hashtable();
        Hashtable _htMenuCode = new Hashtable();
        Hashtable _htMenuLinkPool = new Hashtable();

        private static TbSysMenu__Lib _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static TbSysMenu__Lib Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new TbSysMenu__Lib();
                return _thisInstance;
            }
        }

        /// <summary>
        /// 清空菜单记录库
        /// </summary>
        public static void Reset()
        {
            _thisInstance = null;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TbSysMenu__Lib()
        {
            DataSet ds = TbSysMenu.GetDataSetOfAll();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TbSysMenu menuRecord = new TbSysMenu();
                menuRecord.AssignByDataRow(ds, i);
                AddMenuRecordToLib(menuRecord);
            }
        }

        /// <summary>
        /// 将菜单记录加到库中
        /// </summary>
        /// <param name="menuRecord">菜单记录</param>
        private void AddMenuRecordToLib(TbSysMenu menuRecord)
        {

            //========= 1. 按menu_code增加到HashTable中 ========
            if (_htMenuCode[menuRecord.MenuCode] != null)
                throw new Exception("TbSysMenu__Lib.AddMenuRecordToLib() Error: menuCode冲突");
            else
                _htMenuCode[menuRecord.MenuCode] = menuRecord;

            //======== 2. 按parent_menu_code增加到HashTable中 =======
            ArrayList childMenuList = null;
            if (_htParentMenuCode[menuRecord.ParentMenuCode] == null)
            {
                childMenuList = new ArrayList();
                _htParentMenuCode[menuRecord.ParentMenuCode] = childMenuList;
            }
            else
                childMenuList = (ArrayList)_htParentMenuCode[menuRecord.ParentMenuCode];

            childMenuList.Add(menuRecord);

            //========= 3.菜单链接池 =================

            if (menuRecord.MenuLink != "")
            {
                string sMenuLinkUrl = menuRecord.MenuLink.ToLower();

                if (sMenuLinkUrl.Contains("?"))
                {
                    sMenuLinkUrl = sMenuLinkUrl.Substring(0, sMenuLinkUrl.IndexOf('?'));
                }

                _htMenuLinkPool[sMenuLinkUrl] = menuRecord;
            }

        }

        /// <summary>
        /// 按照菜单编码获取菜单记录
        /// </summary>
        /// <param name="sMenuCode">菜单编码</param>
        /// <returns></returns>
        public TbSysMenu GetMenuRecordByMenuCode(string sMenuCode)
        {
            return (TbSysMenu)_htMenuCode[sMenuCode];
        }

        /// <summary>
        /// 按照菜单编码获取子菜单列表
        /// </summary>
        /// <param name="sMenuCode">菜单编码</param>
        /// <returns></returns>
        public ArrayList GetChildRecordsByMenuCode(string sMenuCode)
        {
            ArrayList arrRet = (ArrayList)_htParentMenuCode[sMenuCode];
            if (arrRet == null)
                return new ArrayList();
            else
                return arrRet;
        }


        public TbSysMenu GetMenuRecordByMenuLink(string sMenuLink)
        {
            return (TbSysMenu)_htMenuLinkPool[sMenuLink];
        }

    }
}

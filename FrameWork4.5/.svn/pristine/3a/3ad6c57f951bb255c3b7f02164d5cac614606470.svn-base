using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Framework;

namespace Sigbit.Framework.Test
{
    public class TestExecCSFunction
    {
        public static string DoTest(string sMenuCode)
        {
            TbSysMenu tblMenu = new TbSysMenu();
            tblMenu.MenuCode = sMenuCode;
            if (tblMenu.Fetch(true))
            {
                return "菜单" + sMenuCode + "标题为" + tblMenu.MenuName;
            }
            else
                return "找不到菜单项: " + sMenuCode;
        }
    }
}

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
using Sigbit.Data;
using Sigbit.Common;

public partial class framework_ctrlpanel_test : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        string sImport = memoImport.Text.Trim();
        string[] arrLines = StringUtil.Split(sImport, "\r\n");

        //========== 1. 读取每一行 ==========
        for (int i = 0; i < arrLines.Length; i++)
        {
            //======= 2. 分隔出各个字段 ==========
            string sLine = arrLines[i];
            string [] arrItems = sLine.Split('\t');
            if (arrItems.Length < 4)
                continue;

            //======== 2.1 菜单编码 ==========
            string sMenuCode = arrItems[1];
            if (sMenuCode == "")
                continue;

            //======= 2.2 菜单名称 ======
            string sMenuName = arrItems[0];

            //======= 2.3 菜单链接 ======
            string sMenuIcon = arrItems[3];

            //======== 2.4 菜单链接 =======
            string sMenuLink;
            if (arrItems.Length >= 5)
                sMenuLink = arrItems[4];
            else
                sMenuLink = "";
            if (sMenuLink == "")
                sMenuLink = "~/demo.aspx";

            //======== 3. 准备数据，并写入表中 ==========
            TbSysMenu tbl = new TbSysMenu();
            tbl.MenuCode = sMenuCode;
            tbl.MenuName = sMenuName;
            tbl.MenuLink = sMenuLink;
            tbl.MenuClass = "main_menu";
            tbl.MenuStyle = "item";
            tbl.MenuIcon = sMenuIcon;

            //======== 3.1 父菜单号 =======
            int nLastUnderIndex = sMenuCode.LastIndexOf('_');
            if (nLastUnderIndex != -1)
                tbl.ParentMenuCode = sMenuCode.Substring(0, nLastUnderIndex);

            //======== 3.2 所处位置(层) ======
            tbl.LevelNum = StringUtil.Occurs("_", sMenuCode) + 1;
            tbl.ListOrder = 100 + i;
            tbl.IsActive = "Y";
            tbl.HasChild = "Y";
            tbl.IsMenuItem = "Y";
            tbl.IsLogItem = "Y";
            tbl.IsRightItem = "Y";
            tbl.Remarks = "";
            tbl.Insert();
        }
    }
}



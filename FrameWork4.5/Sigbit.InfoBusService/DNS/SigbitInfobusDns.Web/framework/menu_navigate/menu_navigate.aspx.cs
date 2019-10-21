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
using Sigbit.Data;
using Sigbit.Framework;

public partial class framework_menu_navigate_menu_navigate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //========= 1. 得到菜单代码 ===============
        string sNaviMenuCode = ConvertUtil.ToString(Request["nav_mnu_cod"]);

        //======== 2. 得到菜单代码相应的管理页面 =============
        TbSysMenu tblMenu = TbSysMenu__Lib.Instance.GetMenuRecordByMenuCode(sNaviMenuCode);
        if (tblMenu == null)
        {
            Response.Redirect("~/framework/main/table_index/intel_view.aspx");
            return;
        }

        string sNavigateUrl = tblMenu.MenuLink;

        //========= 3. 在上下文中记录访问的菜单项 ==========
        SbtAppContext.VisitMenu(sNaviMenuCode);

        //======= 4. 转到相应的管理页面 =========
        Response.Redirect(sNavigateUrl);
    }
}

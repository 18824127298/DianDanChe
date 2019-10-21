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

public partial class framework_tab_delete : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //========= 1. 得到TabItem索引 ===============
        string sTabIndex = ConvertUtil.ToString(Request["tab_index"]);
        int nTabIndex = ConvertUtil.ToInt(sTabIndex);

        //========= 2 得到Bar名称 ===============
        string sBarName = ConvertUtil.ToString(Request["bar_name"]);
        if(sBarName!="")
        {
            this.NaviTabController.SwitchToBar(sBarName);
        }

        //========= 3. 删除TabItem并返回当前页面 =============
        string sURL = this.NaviTabController.RemoveAndReturn(nTabIndex);
        Response.Redirect(sURL);
    }
}

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

public partial class module_gcmap_gcfence_manage_result : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //================ 1.0 首次返回 ================
        if (IsPostBack)
            return;

        //================ 2.0 获取围栏参数 ========  
        string sPointALatitude = ConvertUtil.ToString(Request["ALatitude"]);
        string sPointALongitude = ConvertUtil.ToString(Request["ALongitude"]); 
        string sPointBLatitude = ConvertUtil.ToString(Request["BLatitude"]);
        string sPointBLongitude = ConvertUtil.ToString(Request["BLongitude"]);  

        Response.Write("A:" + sPointALatitude + "," + sPointALongitude);

        Response.Write("B:" + sPointBLatitude + "," + sPointBLongitude);
    }
}

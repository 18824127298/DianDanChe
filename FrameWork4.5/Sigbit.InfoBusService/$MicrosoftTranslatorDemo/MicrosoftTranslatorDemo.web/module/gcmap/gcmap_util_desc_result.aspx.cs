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

public partial class module_gcmap_gcmap_util_desc_result : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {             
        if (IsPostBack)
            return;

        //=================== 1.0 获取参数 ====================
        string sDiscription = Request["info"];
        if (sDiscription == null)
        {
            return;
        }
        string[] arrayDiscription = sDiscription.Split('|');
        Response.Charset = "utf-8";

        //=================== 2.0 获取参数内容 ================
        Response.Write(sDiscription);    
    }
}

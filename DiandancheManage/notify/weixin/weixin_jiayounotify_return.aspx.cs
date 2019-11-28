using JiaYouWxPayApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class notify_weixin_weixin_jiayounotify_return : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ResultNotify resultNotify = new ResultNotify(this);
        resultNotify.ProcessNotify();
    }
}
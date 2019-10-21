using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Sigbit.Lib.Translator.Microsoft;

public partial class temp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TranslateService trans = new TranslateService("sigbit_siri", "4Gbkqbsa2fPk9eWUn+2WLhkwnoEGxFmiD62igFD0fpE=");
        ltResult.Text = trans.DetectLanguage("hello");
    }
}

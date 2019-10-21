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
using Sigbit.Framework;
using Sigbit.App.TeleXXG;

public partial class module_Editor_include_file : SbtPageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        string _fileSavePath = ConvertUtil.ToString(Request["path"]);
        ViewState["fileSavePath"] = _fileSavePath;
    }

    //上传文件



    protected void btnUpLoad_Click(object sender, EventArgs e)
    {
        string sFileDesc = edtFileDesc.Text.Trim();
        if (sFileDesc.Length <= 0)
        {
            sFileDesc = "附件下载";
        }
        string sRetUrl = XXGUtil.UploadFile(fuUpload, ViewState["fileSavePath"].ToString());
        string sJS = @"<script language=javascript>";
        sJS += " window.returnValue =  '<a href=\"" + sRetUrl + "\" target=\"_blank\">" + sFileDesc + "</a>';";
        sJS += "window.close();";
        sJS += "</script>";
        Response.Write(sJS);
    }
}

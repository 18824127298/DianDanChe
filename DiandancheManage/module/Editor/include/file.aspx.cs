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
using Sigbit.Web.JavaScipt;
using Sigbit.Web.MediaServer;
using System.IO;

public partial class module_Editor_include_file : SbtPageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        string _fileSavePath = ConvertUtil.ToString(Request["path"]);
        _fileSavePath = Server.UrlDecode(_fileSavePath);
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
        if (fuUpload.PostedFile.ContentLength <= 0)
        {
            JSMessageBox.Alert("请选择文件再上传");
            return;
        }

        try
        {
            string sFileExt = FileUtil.ExtractFileExt(fuUpload.PostedFile.FileName);

            string sFileName = RandUtil.NewString(10, RandStringType.UpperLowerNumber) + sFileExt;

            string sFullFileName = ViewState["fileSavePath"] + sFileName;

            MediaServerPath mediaPath = new MediaServerPath();
            mediaPath.RelativePath = sFullFileName;

            FileInfo uploadFile = new FileInfo(mediaPath.FullPath);
            if (!uploadFile.Directory.Exists)
            {
                uploadFile.Directory.Create();
            }

            fuUpload.PostedFile.SaveAs(mediaPath.FullPath);

            string sJS = @"<script language=javascript>";
            sJS += " window.returnValue =  '<a href=\"" + mediaPath.FullUrl
                + "\" target=\"_blank\">" + sFileDesc + "</a>';";
            sJS += "window.close();";
            sJS += "</script>";
            Response.Write(sJS);
        }
        catch (Exception ex)
        {
            JSMessageBox.Alert(ex.Message);
        }

       
    }
}

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

using System.IO;
using Sigbit.Common;
using Sigbit.Web.JavaScipt;
using Sigbit.Web.MediaServer;

public partial class Editor_include_img : System.Web.UI.Page
{
    /// <summary>
    /// 图片保存路径
    /// </summary>
    private string _SavePath = "";
    private void Page_Load(object sender, System.EventArgs e)
    {
        _SavePath = Server.UrlDecode(Request["path"]);

        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");
    }

    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// 设计器支持所需的方法 - 不要使用代码编辑器修改
    /// 此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
        this.btnUpLoad.Click += new System.EventHandler(this.btnUpLoad_Click);
        this.Load += new System.EventHandler(this.Page_Load);

    }
    #endregion

    private void btnUpLoad_Click(object sender, System.EventArgs e)
    {
        if (fupImage.PostedFile.ContentLength <= 0)
        {
            JSMessageBox.Alert("请选择图片文件后再上传!");
            return;
        }

        //文件后缀
        string sFileExt = FileUtil.ExtractFileExt(fupImage.PostedFile.FileName);
        sFileExt=sFileExt.ToLower();

        //保存文件名称
        string sSaveFile = RandUtil.NewString(10, RandStringType.UpperLowerNumber) + sFileExt;

        //保存文件完整名称
        string sFullFileName = _SavePath + sSaveFile;

        try
        {

            MediaServerPath mediaPath = new MediaServerPath();
            mediaPath.RelativePath = sFullFileName;

            FileInfo uploadFile = new FileInfo(mediaPath.FullPath);
            if (!uploadFile.Directory.Exists)
            {
                uploadFile.Directory.Create();
            }
            if (sFileExt != ".jpg" && sFileExt != ".gif" && sFileExt != ".jpeg" && sFileExt != ".bmp" && sFileExt != ".png")
            {
                Sigbit.Web.JavaScipt.JSMessageBox.Alert("错误!!文件类型必须为jpg,gif,bmp,png其中一种!");
                return;
            }
            fupImage.PostedFile.SaveAs(mediaPath.FullPath);


            System.Drawing.Image img = System.Drawing.Image.FromFile(mediaPath.FullPath);
            int nImgWidth = img.Width;
            if (img.Width > 780)
            {
                nImgWidth = 600;
            }


            string sJSTemplate = @"
                  <script language=javascript>
                        window.returnValue = '<img src=""{0}""  width=""{1}px"" />';
                        window.close();
                  </script>";

            string sJS = string.Format(sJSTemplate, mediaPath.FullUrl, nImgWidth);
            Response.Write(sJS);
        }
        catch (OutOfMemoryException oex)
        {
            JSMessageBox.Alert(oex.Message);
        }
        catch (Exception ex)
        {
            JSMessageBox.Alert(ex.Message);
        }
    }

    public bool ThumbnailCallback()
    {
        return false;
    }
}

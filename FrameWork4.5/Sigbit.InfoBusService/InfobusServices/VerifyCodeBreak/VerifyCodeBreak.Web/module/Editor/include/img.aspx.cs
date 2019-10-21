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
//using WeYyzyq.Comp;

using System.IO;
using Sigbit.Web.JavaScipt;
using Sigbit.App.RobotXiaoQiang.HWInterf.Common;
//using Sigbit.App.TeleXXG;

public partial class Editor_include_img : System.Web.UI.Page
{
    /// <summary>
    /// 图片保存路径
    /// </summary>
    private string _SavePath = "";
    private void Page_Load(object sender, System.EventArgs e)
    {
        _SavePath = "";

        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");
    }

    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
        //
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
        upfile();
    }


    /// <summary>
    /// 文件上传
    /// </summary>
    /// <returns>文件上传到服务器的路径</returns>
    void upfile()
    {
        #region
        string strWidth = "";
        Random myrd = new Random();
        string path, filename, upfilepath;	//上传文件路径,上传文件名称
        //path = Server.MapPath("~/upfile/users/");
        //path = SiteGlobalConfig.MediaServerPath + _SavePath;

        //======= 2010-05-13 调整此行 ============
        //path = XXGConfig.Instance.MediaServerRootPath + _SavePath;
        path = HWInterfConfig.Instance.KBMImageFileSavePath + _SavePath;
        //==========================================
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }


        filename = DateTime.Now.ToString().Replace("-", "").Replace(":", "").Replace(" ", "") + myrd.Next(1000).ToString();		//上传文件名称

        //取得文件的扩展名
        string fileExtension = System.IO.Path.GetExtension(File1.PostedFile.FileName).ToLower();
        path = path + filename + fileExtension;
        if (fileExtension != ".jpg" && fileExtension != ".gif" && fileExtension != ".jpeg")
        {
            //Jscript.Alert("错误!!文件类型必须为jpg或者gif!");
            Sigbit.Web.JavaScipt.JSMessageBox.Alert("错误!!文件类型必须为jpg或者gif!");
        }
        //else if (File1.PostedFile.ContentLength > 120400)
        //    //Jscript.Alert("错误!!文件大小不能超过100K！");
        //    Sigbit.Web.JavaScipt.JSMessageBox.Alert("错误!!文件大小不能超过100K！");
        else
        {
            upfilepath = _SavePath + filename + fileExtension;
            File1.PostedFile.SaveAs(path);
            try
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(path);
                //System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
                int w = image.Width;
                strWidth = (w >= 780) ? " width=\"600\" " : "";
                string js = @"<script language=javascript>";
                //======= 2010-05-13 调整此行 ============
                //js += " window.returnValue =  '<img src= \"" + "/MediaServer/" + XXGConfig.Instance.SiteName + "/" + upfilepath + "\" " + strWidth + "/>';";
                js += " window.returnValue =  ";
                js += "'<img src= \"";
                js += HWInterfConfig.Instance.KBMImageVisitUrlPath;
                js += "" + upfilepath + "\" " + strWidth + "/>';";
                //=========================================
                js += "window.close();";
                js += "</script>";
                Response.Write(js);
            }
            catch (Exception ex)
            {
                JSMessageBox.Alert(ex.Message);
            }
        }
        #endregion
    }

    public bool ThumbnailCallback()
    {
        return false;
    }
}

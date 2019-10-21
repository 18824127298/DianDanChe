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
using Sigbit.Web.JavaScipt;

/// <summary>
/// HTML在线编辑器
/// 创建者：马先光
/// 创建时间：2006-8-11
/// </summary>
public partial class Controls_HtmlEdit : System.Web.UI.UserControl
{
    private int _Width = 300;

    /// <summary>
    /// 控件宽度
    /// </summary>
    public int Width
    {
        get { return _Width; }
        set { _Width = value; }
    }

    private int _Height = 280;
    /// <summary>
    /// 控件高度
    /// </summary>
    public int Height
    {
        get { return _Height; }
        set { _Height = value; }
    }

    private string _ImgSavePath = "/upload/images/";
    /// <summary>
    /// 图片保存路径
    /// </summary>
    public string ImgSavePath
    {
        get { return _ImgSavePath; }
        set { _ImgSavePath = value; }
    }

    private string _fileSavePath = "/upload/files/";
    /// <summary>
    /// 文件保存路径
    /// </summary>
    public string FileSavePath
    {
        get { return _fileSavePath; }
        set { _fileSavePath = value; }
    }

    private string _JSRootPath = "";

    public string JSRootPath
    {
        get { return _JSRootPath; }
    }

    private void Page_Load(object sender, System.EventArgs e)
    {
        //加module可根据实际路径调整
        _JSRootPath = GenJSRootPath() + "module/";

        if (!IsPostBack)
        {
            Response.Expires = 0;
            //清空缓存
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
        }
    }

    /// <summary>
    /// 生成JS根路径
    /// </summary>
    /// <returns></returns>
    protected string GenJSRootPath()
    {
        string sRet = "";
        int nFindCount = Occurs("/", this.Page.AppRelativeVirtualPath);
        for (int i = 1; i < nFindCount; i++)
        {
            sRet += "../";
        }
        return sRet;
    }

    /// <summary>
    /// 计数某子串在字符串中产生的次数
    /// </summary>
    /// <param name="sSubStr">待寻找的子串</param>
    /// <param name="sString">包含子串的字符串</param>
    /// <returns>计数得到的次数</returns>
    public int Occurs(string sSubStr, string sString)
    {
        int nRet = 0;
        bool bFound = true;

        while (bFound)
        {
            int nFindPos = sString.IndexOf(sSubStr);
            if (nFindPos != -1)
            {
                nRet++;
                sString = sString.Substring(nFindPos + sSubStr.Length);
            }
            else
                bFound = false;
        }

        return nRet;
    }

    private string ShowStyle()
    {
        string sStyle = "width:{0}px; height:{1}px;";
        return string.Format(sStyle, Width, Height);
    }

    /// <summary>
    /// 设置提交控件
    /// </summary>
    /// <param name="ctrl"></param>
    public void SetControl(Control ctrl)
    {
        switch (ctrl.GetType().ToString())
        {
            case "System.Web.UI.WebControls.Button":
                ((Button)ctrl).Attributes.Add("onClick", "CheckForm()");
                break;
            case "System.Web.UI.WebControls.Wizard":
                ((Wizard)ctrl).Attributes.Add("onClick", "CheckForm()");
                break;
        }
    }

    public void GetControl(Wizard ctrl)
    {
        ctrl.Attributes.Add("onClick", "CheckFormChange()");
    }

    public string Content
    {
        get
        {
            return GetHtmlEditReplace(content.Value);
            //return GetHtmlEditReplace(content.Text);
        }
        set
        {
            content.Value = value;
            //content.Text = value;
        }
    }

    /// <summary>
    /// 返回文本编辑器替换后的字符串
    /// </summary>
    /// <param name="str">要替换的字符串</param>
    /// <returns></returns>
    public string GetHtmlEditReplace(string str)
    {
        #region
        str = str.Replace("http://" + Page.Request.Url.Authority, "");

        //%%%%%%%% ======= 2010.06.26 BY OLDIX 解决间距过大的问题 ===========
        str = str.Replace("<P>", "");
        str = str.Replace("</P>", "<BR>");

        //%%%%%%%% ======= end of 2010.06.26 BY OLDIX 解决间距过大的问题 ===========

        return str;
        //return str.Replace("'", "''").Replace("&nbsp;", " ").Replace(",", "，").Replace("%", "％").Replace("script", "").Replace(".js", "");
        #endregion
    }

}

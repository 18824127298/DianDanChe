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
using Sigbit.Web.JavaScipt;
using Sigbit.Web.MediaServer;

/// <summary>
/// HTML编辑器
/// </summary>
public partial class Controls_HtmlEdit : System.Web.UI.UserControl
{

    private string _width = "500px";
    /// <summary>
    /// 控件宽度
    /// </summary>
    public string Width
    {
        get
        {
            return _width;
        }
        set
        {
            _width = value.Trim().ToLower();
        }
    }


    private string _height = "300px";
    /// <summary>
    /// 控件高度
    /// </summary>
    public string Height
    {
        get
        {
            return _height;
        }
        set
        {
            _height = value.Trim().ToLower();
        }
    }


    private string _imgSavePath = "";
    /// <summary>
    /// 图片保存根路径
    /// </summary>
    public string ImgSavePath
    {
        get
        {
            return _imgSavePath;
        }
        set
        {
            _imgSavePath = ReguSubPath(value);
        }
    }


    private string ReguSubPath(string sPath)
    {
        //======1.去除空格======
        sPath = sPath.Trim();

        //======2.去除前导"\"号=====
        sPath = sPath.TrimStart('\\');

        //======3.去除前导"/"号=====
        sPath = sPath.TrimStart('/');

        //======4.去除结尾"\"号=====
        sPath = sPath.TrimEnd('\\');

        //======5.去除结尾"/"号=====
        sPath = sPath.TrimEnd('/');

        //======6.结尾添加"\"号====
        sPath += "\\";

        return sPath;
    }


    private string _fileSavePath = "";
    /// <summary>
    /// 上传文件保存根路径 
    /// </summary>
    public string FileSavePath
    {
        get
        {
            return _fileSavePath;
        }
        set
        {
            _fileSavePath = ReguSubPath(value);
        }
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

        string sStyle = "width:{0}; height:{1};";
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

    /// <summary>
    /// 正文内容
    /// </summary>
    public string Content
    {
        get
        {
            string sContent = MediaServerUtil.UrlFormat(content.Value);
            return sContent;
        }
        set
        {
            content.Value = MediaServerUtil.UrlUnFormat(value);
        }
    }

    
}

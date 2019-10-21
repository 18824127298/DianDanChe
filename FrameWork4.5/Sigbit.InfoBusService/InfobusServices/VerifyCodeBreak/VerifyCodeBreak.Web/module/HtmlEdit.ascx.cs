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
/// HTML���߱༭��
/// �����ߣ����ȹ�
/// ����ʱ�䣺2006-8-11
/// </summary>
public partial class Controls_HtmlEdit : System.Web.UI.UserControl
{
    private int _Width = 300;

    /// <summary>
    /// �ؼ����
    /// </summary>
    public int Width
    {
        get { return _Width; }
        set { _Width = value; }
    }

    private int _Height = 280;
    /// <summary>
    /// �ؼ��߶�
    /// </summary>
    public int Height
    {
        get { return _Height; }
        set { _Height = value; }
    }

    private string _ImgSavePath = "/upload/images/";
    /// <summary>
    /// ͼƬ����·��
    /// </summary>
    public string ImgSavePath
    {
        get { return _ImgSavePath; }
        set { _ImgSavePath = value; }
    }

    private string _fileSavePath = "/upload/files/";
    /// <summary>
    /// �ļ�����·��
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
        //��module�ɸ���ʵ��·������
        _JSRootPath = GenJSRootPath() + "module/";

        if (!IsPostBack)
        {
            Response.Expires = 0;
            //��ջ���
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
        }
    }

    /// <summary>
    /// ����JS��·��
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
    /// ����ĳ�Ӵ����ַ����в����Ĵ���
    /// </summary>
    /// <param name="sSubStr">��Ѱ�ҵ��Ӵ�</param>
    /// <param name="sString">�����Ӵ����ַ���</param>
    /// <returns>�����õ��Ĵ���</returns>
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
    /// �����ύ�ؼ�
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
    /// �����ı��༭���滻����ַ���
    /// </summary>
    /// <param name="str">Ҫ�滻���ַ���</param>
    /// <returns></returns>
    public string GetHtmlEditReplace(string str)
    {
        #region
        str = str.Replace("http://" + Page.Request.Url.Authority, "");

        //%%%%%%%% ======= 2010.06.26 BY OLDIX �������������� ===========
        str = str.Replace("<P>", "");
        str = str.Replace("</P>", "<BR>");

        //%%%%%%%% ======= end of 2010.06.26 BY OLDIX �������������� ===========

        return str;
        //return str.Replace("'", "''").Replace("&nbsp;", " ").Replace(",", "��").Replace("%", "��").Replace("script", "").Replace(".js", "");
        #endregion
    }

}

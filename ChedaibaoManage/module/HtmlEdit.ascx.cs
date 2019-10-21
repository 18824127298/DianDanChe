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
/// HTML�༭��
/// </summary>
public partial class Controls_HtmlEdit : System.Web.UI.UserControl
{

    private string _width = "500px";
    /// <summary>
    /// �ؼ����
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
    /// �ؼ��߶�
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
    /// ͼƬ�����·��
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
        //======1.ȥ���ո�======
        sPath = sPath.Trim();

        //======2.ȥ��ǰ��"\"��=====
        sPath = sPath.TrimStart('\\');

        //======3.ȥ��ǰ��"/"��=====
        sPath = sPath.TrimStart('/');

        //======4.ȥ����β"\"��=====
        sPath = sPath.TrimEnd('\\');

        //======5.ȥ����β"/"��=====
        sPath = sPath.TrimEnd('/');

        //======6.��β���"\"��====
        sPath += "\\";

        return sPath;
    }


    private string _fileSavePath = "";
    /// <summary>
    /// �ϴ��ļ������·�� 
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

        string sStyle = "width:{0}; height:{1};";
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

    /// <summary>
    /// ��������
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

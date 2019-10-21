using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using Sigbit.Common;


using System.Text;
using System.IO;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    #region  文件类名规整

    protected void ReguFile()
    {
        //============= 1.获取所有Aspx即CS文件 =============

        string sCurPath = MapPath("~");

        string[] arrAspxCsFiles = Directory.GetFiles(sCurPath, "*.aspx.cs", SearchOption.AllDirectories);


        for (int i = 0; i < arrAspxCsFiles.Length; i++)
        {
            string sFileName = arrAspxCsFiles[i];

            if (sFileName.Contains("Default.aspx.cs"))
                continue;

            ReguAspxCsFile(arrAspxCsFiles[i], sCurPath.Length);
        }


        //lblMessage.Text = "成功规整" + arrAspxCsFiles.Length + "个文件！";
    }

    protected void ReguAspxFile(string sAspxFile, string sReguClassName)
    {
        byte[] btArrFiles = FileUtil.ReadBytesFromFile(sAspxFile);

        string sFileContent = Encoding.UTF8.GetString(btArrFiles);


        //<%@ Page Language="C#" AutoEventWireup="true" CodeFile="v03_dtmf_and_record.aspx.cs" Inherits="gxb_ivr_flows_single_node_dial_v02_dtmf_predial" %>


        //=========== 1.寻找第一个Inherits =============

        int nFirstInheritsIndex = sFileContent.IndexOf("Inherits=\"") + 10;
        int nFirstInheritEndIndex = sFileContent.IndexOf("\"", nFirstInheritsIndex);

        string sFileInClassName = sFileContent.Substring(nFirstInheritsIndex, nFirstInheritEndIndex - nFirstInheritsIndex);

        sFileContent = sFileContent.Replace(sFileInClassName, sReguClassName);

        SaveToFile(sAspxFile, sFileContent);

    }



    protected void ReguAspxCsFile(string sAspxCsFile, int nStartIndex)
    {
        //public partial class gxb_ivr_flows_single_node_dial_v02_dtmf_predial : CLCIvrRecordRawPageBase


        byte[] btArrFiles = FileUtil.ReadBytesFromFile(sAspxCsFile);

        string sFileContent = Encoding.UTF8.GetString(btArrFiles);



        //============ 寻找第一个class ==============

        int nFirstClassIndex = sFileContent.IndexOf(" class ") + 7;


        //============ 寻找第一个:号 ================

        int nFirstColonIndex = sFileContent.IndexOf(" : ");


        string sSearchClassName = sFileContent.Substring(nFirstClassIndex, nFirstColonIndex - nFirstClassIndex);


        //html_pages_demo\single_node_dial_english_version\v02_dtmf_predial.aspx.cs
        string sReguClassName = sAspxCsFile.ToLower().Substring(nStartIndex).TrimStart('\\');

        sReguClassName = sReguClassName.Replace(".aspx.cs", "").Replace('\\', '_');


        sFileContent = sFileContent.Replace(sSearchClassName, sReguClassName);
        SaveToFile(sAspxCsFile, sFileContent);


        //================= 2.ASPX文件的修改 ================

        string sAspxFile = sAspxCsFile.Replace(".cs", "");

        ReguAspxFile(sAspxFile, sReguClassName);

    }

    void SaveToFile(string sFileName, string sFileContent)
    {

        byte[] btFileContent = Encoding.UTF8.GetBytes(sFileContent);

        FileUtil.WriteBytesToFile(sFileName, btFileContent);
    }




    protected void btnReguPageClassName_Click(object sender, EventArgs e)
    {
        ReguFile();
    }


    #endregion

}

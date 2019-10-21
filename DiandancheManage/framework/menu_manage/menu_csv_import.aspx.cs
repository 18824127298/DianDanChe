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

using System.Text;
using System.IO;

using Sigbit.Common;

using Sigbit.Framework;
using Sigbit.Net.CsvPacket;
using Sigbit.Framework;
using Sigbit.Net.CsvPacket.BatchIO;

public partial class genui_GUSU_charge_card_csv_export : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        lblLog.Text = "";

        //========== 1. 判断有无上传文件 ============
        if (FileUploadCSV.PostedFile.ContentLength == 0)
        {
            lblErrMessage.Text = "请选择待导入的文件";
            lblErrMessage.Visible = true;
            return;
        }

        //=========== 2. 将上传的文件保存到服务器端 ===========
        string sCSVFileName = CSVImportPathUtil.TempFilePathNameOfPrefix("menu");
        FileUploadCSV.PostedFile.SaveAs(sCSVFileName);

        //============= 3. 导入CSV文件 ==============
        CsvPacket csvPacket = new CsvPacket();
        csvPacket.ReadFromFile(sCSVFileName);

        for (int i = 1; i < csvPacket.GetRecordCount(); i++)
        {
            VerifyOneRecord(csvPacket, i);
        }
    }

    private void VerifyOneRecord(CsvPacket csvPacket, int nRecordNo)
    {
        TbSysMenu tblMenuImport = new TbSysMenu();

        tblMenuImport.MenuCode = csvPacket.GetItemString(nRecordNo, "menu_code");
        tblMenuImport.MenuName = csvPacket.GetItemString(nRecordNo, "menu_name");
        tblMenuImport.MenuLink = csvPacket.GetItemString(nRecordNo, "menu_link");
        tblMenuImport.MenuClass = csvPacket.GetItemString(nRecordNo, "menu_class");
        tblMenuImport.MenuStyle = csvPacket.GetItemString(nRecordNo, "menu_style");
        tblMenuImport.MenuIcon = csvPacket.GetItemString(nRecordNo, "menu_icon");
        tblMenuImport.ParentMenuCode = csvPacket.GetItemString(nRecordNo, "parent_menu_code");
        tblMenuImport.LevelNum = ConvertUtil.ToInt(csvPacket.GetItemString(nRecordNo, "level_num"));
        tblMenuImport.ListOrder = ConvertUtil.ToInt(csvPacket.GetItemString(nRecordNo, "list_order"));
        tblMenuImport.IsActive = csvPacket.GetItemString(nRecordNo, "is_active");
        tblMenuImport.HasChild = csvPacket.GetItemString(nRecordNo, "has_child");
        tblMenuImport.IsMenuItem = csvPacket.GetItemString(nRecordNo, "is_menu_item");
        tblMenuImport.IsLogItem = csvPacket.GetItemString(nRecordNo, "is_log_item");
        tblMenuImport.IsRightItem = csvPacket.GetItemString(nRecordNo, "is_right_item");
        tblMenuImport.Remarks = csvPacket.GetItemString(nRecordNo, "remarks");

        //========== 1. 是否存在菜单 ============
        TbSysMenu tblMenuExists = new TbSysMenu();
        tblMenuExists.MenuCode = tblMenuImport.MenuCode;
        if (!tblMenuExists.Fetch(true))
        {
            tblMenuImport.MenuCode = "<font color='red'>" + tblMenuImport.MenuCode;
            tblMenuImport.Remarks += "</font>";
        }
        else
        {
            //========== 2. 每个菜单的不同点 =============
            if (tblMenuImport.MenuName != tblMenuExists.MenuName)
                tblMenuImport.MenuName = "<font color='red'>" + tblMenuImport.MenuName + "</font>";
            if (tblMenuImport.MenuLink != tblMenuExists.MenuLink)
                tblMenuImport.MenuLink = "<font color='red'>" + tblMenuImport.MenuLink + "</font>";
            if (tblMenuImport.MenuClass != tblMenuExists.MenuClass)
                tblMenuImport.MenuClass = "<font color='red'>" + tblMenuImport.MenuClass + "</font>";
            if (tblMenuImport.MenuIcon != tblMenuExists.MenuIcon)
                tblMenuImport.MenuIcon = "<font color='red'>" + tblMenuImport.MenuIcon + "</font>";
            if (tblMenuImport.Remarks != tblMenuExists.Remarks)
                tblMenuImport.Remarks = "<font color='red'>" + tblMenuImport.Remarks + "</font>";
        }

        //=========== 3. 显示导入内容 ==========
        string sImportLineText = GetMenuDisplayLine(tblMenuImport);
        lblLog.Text += sImportLineText;
    }

    private string GetMenuDisplayLine(TbSysMenu tblMenu)
    {
        StringBuilder sbRet = new StringBuilder();
        sbRet.Append(tblMenu.MenuCode + "|");
        sbRet.Append(tblMenu.MenuName + "|");
        sbRet.Append(tblMenu.MenuLink + "|");
        sbRet.Append(tblMenu.MenuClass + "|");
        sbRet.Append(tblMenu.MenuIcon + "|");
        sbRet.Append(tblMenu.Remarks);
        sbRet.Append("<br />\r\n");
        
        return sbRet.ToString();
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        lblLog.Text = "";

        //========== 1. 判断有无上传文件 ============
        if (FileUploadCSV.PostedFile.ContentLength == 0)
        {
            lblErrMessage.Text = "请选择待导入的文件";
            lblErrMessage.Visible = true;
            return;
        }

        //=========== 2. 将上传的文件保存到服务器端 ===========
        string sCSVFileName = CSVImportPathUtil.TempFilePathNameOfPrefix("menu");
        FileUploadCSV.PostedFile.SaveAs(sCSVFileName);

        //============= 3. 导入CSV文件 ==============
        CsvPacket csvPacket = new CsvPacket();
        csvPacket.ReadFromFile(sCSVFileName);

        for (int i = 1; i < csvPacket.GetRecordCount(); i++)
        {
            ImportOneRecord(csvPacket, i);
        }
    }

    private void ImportOneRecord(CsvPacket csvPacket, int nRecordNo)
    {
        TbSysMenu tblMenuImport = new TbSysMenu();

        tblMenuImport.MenuCode = csvPacket.GetItemString(nRecordNo, "menu_code");
        tblMenuImport.MenuName = csvPacket.GetItemString(nRecordNo, "menu_name");
        tblMenuImport.MenuLink = csvPacket.GetItemString(nRecordNo, "menu_link");
        tblMenuImport.MenuClass = csvPacket.GetItemString(nRecordNo, "menu_class");
        tblMenuImport.MenuStyle = csvPacket.GetItemString(nRecordNo, "menu_style");
        tblMenuImport.MenuIcon = csvPacket.GetItemString(nRecordNo, "menu_icon");
        tblMenuImport.ParentMenuCode = csvPacket.GetItemString(nRecordNo, "parent_menu_code");
        tblMenuImport.LevelNum = ConvertUtil.ToInt(csvPacket.GetItemString(nRecordNo, "level_num"));
        tblMenuImport.ListOrder = ConvertUtil.ToInt(csvPacket.GetItemString(nRecordNo, "list_order"));
        tblMenuImport.IsActive = csvPacket.GetItemString(nRecordNo, "is_active");
        tblMenuImport.HasChild = csvPacket.GetItemString(nRecordNo, "has_child");
        tblMenuImport.IsMenuItem = csvPacket.GetItemString(nRecordNo, "is_menu_item");
        tblMenuImport.IsLogItem = csvPacket.GetItemString(nRecordNo, "is_log_item");
        tblMenuImport.IsRightItem = csvPacket.GetItemString(nRecordNo, "is_right_item");
        tblMenuImport.Remarks = csvPacket.GetItemString(nRecordNo, "remarks");

        tblMenuImport.Remarks = "";

        //========== 1. 是否存在菜单 ============
        TbSysMenu tblMenuExists = new TbSysMenu();
        tblMenuExists.MenuCode = tblMenuImport.MenuCode;
        if (!tblMenuExists.Fetch(true))
        {
            tblMenuImport.Remarks = "IMPORT-NEW-" + DateTimeUtil.Now;
            tblMenuImport.Insert();
        }
        else
        {
            tblMenuImport.Remarks = "";

            //========== 2. 每个菜单的不同点 =============
            if (tblMenuImport.MenuName != tblMenuExists.MenuName)
                tblMenuImport.Remarks += "【menu_name】" + tblMenuExists.MenuName;
            if (tblMenuImport.MenuLink != tblMenuExists.MenuLink)
                tblMenuImport.Remarks += "【menu_link】" + tblMenuExists.MenuLink;
            if (tblMenuImport.MenuClass != tblMenuExists.MenuClass)
                tblMenuImport.Remarks += "【menu_class】" + tblMenuExists.MenuClass;
            if (tblMenuImport.MenuIcon != tblMenuExists.MenuIcon)
                tblMenuImport.Remarks += "【menu_icon】" + tblMenuExists.MenuIcon;

            if (tblMenuImport.Remarks != "")
            {
                tblMenuImport.Remarks = "IMPORT-UPDATE-" + DateTimeUtil.Now + tblMenuImport.Remarks;
                tblMenuImport.Update();
            }
        }

        //=========== 3. 显示导入内容 ==========
        if (tblMenuImport.Remarks != "")
        {
            string sImportLineText = GetMenuDisplayLine(tblMenuImport);
            lblLog.Text += sImportLineText;
        }
    }


}

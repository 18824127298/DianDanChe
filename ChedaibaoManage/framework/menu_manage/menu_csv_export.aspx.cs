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
using Sigbit.Data;
using Sigbit.Framework;
using Sigbit.Net.CsvPacket;
using Sigbit.Net.CsvPacket.BatchIO;

public partial class genui_GUSU_charge_card_csv_export : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        string sSQL = "select * from sbt_sys_menu";

        CsvPacket csv = new CsvPacket();
      
        csv.AddField(TbSysMenuF.MenuCode);
        csv.AddField(TbSysMenuF.MenuName);
        csv.AddField(TbSysMenuF.MenuLink);
        csv.AddField(TbSysMenuF.MenuClass);
        csv.AddField(TbSysMenuF.MenuStyle);
        csv.AddField(TbSysMenuF.MenuIcon);
        csv.AddField(TbSysMenuF.ParentMenuCode);
        csv.AddField(TbSysMenuF.LevelNum);
        csv.AddField(TbSysMenuF.ListOrder);
        csv.AddField(TbSysMenuF.IsActive);
        csv.AddField(TbSysMenuF.HasChild);
        csv.AddField(TbSysMenuF.IsMenuItem);
        csv.AddField(TbSysMenuF.IsLogItem);
        csv.AddField(TbSysMenuF.IsRightItem);
        csv.AddField(TbSysMenuF.Remarks);


        DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            TbSysMenu tblMenu = new TbSysMenu();
            tblMenu.AssignByDataRow(ds, i);

            csv.SetItemString(i + 1, TbSysMenuF.MenuCode, tblMenu.MenuCode);
            csv.SetItemString(i + 1, TbSysMenuF.MenuName, tblMenu.MenuName);
            csv.SetItemString(i + 1, TbSysMenuF.MenuLink, tblMenu.MenuLink);
            csv.SetItemString(i + 1, TbSysMenuF.MenuClass, tblMenu.MenuClass);
            csv.SetItemString(i + 1, TbSysMenuF.MenuStyle, tblMenu.MenuStyle);
            csv.SetItemString(i + 1, TbSysMenuF.MenuIcon, tblMenu.MenuIcon);
            csv.SetItemString(i + 1, TbSysMenuF.ParentMenuCode, tblMenu.ParentMenuCode);
            csv.SetItemString(i + 1, TbSysMenuF.LevelNum, tblMenu.LevelNum.ToString());
            csv.SetItemString(i + 1, TbSysMenuF.ListOrder, tblMenu.ListOrder.ToString());
            csv.SetItemString(i + 1, TbSysMenuF.IsActive, tblMenu.IsActive);
            csv.SetItemString(i + 1, TbSysMenuF.HasChild, tblMenu.HasChild);
            csv.SetItemString(i + 1, TbSysMenuF.IsMenuItem, tblMenu.IsMenuItem);
            csv.SetItemString(i + 1, TbSysMenuF.IsLogItem, tblMenu.IsLogItem);
            csv.SetItemString(i + 1, TbSysMenuF.IsRightItem, tblMenu.IsRightItem);
            csv.SetItemString(i + 1, TbSysMenuF.Remarks, tblMenu.Remarks);

        }

        string sFileName = "C://temp/menu.csv";
        csv.WriteToFile(sFileName);

        Response.Buffer = true;
        Response.Charset = "GB2312";
        Response.ContentType = "application/ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" +
            System.Web.HttpUtility.UrlEncode(sFileName, System.Text.Encoding.UTF8));
        Response.TransmitFile(sFileName);
        Response.End();

    }
}

﻿using System;
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

public partial class module_uploadMenuIcon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetNoStore();
        if (IsPostBack)
        {
            return;
        }
        string sPath = Request["path"];
        ViewState["path"] = sPath;
        string sWidth = Request["width"];
        string sHeight = Request["height"];
        string sColumns = Request["columns"];
        string sFilePath = this.Request.PhysicalPath.Replace("module\\uploadMenuIcon.aspx", "")+"images\\menu_icon";
        DirectoryInfo dInfo = new DirectoryInfo(sFilePath);
        DataSet ds = new DataSet();
        DataTable table = new DataTable();
        table.Columns.Add("filepath");
        foreach (FileInfo fInfo in dInfo.GetFiles())
        {
            if (fInfo.Extension.ToLower() == ".gif" || fInfo.Extension.ToLower() == ".jpg")
            {
                string sImageUrl = "../images/menu_icon/" + fInfo.Name;
                object[] values ={ sImageUrl };
                table.Rows.Add(values);
            }
        }

        ds.Tables.Add(table);
        dlImages.DataSource = ds;
        dlImages.RepeatColumns = Convert.ToInt32(sColumns);
        dlImages.DataBind();
    }
}
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
using Sigbit.Framework;
using Sigbit.Framework.SubSystem;
using Sigbit.Framework.SubSystem.DBDefine;
using Sigbit.Framework.License;
using Sigbit.Framework.Patch.ScheduleEngine;

public partial class farmwork_homepage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SbtPatchScheduleEngine.Instance.DoPatchEngineWork();

        Response.Cache.SetNoStore();

        if (IsPostBack)
            return;


        lblSystemName.Text = SbtLicIDFeaturesConfig.Instance.SystemFullName;
        this.Title = SbtLicIDFeaturesConfig.Instance.SystemFullName;

        Application["SystemName"] = SbtLicIDFeaturesConfig.Instance.SystemBriefName;

        if (ConvertUtil.ToString(Request["log_out"]) == "Y")
        {
            Session.Abandon();
            Session["currentUserMEFTMG"] = null;
        }

        if (Session["currentUserMEFTMG"] != null)
        {
            SbtUser user = Session["currentUserMEFTMG"] as SbtUser;
            ltCurrentUser.Text = "当前用户：" + user.RealName;
            hlkLogout.NavigateUrl = "homepage.aspx?log_out=Y";
            hlkLogout.Text = "注销用户";
            hlkLogout.Style[HtmlTextWriterStyle.TextDecoration] = "underline";
        }

        LoadSubSystem();
    }


    protected void LoadSubSystem()
    {
        DataTable dtSubSystem = new DataTable();
        dtSubSystem.Columns.Add("system_id");
        dtSubSystem.Columns.Add("system_name");
        dtSubSystem.Columns.Add("bgcolor");

        for (int i = 0; i < SUSSubSystem.PoolSubSystem.SubSystemCount; i++)
        {
            TbSysSubSystemDefine tblSubSystem = SUSSubSystem.PoolSubSystem.GetSubSystemRec(i);

            if (tblSubSystem.DisplayOrder <= 0)
                continue;

            dtSubSystem.Rows.Add(tblSubSystem.SubSystemId, tblSubSystem.SubSystemName, tblSubSystem.SubSystemColor);

        }

        rptSubSystemList.DataSource = dtSubSystem;
        rptSubSystemList.DataBind();
    }


    protected string VIVSubSystemGrid(string sSubSystemID)
    {
        ////<div class="grid" style="background-color:red">
        //                            <a href="admin.aspx?sub_sys=<%# Eval("system_id") %>">
        //                                <img src="images/homepage/ivr.gif" class="grid-image" />
        //                                <span class="module_title"><%# Eval("system_name") %></span></a>
        //                        </div>

        bool bIsActive = true;
        if (Session["currentUserMEFTMG"] != null)
        {
            SbtUser user = Session["currentUserMEFTMG"] as SbtUser;
            SbtMenuNode rootMenuNode = user.MainMenuRootNode;
            rootMenuNode.MenuSetName = sSubSystemID;

            if (rootMenuNode.ChildNodes.Count == 0)
            {
                bIsActive = false;
            }
        }

        TbSysSubSystemDefine tblSubSystem = SUSSubSystem.PoolSubSystem.GetSubSystemRecBySubSystemID(sSubSystemID);

        string sBgColor = tblSubSystem.SubSystemColor;

        if (sBgColor == "")
            sBgColor = "1eb3b9";

        string sRetGridHtml = "";


        string sShowImage = tblSubSystem.HomepageGraph;
        if (sShowImage == "")
            sShowImage = tblSubSystem.SubSystemId + ".gif";

        string sShowTitle = tblSubSystem.HomepageCaption;
        if (sShowTitle == "")
            sShowTitle = tblSubSystem.SubSystemName;


        if (bIsActive)
        {

            sRetGridHtml = @"<div class='grid' style='background-color:" + sBgColor + @"'>
                            <a href='admin.aspx?sub_sys=" + sSubSystemID + @"'><img src='images/homepage/"
                                                             + sShowImage + "' class='grid-image' /><span class='module_title' style='cursor:hand;'>"
                                                             + sShowTitle + "</span></a></div>";
        }
        else
        {
            sRetGridHtml = @"<div class='grid' style='background-color:gray'>
                                <img src='images/homepage/" + sShowImage + "' class='grid-image' /><span class='module_title'>"
                                                             + sShowTitle + "</span></div>";
        }

        return sRetGridHtml;

    }


    /// <summary>
    /// 子系统访问加载
    /// </summary>
    protected void ShowSubSytemGrid()
    {
        //============ 1.读取所有子系统 =============
        for (int i = 0; i < SUSSubSystem.PoolSubSystem.SubSystemCount; i++)
        {

            TbSysSubSystemDefine tblSubSystem = SUSSubSystem.PoolSubSystem.GetSubSystemRec(i);


            bool bIsActive = true;
            if (Session["currentUserMEFTMG"] != null)
            {
                SbtUser user = Session["currentUserMEFTMG"] as SbtUser;
                SbtMenuNode rootMenuNode = user.MainMenuRootNode;
                rootMenuNode.MenuSetName = tblSubSystem.SubSystemId;

                if (rootMenuNode.ChildNodes.Count == 0)
                {
                    bIsActive = false;
                }
            }


            int nGridLocation = i + 1;

            Literal ltlCurrent = FindControl("ltl" + nGridLocation.ToString("00") + "Grid") as Literal;

            if (ltlCurrent != null)
            {
                ltlCurrent.Text = GenGridSingleRowText(tblSubSystem.SubSystemId, bIsActive, tblSubSystem.SubSystemName, tblSubSystem.SubSystemColor);
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sSubSystemID"></param>
    /// <param name="bIsActive"></param>
    /// <param name="sShowText"></param>
    /// <param name="sBgColor"></param>
    /// <returns></returns>
    protected string GenGridSingleRowText(string sSubSystemID, bool bIsActive, string sShowText, string sBgColor)
    {
        if (!bIsActive)
            sBgColor = "gray";


        string sGenRetText = "";

        if (bIsActive)
        {
            sGenRetText = "<td bgcolor='" + sBgColor + @"' class='grid'>
                            <a href='../../../framework/admin/admin.aspx?sub_sys=" + sSubSystemID + @"'>
                                <img src='../images/homepage/" + sSubSystemID + @".gif' class='grid-image' />
                                <span class='module_title'>" + sShowText + @"</span></a>
                           </td>";
        }
        else
        {
            sGenRetText = "<td bgcolor='" + sBgColor + @"' class='grid'>
                                <img src='../images/homepage/" + sSubSystemID + @".gif' class='grid-image' />
                                <span class='module_title'>" + sShowText + @"</span>
                           </td>";
        }
        return sGenRetText;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sSubSystemID"></param>
    /// <param name="bIsActive"></param>
    /// <param name="sShowText"></param>
    /// <param name="sBgColor"></param>
    /// <returns></returns>
    protected string GenGridSingleColumnText(string sSubSystemID, bool bIsActive, string sShowText, string sBgColor)
    {
        string sGenRetText = "";

        if (bIsActive)
        {
            sGenRetText = "<td bgcolor='" + sBgColor + @"' class='grid'>
                            <a href='../../../framework/admin/admin.aspx?sub_sys=" + sSubSystemID + @"'>
                                <img src='../images/homepage/" + sSubSystemID + @".gif' class='grid-image' /><br/>
                                <span class='module_title'>" + sShowText + @"</span></a>
                           </td>";
        }
        else
        {
            sGenRetText = @"<td bgcolor='gray' class='grid'>
                                <img src='../images/homepage/" + sSubSystemID + @".gif' class='grid-image' /><br/>
                                <span class='module_title'>" + sShowText + @"</span>
                           </td>";
        }
        return sGenRetText;
    }

}

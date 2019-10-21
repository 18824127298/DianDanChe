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

using Sigbit.Common;
using Sigbit.Framework;
using Sigbit.Framework.Role;

public partial class framework_organize_role_right_edit : SbtPageBase
{
    private string _currentRoleUid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string sRoleUid = ConvertUtil.ToString(Request["rol_uid"]);
        if (sRoleUid == "")
            return;

        _currentRoleUid = sRoleUid;
        hidSBMXRoleUid.Value = sRoleUid;

        TbRole tblRole = new TbRole();
        tblRole.RoleUid = sRoleUid;
        tblRole.Fetch();

        lblTitle.Text = "编辑角色权限 — （" + tblRole.RoleName + "）";

        ShowLiteralRightCBX();
    }

    /// <summary>
    /// 显示权限的复选框
    /// </summary>
    private void ShowLiteralRightCBX()
    {
        string sLiteralText = "";

        SbtMenuNode rootNode = new SbtMenuNode();
        rootNode.MenuCode = "";
        rootNode.MenuLevel = 0;
        rootNode.MenuNavigateMethod = SbtMenuNaviMeth.PopedomOfRole;
        //rootNode.MenuSetName = "main_menu";
        rootNode.MenuSetName = "";     //WKMN - 将MenuSet置空，可以取出所有菜单

        SbtMenuNodeList firstNodes = rootNode.ChildNodes;
        for (int i = 0; i < firstNodes.Count; i++)
        {
            sLiteralText += GetCBXTableTDText(firstNodes.GetNode(i));
        }

        literalRightCBX.Text = sLiteralText;
    }

    private string GetCBXTableTDText(SbtMenuNode firstNode)
    {
        StringBuilder sbContent = new StringBuilder();

        //========= 1. 将一级节点形成标题 ==============
        sbContent.AppendLine("<td valign=\"top\">");
        sbContent.AppendLine("  <table border=\"0\" cellspacing=\"1\" class=\"contentTable\" "
                + "cellpadding=\"3\" align=\"center\">");
        sbContent.AppendLine("  <thead>");
        sbContent.AppendLine("    <tr>");
        sbContent.AppendLine("      <td nowrap>");
        sbContent.AppendLine("        <input type=\"checkbox\" name=\"" + "SBMX" + firstNode.MenuCode 
                + "\" onClick=\"check_all(this,'" + "SBMX" + firstNode.MenuCode + "');\">");
        sbContent.AppendLine("        <img src=\"../../images/menu_icon/" + firstNode.MenuIcon 
                + "\">&nbsp;&nbsp;" + firstNode.ChineseName);
        sbContent.AppendLine("      </td>");
        sbContent.AppendLine("    </tr>");
        sbContent.AppendLine("  </thead>");

        //=========== 2. 得到二级节点 =================
        SbtMenuNodeList secondNodes = firstNode.ChildNodes;
        for (int i = 0; i < secondNodes.Count; i++)
        {
            string sSecondNodeText = GetSecondNodeText(firstNode, secondNodes.GetNode(i));
            sbContent.Append(sSecondNodeText);
        }

        //=========== x. 表格收尾 =================
        sbContent.AppendLine("  </table>");
        sbContent.AppendLine("</td>");

        return sbContent.ToString();
    }

    private string GetSecondNodeText(SbtMenuNode firstNode, SbtMenuNode secondNode)
    {
        StringBuilder sbContent = new StringBuilder();

        sbContent.AppendLine("    <tr>");
        sbContent.AppendLine("      <td nowrap>");

        string sCheckedString = "";
        if (SbtRoleRight__Lib.Instance.RoleHasPopedom(_currentRoleUid, secondNode.MenuCode))
            sCheckedString = " checked";

        sbContent.AppendLine("        <input type=\"checkbox\" name=\"" + "SBMX" + firstNode.MenuCode
                + "\" value=\"" + "SBMX" + secondNode.MenuCode + "\"" + sCheckedString + ">");
        sbContent.Append("        <img src=\"../../images/menu_icon/" + secondNode.MenuIcon
                + "\">&nbsp;&nbsp;" + secondNode.ChineseName);

        string sIndentText = GetIndentNodeText(firstNode, secondNode);
        sbContent.Append(sIndentText);

        sbContent.AppendLine();
        sbContent.AppendLine("      </td>");
        sbContent.AppendLine("    </tr>");

        return sbContent.ToString();
    }

    private string GetIndentNodeText(SbtMenuNode firstNode, SbtMenuNode indentParentNode)
    {
        StringBuilder sbContent = new StringBuilder();

        SbtMenuNodeList childNodes = indentParentNode.ChildNodes;
        for (int i = 0; i < childNodes.Count; i++)
        {
            SbtMenuNode childNode = childNodes.GetNode(i);
            sbContent.AppendLine("<br />");

            string sIndentSpace = StringUtil.RepeatChar("&nbsp;", (childNode.MenuLevel - 2) * 4);

            string sCheckedString = "";
            if (SbtRoleRight__Lib.Instance.RoleHasPopedom(_currentRoleUid, childNode.MenuCode))
                sCheckedString = " checked";

            sbContent.AppendLine("        " + sIndentSpace + "<input type=\"checkbox\" name=\"" 
                    + "SBMX" + firstNode.MenuCode + "\" value=\"" + "SBMX" + childNode.MenuCode + "\""
                    + sCheckedString + ">");
            sbContent.Append("        <img src=\"../../images/menu_icon/" + childNode.MenuIcon
                    + "\">&nbsp;&nbsp;" + childNode.ChineseName);

            string sChildSubsText = GetIndentNodeText(firstNode, childNode);
            if (sChildSubsText != "")
                sbContent.Append(sChildSubsText);
        }

        return sbContent.ToString();
    }
}

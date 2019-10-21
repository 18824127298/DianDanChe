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

using Sigbit.Framework;


public partial class farmwork_main_function_panel_menu_operation : SbtPageBase
{
    protected string _bodyTemplate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        _bodyTemplate = SbtTheme.GetThemeTemplate(CurrentUser.ThemePreference, "fpanel_menu_operation");

        

    }
}

/*
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu_operation.aspx.cs" Inherits="farmwork_main_function_panel_menu_operation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>菜单选项</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<style type="text/css">
BODY 
{
	MARGIN: 0px 0px 0px 5px; BACKGROUND-COLOR: #dbe0d9
}
BODY 
{
	COLOR: #ffffff
}
TD 
{
	COLOR: #ffffff
}
TH 
{
	COLOR: #ffffff
}
</style>

    <script>


// <!--屏蔽鼠标右键开始-->
if (window.Event)
  document.captureEvents(Event.MOUSEUP);

function nocontextmenu()
{
 event.cancelBubble = true
 event.returnValue = false;

 return false;
}

function norightclick(e)
{
 if (window.Event)
 {
  if (e.which == 2 || e.which == 3)
   return false;
 }
 else
  if (event.button == 2 || event.button == 3)
  {
   event.cancelBubble = true
   event.returnValue = false;
   return false;
  }

}
document.oncontextmenu = nocontextmenu;  // for IE5+     
document.onmousedown = norightclick;     // for all others    
// <!--屏蔽鼠标右键结束-->

    </script>

    <meta content="MSHTML 6.00.2900.3059" name="GENERATOR">
    <link href="../../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <table class="Small" height="90" cellspacing="0" cellpadding="0" border="0">
        <tbody>
            <tr>
                <td id="menu_6" title="收藏夹" style="cursor: hand" valign="top"
                    align="middle" width="26" background="../../../theme_images/main/menu/index_21.gif">
                    <table height="25" cellspacing="0" cellpadding="0" width="15" border="0">
                        <tbody>
                            <tr>
                                <td width="10">
                                    &nbsp;</td>
                            </tr>
                        </tbody>
                    </table>
                    收<br>
                    藏<br>
                    夹</td>
                <td width="4">
                    &nbsp;</td>
                <td id="menu_4" title="快捷方式" style="cursor: hand" valign="top"
                    align="middle" width="26" background="../../../theme_images/main/menu/index_21.gif">
                    <table height="14" cellspacing="0" cellpadding="0" width="15" border="0">
                        <tbody>
                            <tr>
                                <td width="10">
                                    &nbsp;</td>
                            </tr>
                        </tbody>
                    </table>
                    快<br>
                    捷<br>
                    方<br>
                    式</td>
                <td width="4">
                    &nbsp;</td>
                <td id="menu_1" title="主菜单" style="cursor: hand" valign="top"
                    align="middle" width="26" background="../../../theme_images/main/menu/index_21.gif">
                    <table height="14" cellspacing="0" cellpadding="0" width="15" border="0">
                        <tbody>
                            <tr>
                                <td width="10">
                                    &nbsp;</td>
                            </tr>
                        </tbody>
                    </table>
                    功<br>
                    能<br>
                    菜<br>
                    单</td>
                <td width="4">
                    &nbsp;</td>
                <td id="menu_2" title="在线人员" style="cursor: hand" valign="top"
                    align="middle" width="26" background="../../../theme_images/main/menu/index_21.gif">
                    <table height="14" cellspacing="0" cellpadding="0" width="15" border="0">
                        <tbody>
                            <tr>
                                <td width="10">
                                    &nbsp;</td>
                            </tr>
                        </tbody>
                    </table>
                    在<br>
                    线<br>
                    人<br>
                    员</td>
                <td width="4">
                    &nbsp;</td>
                <td id="menu_3" title="全部人员" style="cursor: hand" valign="top"
                    align="middle" width="26" background="../../../theme_images/main/menu/index_21.gif">
                    <table height="14" cellspacing="0" cellpadding="0" width="15" border="0">
                        <tbody>
                            <tr>
                                <td width="10">
                                    &nbsp;</td>
                            </tr>
                        </tbody>
                    </table>
                    全<br>
                    部<br>
                    人<br>
                    员</td>
                <td width="4">
                    &nbsp;</td>
                <td id="menu_5" title="短信箱" style="cursor: hand" valign="top"
                    align="middle" width="26" background="../../../theme_images/main/menu/index_21.gif">
                    <table height="25" cellspacing="0" cellpadding="0" width="15" border="0">
                        <tbody>
                            <tr>
                                <td width="10">
                                    &nbsp;</td>
                            </tr>
                        </tbody>
                    </table>
                    短<br>
                    信<br>
                    箱</td>
            </tr>
        </tbody>
    </table>
    <br>
    <br>
    <!-- 状态图片预载 -->
    <span class="small">
        <img src="../../../theme_images/main/menu/index_21-2.gif">
        <img src="../../../theme_images/main/menu/index_21-3.gif">
    </span>
</body>
</html>
 */

/*
 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu_operation.aspx.cs" Inherits="farmwork_main_function_panel_menu_operation" %>

<!--DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>菜单选项</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta content="MSHTML 6.00.2900.3059" name="GENERATOR">
</head>
<%=_bodyTemplate %>
</html>
*/
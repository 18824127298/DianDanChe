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

public partial class farmwork_main_status_bar : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Text1.Value = Application["NumVisits"].ToString();
    }
}

/* --------------- 原aspx -----------------
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="status_bar.aspx.cs" Inherits="farmwork_main_status_bar" %>

<!--DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <SCRIPT language=JavaScript>
//============================== 普通界面调用函数 ====================================
function killErrors()
{
  return true;
}
window.onerror = killErrors;

function show_online()
{
   parent.function_panel.view_menu(2);
}

function main_refresh()
{
   parent.table_index.table_main.location.reload();
}
</SCRIPT>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" marginheight="0" marginwidth="0">
<div class="statusBar">
    <table class="small" height="20" cellspacing="0" cellpadding="0" width="100%" background="../../theme_images/main/menu/sb_bg.gif"
        border="0">
        <tbody>
            <tr>
                <td width="6">
                    <img height="20" src="../../theme_images/main/menu/sb_bg.gif" width="6"></td>
                <td style="cursor: hand" align="left" width="85">
                    <a class="sb_online_text" title="点击这里显示在线用户" onclick="javascript:show_online();">共<input
                        class="sb_online_num" readonly size="3" name="user_count1">人在线</a></td>
                <td style="color: #ffffff" align=center width="38">
                    <span id="new_sms"></span>
                </td>
                <td style="color: #ffffff" align=center width="57">
                    <span id="new_leter"></span>
                </td>
                <td class="sb_center_text" title="点击这里刷新主操作区" 
                    nowrap align=center>
                    技术提高效率&nbsp;科技创造未来</td>
                <td valign= middle align=center width="91">
                    <a style="color: #d50000" href="" target="table_main">
                        <b></b></a></td>
                <td align="right" width="25">
                    <a href="javascript:history.back()">
                        <img height="16" alt="后退" src="../../theme_images/main/menu/arrow_back.gif" width="16" border="0"></a></td>
                <td align="right" width="25">
                    <a href="javascript:history.forward()">
                        <img height="16" alt="前进" src="../../theme_images/main/menu/arrow_forward.gif" width="16" border="0"></a></td>
                <td nowrap align="right" width="25">
                    <a href="" target="_blank">
                        <img height="16" alt="使用帮助" src="../../images/help.gif" width="16" border="0"></a></td>
                <td nowrap align="right" width="25">
                    <a title="显示软件版权信息" href="" target="table_main">
                        <img height="16" alt="版权信息" src="../../images/i_about.gif" width="16" border="0"></a></td>
                <td width="6">
                    &nbsp;</td>
            </tr>
        </tbody>
    </table>
</div>
    <iframe name="ref_new_letter" src="" width="0"
        height="0"></iframe>
    <iframe name="ref_sms" src="" width="0" height="0"></iframe>
</body>
</html>
 */


/* -------------------- 黄城 ----------------
<link rel="stylesheet" type="text/css" href="/theme/3/style.css">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<title>底部状态条</title>

<SCRIPT LANGUAGE="JavaScript">
//============================== 普通界面调用函数 ====================================
function killErrors()
{
  return true;
}
window.onerror = killErrors;

var ctroltime;

function MyLoad()
{
  setTimeout("ref_new_letter1()",30000);
  ctroltime=setTimeout("ref_sms1()",3000);
}

function ref_new_letter1()
{
  ref_new_letter.location.reload();
  setTimeout("ref_new_letter1()",900000);
}

function ref_sms1()
{
  ctroltime=setTimeout("ref_sms1()",35000);
  ref_sms.location="ref_sms.php";
}

//------------------------------------
function show_sms()
{
   clearTimeout(ctroltime);
   ctroltime=window.setTimeout('ref_sms1()',45000);

   mytop=screen.availHeight-190;
   myleft=0;
   window.open("sms_show.php","auto_call_show","height=150,width=350,status=0,toolbar=no,menubar=no,location=no,scrollbars=no,top="+mytop+",left="+myleft+",resizable=yes");
}

function show_email()
{
   parent.table_index.table_main.location="/function/email/inbox?BOX_ID=0";
}

function show_online()
{
  // parent.callleftmenu.leftmenu_open();
   parent.function_panel.view_menu(2);
}

function main_refresh()
{
   parent.table_index.table_main.location.reload();
}

//-------------------- 菜单窗口控制 -----------------------
menu_flag=0;
var STATUS_BAR_MENU;

function show_menu()
{
   mytop=screen.availHeight-480;
   myleft=screen.availWidth-215;
   if(menu_flag==0)
       STATUS_BAR_MENU=window.open("/function/function_panel/menu.php?OA_SUB_WINDOW=1","STATUS_BAR_MENU","height=400,width=200,status=0,toolbar=no,menubar=no,location=no,scrollbars=yes,top="+mytop+",left="+myleft+",resizable=no");

   STATUS_BAR_MENU.focus();
}

function MyUnload()
{
   if(menu_flag==1)
   {
     STATUS_BAR_MENU.focus();
     STATUS_BAR_MENU.MAIN_CLOSE=1;
     STATUS_BAR_MENU.close();
   }
}
</script>



</head>
<body onselectstart="return false" topmargin="0" leftmargin="0" marginwidth="0" marginheight="0" onLoad="MyLoad();">  

<table width="100%" height="20" border="0" cellpadding="0" cellspacing="0" background="/theme/3/images/sb_bg.gif" class="small">
  <tr>
    <td width="6"><img src="/theme/3/images/sb_bg.gif" width="6" height="20"></td>
    <td width="85" align="left" style="cursor:hand"><a  title="点击这里显示在线用户" onclick='javascript:show_online();' class="sb_online_text" >共<input name="user_count1" class="sb_online_num" value="" size="3" readonly>人在线</a></td>
	<td width="38" align="center" style="color:#ffffff;"><span id="new_sms"></span></td>
	<td width="57" align="center" style="color:#ffffff;"><span id="new_leter"></span></td>
	
	<td align="center" nowrap title="点击这里刷新主操作区" class="sb_center_text" onClick="javascript:parent.table_index.table_main.location.reload();">技术提高效率&nbsp;科技创造未来 －  软件注册前还可试用<b><font color=#FF0000>26</font></b>天</td>
    <td width="91" align="center" valign="middle" nowrap ><a href="/include/register.php" target="table_main" style="color:D50000"><b>软件注册</b></a></td>
    <td width="25" align="right" > <a href="javascript:history.back()"><img src="../../theme/3/images/arrow_back.gif" alt="后退" width="16" height="16" border="0"></a></td>
	<td width="25" align="right" ><a href="javascript:history.forward()"><img src="../../theme/3/images/arrow_forward.gif" alt="前进" width="16" height="16" border="0"></a></td>
	<td width="25" align="right" nowrap ><a href="../../help/" target="_blank"><img src="../../theme/3/images/help.gif" alt="使用帮助" width="16" height="16" border="0"></a></td>
	<td width="25" align="right" nowrap ><a href="/function/about.php" target="table_main" title="显示软件版权信息" ><img src="../../theme/3/images/i_about.gif" alt="版权信息" width="16" height="16" border="0"></a></td>   
	<td width="6" >&nbsp;</td>

  </tr>
</table>

<iframe name="ref_new_letter" src="/function/status_bar/ref_new_letter.php" width="0" height="0"></iframe>
<iframe name="ref_sms" src="" width="0" height="0"></iframe>
</body>
</html>
 */
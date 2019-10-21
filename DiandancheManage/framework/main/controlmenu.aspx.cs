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

public partial class farmwork_main_controlmenu : SbtPageBase
{
    protected string _pageTemplate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        _pageTemplate = SbtTheme.GetThemeTemplate(CurrentUser.ThemePreference, "controlmenu");
    }
}

/*
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="controlmenu.aspx.cs" Inherits="farmwork_main_controlmenu" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>菜单显隐控制条</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">

    <script language="JavaScript">
	var AUTO_HIDE_MENU=0;

var arrowpic1="../../theme_images/main/menu/index_24-1.jpg";
var arrowpic2="../../theme_images/main/menu/index_24-2.jpg";

//--------------------- 状态初始 ----------------------
var MENU_SWITCH;
function panel_menu_open()
{
 MENU_SWITCH=AUTO_HIDE_MENU;
 panel_menu_ctrl();
}


//------------------ 面板状态切换 ---------------------
function panel_menu_ctrl()
{
   if(MENU_SWITCH==0)
   {
      parent.frame2.cols="2,186,20,*,8";
      MENU_SWITCH=1;
      arrow.src=arrowpic1;
   }
   else
   {
      parent.frame2.cols="0,0,20,*,8";
      MENU_SWITCH=0;
      arrow.src=arrowpic2;
   }
}


//------------------ 面板状态切换 ---------------------
function enter_menu_ctrl()
{
   if(AUTO_HIDE_MENU==1)    // 判断面板是否允许自动隐藏
   {
     if(MENU_SWITCH==0)
     {
        parent.frame2.cols="2,186,20,*,8";
        MENU_SWITCH=1;
        arrow.src=arrowpic1;
     }
     else
     {
        parent.frame2.cols="0,0,20,*,8";
        MENU_SWITCH=0;
        arrow.src=arrowpic2;
     }
   }
}


//--------------- 上下框架页显示控制 -----------------
var DB_VIEW=0;                          // 状态值初始
var DB_rows=parent.parent.frame1.rows;  // 保存原始值
function DB_Display() 
{
   if (DB_VIEW==0)     // 未隐藏
   {
    parent.parent.frame1.rows="0,*,0";
	DB_VIEW=1;
   }
   else                // 已隐藏
   {
    parent.parent.frame1.rows=DB_rows;   
    DB_VIEW=0;
   }
}
    </script>

    <style type="text/css">
    BODY 
    {
        BACKGROUND-COLOR: #f0f9f0
    }
</style>
    <meta content="MSHTML 6.00.2900.3059" name="GENERATOR">
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body oncontextmenu="DB_Display();return false;" onselectstart="return false" leftmargin="0"
    topmargin="0" onload="panel_menu_open()">
    <table onmousemove="enter_menu_ctrl()" height="100%" cellspacing="0" cellpadding="0"
        width="20" border="0">
        <tbody>
            <tr>
                <td valign="top" height="56">
                    <img height="56" alt="" src="../../theme_images/main/menu/index_11.jpg" width="20"></td>
            </tr>
            <tr style="cursor: hand" onclick="panel_menu_ctrl()">
                <td background="../../theme_images/main/menu/index_27.jpg">
                    &nbsp;
                </td>
            </tr>
            <tr style="cursor: hand" onclick="panel_menu_ctrl()">
                <td height="59">
                    <img id="arrow" height="59" alt="左键点击控制菜单栏面板，右键点击控制上下状态栏。" src="../../theme_images/main/menu/index_24-1.jpg"
                        width="20" galleryimg="no"></td>
            </tr>
            <tr style="cursor: hand" onclick="panel_menu_ctrl()">
                <td background="../../theme_images/main/menu/index_27.jpg">
                    &nbsp;</td>
            </tr>
            <tr>
                <td valign="bottom" height="46">
                    <img id="DBarrow" height="46" src="../../theme_images/main/menu/index_29.jpg" width="20"></td>
            </tr>
        </tbody>
    </table>
</body>
</html>
 */
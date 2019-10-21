<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mainframe.aspx.cs" Inherits="farmwork_main_mainframe" %>

<!-- DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" -->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=Application["SystemName"] %></title>
    <link href="../../css/style.css" type="text/css" rel="stylesheet">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">

    <script language="JavaScript">
//-------------------- 防止出错 ---------------------------
function killErrors()
{
  return true;
}
window.onerror = killErrors;

//------------------- 窗口最大化 --------------------------
self.moveTo(0,0);                                  <!-- 将当前窗口缩小为 -->
//self.resizeTo(1024,screen.availHeight); <!-- 将当前窗口设置为屏幕大小 -->
self.resizeTo(screen.availWidth,screen.availHeight); <!-- 将当前窗口设置为屏幕大小 -->
self.focus();    


// 状态栏显示文字
window.defaultStatus=""; 
    </script>

    <meta content="MSHTML 6.00.2900.3059" name="GENERATOR">
</head>
<%=_frameSetTemplate %>
</html>

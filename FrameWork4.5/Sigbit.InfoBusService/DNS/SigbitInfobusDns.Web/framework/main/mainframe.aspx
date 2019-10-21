<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mainframe.aspx.cs" Inherits="framework_main_mainframe" %>

<!-- DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" -->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=Application["SystemName"] %></title>
    <link href="../../css/style.css" type="text/css" rel="stylesheet">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">

    <script language="JavaScript">
//-------------------- ��ֹ���� ---------------------------
function killErrors()
{
  return true;
}
window.onerror = killErrors;

//------------------- ������� --------------------------
self.moveTo(0,0);                                  <!-- ����ǰ������СΪ -->
//self.resizeTo(1024,screen.availHeight); <!-- ����ǰ��������Ϊ��Ļ��С -->
self.resizeTo(screen.availWidth,screen.availHeight); <!-- ����ǰ��������Ϊ��Ļ��С -->
self.focus();    


// ״̬����ʾ����
window.defaultStatus=""; 
    </script>

    <meta content="MSHTML 6.00.2900.3059" name="GENERATOR">
</head>
<%=_frameSetTemplate %>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu_info.aspx.cs" Inherits="farmwork_main_function_panel_menu_info" %>

<!--DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>��¼��Ϣ��</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <style type="text/css">BODY {
	MARGIN: 0px
}
.style1 {
	FONT-FAMILY: "����"
}

.small {
font-size:9pt;
}
</style>

    <script language="JavaScript">

// ------ ��ʱˢ��ҳ�� -------
//window.setTimeout('this.location.reload();',1050000);  <!-- ��ʱˢ��  -->


// <!--��������Ҽ���ʼ-->
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
// <!--��������Ҽ�����-->

    </script>

    <meta content="MSHTML 6.00.2900.3059" name="GENERATOR">
    <!--link href="../../../css/style.css" rel="stylesheet" type="text/css" /-->
</head>
<%=_bodyTemplate %>
</html>

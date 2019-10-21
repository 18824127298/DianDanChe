<%@ Page Language="C#" AutoEventWireup="true" CodeFile="table_top.aspx.cs" Inherits="framework_main_table_index_table_top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>���涥����</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <style type="text/css">
    BODY 
    {
	    MARGIN: 0px; 
    }
</style>

    <script language="JavaScript">

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

    <!--  -->
    <meta content="MSHTML 6.00.2900.3059" name="GENERATOR">
</head>
<%=_bodyTemplate %>
</html>

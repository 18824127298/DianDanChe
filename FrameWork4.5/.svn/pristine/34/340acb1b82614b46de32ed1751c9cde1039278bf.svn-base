<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin.aspx.cs" Inherits="framework_admin_admin" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户登陆</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    
        <script language="JavaScript">
//-------------------- 防止出错 ---------------------------
function killErrors()
{
  return true;
}
window.onerror = killErrors;

//------------------- 窗口最大化 --------------------------
self.moveTo(0,0);                                  <!-- 将当前窗口缩小为 -->
self.resizeTo(screen.availWidth,screen.availHeight); <!-- 将当前窗口设置为屏幕大小 -->
self.focus();    


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
</head>
<body scroll="no" onselectstart="return false" background="images/adminback.jpg">
    <form id="form1" runat="server">
        <table id="Table4" cellspacing="0" cellpadding="0" width="100%" height="100%" border="0">
            <tr valign="middle">
                <td>
                    <p>
                        <table id="Table1" height="412" cellspacing="0" cellpadding="0" width="399" align="center"
                            border="0">
                            <tr>
                                <td valign="middle" align="right" background="images/admin.jpg" runat="server" id="tdMainInput">
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <table class="small" id="Table2" style="margin-top: 10px;" cellspacing="8"
                                        cellpadding="0" width="205" align="center" border="0">
                                        <tr>
                                            <td align="right" width="60">
                                                <b><font color="#0077b2">用户名：</font></b></td>
                                            <td align="left">
                                                <font face="宋体">
                                                    <asp:TextBox ID="edtUserName" Width="120px" runat="server"></asp:TextBox></font></td>
                                        </tr>
                                        <tr>
                                            <td align="right" width="60">
                                                <b><font color="#0077b2">密 &nbsp;码：</font></b></td>
                                            <td align="left">
                                                <font face="宋体">
                                                    <asp:TextBox ID="edtPassword" Width="120px" runat="server" TextMode="Password"></asp:TextBox></font></td>
                                        </tr>
                                    </table>
                                    <table id="Table3" style="margin-top: 5px" cellspacing="6" cellpadding="0" width="205"
                                        align="center" border="0">
                                        <tr>
                                            <td>
                                                <font face="宋体">&nbsp;</font>
                                                <asp:ImageButton ID="imgbtnAdmin" runat="server" ImageUrl="~/framework/admin/images/button_03.gif" OnClick="imgbtnAdmin_Click">
                                                </asp:ImageButton><font face="宋体">&nbsp;&nbsp;&nbsp;&nbsp; </font>
                                                <asp:ImageButton ID="imgbtnReset" runat="server" ImageUrl="~/framework/admin/images/button_05.gif" OnClick="imgbtnReset_Click">
                                                </asp:ImageButton></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </p>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin.aspx.cs" Inherits="farmwork_admin_Admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>��ӭʹ��</title>
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
self.resizeTo(screen.availWidth,screen.availHeight); <!-- ����ǰ��������Ϊ��Ļ��С -->
self.focus();    

// ״̬����ʾ����
window.defaultStatus=""; 

//��½
function Admin()
{
    window.open("../main/mainframe.aspx");
 //   window.open('../main/mainframe.aspx?username="+USERNAME+"',600,800);
    window.opener = null;
    window.close();
}


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

    <style type="text/css">
        body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
    </style>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body scroll="no" onselectstart="return false" style="height: 100%;">
    <form name="form1" method="post" runat="server">
    <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 100%; height: 490px; background-image: url(images/admin_bg.gif)">
                <table width="630px" height="490px" border="0" align="center" cellpadding="0" cellspacing="0"
                    style="background-image: url(images/admin_box_bg.jpg);">
                    <tr>
                        <td style="width: 630px; height: 490px; text-align: center; vertical-align: top;">
                            <table width="300px" border="0" cellspacing="8" cellpadding="0" style="margin-top: 220px;"
                                align="center">
                                <tr>
                                    <td height="32" style="text-align: center; color: #4bb2df; font-family: ΢���ź�,����,Arial,sans-serif;
                                        font-size: 24px; font-weight: bold;">
                                        <asp:Label ID="lblSystemName" runat="server" Text="˼�ش��ۺ�Ӧ�ù���ƽ̨" />
                                    </td>
                                </tr>
                            </table>
                            <table width="300px" border="0" cellspacing="8" cellpadding="0" class="small" align="center">
                                <tr>
                                    <td width="96" align="right">
                                        <b><font color="#4bb2df">�û�����</font></b>
                                    </td>
                                    <td align="left">
                                        &nbsp;<asp:TextBox ID="edtUserName" runat="server" CssClass="SmallInput" Width="12em"
                                            BorderColor="#4bb2df" ForeColor="#4bb2df"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="96" align="right">
                                        <b><font color="#4bb2df">��&nbsp; &nbsp;�룺</font></b>
                                    </td>
                                    <td align="left">
                                        &nbsp;<asp:TextBox ID="edtPassword" runat="server" CssClass="SmallInput" Width="12em"
                                            BorderColor="#4bb2df" ForeColor="#4bb2df" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table width="300px" border="0" cellspacing="8" cellpadding="0" align="center">
                                <tr>
                                    <td height="32" align="right">
                                        <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="images/admin_login.gif" OnClick="btnLogin_Click"
                                            AlternateText="��¼" />
                                        &nbsp;
                                        <asp:ImageButton ID="btnRefill" runat="server" ImageUrl="images/admin_reset.gif"
                                            OnClick="btnRefill_Click" />
                                    </td>
                                </tr>
                            </table>
                            <table width="300px" border="0" cellspacing="0" cellpadding="0" align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLoginErrorMsg" runat="server" Font-Size="9pt" ForeColor="Red" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <div style="position: absolute; height: 15pt; width: 100%; left: 0; bottom: 0;">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="small">
            <tr valign="bottom">
                <td width="80" align="center" valign="middle" nowrap>
                    <img src="../../images/sethome.gif" width="16" height="16" border="0" align="absbottom">
                    <a href="#" target="_self" style="cursor: hand" onclick="this.style.behavior='url(#default#homepage)';this.setHomePage(self.location);return false;">
                        <font color="#4bb2df"><u>��Ϊ��ҳ</u></font></a>
                </td>
                <td align="center" valign="middle" nowrap>
                    <font color="#4bb2df">��ӭʹ��<a href="http://www.sigbit.com" target="_blank"><font color="#4bb2df">˼�ش��ۺ�Ӧ��ƽ̨</font></a>����ϵͳ�Ƽ�������IE8.0��</font>
                </td>
                <td width="80" align="center" valign="middle" nowrap>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

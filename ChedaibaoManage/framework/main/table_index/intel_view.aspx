<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="intel_view.aspx.cs"
    Inherits="farmwork_main_table_index_intel_view1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的办公桌</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <%--<meta http-equiv="refresh"   content="20" />
    <script language="JavaScript">
        window.setTimeout('this.location.reload();',600000);
    </script>--%>
    <meta content="MSHTML 6.00.2900.3059" name="GENERATOR">
    <link href="../../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="1" style="padding: 0 0 0 0">
    <form id="frmMain" runat="server">
    <table cellspacing="0" height="100%" width="100%" border="0" bordercolorlight="#000000"
        bordercolordark="#FFFFFF" cellpadding="0">
        <tr valign="middle">
            <td align="center">
                <%--<img src="images/desktop.jpg">--%>
                <asp:Image ID="imgDesktop" runat="server" ImageUrl="images/desktop.png" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

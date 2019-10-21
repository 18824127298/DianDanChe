<%@ Page Language="C#" AutoEventWireup="true" Theme="green-bamboo" CodeFile="preference_setting.aspx.cs" Inherits="framework_ctrlpanel_test" %>

<!--DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<script>

function GoDesktop()
{
    if (<%=_refreshDesktop %>)
        parent.parent.location="../main/mainframe.aspx";
}
</script>
<body topmargin="5" onload="GoDesktop()">
    <form id="form1" runat="server">
    <div>

    <div class="titleTable">
    <table border="0" width="100%" cellspacing="0" cellpadding="3">
      <tr>
        <td>
            <img src="../../images/menu_icon/attendance.gif" WIDTH="16" HEIGHT="16" align="absmiddle">
            <span>个人喜好设置</span>
        </td>
      </tr>
    </table>
    <hr>
    </div>
    
    <div class="contentTable">
    <table border="0" width="450" cellpadding="4" cellspacing="1" align="center">
    <tbody>
        <tr>
          <td nowrap>选择语言偏好：</td>
          <td>
            <asp:DropDownList ID="ddlbLanguage" runat="server" Width="180px">
            </asp:DropDownList>
          </td>
        </tr>
        <tr>
          <td nowrap>选择页面风格：</td>
          <td>
            <asp:DropDownList ID="ddlbTheme" runat="server" Width="180px">
            </asp:DropDownList>
          </td>
        </tr>
    </tbody>
    <tfoot>
        <tr align="center"> 
            <td colspan="2" nowrap>
                <asp:Button ID="btnOK" runat="server" Text="确定" CssClass="normalButton" OnClick="btnOK_Click" />
            </td>
        </tr>
    </tfoot>
    </table>
    </div>
    

    </div>
    </form>
</body>
</html>

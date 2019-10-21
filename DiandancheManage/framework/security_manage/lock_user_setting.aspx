<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lock_user_setting.aspx.cs" Inherits="framework_security_manage_lock_user_setting" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>用户锁定设置</title>
</head>
<body topmargin="5">
    <form id="form1" runat="server">
    <div>

    <div class="titleTable">
    <table border="0" width="100%" cellspacing="0" cellpadding="3">
      <tr>
        <td>
            <img src="../../images/menu_icon/lock.gif" WIDTH="16" HEIGHT="16" align="absmiddle">
            <span>用户锁定设置</span>
        </td>
      </tr>
    </table>
    <hr>
    </div>
    <br />
    <div class="contentTable">
    <table border="0" width="400" cellpadding="6" cellspacing="1" align="center">
    <thead>
    <tr><td>用户锁定设置</td></tr>
    </thead>
    <tbody>
        <tr>
          <td>
              <asp:CheckBox ID="cbxLockEnabled" runat="server" Text="启用自动锁定用户功能" AutoPostBack="True" OnCheckedChanged="cbxLockEnabled_CheckedChanged" /><br />
                  &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblLockHint1" runat="server" Text="当重复输入错误密码次数超过" Enabled="false">
                  </asp:Label>&nbsp;<asp:TextBox ID="edtLockTimes" runat="server" Width="30px" Enabled="false">
                  </asp:TextBox>&nbsp;<asp:Label ID="lblLockHint2" runat="server" Text="次后，自动锁定用户"  Enabled="false">
                  </asp:Label><br /><br />
              <asp:CheckBox ID="cbxUnlockEnabled" runat="server" Text="启用自动解锁用户功能（仅对自动锁定的用户有效）" AutoPostBack="True" OnCheckedChanged="cbxUnlockEnabled_CheckedChanged" /><br />
                  &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblUnlockHint1" runat="server" Text="自动解锁时间为" Enabled="false">
                  </asp:Label>&nbsp;<asp:TextBox ID="edtUnlockMinutes" runat="server" Width="30px" Enabled="false">
                  </asp:TextBox>&nbsp;<asp:Label ID="lblUnlockHint2" runat="server" Text="分钟"  Enabled="false">
                  </asp:Label><br />
        </tr>
    </tbody>
    <tfoot>
        <tr align="center"> 
            <td nowrap>
                <asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDefaultValue" runat="server" Text="恢复默认设置" OnClick="btnDefaultValue_Click" />
            </td>
        </tr>
    </tfoot>
    </table>
    </div>
        <table width="300" align="center">
        <tr><td>&nbsp;<asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td></tr>
        </table>
    </div>
    </form>
</body>
</html>

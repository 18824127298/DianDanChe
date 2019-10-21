<%@ Page Language="C#" AutoEventWireup="true" CodeFile="password_strength_setting.aspx.cs" Inherits="mdtseat_chat_log_config_log_config_setting" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>密码强度设置</title>
</head>
<body topmargin="5">
    <form id="form1" runat="server">
    <div>

    <div class="titleTable">
    <table border="0" width="100%" cellspacing="0" cellpadding="3">
      <tr>
        <td>
            <img src="../../images/menu_icon/hammer.gif" WIDTH="16" HEIGHT="16" align="absmiddle">
            <span>密码强度设置</span>
        </td>
      </tr>
    </table>
    <hr>
    </div>
    
    <div class="contentTable">
    <table border="0" width="300" cellpadding="6" cellspacing="1" align="center">
    <thead>
    <tr><td>密码强度设置</td></tr>
    </thead>
    <tbody>
        <tr>
          <td>
              <asp:RadioButton ID="rbLevel0" runat="server" Text="零级" Font-Bold="true" GroupName="PasswordLevel" /><br />
              &nbsp; &nbsp; &nbsp; 密码允许为空<br />
              <asp:RadioButton ID="rblevel1" runat="server" Text="一级" Font-Bold="true" GroupName="PasswordLevel" /><br />
              &nbsp; &nbsp; &nbsp; 密码不能为空<br />
              <asp:RadioButton ID="rbLevel2" runat="server" Text="二级" Font-Bold="true" GroupName="PasswordLevel" /><br />
              &nbsp; &nbsp; &nbsp; 密码长度至少6位，且满足两种组合<br />
              <asp:RadioButton ID="rblevel3" runat="server" Text="三级" Font-Bold="true" GroupName="PasswordLevel" /><br />
              &nbsp; &nbsp; &nbsp; 密码长度至少6位，且满足三种组合<br />
              <asp:RadioButton ID="rbLevel4" runat="server" Text="四级" Font-Bold="true" GroupName="PasswordLevel" /><br />
              &nbsp; &nbsp; &nbsp; 密码长度至少6位，且满足四种组合<br />
              <font color="gray">（组合是指数字、小写字母、大写字母、其它字符的组合）</font><br /><br />
              <asp:CheckBox ID="cbxModiPasswordIfLower" runat="server" Text="低于密码强度时，登录后要求先修改密码" />
        </tr>
    </tbody>
    <tfoot>
        <tr align="center"> 
            <td nowrap>
                <asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" />
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

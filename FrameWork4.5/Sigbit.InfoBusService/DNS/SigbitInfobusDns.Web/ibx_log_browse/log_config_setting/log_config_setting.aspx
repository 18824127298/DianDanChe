<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_config_setting.aspx.cs" Inherits="mdtseat_chat_log_config_log_config_setting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body topmargin="5">
    <form id="form1" runat="server">
    <div>

    <div class="titleTable">
    <table border="0" width="100%" cellspacing="0" cellpadding="3">
      <tr>
        <td>
            <img src="../../images/menu_icon/note.gif" WIDTH="16" HEIGHT="16" align="absmiddle">
            <span>设置总线消息日志记录规则</span>
        </td>
      </tr>
    </table>
    <hr>
    </div>
    
    <div class="contentTable">
    <table border="0" width="350" cellpadding="6" cellspacing="1" align="center">
    <thead>
    <tr><td>设置总线消息日志记录规则</td></tr>
    </thead>
    <tbody>
        <tr>
          <td nowrap>
              <asp:CheckBox ID="cbxLogInDB" runat="server" AutoPostBack="True" OnCheckedChanged="cbxLogInDB_CheckedChanged"
                  Text="记录通信消息日志" /><br />
              &nbsp; &nbsp; &nbsp; &nbsp; 通信消息日志保存
              <asp:TextBox ID="edtClearBeforeHours" runat="server" Width="40px"></asp:TextBox>
              小时</td>
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

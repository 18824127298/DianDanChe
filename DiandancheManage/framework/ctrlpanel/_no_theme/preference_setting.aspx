<%@ Page Language="C#" AutoEventWireup="true" CodeFile="preference_setting.aspx.cs" Inherits="framework_ctrlpanel_preference_setting" %>

<!--!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link rel="stylesheet" type="text/css" href="pref_style.css">
</head>
<body class="bodycolor" topmargin="5">
    <form id="form1" runat="server">
    <div>
    <table border="0" width="100%" cellspacing="0" cellpadding="3" class="small">
      <tr>
        <td class="Big">
            <img src="../../../images/menu_icon/attendance.gif" WIDTH="16" HEIGHT="16" align="absmiddle">
            <span class="big3"> 个人喜好设置</span>
        </td>
      </tr>
    </table>
    <hr class="hr1" >
    <table border="0" width="450" cellpadding="2" cellspacing="1" align="center" bgcolor="#000000" class="small">
        <tr>
          <td nowrap class="TableData">选择语言偏好：</td>
          <td class="TableData">
            <asp:DropDownList ID="ddlbLanguage" runat="server" Width="180px">
                <asp:ListItem Value="zh-cn">简体中文</asp:ListItem>
                <asp:ListItem Value="tw-cn">繁体中文</asp:ListItem>
                <asp:ListItem Value="us-en">English</asp:ListItem>
            </asp:DropDownList>
          </td>
        </tr>
        <tr>
          <td nowrap class="TableData">选择页面风格：</td>
          <td class="TableData">
            <asp:DropDownList ID="ddlbTheme" runat="server" Width="180px">
            <asp:ListItem Value="green-bamboo">绿竹青青掩映</asp:ListItem>
            <asp:ListItem Value="blue-comfort">简约蓝调舒适</asp:ListItem>
            </asp:DropDownList>
          </td>
        </tr>
        <tr align="center" class="TableControl"> 
            <td colspan="2" nowrap>
                <asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" CssClass="BigButton" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu_import_tool.aspx.cs" Inherits="framework_ctrlpanel_test" %>

<!--DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>

<body topmargin="5" onload="GoDesktop()">
    <form id="form1" runat="server">
    <div>

    <div class="titleTable">
    <table border="0" width="100%" cellspacing="0" cellpadding="3">
      <tr>
        <td>
            <img src="../../images/menu_icon/salary.gif" WIDTH="16" HEIGHT="16" align="absmiddle">
            用户菜单配置
        </td>
      </tr>
    </table>
    <hr>
    </div>

    <br>
    <table width="75%" align="center">
    <tr>
      <td valign="top">　　完成菜单的导入工作。准备的数据每一行以TAB分隔，分别为<br />
          菜单中文名，菜单标识，空，图标，链接</td>
    </tr>
    </table>
    <br>

    <table width="75%" align="center">
    <tr>
      <td valign="top">
          导入的数据：<br />
          <asp:TextBox ID="memoImport" runat="server" Rows="8" TextMode="MultiLine" Width="534px" SkinID="multiLine"></asp:TextBox><br />
          <asp:Button ID="btnImport" runat="server" Text="导入" OnClick="btnImport_Click" /></td>
    </tr>
    </table>


    <br>
    <br>

<div runat="server" id="divResult" visible="false">
    &nbsp;</div>    

<div runat="server" id="divError" visible="false" align="center">
    &nbsp;</div>

    </div>
    </form>
</body>
</html>

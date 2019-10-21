<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu_csv_import.aspx.cs" Inherits="genui_GUSU_charge_card_csv_export" %>

<!--DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>导入菜单</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img align="absMiddle" height="16" src="../../images/menu_icon/salary.gif" width="16" />
                            导入菜单</td>
                    </tr>
                </table>
                <hr />
            </div>
                <br />
                <br />
                <table align="center" border="0" cellpadding="4" cellspacing="1" width="550px">
                <tr>
                <td>
                <p><b>说明：</b>导入的CSV文件中菜单信息。导入过程只导入菜单中的增量部分。</p>
                </td>
                </tr>
                </table>
                <br/>

            <div class="contentTable">
                <table align="center" border="0" cellpadding="8" cellspacing="1" width="450px">
                    <tbody>
                        <tr>
                            <td nowrap="nowrap" style="text-align: center;">
                                导入CSV文件：</td>
                            <td>
                                &nbsp;<asp:FileUpload ID="FileUploadCSV" runat="server" Width="300px" /></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap="nowrap">
                                <asp:Button ID="btnVerify" runat="server" onclick="btnVerify_Click" 
                                    Text="校验菜单项" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click"
                                    Text="导入菜单项" />
                                </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <br />
        <table width="400" align="center">
        <tr><td>&nbsp;<asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td></tr>
        </table>
    
    <div style="text-align: left">
        <asp:Label ID="lblLog" runat="server" Text=""></asp:Label>
    </div>
    </div>
    </form>
</body>
</html>

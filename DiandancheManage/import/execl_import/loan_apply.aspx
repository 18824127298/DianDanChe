<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loan_apply.aspx.cs" Inherits="import_execl_import_loan_apply" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/key2.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="借款批量导入"></asp:Label>
                        </td>
                    </tr>
                </table>
                <hr />
            </div>
            <div class="contentTable">
                <br />
                <table border="0" width="450" cellpadding="8" cellspacing="1" align="center">
                    <thead>
                        <tr>
                            <td colspan="2" align="center">借款批量导入
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td align="right" style="width: 25%">文件导入：
                            </td>
                            <td>&nbsp;
                            <asp:FileUpload ID="fulUpload" runat="server" />
                            </td>
                        </tr>
                       <%-- <tr>
                            <td align="right">文件示例：
                            </td>
                            <td>&nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="会员批量导入.csv">下载示例文件</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">数据导入内容：
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="姓名 手机号"></asp:Label>
                            </td>
                        </tr>--%>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap>&nbsp;<asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" />
                                &nbsp; &nbsp; &nbsp;
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <br />
            <table width="450" align="center">
                <tr>
                    <td>
                        <label id="lblCount"></label>
                        <asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="" Visible="true"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>

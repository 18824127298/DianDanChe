<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dept_new.aspx.cs" Inherits="framework_organize_dept_new" %>

<html>
<head runat="server">
    <title>新建部门信息</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/diary.gif" />
                            新建部门/成员单位 - 当前节点：[<asp:Label ID="lblDeptName" runat="server" Text="当前节点名"></asp:Label>]</td>
                    </tr>
                </table>
                <hr />
            </div>
            <div class="contentTable">
                <table border="0" width="450" cellpadding="4" cellspacing="1" align="center">
                    <tbody>
                        <tr>
                            <td nowrap>
                                部门名称：</td>
                            <td>
                                &nbsp;<asp:TextBox ID="edtDeptName" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td nowrap>
                                顺序号：</td>
                            <td>
                                &nbsp;<asp:TextBox ID="edtListOrder" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td nowrap>
                                备注：</td>
                            <td>
                                &nbsp;<asp:TextBox ID="edtRemarks" runat="server" Width="300px" Height="100px" TextMode="MultiLine" SkinID="multiLine"></asp:TextBox></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap>
                                &nbsp;<asp:Button ID="btnOK" runat="server" Text="新建部门" OnClick="btnOK_Click" /></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <br />
            <table width="450" align="center">
                <tr>
                    <td>
                        &nbsp;<asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

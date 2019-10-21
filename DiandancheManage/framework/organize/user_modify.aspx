<%@ Page Language="C#" AutoEventWireup="true" CodeFile="user_modify.aspx.cs" Inherits="genui_CRPN_user_modify" %>

<html>
<head runat="server">
    <title>�û�����</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/netchat.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="�û�����"></asp:Label>
                    </td>
                </tr>
            </table>
            <hr />
        </div>
        <div class="contentTable">
            <br />
            <table border="0" width="450" cellpadding="4" cellspacing="1" align="center">
                <tbody>
                    <tr>
                        <td nowrap>
                            �ʺţ�
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtUserName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            ������
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtRealName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            ���룺
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtPassword" runat="server" Width="200px">***** ***** ***** *****</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            ���ڵ��У�
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtAreaName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            �̶��绰��
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtTelephone" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            �ƶ��绰��
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtMobilePhone" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            ��ϵ���䣺
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtEmail" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            �Ա�
                        </td>
                        <td>
                            &nbsp;<asp:DropDownList ID="ddlbSex" runat="server" Width="50px">
                                <asp:ListItem Value="M">��</asp:ListItem>
                                <asp:ListItem Value="F">Ů</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            ��ɫ��
                        </td>
                        <td>
                            &nbsp;<asp:DropDownList ID="ddlbRole" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            ״̬��
                        </td>
                        <td>
                            &nbsp;<asp:DropDownList ID="ddlbIsActive" runat="server" Width="150px">
                                <asp:ListItem Value="Y">����</asp:ListItem>
                                <asp:ListItem Value="N">����</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlAdminArea" runat="server">
                        <tr>
                            <td nowrap>
                            </td>
                            <td>
                                &nbsp;<asp:CheckBox ID="cbxIsAdmin" runat="server" Text="��������Ա" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td nowrap>
                            ��ע��
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtRemarks" runat="server" Width="300px" Height="50px" SkinID="multiLine"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr align="center">
                        <td colspan="2" nowrap>
                            &nbsp;<asp:Button ID="btnOK" runat="server" Text="ȷ��" OnClick="btnOK_Click" />
                            &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="ȡ��" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
        <table width="450" align="center">
            <tr>
                <td>
                    &nbsp;<asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

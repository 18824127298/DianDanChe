<%@ Page Language="C#" AutoEventWireup="true" CodeFile="system_define_modify.aspx.cs"
    Inherits="genui_WSQV_system_define_modify" %>

<html>
<head runat="server">
    <title>ϵͳ�����޸�</title>

    <script>
       
function ChoiceColor()
{
	var arr = showModalDialog("selcolor.htm", window, "dialogWidth:300px; dialogHeight:300px; status:0; help:0");
	if (arr)
	{
//	    document.getElementById("lblSystemColor").style.color=arr;
        document.getElementById("tdHomeGraph").style.backgroundColor=arr;
        document.getElementById("hfSystemColor").value=arr;
	}
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfSystemColor" runat="server" />
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/comm.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="ϵͳ�����޸�"></asp:Label>
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
                            ϵͳ��ţ�
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtSubSystemId" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            ϵͳ��ƣ�
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtSubSystemName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            ϵͳ��ɫ��
                        </td>
                        <td>
                            <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="2" align="center" id="tdHomeGraph" runat="server">
                                        <asp:Image ID="imgHomepageGraph" runat="server" ImageUrl="~/framework/admin/images/homepage/ivr.gif"
                                            Width="40px" Height="40px" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlbHomepageGraph" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbHomepageGraph_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="button" value="��������" class="normalButton" onclick="ChoiceColor()" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            �������֣�
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtFullName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            Ӧ�����⣺
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtAppTheme" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            ��ҳ���⣺
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtHomepageCaption" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            ҳ������ı���
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtPageTitleText" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            ҳ�����ͼƬ��
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtPageTitleImage" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            �ı��˵�����
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtMenuRootText" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            ��ʾ˳��
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtDisplayOrder" runat="server" Width="200px" Text="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            ��ע��
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtRemarks" runat="server" Width="300px" Height="70px" SkinID="multiLine"
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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="system_define_modify.aspx.cs"
    Inherits="genui_WSQV_system_define_modify" %>

<html>
<head runat="server">
    <title>系统定义修改</title>

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
                        <asp:Label ID="lblTitle" runat="server" Text="系统定义修改"></asp:Label>
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
                            系统编号：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtSubSystemId" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            系统简称：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtSubSystemName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            系统颜色：
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
                                        <input type="button" value="背景更换" class="normalButton" onclick="ChoiceColor()" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            完整名字：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtFullName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            应用主题：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtAppTheme" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            首页标题：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtHomepageCaption" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            页面标题文本：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtPageTitleText" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            页面标题图片：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtPageTitleImage" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            文本菜单根：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtMenuRootText" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            显示顺序：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtDisplayOrder" runat="server" Width="200px" Text="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap>
                            备注：
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
                            &nbsp;<asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" />
                            &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="god_opera_log_list.aspx.cs" Inherits="genui_UZAY_opera_log_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>
<%@ Register Src="../../module/My97DatePicker.ascx" TagName="My97DatePicker" TagPrefix="uc1" %>
<html>
<head runat="server">
    <title>������־���</title>
</head>
<script language="javascript" src="../../js/grid_ui_func.js"></script>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img runat="server" id="imgIcon" src="../../images/menu_icon/notepad.gif" />
                            <asp:Label runat="server" ID="lblText" Text="������־���"></asp:Label></td>
                    </tr>
                </table>
                <hr />
            </div>

            <table cellspacing="0" cellpadding="4" width="100%" border="0" align="center">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
                            <tr>
                                <td align="left">
                                    �������ͣ�<asp:DropDownList ID="ddlbOperaType" runat="server" />
                                    &nbsp;&nbsp;����ʱ�䣺<uc1:My97DatePicker ID="dpFromTime" runat="server" Width="150px" ShowDateFmt="yyyy-MM-dd HH:mm:ss" />
                                    -<uc1:My97DatePicker ID="dpToTime" runat="server" Width="150px" ShowDateFmt="yyyy-MM-dd HH:mm:ss" />
                                    &nbsp;&nbsp;��־����:<asp:TextBox ID="edtContent" runat="server" />
                                    &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text=" ���� " OnClick="btnSearch_Click" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:GridView ID="gvList" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False" CellPadding="6">
                            <Columns>
                                <asp:TemplateField HeaderText="����ʱ��" SortExpression="CreateTime">
                                    <ItemStyle HorizontalAlign="Center" Width="13em" />
                                    <ItemTemplate>
                                        <%# VIVDateTime(Eval("CreateTime")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="������" SortExpression="OperaCode">
                                    <ItemStyle HorizontalAlign="Center" Width="7em" />
                                    <ItemTemplate>
                                       <%# Eval("OperaCode") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="������ʽ" SortExpression="OperaType">
                                    <ItemStyle HorizontalAlign="Center" Width="7em" />
                                    <ItemTemplate>
                                        <%# VIVOperaType(Eval("OperaType"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="������ַ" SortExpression="ClientAddress">
                                    <ItemStyle HorizontalAlign="Center" Width="8em" />
                                    <ItemTemplate>
                                        <%# Eval("ClientAddress") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Remark" HeaderText="������ע" SortExpression="Remark">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                            </Columns>
                            <PagerSettings Visible="False" />
                        </asp:GridView>
                    </td>
                </tr>

                <tr>
                    <td>
                        <uc2:GridViewPager ID="gridViewPager" runat="server" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="divSearchCondition" runat="server" Width="100%">
                <asp:Button ID="btnClearCondition" runat="server" Text="�������" OnClick="btnClearCondition_Click" />&nbsp;<asp:Label ID="lblConditionDesc" runat="server"
                    Text="Label"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

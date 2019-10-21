<%@ Page Language="C#" AutoEventWireup="true" CodeFile="opera_log_list.aspx.cs" Inherits="genui_UZAY_opera_log_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>
<%@ Register Src="../../module/My97DatePicker.ascx" TagName="My97DatePicker" TagPrefix="uc1" %>
<html>
<head runat="server">
    <title>日志列表</title>
</head>
<script language="javascript" src="../../js/grid_ui_func.js"></script>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img runat="server" id="imgIcon" src="../../images/menu_icon/attendance.gif" />
                            <asp:Label runat="server" ID="lblText" Text="日志列表"></asp:Label></td>
                    </tr>
                </table>
                <hr />
            </div>

            <table cellspacing="0" cellpadding="4" width="100%" border="0" align="center">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="95%" border="0" align="center">
                            <tr>
                                <td align="left">操作时间：<uc1:My97DatePicker ID="dpFromTime" runat="server" Width="150px" ShowDateFmt="yyyy-MM-dd HH:mm:ss" />
                                    -<uc1:My97DatePicker ID="dpToTime" runat="server" Width="150px" ShowDateFmt="yyyy-MM-dd HH:mm:ss" />
                                    &nbsp;&nbsp;日志内容:<asp:TextBox ID="edtContent" runat="server" />
                                    &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text=" 搜索 " OnClick="btnSearch_Click" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:GridView ID="gvList" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False" CellPadding="6">
                            <Columns>
                                <asp:TemplateField HeaderText="操作时间" SortExpression="CreateTime">
                                    <ItemStyle HorizontalAlign="Center" Width="13em" />
                                    <ItemTemplate>
                                        <%# VIVDateTime(Eval("CreateTime")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="操作方式" SortExpression="OperaType">
                        <ItemStyle HorizontalAlign="Center" Width="7em" />
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink2" runat="server" Text='<%# VIVOperaType(Eval("OperaType"))%>' >HyperLink</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                <asp:BoundField DataField="Remark" HeaderText="操作备注" SortExpression="Remark">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="操作地址" SortExpression="ClientAddress">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# Eval("ClientAddress") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="关联用户" SortExpression="GodId">
                                    <ItemStyle HorizontalAlign="Center" Width="13em" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# VIVGodName(Eval("GodId"))%>'>HyperLink</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                <asp:Button ID="btnClearCondition" runat="server" Text="清空条件" OnClick="btnClearCondition_Click" />&nbsp;<asp:Label ID="lblConditionDesc" runat="server"
                    Text="Label"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

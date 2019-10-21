<%@ Page Language="C#" AutoEventWireup="true" CodeFile="operation_log_browse_list.aspx.cs"
    Inherits="genui_PAIM_operation_log_browse_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>
<html>
<head runat="server">
    <title>系统维护日志管理</title>
</head>

<script language="javascript" src="../../js/grid_ui_func.js"></script>

<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/browse.gif" />
                        <asp:Label ID="lblTitle" runat="server" />--维护日志管理
                    </td>
                </tr>
            </table>
            <hr />
        </div>
        <table cellspacing="0" cellpadding="4" width="100%" border="0" align="center">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="90%" border="0" align="center">
                        <tr>
                            <td align="right">
                                &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text=" 搜索 " OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvList" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                        AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False" CellPadding="3">
                        <Columns>
                            <asp:TemplateField HeaderText="操作员" SortExpression="user_name">
                                <ItemStyle HorizontalAlign="Center" Width="5em" />
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("user_name")%>' NavigateUrl='<%# "operation_log_browse_modify.aspx?rec_key=" + Eval("log_uid")%>'>HyperLink</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="时间" SortExpression="proc_time">
                                <ItemStyle HorizontalAlign="Center" Width="7em" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# VIVProcTime(Eval("proc_time").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="proc_class_name" HeaderText="操作类型" SortExpression="proc_class_name">
                                <ItemStyle HorizontalAlign="Center" Width="7em" />
                            </asp:BoundField>
                            <asp:BoundField DataField="proc_subclass_name" HeaderText="子类型" SortExpression="proc_subclass_name">
                                <ItemStyle HorizontalAlign="Center" Width="7em" />
                            </asp:BoundField>
                            <asp:BoundField DataField="action_name" HeaderText="操作" SortExpression="action_name">
                                <ItemStyle HorizontalAlign="Center" Width="7em" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="操作描述" SortExpression="action_description">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# VIVActionDescription(Eval("action_description").ToString()) %>'></asp:Label>
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
            <asp:Button ID="btnClearCondition" runat="server" Text="清空条件" OnClick="btnClearCondition_Click" />&nbsp;<asp:Label
                ID="lblConditionDesc" runat="server" Text="Label"></asp:Label>
        </asp:Panel>
    </div>
    </form>
</body>
</html>

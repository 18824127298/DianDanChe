<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login_log_browse_list.aspx.cs" Inherits="genui_DITJ_login_log_browse_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head runat="server">
    <title>系统登录日志浏览</title>
</head>
    <script language="javascript" src="../../js/grid_ui_func.js"></script>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/inbox.gif" />
                        系统登录日志浏览</td>
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
                    &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text=" 搜索 " OnClick="btnSearch_Click" /></td>
            </tr>
            </table>
        </td>
        </tr>

        <tr>
        <td>
            <asp:GridView ID="gvList" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False" CellPadding="4" >
                <Columns>
                    <asp:TemplateField HeaderText="登录时间" SortExpression="login_time">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("login_time")%>' NavigateUrl='<%# "login_log_browse_modify.aspx?rec_key=" + Eval("login_log_uid")%>'>HyperLink</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="user_name" HeaderText="用户名" SortExpression="user_name" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="登录成功" SortExpression="is_login_success">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <img src="images/<%# VIVIsLoginSuccess(Eval("is_login_success").ToString()) %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="锁定" SortExpression="lock_operation">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <img src="images/<%# VIVLockOperationImage(Eval("lock_operation").ToString()) %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="login_fail_desc" HeaderText="失败原因" SortExpression="login_fail_desc" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="已退出" SortExpression="has_logout">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <img src="images/<%# VIVHasLogout(Eval("has_logout").ToString()) %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="logout_time" HeaderText="退出时间" SortExpression="logout_time" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="时长" SortExpression="in_system_duration">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# VIVInSystemDuration(Eval("in_system_duration").ToString()) %>'></asp:Label>
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

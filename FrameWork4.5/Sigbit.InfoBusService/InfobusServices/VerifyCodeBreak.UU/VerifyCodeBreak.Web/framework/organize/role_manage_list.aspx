<%@ Page Language="C#" AutoEventWireup="true" CodeFile="role_manage_list.aspx.cs"
    Inherits="genui_ETJJ_role_manage_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>
<html>
<head runat="server">
    <title>用户角色信息管理</title>

    <script language="javascript">
        function DeleteData()
        {
            var sSelectedIDs=jcomGetAllSelectedRecords();
            if(sSelectedIDs=="")
            {
                alert("请选择待删除的记录！");
                return;
            }
            if(!window.confirm("本操作将删除所有选择的数据记录。\n确认吗？"))
            {
                return;
            }
            location="role_manage_modify.aspx?del_rec=" + sSelectedIDs;
        }
    </script>

</head>

<script language="javascript" src="../../js/grid_ui_func.js"></script>

<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/netmeeting.gif" />
                            用户角色信息管理</td>
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
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" Text="新增角色" OnClick="btnAdd_Click" />
                                    &nbsp;&nbsp;&nbsp; &nbsp;
                                    <input id="btnDelete1" type="button" value="删除角色" class="normalButton"
                                        onclick="DeleteData()" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvList" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                            AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False" CellPadding="4">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <input id="chkSelect" type="checkbox" value='<%# Eval("role_uid") %>' />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <input id="chkSelectAll" type="checkbox" title="全选或全部取消" onclick="jcomSelectAllRecords()" />
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="2em" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="角色名称" SortExpression="role_name">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("role_name")%>' NavigateUrl='<%# "role_manage_modify.aspx?rec_key=" + Eval("role_uid")%>'>HyperLink</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="create_time" HeaderText="创建时间" SortExpression="create_time">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="操作">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hplnkEditRight" runat="server" NavigateUrl='<%# "~/framework/organize/role_right_edit.aspx?rol_uid=" + Eval("role_uid")%>'>编辑权限</asp:HyperLink>&nbsp; &nbsp;
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

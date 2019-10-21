<%@ Page Language="C#" AutoEventWireup="true" CodeFile="role_manage_list.aspx.cs"
    Inherits="framework_organize_role_manage_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>
<html>
<head runat="server">
    <title>�û���ɫ��Ϣ����</title>

    <script language="javascript">
        function DeleteData()
        {
            var sSelectedIDs=jcomGetAllSelectedRecords();
            if(sSelectedIDs=="")
            {
                alert("��ѡ���ɾ���ļ�¼��");
                return;
            }
            if(!window.confirm("��������ɾ������ѡ������ݼ�¼��\nȷ����"))
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
                            �û���ɫ��Ϣ����</td>
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
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" Text="������ɫ" OnClick="btnAdd_Click" />
                                    &nbsp;&nbsp;&nbsp; &nbsp;
                                    <input id="btnDelete1" type="button" value="ɾ����ɫ" class="normalButton"
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
                                        <input id="chkSelectAll" type="checkbox" title="ȫѡ��ȫ��ȡ��" onclick="jcomSelectAllRecords()" />
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="2em" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="��ɫ����" SortExpression="role_name">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("role_name")%>' NavigateUrl='<%# "role_manage_modify.aspx?rec_key=" + Eval("role_uid")%>'>HyperLink</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="create_time" HeaderText="����ʱ��" SortExpression="create_time">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="����">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hplnkEditRight" runat="server" NavigateUrl='<%# "~/framework/organize/role_right_edit.aspx?rol_uid=" + Eval("role_uid")%>'>�༭Ȩ��</asp:HyperLink>&nbsp; &nbsp;
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
                <asp:Button ID="btnClearCondition" runat="server" Text="�������" OnClick="btnClearCondition_Click" />&nbsp;<asp:Label
                    ID="lblConditionDesc" runat="server" Text="Label"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

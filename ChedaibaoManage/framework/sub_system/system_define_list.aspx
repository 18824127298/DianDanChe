<%@ Page Language="C#" AutoEventWireup="true" CodeFile="system_define_list.aspx.cs"
    Inherits="genui_WSQV_system_define_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>
<html>
<head runat="server">
    <title>ϵͳ�������</title>

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
            location="system_define_modify.aspx?del_rec=" + sSelectedIDs;
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
                        <img src="../../images/menu_icon/system.gif" />
                        ϵͳ�������
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
                                &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text=" ���� " OnClick="btnSearch_Click" />
                                &nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" Text=" ���� " OnClick="btnAdd_Click" />
                                &nbsp;&nbsp;<input id="btnDelete" type="button" value=" ɾ�� " class="normalButton"
                                    onclick="DeleteData()" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvList" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                        AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <input id="chkSelect" type="checkbox" value='<%# Eval("sub_system_id") %>' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <input id="chkSelectAll" type="checkbox" title="ȫѡ��ȫ��ȡ��" onclick="jcomSelectAllRecords()" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="2em" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ϵͳ���" SortExpression="sub_system_id">
                                <ItemStyle HorizontalAlign="Center" Width="8em" />
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("sub_system_id")%>'
                                        NavigateUrl='<%# "system_define_modify.aspx?rec_key=" + Eval("sub_system_id")%>'>HyperLink</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ϵͳ���" SortExpression="sub_system_name">
                                <ItemStyle HorizontalAlign="Center" Width="10em" />
                                <ItemTemplate>
                                    <%# Eval("sub_system_name").ToString() %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="��ҳ��ʾ" SortExpression="sub_system_color">
                                <ItemStyle HorizontalAlign="Center" Width="8em" Height="2em" />
                                <ItemTemplate>
                                    <span style='background-color: <%# Eval("sub_system_color") %>; width: 80px;height:50px;'><img src="../admin/images/homepage/<%# Eval("homepage_graph") %>" style="width:50px;height:50px;" border="0" /></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="full_name" HeaderText="ϵͳȫ��" SortExpression="full_name">
                                <ItemStyle HorizontalAlign="Center" Width="20em" />
                            </asp:BoundField>
                            <asp:BoundField DataField="remarks" HeaderText="��ע" SortExpression="remarks">
                                <ItemStyle HorizontalAlign="Center" />
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
            <asp:Button ID="btnClearCondition" runat="server" Text="�������" OnClick="btnClearCondition_Click" />&nbsp;<asp:Label
                ID="lblConditionDesc" runat="server" Text="Label"></asp:Label>
        </asp:Panel>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="break_algol_list.aspx.cs" Inherits="genui_AURP_break_algol_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head runat="server">
    <title>��֤���ƽ��㷨ά��</title>
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
            location="break_algol_modify.aspx?del_rec=" + sSelectedIDs;
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
                        <img src="../../images/menu_icon/key2.gif" />
                        ��֤���ƽ��㷨ά��</td>
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
                    &nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" Text=" ���� " OnClick="btnAdd_Click" />
                    &nbsp;&nbsp;<input id="btnDelete" type="button" value=" ɾ�� " class="normalButton" onclick="DeleteData()" /></td>
            </tr>
            </table>
        </td>
        </tr>

        <tr>
        <td>
            <asp:GridView ID="gvList" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" 
                    AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False" CellPadding="4" >
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input id="chkSelect" type="checkbox" value='<%# Eval("algol_id") %>' />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <input id="chkSelectAll" type="checkbox" title="ȫѡ��ȫ��ȡ��" onclick="jcomSelectAllRecords()" />
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="2em" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�㷨��ʶ" SortExpression="algol_id">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("algol_id")%>' NavigateUrl='<%# "break_algol_modify.aspx?rec_key=" + Eval("algol_id")%>'>HyperLink</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="algol_name" HeaderText="�㷨����" SortExpression="algol_name" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="algol_desc" HeaderText="�㷨����" SortExpression="algol_desc" >
                        <ItemStyle Width="33%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="algol_data_01" HeaderText="����һ" SortExpression="algol_data_01" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="algol_data_02" HeaderText="���ݶ�" SortExpression="algol_data_02" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="algol_data_03" HeaderText="������" SortExpression="algol_data_03" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="algol_data_04" HeaderText="������" SortExpression="algol_data_04" >
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
            <asp:Button ID="btnClearCondition" runat="server" Text="�������" OnClick="btnClearCondition_Click" />&nbsp;<asp:Label ID="lblConditionDesc" runat="server"
                Text="Label"></asp:Label>
        </asp:Panel>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dns_trans_code_list.aspx.cs" Inherits="genui_LNWX_dns_trans_code_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head runat="server">
    <title>�����������</title>
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
            location="dns_trans_code_modify.aspx?del_rec=" + sSelectedIDs;
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
                        <img src="../../images/menu_icon/plug.gif" />
                        �����������</td>
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
            <asp:GridView ID="gvList" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False" >
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input id="chkSelect" type="checkbox" value='<%# Eval("trans_code") %>' />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <input id="chkSelectAll" type="checkbox" title="ȫѡ��ȫ��ȡ��" onclick="jcomSelectAllRecords()" />
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="2em" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="������" SortExpression="trans_code">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("trans_code")%>' NavigateUrl='<%# "dns_trans_code_modify.aspx?rec_key=" + Eval("trans_code")%>'>HyperLink</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="trans_code_name" HeaderText="����" SortExpression="trans_code_name" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Ѱַ����" SortExpression="service_id">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# VIVServiceId(Eval("service_id").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="trans_code_desc" HeaderText="����" SortExpression="trans_code_desc" >
                        <ItemStyle Width="50%" />
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

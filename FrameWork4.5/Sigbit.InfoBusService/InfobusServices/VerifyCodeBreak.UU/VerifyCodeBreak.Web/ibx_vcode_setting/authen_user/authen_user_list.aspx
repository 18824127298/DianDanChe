<%@ Page Language="C#" AutoEventWireup="true" CodeFile="authen_user_list.aspx.cs" Inherits="genui_EEQY_authen_user_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head runat="server">
    <title>��֤����Ȩ�û�����</title>
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
            location="authen_user_modify.aspx?del_rec=" + sSelectedIDs;
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
                        <img src="../../images/menu_icon/payment.gif" />
                        ��֤����Ȩ�û�����</td>
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
                            <input id="chkSelect" type="checkbox" value='<%# Eval("authen_user_uid") %>' />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <input id="chkSelectAll" type="checkbox" title="ȫѡ��ȫ��ȡ��" onclick="jcomSelectAllRecords()" />
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="2em" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�û���" SortExpression="authen_user_name">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("authen_user_name")%>' NavigateUrl='<%# "authen_user_modify.aspx?rec_key=" + Eval("authen_user_uid")%>'>HyperLink</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="authen_password" HeaderText="����" SortExpression="authen_password" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="��������" SortExpression="limit_per_minute">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# VIVLimitPer(Eval("limit_per_minute"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Сʱ����" SortExpression="limit_per_hour">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# VIVLimitPer(Eval("limit_per_hour"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ÿ������" SortExpression="limit_per_day">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# VIVLimitPer(Eval("limit_per_day"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="remarks" HeaderText="��ע" SortExpression="remarks" >
                        <ItemStyle Width="40%" />
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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="user_list.aspx.cs" Inherits="framework_organize_user_list" %>

<html>
<head runat="server">
    <title>[��������]�û��б�</title>
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
            location="user_modify.aspx?del_rec=" + sSelectedIDs;
        }
        function UnlockUser(sUserUid)
        {
            if(!window.confirm("�������������û���\nȷ����"))
            {
                return;
            }
            location="user_modify.aspx?unlock_rec=" + sUserUid;
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
                        <img src="../../images/menu_icon/netchat.gif" />
                        <asp:Label ID="lblDeptName" runat="server" Text="��������"></asp:Label>
                        �û��б�</td>
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
                    &nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" Text=" �����û� " OnClick="btnAdd_Click" />
                    &nbsp;&nbsp;<input runat="server" id="btnDelete" type="button" value=" ɾ���û� " class="normalButton" onclick="DeleteData()" /></td>
            </tr>
            </table>
        </td>
        </tr>

        <tr>
        <td>
            <asp:GridView ID="gvList" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False" >
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input id="chkSelect" type="checkbox" value='<%# Eval("user_uid") %>' />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <input id="chkSelectAll" type="checkbox" title="ȫѡ��ȫ��ȡ��" onclick="jcomSelectAllRecords()" />
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="2em" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�Ա�" SortExpression="sex">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <img src="images/<%# VIVSex(Eval("sex").ToString()) %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�ʺ�" SortExpression="user_name">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("user_name")%>' NavigateUrl='<%# "user_modify.aspx?rec_key=" + Eval("user_uid")%>'>HyperLink</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="real_name" HeaderText="����" SortExpression="real_name" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="״̬" SortExpression="is_active">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <img src="images/<%# VIVIsActive(Eval("is_active").ToString()) %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����">
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <img src="../../images/menu_icon/lock.gif" border="0" onclick="UnlockUser('<%# Eval("user_uid") %>')" width="<%# VIVLockWidth(Eval("user_uid").ToString()) %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Visible="False" />
            </asp:GridView>
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

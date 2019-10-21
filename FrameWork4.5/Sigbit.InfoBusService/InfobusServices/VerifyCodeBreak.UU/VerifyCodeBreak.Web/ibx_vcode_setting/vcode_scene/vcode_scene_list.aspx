<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vcode_scene_list.aspx.cs" Inherits="genui_LYOG_vcode_scene_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head runat="server">
    <title>��֤�볡������</title>
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
            location="vcode_scene_modify.aspx?del_rec=" + sSelectedIDs;
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
                        <img src="../../images/menu_icon/house.gif" />
                        ��֤�볡������</td>
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
            <asp:GridView ID="gvList" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" 
                    OnSorting="gvList_Sorting" EnableViewState="False" CellPadding="4">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input id="chkSelect" type="checkbox" value='<%# Eval("vcode_id") %>' />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <input id="chkSelectAll" type="checkbox" title="ȫѡ��ȫ��ȡ��" onclick="jcomSelectAllRecords()" />
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="2em" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��֤���ʶ" SortExpression="vcode_id">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("vcode_id")%>' NavigateUrl='<%# "vcode_scene_modify.aspx?rec_key=" + Eval("vcode_id")%>'>HyperLink</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="vcode_name" HeaderText="��֤������" SortExpression="vcode_name" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="vcode_desc" HeaderText="����" SortExpression="vcode_desc" >
                        <ItemStyle Width="25%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="�㷨" SortExpression="algol_id">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# VIVAlgolId(Eval("algol_id").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="algol_params" HeaderText="�㷨����" SortExpression="algol_params" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="���������" SortExpression="call_rate">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# VIVCallRate(Eval("call_rate"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="αװʱ��" SortExpression="call_fake_min_sec">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# VIVCallFakeMinSec(Eval("call_fake_min_sec"), Eval("call_fake_max_sec"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ǿ��ʱ��" SortExpression="call_force_min_sec">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# VIVCallFakeMinSec(Eval("call_force_min_sec"), Eval("call_force_max_sec"))%>
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
            <asp:Button ID="btnClearCondition" runat="server" Text="�������" OnClick="btnClearCondition_Click" />&nbsp;<asp:Label ID="lblConditionDesc" runat="server"
                Text="Label"></asp:Label>
        </asp:Panel>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_voice_qan_list.aspx.cs" Inherits="genui_FBSL_log_voice_qan_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head runat="server">
    <title>����������־���</title>
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
            location="log_voice_qan_modify.aspx?del_rec=" + sSelectedIDs;
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
                        <img src="../../images/menu_icon/salary.gif" />
                        ����������־���</td>
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
                    &nbsp;&nbsp;<input id="btnDelete" type="button" value=" ɾ�� " class="normalButton" onclick="DeleteData()" /></td>
            </tr>
            </table>
        </td>
        </tr>

        <tr>
        <td>
            <asp:GridView ID="gvList" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False" CellPadding="4" >
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input id="chkSelect" type="checkbox" value='<%# Eval("log_uid") %>' />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <input id="chkSelectAll" type="checkbox" title="ȫѡ��ȫ��ȡ��" onclick="jcomSelectAllRecords()" />
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="2em" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�ϴ�ʱ��" SortExpression="upload_time">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("upload_time")%>' NavigateUrl='<%# "log_voice_qan_modify.aspx?rec_key=" + Eval("log_uid")%>'>HyperLink</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="voice_file_for_qan" HeaderText="���������ļ�" SortExpression="voice_file_for_qan" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="״̬" SortExpression="current_status">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# VIVCurrentStatus(Eval("current_status").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�������" SortExpression="quality_value_01">
                        <ItemStyle HorizontalAlign="Center" Width="40%" />
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# VIVQualityValue01(Eval("quality_value_01"), Eval("quality_value_02"), Eval("quality_value_03"), Eval("quality_value_04")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="qan_delay" HeaderText="����ʱ��" SortExpression="qan_delay" >
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

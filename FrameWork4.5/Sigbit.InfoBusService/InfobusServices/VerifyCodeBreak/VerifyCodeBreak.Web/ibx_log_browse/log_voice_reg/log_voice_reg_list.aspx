<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_voice_reg_list.aspx.cs" Inherits="genui_NVAI_log_voice_reg_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head runat="server">
    <title>����ʶ����־���</title>
</head>
    <script language="javascript" src="../../js/grid_ui_func.js"></script>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/comm.gif" />
                        ����ʶ����־���</td>
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
                    &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text=" ���� " OnClick="btnSearch_Click" /></td>
            </tr>
            </table>
        </td>
        </tr>

        <tr>
        <td>
            <asp:GridView ID="gvList" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False" CellPadding="4" >
                <Columns>
                    <asp:TemplateField HeaderText="�ϴ�ʱ��" SortExpression="upload_time">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("upload_time")%>' NavigateUrl='<%# "log_voice_reg_modify.aspx?rec_key=" + Eval("log_uid")%>'>HyperLink</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="reqeust_third_id" HeaderText="����������" SortExpression="reqeust_third_id" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="voice_file_local" HeaderText="�����ļ�" SortExpression="voice_file_local" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="voice_file_for_isr" HeaderText="�����ļ�" SortExpression="voice_file_for_isr" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="grammar_id" HeaderText="�﷨" SortExpression="grammar_id" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="from_client_id" HeaderText="�ͻ���" SortExpression="from_client_id" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="current_status" HeaderText="��ǰ״̬" SortExpression="current_status" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="reg_fail_desc" HeaderText="ʧ��ԭ��" SortExpression="reg_fail_desc" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="reg_result_text" HeaderText="ʶ������" SortExpression="reg_result_text" >
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

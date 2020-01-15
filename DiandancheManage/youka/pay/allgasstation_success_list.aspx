<%@ Page Language="C#" AutoEventWireup="true" CodeFile="allgasstation_success_list.aspx.cs" Inherits="youka_pay_allgasstation_success_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>
<%@ Register Src="../../module/My97DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>

<html>
<head id="Head1" runat="server">
    <title>客户线上支付</title>
</head>

<script language="javascript" src="../../js/grid_ui_func.js"></script>

<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/star.gif" />
                            客户线上支付</td>
                    </tr>
                </table>
                <hr />
            </div>
            <div>
                &nbsp;支付的时间：<uc1:DatePicker ID="DatePickerFrom" runat="server" ShowDateFmt="yyyy-MM-dd" />
                ~&nbsp;<uc1:DatePicker ID="DatePickerTo" runat="server" ShowDateFmt="yyyy-MM-dd" />
                &nbsp;<asp:CheckBox ID="ckbIsAudit" runat="server" Text="支付成功" />
                &nbsp;<asp:Button ID="btnSearch" runat="server" Text=" 搜索 " OnClick="btnSearch_Click" />
                &nbsp;<asp:Button ID="btnExportExcel" runat="server" OnClick="btnExportExcel_Click" Text="导出EXCEL" />
            </div>

            <table cellspacing="0" cellpadding="4" width="100%" border="0" align="center">
                <tr>
                    <td>
                        <asp:GridView ID="gvList" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False" CellPadding="8">
                            <Columns>
                                <asp:BoundField DataField="PayTime" HeaderText="支付时间" SortExpression="PayTime">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="加油站价格" SortExpression="GasStationAmount">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVamount(Eval("GasStationAmount")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="实际支付金额" SortExpression="ActualAmount">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVamount(Eval("ActualAmount")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:BoundField DataField="RiseNumber" HeaderText="升数" SortExpression="RiseNumber">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="加油站" SortExpression="Name">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OrderNumber" HeaderText="订单号" SortExpression="OrderNumber">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="状态" SortExpression="IsAudit">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVIsAudit(Eval("IsAudit"),Eval("CreateTime"),Eval("Id")) %>
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
                <tr>
                    <td>&nbsp;<%--累计充值笔数:123笔，充值金额共982.23元，手续费：23.23元；--%>
                        <asp:Label ID="lblSummaryInfo" runat="server" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="divSearchCondition" runat="server" Width="100%">
                <asp:Button ID="btnClearCondition" runat="server" Text="清空条件" OnClick="btnClearCondition_Click" />&nbsp;<asp:Label ID="lblConditionDesc" runat="server"
                    Text="Label"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
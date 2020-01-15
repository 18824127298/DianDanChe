<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loan_bill.aspx.cs" Inherits="loan_loan_list_loan_bill" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<%@ Register Src="../../module/My97DatePicker.ascx" TagName="My97DatePicker" TagPrefix="uc1" %>
<html>
<head id="Head1" runat="server">
    <title>账单</title>
</head>
<script language="javascript" src="../../js/grid_ui_func.js"></script>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/pen.gif" />
                            账单</td>
                    </tr>
                </table>
                <hr />
            </div>

            <table cellspacing="0" cellpadding="4" width="100%" border="0" align="center">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="90%" border="0" align="center" style="width: 1217px">
                            <tr>
                                <td>支付日：<uc1:My97DatePicker ID="dpFromTime" runat="server" Width="90px" />
                                    -<uc1:My97DatePicker ID="dpToTime" runat="server" Width="90px" />
                                    <asp:Button ID="btnSearch" runat="server" Text=" 搜 索 " OnClick="btnSearch_Click" />
                                    &nbsp;<asp:Button ID="btnExportExcel" runat="server" OnClick="btnExportExcel_Click" Text="导出EXCEL" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvList" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False" CellPadding="8">
                            <Columns>
                                <asp:BoundField DataField="FullName" HeaderText="客户" SortExpression="FullName">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Phone" HeaderText="手机号" SortExpression="Phone">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="业务员名字">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVSalesmanName(Eval("SalesmanId")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UnPrincipal" HeaderText="未还融资租赁总额" SortExpression="UnPrincipal">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UnTotalInterest" HeaderText="未还手续费" SortExpression="UnTotalInterest">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="unSumAmount" HeaderText="未还总金额" SortExpression="unSumAmount">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Stages" HeaderText="该期次" SortExpression="Stages">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalPeriod" HeaderText="总期次" SortExpression="TotalPeriod">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="到期日">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVRepaymentDate(Eval("RepaymentDate")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CreditAmount" HeaderText="总金额" SortExpression="CreditAmount">
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
        </div>
    </form>
</body>
</html>

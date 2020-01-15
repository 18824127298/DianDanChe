<%@ Page Language="C#" AutoEventWireup="true" CodeFile="borrower_borrow_list.aspx.cs" Inherits="loan_loan_list_borrower_borrow_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head id="Head1" runat="server">
    <title>客户融资租赁账单</title>
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
                            客户融资租赁账单</td>
                    </tr>
                </table>
                <hr />
            </div>

            <table cellspacing="0" cellpadding="4" width="100%" border="0" align="center">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="90%" border="0" align="center" style="width: 1217px">
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvList" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False" CellPadding="8">
                            <Columns>
                                 <asp:BoundField DataField="FullName" HeaderText="姓名" SortExpression="FullName">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="CreditPhone" HeaderText="手机号" SortExpression="CreditPhone">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Stages" HeaderText="期次" SortExpression="Stages">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="应支付时间" SortExpression="RepaymentDate">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVHTime(Eval("RepaymentDate")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Principal" HeaderText="融资租赁总额" SortExpression="Principal">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UnPrincipal" HeaderText="未还融资租赁总额" SortExpression="UnPrincipal">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Interest" HeaderText="手续费" SortExpression="Interest">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OverInterest" HeaderText="违约金" SortExpression="OverInterest">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OverDay" HeaderText="逾期天数" SortExpression="OverDay">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UnTotalInterest" HeaderText="未还手续费" SortExpression="UnTotalInterest">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sumuntotal" HeaderText="未还总金额" SortExpression="sumuntotal">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="实际支付时间" SortExpression="ActualRepaymentDate">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVHTime(Eval("ActualRepaymentDate")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="类型" SortExpression="RepaymentPlanMode">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVRepayment(Eval("RepaymentPlanMode")) %>
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
        </div>
    </form>
</body>
</html>

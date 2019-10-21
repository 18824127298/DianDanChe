<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cdb_borrow_secondauditing.aspx.cs" Inherits="cdb_borrow_cdb_borrow_auditing_cdb_borrow_secondauditing" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head id="Head1" runat="server">
    <title>借款申请列表</title>
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
                            借款申请列表</td>
                    </tr>
                </table>
                <hr />
            </div>

            <table cellspacing="0" cellpadding="8" width="100%" border="0" align="center">
                <tr>
                    <td style="text-align: left;">
                        <table style="float: left;" cellspacing="0" cellpadding="0" width="90%" border="0" align="center">
                            <tr>
                                </asp:TextBox>&nbsp;&nbsp 申请人手机号：<asp:TextBox ID="edtCreditPhone" runat="server"></asp:TextBox>
                                &nbsp;&nbsp 申请人姓名：<asp:TextBox ID="edtCreditName" runat="server"></asp:TextBox>
                                &nbsp;&nbsp<asp:Button ID="btnSearch" runat="server" Text=" 搜索 " OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>

            <tr>
                <td>
                    <asp:GridView ID="gvList" runat="server" CellSpacing="0" CellPadding="6" Width="100%" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False">
                        <Columns>
                            <asp:BoundField DataField="LoanTime" HeaderText="申请时间" SortExpression="LoanTime">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="申请人注册手机号">
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                <ItemTemplate>
                                    <%# VIVAESDecrypt(Eval("CreditPhone")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="申请人姓名">
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                <ItemTemplate>
                                    <%# VIVAESDecrypt(Eval("CreditName")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CarName" HeaderText="车名" SortExpression="CarName">
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CarTitle" HeaderText="车标题" SortExpression="CarTitle">
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="车类型">
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                <ItemTemplate>
                                    <%# VIVOperateModel(Eval("OperateModelId")) %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="提车款">
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                <ItemTemplate>
                                    <%# VIVCreditAmount(Eval("LiftFares").ToString()) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="尾付款">
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                <ItemTemplate>
                                    <%# VIVCreditAmount(Eval("Retainage").ToString()) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="月供">
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                <ItemTemplate>
                                    <%# VIVCreditAmount(Eval("MonthPrice").ToString()) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Stages" HeaderText="借款期限" SortExpression="Stages">
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CreditDescription" HeaderText="借款描述" SortExpression="CreditDescription">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="审核">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# VIVaduit(Eval("SecondAuditResult"),Convert.ToInt32(Eval("lId")), Eval("SecondAuditor").ToString()) %>
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
            <asp:Panel ID="divSearchCondition" runat="server" Width="100%" Visible="false">
                <asp:Button ID="btnClearCondition" runat="server" Text="清空条件" OnClick="btnClearCondition_Click" />&nbsp;<asp:Label ID="lblConditionDesc" runat="server"
                    Text="Label"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>


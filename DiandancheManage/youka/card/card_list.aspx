<%@ Page Language="C#" AutoEventWireup="true" CodeFile="card_list.aspx.cs" Inherits="youka_card_card_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<%@ Register Src="../../module/My97DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<html>
<head id="Head1" runat="server">
    <title>卡号表</title>
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
                            卡号表</td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="Insert" runat="server" Text=" 新 增 " OnClick="Insert_Click" />
                                    </td>
                                    <td>
                                        卡号：<asp:TextBox ID="CardNumber" runat="server" />
                                        <asp:Button ID="btnsearch" runat="server" Text=" 查 询 " OnClick="btnsearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
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
                                <asp:TemplateField HeaderText="卡号" SortExpression="Name">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("CardNumber")%>' NavigateUrl='<%# "card_update.aspx?id=" + Eval("Id")%>'>HyperLink</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CardDiscount" HeaderText="折扣" SortExpression="CardDiscount">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CardLevel" HeaderText="等级" SortExpression="CardLevel">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CardRoyalty" HeaderText="卡提成" SortExpression="CardRoyalty">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AgentName" HeaderText="代理商" SortExpression="AgentName">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MemberName" HeaderText="会员的姓名" SortExpression="MemberName">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SupplierNumber" HeaderText="主账号" SortExpression="SupplierNumber">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CarNumber" HeaderText="车牌号" SortExpression="CarNumber">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="卡片的状态">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVCardStatus(Eval("CardStatus")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="是否可以充值">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVIsRecharge(Eval("IsRecharge")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="所属的公司">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVCardBrand(Eval("CardBrand")) %>
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

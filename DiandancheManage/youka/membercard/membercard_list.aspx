<%@ Page Language="C#" AutoEventWireup="true" CodeFile="membercard_list.aspx.cs" Inherits="youka_membercard_membercard_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head id="Head1" runat="server">
    <title>代理商列表</title>
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
                            代理商列表</td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
                                <tr>
                                    <td>手机号：<asp:TextBox ID="edtPhone" runat="server" />&nbsp;
                                        <asp:Button ID="btnSearch" runat="server" Text=" 搜 索 " OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnInsert" runat="server" Text=" 新 增 " OnClick="btnInsert_Click" />
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
                                     <asp:TemplateField HeaderText="会员手机号" SortExpression="MemberPhone">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="MemberPhone" runat="server" Text='<%# Eval("MemberPhone")%>' NavigateUrl='<%# "membercard_update.aspx?id=" + Eval("Id")%>'>HyperLink</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CardNumber" HeaderText="卡号" SortExpression="CardNumber">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Discount" HeaderText="折扣" SortExpression="Discount">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CardLevel" HeaderText="等级" SortExpression="CardLevel">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AgentName" HeaderText="卡的代理商" SortExpression="AgentName">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CreateTime" HeaderText="绑卡的时间" SortExpression="CreateTime">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="卡所属品牌">
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

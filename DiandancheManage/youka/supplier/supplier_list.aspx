<%@ Page Language="C#" AutoEventWireup="true" CodeFile="supplier_list.aspx.cs" Inherits="youka_supplier_supplier_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head id="Head1" runat="server">
    <title>用户的信息</title>
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
                            用户的信息</td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
                                <tr>
                                    <td>姓名：<asp:TextBox ID="edtCreditName" runat="server" />&nbsp;
                                    手机号：<asp:TextBox ID="edtCreditPhone" runat="server" />&nbsp;
                                        <asp:Button ID="btnSearch" runat="server" Text=" 搜 索 " OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnInsert" runat="server" Text=" 新 增 " OnClick="btnInsert_Click"/>
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
                         <%--       <asp:TemplateField HeaderText="手机号" SortExpression="Phone">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("Phone")%>' NavigateUrl='<%# "borrower_update.aspx?id=" + Eval("Id")%>'>HyperLink</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                 <asp:TemplateField HeaderText="名字">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# Name(Eval("Name"),Eval("Id")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="名字" SortExpression="Name">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Phone" HeaderText="手机号" SortExpression="Phone">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CreateTime" HeaderText="注册的时间" SortExpression="CreateTime">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Concessional" HeaderText="旧优惠" SortExpression="Concessional">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NewConcessional" HeaderText="新优惠" SortExpression="NewConcessional">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ConcessionalPointTime" HeaderText="时间点" SortExpression="ConcessionalPointTime">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Balance" HeaderText="余额" SortExpression="Balance">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="充值">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# Recharge(Eval("Id")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="记录">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# RechargeList(Eval("Id")) %>
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

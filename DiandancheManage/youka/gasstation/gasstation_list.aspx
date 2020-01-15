<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gasstation_list.aspx.cs" Inherits="youka_gasstation_gasstation_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<%@ Register Src="../../module/My97DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<html>
<head id="Head1" runat="server">
    <title>加油站店列表</title>
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
                            加油站的信息</td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="Insert" runat="server" Text=" 新 增 " OnClick="Insert_Click" />
                                    </td>
                                    <td>油号：<asp:DropDownList ID="ddlOilNumber" runat="server">
                                        <Items>
                                            <asp:ListItem Value='0#柴油'>0#柴油</asp:ListItem>
                                            <asp:ListItem Value='92#汽油'>92#汽油</asp:ListItem>
                                            <asp:ListItem Value='95#汽油'>95#汽油</asp:ListItem>
                                            <asp:ListItem Value='98#汽油'>98#汽油</asp:ListItem>
                                        </Items>
                                    </asp:DropDownList>
                                        国标价：<asp:TextBox ID="CountryMarkPrice" runat="server" />&nbsp;
                                        新的国标价：<asp:TextBox ID="NewCountryPrice" runat="server" />&nbsp;
                                        国标价节点：<uc1:DatePicker ID="CountryPointTime" runat="server" ShowDateFmt="yyyy-MM-dd HH:mm:ss" />
                                        &nbsp;
                                        <asp:Button ID="btnupdate" runat="server" Text=" 修 改 " OnClick="btnupdate_Click" />
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
                                <asp:TemplateField HeaderText="加油站名字" SortExpression="Name">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("Name")%>' NavigateUrl='<%# "gasstation_update.aspx?id=" + Eval("Id")%>'>HyperLink</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AddressName" HeaderText="地址名" SortExpression="AddressName">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Longitude" HeaderText="经度" SortExpression="Longitude">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Dimension" HeaderText="纬度" SortExpression="Dimension">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PrinterNumber" HeaderText="打印机编号" SortExpression="PrinterNumber">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SupplierName" HeaderText="所属物流公司" SortExpression="SupplierName">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="优惠">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVYouHui(Eval("Id")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="配置">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVPeiZhi(Eval("Id")) %>
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

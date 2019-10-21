<%@ Page Language="C#" AutoEventWireup="true" CodeFile="operatemodes_list.aspx.cs" Inherits="cdb_carillegal_cbd_car_operatemodes_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head id="Head1" runat="server">
    <title>汽车运营类型列表</title>
</head>
<script language="javascript" src="../../js/grid_ui_func.js"></script>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/comm.gif" />汽车运营类型列表</td>
                    </tr>
                </table>
                <hr />
            </div>
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="4" width="100%" border="0" align="center">
                        <tr>
                            <td align="right">
                                <asp:Button ID="Button1" runat="server" Text="新增" OnClick="Button1_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvList" runat="server" CellSpacing="0" CellPadding="6" Width="100%" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False">
                        <Columns>
                            <asp:TemplateField HeaderText="运营车型名称" SortExpression="Name">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <a href="operatemodes_create.aspx?rec_key=<%# Eval("Id") %>"><%# Eval("Name") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CreateTime" HeaderText="创建的时间" SortExpression="CreateTime">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
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
        </div>
    </form>
</body>
</html>




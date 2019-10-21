<%@ Page Language="C#" AutoEventWireup="true" CodeFile="borrow_car_list.aspx.cs" Inherits="cdb_carillegal_cbd_car_borrow_car_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head id="Head1" runat="server">
    <title>会员汽车列表</title>
</head>
<script language="javascript" src="../../js/grid_ui_func.js"></script>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/comm.gif" />会员汽车列表</td>
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
                            <asp:TemplateField HeaderText="会员手机号" >
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <a href="borrow_car_create.aspx?rec_key=<%# Eval("Id") %>"><%# VIVBorrowerPhone(Eval("BorrowerId"))%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="会员的姓名">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <%# VIVBorrowerName(Eval("BorrowerId")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CarSystem" HeaderText="车系" SortExpression="CarSystem">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CarNumber" HeaderText="车牌号" SortExpression="CarNumber">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EngineNumber" HeaderText="发动机号码" SortExpression="EngineNumber">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BodyRackNumber" HeaderText="车身架号码" SortExpression="BodyRackNumber">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="图片">
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                <ItemTemplate>
                                    <%# VIVFilePath(Eval("ImageUrl").ToString()) %>
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




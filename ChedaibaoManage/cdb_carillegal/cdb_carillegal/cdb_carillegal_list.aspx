<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cdb_carillegal_list.aspx.cs" Inherits="cdb_carillegal_cdb_carillegal_cdb_carillegal_list" %>

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
                            <asp:TemplateField HeaderText="会员手机号">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <a href="carillegal_create.aspx?rec_key=<%# Eval("Id") %>"><%# VIVBorrowerPhone(Eval("BorrowerId"))%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="会员的姓名">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <%# VIVBorrowerName(Eval("BorrowerId")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LicensePlate" HeaderText="车牌号" SortExpression="LicensePlate">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IllegalTitle" HeaderText="违规的标题" SortExpression="IllegalTitle">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IllegalDescribe" HeaderText="违规的描述" SortExpression="IllegalDescribe">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IllegalAddress" HeaderText="违章的地点" SortExpression="IllegalAddress">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="违章的时间" SortExpression="IllegalTime">
                                    <ItemStyle HorizontalAlign="Center" Width="5%"/>
                                    <ItemTemplate>
                                        <%# VIVDay( Eval("IllegalTime").ToString()) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="罚款的金额">
                                <ItemStyle HorizontalAlign="Center" Width="5%"/>
                                <ItemTemplate>
                                    <%# VIVMoney(Eval("FinePrice")) %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="手续费">
                                <ItemStyle HorizontalAlign="Center" Width="5%"/>
                                <ItemTemplate>
                                    <%# VIVMoney(Eval("AroundFee")) %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Points" HeaderText="分数" SortExpression="Points">
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





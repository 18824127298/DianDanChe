<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cdb_borrower_list.aspx.cs" Inherits="cdb_borrow_cdb_borrower_cdb_borrower_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head id="Head1" runat="server">
    <title>借款人列表</title>
</head>
<script language="javascript" src="../../js/grid_ui_func.js"></script>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/comm.gif" />借款人列表</td>
                    </tr>
                </table>
                <hr />
            </div>
            <tr>
                <td>
                    <asp:GridView ID="gvList" runat="server" CellSpacing="0" CellPadding="6" Width="100%" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False">
                        <Columns>
                            <asp:TemplateField HeaderText="手机号">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <a href="carillegal_create.aspx?rec_key=<%# Eval("Id") %>"><%# VIVBorrowerPhone(Eval("Id"))%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="姓名">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <%# VIVBorrowerName(Eval("Id")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="身份证">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <%# VIVBorrowerIDNumber(Eval("IDNumber")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="推荐人">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <%# VIVBorrowerRecommend(Eval("RecommendBorrowerId")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="注册的时间">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <%# VIVDay(Eval("CreateTime").ToString()) %>
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
        </div>
    </form>
</body>
</html>





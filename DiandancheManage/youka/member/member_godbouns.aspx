<%@ Page Language="C#" AutoEventWireup="true" CodeFile="member_godbouns.aspx.cs" Inherits="youka_member_member_godbouns" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head id="Head1" runat="server">
    <title>会员红包列表</title>
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
                            会员红包列表</td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
                                <tr>
                                    <td>
                                    手机号：<asp:TextBox ID="edtPhone" runat="server" />&nbsp;
                                        <asp:Button ID="btnSearch" runat="server" Text=" 搜 索 " OnClick="btnSearch_Click" />
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
                                <asp:BoundField DataField="Phone" HeaderText="手机号" SortExpression="Phone">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CreateTime" HeaderText="注册的时间" SortExpression="CreateTime">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="gname" HeaderText="红包的名字" SortExpression="gname">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BounsAmount" HeaderText="红包的金额" SortExpression="BounsAmount">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="红包的状态">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVBounsStatus(Eval("BounsStatus")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RecommendPhone" HeaderText="推荐人手机号" SortExpression="RecommendPhone">
                                    <ItemStyle HorizontalAlign="Center" />
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
            </table>
        </div>
    </form>
</body>
</html>


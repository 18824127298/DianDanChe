<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gasstationlevel_list.aspx.cs" Inherits="youka_gasstationlevel_gasstationlevel_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head id="Head1" runat="server">
    <title></title>
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="Insert" runat="server" Text=" 新 增 " OnClick="Insert_Click" />
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
                                <asp:TemplateField HeaderText="加油站名字">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# Name(Eval("Name"), Eval("Id")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MemberLevel" HeaderText="会员的等级" SortExpression="MemberLevel">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="减免额">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# Amount(Eval("Reduction")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="新的减免额">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# Amount(Eval("NewReduction")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ReductionTime" HeaderText="减免额时间点" SortExpression="ReductionTime">
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

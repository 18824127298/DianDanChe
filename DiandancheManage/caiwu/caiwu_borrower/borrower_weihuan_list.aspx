<%@ Page Language="C#" AutoEventWireup="true" CodeFile="borrower_weihuan_list.aspx.cs" Inherits="caiwu_caiwu_borrower_borrower_weihuan_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<%@ Register Src="../../module/My97DatePicker.ascx" TagName="My97DatePicker" TagPrefix="uc1" %>
<html>
<head id="Head1" runat="server">
    <title>客户未还金额</title>
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
                            客户未还金额</td>
                    </tr>
                </table>
                <hr />
            </div>

            <table cellspacing="0" cellpadding="4" width="100%" border="0" align="center">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="90%" border="0" align="center" style="width: 1217px">
                            <tr>
                                <td>截止时间：<uc1:My97DatePicker ID="dpApplyToTime" runat="server" Width="90px" />
                                    商户：
                                    <asp:DropDownList ID="ddlCompany" runat="server">
                                        <Items>
                                            <asp:ListItem Value='0'>翼速</asp:ListItem>
                                            <asp:ListItem Value='1'>李思静</asp:ListItem>
                                        </Items>
                                    </asp:DropDownList>
                                    <asp:Button ID="btnSearch" runat="server" Text=" 搜 索 " OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>总未还金额：
                            <asp:Label ID="lblAmount" runat="server"></asp:Label>元
                                </td>
                            </tr>
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
                                <asp:BoundField DataField="FullName" HeaderText="姓名" SortExpression="FullName">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Principal" HeaderText="未还的本金" SortExpression="Principal">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="InterestDate" HeaderText="起息日" SortExpression="InterestDate">
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

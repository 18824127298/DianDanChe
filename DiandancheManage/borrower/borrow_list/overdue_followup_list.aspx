<%@ Page Language="C#" AutoEventWireup="true" CodeFile="overdue_followup_list.aspx.cs" Inherits="borrower_borrow_list_overdue_followup_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head id="Head1" runat="server">
    <title>跟踪的情况</title>
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
                            跟踪的情况</td>
                    </tr>
                </table>
                <hr />
            </div>
            <div>
                &nbsp;<asp:Button ID="btnExportExcel" runat="server" OnClick="btnExportExcel_Click" Text="导出EXCEL" />
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
                                <asp:BoundField DataField="FullName" HeaderText="客户">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Phone" HeaderText="手机号">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="业务员名字">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVSalesmanName(Eval("SalesmanId")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                          <%--      <asp:BoundField DataField="BusinessName" HeaderText="商户名称">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="RecruitmentName" HeaderText="招聘点名称">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="融资租赁日">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVInterestDate(Eval("InterestDate")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MonthlyPayment" HeaderText="融资租赁费">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="overday" HeaderText="逾期的天数">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="应还未还期数">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVBorrowerId(Eval("BorrowerId")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="已还的期数">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVStagesCount(Eval("BorrowerId")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="客户经理跟进">
                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    <ItemTemplate>
                                        <%# VIVKH(Eval("Id"),Eval("Phone")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="区域经理跟进">
                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    <ItemTemplate>
                                        <%# VIVQY(Eval("Id"),Eval("Phone")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="风控人员跟进">
                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    <ItemTemplate>
                                        <%# VIVFK(Eval("Id"),Eval("Phone")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="跟进">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVFollowUp(Eval("Id")) %>
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

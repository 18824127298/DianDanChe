<%@ Page Language="C#" AutoEventWireup="true" CodeFile="post_loan_management_list.aspx.cs" Inherits="loan_loan_list_post_loan_management_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<%@ Register Src="../../module/My97DatePicker.ascx" TagName="My97DatePicker" TagPrefix="uc1" %>
<html>
<head id="Head1" runat="server">
    <title>融资租赁后管理列表</title>
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
                            融资租赁后管理列表</td>
                    </tr>
                    <tr>
                        <td>客户姓名：<asp:TextBox ID="edtCreditName" runat="server" />&nbsp;
                            客户手机号：<asp:TextBox ID="edtCreditPhone" runat="server" />&nbsp;
                            申请时间：<uc1:My97DatePicker ID="dpApplyFromTime" runat="server" Width="90px" />
                            -<uc1:My97DatePicker ID="dpApplyToTime" runat="server" Width="90px" />
                            商户：
                            <asp:DropDownList ID="ddlCompany" runat="server">
                                <Items>
                                    <asp:ListItem Value='All'>全部</asp:ListItem>
                                    <asp:ListItem Value='0'>翼速</asp:ListItem>
                                    <asp:ListItem Value='1'>李思静</asp:ListItem>
                                </Items>
                            </asp:DropDownList>
                            是否为骑手：
                            <asp:DropDownList ID="ddlIsRider" runat="server">
                                <Items>
                                    <asp:ListItem Value='All'>全部</asp:ListItem>
                                    <asp:ListItem Value='1'>是</asp:ListItem>
                                    <asp:ListItem Value='0'>否</asp:ListItem>
                                </Items>
                            </asp:DropDownList>
                            是否有接听：
                            <asp:DropDownList ID="ddlIsAnswer" runat="server">
                                <Items>
                                    <asp:ListItem Value='All'>全部</asp:ListItem>
                                    <asp:ListItem Value='1'>是</asp:ListItem>
                                    <asp:ListItem Value='0'>否</asp:ListItem>
                                </Items>
                            </asp:DropDownList>
                            是否有异常：
                            <asp:DropDownList ID="ddlIsAbnormal" runat="server">
                                <Items>
                                    <asp:ListItem Value='All'>全部</asp:ListItem>
                                    <asp:ListItem Value='1'>是</asp:ListItem>
                                    <asp:ListItem Value='0'>否</asp:ListItem>
                                </Items>
                            </asp:DropDownList>
                            <asp:Button ID="btnSearch" runat="server" Text=" 搜 索 " OnClick="btnSearch_Click" />
                            &nbsp;<asp:Button ID="btnExportExcel" runat="server" OnClick="btnExportExcel_Click" Text="导出EXCEL" />
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
                                <asp:TemplateField HeaderText="手机号" SortExpression="CreditPhone">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("CreditPhone")%>' NavigateUrl='<%# VIVNavigateUrl(Eval("Id"),Eval("LoanType")) %>'>HyperLink</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="客户姓名" SortExpression="Name">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FullName" HeaderText="业务员" SortExpression="FullName">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BusinessName" HeaderText="商户名称" SortExpression="BusinessName">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RecruitmentName" HeaderText="招聘点名称" SortExpression="RecruitmentName">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Auditor" HeaderText="审核人" SortExpression="Auditor">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AuditTime" HeaderText="审核时间" SortExpression="AuditTime">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VisitRecord" HeaderText="回访记录" SortExpression="VisitRecord">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                 <asp:TemplateField HeaderText="是否为骑手">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVIsRider(Eval("IsRider")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="是否有接听">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVIsAnswer(Eval("IsAnswer")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="是否有异常">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVIsAbnormal(Eval("IsAbnormal")) %>
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
            <asp:Panel ID="divSearchCondition" runat="server" Width="100%">
                <asp:Button ID="btnClearCondition" runat="server" Text="清空条件" OnClick="btnClearCondition_Click" />&nbsp;<asp:Label ID="lblConditionDesc" runat="server"
                    Text="Label"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="unaudited_loans_list.aspx.cs" Inherits="loan_loan_list_unaudited_loans_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head id="Head1" runat="server">
    <title>未审核的融资租赁</title>
    <script language="javascript">
        function DeleteData() {
            var sSelectedIDs = jcomGetAllSelectedRecords();
            if (sSelectedIDs == "") {
                alert("请选择待删除的记录！");
                return;
            }
            if (!window.confirm("本操作将删除所有选择的数据记录。\n确认吗？")) {
                return;
            }
            location = "unaudited_loans_list.aspx?del_rec=" + sSelectedIDs;
        }
    </script>
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
                            未审核的融资租赁</td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
                                <tr>
                                    <td>客户姓名：<asp:TextBox ID="edtCreditName" runat="server" />&nbsp;
                                    客户手机号：<asp:TextBox ID="edtCreditPhone" runat="server" />&nbsp;
                                        <asp:Button ID="btnSearch" runat="server" Text=" 搜 索 " OnClick="btnSearch_Click" />
                                        &nbsp;&nbsp;<input id="btnDelete" type="button" value=" 删除 " class="normalButton"
                                            onclick="DeleteData()" /></td>
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
                        <asp:GridView ID="gvList" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" EnableViewState="False" CellPadding="8">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <input id="chkSelect" type="checkbox" value='<%# Eval("Id") %>' />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <input id="chkSelectAll" type="checkbox" title="全选或全部取消" onclick="jcomSelectAllRecords()" />
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="2em" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="手机号">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("CreditPhone")%>' NavigateUrl='<%# VIVNavigateUrl(Eval("Id"),Eval("LoanType")) %>'>HyperLink</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="客户姓名">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FullName" HeaderText="业务员">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BusinessName" HeaderText="商户名称">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RecruitmentName" HeaderText="招聘点名称">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Deadline" HeaderText="期数">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CustomerClassification" HeaderText="客户的类型">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="类型">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVLoanType(Eval("LoanType")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CreateTime" HeaderText="申请的时间">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="客户补充资料">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVinformation(Eval("Id")) %>
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

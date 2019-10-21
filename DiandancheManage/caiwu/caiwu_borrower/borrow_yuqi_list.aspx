<%@ Page Language="C#" AutoEventWireup="true" CodeFile="borrow_yuqi_list.aspx.cs" Inherits="caiwu_caiwu_borrower_borrow_yuqi_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head id="Head1" runat="server">
    <title>逾期的账单</title>
</head>
<script language="javascript" src="../../js/grid_ui_func.js"></script>
<script src="../../js/jquery-1.8.2.min.js"></script>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/pen.gif" />
                            逾期的账单</td>
                    </tr>
                    <tr>
                        <td>用户的余额：
                            <asp:Label ID="lblBalance" runat="server"></asp:Label>元
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
                                <asp:BoundField DataField="FullName" HeaderText="客户姓名" SortExpression="FullName">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Phone" HeaderText="手机号" SortExpression="Phone">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="业务员名字">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# VIVSalesmanName(Eval("SalesmanId")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UnPrincipal" HeaderText="未还本金" SortExpression="UnPrincipal">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UnTotalInterest" HeaderText="未还总手续费" SortExpression="UnTotalInterest">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Brand" HeaderText="逾期手续费" SortExpression="Brand">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Stages" HeaderText="该期次" SortExpression="Stages">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalPeriod" HeaderText="总期次" SortExpression="TotalPeriod">
                                    <ItemStyle HorizontalAlign="Center" /> 
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="还款">
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemTemplate>
                                        <input name="button_test" type="button" value="还款" onclick="fn_huankuan(<%#Eval("Id") %>,<%#Eval("lId") %>)" />
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
<script type="text/javascript">
    function fn_huankuan(Id,lId)
    {
        $.ajax({
            type: 'get',
            async: false,
            url: 'borrow_yuqi_list.aspx?r=' + Math.random(),
            data: {
                Action: 'SingleTransaction',
                'bId': Id
            },
            success: function (data) {
                alert(data);
                location.href = "borrow_yuqi_list.aspx?Id="+lId;
            }
        });
    }
</script>

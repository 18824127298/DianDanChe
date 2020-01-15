﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="caiwu_fangkuan_list.aspx.cs" Inherits="caiwu_caiwu_fangkua_caiwu_fangkuan_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head id="Head1" runat="server">
    <title>未放款列表</title>
    <script src="../../js/jquery-1.8.2.min.js"></script>
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
                            未放款列表</td>
                    </tr>
                    <tr>
                        <td>客户姓名：<asp:TextBox ID="edtCreditName" runat="server" />&nbsp;
                                    客户手机号：<asp:TextBox ID="edtCreditPhone" runat="server" />&nbsp; 
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
                                <asp:BoundField DataField="CreditPhone" HeaderText="手机号" SortExpression="CreditPhone">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
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
                                <asp:BoundField DataField="TotalAmountStage" HeaderText="融资租赁总额" SortExpression="TotalAmountStage">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Deadline" HeaderText="期数" SortExpression="Deadline">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Auditor" HeaderText="审核人" SortExpression="Auditor">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AuditTime" HeaderText="审核时间" SortExpression="AuditTime">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="放款">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <input name="button_test" type="button" value="放款" onclick="fn_huankuan(<%#Eval("ID")%>)" />
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

<script type="text/javascript">
    function fn_huankuan(Id)
    {
        $.ajax({
            type: 'get',
            async: false,
            url: 'caiwu_fangkuan_list.aspx?r=' + Math.random(),
            data: {
                Action: 'Lending',
                'Id': Id
            },
            success: function (data) {
                alert(data);
                location.href = "caiwu_fangkuan_list.aspx";
            }
        });
    }
</script>



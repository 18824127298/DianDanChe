<%@ Page Language="C#" AutoEventWireup="true" CodeFile="driver_application_list.aspx.cs" Inherits="cdb_borrow_cdb_borrow_auditing_driver_application_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head id="Head1" runat="server">
    <title>借款申请列表</title>
</head>
<script language="javascript" src="../../js/grid_ui_func.js"></script>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/comm.gif" />
                            借款申请列表</td>
                    </tr>
                </table>
                <hr />
            </div>

            <table cellspacing="0" cellpadding="8" width="100%" border="0" align="center">
                <tr>
                    <td style="text-align: left;">
                        <table style="float: left;" cellspacing="0" cellpadding="0" width="90%" border="0" align="center">
                            <tr>
                                </asp:TextBox>&nbsp;&nbsp 申请人手机号：<asp:TextBox ID="edtCreditPhone" runat="server"></asp:TextBox>
                                &nbsp;&nbsp 申请人姓名：<asp:TextBox ID="edtCreditName" runat="server"></asp:TextBox>
                                &nbsp;&nbsp<asp:Button ID="btnSearch" runat="server" Text=" 搜索 " OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>

            <tr>
                <td>
                    <asp:GridView ID="gvList" runat="server" CellSpacing="0" CellPadding="6" Width="100%" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False">
                        <Columns>
                             <asp:TemplateField HeaderText="申请人手机号">
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                <ItemTemplate>
                                    <%# VIVAESDecrypt(Eval("Phone")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="申请人姓名">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <%# VIVAESDecrypt(Eval("FullName")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="申请人是否实名认证">
                                <ItemStyle HorizontalAlign="Center" Width="18%" />
                                <ItemTemplate>
                                    <%# VIVIsIdNumber(Eval("IsIdNumber")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CreateTime" HeaderText="申请的时间" SortExpression="CreateTime">
                                <ItemStyle HorizontalAlign="Center" Width="18%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="审核">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# VIVaduit(Eval("IsAudit"),Convert.ToInt32(Eval("Id")), Eval("Auditor").ToString()) %>
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
            <asp:Panel ID="divSearchCondition" runat="server" Width="100%" Visible="false">
                <asp:Button ID="btnClearCondition" runat="server" Text="清空条件" OnClick="btnClearCondition_Click" />&nbsp;<asp:Label ID="lblConditionDesc" runat="server"
                    Text="Label"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>


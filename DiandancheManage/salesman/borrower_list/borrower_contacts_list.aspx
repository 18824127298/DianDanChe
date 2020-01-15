<%@ Page Language="C#" AutoEventWireup="true" CodeFile="borrower_contacts_list.aspx.cs" Inherits="salesman_borrower_list_borrower_contacts_list" %>

<%@ Register Src="../../module/My97DatePicker.ascx" TagName="My97DatePicker" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户基本信息
    </title>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/mytable.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="客户基本信息"></asp:Label></td>
                    </tr>
                </table>
                <hr />
            </div>
            <div class="contentTable">

                <table width="100%" cellpadding="8" cellspacing="1" align="center" style="margin-top: 10px;">
                    <thead>
                        <tr>
                            <td colspan="3" style="text-align: center;">联系人信息
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:GridView ID="ContactsList" runat="server" Width="100%" AutoGenerateColumns="False" EnableViewState="False" CellPadding="8">
                                    <Columns>
                                        <asp:BoundField DataField="Contact" HeaderText="关系" SortExpression="Contact">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Phone" HeaderText="手机号" SortExpression="Phone">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="是否知晓融资租赁" SortExpression="IsKnowStages">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%# VIVIsKnowStages(Eval("IsKnowStages")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="修改时间" SortExpression="UpdateTime">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%# VIVHTime(Eval("UpdateTime")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Visible="False" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </tbody>
                </table>

            </div>
            <br />
            <table width="450" align="center">
                <tr>
                    <td>&nbsp;<asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>
</body>

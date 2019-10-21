<%@ Page Language="C#" AutoEventWireup="true" CodeFile="driver_application_audit.aspx.cs" Inherits="cdb_borrow_cdb_borrow_auditing_driver_application_audit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>申请人信息浏览
    </title>
    <style type="text/css">
        .auto-style1 {
            height: 114px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/mytable.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="申请人信息浏览"></asp:Label></td>
                    </tr>
                </table>
                <hr />
            </div>
            <div class="contentTable">
                <table width="50%" cellpadding="8" cellspacing="1" align="center">
                    <thead>
                        <tr>
                            <td colspan="4">申请人信息
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td colspan="2">申请人姓名：
                                <asp:Label ID="lblName" runat="server" Width="350px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">申请人手机号：
                                <asp:Label ID="lblPhone" runat="server" Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">申请的时间：
                                <asp:Label ID="lblCreateTIme" runat="server" />月
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="50%" cellpadding="8" cellspacing="1" align="center">
                    <thead>
                        <tr>
                            <td colspan="5">申请审核记录
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td colspan="5" class="auto-style1">
                                <asp:TextBox ID="edtAuditRemark" runat="server" Width="100%" SkinID="MultiLine" TextMode="MultiLine" Rows="5" Text="请在此填写审核意见...." onchange="clear(this)" />
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="5" nowrap>&nbsp;<asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" OnClientClick="return confirm('确认审核通过吗?')" Text="审核通过" />
                                &nbsp;&nbsp;<asp:Button ID="btnUnAudit" runat="server" Text=" 审核不通过 " OnClientClick="return confirm('您确认审核不通过吗?')" OnClick="btnUnAudit_Click" />
                                &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text=" 返 回 " OnClick="btnCancel_Click" Style="height: 21px" />
                            </td>
                        </tr>
                    </tfoot>
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
</html>


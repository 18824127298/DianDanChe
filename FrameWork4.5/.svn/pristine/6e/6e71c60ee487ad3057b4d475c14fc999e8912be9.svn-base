<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dept_edit.aspx.cs" Inherits="framework_organize_dept_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/diary.gif" />
                            编辑部门/成员单位 - [<asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label>]：
                            </td>
                    </tr>
                </table>
                <hr />
            </div>
            <div class="contentTable">
                <table border="0" width="450" cellpadding="4" cellspacing="1" align="center">
                    <tbody>
                        <tr>
                            <td nowrap>
                                部门名称：</td>
                            <td>
                                &nbsp;<asp:TextBox ID="edtDeptName" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td nowrap>
                                显示顺序：</td>
                            <td>
                                &nbsp;<asp:TextBox ID="edtListOrder" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td nowrap>
                                备注：</td>
                            <td>
                                &nbsp;<asp:TextBox ID="edtRemarks" runat="server" Width="300px" Height="75px" SkinID="multiLine" TextMode="MultiLine"></asp:TextBox></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap>
                                &nbsp;<asp:Button ID="btnOK" runat="server" CssClass="normalButton" Text="保存修改" OnClick="btnOK_Click" /></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <table width="450" align="center">
                <tr>
                    <td>
                        &nbsp;<asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td>
                </tr>
            </table>
            <br />
            <div class="titleTable">
                <table border="0" width="100%" cellspacing="0" cellpadding="3" class="small">
                    <tr>
                        <td class="Big">
                            <img src="../../images/menu_icon/dept.gif" width="16" height="16" align="absmiddle"><span
                                class="big3"> 当前部门/成员单位 - 相关操作</span>
                        </td>
                    </tr>
                </table>
            </div>
            <br>
            <div align="center">
                <asp:Button ID="btnCreateNewDept" runat="server" OnClick="btnCreateNewDept_Click"
                    Text="新建下级部门/成员单位" /><br />
                <br>
                <asp:Button ID="btnDeleteDept" runat="server" Text="删除当前部门/成员单位" OnClick="btnDeleteDept_Click" />&nbsp;</div>
                <table width="350" align="center">
                    <tr>
                        <td>
                            &nbsp;<asp:Label ID="lblDeleteMsg" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td>
                    </tr>
                </table>
        </div>
    </form>
</body>
</html>

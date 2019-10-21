<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu_edit.aspx.cs" Inherits="framework_menu_navigate_menu_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>菜单编辑</title>
</head>

<script type="text/javascript">
        function ShowWindow()
        {
            var ret= window.showModalDialog("uploadMenuIcon.aspx","","dialogWidth=300px;dialogHeight=300px");
            if(ret!=null)
            {
                var nLastIndex=ret.lastIndexOf('/');
                var imgMenuIcon=ret.substring(nLastIndex+1);
                document.getElementById("hfImg").value=imgMenuIcon;
                document.getElementById("edtMenuIcon").value=imgMenuIcon; 
                document.getElementById("imgMenuIcon").src=ret;           
            }
        }
</script>

<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hfImg" runat="server" />
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/diary.gif" />
                            编辑菜单 - [<asp:Label ID="lblMenuName" runat="server" Text=""></asp:Label>]：
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
                                菜单编码：
                            </td>
                            <td>
                                &nbsp;<asp:TextBox ID="edtMenuCode" runat="server" Width="150px" />
                            </td>
                            <td nowrap>
                                菜单名称：</td>
                            <td>
                                &nbsp;<asp:TextBox ID="edtMenuName" runat="server" Width="150px" /></td>
                        </tr>
                        <tr>
                            <td nowrap>
                                菜单图标：
                            </td>
                            <td>
                                &nbsp;<asp:Image ID="imgMenuIcon" runat="server" /><asp:TextBox ID="edtMenuIcon"
                                    runat="server" Width="100px" /><input type="button" value="更换" onclick="ShowWindow()"
                                        class="normalButton" style="height: 22px" />
                            </td>
                            <td nowrap>
                                显示顺序：</td>
                            <td>
                                &nbsp;<asp:TextBox ID="edtListOrder" runat="server" Width="50px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td nowrap>
                                菜单链接：</td>
                            <td colspan="3">
                                &nbsp;<asp:TextBox ID="edtMenuLink" runat="server" Width="400px" /></td>
                        </tr>
                        <tr>
                            <td nowrap>
                                菜单选项：
                            </td>
                            <td colspan="3">
                                &nbsp;<asp:CheckBox ID="ckbIsActive" runat="server" Checked="true" Text="是否激活" />
                                &nbsp;<asp:CheckBox ID="ckbIsMenuItem" runat="server" Checked="true" Text="是否菜单" />
                                &nbsp;<asp:CheckBox ID="ckbIsLogItem" runat="server" Checked="true" Text="是否日志" />
                                &nbsp;<asp:CheckBox ID="ckbIsRightItem" runat="server" Checked="true" Text="是否权限" />
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="4" nowrap>
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
                                class="big3"> 当前菜单 - 相关操作</span>
                        </td>
                    </tr>
                </table>
            </div>
            <br>
            <div align="center">
                <asp:Button ID="btnCreateNewMenu" runat="server" Text="新建下级菜单/子菜单" Visible="true"
                    OnClick="btnCreateNewMenu_Click" /><br />
                <br>
                <asp:Button ID="btnDeleteMenu" runat="server" Visible="true" Text="删除当前菜单/子菜单" OnClick="btnDeleteMenu_Click" />&nbsp;</div>
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

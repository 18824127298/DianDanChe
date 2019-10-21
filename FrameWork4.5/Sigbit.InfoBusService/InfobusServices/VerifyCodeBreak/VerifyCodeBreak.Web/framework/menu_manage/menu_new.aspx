<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu_new.aspx.cs" Inherits="framework_menu_navigate_menu_new" %>

<html>
<head id="Head1" runat="server">
    <title>新建部门信息</title>
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
                            新建菜单 - 当前节点：[<asp:Label ID="lblMenuName" runat="server" Text="当前节点名"></asp:Label>]</td>
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
                                &nbsp;<asp:Image ID="imgMenuIcon" ImageUrl="../../images/menu_icon/struct.gif" runat="server" />
                                <asp:TextBox ID="edtMenuIcon"  runat="server" Width="70px" />
                                <input type="button" value="图标" onclick="ShowWindow()" class="normalButton" style="height: 22px" />
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
                                &nbsp;<asp:Button ID="btnOK" runat="server" CssClass="normalButton" Text=" 保存 "  OnClick="btnOK_Click" /></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <br />
            <table width="450" align="center">
                <tr>
                    <td>
                        &nbsp;<asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

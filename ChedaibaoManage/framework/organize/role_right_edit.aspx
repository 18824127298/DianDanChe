<%@ Page Language="C#" AutoEventWireup="true" CodeFile="role_right_edit.aspx.cs" Inherits="framework_organize_role_right_edit" %>

<!--DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>编辑角色权限</title>
</head>
<script language="javascript">
    function check_all(menu_all,MENU_ID)
    {
        for (i=0;i<document.all(MENU_ID).length;i++)
        {
            if(menu_all.checked)
                document.all(MENU_ID).item(i).checked=true;
            else
                document.all(MENU_ID).item(i).checked=false;
        }

        if(i==0)
        {
            if(menu_all.checked)
                document.all(MENU_ID).checked=true;
            else
                document.all(MENU_ID).checked=false;
        }
    }
</script>
<body>
    <form id="form1" action="role_right_edit_doupdate.aspx" method="post">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td style="height: 30px">
                        <img src="../../images/menu_icon/comm.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="编辑角色权限 — （角色名称）"></asp:Label>
                        &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                        <input id="Submit1" class="normalButton" type="submit" value=" 确定 " />
                        &nbsp;&nbsp;&nbsp; &nbsp;
                        <input id="Button1" class="normalButton" type="button" value=" 返回 " onclick="location='role_manage_list.aspx'" /></td>
                </tr>
            </table>
            <hr />
        </div>
        <table border="0" cellspacing="2" cellpadding="3" align="center">
        <tr>
            <asp:Literal ID="literalRightCBX" runat="server"></asp:Literal></tr>
        <tr>
          <td align="center" colspan="50" class="controlPanel">
              <input id="hidSBMXRoleUid" type="hidden" runat="server" />
                        <input id="Submit2" class="normalButton" type="submit" value=" 确定 " />
                        &nbsp;&nbsp;&nbsp; &nbsp;
                        <input id="Button2" class="normalButton" type="button" value=" 返回 " onclick="location='role_manage_list.aspx'" />
          </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>

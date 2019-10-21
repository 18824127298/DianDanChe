<%@ Page Language="C#" AutoEventWireup="true" CodeFile="new_request_notice.aspx.cs" Inherits="ibx_voice_reg_manual_input_reg_result_new_request_notice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <EMBED src="voices/short_notice.wav" loop="true" width="0" height="0" />    
        
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/info.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="新的请求到达"></asp:Label></td>
                </tr>
            </table>
            <hr />
        </div>

        <div class="contentTable">
        <br />
        <table border="0" width="450" cellpadding="4" cellspacing="1" align="center">
        <tbody>
            <tr>
                <td align="center" style="font-size: 40px; color: #FF0000;">
                    &nbsp;有新的请求到达</td>
            </tr>
        </tbody>
        <tfoot>
            <tr align="center">
                <td nowrap>
                    <asp:Button ID="btnBackToFillPage" runat="server" Text="转到填写界面" 
                        onclick="btnBackToFillPage_Click" />
                </td>
            </tr>
        </tfoot>
        </table>
        </div>
        
    </div>
    </form>
</body>
</html>

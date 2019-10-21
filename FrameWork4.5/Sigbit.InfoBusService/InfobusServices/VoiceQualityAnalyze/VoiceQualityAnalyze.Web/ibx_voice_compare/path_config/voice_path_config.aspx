<%@ Page Language="C#" AutoEventWireup="true" CodeFile="voice_path_config.aspx.cs"
    Inherits="ibx_voice_reg_manual_input_reg_result_new_request_notice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>语音根目录配置</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="titleTable">
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <img src="../../images/menu_icon/info.gif" />
                        <asp:Label ID="lblTitle" runat="server" Text="语音根目录配置"></asp:Label>
                    </td>
                </tr>
            </table>
            <hr />
        </div>
        <div class="contentTable">
            <br />
            <table border="0" width="600" cellpadding="4" cellspacing="1" align="center">
                <tbody>
                    <tr>
                        <td>
                            语音路径：
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="edtRootUrl" runat="server" Width="450px" />
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr align="center">
                        <td nowrap colspan="2">
                            <asp:Button ID="btnOK" runat="server" Text=" 确 定 " OnClick="btnOK_Click" 
                                style="height: 26px" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="module_map_html_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script type="text/javascript" language="javascript">
  
function showvalue()
{            
    debugger;
    var oText = document.getElementById("Text1"); 
//    oText.value = HiddenALatitude.value + HiddenALongitude.value + HiddenBLatitude.value+ HiddenBLongitude.value;
// 
    alert("a:" + oText.value);
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="font-size：">
            纬度：<asp:TextBox ID="edtLatitude" Text="23.1263" runat="server"></asp:TextBox><br />
            经度：<asp:TextBox ID="edtLongitude" Text="113.3683" runat="server"></asp:TextBox><br />
              
            纬度：<asp:TextBox ID="edtLatitudeB" Text="23.1263" runat="server"></asp:TextBox><br />
            经度：<asp:TextBox ID="edtLongitudeB" Text="113.3683" runat="server"></asp:TextBox><br />
            <asp:Button ID="Button1" runat="server" Text="查看点位置" OnClick="Button1_Click" /><br />
            <asp:Button ID="Button2" runat="server" Text="查看点线图" OnClick="Button2_Click" /><br />
            <asp:Button ID="Button3" runat="server" Text="查看围栏图" OnClick="Button3_Click" /><br />
            <asp:Button ID="Button4" runat="server" Text="查看围栏图" OnClick="Button4_Click" />
        </div>
    </form>
</body>
</html>

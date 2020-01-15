<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="提还" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="正常还" OnClick="Button2_Click" />
            <asp:Button ID="Button3" runat="server" Text="逾期" OnClick="Button3_Click" Style="width: 40px" />
            <asp:Button ID="BtnWeChat" runat="server" Text="微信推送消息" OnClick="BtnWeChat_Click" />
            <asp:Button ID="Btnjjr" runat="server" Text="判断是否为节假日" OnClick="Btnjjr_Click" />
            <asp:Button ID="BtnPhoto" runat="server" Text="图片的大小" OnClick="BtnPhoto_Click" />
            <asp:Button ID="BtnJieQu" runat="server" Text="截取" OnClick="BtnJieQu_Click" />
            <asp:Button ID="BtnXiaZai" runat="server" Text="下载" OnClick="BtnXiaZai_Click" />

            <asp:Button ID="btnBaoCun" runat="server" Text="保存" OnClick="btnBaoCun_Click" Width="40px" />
            <asp:Button ID="btnYaSuo" runat="server" Text="压缩" OnClick="btnYaSuo_Click" Style="height: 21px" />


            <asp:Button ID="btnSendSms" runat="server" Text="发送提醒短信" Style="height: 21px" OnClick="btnSendSms_Click" />
            <asp:Button ID="btnSave" runat="server" Text="单一标的保存" Style="height: 21px" OnClick="btnSave_Click" />


            <asp:Button ID="btnFangKuang" runat="server" Text="放款" OnClick="btnFangKuang_Click" Style="height: 21px" />

            <asp:Button ID="btnPiChuLi" runat="server" Text="补跑批处理" OnClick="btnPiChuLi_Click" />

            <asp:Button ID="btnHuiKuang" runat="server" Text="测试发送客户支付成功提醒" OnClick="btnHuiKuang_Click" style="margin-bottom: 0px" />
            <asp:Button ID="btnResult" runat="server" Text="测试客户融资租赁审核结果通知" OnClick="btnResult_Click"/>
            <asp:Button ID="btnOilPaymentReminder" runat="server" Text="测试油卡支付成功提醒" OnClick="btnOilPaymentReminder_Click"/>
            
            <asp:Button ID="btn_dayinji" runat="server" Text="测试打印机" OnClick="btn_dayinji_Click" />
            <asp:Button ID="btnDownLoad" runat="server" Text=" 下载 " OnClick="btnDownLoad_Click" />
            
            <asp:Button ID="btn_buzijin" runat="server" Text=" 补资金 " OnClick="btn_buzijin_Click" style="height: 21px"  />
        </div>
    </form> 
</body>
</html>

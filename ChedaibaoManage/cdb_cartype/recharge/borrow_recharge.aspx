<%@ Page Language="C#" AutoEventWireup="true" CodeFile="borrow_recharge.aspx.cs" Inherits="cdb_cartype_recharge_borrow_recharge" %>

<%@ Register Src="../../module/My97DatePicker.ascx" TagName="My97DatePicker" TagPrefix="uc1" %>

<html>
<head id="Head1" runat="server">
    <title>客户线下充值</title>
    <style>
        .input-text {
            height: 40px;
        }
    </style>
    <link href="../../module/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="../../module/plugins/select2/select2.css" rel="stylesheet" />
    <script src="../../js/jquery-1.8.2.min.js"></script>
    <script src="../../module/plugins/select2/select2.js"></script>

</head>
<script>

    function FormatAmount(control) {

        document.getElementById("divAmountCny").innerHTML = atoc(control.value);

    }

    function FormatFee(control) {

        document.getElementById("divActualRechargeFee").innerHTML = atoc(control.value);

    }


    function RemarkSelect(control) {
        document.getElementById("edtRemark").value = control.value;
    }

    function atoc(numberValue) {
        var numberValue = new String(Math.round(numberValue * 100)); // 数字金额
        var chineseValue = ""; // 转换后的汉字金额
        var String1 = "零壹贰叁肆伍陆柒捌玖"; // 汉字数字
        var String2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; // 对应单位
        var len = numberValue.length; // numberValue 的字符串长度
        var Ch1; // 数字的汉语读法
        var Ch2; // 数字位的汉字读法
        var nZero = 0; // 用来计算连续的零值的个数
        var String3; // 指定位置的数值
        if (len > 15) {
            alert("超出计算范围");
            return "";
        }
        if (numberValue == 0) {
            chineseValue = "零元整";
            return chineseValue;
        }
        String2 = String2.substr(String2.length - len, len); // 取出对应位数的STRING2的值
        for (var i = 0; i < len; i++) {
            String3 = parseInt(numberValue.substr(i, 1), 10); // 取出需转换的某一位的值
            if (i != (len - 3) && i != (len - 7) && i != (len - 11) && i != (len - 15)) {
                if (String3 == 0) {
                    Ch1 = "";
                    Ch2 = "";
                    nZero = nZero + 1;
                }
                else if (String3 != 0 && nZero != 0) {
                    Ch1 = "零" + String1.substr(String3, 1);
                    Ch2 = String2.substr(i, 1);
                    nZero = 0;
                }
                else {
                    Ch1 = String1.substr(String3, 1);
                    Ch2 = String2.substr(i, 1);
                    nZero = 0;
                }
            }
            else { // 该位是万亿，亿，万，元位等关键位
                if (String3 != 0 && nZero != 0) {
                    Ch1 = "零" + String1.substr(String3, 1);
                    Ch2 = String2.substr(i, 1);
                    nZero = 0;
                }
                else if (String3 != 0 && nZero == 0) {
                    Ch1 = String1.substr(String3, 1);
                    Ch2 = String2.substr(i, 1);
                    nZero = 0;
                }
                else if (String3 == 0 && nZero >= 3) {
                    Ch1 = "";
                    Ch2 = "";
                    nZero = nZero + 1;
                }
                else {
                    Ch1 = "";
                    Ch2 = String2.substr(i, 1);
                    nZero = nZero + 1;
                }
                if (i == (len - 11) || i == (len - 3)) { // 如果该位是亿位或元位，则必须写上
                    Ch2 = String2.substr(i, 1);
                }
            }
            chineseValue = chineseValue + Ch1 + Ch2;
        }
        if (String3 == 0) { // 最后一位（分）为0时，加上“整”
            chineseValue = chineseValue + "整";
        }
        return chineseValue;
    }


</script>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/pen.gif" />
                            线下充值提单</td>
                    </tr>
                </table>
                <hr />
            </div>

            <div class="contentTable">
                <table border="1" width="800" cellpadding="8" cellspacing="1" align="center">
                    <thead>
                        <tr>
                            <td colspan="2" align="center">线下充值提单
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td nowrap>会员手机号：</td>
                            <td>&nbsp;
                                <input type="text" id="edtBorrowPhone" style="width: 350px;" onblur="fn_findname()" />
                                <label id="LblBorrowName" runat="server"></label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">充入银行：</td>
                            <td style="padding-left: 27px">
                               <asp:TextBox ID="edtBankCard" runat="server" Width="350px" Text="" />
                                <asp:Label ID="LblBankCardId" runat="server"  hidden="hidden"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">充值流水号：</td>
                            <td style="padding-left: 27px">
                                <asp:TextBox ID="edtOrderNumber" runat="server" Width="300px" Text="" />&nbsp;(可填写银行交易流水号)
                            </td>
                        </tr>
                        <tr>
                            <td align="right">到帐金额：</td>
                            <td style="padding-left: 27px">
                                <asp:TextBox ID="edtAmount" runat="server" onkeyup="FormatAmount(this)" Width="100px" Text="0" />&nbsp;<span id="divAmountCny" style="color: blue;"></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">到账时间：</td>
                            <td style="padding-left: 27px">
                                <uc1:My97DatePicker ID="dpRechargeTime" runat="server" MaxDate="now" ShowDateFmt="yyyy-MM-dd HH:mm:ss" />
                                &nbsp;(对应该笔记录到帐时间)
                            </td>
                        </tr>
                        <%-- <tr>
                            <td align="right">银行手续费：</td>
                            <td style="padding-left: 27px">
                                <asp:TextBox ID="edtActualRechargeFee" runat="server" onkeyup="FormatFee(this)" Width="100px" Text="0" />&nbsp;<span id="divActualRechargeFee" style="color: blue;"></span>
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="right">备注：
                            </td>
                            <td style="padding-left: 27px">
                                <asp:TextBox ID="edtRemark" runat="server" Width="500px" SkinID="MultiLine" TextMode="MultiLine" Rows="3" />

                                &nbsp; <%--<a href="../../wangdai_site_set/quick_note/quick_note_modify.aspx" class="btn btn-primary" target="_blank">添加</a>--%></td>
                        </tr>

                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap>&nbsp;<asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" OnClientClick="return confirm('您确认给当前用户进行线下充值吗？')" Width="80px" />
                                &nbsp; &nbsp; &nbsp;
                            </td>
                        </tr>
                    </tfoot>
                </table>
                <table width="450" align="center">
                    <tr>
                        <td>&nbsp;<asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
<script type="text/javascript">
    var fn_findname = function () {
        $.ajax({
            type: 'get',
            url: "borrow_recharge.aspx?r=" + Math.random(),
            data: {
                Action: 'FindName',
                'Phone': $("#edtBorrowPhone").val()
            },
            success: function (result) {
                var json = JSON.parse(result);
                $("#LblBorrowName").text(json.FullName);
                $("input[id*=edtBankCard]").val(json.BankCard);
                //$(<%=LblBankCardId.ClientID%>).text(json.BankCardId);
            }
        });
    }
</script>
<script>
    $(function () {
        $("#ddlRemark").change(function () {
            $("#edtRemark").val($(this).val());
        });

    })
</script>
</html>

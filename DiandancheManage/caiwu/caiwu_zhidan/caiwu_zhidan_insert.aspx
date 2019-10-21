<%@ Page Language="C#" AutoEventWireup="true" CodeFile="caiwu_zhidan_insert.aspx.cs" Inherits="caiwu_caiwu_zhidan_caiwu_zhidan_insert" %>

<%@ Register Src="../../module/My97DatePicker.ascx" TagName="My97DatePicker" TagPrefix="uc1" %>

<html>
<head id="Head1" runat="server">
    <title>后台线下充值</title>
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
                            <img src="../../images/menu_icon/asset.gif" />
                            后台线下充值</td>
                    </tr>
                </table>
                <hr />
            </div>

            <div class="contentTable">
                <table border="1" width="800" cellpadding="8" cellspacing="1" align="center">
                    <thead>
                        <tr>
                            <td colspan="2" align="center">后台线下充值
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td align="right">用户：</td>
                            <td style="padding-left: 27px">
                                <div class="span3">
                                    <input type="hidden" runat="server" id="BorrowerId" name="AjaxPageOfSearch.BorrowerId" style="width: 187px" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">充值金额：</td>
                            <td style="padding-left: 27px">
                                <asp:TextBox ID="edtAmount" runat="server" onkeyup="FormatAmount(this)" Width="100px" Text="0" />&nbsp;<span id="divAmountCny" style="color: blue;"></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">备注：
                            </td>
                            <td style="padding-left: 27px">
                                <asp:TextBox ID="edtRemark" runat="server" Width="500px" SkinID="MultiLine" TextMode="MultiLine" Rows="3" />
                            </td>
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
    $(function () {
        $('#BorrowerId').select2({
            placeholder: "请选择一个用户",
            minimumInputLength: 0,
            ajax: {
                url: "caiwu_zhidan_insert.aspx?r=" + Math.random(),
                dataType: 'json',
                data: function (term, page) {
                    return {
                        Action: 'GetBorrower',
                        Borrower: term
                    };
                },
                selectOnBlur: true,
                results: function (data, page) {
                    return {
                        results: data
                    };
                }
            }
        });
        $('a.select2-choice>span').html("<%=GetBorrowerName()%>");

    })
</script>
<script>
    $(function () {
        $("#ddlRemark").change(function () {
            $("#edtRemark").val($(this).val());
        });

        $("#edtBankCardNumber").keypress(function (event) {

            var e = event || window.event || arguments.callee.caller.arguments[0];
            var keycode = parseInt(e.keyCode || e.which || e.charCode);
            if (keycode != 8 && keycode != 9 && (keycode < 48 || keycode > 57))
                return false;
            if ($(this).val().length >= 23 && keycode != 8)
                return false;
        }).keyup(function () {
            $(this).val($(this).val().replace(/[^\d]*/g, '').replace(/(\d{4})(?=\d)/g, "$1 "));
        });

    })
</script>
</html>

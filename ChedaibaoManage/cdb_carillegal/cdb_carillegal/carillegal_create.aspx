<%@ Page Language="C#" AutoEventWireup="true" CodeFile="carillegal_create.aspx.cs" Inherits="cdb_carillegal_cdb_carillegal_carillegal_create" %>

<%@ Register Src="../../module/My97DatePicker.ascx" TagName="My97DatePicker" TagPrefix="uc1" %>
<html>
<head id="Head1" runat="server">
    <title>会员汽车违章录入</title>
    <script src="../../js/jquery-1.8.2.min.js"></script>
    <script type="text/javascript">
        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]);
            return null;
        }

        $(function () {
            if (GetQueryString("rec_key")) {
                $.ajax({
                    type: 'get',
                    url: 'carillegal_create.aspx?r=' + Math.random(),
                    data: {
                        Action: 'Get',
                        'rec_key': GetQueryString("rec_key")
                    },
                    success: function (data) {
                        var json = JSON.parse(data);
                        $("#edtCarSystem").val(json.CarSystem);
                        $("#edtCarNumber").val(json.CarNumber);
                        $("#edtEngineNumber").val(json.EngineNumber);
                        $("#edtBodyRackNumber").val(json.BodyRackNumber);
                        $("#edtRemark").val(json.Remark);
                    }
                });
            }
        });


        var fn_sure = function () {
            $.ajax({
                type: 'get',
                url: "carillegal_create.aspx?r=" + Math.random(),
                data: {
                    Action: 'Save',
                    'BorrowPhone':$("#edtBorrowPhone").val(),
                    'LicensePlate': $("#edtLicensePlate").val(),
                    'IllegalTitle': $("#edtIllegalTitle").val(),
                    'IllegalDescribe': $("#edtIllegalDescribe").val(),
                    'IllegalAddress': $("#edtIllegalAddress").val(),
                    'IllegalTime': $("input[id*=dpIllegalTime]").val(),
                    'FinePrice': $("#edtFinePrice").val(),
                    'AroundFee': $("#edtAroundFee").val(),
                    'Points': $("#edtPoints").val()
                },
                success: function (result) {
                    alert(result);
                    location.href = "cdb_carillegal_list.aspx";
                }
            });
        }

        var fn_findname = function () {
            $.ajax({
                type: 'get',
                url: "carillegal_create.aspx?r=" + Math.random(),
                data: {
                    Action: 'FindName',
                    'Phone': $("#edtBorrowPhone").val()
                },
                success: function (result) {
                    $("#LblBorrowName").text(result);
                }
            });
        }
    </script>
    <style type="text/css">
        #Sure {
            margin-bottom: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/picture.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="会员汽车违章录入"></asp:Label>
                    </tr>
                </table>
                <hr />
            </div>
            <div class="contentTable">
                <br />
                <table border="0" width="450" cellpadding="8" cellspacing="1" align="center">
                    <tbody>
                        <tr>
                            <td nowrap>会员手机号：</td>
                            <td>&nbsp;
                                <input type="text" id="edtBorrowPhone" style="width: 350px;" onblur="fn_findname()" />
                                <label id="LblBorrowName"></label>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>车牌号：</td>
                            <td>&nbsp;
                                <input type="text" id="edtLicensePlate" style="width: 350px;" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>违规的标题：</td>
                            <td>&nbsp;
                                <input type="text" id="edtIllegalTitle" style="width: 350px;" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>违规的描述：</td>
                            <td>&nbsp;
                                <input type="text" id="edtIllegalDescribe" style="width: 350px;" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>违章的地点：</td>
                            <td>&nbsp;
                                <input type="text" id="edtIllegalAddress" style="width: 100px;" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>违章的时间：</td>
                            <td>&nbsp;
                               <uc1:My97DatePicker ID="dpIllegalTime" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>罚款的金额：</td>
                            <td>&nbsp;
                                <input type="text" id="edtFinePrice" style="width: 350px;" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>手续费：</td>
                            <td>&nbsp;
                                <input type="text" id="edtAroundFee" style="width: 350px;" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>分数：</td>
                            <td>&nbsp;
                                <input type="text" id="edtPoints" style="width: 100px;" />
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap>&nbsp;
                                <input type="button" value="确定" id="Sure" onclick="fn_sure()" />
                                &nbsp; &nbsp; &nbsp;
                                <asp:HiddenField ID="hid" runat="server" />
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <br />
            <table width="450" align="center">
                <tr>
                    <td>&nbsp;<asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>




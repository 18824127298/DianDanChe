<%@ Page Language="C#" AutoEventWireup="true" CodeFile="borrow_car_create.aspx.cs" Inherits="cdb_carillegal_cbd_car_borrow_car_create" %>

<html>
<head id="Head1" runat="server">
    <title>会员汽车录入</title>
    <link href="../../js/uploadify/uploadify.css" rel="stylesheet" type="text/javascript" />
    <script src="../../js/jquery-1.8.2.min.js"></script>
    <script src="../../js/uploadify/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <script src="../../js/uploadify/jquery.uploadify-3.1.js"></script>
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
                    url: 'borrow_car_create.aspx?r=' + Math.random(),
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


        $(function () {
            pic = $("#Hpic").val() + "|";
            GetMain_Pic();
            $("#uploadify").uploadify({
                'swf': '../../js/uploadify/uploadify.swf?ver=' + Math.random(),
                'uploader': '../../module/upload/AjaxuploadHandler.ashx',
                'buttonText': '上传图片',
                'fileTypeDesc': 'Image Files',
                'fileTypeExts': '*.gif; *.jpg; *.png',
                'formData': {
                    'file_size_limit': '20480',
                    'save_path': 'Cartype'
                },
                'queueID': 'fileQueue',
                'auto': true,
                'multi': Boolean.parse($("#HidMulti").val()),
                'onUploadError': function (file, errorCode, errorMsg, errorString) {
                    alert('文件：' + file.name + ' 上传失败: ' + errorString + errorCode + errorMsg);
                },
                'onUploadSuccess': function (file, data, response) {
                    $('#' + file.id).find('.data').html(' 上传完毕');
                    var Item = data.split('|')[1].toString();
                    var newval = "";
                    var oldval = $("#Hpic").val();
                    if (Boolean.parse($("#HidMulti").val()) == false) {
                        oldval = "";
                    }
                    if (oldval.split('|').length == 1 && oldval != "" && Item == "")
                        newval = oldval + "|";
                    else if (oldval.split('|').length == 1 && oldval != "" && Item != "")
                        newval = oldval + "|" + Item + "|";
                    else if (oldval.split('|').length != 1 && Item == "")
                        newval = oldval;
                    else if (oldval.split('|').length != 1 && Item != "")
                        newval = oldval + Item + "|";
                    else if (oldval.split('|').length == 1 && oldval == "" && Item != "")
                        newval = Item + "|";
                    else newval = "";
                    $("#Hpic").val(newval);
                    pic = $("#Hpic").val();
                    if (pic != "") {
                        var arr = new Array();
                        var html = "";
                        arr = pic.split('|');
                        $("#imgHtml").val("");
                        for (var i = 0; i < arr.length - 1; i++) {
                            html += "<img src='" + arr[i].toString() + "' id='" + i.toString() + "img' width='300px' height='300px' style=\"margin-bottom: 10px;\">";
                        }
                        $("#imgHtml").html(html);
                    }
                }
            });
        });
        var pic = "";

        var GetMain_Pic = function () {
            var html = "";
            if (pic != "") {
                var arr = new Array();
                arr = pic.split('|');
                for (var i = 0; i < arr.length - 1; i++) {
                    html += "<img src='" + arr[i].toString() + "' id='" + i.toString() + "img' width='300px' height='300px'  style=\"margin-bottom: 10px;\">";
                }
            } else {
                html += "<img src='" + pic + "' id='1img' width='300px' height='300px' > <input type='button' value='删除图片'   />";
            }
            $("#imgHtml").html(html);
            $("#FileName0").val($("#HidFileName").val());
        }

        var Del_Pic = function (url) {
            if (url != "") {
                $("#Hpic").val(pic.replace(url + "|", ""));
            }
            pic = $("#Hpic").val();
            GetMain_Pic();
        }

        Boolean.parse = function (str) {
            switch (str.toLowerCase()) {
                case "true":
                    return true;
                case "false":
                    return false;
                default:
                    throw new Error("Boolean.parse: Cannot convert string to boolean.");
            }
        };

        var fn_sure = function () {
            $.ajax({
                type: 'get',
                url: "borrow_car_create.aspx?r=" + Math.random(),
                data: {
                    Action: 'Save',
                    'Hpic': $("#Hpic").val(),
                    'BorrowPhone': $("#edtBorrowPhone").val(),
                    'CarSystem': $("#edtCarSystem").val(),
                    'CarNumber': $("#edtCarNumber").val(),
                    'EngineNumber': $("#edtEngineNumber").val(),
                    'BodyRackNumber': $("#edtBodyRackNumber").val(),
                    'Remark': $("#edtRemark").val()
                },
                success: function (result) {
                    alert(result);
                    location.href = "car_type_list.aspx";
                }
            });
        }

        var fn_findname = function () {
            $.ajax({
                type: 'get',
                url: "borrow_car_create.aspx?r=" + Math.random(),
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/picture.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="会员汽车录入"></asp:Label>
                    </tr>
                </table>
                <hr />
            </div>
            <asp:HiddenField ID="HidMulti" runat="server" Value="false" />
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
                            <td nowrap>车系：</td>
                            <td>&nbsp;
                                <input type="text" id="edtCarSystem" style="width: 350px;" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>车牌号：</td>
                            <td>&nbsp;
                                <input type="text" id="edtCarNumber" style="width: 350px;" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>发动机号码：</td>
                            <td>&nbsp;
                                <input type="text" id="edtEngineNumber" style="width: 350px;" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>车身架号码：</td>
                            <td>&nbsp;
                                <input type="text" id="edtBodyRackNumber" style="width: 100px;" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>备注：</td>
                            <td>&nbsp;
                                <input type="text" id="edtRemark" style="width: 100px;" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>图片：</td>
                            <td>&nbsp;
                                <input type="file" name="uploadify" id="uploadify" />
                                <div id="fileQueue">
                                </div>
                                <div id="imgHtml">
                                </div>
                                <input type="hidden" id="Hpic" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap>&nbsp;
                                <input type="button" value="确定" id="Sure" onclick="fn_sure()" />
                                &nbsp; &nbsp; &nbsp;
                    <%--<asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />--%>
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


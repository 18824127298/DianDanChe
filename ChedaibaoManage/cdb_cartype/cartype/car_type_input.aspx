<%@ Page Language="C#" AutoEventWireup="true" CodeFile="car_type_input.aspx.cs" Inherits="cdb_cartype_cartype_car_type_input" %>

<html>
<head id="Head1" runat="server">
    <title>车型的录入</title>
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
            $.ajax({
                type: 'get',
                url: 'car_type_input.aspx?r=' + Math.random(),
                data: {
                    Action: 'GetCarModels'
                },
                success: function (data) {
                    var json = JSON.parse(data);
                    for (var i = 0; i < json.length; i++) {
                        $("#selCarModels").append("<option value='" + json[i].Id + "'>" + json[i].Name + "</option>");
                    }
                }
            });
            if (GetQueryString("rec_key")) {
                $.ajax({
                    type: 'get',
                    url: 'car_type_input.aspx?r=' + Math.random(),
                    data: {
                        Action: 'Get',
                        'rec_key': GetQueryString("rec_key")
                    },
                    success: function (data) {
                        var json = JSON.parse(data);
                        $("#edtCarName").val(json.CarName);
                        $("#selCarModels").val(json.OperateModelId);
                        $("#edtLiftFares").val(json.LiftFares);
                        $("#edtRetainage").val(json.Retainage);
                        $("#edtMonthPrice").val(json.MonthPrice);
                        $("#edtCarTitle").val(json.CarTitle);
                        $("#edtStages").val(json.Stages);
                        $("#Hpic").val(json.ImageUrl);
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
                url: "car_type_input.aspx?r=" + Math.random(),
                data: {
                    Action: 'Save',
                    'Hpic': $("#Hpic").val(),
                    'CarName': $("#edtCarName").val(),
                    'CarTitle': $("#edtCarTitle").val(),
                    'CarModels': $("#edtCarModels").val(),
                    'LiftFares': $("#edtLiftFares").val(),
                    'Retainage': $("#edtRetainage").val(),
                    'MonthPrice': $("#edtMonthPrice").val(),
                    'Stages': $("#edtStages").val()
                },
                success: function (result) {
                    alert(result);
                    location.href = "car_type_list.aspx";
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
                            <asp:Label ID="lblTitle" runat="server" Text="汽车类型录入"></asp:Label>
                            <asp:Label ID="lblFilePath" CssClass="style" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <hr />
            </div>
            <asp:HiddenField ID="HidMulti" runat="server" Value="false" />
            <asp:HiddenField ID="hidThumbnail" runat="server" />
            <asp:HiddenField ID="HidFileName" runat="server" />
            <div class="contentTable">
                <br />
                <table border="0" width="450" cellpadding="8" cellspacing="1" align="center">
                    <tbody>
                        <tr>
                            <td nowrap>文件：</td>
                            <td>&nbsp;
                                <input type="file" name="uploadify" id="uploadify" />
                                <div id="fileQueue">
                                </div>
                                <div id="imgHtml">
                                </div>
                                <input type="hidden" id="Hpic" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>车名：</td>
                            <td>&nbsp;
                                <input type="text" id="edtCarName" style="width: 350px;" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>车标题：</td>
                            <td>&nbsp;
                                <input type="text" id="edtCarTitle" style="width: 350px;" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>车型：</td>
                            <td>&nbsp;
                                <select id="selCarModels" style="width:100px">
                                </select>
                                <%--<input type="text" id="edtCarModels" style="width: 350px;" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>提车费：</td>
                            <td>&nbsp;
                                <input type="text" id="edtLiftFares" style="width: 100px;" />元
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>尾款：</td>
                            <td>&nbsp;
                                <input type="text" id="edtRetainage" style="width: 100px;" />元
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>月供：</td>
                            <td>&nbsp;
                                <input type="text" id="edtMonthPrice" style="width: 100px;" />元
                            </td>
                        </tr>
                        <tr>
                            <td nowrap>期次：</td>
                            <td>&nbsp;
                                <input type="text" id="edtStages" style="width: 100px;" />期
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr align="center">
                            <td colspan="2" nowrap>&nbsp;
                                <input type="button" value="确定" id="Sure" onclick="fn_sure()" />
                                &nbsp; &nbsp; &nbsp;
                    <%--<asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />--%></td>
                            <asp:HiddenField ID="hid" runat="server" />
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


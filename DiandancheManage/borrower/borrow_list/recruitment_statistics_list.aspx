<%@ Page Language="C#" AutoEventWireup="true" CodeFile="recruitment_statistics_list.aspx.cs" Inherits="borrower_borrow_list_recruitment_statistics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .w800 {
            width: 790px;
        }

            .w800 h1 {
                font-size: 18px;
                font-weight: normal;
                padding: 10px 0 10px 10px;
                color: #0e6eb8;
            }

            .member_center .border, .w800.border {
                border: 0;
            }


        .pd20 {
            overflow: hidden;
            padding: 20px;
        }

        .table01 th {
            background-color: #009ae2;
            padding: 10px 8px;
            font-size: 14px;
            text-align: center;
            border: 1px solid #fff;
            color: #FFF;
        }

        .table01 td {
            padding: 8px 5px;
            text-align: center;
            font-size: 14px;
            border: 1px solid #dadada;
            color: #666;
        }

            .table01 td a {
                color: #0d60a1;
            }

                .table01 td a:hover {
                    color: #f47a2d;
                }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class=" w800 fr border" style="width: 100%">
            <div class="news-title">
                <h3>招聘站点单量统计列表</h3>
            </div>
            <div class="pd20">
                <table width="100%" border="0" class="table01" id="FinanceLogTable">
                    <thead>
                        <tr>
                            <th colspan="11">2019</th>
                            <th colspan="3">2020</th>
                        </tr>
                        <tr>
                            <th>招聘点名称</th>
                            <th>三月</th>
                            <th>四月</th>
                            <th>五月</th>
                            <th>六月</th>
                            <th>七月</th>
                            <th>八月</th>
                            <th>九月</th>
                            <th>十月</th>
                            <th>十一月</th>
                            <th>十二月</th>
                            <th>一月</th>
                            <th>二月</th>
                            <th>总计</th>
                        </tr>
                    </thead>
                    <tbody id="ECLTAB">
                    </tbody>
                </table>
            </div>
        </div>
    </form>
</body>
<script src="../../js/jquery-1.8.2.min.js"></script>
<script type="text/javascript">
    var html = "";
    $(function () {
        $.ajax({
            type: 'get',
            async: false,
            url: 'recruitment_statistics_list.aspx?r=' + Math.random(),
            data: {
                Action: 'RecruitmentStatisticsList'
            },
            success: function (data) {
                var json = JSON.parse(data);
                var JanuaryAmount = 0;
                var FebruaryAmount = 0;
                var MarchAmount = 0;
                var AprilAmount = 0;
                var MayAmount = 0;
                var JuneAmount = 0;
                var JulyAmount = 0;
                var AugustAmount = 0;
                var SeptemberAmount = 0;
                var OctoberAmount = 0;
                var NovemberAmount = 0;
                var DecemberAmount = 0;
                var sumAmount = 0;
                for (var i = 0; i < json.length; i++) {
                    JanuaryAmount += parseInt(json[i].January);
                    FebruaryAmount += parseInt(json[i].February);
                    MarchAmount += parseInt(json[i].March);
                    AprilAmount += parseInt(json[i].April);
                    MayAmount += parseInt(json[i].May);
                    JuneAmount += parseInt(json[i].June);
                    JulyAmount += parseInt(json[i].July);
                    SeptemberAmount += parseInt(json[i].September);
                    AugustAmount += parseInt(json[i].August);
                    OctoberAmount += parseInt(json[i].October);
                    NovemberAmount += parseInt(json[i].November);
                    DecemberAmount += parseInt(json[i].December);
                    var sumAmount1 = parseInt(json[i].January) + parseInt(json[i].February) + parseInt(json[i].March) + parseInt(json[i].April) + parseInt(json[i].May) + parseInt(json[i].June)
                         + parseInt(json[i].July) + parseInt(json[i].August) + parseInt(json[i].September) + parseInt(json[i].October) + parseInt(json[i].November) + parseInt(json[i].December);
                    html += "<tr>";
                    html += "    <td style='color:#ff0000'>" + json[i].RecruitmentName + "</td>";
                    if (json[i].March > 0)
                        html += "    <td style='color:#ff0000'>" + json[i].March + "</td>";
                    else
                        html += "    <td>" + json[i].March + "</td>";
                    if (json[i].April > 0)
                        html += "    <td style='color:#ff0000'>" + json[i].April + "</td>";
                    else
                        html += "    <td>" + json[i].April + "</td>";
                    if (json[i].May > 0)
                        html += "    <td style='color:#ff0000'>" + json[i].May + "</td>";
                    else
                        html += "    <td>" + json[i].May + "</td>";
                    if (json[i].June > 0)
                        html += "    <td style='color:#ff0000'>" + json[i].June + "</td>";
                    else
                        html += "    <td>" + json[i].June + "</td>";
                    if (json[i].July > 0)
                        html += "    <td style='color:#ff0000'>" + json[i].July + "</td>";
                    else
                        html += "    <td>" + json[i].July + "</td>";
                    if (json[i].August > 0)
                        html += "    <td style='color:#ff0000'>" + json[i].August + "</td>";
                    else
                        html += "    <td>" + json[i].August + "</td>";
                    if (json[i].September > 0)
                        html += "    <td style='color:#ff0000'>" + json[i].September + "</td>";
                    else
                        html += "    <td>" + json[i].September + "</td>";
                    if (json[i].October > 0)
                        html += "    <td style='color:#ff0000'>" + json[i].October + "</td>";
                    else
                        html += "    <td>" + json[i].October + "</td>";
                    if (json[i].November > 0)
                        html += "    <td style='color:#ff0000'>" + json[i].November + "</td>";
                    else
                        html += "    <td>" + json[i].November + "</td>";
                    if (json[i].December > 0)
                        html += "    <td style='color:#ff0000'>" + json[i].December + "</td>";
                    else
                        html += "    <td>" + json[i].December + "</td>";
                    if (json[i].January > 0)
                        html += "    <td style='color:#ff0000'>" + json[i].January + "</td>";
                    else
                        html += "    <td>" + json[i].January + "</td>";
                    if (json[i].February > 0)
                        html += "    <td style='color:#ff0000'>" + json[i].February + "</td>";
                    else
                        html += "    <td>" + json[i].February + "</td>";
                    html += "    <td style='color:#ff0000'>" + sumAmount1 + "</td>";
                    html += "</tr>";
                    sumAmount += sumAmount1;
                }
                html += "<tr>";
                html += "    <td style='color:#ff0000'>总计</td>";
                html += "    <td style='color:#ff0000'>" + MarchAmount + "</td>";
                html += "    <td style='color:#ff0000'>" + AprilAmount + "</td>";
                html += "    <td style='color:#ff0000'>" + MayAmount + "</td>";
                html += "    <td style='color:#ff0000'>" + JuneAmount + "</td>";
                html += "    <td style='color:#ff0000'>" + JulyAmount + "</td>";
                html += "    <td style='color:#ff0000'>" + AugustAmount + "</td>";
                html += "    <td style='color:#ff0000'>" + SeptemberAmount + "</td>";
                html += "    <td style='color:#ff0000'>" + OctoberAmount + "</td>";
                html += "    <td style='color:#ff0000'>" + NovemberAmount + "</td>";
                html += "    <td style='color:#ff0000'>" + DecemberAmount + "</td>";
                html += "    <td style='color:#ff0000'>" + JanuaryAmount + "</td>";
                html += "    <td style='color:#ff0000'>" + FebruaryAmount + "</td>";
                html += "    <td style='color:#ff0000'>" + sumAmount + "</td>";
                html += "</tr>";
                $("#ECLTAB").html(html);
            }
        });
    })

</script>
</html>

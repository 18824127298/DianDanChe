<%@ Page Language="C#" AutoEventWireup="true" CodeFile="expect_collected_list.aspx.cs" Inherits="borrower_borrow_list_expect_collected_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                <h3>预计每月回款金额列表</h3>
            </div>
            <div class="pd20">
                <table width="100%" border="0" class="table01" id="FinanceLogTable">
                    <thead>
                        <tr>
                            <th>年月份</th>
                            <th>预计回款融资租赁总额</th>
                            <th>预计回款手续费</th>
                            <th>预计回款融资租赁费</th>
                            <th>提还总金额</th>
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
            url: 'expect_collected_list.aspx?r=' + Math.random(),
            data: {
                Action: 'ExpectCollectedList'
            },
            success: function (data) {
                var json = JSON.parse(data);
                for (var i = 0; i < json.length; i++) {
                    html += "<tr>";
                    html += "    <td>" + json[i].RepaymentDate + "</td>";
                    html += "    <td>" + json[i].UnPrincipal + "</td>";
                    html += "    <td>" + json[i].UnTotalInterest + "</td>";
                    html += "    <td>" + json[i].SumAmount + "</td>";
                    html += "    <td>" + json[i].ThAmount + "</td>";
                    html += "</tr>";
                }
                $("#ECLTAB").html(html);
            }
        });
    })

</script>
</html>

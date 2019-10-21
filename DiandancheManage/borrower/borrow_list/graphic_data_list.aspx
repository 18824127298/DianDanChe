<%@ Page Language="C#" AutoEventWireup="true" CodeFile="graphic_data_list.aspx.cs" Inherits="borrower_borrow_list_graphic_data_list" %>


<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../css/newcss/base.css" rel="stylesheet" />
    <link href="../../css/newcss/gobal.css" rel="stylesheet" />
    <link href="../../css/newcss/npage.css" rel="stylesheet" />
    <script src="../../js/jquery-1.8.2.js"></script>
    <script src="../../js/newjs/echarts.js"></script>
</head>

<body>

    <div class="content" style="background: #f9fafb">
        <div class="wrap">
            <div class="treb-mls">
                <div class="tit" style="font-size: 28px;">每月成交量</div>
                <div id="trebChart" style="width: 1100px; height: 324px; margin: 0 auto"></div>
                <div class="selcomponent">
                    <select id="selChart">
                        <option value="2019">2019</option>
                        <option value="2018">2018</option>
                    </select>
                    <div style="font-size: 15px; color: #000; padding-top: 5px; text-align: center">可选择年限</div>
                </div>
            </div>

            <div class="treb-mls">
                <div class="tit" style="font-size: 28px;">每月成交笔数</div>
                <div id="trebChart1" style="width: 1100px; height: 324px; margin: 0 auto"></div>
                <div class="selcomponent">
                    <select id="selChart1">
                        <option value="2019">2019</option>
                        <option value="2018">2018</option>
                    </select>
                    <div style="font-size: 15px; color: #000; padding-top: 5px; text-align: center">可选择年限</div>
                </div>
            </div>
            <div class="trade-data">
                <div class="item-box">
                    <div class="item-tit" style="font-size: 28px;">
                        业务员本月单量
                    </div>
                    <div class="item-con">
                        <ul class="bchart clearfix">
                            <li id="threeB1" style="width:100%"></li>
                        </ul>
                    </div>
                </div>
            </div>
             
            <div class="trade-data">
                <div class="item-box">
                    <div class="item-tit" style="font-size: 28px;">
                        本月每日成交笔数
                    </div>
                    <div class="item-con">
                        <div id="montreb" style="width: 1100px; height: 324px; margin: 0 auto"></div>
                    </div>
                </div>
            </div>

            <div class="trade-data">
                <div class="item-box">
                    <div class="item-tit" style="font-size: 28px;">
                        还款金额
                    </div>
                    <div class="item-con">
                        <ul class="tab clearfix">
                            <li class="on">还款金额</li>
                            <%--                            <li>回款金额</li>
                            <li>平均满标时间</li>--%>
                        </ul>
                        <div id="moreChart" style="width: 1100px; height: 324px; margin: 0 auto"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
<script>
    $(function () {
        //每月成交量
        $.ajax({
            type: 'get',
            async: false,
            url: 'graphic_data_list.aspx?r=' + Math.random(),
            data: {
                Action: 'Turnover'
            },
            success: function (data) {
                var json = JSON.parse(data);
                var data2019 = json.lists19;
                var data2018 = [];
                var myChart1 = echarts.init(document.getElementById("trebChart"));
                var option1 = {
                    color: ['#fc9246'],
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                            type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                        }
                    },
                    xAxis: [
                        {
                            type: 'category',
                            data: function () {
                                var list = [];
                                for (var i = 1; i <= 12; i++) {
                                    list.push(i + '月');
                                }
                                return list;
                            }(),
                            axisTick: {
                                alignWithLabel: true
                            }
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value',
                            scale: true,
                            name: '单位（元）',
                            max: 1000000,
                            min: 0,
                            boundaryGap: [0.2, 0.2]
                        }
                    ],
                    series: [
                        {
                            name: '金额',
                            type: 'bar',
                            barWidth: '60%',
                            data: data2019
                        }
                    ]
                };
                myChart1.setOption(option1);
                $("#selChart").change(function () {
                    var val = $("#selChart option:selected").val();//获取选中的项
                    if (val === '2018') {
                        myChart1.setOption({ series: [{ data: data2018 }] });
                    } else if (val === '2019') {
                        myChart1.setOption({ series: [{ data: data2019 }] });
                    }
                });
            }
        });

        //每月成交笔数
        $.ajax({
            type: 'get',
            async: false,
            url: 'graphic_data_list.aspx?r=' + Math.random(),
            data: {
                Action: 'Transactions'
            },
            success: function (data) {
                var json = JSON.parse(data);
                var data2019 = json.lists19;
                var data2018 = [];
                var myChart1 = echarts.init(document.getElementById("trebChart1"));
                var option1 = {
                    color: ['#fc9246'],
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                            type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                        }
                    },
                    xAxis: [
                        {
                            type: 'category',
                            data: function () {
                                var list = [];
                                for (var i = 1; i <= 12; i++) {
                                    list.push(i + '月');
                                }
                                return list;
                            }(),
                            axisTick: {
                                alignWithLabel: true
                            }
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value',
                            scale: true,
                            name: '单位（笔）',
                            max: 500,
                            min: 0,
                            boundaryGap: [0.2, 0.2]
                        }
                    ],
                    series: [
                        {
                            name: '笔',
                            type: 'bar',
                            barWidth: '60%',
                            data: data2019
                        }
                    ]
                };
                myChart1.setOption(option1);
                $("#selChart1").change(function () {
                    var val = $("#selChart1 option:selected").val();//获取选中的项
                    if (val === '2018') {
                        myChart1.setOption({ series: [{ data: data2018 }] });
                    } else if (val === '2019') {
                        myChart1.setOption({ series: [{ data: data2019 }] });
                    }
                });
            }
        });

        // 近三个月标类型、期限、金额统计饼图
        $.ajax({
            type: 'get',
            async: false,
            url: 'graphic_data_list.aspx?r=' + Math.random(),
            data: {
                Action: 'CurrentMonthNumber'
            },
            success: function (data) {
                var json = JSON.parse(data);
                var myChart2 = echarts.init(document.getElementById("threeB1"));
                //var myChart3 = echarts.init(document.getElementById("threeB2"));
                //var myChart4 = echarts.init(document.getElementById("threeB3"));
                var option2 = {
                    legend: {
                        orient: 'vertical',
                        left: 'left',
                        data: json.namelists.split(',')
                    },
                    series: [
                        {
                            name: '访问来源',
                            type: 'pie',
                            radius: '55%',
                            center: ['50%', '60%'],
                            label: {
                                normal: {
                                    show: true,
                                    formatter: '{b}: {c}({d}%)'
                                }
                            },
                            data: JSON.parse(json.jolists)
                        }
                    ]
                };
                //var option3 = {
                //    legend: {
                //        orient: 'vertical',
                //        left: 'left',
                //        data: ['3月标', '6月标', '12月标', '>12月标']
                //    },
                //    series: [
                //        {
                //            name: '访问来源',
                //            type: 'pie',
                //            radius: '55%',
                //            center: ['50%', '60%'],
                //            data: [
                //                { value: "@ViewBag.ThreeCredit", name: '3月标' },
                //                { value: "@ViewBag.SixCredit", name: '6月标' },
                //                { value: "@ViewBag.TwelveCredit", name: '12月标' },
                //                { value: "@ViewBag.DTwelveCredit", name: '>12月标' }
                //            ]
                //        }
                //    ]
                //};
                //var option4 = {
                //    legend: {
                //        orient: 'vertical',
                //        left: 'left',
                //        data: ['10万以下', '10-15万', '15-20万', '20万及以上']
                //    },
                //    series: [
                //        {
                //            name: '访问来源',
                //            type: 'pie',
                //            radius: '55%',
                //            center: ['50%', '60%'],
                //            data: [
                //                { value: "@ViewBag.lessoht", name: '10万以下' },
                //                { value: "@ViewBag.lessfht", name: '10-15万' },
                //                { value: "@ViewBag.lesstht", name: '15-20万' },
                //                { value: "@ViewBag.greatertht", name: '20万及以上' }
                //            ]
                //        }
                //    ]
                //};
                myChart2.setOption(option2);
                //myChart3.setOption(option3);
                //myChart4.setOption(option4);
            }
        });
        //本月每天成交量
        $.ajax({
            type: 'get',
            async: false,
            url: 'graphic_data_list.aspx?r=' + Math.random(),
            data: {
                Action: 'DailyTurnover'
            },
            success: function (data) {
                var json = JSON.parse(data);
                var myChart5 = echarts.init(document.getElementById("montreb"));
                var option5 = {
                    color: ['#fc9246'],
                    legend: {
                        show: false,
                        left: 100
                    },
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                            type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                        }
                    },
                    grid: {
                        left: '3%',
                        right: '4%',
                        bottom: '3%',
                        containLabel: true
                    },
                    xAxis: [
                    {
                        type: 'category',
                        data: function () {
                            var list = [];
                            for (var i = 1; i <= mGetDate() ; i++) {
                                list.push(i + '日');
                            }
                            return list;
                        }(),
                        axisTick: {
                            alignWithLabel: true
                        }
                    }
                    ],
                    yAxis: [
                    {
                        type: 'value',
                        scale: true,
                        name: '单位（笔）',
                        max: 30,
                        min: 0,
                        boundaryGap: [0.1, 0.1]
                    }
                    ],
                    series: [
                    {
                        name: '笔数',
                        type: 'bar',
                        barWidth: '50%',
                        data: json.lists19
                    }
                    ]
                };
                myChart5.setOption(option5);
            }
        });
        // 更多统计
        $.ajax({
            type: 'get',
            async: false,
            url: 'graphic_data_list.aspx?r=' + Math.random(),
            data: {
                Action: 'MonthlyRepayment'
            },
            success: function (data) {
                var json = JSON.parse(data);
                var flag = -1;
                var myChart6 = echarts.init(document.getElementById("moreChart"));
                var option6 = {
                    title: {
                        text: ''
                    },
                    tooltip: {
                        trigger: 'axis'
                    },
                    legend: {
                        data: ['还款金额']
                    },
                    grid: {
                        left: '3%',
                        right: '4%',
                        bottom: '3%',
                        containLabel: true
                    },
                    toolbox: {
                        feature: {
                            saveAsImage: {}
                        }
                    },
                    xAxis: {
                        type: 'category',
                        boundaryGap: false,
                        data: function () {
                            var list = [];
                            for (var i = 1; i <= 12; i++) {
                                list.push(i + '月');
                            }
                            return list;
                        }()
                    },
                    yAxis: {
                        type: 'value',
                        scale: true,
                        name: '单位（元）',
                        max: 1000000,
                        min: 0,
                        boundaryGap: [0.1, 0.1]
                    },
                    series: [
                    {
                        name: '还款金额',
                        type: 'line',
                        stack: '总量',
                        data: json.lists19
                    }
                    ]
                };
                myChart6.setOption(option6);
            }
        });

    });
    function mGetDate() {
        var date = new Date();
        var year = date.getFullYear();
        var month = date.getMonth() + 1;
        var d = new Date(year, month, 0);
        return d.getDate();
    }
</script>


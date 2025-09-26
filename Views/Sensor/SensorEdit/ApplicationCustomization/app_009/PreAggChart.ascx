<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.PreAggregatePageModel>" %>

<%   
    StringBuilder trueCounts = new StringBuilder();
    StringBuilder falseCounts = new StringBuilder();

    bool Reverse = OpenClosed.GetMagnetPresentValue(Model.sensor.SensorID) == "Introduced";
    string OneValue = "Open";
    string ZeroValue = "Closed";
    if (!Reverse)
    {
        OneValue = "Closed";
        ZeroValue = "Open";
    }

    foreach (PreAggregatedData item in Model.PreAggregateList)
    {
        trueCounts.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.TrueCount + "],");
        falseCounts.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.FalseCount + "],");
    }
%>

<div id="sensorChartDiv" style="height: 350px;"></div>


<script type="text/javascript">

    $(document).ready(function () {

        var echartBar = echarts.init(document.getElementById('sensorChartDiv', 'infographic'));

        echartBar.setOption({
            baseOption:{
                tooltip: {
                    trigger: 'item',
                    formatter: function (params) {
                        var date = new Date(params.value[0]);
                
                        return "Date: " + echarts.format.formatTime('<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>', date, true) + '<br/>' +
                           'Count: ' + params.value[1];
                }
            },
                toolbox: {
                    feature: {
                        saveAsImage: {
                            show: false,
                            title: 'Save',
                        },
                    }
                },
                dataZoom: [
                        {
                            showDetail: false,
                            show: true,
                            realtime: true,
                            start: 0,
                            end: 100,

                        },
                       {
                           type: 'inside',
                           realtime: true,
                           start: 0,
                           end: 100,

                       }
                ],
                legend: { type: 'scroll',
                    top: 'top',
                    data: ['<%=OneValue%>', '<%=ZeroValue%>'],
            },
                grid: {
                    y2: 80,
                    left: 55,
                    right: 50,
                },
                xAxis: [{
                    type: 'time',
                    axisLabel: {
                        formatter: function (params) {
                            var date = new Date(params);
                            return echarts.format.formatTime('<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>', date, true);
                    }

                }
            }],
                yAxis: [{
                    type: 'value'
                }],
                series: [
                    {
                        name: '<%=OneValue%>',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=trueCounts.ToString().TrimEnd(',')%>]
                },
                 {
                     name: '<%=ZeroValue%>',
                     type: 'line',
                     showAllSymbol: true,
                     data: [<%=falseCounts.ToString().TrimEnd(',')%>]
                 }
            ]

            },
            media: [
                            {
                                option: {
                                    grid: {
                                        left: 55,
                                        top: '8%',
                                        right: 30,

                                    },
                                    xAxis: {
                                        nameLocation: 'end',
                                        nameGap: 10,
                                        splitNumber: 8,
                                        splitLine: {
                                            show: true
                                        }
                                    },
                                    yAxis: {
                                        splitNumber: 4,
                                        splitLine: {
                                            show: true
                                        }
                                    },
                                }
                            },
                            {
                                query: {
                                    maxWidth: 670, minWidth: 550
                                },
                                option: {
                                    grid: {
                                        left: 55,
                                        top: '8%',
                                        right: 30,
                                    },
                                    xAxis: {
                                        nameLocation: 'end',
                                        nameGap: 10,
                                        splitNumber: 5,
                                        splitLine: {
                                            show: true
                                        }
                                    },
                                    yAxis: {
                                        splitNumber: 4,
                                        splitLine: {
                                            show: true
                                        }
                                    },
                                }
                            },
                            {
                                query: {
                                    maxWidth: 550
                                },
                                option: {
                                    grid: {
                                        left: 55,
                                        top: '8%',
                                        right: 30,
                                    },
                                    xAxis: {
                                        nameLocation: 'middle',
                                        nameGap: 25,
                                        splitNumber: 2
                                    },
                                    yAxis: {
                                        splitNumber: 2,
                                        splitLine: {
                                            show: true
                                        }
                                    },
                                }
                            }
            ]
        });


        $(window).on('resize', function () {
            if (echartBar != null && echartBar != undefined) {
                echartBar.resize();
            }
        });

    });

</script>



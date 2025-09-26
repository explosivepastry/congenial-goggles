<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.PreAggregatePageModel>" %>

<%
    //   Battery PreAgg Chart

    StringBuilder dataVals = new StringBuilder();
    foreach (PreAggregatedData item in Model.PreAggregateList)
    {
        string battery = "";
        if (item.Avg_Voltage == 0) //Line Power Detected
        {
            battery = "100";
        }
        else
        {
            battery = PowerSource.Load(Model.sensor.PowerSourceID).Percent(item.Avg_Voltage).ToInt().ToString();
        }

        dataVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + battery + "],");
    }



%>
<div id="sensorChartDiv" style="height: 350px;"></div>


<script type="text/javascript">


    $(document).ready(function () {

        var echartBar = echarts.init(document.getElementById('sensorChartDiv', 'infographic'));

        echartBar.setOption({

            baseOption: {
                tooltip: {
                    trigger: 'item',
                    formatter: function (params) {
                        var date = new Date(params.value[0]);
                        return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                            'Battery: ' + params.value[1] + ' %';;
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
                legend: {
                    type: 'scroll',
                    top: 'top',
                    data: ['Average Daily Battery'],
                },
                grid: {
                    y2: 80,
                    left: 70,
                    right: 50,
                },
                xAxis: [{
                    type: 'time', axisLabel: {
                        formatter: function (params) {
                            var date = new Date(params);
                            return echarts.format.formatTime('<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>', date, true);
                        }

                    }
                }],
                yAxis: [{
                    type: 'value',
                    min: 0,
                    max: 100,
                    splitNumber: 8,
                    axisLabel: {
                        formatter: '{value} %'
                    }
                }],
                series: [
                    {
                    name: 'Average Daily Battery',
                    showAllSymbol: true,
                    type: 'line',
                    data: [<%=dataVals.ToString().TrimEnd(',')%>]
                    },
                    {
                        name: 'test',
                        showAllSymbol: true,
                        type: 'line',
                        data: []

                    }
                ]
            },
            media: [
                {
                    option: {
                        grid: {
                            left: 80,
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
                            splitNumber: 5,
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
                            left: 80,
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
                            left: 80,
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



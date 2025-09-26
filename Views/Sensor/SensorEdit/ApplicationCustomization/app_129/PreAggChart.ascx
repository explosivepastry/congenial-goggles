<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.PreAggregatePageModel>" %>

<%
    // 129 = ThreePhase500AmpMeter PreAgg Chart

    StringBuilder minVals = new StringBuilder();
    StringBuilder maxVals = new StringBuilder();
    StringBuilder avgVals = new StringBuilder();
    double VoltValue = ThreePhase500AmpMeter.GetVoltValue(Model.sensor.SensorID);
    string label = ThreePhase500AmpMeter.GetLabel(Model.sensor.SensorID);

    foreach (PreAggregatedData item in Model.PreAggregateList)
    {
        double min = item.Min;
        double max = item.Max;
        double avg = item.Avg;

        if (label == "Wh")
        {
            min = (min * VoltValue);
            max = (max * VoltValue);
            avg = (avg * VoltValue);
        }
        else if (label == "kWh")
        {
            min = (min * VoltValue / 1000.0);
            max = (max * VoltValue / 1000.0);
            avg = (avg * VoltValue / 1000.0);
        }

        minVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + min + "],");
        maxVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + max + "],");
        avgVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + avg + "],");
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

                        return "Date: " + echarts.format.formatTime('<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>', date, true) + '<br/>' +
                            'Value: ' + params.value[1] + ' <%=label%>';
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
                    data: ['Maximum', 'Minimum', 'Average'],
                },
                grid: {
                    y2: 80,
                    left: 55,
                    right: 50,
                },
                xAxis: [
                    {
                        type: 'time',
                        axisLabel: {
                            formatter: function (params) {
                                var date = new Date(params);
                                return echarts.format.formatTime('<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>', date, true);
                            }

                        }
                    }
                ],
                yAxis: [
                    {
                        name: 'TotalCurrentAccumulation',
                        x: 'left',
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value} <%=label == "Amp Hours" ? "Ah" : label%>'
                        }

                    },
                    {
                        name: 'Phase Average',
                        x: 'Right',
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        inverse: false,
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value} amps',
                        }
                    }
                ],
                series: [
                    {
                        name: 'Maximum',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=maxVals.ToString().TrimEnd(',')%>],

                    },
                    {
                        name: 'Minimum',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=minVals.ToString().TrimEnd(',')%>],

                    },
                    {
                        name: 'Average',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=avgVals.ToString().TrimEnd(',')%>],

                    },

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



<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.PreAggregatePageModel>" %>

<%
    // 072 = 0-5V Measure PreAgg Chart

    StringBuilder minVals = new StringBuilder();
    StringBuilder maxVals = new StringBuilder();
    StringBuilder avgVals = new StringBuilder();
    double LowValue = ZeroToFiveVolts.GetLowValue(Model.sensor.SensorID);
    double HighValue = ZeroToFiveVolts.GetHighValue(Model.sensor.SensorID);
    string label = ZeroToFiveVolts.GetLabel(Model.sensor.SensorID);

    foreach (PreAggregatedData item in Model.PreAggregateList)
    {
        minVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Min.LinearInterpolation(0, LowValue, 5, HighValue).ToString("#0.###", System.Globalization.CultureInfo.InvariantCulture) + "],");
        maxVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Max.LinearInterpolation(0, LowValue, 5, HighValue).ToString("#0.###", System.Globalization.CultureInfo.InvariantCulture) + "],");
        avgVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Avg.LinearInterpolation(0, LowValue, 5, HighValue).ToString("#0.###", System.Globalization.CultureInfo.InvariantCulture) + "],");
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
                yAxis: [{
                    type: 'value',
                    min: 'dataMin',
                    max: 'dataMax',
                    name: '<%=label%>',
                    axisLabel: {
                        showMinLabel: false,
                        showMaxLabel: false,
                        formatter: '{value}'
                    }
                }],
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



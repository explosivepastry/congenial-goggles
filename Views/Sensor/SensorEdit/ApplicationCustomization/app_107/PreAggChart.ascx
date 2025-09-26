<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.PreAggregatePageModel>" %>

<%
    

    StringBuilder Lux_minVals = new StringBuilder();
    StringBuilder Lux_maxVals = new StringBuilder();
    StringBuilder Lux_avgVals = new StringBuilder();

    StringBuilder LightDetect_TrueVals = new StringBuilder();
    StringBuilder LightDetect_FalseVals = new StringBuilder();


    foreach (PreAggregatedData item in Model.PreAggregateList)
    {
        switch (item.SplitValue)
        {

            case "Lux":
                Lux_minVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Min.ToString("#0.#") + ", 'Lux' ],");
                Lux_maxVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Max.ToString("#0.#") + ", 'Lux' ],");
                Lux_avgVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Avg.ToString("#0.#") + ", 'Lux' ],");
                break;
            case "LightDetect":
                LightDetect_TrueVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.TrueCount + ", 'Detect' ],");
                LightDetect_FalseVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.FalseCount + ", 'Detect' ],");
                break;
            default:
                break;
        }

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

                        if (params.value[2] == 'Lux') {

                            return "Date: " + echarts.format.formatTime('<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>', date, true) + '<br/>' +
                                'Lux: ' + params.value[1];
                    } else {

                        return "Date: " + echarts.format.formatTime('<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>', date, true) + '<br/>' +
                               'Count: ' + params.value[1];
                    }
                }
            },
                toolbox: {
                    feature: { saveAsImage: { show: false, title: 'Save', }, }
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
                    type: 'scroll',
                    top: -5,
                    data:
                        ['Lux Max', 'Lux Min', 'Lux Avg',
                         'Light', 'No Light'
                        ],

                },
                grid: {
                    y2: 80,
                    left: 65,
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
                        name: 'Lux',
                        x: 'left',
                        type: 'value',
                        axisLabel: {
                            formatter: '{value}'
                        }

                    },
                    {
                        name: 'Counts',
                        x: 'Right',
                        type: 'value',
                        inverse: false,
                        axisLabel: {
                            formatter: '{value}'
                        }
                    }
                ],
                series: [
                      {
                          name: 'Lux Max',
                          type: 'line',
                          showAllSymbol: true,
                          data: [<%=Lux_maxVals.ToString().TrimEnd(',')%>],

                  },
                {
                    name: 'Lux Min',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=Lux_minVals.ToString().TrimEnd(',')%>],

                },
                   {
                       name: 'Lux Avg',
                       type: 'line',
                       showAllSymbol: true,
                       data: [<%=Lux_avgVals.ToString().TrimEnd(',')%>],

                   },
                  {
                      name: 'Light',
                      type: 'line',
                      showAllSymbol: true,
                      yAxisIndex: 1,
                      data: [<%=LightDetect_TrueVals.ToString().TrimEnd(',')%>],

                  },
                {
                    name: 'No Light',
                    type: 'line',
                    showAllSymbol: true,
                    yAxisIndex: 1,
                    data: [<%=LightDetect_FalseVals.ToString().TrimEnd(',')%>],

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



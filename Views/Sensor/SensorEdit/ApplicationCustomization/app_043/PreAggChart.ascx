<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.PreAggregatePageModel>" %>

<%
    bool isF = Temperature.IsFahrenheit(Model.sensor.SensorID);

    StringBuilder Temperature_minVals = new StringBuilder();
    StringBuilder Temperature_maxVals = new StringBuilder();
    StringBuilder Temperature_avgVals = new StringBuilder();

    StringBuilder Humidity_minVals = new StringBuilder();
    StringBuilder Humidity_maxVals = new StringBuilder();
    StringBuilder Humidity_avgVals = new StringBuilder();

    foreach (PreAggregatedData item in Model.PreAggregateList)
    {
        switch (item.SplitValue)
        {

            case "Temperature":
                Temperature_minVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + (isF ? item.Min.ToFahrenheit() : item.Min).ToString("#0.#") + ", 'Temperature' ],");
                Temperature_maxVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + (isF ? item.Max.ToFahrenheit() : item.Max).ToString("#0.#") + ", 'Temperature' ],");
                Temperature_avgVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + (isF ? item.Avg.ToFahrenheit() : item.Avg).ToString("#0.#") + ", 'Temperature' ],");
                break;
            case "Humidity":
                Humidity_minVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Min.ToString("#0.##") + ", 'Humidity' ],");
                Humidity_maxVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Max.ToString("#0.##") + ", 'Humidity' ],");
                Humidity_avgVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Avg.ToString("#0.##") + ", 'Humidity' ],");
                break;
            default:
                break;
        }

    }


        
%>

<div id="sensorChartDiv" style="height: 350px;"></div>


<script type="text/javascript">


    $(document).ready(function () {

        var tempScale = '<%:isF?"F":"C" %>';


        var echartBar = echarts.init(document.getElementById('sensorChartDiv', 'infographic'));

        echartBar.setOption({

            baseOption: {
                tooltip: {
                    trigger: 'item',
                    formatter: function (params) {
                        var date = new Date(params.value[0]);
                        if (params.value[2] == 'Temperature') {

                            return "Date: " + echarts.format.formatTime('<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>', date, true) + '<br/>' +
                               'Temperature: ' + params.value[1] + ' °' + tempScale;
                    } else {

                        return "Date: " + echarts.format.formatTime('<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>', date, true) + '<br/>' +
                               'Humidity: ' + params.value[1] + ' %';
                    }
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
                    data:
                    ['Humidity Max', 'Humidity Min', 'Humidity Avg',
                     'Temperature Max', 'Temperature Min', 'Temperature Avg'
                    ],

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
                yAxis: [
                    {
                        name: 'Humidity',
                        x: 'left',
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        axisLabel: {
                            formatter: '{value} %'
                        }

                    },
                    {
                        name: 'Temperature',
                        x: 'Right',
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        inverse: false,
                        axisLabel: {
                            formatter: '{value} °' + tempScale
                        }
                    }
                ],
                series: [
                      {
                          name: 'Humidity Max',
                          type: 'line',
                          showAllSymbol: true,
                          data: [<%=Humidity_maxVals.ToString().TrimEnd(',')%>],

                  },
                {
                    name: 'Humidity Min',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=Humidity_minVals.ToString().TrimEnd(',')%>],

                },
                   {
                       name: 'Humidity Avg',
                       type: 'line',
                       showAllSymbol: true,
                       data: [<%=Humidity_avgVals.ToString().TrimEnd(',')%>],

                   },
                  {
                      name: 'Temperature Max',
                      type: 'line',
                      showAllSymbol: true,
                      yAxisIndex: 1,
                      data: [<%=Temperature_maxVals.ToString().TrimEnd(',')%>],

                  },
                {
                    name: 'Temperature Min',
                    type: 'line',
                    showAllSymbol: true,
                    yAxisIndex: 1,
                    data: [<%=Temperature_minVals.ToString().TrimEnd(',')%>],

                },
                   {
                       name: 'Temperature Avg',
                       type: 'line',
                       showAllSymbol: true,
                       yAxisIndex: 1,
                       data: [<%=Temperature_avgVals.ToString().TrimEnd(',')%>],

                   }

            ]
            },
            media: [
                            {
                                option: {
                                    grid: {
                                        left: 55,
                                        top: '8%',
                                        right: 55,

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



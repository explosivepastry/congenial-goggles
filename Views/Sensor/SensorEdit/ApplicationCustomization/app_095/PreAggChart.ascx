<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.PreAggregatePageModel>" %>

<%
   

    StringBuilder X_minSpeedVals = new StringBuilder();
    StringBuilder X_maxSpeedVals = new StringBuilder();
    StringBuilder X_avgSpeedVals = new StringBuilder();
    StringBuilder Y_minSpeedVals = new StringBuilder();
    StringBuilder Y_maxSpeedVals = new StringBuilder();
    StringBuilder Y_avgSpeedVals = new StringBuilder();
    StringBuilder Z_minSpeedVals = new StringBuilder();
    StringBuilder Z_maxSpeedVals = new StringBuilder();
    StringBuilder Z_avgSpeedVals = new StringBuilder();
    StringBuilder X_minFrequencyVals = new StringBuilder();
    StringBuilder X_maxFrequencyVals = new StringBuilder();
    StringBuilder X_avgFrequencyVals = new StringBuilder();
    StringBuilder Y_minFrequencyVals = new StringBuilder();
    StringBuilder Y_maxFrequencyVals = new StringBuilder();
    StringBuilder Y_avgFrequencyVals = new StringBuilder();
    StringBuilder Z_minFrequencyVals = new StringBuilder();
    StringBuilder Z_maxFrequencyVals = new StringBuilder();
    StringBuilder Z_avgFrequencyVals = new StringBuilder();
    foreach (PreAggregatedData item in Model.PreAggregateList)
    {
        switch (item.SplitValue)
        {
            case "X-Axis Frequency":
                X_minFrequencyVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Min + ", 'X-Axis Frequency' ],");
                X_maxFrequencyVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Max + ", 'X-Axis Frequency' ],");
                X_avgFrequencyVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Avg + ", 'X-Axis Frequency' ],");
                break;
            case "Y-Axis Frequency":
                Y_minFrequencyVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Min + ", 'Y-Axis Frequency' ],");
                Y_maxFrequencyVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Max + ", 'Y-Axis Frequency' ],");
                Y_avgFrequencyVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Avg + ", 'Y-Axis Frequency' ],");
                break;
            case "Z-Axis Frequency":
                Z_minFrequencyVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Min + ", 'Z-Axis Frequency' ],");
                Z_maxFrequencyVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Max + ", 'Z-Axis Frequency' ],");
                Z_avgFrequencyVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Avg + ", 'Z-Axis Frequency' ],");
                break;
            case "X-Axis Speed":
                X_minSpeedVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Min + ", 'X-Axis Speed' ],");
                X_maxSpeedVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Max + ", 'X-Axis Speed' ],");
                X_avgSpeedVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Avg + ", 'X-Axis Speed' ],");
                break;
            case "Y-Axis Speed":
                Y_minSpeedVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Min + ", 'Y-Axis Speed' ],");
                Y_maxSpeedVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Max + ", 'Y-Axis Speed' ],");
                Y_avgSpeedVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Avg + ", 'Y-Axis Speed' ],");
                break;
            case "Z-Axis Speed":
                Z_minSpeedVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Min + ", 'Z-Axis Speed' ],");
                Z_maxSpeedVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Max + ", 'Z-Axis Speed' ],");
                Z_avgSpeedVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Avg + ", 'Z-Axis Speed' ],");
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

                        var ReturnString = "Date: " + echarts.format.formatTime('<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>', date, true) + '<br/>';


                    switch (params.value[2]) {
                        case "X-Axis Frequency":
                            ReturnString = ReturnString + 'X-Axis Frequency: ' + params.value[1] + ' Hz';
                            break;
                        case "Y-Axis Frequency":
                            ReturnString = ReturnString + 'Y-Axis Frequency: ' + params.value[1] + ' Hz';
                            break;
                        case "Z-Axis Frequency":
                            ReturnString = ReturnString + 'Z-Axis Frequency: ' + params.value[1] + ' Hz';
                            break;
                        case "X-Axis Speed":
                            ReturnString = ReturnString + 'X-Axis Speed: ' + params.value[1] + ' mm/s';
                            break;
                        case "Y-Axis Speed":
                            ReturnString = ReturnString + 'Y-Axis Speed: ' + params.value[1] + ' mm/s';
                            break;
                        case "Z-Axis Speed":
                            ReturnString = ReturnString + 'Z-Axis Speed: ' + params.value[1] + ' mm/s';
                            break;
                        default:
                            break;

                    }

                    return ReturnString;

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
                    type: 'scroll',
                    top: -5,
                    data:
                        ['X Speed Max', 'X Speed Min', 'X Speed Avg',
                           'Y Speed Max', 'Y Speed Min', 'Y Speed Avg',
                           'Z Speed Max', 'Z Speed Min', 'Z Speed Avg',
                           'X Freq Max', 'X Freq Min', 'X Freq Avg',
                           'Y Freq Max', 'Y Freq Min', 'Y Freq Avg',
                           'Z Freq Max', 'Z Freq Min', 'Z Freq Avg'
                        ],

                },
                grid: {
                    top:20,
                    y2: 80,
                    left: 55,
                    right: 50,
                },
                xAxis: [
                    {
                        type: 'time',
                        boundaryGap: false,
                        axisLine: { onZero: false },
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
                        name: 'Speed (mm/s)',
                        x: 'left',
                        type: 'value', min: 'dataMin', max: 'dataMax',
                        axisLabel: {
                            formatter: '{value} '
                        }
                    },
                    {
                        name: 'Frequency (Hz)                 ',
                        type: 'value', min: 'dataMin', max: 'dataMax',
                        inverse: false,
                        axisLabel: {
                            formatter: '{value} '
                        }
                    }
                ],
                series: [
                      {
                          name: 'X Speed Max',
                          type: 'line',
                          showAllSymbol: true,
                          data: [<%=X_maxSpeedVals.ToString().TrimEnd(',')%>],

                  },
                {
                    name: 'X Speed Min',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=X_minSpeedVals.ToString().TrimEnd(',')%>],

                },
                   {
                       name: 'X Speed Avg',
                       type: 'line',
                       showAllSymbol: true,
                       data: [<%=X_avgSpeedVals.ToString().TrimEnd(',')%>],

                   },
                  {
                      name: 'Y Speed Max',
                      type: 'line',
                      showAllSymbol: true,
                      data: [<%=Y_maxSpeedVals.ToString().TrimEnd(',')%>],

                  },
                {
                    name: 'Y Speed Min',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=Y_minSpeedVals.ToString().TrimEnd(',')%>],

                },
                   {
                       name: 'Y Speed Avg',
                       type: 'line',
                       showAllSymbol: true,
                       data: [<%=Y_avgSpeedVals.ToString().TrimEnd(',')%>],

                   },
                  {
                      name: 'Z Speed Max',
                      type: 'line',
                      showAllSymbol: true,
                      data: [<%=Z_maxSpeedVals.ToString().TrimEnd(',')%>],

                  },
                {
                    name: 'Z Speed Min',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=Z_minSpeedVals.ToString().TrimEnd(',')%>],

                },
                   {
                       name: 'Z Speed Avg',
                       type: 'line',
                       showAllSymbol: true,
                       data: [<%=Z_avgSpeedVals.ToString().TrimEnd(',')%>],

                   },
                  {
                      name: 'X Freq Max',
                      type: 'line',
                      yAxisIndex: 1,
                      showAllSymbol: true,
                      data: [<%=X_maxFrequencyVals.ToString().TrimEnd(',')%>],

                  },
                {
                    name: 'X Freq Min',
                    type: 'line',
                    yAxisIndex: 1,
                    showAllSymbol: true,
                    data: [<%=X_minFrequencyVals.ToString().TrimEnd(',')%>],

                },
                   {
                       name: 'X Freq Avg',
                       type: 'line',
                       yAxisIndex: 1,
                       showAllSymbol: true,
                       data: [<%=X_avgFrequencyVals.ToString().TrimEnd(',')%>],

                   },
                  {
                      name: 'Y Freq Max',
                      type: 'line',
                      yAxisIndex: 1,
                      showAllSymbol: true,
                      data: [<%=Y_maxFrequencyVals.ToString().TrimEnd(',')%>],

                  },
                {
                    name: 'Y Freq Min',
                    type: 'line',
                    yAxisIndex: 1,
                    showAllSymbol: true,
                    data: [<%=Y_minFrequencyVals.ToString().TrimEnd(',')%>],

                },
                   {
                       name: 'Y Freq Avg',
                       type: 'line',
                       yAxisIndex: 1,
                       showAllSymbol: true,
                       data: [<%=Y_avgFrequencyVals.ToString().TrimEnd(',')%>],

                   },
                  {
                      name: 'Z Freq Max',
                      type: 'line',
                      showAllSymbol: true,
                      data: [<%=Z_maxFrequencyVals.ToString().TrimEnd(',')%>],

                  },
                {
                    name: 'Z Freq Min',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=Z_minFrequencyVals.ToString().TrimEnd(',')%>],

                },
                   {
                       name: 'Z Freq Avg',
                       type: 'line',
                       showAllSymbol: true,
                       data: [<%=Z_avgFrequencyVals.ToString().TrimEnd(',')%>],

                   }

            ]
            },
            media: [
                            {
                                option: {
                                    grid: {
                                        left: 55,
                                        top: '18%',
                                        right: 45,

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
                                        top: '18%',
                                        right: 45,
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
                                        top: '18%',
                                        right: 45,
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



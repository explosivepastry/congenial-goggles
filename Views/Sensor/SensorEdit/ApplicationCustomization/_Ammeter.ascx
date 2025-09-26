<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%
  
    

    

    StringBuilder CurrentAccDataVals = ViewBag.CurrentAccDataVals;
    StringBuilder Phase1AverageDataVals = ViewBag.Phase1AverageDataVals;
    StringBuilder Phase2AverageDataVals = ViewBag.Phase2AverageDataVals;
    StringBuilder Phase3AverageDataVals = ViewBag.Phase3AverageDataVals;
    string label = ViewBag.Label;

     
%>

<div id="sensorChartDiv" style="height: 350px;"></div>


<script type="text/javascript">


    $(document).ready(function () {

        var fontSize = 12;
        var legendItemHeight = 14;
        var legendItemWidth = 25;

        echartBar = echarts.init(document.getElementById('sensorChartDiv', 'infographic'));

        echartBar.setOption({

            baseOption: {
                tooltip: {
                    trigger: 'item',
                    formatter: function (params) {
                        var date = new Date(params.value[0]);

                        var dateString = "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>';
                        var statusString = 'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                        var valueString = '';
                        switch (params.value[3]) {
                            case 'CurrentAccumulation':
                                valueString = 'Total Current Accumulation: ' + params.value[1] + ' <%=label%>' + '<br/>';
                                break;
                            case 'Phase1Average':
                                valueString = 'Phase 1 Average: ' + params.value[1] + ' amps' + '<br/>';
                                break;
                            case 'Phase2Average':
                                valueString = 'Phase 2 Average: ' + params.value[1] + ' amps' + '<br/>';
                                break;
                            case 'Phase3Average':
                                valueString = 'Phase 3 Average: ' + params.value[1] + ' amps' + '<br/>';
                                break;
                            default:
                                break;
                        }

                        return dateString + valueString + statusString;

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
                    orient: 'horizontal',
                    top: -5,
                    left: 'center',
                    width: '60%',
                    formatter: function (x) {
                        //console.log(window.matchMedia("(max-width: 550px)").matches);
                        //console.log(x);
                        
                        
                        //console.log(x.match(/(Phase\d) Average/));

                        //if (window.matchMedia("(max-width: 550px)").matches) {
                        //    const phaseRegex = /(Phase\d) Average/;
                        //    var phaseRegexMatch = x.match(phaseRegex);
                        //    if (phaseRegexMatch)
                        //        return `${phaseRegexMatch[1]}Avg`
                        //    else
                        //        return '\u2211Current';
                            
                        //}
                        return x;
                    },
                    data: ['Total Current Accumulation', 'Phase1 Average', 'Phase2 Average', 'Phase3 Average'],
                },
                grid: {
                    y2: '22%',
                    left: '10%',
                    right: '10%',
                    top: '15%',
                },
                xAxis: [{
                    type: 'time', min: <%=MonnitSession.HistoryFromDate.ToEpochTime()%>, max: <%=MonnitSession.HistoryToDate.ToEpochTime()%>,
                    axisLabel: {
                        formatter: function (params) {
                            var date = new Date(params);
                            return TimePreferenceFormat(params, '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '\r\n' + DatePreferenceFormat(params, '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"]) %>');
                        }

                    }
                }],
                yAxis: [
                    {
                        name: 'Current Accumulation',
                        nameGap: 5,
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
                        nameGap: 5,
                        x: 'Right',
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        inverse: false,
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value} amps'
                        }
                    }
                ],
                series: [
                   {
                        name: 'Current Accumulation',
                        formatter: function (x) {
                            if (window.matchMedia("(min-width: 550px)").matches) {
                                return 'TtlCurrAcc';
                            }
                            return x;

                        },
                       type: 'line',
                       showAllSymbol: true,
                       data: [<%=CurrentAccDataVals.ToString().TrimEnd(',')%>],

                   },
                  {
                      name: 'Phase1 Average',
                      type: 'line',
                      showAllSymbol: true,
                      yAxisIndex: 1,
                      data: [<%=Phase1AverageDataVals.ToString().TrimEnd(',')%>],

                  },
                  {
                      name: 'Phase2 Average',
                      type: 'line',
                      showAllSymbol: true,
                      yAxisIndex: 1,
                      data: [<%=Phase2AverageDataVals.ToString().TrimEnd(',')%>],

                  },
                  {
                      name: 'Phase3 Average',
                      type: 'line',
                      showAllSymbol: true,
                      yAxisIndex: 1,
                      data: [<%=Phase3AverageDataVals.ToString().TrimEnd(',')%>],

                  }
                ]
            },
            media: [
                            {
                                option: {
                                    legend: {
                                        textStyle: {
                                            fontSize: fontSize,
                                        },
                                    },
                                    xAxis: {
                                        splitNumber: 8,
                                        axisLabel: {
                                            fontSize: fontSize,
                                        }
                                    },
                                    yAxis: [
                                        {
                                            nameTextStyle: {
                                                fontSize: fontSize,
                                            },
                                            axisLabel: {
                                                fontSize: fontSize,
                                                rotate: 0,
                                            },
                                            splitNumber: 4,
                                        },
                                        {
                                            nameTextStyle: {
                                                fontSize: fontSize,
                                            },
                                            axisLabel: {
                                                fontSize: fontSize,
                                                rotate: 0,
                                            },
                                            splitNumber: 4,
                                        }
                                    ],
                                }
                            },
                            {
                                query: {
                                    maxWidth: 670, minWidth: 550
                                },
                                option: {
                                    legend: {
                                        textStyle: {
                                            fontSize: fontSize - 1,
                                        },
                                    },
                                    xAxis: {
                                        splitNumber: 5,
                                        axisLabel: {
                                            fontSize: fontSize - 1,
                                        }
                                    },
                                    yAxis: [
                                        {
                                            nameTextStyle: {
                                                fontSize: fontSize - 1,
                                            },
                                            axisLabel: {
                                                fontSize: fontSize - 1,
                                            },
                                            splitNumber: 4,
                                        },
                                        {
                                            nameTextStyle: {
                                                fontSize: fontSize - 1,
                                            },
                                            axisLabel: {
                                                fontSize: fontSize - 1,
                                            },
                                                splitNumber: 4,
                                        }
                                    ],
                                }
                            },
                            {
                                query: {
                                    minWidth: 375, maxWidth: 550
                                },
                                option: {
                                    legend: {
                                        textStyle: {
                                            fontSize: fontSize - 2,
                                        },
                                    },
                                    xAxis: {
                                        splitNumber: 2,
                                        axisLabel: {
                                            fontSize: fontSize - 2,
                                        }
                                    },
                                    yAxis: [
                                        {
                                            nameTextStyle: {
                                                fontSize: fontSize - 2,
                                            },
                                            axisLabel: {
                                                fontSize: fontSize - 2,
                                            },
                                            splitNumber: 2,
                                        },
                                        {
                                            nameTextStyle: {
                                                fontSize: fontSize - 2,
                                            },
                                            axisLabel: {
                                                fontSize: fontSize - 2,
                                            },
                                            splitNumber: 2,
                                        }
                                    ]
                                }
                },
                {
                    query: {
                        maxWidth: 375
                    },
                    option: {
                        grid: {
                            left: '11%',
                        },
                        legend: {
                            width: '95%',
                            textStyle: {
                                fontSize: fontSize - 3,
                            },
                            itemHeight: legendItemHeight / 2,
                            itemWidth: legendItemWidth / 2,
                        },
                        xAxis: {
                            splitNumber: 2,
                            axisLabel: {
                                fontSize: fontSize - 3,
                            }
                        },
                        yAxis: [
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 3,
                                    padding: [0, 0, 0, 35],
                                },
                                axisLabel: {
                                    fontSize: fontSize - 3,
                                    rotate: 55,
                                },
                                splitNumber: 2,
                            },
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 3,
                                },
                                axisLabel: {
                                    fontSize: fontSize - 3,
                                    rotate: 55,
                                },
                                splitNumber: 2,
                            }
                        ],
                    }
                }
            ]
        });


        $(window).on('resize', function () {
            if (echartBar != null && echartBar != undefined) {
                echartBar.resize();
            }
        });
        echartBar.on('mouseover', function (params) {
            $('.message_' + params.value[4]).css("background", "lightgray"); //change second element background
            setTimeout(function () {
                $('.message_' + params.value[4]).css('background', 'white'); // change it back after ...
            }, 3000); // waiting one second
        });
    });

</script>



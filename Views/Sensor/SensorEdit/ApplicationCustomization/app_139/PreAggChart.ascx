<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.PreAggregatePageModel>" %>

<%


    StringBuilder PPFD_minVals = new StringBuilder();
    StringBuilder PPFD_maxVals = new StringBuilder();
    StringBuilder PPFD_avgVals = new StringBuilder();

    StringBuilder DLI_maxVals = new StringBuilder();

    //StringBuilder LightDetect_TrueVals = new StringBuilder();
    //StringBuilder LightDetect_FalseVals = new StringBuilder();


    foreach (PreAggregatedData item in Model.PreAggregateList)
    {
        switch (item.SplitValue)
        {

            case "PPFD":
                PPFD_minVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Min.ToString("#0.#") + ", 'μmol/m2/s' ],");
                PPFD_maxVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Max.ToString("#0.#") + ", 'μmol/m2/s' ],");
                PPFD_avgVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Avg.ToString("#0.#") + ", 'μmol/m2/s' ],");
                break;
            case "DLI":
                DLI_maxVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.Max.ToString("#0.#") + ", 'mol/m2/day' ],");

                break;
            //case "LightStatus":
            //    LightDetect_TrueVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.TrueCount + ", 'Detect' ],");
            //    LightDetect_FalseVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + item.FalseCount + ", 'Detect' ],");
            //    break;
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

                        if (params.value[2] == 'μmol/m2/s') {

                            return "Date: " + echarts.format.formatTime('<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>', date, true) + '<br/>' +
                                'PPFD: ' + params.value[1];
                    } else {

                        return "Date: " + echarts.format.formatTime('<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>', date, true) + '<br/>' +
                               'DLI: ' + params.value[1];
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
                    top: 'top',
                    data: ['PPFD Max', 'PPFD Min', 'PPFD Avg','PAR DLI'],
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
                        name: 'PPFD μmol/m2/s',
                        x: 'left',
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value}'
                        }
                    },
                    {
                        name: 'DLI  mol/m2/d',
                        x: 'right',
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value}'
                        }
                    }
                ],
                series: [
                      {
                          name: 'PPFD Max',
                          type: 'line',
                          showAllSymbol: true,
                          data: [<%=PPFD_maxVals.ToString().TrimEnd(',')%>],

                  },
                {
                    name: 'PPFD Min',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=PPFD_minVals.ToString().TrimEnd(',')%>],

                },
                   {
                       name: 'PPFD Avg',
                       type: 'line',
                       showAllSymbol: true,
                       data: [<%=PPFD_avgVals.ToString().TrimEnd(',')%>],

                   },
                  {
                      name: 'PAR DLI',
                      type: 'line',
                      showAllSymbol: true,
                      yAxisIndex: 1,
                      data: [<%=DLI_maxVals.ToString().TrimEnd(',')%>],

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



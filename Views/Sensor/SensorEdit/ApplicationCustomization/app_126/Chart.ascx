<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%

    List<DataMessage> list = DataMessage.LoadAllForChart(
                  Model.Sensors.ElementAt(0).SensorID,
                  Model.FromDate,
                  Model.ToDate
                  );

    StringBuilder X_MaxVals = new StringBuilder();
    StringBuilder Y_MaxVals = new StringBuilder();
    StringBuilder Z_MaxVals = new StringBuilder();
    StringBuilder Mag_MaxVals = new StringBuilder();
    StringBuilder X_MeanVals = new StringBuilder();
    StringBuilder Y_MeanVals = new StringBuilder();
    StringBuilder Z_MeanVals = new StringBuilder();
    StringBuilder Mag_MeanVals = new StringBuilder();

    foreach (DataMessage item in list)
    {
        GeforceMaxAvg Data = Monnit.GeforceMaxAvg.Deserialize(Model.Sensors[0].FirmwareVersion, item.Data);

        X_MaxVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.XMax.ToDouble() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'XMax' ,'" + item.DataMessageGUID +  "' ],");
        Y_MaxVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.YMax.ToDouble() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'YMax' ,'" + item.DataMessageGUID +  "' ],");
        Z_MaxVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.ZMax.ToDouble() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'ZMax' ,'" + item.DataMessageGUID +  "' ],");
        Mag_MaxVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.MagnitudeMax.ToDouble() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'MagnitudeMax' ,'" + item.DataMessageGUID +  "' ],");

        X_MeanVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.XMean.ToDouble() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'XMean' ,'" + item.DataMessageGUID +  "' ],");
        Y_MeanVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.YMean.ToDouble() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'YMean' ,'" + item.DataMessageGUID +  "' ],");
        Z_MeanVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.ZMean.ToDouble() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'ZMean' ,'" + item.DataMessageGUID +  "' ],");
        Mag_MeanVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.MagnitudeMean.ToDouble() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'MagnitudeMean' ,'" + item.DataMessageGUID +  "' ],");

    }
        
%>

<div id="sensorChartDiv" style="height: 350px;"></div>


<script type="text/javascript">


    $(document).ready(function () {

        var echartBar = echarts.init(document.getElementById('sensorChartDiv', 'infographic'));

        echartBar.setOption({

            baseOption: {
                tooltip: {
                    backgroundColor: "#ffffff96",
                    textStyle: {
                        fontWeight: 'bold',
                        color: 'black',
                    },
                    trigger: 'item',
                    formatter: function (params) {
                        var date = new Date(params.value[0]);

                        var dateString = "Date: " + echarts.format.formatTime('<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"] + ' ' + MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>', date, true) + '<br/>';
                    var statusString = 'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                    var valueString = '';
                    switch (params.value[3]) {
                        case 'XMax':
                            valueString = 'X-Axis Max: ' + params.value[1] + ' g' + '<br/>';
                            break;
                        case 'YMax':
                            valueString = 'Y-Axis Max: ' + params.value[1] + ' g' + '<br/>';
                            break;
                        case 'ZMax':
                            valueString = 'Z-Axis Max: ' + params.value[1] + ' g' + '<br/>';
                            break;
                        case 'MagnitudeMax':
                            valueString = 'Magnitude Max: ' + params.value[1] + ' g' + '<br/>';
                            break;
                        case 'XMean':
                            valueString = 'X-Axis Mean: ' + params.value[1] + ' g' + '<br/>';
                            break;
                        case 'YMean':
                            valueString = 'Y-Axis Mean: ' + params.value[1] + ' g' + '<br/>';
                            break;
                        case 'ZMean':
                            valueString = 'Z-Axis Mean: ' + params.value[1] + ' g' + '<br/>';
                            break;
                        case 'MagnitudeMean':
                            valueString = 'Magnitude Mean: ' + params.value[1] + ' g' + '<br/>';
                            break;
                        default:
                            break;
                    }

                    return dateString + valueString + statusString;

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
                        ['X Max', 'Y Max', 'Z Max', 'Magnitude Max',
                         'X Mean', 'Y Mean', 'Z Mean', 'Magnitude Mean'
                        ],

                },
                grid: {
                    y2: 80,
                    left: 75,
                    right: 50,
                },
                xAxis: [
                    {
                        type: 'time', min: <%=MonnitSession.HistoryFromDate.ToEpochTime()%>, max: <%=MonnitSession.HistoryToDate.ToEpochTime()%>,
                        axisLabel: {
                            formatter: function (params) {
                                var date = new Date(params);
                                return TimePreferenceFormat(params, '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '\r\n' + DatePreferenceFormat(params, '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"]) %>');
                        }

                    }
                }
            ],
                yAxis: [
                    {
                        name: 'Geforce',
                        x: 'left',
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value} '
                        }
                    }
                ],
                series: [
                    {
                        name: 'X Max',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=X_MaxVals.ToString().TrimEnd(',')%>],

                },
                {
                    name: 'Y Max',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=Y_MaxVals.ToString().TrimEnd(',')%>],

                },
                {
                    name: 'Z Max',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=Z_MaxVals.ToString().TrimEnd(',')%>],

                },
                  {
                      name: 'Magnitude Max',
                      type: 'line',
                      showAllSymbol: true,
                      data: [<%=Mag_MaxVals.ToString().TrimEnd(',')%>],

                  },
                      {
                          name: 'X Mean',
                          type: 'line',
                          showAllSymbol: true,
                          data: [<%=X_MeanVals.ToString().TrimEnd(',')%>],

                      },
                   {
                       name: 'Y Mean',
                       type: 'line',
                       showAllSymbol: true,
                       data: [<%=Y_MeanVals.ToString().TrimEnd(',')%>],

                   },
                     {
                         name: 'Z Mean',
                         type: 'line',
                         showAllSymbol: true,
                         data: [<%=Z_MeanVals.ToString().TrimEnd(',')%>],

                     },
                     {
                         name: 'Magnitude Mean',
                         type: 'line',
                         showAllSymbol: true,
                         data: [<%=Mag_MeanVals.ToString().TrimEnd(',')%>],

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
        echartBar.on('mouseover', function (params) {
            $('.message_' + params.value[4]).css("background", "lightgray"); //change second element background
            setTimeout(function () {
                $('.message_' + params.value[4]).css('background', 'white'); // change it back after ...
            }, 3000); // waiting one second
        });
    });


</script>



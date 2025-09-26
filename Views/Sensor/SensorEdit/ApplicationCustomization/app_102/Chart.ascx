<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%
    List<DataMessage> list = DataMessage.LoadAllForChart(
          Model.Sensors[0].SensorID,
          Model.FromDate,
          Model.ToDate
          );


    double maxAware = (Model.Sensors[0].MaximumThreshold);
    double minAware = (Model.Sensors[0].MinimumThreshold);
    double hystAware = (Model.Sensors[0].Hysteresis);

    StringBuilder PM25Vals = new StringBuilder();
    StringBuilder PM10Vals = new StringBuilder();
    StringBuilder PM1Vals = new StringBuilder();
    string label = AirQuality.GetLabel(Model.Sensors[0].SensorID);

    foreach (DataMessage item in list)
    {
        Monnit.AirQuality Data = Monnit.AirQuality.Deserialize(Model.Sensors[0].FirmwareVersion, item.Data);
        PM25Vals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.PM25.ToDouble().ToString() + "," + ((item.State & 2) == 2).ToInt() + ", 'PM25' ,'" + item.DataMessageGUID + "' ],");
        PM10Vals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.PM10.ToDouble().ToString() + "," + ((item.State & 2) == 2).ToInt() + ", 'PM10' ,'" + item.DataMessageGUID + "' ],");
        PM1Vals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.PM1.ToDouble().ToString() + "," + ((item.State & 2) == 2).ToInt() + ", 'PM1' ,'" + item.DataMessageGUID + "' ],");



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

                        var dateString = "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>';
                        var statusString = 'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                        var valueString = '';
                        switch (params.value[3]) {
                            case 'PM25':
                                valueString = 'PM 2.5: ' + params.value[1] + ' <%=label%>' + '<br/>';
                                break;
                            case 'PM10':
                                valueString = 'PM 10: ' + params.value[1] + ' <%=label%>' + '<br/>';
                                break;
                            case 'PM1':
                                valueString = 'PM 1: ' + params.value[1] + ' <%=label%>' + '<br/>';
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
                        ['PM25', 'PM10', 'PM1'
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
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        name: '<%=label%>',
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value}'
                        }
                    }
                ],
                series: [
                    {
                        name: 'PM25',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=PM25Vals.ToString().TrimEnd(',')%>],

                    },
                    {
                        name: 'PM10',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=PM10Vals.ToString().TrimEnd(',')%>],

                    },
                    {
                        name: 'PM1',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=PM1Vals.ToString().TrimEnd(',')%>],

                        markLine: {
                            lineStyle: {
                                color: 'green',
                            },
                            silent: true,
                            data: [{

                                label: {
                                    normal: {
                                        color: 'black',
                                        position: 'middle',
                                        formatter: 'PM2.5 Threshold -' + <%=minAware%>
                                    }
                                },
                                yAxis: '<%=minAware%>'
                            }, {
                                    label: {
                                        normal: {
                                            color: 'black',
                                            position: 'middle',
                                            formatter: 'PM10 Threshold -' + <%=maxAware%>
                                        }
                                    },
                                    yAxis: '<%=maxAware%>'
                                }, {
                                    label: {
                                        normal: {
                                            color: 'black',
                                            position: 'middle',
                                            formatter: 'PM1.0 Threshold -' + <%=hystAware%>
                                        }
                                    },
                                    yAxis: '<%=hystAware%>'
                                }]
                        }
                    }
                ]
            },
            media: [
                {
                    option: {
                        grid: {
                            left: 90,
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
                            left: 90,
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
                            left: 90,
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



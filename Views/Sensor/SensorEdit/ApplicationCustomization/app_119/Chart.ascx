<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%
    List<DataMessage> list = DataMessage.LoadAllForChart(
           Model.Sensors[0].SensorID,
           Model.FromDate,
           Model.ToDate
           );


    StringBuilder LuxDataVals = new StringBuilder();
    StringBuilder LightDataVals = new StringBuilder();

    foreach (DataMessage item in list)
    {
        CarDetect Data = Monnit.CarDetect.Deserialize(Model.Sensors[0].FirmwareVersion, item.Data);
        LuxDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.Count + "," + ((item.State & 2) == 2 ).ToInt() + ", 'Count' ,'" + item.DataMessageGUID +  "' ],");
        LightDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + ((Data.Detected.ToInt() > 0) ? 1 : 0) + "," + ((item.State & 2) == 2 ).ToInt() + ", 'Car Detected' ,'" + item.DataMessageGUID +  "' ],");
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
                        if (params.value[3] == 'Count') {

                            return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                               'Value: ' + params.value[1] + ' Count' + '<br/>' +
                               'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                        } else {

                            return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                                   'Value: ' + (params.value[1] == 1 ? 'Car' : 'No Car') + '<br/>' +
                                   'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
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
                    data: ['Count', 'Car Detect'],

                },
                grid: {
                    y2: 80,
                    left: 75,
                    right: 60,

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
                    name: 'Count',
                    x: 'left',
                    type: 'value',
                    axisLabel: {
                        formatter: '{value}'
                    }

                },
                {
                    name: 'Car Detect',
                    x: 'Right',
                    type: 'category',
                    inverse: false,
                    data: ['No Car', 'Car'],
                }
                ],
                series: [
                    {
                        name: 'Count',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=LuxDataVals.ToString().TrimEnd(',')%>],

                    },
                  {
                      name: 'Car Detect',
                      type: 'line',
                      step: 'end',
                      showAllSymbol: true,
                      yAxisIndex: 1,
                      data: [<%=LightDataVals.ToString().TrimEnd(',')%>],

                  }
                ]
            },
            media: [
                            {
                                option: {
                                    grid: {
                                        left: 80,
                                        top: '8%',
                                        right: 60,

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
                                        right: 60,
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
                                        right: 60,
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



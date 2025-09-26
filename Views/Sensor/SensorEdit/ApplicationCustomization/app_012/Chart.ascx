<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%
    Sensor sens = Model.Sensors[0];
    List<DataMessage> list = DataMessage.LoadAllForChart(
           sens.SensorID,
           Model.FromDate,
           Model.ToDate
           );


    StringBuilder Relay1DataVals = new StringBuilder();
    StringBuilder Relay2DataVals = new StringBuilder();
    string relay1Name = Control_1.Relay1Name(sens.SensorID);
    string relay2Name = Control_1.Relay2Name(sens.SensorID);

    foreach (DataMessage item in list)
    {
        Control_1 Data = Monnit.Control_1.Deserialize(sens.FirmwareVersion, item.Data);
        Relay1DataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + ((Data.RelayState1.ToInt() > 0) ? 1 : 0) + "," + ((item.State & 2) == 2 ).ToInt() + ", '" + relay1Name + "','" + item.DataMessageGUID +  "' ],");
        Relay2DataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + ((Data.RelayState2.ToInt() > 0) ? 1 : 0) + "," + ((item.State & 2) == 2 ).ToInt() + ", '" + relay2Name + "','" + item.DataMessageGUID +  "' ],");
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
                        if (params.value[3] == '<%=relay1Name%>') {

                            return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                                   '<%=relay1Name%>: ' + (params.value[1] == 1 ? 'On' : 'Off') + '<br/>' +
                                   'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                        } else {

                            return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                                  '<%=relay2Name%>: ' + (params.value[1] == 1 ? 'On' : 'Off') + '<br/>' +
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
                    data: ['<%=relay1Name%>', '<%=relay2Name%>'],

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
                    name: '<%=relay1Name%>',
                    x: 'left',
                    type: 'category',
                    data: ['Off', 'On'],
                },
                {
                    name: '<%=relay2Name%>',
                    x: 'Right',
                    type: 'category',
                    inverse: false,
                    data: ['Off', 'On'],
                }
                ],
                series: [
                    {
                        name: '<%=relay1Name%>',
                        type: 'line',
                        step: 'end',
                        showAllSymbol: true,
                        data: [<%=Relay1DataVals.ToString().TrimEnd(',')%>],

                    },
                  {
                      name: '<%=relay2Name%>',
                      type: 'line',
                      step: 'end',
                      showAllSymbol: true,
                      yAxisIndex: 1,
                      data: [<%=Relay2DataVals.ToString().TrimEnd(',')%>],

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



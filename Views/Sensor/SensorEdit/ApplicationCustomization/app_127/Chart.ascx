<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%

  
    List<DataMessage> list = DataMessage.LoadAllForChart(
           Model.Sensors[0].SensorID,
           Model.FromDate,
           Model.ToDate
           );


    StringBuilder dataVals = new StringBuilder();
    string OneValue = QuadContact.GetOneValue(Model.Sensors[0].SensorID);
    string ZeroValue = QuadContact.GetZeroValue(Model.Sensors[0].SensorID);

    StringBuilder Probe1Vals = new StringBuilder();
    StringBuilder Probe2Vals = new StringBuilder();
    StringBuilder Probe3Vals = new StringBuilder();
    StringBuilder Probe4Vals = new StringBuilder();

    string Probe1Name = QuadContact.GetContact1Label(Model.Sensors[0].SensorID);
    string Probe2Name = QuadContact.GetContact2Label(Model.Sensors[0].SensorID);
    string Probe3Name = QuadContact.GetContact3Label(Model.Sensors[0].SensorID);
    string Probe4Name = QuadContact.GetContact4Label(Model.Sensors[0].SensorID);

    foreach (DataMessage item in list)
    {
        Monnit.QuadContact Data = Monnit.QuadContact.Deserialize(Model.Sensors[0].FirmwareVersion, item.Data);
        Probe1Vals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + ((Data.Contact1.ToInt() > 0) ? 1 : 0) + "," + ((item.State & 2) == 2 ).ToInt() + ", '" + Probe1Name + "' ,'" + item.DataMessageGUID +  "' ],");
        Probe2Vals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + ((Data.Contact2.ToInt() > 0) ? 1 : 0) + "," + ((item.State & 2) == 2 ).ToInt() + ", '" + Probe2Name + "' ,'" + item.DataMessageGUID +  "' ],");
        Probe3Vals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + ((Data.Contact3.ToInt() > 0) ? 1 : 0) + "," + ((item.State & 2) == 2 ).ToInt() + ", '" + Probe3Name + "' ,'" + item.DataMessageGUID +  "' ],");
        Probe4Vals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + ((Data.Contact4.ToInt() > 0) ? 1 : 0) + "," + ((item.State & 2) == 2 ).ToInt() + ", '" + Probe4Name + "' ,'" + item.DataMessageGUID +  "' ],");

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
                            case '<%=Probe1Name%>':
                            valueString = '<%=Probe1Name%>: ' + (params.value[1] == 1 ? '<%=OneValue%>' : '<%=ZeroValue%>') + '<br/>';
                            break;
                        case '<%=Probe2Name%>':
                            valueString = '<%=Probe2Name%>: ' + (params.value[1] == 1 ? '<%=OneValue%>' : '<%=ZeroValue%>') + '<br/>';
                            break;
                        case '<%=Probe3Name%>':
                            valueString = '<%=Probe3Name%>: ' + (params.value[1] == 1 ? '<%=OneValue%>' : '<%=ZeroValue%>') + '<br/>';
                                break;
                            case '<%=Probe4Name%>':
                            valueString = '<%=Probe4Name%>: ' + (params.value[1] == 1 ? '<%=OneValue%>' : '<%=ZeroValue%>') + '<br/>';
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
                    type: 'scroll',
                    type: 'scroll',
                    top: -5,
                    data:
                        ['<%=Probe1Name%>', '<%=Probe2Name%>', '<%=Probe3Name%>', '<%=Probe4Name%>'],

                },
                grid: {
                    y2: 80,
                    left: 75,
                    right: 50,
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
                yAxis: [{
                    type: 'category',
                    data: ['<%=OneValue%>', '<%=ZeroValue%>'],
                }],
                series: [
                {
                    name: '<%=Probe1Name%>',
                    step: 'end',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=Probe1Vals.ToString().TrimEnd(',')%>],

                },
                {
                    name: '<%=Probe2Name%>',
                    step: 'end',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=Probe2Vals.ToString().TrimEnd(',')%>],

                },
                {
                    name: '<%=Probe3Name%>',
                    step: 'end',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=Probe3Vals.ToString().TrimEnd(',')%>],

                },
                 {
                     name: '<%=Probe4Name%>',
                     step: 'end',
                     type: 'line',
                     showAllSymbol: true,
                     data: [<%=Probe4Vals.ToString().TrimEnd(',')%>],

                 }
                ]
            },
            media: [
                            {
                                option: {
                                    grid: {
                                        left: 70,
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
                                        left: 70,
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
                                        left: 70,
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



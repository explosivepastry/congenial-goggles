<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%
    List<DataMessage> list = DataMessage.LoadAllForChart(
          Model.Sensors[0].SensorID,
          Model.FromDate,
          Model.ToDate
          );


    StringBuilder X_SpeedVals = new StringBuilder();
    StringBuilder Y_SpeedVals = new StringBuilder();
    StringBuilder Z_SpeedVals = new StringBuilder();
    StringBuilder X_FrequencyVals = new StringBuilder();
    StringBuilder Y_FrequencyVals = new StringBuilder();
    StringBuilder Z_FrequencyVals = new StringBuilder();

    foreach (DataMessage item in list)
    {
        VibrationMeter Data = Monnit.VibrationMeter.Deserialize(Model.Sensors[0].FirmwareVersion, item.Data);
        X_SpeedVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.xSpeed.ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'XSpeed' ,'" + item.DataMessageGUID +  "' ],");
        Y_SpeedVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.ySpeed.ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'YSpeed' ,'" + item.DataMessageGUID +  "' ],");
        Z_SpeedVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.zSpeed.ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'ZSpeed' ,'" + item.DataMessageGUID +  "' ],");

        X_FrequencyVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.xFrequency.ToDouble() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'XFrequency' ,'" + item.DataMessageGUID +  "' ],");
        Y_FrequencyVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.yFrequency.ToDouble() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'YFrequency' ,'" + item.DataMessageGUID +  "' ],");
        Z_FrequencyVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.zFrequency.ToDouble() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'ZFrequency' ,'" + item.DataMessageGUID +  "' ],");

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
                            case 'XSpeed':
                                valueString = 'X-Axis Speed: ' + params.value[1] + ' mm/s' + '<br/>';
                                break;
                            case 'YSpeed':
                                valueString = 'Y-Axis Speed: ' + params.value[1] + ' mm/s' + '<br/>';
                                break;
                            case 'ZSpeed':
                                valueString = 'Z-Axis Speed: ' + params.value[1] + ' mm/s' + '<br/>';
                                break;
                            case 'XFrequency':
                                valueString = 'X-Axis Frequency: ' + params.value[1] + ' Hz' + '<br/>';
                                break;
                            case 'YFrequency':
                                valueString = 'Y-Axis Frequency: ' + params.value[1] + ' Hz' + '<br/>';
                                break;
                            case 'ZFrequency':
                                valueString = 'Z-Axis Frequency: ' + params.value[1] + ' Hz' + '<br/>';
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
                        ['X Speed', 'Y Speed', 'Z Speed',
                         'X Frequency', 'Y Frequency', 'Z Frequency'
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
                        name: 'Speed (mm/s)',
                        x: 'left',
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value} '
                        }
                    },
                    {
                        name: 'Frequency (Hz)',
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        inverse: false,
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value} '
                        }
                    }
                ],
                series: [
                    {
                        name: 'X Speed',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=X_SpeedVals.ToString().TrimEnd(',')%>],

                    },
                {
                    name: 'Y Speed',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=Y_SpeedVals.ToString().TrimEnd(',')%>],

                },
                {
                    name: 'Z Speed',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=Z_SpeedVals.ToString().TrimEnd(',')%>],

                },
                 {
                     name: 'X Frequency',
                     type: 'line',
                     yAxisIndex: 1,
                     showAllSymbol: true,
                     data: [<%=X_FrequencyVals.ToString().TrimEnd(',')%>],

                 },
                   {
                       name: 'Y Frequency',
                       type: 'line',
                       yAxisIndex: 1,
                       showAllSymbol: true,
                       data: [<%=Y_FrequencyVals.ToString().TrimEnd(',')%>],

                   },
                     {
                         name: 'Z Frequency',
                         type: 'line',
                         yAxisIndex: 1,
                         showAllSymbol: true,
                         data: [<%=Z_FrequencyVals.ToString().TrimEnd(',')%>],

                     }
                ]
            },
            media: [
                            {
                                option: {
                                    grid: {
                                        left: 75,
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
                                        left: 75,
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
                                        left: 75,
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



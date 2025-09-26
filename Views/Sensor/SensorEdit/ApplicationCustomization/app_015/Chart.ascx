<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%
    List<DataMessage> list = DataMessage.LoadAllForChart(
          Model.Sensors[0].SensorID,
          Model.FromDate,
          Model.ToDate
          );


    StringBuilder X_AxisVals = new StringBuilder();
    StringBuilder Y_AxisVals = new StringBuilder();
    StringBuilder Z_AxisVals = new StringBuilder();


    foreach (DataMessage item in list)
    {
        Accelerometer Data = Monnit.Accelerometer.Deserialize(Model.Sensors[0].FirmwareVersion, item.Data);
        X_AxisVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.Xval.ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'XAxis','" + item.DataMessageGUID +  "' ],");
        Y_AxisVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.Yval.ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'YAxis','" + item.DataMessageGUID +  "' ],");
        Z_AxisVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.Zval.ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'ZAxis','" + item.DataMessageGUID +  "' ],");



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
                            case 'XAxis':
                                valueString = 'X-Axis: ' + params.value[1] + ' ' + '<br/>';
                                break;
                            case 'YAxis':
                                valueString = 'Y-Axis: ' + params.value[1] + ' ' + '<br/>';
                                break;
                            case 'ZAxis':
                                valueString = 'Z-Axis: ' + params.value[1] + ' ' + '<br/>';
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
                    top: -5,
                    data:
                        ['X Axis', 'Y Axis', 'Z Axis'],

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
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value} '
                        }
                    }

                ],
                series: [
                    {
                        name: 'X Axis',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=X_AxisVals.ToString().TrimEnd(',')%>],

                    },
                {
                    name: 'Y Axis',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=Y_AxisVals.ToString().TrimEnd(',')%>],

                },
                {
                    name: 'Z Axis',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=Z_AxisVals.ToString().TrimEnd(',')%>],

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



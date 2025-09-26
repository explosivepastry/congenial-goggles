<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%

    List<DataMessage> list = DataMessage.LoadAllForChart(
           Model.Sensors[0].SensorID,
           Model.FromDate,
           Model.ToDate
           );


    StringBuilder channel1_DataVals = new StringBuilder();
    StringBuilder channel2_DataVals = new StringBuilder();
    string channel1_Label = TwoInputPulseCounter.GetLabel_Channel1(Model.Sensors[0].SensorID);
    string channel2_Label = TwoInputPulseCounter.GetLabel_Channel1(Model.Sensors[0].SensorID);
    foreach (DataMessage item in list)
    {

        TwoInputPulseCounter Data = (TwoInputPulseCounter)MonnitApplicationBase.LoadMonnitApplication(Model.Sensors[0].FirmwareVersion, item.Data, Model.Sensors[0].ApplicationID, Model.Sensors[0].SensorID);

        channel1_DataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.PlotValue.ToDouble().ToString() + "," + ((item.State & 2) == 2).ToInt() + ", 'Channel 1' ,'" + item.DataMessageGUID + "' ],");
        channel2_DataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," +  Data.PlotValue_Channel2.ToDouble().ToString() + "," + ((item.State & 2) == 2).ToInt() + ", 'Channel 2' ,'" + item.DataMessageGUID +  "' ],");

        //double channelTwoPlotValue = Data.Channel2.ToDouble();;

        //if (Data.Channel2TransformValue != null && (Data.Channel2TransformValue.Value != "0" && Data.Channel2TransformValue.Value != "1"))
        //{
        //    channelTwoPlotValue = Data.Channel2.ToDouble() * Data.Channel2TransformValue.Value.ToDouble();
        //}

        //channel2_DataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + channelTwoPlotValue.ToString() + "," + ((item.State & 2) == 2).ToInt() + ", 'Channel 2' ,'" + item.DataMessageGUID + "' ],");
    }

%>

<div id="sensorChartDiv" style="height: 350px;"></div>

<script type="text/javascript">


    $(document).ready(function () {
        var channel1_Label = '<%=channel1_Label%>';
        var channel2_Label = '<%=channel2_Label%>';
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
                            case 'Channel 1':
                                valueString = 'Channel 1: ' + params.value[1] + ' ' + channel1_Label + '<br/>';
                                break;
                            case 'Channel 2':
                                valueString = 'Channel 2: ' + params.value[1] + ' ' + channel2_Label + '<br/>';
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
                        ['Channel 1', 'Channel 2'],

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
                        name: 'Channel 1',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=channel1_DataVals.ToString().TrimEnd(',')%>],

                    },
                    {
                        name: 'Channel 2',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=channel2_DataVals.ToString().TrimEnd(',')%>],

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





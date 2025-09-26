<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%

    Sensor sensor = Model.Sensors[0];
    bool isF = SoilMoisture.IsFahrenheit(sensor.SensorID);
    string label = SoilMoisture.GetLabel(sensor.SensorID);

    List<DataMessage> list = DataMessage.LoadAllForChart(
           sensor.SensorID,
           Model.FromDate,
           Model.ToDate
           );


    StringBuilder TemperatureDataVals = new StringBuilder();
    StringBuilder MoistureDataVals = new StringBuilder();



    foreach (DataMessage item in list)
    {
        SoilMoisture Data = (SoilMoisture)MonnitApplicationBase.LoadMonnitApplication(sensor.FirmwareVersion, item.Data, sensor.ApplicationID, sensor.SensorID);

        TemperatureDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (isF ? Data.Temperature.ToFahrenheit() : Data.Temperature).ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'Temperature' ,'" + item.DataMessageGUID +  "' ],");
        MoistureDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (Data.PlotValue.ToDouble().ToString("#0.##")) + "," + ((item.State & 2) == 2 ).ToInt() + ", 'Moisture' ,'" + item.DataMessageGUID +  "' ],");
    }

    double maxAware = SoilMoisture.GetMoistureThreshMax(sensor);
    double minAware = SoilMoisture.GetMoistureThreshMin(sensor);
%>

<div id="sensorChartDiv" style="height: 350px;"></div>


<script type="text/javascript">


    $(document).ready(function () {

        var tempScale = '<%:isF?"F":"C" %>';
        var label = '<%=label%>';

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
                        if (params.value[3] == 'Temperature') {

                            return 'Temperature <br/>' +
                                   'Date: ' + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                                   'Value: ' + params.value[1] + ' °' + tempScale + '<br/>' +
                                   'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                        } else {

                            return 'Moisture Tension <br/>' +
                                   'Date: ' + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                                   'Value: ' + params.value[1] + ' ' + label + '<br/>' +
                                   'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                        }
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
                    top: 'top',
                    data: ['Moisture Tension', 'Temperature'],

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
                yAxis: [
                    {
                        name: 'Moisture Tension',
                        x: 'left',
                        type: 'value', min: 'dataMin', max: 'dataMax',
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value} \r\n' + label
                        }

                    },
                    {
                        name: 'Temperature',
                        x: 'Right',
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        inverse: false,
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value} °' + tempScale
                        }
                    }
                ],
                series: [
                    {
                        name: 'Moisture Tension',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=MoistureDataVals.ToString().TrimEnd(',')%>],


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
                                        formatter: 'Minimum Threshold'  
                                    }
                                },
                                yAxis: '<%=minAware%>'
                            }, {
                                    label: {
                                        normal: {
                                            color: 'black',
                                            position: 'middle',
                                            formatter: 'Maximum Threshold'
                                        }
                                    },
                                    yAxis: '<%=maxAware%>'
                                }]
                        }

                    },
                    {
                        name: 'Temperature',
                        type: 'line',
                        showAllSymbol: true,
                        yAxisIndex: 1,
                        data: [<%=TemperatureDataVals.ToString().TrimEnd(',')%>],

                    }
                ]
            },
            media: [
                {
                    option: {
                        grid: {
                            left: 75,
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
                        yAxis: [{
                            splitNumber: 4,
                            splitLine: {
                                show: true
                            }
                        },
                        {
                            splitNumber: 4,
                            splitLine: {
                                show: true
                            }
                        }],
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
                        yAxis: [{
                            splitNumber: 4,
                            splitLine: {
                                show: true
                            }
                        },
                        {
                            splitNumber: 4,
                            splitLine: {
                                show: true
                            }
                        }],
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
                            right: 60,
                        },
                        xAxis: {
                            nameLocation: 'middle',
                            nameGap: 25,
                            splitNumber: 2
                        },
                        yAxis: [{
                            splitNumber: 2,
                            splitLine: {
                                show: true
                            }
                        },
                        {
                            splitNumber: 2,
                            splitLine: {
                                show: true
                            }
                        }],
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



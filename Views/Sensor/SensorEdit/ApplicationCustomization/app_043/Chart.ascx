<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%
    long SensorID = Model.Sensors[0].SensorID;
    bool isF = HumiditySHT25.IsFahrenheit(SensorID);

    double maxAware = (Model.Sensors[0].MaximumThreshold >> 16) / 100.0;

    double minAware = (Model.Sensors[0].MinimumThreshold >> 16) / 100.0;


    List<DataMessage> list = DataMessage.LoadAllForChart(
           SensorID,
           Model.FromDate,
           Model.ToDate
           );

    //Sensor currentSensor = Sensor.LoadById
    StringBuilder TemperatureDataVals = new StringBuilder();
    StringBuilder HumidityDataVals = new StringBuilder();
    StringBuilder HeatIndex = new StringBuilder();

    bool heatIndex = HumiditySHT25.ShowHeatIndex(SensorID).ToBool() ? true : false;

    foreach (DataMessage item in list)
    {
        HumiditySHT25 Data = (HumiditySHT25)MonnitApplicationBase.LoadMonnitApplication(Model.Sensors[0].FirmwareVersion, item.Data, Model.Sensors[0].ApplicationID, SensorID);
        //HumiditySHT25 Data = Monnit.HumiditySHT25.Deserialize(Model.Sensors[0].FirmwareVersion, item.Data);
        TemperatureDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (isF ? Data.Temperature.ToFahrenheit() : Data.Temperature).ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2).ToInt() + ", 'Temperature' ,'" + item.DataMessageGUID + "' ],");

        if (heatIndex)
            HeatIndex.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (isF ? Data.HeatIndex.ToFahrenheit() : Data.HeatIndex).ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2).ToInt() + ", 'Heat Index' ,'" + item.DataMessageGUID + "' ],");
        HumidityDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (Data.PlotValue.ToDouble().ToString("#0.##")) + "," + ((item.State & 2) == 2).ToInt() + ", 'Humidity' ,'" + item.DataMessageGUID + "' ],");
    }

%>

<div id="sensorChartDiv_<%= SensorID %>" style="height: 350px;"></div>


<script type="text/javascript">


    $(document).ready(function () {


        var fontSize = 12;
        var legendItemHeight = 14;
        var legendItemWidth = 25;

        var minThreshold = Number(<%= minAware%>);
        var maxThreshold = Number(<%= maxAware%>);

        var xAxisNameGap = 10;

        var yAxisNameGap = 5;


        var tempScale = '<%:isF?"F":"C" %>';

        var echartBar_<%= SensorID %> = echarts.init(document.getElementById('sensorChartDiv_<%= SensorID %>', 'infographic'));

        echartBar_<%= SensorID %>.setOption({

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

                            return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                                'Value: ' + params.value[1] + ' °' + tempScale + '<br/>' +
                                'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                        }
                        else if (params.value[3] == 'Heat Index') {

                            return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                                'Value: ' + params.value[1] + ' °' + tempScale + '<br/>' +
                                'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                        }
                        else {

                            return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                                'Value: ' + params.value[1] + ' %' + '<br/>' +
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
                    orient: 'horizontal',

                    top: -5,

                    width: '50%',

                    data: ['Humidity', 'Temperature', 'Heat Index'],
                },
                grid: {
                    y2: '22%',
                    left: '10%',
                    right: '10%',
                    top: '15%',

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
                        name: 'Humidity',
                        nameGap: 5,
                        x: 'left',
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value} %'
                        }

                    },
                    {
                        name: 'Temperature',
                        nameGap: 5,
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
                    },
                ],
                series: [
                    {
                        name: 'Humidity',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=HumidityDataVals.ToString().TrimEnd(',')%>],
                        markLine: {
                            silent: true,
                            symbol: 'pin',
                            data: [{
                                label: {
                                    normal: {
                                        position: 'middle',
                                        formatter: 'Maximum Threshold - ' + maxThreshold
                                    }
                                },
                                yAxis: maxThreshold
                            },
                                {
                                    label: {
                                        normal: {
                                            position: 'insideMiddleBottom',
                                            formatter: 'Minimum Threshold - ' + minThreshold
                                        }
                                    },
                                    yAxis: minThreshold
                            }

                            ]
                        }
                    },
                    {
                        name: 'Temperature',
                        type: 'line',
                        showAllSymbol: true,
                        yAxisIndex: 1,
                        data: [<%=TemperatureDataVals.ToString().TrimEnd(',')%>],
                    },
                    <% if (heatIndex)
    { %>
                    {
                        name: 'Heat Index',
                        type: 'line',
                        showAllSymbol: true,
                        yAxisIndex: 1,
                        data: [<%= HeatIndex.ToString().TrimEnd(',')%>],
                    }
            <% }%>
                ]
            },
            media: [
                {
                    option: {
                        legend: {
                            textStyle: {
                                fontSize: fontSize,
                            },
                        },
                        xAxis: {
                            splitNumber: 8,
                            axisLabel: {
                                fontSize: fontSize,
                            }
                        },
                        yAxis: [
                            {
                                nameTextStyle: {
                                    fontSize: fontSize,
                                },
                                axisLabel: {
                                    fontSize: fontSize,
                                },
                                splitNumber: 4,
                            },
                            {
                                nameTextStyle: {
                                    fontSize: fontSize,
                                },
                                axisLabel: {
                                    fontSize: fontSize,
                                },
                                splitNumber: 4,
                            }
                        ],
                    }
                },
                {
                    query: {
                        maxWidth: 670, minWidth: 550
                    },
                    option: {
                        legend: {
                            textStyle: {
                                fontSize: fontSize - 1,
                            },
                        },
                        xAxis: {
                            axisLabel: {
                                fontSize: fontSize - 1,
                            },
                            splitNumber: 5,
                        },
                        yAxis: [
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 1,
                                },
                                axisLabel: {
                                    fontSize: fontSize - 1,
                                },
                                splitNumber: 4,

                            },
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 1,
                                },
                                axisLabel: {
                                    fontSize: fontSize - 1,
                                },
                                splitNumber: 4,
                            }
                        ],
                    }
                },
                {
                    query: {
                        minWidth: 375, maxWidth: 550
                    },
                    option: {
                        legend: {
                            textStyle: {
                                fontSize: fontSize - 2,
                            },
                        },
                        xAxis: {
                            axisLabel: {
                                fontSize: fontSize - 2,
                            },
                            splitNumber: 2,
                        },
                        yAxis: [
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 2,
                                },
                                axisLabel: {
                                    fontSize: fontSize - 2,
                                },
                                splitNumber: 2,
                            },
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 2,
                                },
                                axisLabel: {
                                    fontSize: fontSize - 2,
                                },
                                splitNumber: 2,
                            }
                        ],
                    },
                },
                {
                    query: {
                        maxWidth: 375
                    },
                    option: {
                        legend: {
                            textStyle: {
                                fontSize: fontSize - 3,
                            },
                            itemHeight: legendItemHeight / 2,
                            itemWidth: legendItemWidth / 2,
                        },
                        xAxis: {
                            axisLabel: {
                                fontSize: fontSize - 3,
                            },
                            splitNumber: 2,
                        },
                        yAxis: [
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 3,
                                },
                                axisLabel: {
                                    fontSize: fontSize - 3,
                                },
                                splitNumber: 2,
                            },
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 3,
                                },
                                axisLabel: {
                                    fontSize: fontSize - 3,
                                },
                                splitNumber: 2,
                            }
                        ],
                    }
                }
            ]
        });


        $(window).on('resize', function () {
            if (echartBar_<%= SensorID %> != null && echartBar_<%= SensorID %> != undefined) {
                echartBar_<%= SensorID %>.resize();
            }
        });
        echartBar_<%= SensorID %>.on('mouseover', function (params) {
            $('.message_' + params.value[4]).css("background", "lightgray"); //change second element background
            setTimeout(function () {
                $('.message_' + params.value[4]).css('background', 'white'); // change it back after ...
            }, 3000); // waiting one second
        });
    });

</script>



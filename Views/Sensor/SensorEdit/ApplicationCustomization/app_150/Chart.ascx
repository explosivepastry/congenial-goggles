<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>
<%

    bool isF = Motion_RH_WaterDetect.IsFahrenheit(Model.Sensors[0].SensorID);

    List<DataMessage> list = DataMessage.LoadAllForChart(
           Model.Sensors[0].SensorID,
           Model.FromDate,
           Model.ToDate
           );

    //First Chart
    StringBuilder TemperatureDataVals = new StringBuilder();
    StringBuilder HumidityDataVals = new StringBuilder();

    //Sec Chart
    StringBuilder MotionDataVal = new StringBuilder();
    StringBuilder WaterdataVals = new StringBuilder();

    foreach (DataMessage item in list)
    {
        Motion_RH_WaterDetect Data = (Motion_RH_WaterDetect)MonnitApplicationBase.LoadMonnitApplication(Model.Sensors[0].FirmwareVersion, item.Data, Model.Sensors[0].ApplicationID, Model.Sensors[0].SensorID);
        TemperatureDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (isF ? Data.Temperature.ToFahrenheit() : Data.Temperature).ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2).ToInt() + ", 'Temperature' ,'" + item.DataMessageGUID + "' ],");
        HumidityDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (Data.HumidityPlotValue.ToDouble().ToString("#0.##")) + "," + ((item.State & 2) == 2).ToInt() + ", 'Humidity' ,'" + item.DataMessageGUID + "' ],");

        //Sec Chart
        MotionDataVal.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (Data.Motion ? 1 : 0) + "," + ((item.State & 2) == 2).ToInt() + ", 'Motion' ,'" + item.DataMessageGUID + "' ],");
        WaterdataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + ((Data.WaterPlotValue.ToInt() > 0) ? 1 : 0) + "," + ((item.State & 2) == 2).ToInt() + ", 'Water' ,'" + item.DataMessageGUID + "' ],");
        //truefalseMotionDataVal.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + ((Data.PlotValue.ToInt() > 0) ? 1 : 0) + "," + ((item.State & 2) == 2 ).ToInt() + "],");
       
    }
%>

<div id="sensorChartDiv" style="height: 350px;"></div>
<br />
<div id="sensorChartMotion" style="height: 350px;"></div>


<%--Humidity & Temperature Chart--%>
<script type="text/javascript">
    $(document).ready(function () {
        var tempScale = '<%:isF?"F":"C" %>';
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
                            return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                                'Value: ' + params.value[1] + ' °' + tempScale + '<br/>' +
                                'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                        } else {
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
                    type: 'scroll',
                    top: 'top',
                    data: ['Humidity', 'Temperature'],
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
                        name: 'Humidity',
                        x: 'left',
                        type: 'value', min: 'dataMin', max: 'dataMax',
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value} %'
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
                        name: 'Humidity',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=HumidityDataVals.ToString().TrimEnd(',')%>],
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

        /*Sec Chart - Motion & No Motion Chart*/
        var echartBarMotion = echarts.init(document.getElementById('sensorChartMotion', 'infographic'));
        echartBarMotion.setOption({
            baseOption: {
                tooltip: {
                    trigger: 'item',
                    formatter: function (params) {
                        var date = new Date(params.value[0]);
                        if (params.value[3] == 'Motion') {
                            return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                                'Value: ' + (params.value[1] == 1 ? 'Motion' : 'No Motion') + '<br/>' +
                                'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                        } else {
                            return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                                'Value: ' + (params.value[1] == 1 ? 'Water' : 'No Water') + '<br/>' +
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
                    data: ['Motion', 'Water'],
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
                        name: 'Motion',
                        x: 'left',
                        type: 'category',
                        data: ['No Motion', 'Motion']
                    },

                    {
                        name: 'Water',
                        x: 'Right',
                        type: 'category',
                        data: ['No Water', 'Water']
                    }
                ],

                series: [
                    {
                        name: 'Motion',
                        showAllSymbol: true,
                        step: 'end',
                        type: 'line',
                        data: [<%=MotionDataVal.ToString().TrimEnd(',')%>]

                    },
                    {
                        name: 'Water',
                        showAllSymbol: true,
                        step: 'end',
                        type: 'line',
                        data: [<%=WaterdataVals.ToString().TrimEnd(',')%>]
                    }
                ]
            },

            media: [
                {
                    option: {
                        grid: {
                            left: 75,
                            top: '8%',
                            right: 65,
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
                            right: 65,
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
                            right: 65,
                        },

                        xAxis: {
                            nameLocation: 'middle',
                            nameGap: 25,
                            splitNumber: 2
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

<%--    var echartBar = echarts.init(document.getElementById('sensorChartWater', 'infographic'));

    echartBar.setOption({

        baseOption: {

            tooltip: {
                trigger: 'item',
                formatter: function (params) {
                    var date = new Date(params.value[0]);
                    return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                        'Value: ' + (params.value[1] == 1 ? 'Water' : 'No Water') + '<br/>' +
                        'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
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
                data: ['No Water', 'Water']
            }],
            series: [{
                name: 'Status',
                showAllSymbol: true,
                step: 'end',
                type: 'line',
                data: [<%=dataVals.ToString().TrimEnd(',')%>]
            }]
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
    });--%>


    $(window).on('resize', function () {
        if (echartBar != null && echartBar != undefined) {
            echartBar.resize();
        }
    });

    echartBar.on('mouseover', function (params) {
        $('.message_' + params.value[3]).css("background", "lightgray"); //change second element background
        setTimeout(function () {
            $('.message_' + params.value[3]).css('background', 'white'); // change it back after ...
        }, 3000); // waiting one second
    });

    echartBarMotion.on('mouseover', function (params) {
        $('.message_' + params.value[3]).css("background", "lightgray"); //change second element background
        setTimeout(function () {
            $('.message_' + params.value[3]).css('background', 'white'); // change it back after ...
        }, 3000); // waiting one second
    });


</script>








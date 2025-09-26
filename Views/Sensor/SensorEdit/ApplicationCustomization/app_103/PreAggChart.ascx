<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.PreAggregatePageModel>" %>

<%
    // 103 = Differential Pressure PreAgg Chart
    bool isF = Temperature.IsFahrenheit(Model.sensor.SensorID);
    string label = "";
    StringBuilder minVals = new StringBuilder();
    StringBuilder maxVals = new StringBuilder();
    StringBuilder avgVals = new StringBuilder();
    StringBuilder Temperature_minVals = new StringBuilder();
    StringBuilder Temperature_maxVals = new StringBuilder();
    StringBuilder Temperature_avgVals = new StringBuilder();


    foreach (PreAggregatedData item in Model.PreAggregateList)
    {
        switch (item.SplitValue)
        {

            case "Temperature":
                Temperature_minVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + (isF ? item.Min.ToFahrenheit() : item.Min).ToString("#0.#") + ", 'Temperature Min' ],");
                Temperature_maxVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + (isF ? item.Max.ToFahrenheit() : item.Max).ToString("#0.#") + ", 'Temperature Max' ],");
                Temperature_avgVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + (isF ? item.Avg.ToFahrenheit() : item.Avg).ToString("#0.#") + ", 'Temperature Avg' ],");
                break;
            case "DifferentialPressure":
                label = DifferentialPressure.GetLabel(Model.sensor.SensorID);
                double LowValue = DifferentialPressure.GetLowValue(Model.sensor.SensorID);
                double HighValue = DifferentialPressure.GetHighValue(Model.sensor.SensorID);
                double transformVal = 1;
                List<SensorAttribute> attributes = SensorAttribute.LoadBySensorID(Model.sensor.SensorID);
                if (attributes.Where(c => c.Name == "transformValue").ToList().Count() > 0)
                {
                    transformVal = attributes.Where(c => c.Name == "transformValue").FirstOrDefault().Value.ToDouble();
                }
                double min = item.Min;
                double max = item.Max;
                double avg = item.Avg;
                if (string.IsNullOrEmpty(label) || label.ToLower() != "custom")
                {
                    if (transformVal > 0)
                    {
                        min = min * transformVal;
                        max = max * transformVal;
                        avg = avg * transformVal;
                    }
                }
                else
                {
                    min = min.LinearInterpolation(0, LowValue, 300, HighValue).ToString("#0.#", System.Globalization.CultureInfo.InvariantCulture).ToDouble();
                    max = max.LinearInterpolation(0, LowValue, 300, HighValue).ToString("#0.#", System.Globalization.CultureInfo.InvariantCulture).ToDouble();
                    avg = avg.LinearInterpolation(0, LowValue, 300, HighValue).ToString("#0.#", System.Globalization.CultureInfo.InvariantCulture).ToDouble();
                }

                minVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + min + ", 'Differential Pressure Min' ],");
                maxVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + max + ", 'Differential Pressure Max' ],");
                avgVals.Append("[new Date(" + item.Date.ToUniversalTime().ToEpochTime() + ")," + avg + ", 'Differential Pressure Avg' ],");
                break;
        }
    }

%>

<div id="sensorChartDiv" style="height: 350px;"></div>


<script type="text/javascript">

    $(document).ready(function () {

        var tempScale = '<%:isF?"F":"C" %>';
        var diffLabel = "<%=label%>";
        var echartBar = echarts.init(document.getElementById('sensorChartDiv', 'infographic'));

        echartBar.setOption({
            baseOption: {
                tooltip: {
                    trigger: 'item',
                    formatter: function (params) {
                        var date = new Date(params.value[0]);
                        if (params.value[2].includes('Temperature')) {

                            return "Date: " + echarts.format.formatTime('<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>', date, true) + '<br/>' +
                                params.value[2] + ': ' + params.value[1] + ' °' + tempScale;
                    } else {

                            return "Date: " + echarts.format.formatTime('<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>', date, true) + '<br/>' +
                                params.value[2] + ': ' + params.value[1] + diffLabel;
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
                    data: ['Differential Pressure Max', 'Differential Pressure Min', 'Differential Pressure Avg',
                        'Temperature Max', 'Temperature Min', 'Temperature Avg'
                    ],
                },
                grid: {
                    y2: 80,
                    left: 55,
                    right: 50,
                },
                xAxis: [
                    {
                        type: 'time',
                        axisLabel: {
                            formatter: function (params) {
                                var date = new Date(params);
                                return echarts.format.formatTime('<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>', date, true);
                            }

                        }
                    }
                ],
                yAxis: [
                    {
                        name: 'Differential Pressure',
                        x: 'left',
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value}'
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
                        name: 'Differential Pressure Max',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=maxVals.ToString().TrimEnd(',')%>],

                    },
                    {
                        name: 'Differential Pressure Min',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=minVals.ToString().TrimEnd(',')%>],

                    },
                    {
                        name: 'Differential Pressure Avg',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=avgVals.ToString().TrimEnd(',')%>],

                    },
                    {
                        name: 'Temperature Max',
                        type: 'line',
                        showAllSymbol: true,
                        yAxisIndex: 1,
                        data: [<%=Temperature_maxVals.ToString().TrimEnd(',')%>],

                    },
                    {
                        name: 'Temperature Min',
                        type: 'line',
                        showAllSymbol: true,
                        yAxisIndex: 1,
                        data: [<%=Temperature_minVals.ToString().TrimEnd(',')%>],

                },
                   {
                       name: 'Temperature Avg',
                       type: 'line',
                       showAllSymbol: true,
                       yAxisIndex: 1,
                       data: [<%=Temperature_avgVals.ToString().TrimEnd(',')%>],

                   }

                ]
            },
            media: [
                {
                    option: {
                        grid: {
                            left: 60,
                            top: '8%',
                            right: 40,

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
                            left: 60,
                            top: '8%',
                            right: 40,
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
                            left: 60,
                            top: '8%',
                            right: 40,
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

    });

</script>



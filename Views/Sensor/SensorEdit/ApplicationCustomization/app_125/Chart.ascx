<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%
    Sensor sens = Model.Sensors.ElementAt(0);
    bool isF = MultiStageThermostat.IsFahrenheit(sens.SensorID);
    int systemType = MultiStageThermostat.GetSystemType(sens);
    List<DataMessage> list = DataMessage.LoadAllForChart(
                   sens.SensorID,
                   Model.FromDate,
                   Model.ToDate
           );

    StringBuilder TemperatureDataVals = new StringBuilder();
    StringBuilder HeaterDataVals = new StringBuilder();
    StringBuilder CoolerDataVals = new StringBuilder();

    double occSetPoint = MultiStageThermostat.GetOccupiedSetPoint(sens);
    double coolingSetpoint = MultiStageThermostat.GetUnoccupiedCoolingSetpoint(sens);
    double heatingSetpoint = MultiStageThermostat.GetUnoccupiedHeatingSetpoint(sens);

    if (isF)
    {
        occSetPoint = occSetPoint.ToFahrenheit();
        heatingSetpoint = heatingSetpoint.ToFahrenheit();
        coolingSetpoint = coolingSetpoint.ToFahrenheit();
    }

    foreach (DataMessage item in list)
    {
        MultiStageThermostat Data = Monnit.MultiStageThermostat.Deserialize(sens.FirmwareVersion, item.Data);
        TemperatureDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (isF ? Data.Temperature.ToFahrenheit() : Data.Temperature).ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'Temperature' ,'" + item.DataMessageGUID +  "' ],");
        HeaterDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (((Data.Heat1.ToInt() + Data.Heat2.ToInt() + Data.Heat3.ToInt()) > 0) ? 1 : 0) + "," + ((item.State & 2) == 2 ).ToInt() + ", 'Heater' ,'" + item.DataMessageGUID +  "' ],");
        CoolerDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (((Data.Cool1.ToInt() + Data.Cool2.ToInt()) > 0) ? 1 : 0) + "," + ((item.State & 2) == 2 ).ToInt() + ", 'Cooler' ,'" + item.DataMessageGUID +  "' ],");
    }    
%>

<div id="sensorChartDiv" style="height: 350px;"></div>


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
                        var dateString = "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>';
                        var statusString = 'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                        var valueString = '';

                        switch (params.value[3]) {
                            case 'Temperature':
                                valueString = 'Temperature: ' + params.value[1] + ' °' + tempScale + '<br/>';
                                break;
                            case 'Heater':
                                valueString = 'Heater: ' + (params.value[1] == 1 ? 'On' : 'Off') + '<br/>';
                                break;
                            case 'Cooler':
                                valueString = 'A/C: ' + (params.value[1] == 1 ? 'On' : 'Off') + '<br/>';
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
                    padding: 1,
                    top: 'top',
                    data: ['Temperature', 'Heater', 'A/C'],

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
                    name: 'Temperature',
                    x: 'left',
                    type: 'value',
                    axisLabel: {
                        showMinLabel: false,
                        showMaxLabel: false,
                        formatter: '{value} °' + tempScale
                    }

                },
                {
                    name: 'Heater & A/C            ',//spaces to  align correctly
                    x: 'Right',
                    type: 'category',
                    inverse: false,
                    data: ['Off', 'On'],
                }
                ],
                series: [
                    {
                        name: 'Temperature',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=TemperatureDataVals.ToString().TrimEnd(',')%>],

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
                                    formatter: '(<%=occSetPoint%> °' + tempScale +') Occupied Set-Point'
                                }
                            },
                            yAxis: '<%=occSetPoint%>'
                        }, {
                                lineStyle: {
                                    color: 'blue',
                                },
                            label: {
                                normal: {
                                    color: 'black',
                                    position: 'middle',
                                    formatter: '(<%=coolingSetpoint%> °' + tempScale +') Unoccupied Cool Threshold                                                           '
                                }
                            },
                            yAxis: '<%=coolingSetpoint%>'
                            }, {
                                lineStyle: {
                                    color: 'Red',
                                },
                                label: {
                                    distance: [200, 15],
                                   
                                        color: 'black',
                                        position: 'middle',
                                    formatter: '                                                                                  (<%=heatingSetpoint%>°' + tempScale +') Unoccupied Heat Threshold'
                                    
                                },
                                yAxis: '<%=heatingSetpoint%>'
                            }]
                    }
                },
                  {
                      name: 'Heater',
                      type: 'line',
                      step: 'end',
                      showAllSymbol: true,
                      yAxisIndex: 1,
                      data: [<%=HeaterDataVals.ToString().TrimEnd(',')%>],

                  },
                {
                    name: 'A/C',
                    type: 'line',
                    step: 'end',
                    showAllSymbol: true,
                    yAxisIndex: 1,
                    data: [<%=CoolerDataVals.ToString().TrimEnd(',')%>],

                }
            ]
            },
            media: [
                            {
                                option: {
                                    grid: {
                                        left: 80,
                                        top: '12%',
                                        right: 50,

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
                                        top: '12%',
                                        right: 50,
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
                                        top: '12%',
                                        right: 50,
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



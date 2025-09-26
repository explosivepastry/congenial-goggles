<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%
    bool isHz = AdvancedVibration.IsHertz(Model.Sensors[0].SensorID);
    bool isF = AdvancedVibration.IsFahrenheit(Model.Sensors[0].SensorID);
    int mode = AdvancedVibration.GetVibrationMode(Model.Sensors[0]);
    List<DataMessage> list = DataMessage.LoadAllForChart(
          Model.Sensors[0].SensorID,
          Model.FromDate,
          Model.ToDate
          );


    StringBuilder X_FrequencyVals = new StringBuilder();
    StringBuilder Y_FrequencyVals = new StringBuilder();
    StringBuilder Z_FrequencyVals = new StringBuilder();

    //StringBuilder X_CrestFactorVals = new StringBuilder();
    //StringBuilder Y_CrestFactorVals = new StringBuilder();
    //StringBuilder Z_CrestFactorVals = new StringBuilder();

    StringBuilder X_ModeSpecificVals = new StringBuilder();
    StringBuilder Y_ModeSpecificVals = new StringBuilder();
    StringBuilder Z_ModeSpecificVals = new StringBuilder();

    StringBuilder TemperatureVals = new StringBuilder();
    StringBuilder DutyCycleVals = new StringBuilder();
    string ModeName = "";

    switch (mode)
    {
        case 0:
            ModeName = "Velocity Rms";
            break;
        case 1:
            ModeName = "Acceleration Rms";
            break;
        case 2:
            ModeName = "Acceleration Peak mm/s^2 ";
            break;
        case 4:
            ModeName = "Displacement";
            break;
        default:
            break;
    }

    foreach (DataMessage item in list)
    {
        AdvancedVibration Data = Monnit.AdvancedVibration.Deserialize(Model.Sensors[0].FirmwareVersion, item.Data);
        X_FrequencyVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (isHz ? Data.FrequencyAxisX : Data.FrequencyAxisX.ToRPM()).ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'XFrequency' ,'" + item.DataMessageGUID +  "' ],");
        Y_FrequencyVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (isHz ? Data.FrequencyAxisY : Data.FrequencyAxisY.ToRPM()).ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'YFrequency' ,'" + item.DataMessageGUID +  "' ],");
        Z_FrequencyVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (isHz ? Data.FrequencyAxisZ : Data.FrequencyAxisZ.ToRPM()).ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'ZFrequency' ,'" + item.DataMessageGUID +  "' ],");

        //X_CrestFactorVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.CrestFactorAxisX.ToDouble().ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'XCrestFactor' ],");
        //Y_CrestFactorVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.CrestFactorAxisY.ToDouble().ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'YCrestFactor' ],");
        //Z_CrestFactorVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.CrestFactorAxisZ.ToDouble().ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'ZCrestFactor' ],");

        TemperatureVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (isF ? Data.Temperature.ToFahrenheit() : Data.Temperature).ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'Temperature' ,'" + item.DataMessageGUID +  "' ],");
        DutyCycleVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.DutyCycle.ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'DutyCycle' ,'" + item.DataMessageGUID +  "' ],");

        switch (mode)
        {
            case 0:
                X_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.VelocityRmsAxisX.ToString("0.##") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'XVelocityRms' ,'" + item.DataMessageGUID +  "' ],");
                Y_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.VelocityRmsAxisY.ToString("0.##") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'YVelocityRms' ,'" + item.DataMessageGUID +  "' ],");
                Z_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.VelocityRmsAxisZ.ToString("0.##") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'ZVelocityRms' ,'" + item.DataMessageGUID +  "' ],");
                break;
            case 1:
                X_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.AccelerationRmsAxisX.ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'XAccelerationRms' ,'" + item.DataMessageGUID +  "' ],");
                Y_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.AccelerationRmsAxisY.ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'YAccelerationRms' ,'" + item.DataMessageGUID +  "' ],");
                Z_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.AccelerationRmsAxisZ.ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'ZAccelerationRms' ,'" + item.DataMessageGUID +  "' ],");
                break;
            case 2:
                X_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.AccelerationPeakAxisX.ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'XAccelerationPeak' ,'" + item.DataMessageGUID +  "' ],");
                Y_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.AccelerationPeakAxisY.ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'YAccelerationPeak' ,'" + item.DataMessageGUID +  "' ],");
                Z_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.AccelerationPeakAxisZ.ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'ZAccelerationPeak' ,'" + item.DataMessageGUID +  "' ],");
                break;
            case 4:
                X_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.DisplacementAxisX.ToString("0.##") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'XDisplacement' ,'" + item.DataMessageGUID +  "' ],");
                Y_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.DisplacementAxisY.ToString("0.##") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'YDisplacement' ,'" + item.DataMessageGUID +  "' ],");
                Z_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.DisplacementAxisZ.ToString("0.##") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'ZDisplacement' ,'" + item.DataMessageGUID +  "' ],");
                break;

            default:
                break;
        }
    }
        
%>

<div id="sensorChartDivFreq" style="height: 350px;"></div>
<br />
<div id="sensorChartDivMode" style="height: 350px;"></div>


<script type="text/javascript">


    $(document).ready(function () {

        var tempScale = '<%: isF?"F":"C" %>';
        var freqScale = '<%: isHz?" Hz":" RPM" %>';
        var modeName = '<%=ModeName%>';

        var echartBarFreqTemp = echarts.init(document.getElementById('sensorChartDivFreq', 'infographic'));
        

        echartBarFreqTemp.setOption({
            baseOption: {
                tooltip: {
                    backgroundColor: "#ffffff96",
                    textStyle: {
                        fontWeight: 'bold',
                        color: 'black',
                    },
                    trigger: 'item',
                    formatter: function (params) {
                        var dateString = "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>';
                        var statusString = 'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                        var valueString = '';
                        switch (params.value[3]) {
                            case 'XFrequency':
                                valueString = 'X-Axis Frequency: ' + params.value[1] + freqScale + '<br/>';
                                break;
                            case 'YFrequency':
                                valueString = 'Y-Axis Frequency: ' + params.value[1] + freqScale + '<br/>';
                                break;
                            case 'ZFrequency':
                                valueString = 'Z-Axis Frequency: ' + params.value[1] + freqScale + '<br/>';
                                break;
                            case 'Temperature':
                                valueString = 'Temperature: ' + params.value[1] + ' °' + tempScale + '<br/>';
                                break;
                        }

                        return dateString + valueString + statusString;
                    }
                },
                toolbox: {
                    feature: { saveAsImage: { show: true, title: 'Save', }, }
                },
                axisPointer: {
                    link: { xAxisIndex: 'all' }
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
                        ['X Frequency', 'Y Frequency', 'Z Frequency', 'Temperature'],

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
                        name: 'Frequency ' + freqScale,
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
                        name: 'Temperature               ',
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
                        name: 'X Frequency',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=X_FrequencyVals.ToString().TrimEnd(',')%>],

                    },
                    {
                        name: 'Y Frequency',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=Y_FrequencyVals.ToString().TrimEnd(',')%>],

                    },
                    {
                        name: 'Z Frequency',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=Z_FrequencyVals.ToString().TrimEnd(',')%>],

                    },
                    {
                        name: 'Temperature',
                        type: 'line',
                        yAxisIndex: 1,
                        showAllSymbol: true,
                        data: [<%=TemperatureVals.ToString().TrimEnd(',')%>],

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
                            right: 30,
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
       
        var echartBarModeDuty = echarts.init(document.getElementById('sensorChartDivMode', 'infographic'));
        echartBarModeDuty.setOption({
            baseOption: {
                tooltip: {
                    trigger: 'item',
                    formatter: function (params) {
                        var date = new Date(params.value[0]);

                        var dateString = "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>';
                        var statusString = 'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                        var valueString = '';
                        switch (params.value[3]) {
                            case 'XVelocityRms':
                                valueString = 'Velocity rms X: ' + params.value[1] + ' mm/s' + '<br/>';
                                break;
                            case 'YVelocityRms':
                                valueString = 'Velocity rms Y: ' + params.value[1] + ' mm/s' + '<br/>';
                                break;
                            case 'ZVelocityRms':
                                valueString = 'Velocity rms Z: ' + params.value[1] + ' mm/s' + '<br/>';
                                break;
                            case 'XAccelerationRms':
                                valueString = 'Acceleration rms X: ' + params.value[1] + ' mm/s^2' + '<br/>';
                                break;
                            case 'YAccelerationRms':
                                valueString = 'Acceleration rms Y: ' + params.value[1] + ' mm/s^2' + '<br/>';
                                break;
                            case 'ZAccelerationRms':
                                valueString = 'Acceleration rms Z: ' + params.value[1] + ' mm/s^2' + '<br/>';
                                break;
                            case 'XAccelerationPeak':
                                valueString = 'Acceleration Peak X: ' + params.value[1] + ' mm/s^2' + '<br/>';
                                break;
                            case 'YAccelerationPeak':
                                valueString = 'Acceleration Peak Y: ' + params.value[1] + ' mm/s^2' + '<br/>';
                                break;
                            case 'ZAccelerationPeak':
                                valueString = 'Acceleration Peak Z: ' + params.value[1] + ' mm/s^2' + '<br/>';
                                break;
                            case 'XDisplacement':
                                valueString = 'Displacement X: ' + params.value[1] + ' mm' + '<br/>';
                                break;
                            case 'YDisplacement':
                                valueString = 'Displacement Y: ' + params.value[1] + ' mm' + '<br/>';
                                break;
                            case 'ZDisplacement':
                                valueString = 'Displacement Z: ' + params.value[1] + ' mm' + '<br/>';
                                break;
                            case 'DutyCycle':
                                valueString = 'Duty Cycle: ' + params.value[1] + ' %' + '<br/>';
                                break;
                            default:
                                valueString = "";
                                break;
                        }

                        return dateString + valueString + statusString;

                    }
                },
                toolbox: {
                    feature: { saveAsImage: { show: true, title: 'Save', }, }
                },
                axisPointer: {
                    link: { xAxisIndex: 'all' }
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
                        ['X ' + modeName, 'Y ' + modeName, 'Z ' + modeName,'Duty Cycle' ],

                },
                grid: {
                    y2: 80,
                    left: 55,
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
                        name: modeName,
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
                        name: 'Duty Cycle        ',
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        inverse: false,
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value} %'
                        }
                    }
                ],
                series: [
                 {
                     name: 'X ' + modeName,
                     type: 'line',
                     showAllSymbol: true,
                     data: [<%=X_ModeSpecificVals.ToString().TrimEnd(',')%>],

                 },
                   {
                       name: 'Y ' + modeName,
                       type: 'line',
                       showAllSymbol: true,
                       data: [<%=Y_ModeSpecificVals.ToString().TrimEnd(',')%>],

                   },
                     {
                         name: 'Z ' + modeName,
                         type: 'line',
                         showAllSymbol: true,
                         data: [<%=Z_ModeSpecificVals.ToString().TrimEnd(',')%>],

                     },
                      {
                          name: 'Duty Cycle',
                          type: 'line',
                          yAxisIndex: 1,
                          showAllSymbol: true,
                          data: [<%=DutyCycleVals.ToString().TrimEnd(',')%>],

                      },
             
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
            if (echartBarModeDuty != null && echartBarModeDuty != undefined) {
                echartBarModeDuty.resize();
            }
            if (echartBarFreqTemp != null && echartBarFreqTemp != undefined) {
                echartBarFreqTemp.resize();
            }
        });


        echartBarModeDuty.on('mouseover', function (params) {
            $('.message_' + params.value[4]).css("background", "lightgray"); //change second element background
            setTimeout(function () {
                $('.message_' + params.value[4]).css('background', 'white'); // change it back after ...
            }, 3000); // waiting one second
        });

        echartBarFreqTemp.on('mouseover', function (params) {
            $('.message_' + params.value[4]).css("background", "lightgray"); //change second element background
            setTimeout(function () {
                $('.message_' + params.value[4]).css('background', 'white'); // change it back after ...
            }, 3000); // waiting one second
        });
    });

</script>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%
    string velocityScale = AdvancedVibration2.GetVelocityScale(Model.Sensors[0].SensorID);
    string accelerationScale = AdvancedVibration2.GetAccelerationScale(Model.Sensors[0].SensorID);
    string displacementScale = AdvancedVibration2.GetDisplacementScale(Model.Sensors[0].SensorID);

    bool isHz = AdvancedVibration2.IsHertz(Model.Sensors[0].SensorID);
    bool isF = AdvancedVibration2.IsFahrenheit(Model.Sensors[0].SensorID);
    int mode = AdvancedVibration2.GetVibrationMode(Model.Sensors[0]);
    List<DataMessage> list = DataMessage.LoadAllForChart(
          Model.Sensors[0].SensorID,
          Model.FromDate,
          Model.ToDate
          );


    StringBuilder X_FrequencyVals = new StringBuilder();
    StringBuilder Y_FrequencyVals = new StringBuilder();
    StringBuilder Z_FrequencyVals = new StringBuilder();

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
            ModeName = "Acceleration Peak";
            break;
        case 4:
            ModeName = "Displacement";
            break;
        default:
            break;
    }

    foreach (DataMessage item in list)
    {
        AdvancedVibration2 Data = Monnit.AdvancedVibration2.Deserialize(Model.Sensors[0].FirmwareVersion, item.Data);
        X_FrequencyVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (isHz ? Data.FrequencyAxisX : Data.FrequencyAxisX.ToRPM()).ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'XFrequency' ,'" + item.DataMessageGUID +  "' ],");
        Y_FrequencyVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (isHz ? Data.FrequencyAxisY : Data.FrequencyAxisY.ToRPM()).ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'YFrequency' ,'" + item.DataMessageGUID +  "' ],");
        Z_FrequencyVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (isHz ? Data.FrequencyAxisZ : Data.FrequencyAxisZ.ToRPM()).ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'ZFrequency' ,'" + item.DataMessageGUID +  "' ],");

  
        TemperatureVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (isF ? Data.Temperature.ToFahrenheit() : Data.Temperature).ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'Temperature' ,'" + item.DataMessageGUID +  "' ],");
        DutyCycleVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.DutyCycle.ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'DutyCycle' ,'" + item.DataMessageGUID +  "' ],");

        switch (mode)
        {
            case 0:
                X_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + AdvancedVibration2.ConvertVelocityByScale(Data.VelocityRmsAxisX,velocityScale).ToString("0.##") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'XVelocityRms' ,'" + item.DataMessageGUID +  "' ],");
                Y_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + AdvancedVibration2.ConvertVelocityByScale(Data.VelocityRmsAxisY,velocityScale).ToString("0.##") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'YVelocityRms' ,'" + item.DataMessageGUID +  "' ],");
                Z_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + AdvancedVibration2.ConvertVelocityByScale(Data.VelocityRmsAxisZ,velocityScale).ToString("0.##") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'ZVelocityRms' ,'" + item.DataMessageGUID +  "' ],");
                break;
            case 1:
                X_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + AdvancedVibration2.ConvertAccelerationByScale(Data.AccelerationRmsAxisX,accelerationScale).ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'XAccelerationRms' ,'" + item.DataMessageGUID +  "' ],");
                Y_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + AdvancedVibration2.ConvertAccelerationByScale(Data.AccelerationRmsAxisY,accelerationScale).ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'YAccelerationRms' ,'" + item.DataMessageGUID +  "' ],");
                Z_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + AdvancedVibration2.ConvertAccelerationByScale(Data.AccelerationRmsAxisZ,accelerationScale).ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'ZAccelerationRms' ,'" + item.DataMessageGUID +  "' ],");
                break;
            case 2:
                X_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + AdvancedVibration2.ConvertAccelerationByScale(Data.AccelerationPeakAxisX,accelerationScale).ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'XAccelerationPeak' ,'" + item.DataMessageGUID +  "' ],");
                Y_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + AdvancedVibration2.ConvertAccelerationByScale(Data.AccelerationPeakAxisY,accelerationScale).ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'YAccelerationPeak' ,'" + item.DataMessageGUID +  "' ],");
                Z_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + AdvancedVibration2.ConvertAccelerationByScale(Data.AccelerationPeakAxisZ,accelerationScale).ToString() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'ZAccelerationPeak' ,'" + item.DataMessageGUID +  "' ],");
                break;
            case 4:
                X_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + AdvancedVibration2.ConvertDisplacementByScale(Data.DisplacementAxisX,displacementScale).ToString("0.##") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'XDisplacement' ,'" + item.DataMessageGUID +  "' ],");
                Y_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + AdvancedVibration2.ConvertDisplacementByScale(Data.DisplacementAxisY,displacementScale).ToString("0.##") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'YDisplacement' ,'" + item.DataMessageGUID +  "' ],");
                Z_ModeSpecificVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + AdvancedVibration2.ConvertDisplacementByScale(Data.DisplacementAxisZ,displacementScale).ToString("0.##") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'ZDisplacement' ,'" + item.DataMessageGUID +  "' ],");
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

        const fontSize = 12;
        
        const legendLeftPct = 24;
        const legendWidthPct = 75;
        const legendTopPct = -1.5;
        const legendItemHeight = 14;
        const legendItemWidth = 25;
        
        const gridLeftRightPct = 5;
        const gridTopPct = 15;
        const gridY2Pct = 22;

        const xAxisSplitNumber = 8;
        const yAxisSplitNumber = 4;
        const yAxisNamePadding = 5;

        var tempScale = '<%: isF?"F":"C" %>';
        var freqScale = '<%: isHz?" Hz":" RPM" %>';
        var modeName = '<%=ModeName%>';

        echartBarFreqTemp = echarts.init(document.getElementById('sensorChartDivFreq', 'infographic'));
        echartBarFreqTemp.id = 'echartBarFreqTemp';
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
                    orient: 'horizontal',
                    top: numberToPercent(legendTopPct),
                    left: numberToPercent(legendLeftPct),
                    width: numberToPercent(legendWidthPct),
                    
                    data:
                        ['X Frequency', 'Y Frequency', 'Z Frequency', 'Temperature'],
                },
                grid: {
                    y2: numberToPercent(gridY2Pct),
                    left: numberToPercent(gridLeftRightPct),
                    right: numberToPercent(gridLeftRightPct),
                    top: numberToPercent(gridTopPct),
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
                        id: 'yAxisLeft',
                        name: 'Frequency ' + freqScale,
                        nameGap: 5,
                        x: 'left',
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value} ',
                            fontSize: fontSize,
                        },
                        nameTextStyle: {
                            fontSize: fontSize,
                            padding: [0, 0, 0, yAxisNamePadding],
                        },
                    },
                    {
                        id: 'yAxisRight',
                        name: 'Temperature',
                        nameGap: 5,
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        inverse: false,
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value} °' + tempScale,
                            fontSize: fontSize,
                        },
                        nameTextStyle: {
                            fontSize: fontSize,
                            padding: [0, yAxisNamePadding, 0, 0],
                        },
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
                            left: numberToPercent(gridLeftRightPct * 1.2),
                            right: numberToPercent(gridLeftRightPct * 1.2),
                        },
                        legend: {
                            textStyle: {
                                fontSize: fontSize,
                            },
                        },
                        xAxis: {
                            axisLabel: {
                                fontSize: fontSize,
                            },
                            splitNumber: xAxisSplitNumber,
                        },
                        yAxis: [
                            {
                                nameTextStyle: {
                                    fontSize: fontSize,
                                    padding: [0, 0, 0, yAxisNamePadding * 1.2],
                                },
                                axisLabel: {
                                    fontSize: fontSize,
                                },
                                splitNumber: yAxisSplitNumber,
                            },
                            {
                                nameTextStyle: {
                                    fontSize: fontSize,
                                    padding: [0, yAxisNamePadding * 1.2, 0, 0],
                                },
                                axisLabel: {
                                    fontSize: fontSize,
                                },
                                splitNumber: yAxisSplitNumber,
                            }
                        ]
                    },
                },
                {
                    query: {
                        maxWidth: 670, minWidth: 550
                    },
                    option: {
                        grid: {
                            left: numberToPercent(gridLeftRightPct * 1.4),
                            right: numberToPercent(gridLeftRightPct * 1.4),
                        },
                        legend: {
                            textStyle: {
                                fontSize: fontSize - 1,
                            },
                        },
                        xAxis: {
                            axisLabel: {
                                fontSize: fontSize - 1,
                            },
                            splitNumber: xAxisSplitNumber - 3,
                        },
                        yAxis: [
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 1,
                                    padding: [0, 0, 0, yAxisNamePadding * 1.4],
                                },
                                axisLabel: {
                                    fontSize: fontSize - 1,
                                },
                                splitNumber: yAxisSplitNumber,

                            },
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 1,
                                    padding: [0, yAxisNamePadding * 1.4, 0, 0],
                                },
                                axisLabel: {
                                    fontSize: fontSize - 1,
                                },
                                splitNumber: yAxisSplitNumber,
                            }
                        ],
                    }
                },
                {
                    query: {
                        maxWidth: 550, minWidth: 375
                    },
                    option: {
                        grid: {
                            left: numberToPercent(gridLeftRightPct * 1.6),
                            right: numberToPercent(gridLeftRightPct * 1.6),
                        },
                        legend: {
                            textStyle: {
                                fontSize: fontSize - 2,
                            },
                        },
                        xAxis: {
                            axisLabel: {
                                fontSize: fontSize - 2,
                            },
                            splitNumber: xAxisSplitNumber - 6
                        },
                        yAxis: [
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 2,
                                    padding: [0, 0, 0, yAxisNamePadding * 1.6],
                                },
                                axisLabel: {
                                    fontSize: fontSize - 2,
                                },
                                splitNumber: yAxisSplitNumber - 2,
                            },
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 2,
                                    padding: [0, yAxisNamePadding * 1.6, 0, 0],
                                },
                                axisLabel: {
                                    fontSize: fontSize - 2,
                                },
                                splitNumber: yAxisSplitNumber - 2,
                            }
                        ],
                    }
                },
                {
                    query: {
                        maxWidth: 375
                    },
                    option: {
                        grid: {
                            left: numberToPercent(gridLeftRightPct * 1.8),
                            right: numberToPercent(gridLeftRightPct * 1.8),
                        },
                        legend: {
                            textStyle: {
                                fontSize: fontSize - 4,
                            },
                            itemHeight: legendItemHeight / 2,
                            itemWidth:  legendItemWidth / 2,
                        },
                        xAxis: {
                            nameTextStyle: {
                                fontSize: fontSize - 4,
                            },
                            axisLabel: {
                                fontSize: fontSize - 4,
                            },
                            splitNumber: xAxisSplitNumber - 6,
                        },
                        yAxis: [
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 4,
                                    padding: [0, 0, 0, yAxisNamePadding * 1.8],
                                },
                                axisLabel: {
                                    fontSize: fontSize - 4,
                                },
                                splitNumber: yAxisSplitNumber - 2,
                            },
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 4,
                                    padding: [0, yAxisNamePadding * 1.8, 0, 0],
                                },
                                axisLabel: {
                                    fontSize: fontSize - 4,
                                },
                                splitNumber: yAxisSplitNumber - 2,
                            }
                        ],
                    }
                }
            ]
        });


        echartBarModeDuty = echarts.init(document.getElementById('sensorChartDivMode', 'infographic'));
        echartBarModeDuty.id = 'echartBarModeDuty';
        echartBarModeDuty.setOption({
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
                            case 'XVelocityRms':
                                valueString = 'Velocity rms X: ' + params.value[1] + ' <%=velocityScale%>' + '<br/>';
                                break;
                            case 'YVelocityRms':
                                valueString = 'Velocity rms Y: ' + params.value[1] + ' <%=velocityScale%>' + '<br/>';
                                break;
                            case 'ZVelocityRms':
                                valueString = 'Velocity rms Z: ' + params.value[1] + ' <%=velocityScale%>' + '<br/>';
                                break;
                            case 'XAccelerationRms':
                                valueString = 'Acceleration rms X: ' + params.value[1] + ' <%=accelerationScale%>' + '<br/>';
                                break;
                            case 'YAccelerationRms':
                                valueString = 'Acceleration rms Y: ' + params.value[1] + ' <%=accelerationScale%>' + '<br/>';
                                break;
                            case 'ZAccelerationRms':
                                valueString = 'Acceleration rms Z: ' + params.value[1] + ' <%=accelerationScale%>' + '<br/>';
                                break;
                            case 'XAccelerationPeak':
                                valueString = 'Acceleration Peak X: ' + params.value[1] + ' <%=accelerationScale%>' + '<br/>';
                                break;
                            case 'YAccelerationPeak':
                                valueString = 'Acceleration Peak Y: ' + params.value[1] + ' <%=accelerationScale%>' + '<br/>';
                                break;
                            case 'ZAccelerationPeak':
                                valueString = 'Acceleration Peak Z: ' + params.value[1] + ' <%=accelerationScale%>' + '<br/>';
                                break;
                            case 'XDisplacement':
                                valueString = 'Displacement X: ' + params.value[1] + ' <%=displacementScale%>' + '<br/>';
                                break;
                            case 'YDisplacement':
                                valueString = 'Displacement Y: ' + params.value[1] + ' <%=displacementScale%>' + '<br/>';
                                break;
                            case 'ZDisplacement':
                                valueString = 'Displacement Z: ' + params.value[1] + ' <%=displacementScale%>' + '<br/>';
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
                    orient: 'horizontal',
                    top: numberToPercent(legendTopPct),
                    left: numberToPercent(legendLeftPct),
                    width: numberToPercent(legendWidthPct),

                    data:
                        ['X ' + modeName, 'Y ' + modeName, 'Z ' + modeName,'Duty Cycle' ],

                },
                grid: {
                    y2: numberToPercent(gridY2Pct),
                    left: numberToPercent(gridLeftRightPct),
                    right: numberToPercent(gridLeftRightPct),
                    top: numberToPercent(gridTopPct),
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
                        nameGap: 5,
                        x: 'left',
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value} ',
                            fontSize,
                        },
                        nameTextStyle: {
                            fontSize: fontSize,
                            padding: [0, 0, 0, yAxisNamePadding],
                        },
                    },
                    {
                        name: 'Duty Cycle',
                        nameGap: 5,
                        type: 'value',
                        min: 'dataMin',
                        max: 'dataMax',
                        inverse: false,
                        axisLabel: {
                            showMinLabel: false,
                            showMaxLabel: false,
                            formatter: '{value} %',
                            fontSize: fontSize,
                        },
                        nameTextStyle: {
                            fontSize: fontSize,
                            padding: [0, yAxisNamePadding, 0, 0],
                        },
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
                            left: numberToPercent(gridLeftRightPct * 1.2),
                            right: numberToPercent(gridLeftRightPct * 1.2),
                        },
                        legend: {
                            textStyle: {
                                fontSize: fontSize,
                            },
                        },
                        xAxis: {
                            axisLabel: {
                                fontSize: fontSize,
                            },
                            splitNumber: xAxisSplitNumber,
                        },
                        yAxis: [
                            {
                                nameTextStyle: {
                                    fontSize: fontSize,
                                    padding: [0, 0, 0, yAxisNamePadding * 1.2],
                                },
                                axisLabel: {
                                    fontSize: fontSize,
                                },
                                splitNumber: yAxisSplitNumber,
                            },
                            {
                                nameTextStyle: {
                                    fontSize: fontSize,
                                    padding: [0, yAxisNamePadding * 1.2, 0, 0],
                                },
                                axisLabel: {
                                    fontSize: fontSize,
                                },
                                splitNumber: yAxisSplitNumber,
                            }
                        ]
                    }
                },
                {
                    query: {
                        maxWidth: 670, minWidth: 550
                    },
                    option: {
                        grid: {
                            left: numberToPercent(gridLeftRightPct * 1.4),
                            right: numberToPercent(gridLeftRightPct * 1.4),
                        },
                        legend: {
                            textStyle: {
                                fontSize: fontSize - 1,
                            },
                        },
                        xAxis: {
                            axisLabel: {
                                fontSize: fontSize - 1,
                            },
                            splitNumber: xAxisSplitNumber - 3,
                        },
                        yAxis: [
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 1,
                                    padding: [0, 0, 0, yAxisNamePadding * 1.4],
                                },
                                axisLabel: {
                                    fontSize: fontSize - 1,
                                },
                                splitNumber: yAxisSplitNumber,

                            },
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 1,
                                    padding: [0, yAxisNamePadding * 1.4, 0, 0],
                                },
                                axisLabel: {
                                    fontSize: fontSize - 1,
                                },
                                splitNumber: yAxisSplitNumber,
                            }
                        ],
                    }
                },
                {
                    query: {
                        maxWidth: 550, minWidth: 375
                    },
                    option: {
                        grid: {
                            left: numberToPercent(gridLeftRightPct * 1.6),
                            right: numberToPercent(gridLeftRightPct * 1.6),
                        },
                        legend: {
                            textStyle: {
                                fontSize: fontSize - 2,
                            },
                        },
                        xAxis: {
                            axisLabel: {
                                fontSize: fontSize - 2,
                            },
                            splitNumber: xAxisSplitNumber - 6
                        },
                        yAxis: [
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 2,
                                    padding: [0, 0, 0, yAxisNamePadding * 1.6],
                                },
                                axisLabel: {
                                    fontSize: fontSize - 2,
                                },
                                splitNumber: yAxisSplitNumber - 2,
                            },
                            {
                                nameTextStyle: {
                                    fontSize: fontSize - 2,
                                    padding: [0, yAxisNamePadding * 1.6, 0, 0],
                                },
                                axisLabel: {
                                    fontSize: fontSize - 2,
                                },
                                splitNumber: yAxisSplitNumber - 2,
                            }
                        ],
                    }
                 },
                 {
                     query: {
                         maxWidth: 375
                     },
                     option: {
                         grid: {
                             left: numberToPercent(gridLeftRightPct * 1.8),
                             right: numberToPercent(gridLeftRightPct * 1.8),
                         },
                         legend: {
                             textStyle: {
                                 fontSize: fontSize - 4,
                             },
                             itemHeight: legendItemHeight / 2,
                             itemWidth: legendItemWidth / 2,
                         },
                         xAxis: {
                             nameTextStyle: {
                                 fontSize: fontSize - 4,
                             },
                             axisLabel: {
                                 fontSize: fontSize - 4,
                             },
                             splitNumber: xAxisSplitNumber - 6,
                         },
                         yAxis: [
                             {
                                 nameTextStyle: {
                                     fontSize: fontSize - 4,
                                     padding: [0, 0, 0, yAxisNamePadding * 2],
                                 },
                                 axisLabel: {
                                     fontSize: fontSize - 4,
                                 },
                                 splitNumber: yAxisSplitNumber - 2,
                             },
                             {
                                 nameTextStyle: {
                                     fontSize: fontSize - 4,
                                     padding: [0, yAxisNamePadding * 1.8, 0, 0],
                                 },
                                 axisLabel: {
                                     fontSize: fontSize - 4,
                                 },
                                 splitNumber: yAxisSplitNumber - 2,
                             }
                         ],
                     }
                 }
             ]
         });

        //echartBarModeDuty.on('rendered',
        //    function () {
        
        //        var chart = this;
        //        console.log('------------------------------------------------------------------------------------------');
        //        console.log('chart.id = ' + chart.id);
        //        console.log("chart.getOption()['grid'][0].top = " + chart.getOption()['grid'][0].top);
        //        console.log("chart.getOption()['grid'][0].bottom = " + chart.getOption()['grid'][0].bottom);
        //        console.log("chart.getOption()['grid'][0].left = " + chart.getOption()['grid'][0].left);
        //        console.log("chart.getOption()['grid'][0].right = " + chart.getOption()['grid'][0].right);
        //        console.log("chart.getOption()['grid'][0].height = " + chart.getOption()['grid'][0].height);
        //        console.log("chart.getOption()['grid'][0].width = " + chart.getOption()['grid'][0].width);
        //        console.log("chart.getOption()['grid'][0].y2 = " + chart.getOption()['grid'][0].y2);
        //        console.log('');
        //        console.log("chart.getOption()['legend'][0].top = " + chart.getOption()['legend'][0].top);
        //        console.log("chart.getOption()['legend'][0].bottom = " + chart.getOption()['legend'][0].bottom);
        //        console.log("chart.getOption()['legend'][0].left = " + chart.getOption()['legend'][0].left);
        //        console.log("chart.getOption()['legend'][0].right = " + chart.getOption()['legend'][0].right);
        //        console.log("chart.getOption()['legend'][0].height = " + chart.getOption()['legend'][0].height);
        //        console.log("chart.getOption()['legend'][0].width = " + chart.getOption()['legend'][0].width);
        //        console.log("chart.getOption()['legend'][0].itemHeight = " + chart.getOption()['legend'][0].itemHeight);
        //        console.log("chart.getOption()['legend'][0].itemWidth = " + chart.getOption()['legend'][0].itemWidth);
        //        console.log("chart.getOption()['legend'][0].textStyle.fontSize = " + chart.getOption()['legend'][0].textStyle.fontSize);
        //        console.log('');
        //        console.log("chart.getOption()['yAxis'][0].nameTextStyle.padding = " + chart.getOption()['yAxis'][0].nameTextStyle.padding);
        //        console.log("chart.getOption()['yAxis'][1].nameTextStyle.padding = " + chart.getOption()['yAxis'][1].nameTextStyle.padding);
        //        console.log('');
        //        console.log("chart.getHeight() = " + chart.getHeight());
        //        console.log("chart.getWidth() = " + chart.getWidth());
        //        console.log('');
        //        console.log('chart.getModel().getComponent("yAxis", 0) = ' + chart.getModel().getComponent("yAxis", 0));
        //        console.log('chart.getModel().getComponent("yAxis", 1) = ' + chart.getModel().getComponent("yAxis", 1));
        //        console.log('**********************************************************************************************');
        //        console.log('');
        //    }
        //);

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
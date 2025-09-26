<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%
	List<DataMessage> list = DataMessage.LoadAllForChart(
		   Model.Sensors[0].SensorID,
		   Model.FromDate,
		   Model.ToDate
		   );


	StringBuilder PPFDDataVals = new StringBuilder();
	StringBuilder LightStateVals = new StringBuilder();
	StringBuilder PARDLIVals = new StringBuilder();
    	
	foreach (DataMessage item in list)
	{
		LightSensor_PPFD Data = Monnit.LightSensor_PPFD.Deserialize(Model.Sensors[0].FirmwareVersion, item.Data);
		PPFDDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.PlotValue + "," + ((item.State & 2) == 2 ).ToInt() + ", 'μmol/m2/s' ,'" + item.DataMessageGUID +  "' ],");
		LightStateVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.LightState.ToInt() + "," + ((item.State & 2) == 2 ).ToInt() + ", 'Light' ,'" + item.DataMessageGUID +  "' ],");
        PARDLIVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.PARDLI + "," + ((item.State & 2) == 2 ).ToInt() + ", 'mol/m2/day' ,'" + item.DataMessageGUID +  "' ],");
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
						if (params.value[3] == 'μmol/m2/s') {

                            return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
								'PPFD: ' + params.value[1] + ' μmol/m2/s' + '<br/>' +
                               'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                        } else if (params.value[3] == 'mol/m2/day') {

                            return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                                'DLI: ' + params.value[1] + 'mol/m2/day' + '<br/>' +
                                'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                        } else {

                            return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                                'Value: ' + (params.value[1] == 1 ? 'Light' : params.value[1] == 2? 'Saturated': 'No Light') + '<br/>' +
                                   'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
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
					data: ['PPFD', 'DLI', 'Light State'],

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
                    name: 'PPFD μmol/m2/s',
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
                    name: 'DLI  mol/m2/d',
                    x: 'right',
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
                    name: '',
                    x: 'left',
                    type: 'category',
                    inverse: false,
					data: ['', '', '']
                }
                ],
                series: [
                    {
						name: 'PPFD',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=PPFDDataVals.ToString().TrimEnd(',')%>],

                    },
                    {
                        name: 'DLI',
                        type: 'line',
                        showAllSymbol: true,
                        yAxisIndex: 1,
                        data: [<%=PARDLIVals.ToString().TrimEnd(',')%>],

                    },
                    {
                      name: 'Light State',
                      type: 'line',
                      step: 'end',
                      showAllSymbol: true,
                      yAxisIndex: 2,
                      data: [<%=LightStateVals.ToString().TrimEnd(',')%>],

                    }
                ]
            },
            media: [
                            {
                                option: {
                                    grid: {
                                        left: 80,
                                        top: '8%',
                                        right: 70,

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
                                        left: 80,
                                        top: '8%',
                                        right: 30,
                                    },
                                    xAxis: {
                                        nameLocation: 'middle',
                                        nameGap: 25,
                                        splitNumber: 3
                                    },
                                    yAxis: {
                                        splitNumber: 3,
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



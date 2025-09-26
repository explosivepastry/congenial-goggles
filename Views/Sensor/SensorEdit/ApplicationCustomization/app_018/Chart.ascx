<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%
  
    bool isF = Humidity.IsFahrenheit(Model.Sensors[0].SensorID);

    List<DataMessage> list = DataMessage.LoadAllForChart(
           Model.Sensors[0].SensorID,
           Model.FromDate,
           Model.ToDate
           );


    StringBuilder TemperatureDataVals = new StringBuilder();
    StringBuilder HumidityDataVals = new StringBuilder();

    foreach (DataMessage item in list)
    {
        Humidity Data = Monnit.Humidity.Deserialize(Model.Sensors[0].FirmwareVersion, item.Data);
        TemperatureDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (isF ? Data.Temperature.ToFahrenheit() : Data.Temperature).ToDouble().ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'Temperature' ],");
        HumidityDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.Hum.ToString("#0.##") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'Humidity' ],");
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
                        type: 'value', min: 'dataMin', max: 'dataMax',
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


        $(window).on('resize', function () {
            if (echartBar != null && echartBar != undefined) {
                echartBar.resize();
            }
        });

    });



</script>



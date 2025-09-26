<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%
    DateTime utcFromDate = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime utcToDate = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    List<DataPoint> PlotVals;
    int messageCount = 0;

    //TimeSpan ts = utcToDate.Subtract(utcFromDate);
    //if (ts.TotalDays >= 7)

    if (Model.Sensors[0].MonnitApplication.IsTriggerProfile == eApplicationProfileType.Interval)
    {
        if (Model.Sensors[0].ApplicationID != 14)
        {
            List<DataMessage> list = DataMessage.LoadAllForChart(
                   Model.Sensors[0].SensorID,
                   utcFromDate,
                   utcToDate
                   );

            PlotVals = (from dm in list
                        select new DataPoint()
                            {
                                Date = Monnit.TimeZone.GetLocalTimeById(dm.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID),
                                Value = dm.AppBase.PlotValue,
                                SentNotification = (dm.State & 2) == 2
                            }).ToList();
        }
        else
        {
            PlotVals = DataMessage.CountByDay_LoadForChart(Model.Sensors[0].SensorID, utcFromDate, utcToDate, 150);
        }
    }
    else
    {
        Monnit.TimeZone TimeZone = Monnit.TimeZone.Load(MonnitSession.CurrentCustomer.Account.TimeZoneID);
        TimeSpan offset = Monnit.TimeZone.GetCurrentLocalOffset(TimeZone.Info);

        //Trigger Sensors
        PlotVals = DataMessage.CountAwareByDay_LoadForChart(Model.Sensors[0].SensorID, utcFromDate, utcToDate, offset.Hours, 150);
    }

    StringBuilder dataVals = new StringBuilder();
    foreach (DataPoint item in PlotVals)
    {
        dataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.Date, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + item.Value + "," + item.SentNotification.ToInt() + "],");
    }

    List<AppDatum> datum = MonnitApplicationBase.GetAppDatums(Model.Sensors[0].ApplicationID);
        
%>

<div id="sensorChartDiv" style="height: 350px;"></div>

<script type="text/javascript">

    $(document).ready(function () {
        var echartBar = echarts.init(document.getElementById('sensorChartDiv', 'infographic'));
        echartBar.setOption({

            baseOption: {
                tooltip: {
                    trigger: 'item',
                    formatter: function (params) {
                        var date = new Date(params.value[0]);
                        return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                           'Value: ' + params.value[1] + ' ' + '<br/>' +
                           'Status: ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                    }
                },
                toolbox: {
                    feature: {
                        saveAsImage: {
                            show: true,
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
                xAxis: [
                    {
                        type: 'time',
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
                        type: 'value', min: 'dataMin', max: 'dataMax',
                        axisLabel: {
                            formatter: '{value}'
                        }
                    }
                ],
                series: [
                    {
                        name: '<%= datum[0].type%>',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=dataVals.ToString().TrimEnd(',')%>],

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

    });

</script>



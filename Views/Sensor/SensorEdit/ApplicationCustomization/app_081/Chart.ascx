<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%
    List<DataPoint> PlotVals;
    //if (Model.Sensors.ElementAt(0).MonnitApplication.IsTriggerProfile == eApplicationProfileType.Interval)
    //{
    if (Model.Sensors.ElementAt(0).ApplicationID != 14)
    {
        List<DataMessage> list = DataMessage.LoadForChart(
               Model.Sensors.ElementAt(0).SensorID,
               Model.FromDate,
               Model.ToDate,
               150);

        PlotVals = (from dm in list
                    select new DataPoint()
                        {
                            Date = dm.MessageDate,
                            Value = dm.AppBase.PlotValue,
                            SentNotification = (dm.State & 2) == 2
                        }).ToList();

    }
    else
    {
        PlotVals = DataMessage.CountByDay_LoadForChart(Model.Sensors.ElementAt(0).SensorID, Model.FromDate, Model.ToDate, 150);
    }
    //}
    //else
    //{
    //    Monnit.TimeZone TimeZone = Monnit.TimeZone.Load(MonnitSession.CurrentCustomer.Account.TimeZoneID);
    //    TimeSpan offset = Monnit.TimeZone.GetCurrentLocalOffset(TimeZone.Info);

    //    //Trigger Sensors
    //    PlotVals = DataMessage.CountAwareByDay_LoadForChart(Model.Sensors.ElementAt(0).SensorID, Model.FromDate, Model.ToDate, offset.Hours, 150);
    //}

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
                    backgroundColor: "#ffffff96",
                    textStyle: {
                        fontWeight: 'bold',
                        color: 'black',
                    },
                    trigger: 'axis'
                },
                toolbox: {
                    feature: { saveAsImage: { show: false, title: 'Save', }, }
                },
                dataZoom: {
                    show: true,
                    realtime: true,
                    start: 0,
                    end: 100,
                },
                legend: {
                    type: 'scroll',
                    data: ['<%= datum[0].type%>'],
             x: 'left'
         },
         grid: {
             y2: 80
         },
         xAxis: [{
             type: 'time',

         }],
         yAxis: [{
             type: 'value',
             name: '<%= datum[0].type%>',
         }],
           series: [{
               name: 'status',
               type: 'line',
               step: 'end',
               showAllSymbol: true,
               data: [<%=dataVals.ToString().TrimEnd(',')%>],
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
        });


        $(window).on('resize', function () {
            if (echartBar != null && echartBar != undefined) {
                echartBar.resize();
            }
        });

    });



</script>



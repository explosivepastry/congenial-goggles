<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%

    //List<DataMessage> list = DataMessage.LoadAllForChart(
    //       Model.Sensors[0].SensorID,
    //       Model.FromDate,
    //       Model.ToDate
    //       );

    string sensorIds = string.Join("|", Model.Sensors.Select(s => s.SensorID.ToString()));

    Dictionary<DateTime, PeopleCounter> list = PeopleCounter.PeopleCounter_AggData(sensorIds, MonnitSession.HistoryFromDate, MonnitSession.HistoryToDate);

    StringBuilder dataValsIn = new StringBuilder();
    StringBuilder dataValsOut = new StringBuilder();
    StringBuilder dataValsOcc = new StringBuilder();
    //int maxRange = list.Count();
    //maxRange = maxRange > 1000 ? 1000 : maxRange;
    //list = list.GetRange(0, maxRange);
    foreach (DateTime dateTime in list.Keys)
    {

        PeopleCounter Data = list[dateTime];

        dataValsIn.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(dateTime, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.InCount.ToString("#0.###", System.Globalization.CultureInfo.InvariantCulture) + "],");
        dataValsOut.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(dateTime, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + "),-" + Data.OutCount.ToString("#0.###", System.Globalization.CultureInfo.InvariantCulture) + "],");
        dataValsOcc.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(dateTime, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.OccupancyCount.ToString("#0.###", System.Globalization.CultureInfo.InvariantCulture) + "],");

    }
%>
                
<%Html.RenderPartial("SensorGroupButton", Model);%>

<div id="sensorChartDiv" style="height: 350px;"></div>


<%--the Apache Chart Monnit theme--%>
<script src="/content/Overview/js/gridEyeChartTheme.js"></script>

<script>
    function refreshChartdata() {
        
        var ids = "";
        $("input[name='sensorGroupCheckboxes']").filter(':checked').each(function () { ids += $(this).val() + "|"; })
        $('#ChartDiv').html("Loading...");
        return $.post('/Overview/RefreshPeopleCounterChartData', { id: <%=Model.Sensors[0].SensorID%>, selectedSensors: ids }, function (data) {
            
                $('#ChartDiv').html("...Waiting...");
                setTimeout(function () { $('#ChartDiv').html(data); }, 1000);
        }).fail(function (response) {
            
                showSimpleMessageModal("<%=Html.TranslateTag("Error response from mobiDataRefresh ()")%>");
            });
            }

    function mobiDataRefresh() {
        refreshChartdata()
    }
</script>

<script type="text/javascript">

    $(document).ready(function () {
        var chartDom = document.getElementById('sensorChartDiv');
        var myChart = echarts.init(chartDom, "monnit", { renderer: 'svg' });
        var option;

        option = {
            tooltip: {
                backgroundColor: "#ffffff96",
                textStyle: {
                    fontWeight: 'bold',
                    color: 'black',
                },
                trigger: 'axis',
                axisPointer: {
                    type: 'cross',
                    label: {
                        show: false
                    },
                    crossStyle: {
                        color: '#999'
                    }
                }
            },
            toolbox: {
                feature: {
                    dataZoom: {
                        show: window.innerWidth < 450 ? false : true,
                        brushStyle: {
                            color: 'rgba(159, 219, 237, 0.75)'
                        }
                    }
                }
            }, dataZoom: [
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
                data: ['Count In', 'Count Out', 'Occupancy']
            },
            xAxis: [{
                type: 'time',
                min: <%=MonnitSession.HistoryFromDate.ToEpochTime()%>,
                max: <%=MonnitSession.HistoryToDate.ToEpochTime()%>,
                axisLabel: {
                    formatter: function (params) {
                        var date = new Date(params);
                        return TimePreferenceFormat(params, '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '\r\n' + DatePreferenceFormat(params, '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"]) %>');
                    }

                },
                axisPointer: {
                    type: 'shadow'
                }
            }],

            yAxis: [
                {
                    type: 'value',
                    min: 'dataMin',
                    max: 'dataMax',
                    name: window.innerWidth > 450 ? 'In/Out Count' : '',
                    axisLabel: {
                        formatter: '{value}'
                    },
                },
                {
                    show: false,
                    type: 'value',
                    name: 'Occupancy',
                    axisLabel: {
                        formatter: '{value}'
                    }
                }
            ],
            series: [
                {
                    name: 'Count In',
                    type: 'bar',
                    tooltip: {
                        valueFormatter: function (value) {
                            return `+${value}`;
                        }
                    },
                    data: [<%=dataValsIn.ToString().TrimEnd(',')%>],
                    itemStyle: {
                        color: {
                            type: 'linear',
                            y: 0.75,
                            x: 0.25,
                            y2: 0,
                            x2: 0,
                            colorStops: [
                                {
                                    offset: 0,
                                    color: '#08AFE6'
                                },
                                {
                                    offset: 1,
                                    color: '#0469AD'
                                }
                            ]
                        }
                    },
                },
                {
                    name: 'Count Out',
                    type: 'bar',
                    tooltip: {
                        valueFormatter: function (value) {
                            return value;
                        }
                    },
                    data: [<%=dataValsOut.ToString().TrimEnd(',')%>],
                    itemStyle: {
                        color: {
                            type: 'linear',
                            y: 0.75,
                            x: 0.25,
                            y2: 0,
                            x2: 0,
                            colorStops: [
                                {
                                    offset: 0,
                                    color: '#DA5523'
                                },
                                {
                                    offset: 1,
                                    color: '#F89725'
                                }
                            ]
                        }
                    }
                },
                {
                    name: 'Occupancy',
                    type: 'line',
                    symbol: 'circle',
                    symbolSize: 5,
                    tooltip: {
                        valueFormatter: function (value) {
                            return value + ' People';
                        }
                    },
                    data: [<%=dataValsOcc.ToString().TrimEnd(',')%>],
                    itemStyle: {
                        color: '#515356',
                    }
                }
            ],
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

        };


        $(window).on('resize', function () {
            if (myChart != null && myChart != undefined) {
                myChart.resize();
            }
        });
        option && myChart.setOption(option);
    });
</script>

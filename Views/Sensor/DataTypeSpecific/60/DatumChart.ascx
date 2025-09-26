<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.MultiChartSensorDataModel>" %>

<!--DatumType 60 PAR_DLI-->

<%  

    int sensorCount = (int)ViewBag.sensorCount;  %>
<div class="row chart_row">
    <div class="col-12 col-lg-3 col-xl-3 chart_row__details" style="text-align: center;">
        <%=Model.GroupSensor.Sensor.SensorName %><br />
        <%=HttpUtility.UrlDecode(Model.GroupSensor.Alias) %><br />
        <a target="_blank" href="/Overview/SensorChart/<%=Model.GroupSensor.SensorID %>" role="link" class="btn btn-primary btn-sm" style="width: 150px;">
            <%=Html.GetThemedSVG("chart") %>
            &nbsp;<%: Html.TranslateTag("Details","Details")%></a>
        <a href="/Overview/SensorHome/<%=Model.GroupSensor.SensorID %>" role="link" target="_blank" class="btn btn-secondary btn-sm mt-2" style="width: 150px;"><%=Html.GetThemedSVG("list") %>&nbsp;<%: Html.TranslateTag("History","History")%></a>
    </div>
    <div class="col-12 col-lg-9 col-xl-9">
        <div id="sensorChartDiv_<%=Model.GroupSensor.SensorID%>_<%=Model.GroupSensor.DatumIndex%>" style="height: <%=sensorCount > 5 ? "100px" :"200px" %>; width: 100%"></div>
    </div>
</div>

<script type="text/javascript">


    $(document).ready(function () {



        var label = '<%: Model.Label %>';
        var echartBar = echarts.init(document.getElementById('sensorChartDiv_<%=Model.GroupSensor.SensorID%>_<%=Model.GroupSensor.DatumIndex%>', 'infographic'));

        echartBar.setOption({

            baseOption: {
                tooltip: {
                    trigger: 'item',
                    formatter: function (params) {
                        var date = new Date(params.value[0]);
                        return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + ' ' + TimePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>') + '<br/>' +
                            'Value: ' + params.value[1] + ' ' + label;
                    }
                },
                grid: {
                    // y2: 80,
                    top: '8%',
                    left: 50,
                    right: 30,
                    bottom: '20%',

                },
                xAxis: [
                    {
                        //show:false,
                        type: 'time',
                        min: new Date(<%=MonnitSession.HistoryFromDate.ToEpochTime()%>),
                        max: new Date(<%=MonnitSession.HistoryToDate.ToEpochTime()%>),
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

                        //type: 'value',min:'dataMin',max:'dataMax',
                        type: 'value',
                        name: label,
                        nameLocation: 'middle',
                        nameGap: 35,
                    }
                ],
                series: [
                    {
                        name: label,
                        ShowSymbol: 'false',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=Model.MessageString.ToString().TrimEnd(',')%>],

                    }
                ]

            },
            media: [
                {
                    option: {
                        grid: {
                            left: 50,
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
                            left: 50,
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
                            left: 50,
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


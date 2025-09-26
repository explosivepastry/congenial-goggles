<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<iMonnit.Models.CellDataUseModel>>" %>

<%
    StringBuilder dataVals = new StringBuilder();

    string YLabel = "";

    Dictionary<int, string> months = new Dictionary<int, string>();
    months.Add(1, Html.TranslateTag("Jan"));
    months.Add(2, Html.TranslateTag("Feb"));
    months.Add(3, Html.TranslateTag("Mar"));
    months.Add(4, Html.TranslateTag("April"));
    months.Add(5, Html.TranslateTag("May"));
    months.Add(6, Html.TranslateTag("June"));
    months.Add(7, Html.TranslateTag("July"));
    months.Add(8, Html.TranslateTag("Aug"));
    months.Add(9, Html.TranslateTag("Sept"));
    months.Add(10, Html.TranslateTag("Oct"));
    months.Add(11, Html.TranslateTag("Nov"));
    months.Add(12, Html.TranslateTag("Dec"));


    if (Model.Max(m => m.MB) < 1)
    {
        YLabel = "KB";
        foreach (CellDataUseModel item in Model)
        {

            dataVals.Append("[new Date(" + Monnit.TimeZone.GetUTCFromLocalById(item.Date, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (item.KB).ToString() + "],");
            //dataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.Date, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + (item.KB).ToString() + "],");
        }
    }
    else
    {
        YLabel = "MB";
        foreach (CellDataUseModel item in Model)
        {
            dataVals.Append("[new Date(" + item.Date.ToEpochTime() + ")," + item.MB.ToString() + "],");
            //dataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.Date, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + item.MB.ToString() + "],");
        }
    }

%>
<div id="gatewayCellUseChartDiv" style="height: 350px;"></div>


<script type="text/javascript">



    $(function () {

        var monthName = ['<%:months[1]%>', '<%:months[2]%>', '<%:months[3]%>', '<%:months[4]%>', '<%:months[5]%>', '<%:months[6]%>', '<%:months[7]%>', '<%:months[8]%>', '<%:months[9]%>', '<%:months[10]%>', '<%:months[11]%>', '<%:months[12]%>'];

        var echartBar = echarts.init(document.getElementById('gatewayCellUseChartDiv', 'infographic'));

        echartBar.setOption({

            baseOption: {
                tooltip: {
                    trigger: 'item',
                    formatter: function (params) {
                        <%if (ViewBag.Range == "Year")
    {%>
                        var dateVal = new Date(params.value[0]);
                        return "Month: " + monthName[dateVal.getMonth()] + '-' + dateVal.getFullYear() + '<br/>' +
                                '<%:YLabel%>: ' + params.value[1];

                        <%}
    else
    { %>
                        return "Date: " + DatePreferenceFormat(params.value[0], '<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"])%>') + '<br/>' +
                                '<%:YLabel%>: ' + params.value[1];
                        <%}%>

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
                    data: ['Cellular Data Usage'],
                },
                grid: {
                    y2: 80,
                    left: 70,
                    right: 50,
                },
                xAxis: [{
                    type: 'time',
                    axisLabel: {
                        formatter: function (params) {
                            var dateVal = new Date(params);
                            <%if (ViewBag.Range == "Year") {%>
                                return monthName[dateVal.getMonth()] + '\n' + dateVal.getFullYear();
                            <%}
                            else
                            { %>
                                return echarts.format.formatTime('<%=MonnitSession.CurrentCustomer.Preferences["Date Format"]%>', dateVal, true);
                            <%}%>
                        }

                    }
                }],
                yAxis: [{
                    type: 'value',
                    axisLabel: {
                        formatter: '{value} <%:YLabel%>'
                    }
                }],
                series: [
                    {
                        name: 'Cellular Data Use',
                        type: 'line',
                        showAllSymbol: true,
                        data: [<%=dataVals.ToString().TrimEnd(',')%>]
                    }
                ]
            },
            media: [
                {
                    option: {
                        grid: {
                            left: 80,
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
                            splitNumber: 5,
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
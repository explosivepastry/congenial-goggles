<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>
<% 
    
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();


    SensorAttribute ChartFormat = Monnit.SensorAttribute.LoadBySensorID(Model.Sensors.ElementAt(0).SensorID).Where(cf => { return cf.Name.ToLower() == "chartformat"; }).ToList().FirstOrDefault();
    if (ChartFormat != null)
    {
        ViewBag.chartformat = ChartFormat.Value;
    }
    
%>

<script type='text/javascript'>
    //setOnLoadCallback will wait for the page to load before Loading the chart.
    google.charts.setOnLoadCallback(drawChart);
    function drawChart() {
        var dashboard = new google.visualization.Dashboard(document.getElementById('dashboard'));
    
        <%string ChartType = "LineChart";
        if ((Model.Sensors.ElementAt(0).MonnitApplication.IsTriggerProfile == eApplicationProfileType.Trigger || Model.Sensors.ElementAt(0).ApplicationID == 14))
        {
            ChartType = "SteppedAreaChart";
        }%>
    
        // This wrapper is for the graph which allows you to adjust the date
        var chartRangeWrapper = new google.visualization.ControlWrapper({
            'controlType': 'ChartRangeFilter',
            'containerId': 'control_div',
            //OPTIONS 
            'options': {
                // Filter by the date axis.
                'filterColumnIndex': 0,
                'ui': {
                    // Only the following options are currenly supported: 'AreaChart', 'LineChart', 'ComboChart' or 'ScatterChart'.04/06/2017
                    //https://developers.google.com/chart/interactive/docs/gallery/controls#chartrangefilter
                    'chartType': '<%= ChartType%>',
                    'chartOptions': {
                        'chartArea': { 'width': '90%' },
                        'interpolateNulls': 'true',

                        'hAxis': {
                            'baselineColor': 'None',
                            'viewWindow': {
                                'min': new Date('<%: MonnitSession.HistoryFromDate.ToString("MM/dd/yyyy HH:mm:ss")%>'),
                                'max': new Date('<%: MonnitSession.HistoryToDate.AddDays(1).ToString("MM/dd/yyyy HH:mm:ss")%>')
                            }
                        },
                        'gridlines': {
                            'count': -1,
                            'units': {
                                'days': { format: ['MMM dd'] },
                                'hours': { format: ['HH:mm'] },
                            }
                        }
                    },



                    // Display a single series that shows the primary reading.
                    // Thus, this view has two columns: the date (axis) and the primary reading (line series).
                    'chartView': {
                        'columns': [0, 1]
                    },
                    // 1 day in minutes = 24 * 60 = 1440
                    'minRangeSize': 1440,
                    'snapToData':false
                }
            },
            // Initial range: HistoryFromDate to HistoryToDate.
            'state':
            {
                'range':
                  {
                      'start': new Date('<%:MonnitSession.HistoryFromDate.ToString("MM/dd/yyyy HH:mm:ss")%>'),
                      'end': new Date('<%:MonnitSession.HistoryToDate.AddDays(1).ToString("MM/dd/yyyy HH:mm:ss")%>')
                  }
            }
        });

        // This is the wrapper for the Main display graph
        var chartWrapper = new google.visualization.ChartWrapper({

            'chartType': '<%= ChartType%>',
            'containerId': 'chart_div',
            'options': {
                // Use the same chart area width as the control for axis alignment.
                'chartArea': { 'height': '80%', 'width': '90%' },
                'interpolateNulls': 'true',
                'legend': { 'position': 'top' },
                'series': { 1: { type: "scatter" } },
                'areaOpacity': 0,
                // this hard codes the date window to exactly the dates set
                'hAxis': {
                    'viewWindow': {
                        'min': new Date('<%: MonnitSession.HistoryFromDate.ToString("MM/dd/yyyy HH:mm:ss")%>'),
                        'max': new Date('<%: MonnitSession.HistoryToDate.AddDays(1).ToString("MM/dd/yyyy HH:mm:ss")%>')
                    }
                },

                'gridlines': {
                    'count': -1,
                    'units': {
                        'days': { format: ['MMM dd'] },
                        'hours': { format: ['HH:mm', 'ha'] }
                    }
                }
            }
        });

        var data = new google.visualization.DataTable();

        data.addColumn('datetime', 'Date');
        <%SensorAttribute label = SensorAttribute.LoadBySensorID(Model.Sensors.ElementAt(0).SensorID).FirstOrDefault(sa => sa.Name == "Label");
          string name = Model.Sensors.ElementAt(0).MonnitApplication.ApplicationName;
          if (label != null)
          {
              name = label.Value;
          } %>
        data.addColumn('number', '<%=name %>');
                    <%if ((Model.Sensors.ElementAt(0).MonnitApplication.IsTriggerProfile == eApplicationProfileType.Interval && Model.Sensors.ElementAt(0).ApplicationID != 14) || ViewBag.chartformat == "Interval")
                      {%>
        data.addColumn('number', 'Is Aware');
                    <%}%>

        data.addRows(eval(<%Html.RenderPartial("SensorReadingsChartRefresh");%>));

        if (data.getNumberOfRows() > 0) {
            dashboard.bind(chartRangeWrapper, chartWrapper);
            dashboard.draw(data);
        }
        else {
            $('.divChartContainer').hide();
            $('.divNoDataContainer').show();
        }

        // This listener is for when the user clicks and releases the Chart Range thumb to adjust the view of the main graph
        google.visualization.events.addListener(chartRangeWrapper, 'statechange', function (e) {
            if (!e.inProgress) {
                var state = chartRangeWrapper.getState();
                var startDate = state.range.start;
                var endDate = state.range.end;
                // format of below strings: M/D/YYYY H:m:s
                var formattedStartDateString = (startDate.getMonth() + 1) + '/' + startDate.getDate() + '/' + startDate.getFullYear() + ' ' + startDate.getHours() + ':' + startDate.getMinutes() + ":" + startDate.getSeconds();
                var formattedEndDateString = (endDate.getMonth() + 1) + '/' + endDate.getDate() + '/' + endDate.getFullYear() + ' ' + endDate.getHours() + ':' + endDate.getMinutes() + ":" + endDate.getSeconds();

                // Calls the Refresh view
                // Returns a String, 'data', of an Array of data point dates

                $.get('/sensor/SensorReadingsChartRefresh/?id=<%:Model.Sensors.ElementAt(0).SensorID%>&fromDate=' + formattedStartDateString + '&toDate=' + formattedEndDateString, function (data) {
                    var updatedData = new google.visualization.DataTable();
                    updatedData.addColumn('datetime', 'Date');
                    updatedData.addColumn('number', '<%=name %>');
                    <%if ((Model.Sensors.ElementAt(0).MonnitApplication.IsTriggerProfile == eApplicationProfileType.Interval && Model.Sensors.ElementAt(0).ApplicationID != 14) || ViewBag.chartformat == "Interval")
                      {%>
                    updatedData.addColumn('number', 'Is Aware');
                    <%}%>

                    // 'data' is an already formatted array of the data point dates
                    //  Format Ex:
                    //      "[ 
                    //          [new Date(), value, notification], 
                    //          [new Date(), value, notification] 
                    //       ]"
                    updatedData.addRows(eval(data));
                    chartWrapper.getChart().draw(updatedData, {
                        // Use the same chart area width as the control for axis alignment.
                        'chartArea': { 'height': '80%', 'width': '90%' },
                        'interpolateNulls': 'true',
                        'legend': { 'position': 'top' },
                        'series': { 1: { type: "scatter" } },
                        'hAxis': {
                            'viewWindow': {
                                'min': new Date(startDate),
                                'max': new Date(endDate)
                            }
                        },

                        'gridlines': {
                            'count': -1,
                            'units': {
                                'days': { format: ['MMM dd'] },
                                'hours': { format: ['HH:mm', 'ha'] }
                            }
                        }
                    });
                    dashboard.bind(chartRangeWrapper, chartWrapper);
                });
            }
        });
    }

    $('#print').click(function () {
        $('#fullForm').replaceWith($('#PrintArea'));
        window.print();
        $('#ExitPrint').show();
    });

    $('.helpIcon').tipTip();

    function setChartFormat() {
        var spinner = $(`<tr><td colspan=4> <div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div> </td></tr>`);

        var chartformat = $("#ChartFormat");
        var ID = chartformat.val();



        if (ID != null) {

            $.get('/Sensor/setChartFormat?id=<%:Model.Sensors.ElementAt(0).SensorID%>&ChartFormat=' + ID, function (data) {
                if (data = "Success") {

                    var tabContainter = $('.tabContainer').tabs();
                    var active = tabContainter.tabs('option', 'active');
                    tabContainter.tabs('load', active);
                }
            });
        }
    };
</script>

<div id="PrintArea" style="background-color: White;">
    <div class="formtitle">
        Sensor Readings
        <a href="#" id="print">
            <img src="../../Content/images/print.png" style="max-height: 30px; margin: -9px 5px;" /></a>
        <a href="/?sensor=<%:Model.Sensors.ElementAt(0).SensorID %>&tab=Chart" id="ExitPrint" style="display: none;">Exit Print View </a>
        <img alt="help" class="helpIcon" title="Charting data is limited to 150 data points, depending on your sensors heartbeat and the date range selected every data point may not be displayed.  For instance if your sensor has a 1 hour heartbeat the charting can only display about 6 days worth of data.  If you select a date range larger than that the chart will display only a portion of the data points.  For larger date ranges you can export the data (2500 data points) and chart in external tool." src="/Content/images/help.png" />

        <% if (Model.Sensors.ElementAt(0).MonnitApplication.IsTriggerProfile == eApplicationProfileType.Trigger)
           {%>
        <span style="margin-left: 15px;">Chart Format</span>
        <select id="ChartFormat" onchange="setChartFormat(this);" name="ChartFormat" style="width: 150px; height: 27px;">
            <option value="Trigger" <%: ViewBag.chartformat == "Trigger"? "selected":"" %>>Daily Summary </option>
            <option value="Interval" <%: ViewBag.chartformat == "Interval"? "selected":"" %>>All Data Points </option>            
        </select>
        <%} %>

        <%Html.RenderPartial("HistoryDatePicker");%>
    </div>

    <div id="dashboard" class="formBody divChartContainer">
        <h3 style="text-align: center"><%= System.Net.WebUtility.HtmlDecode(Model.Sensors[0].SensorName)%></h3>
        <div id="chart_div" style="height: 320px; width: 860px; margin-bottom: 20px; margin-top: 20px;"></div>
        <div id="control_div" style="height: 50px; width: 100%; margin-bottom: 20px; margin-top: 20px;"></div>
    </div>
    <div class="formBody divNoDataContainer" style="display: none;">
        No data available for time period.
    </div>

    <div class="buttons">
        <div class="clear"></div>
    </div>
</div>

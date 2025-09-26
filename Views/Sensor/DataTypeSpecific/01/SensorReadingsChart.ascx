<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();

%>
<%
    DateTime From = MonnitSession.HistoryFromDate.Date;
    DateTime To = MonnitSession.HistoryToDate.Date.AddDays(1);
    StringBuilder PrimarySeriesVars = new StringBuilder();
    StringBuilder ZoomSeriesVars = new StringBuilder();
    StringBuilder PrimarySeriesLabels = new StringBuilder();
    StringBuilder ZoomSeriesLabels = new StringBuilder();
    bool IncludeSeries1 = false;

    List<DataPoint> HumidityPlotVals;
    List<DataPoint> TemperaturePlotVals;
    //Interval Sensors
    List<DataMessage> list = DataMessage.LoadForChart(
        Model.SensorID,
        Monnit.TimeZone.GetUTCFromLocalById(From, MonnitSession.CurrentCustomer.Account.TimeZoneID),
        Monnit.TimeZone.GetUTCFromLocalById(To, MonnitSession.CurrentCustomer.Account.TimeZoneID),
        70);

    HumidityPlotVals = (from dm in list
                select new DataPoint() { Date = dm.MessageDate, Value = dm.AppBase.PlotValue, SentNotification = dm.MeetsNotificationRequirement }).ToList();
    TemperaturePlotVals = (from dm in list
                select new DataPoint() { Date = dm.MessageDate, Value = dm.AppBase is Humidity ? ((Humidity)dm.AppBase).PlotTemperatureValue : ((HumiditySHT25)dm.AppBase).PlotTemperatureValue, SentNotification = false }).ToList();

    if (list.Count > 0)
    {
        IncludeSeries1 = true;
        bool IncludeSeries2 = false;
        StringBuilder Series = new StringBuilder("var Ser" + Model.SensorID + "A = [");
        StringBuilder Series2 = new StringBuilder("var Ser" + Model.SensorID + "B = [");
        StringBuilder Series3 = new StringBuilder("var Ser" + Model.SensorID + "C = [");
        foreach (DataPoint val in HumidityPlotVals)
        {
            Series.AppendFormat("['{0}', {1}],", MonnitSession.MakeLocal(val.Date).ToString("yyyy-MM-dd HH:mm:ss"), val.Value);
            if (val.SentNotification)
            {
                Series2.AppendFormat("['{0}', {1}],", MonnitSession.MakeLocal(val.Date).ToString("yyyy-MM-dd HH:mm:ss"), val.Value);
                IncludeSeries2 = true;
            } 
        }
        foreach (DataPoint val in TemperaturePlotVals)
        {
            Series3.AppendFormat("['{0}', {1}],", MonnitSession.MakeLocal(val.Date).ToString("yyyy-MM-dd HH:mm:ss"), val.Value);
        }
        if (Series[Series.Length - 1] == ',')
            Series.Remove(Series.Length - 1, 1);
        Series.Append("];");
        if (Series2[Series2.Length - 1] == ',')
            Series2.Remove(Series2.Length - 1, 1);
        Series2.Append("];");
        if (Series3[Series3.Length - 1] == ',')
            Series3.Remove(Series3.Length - 1, 1);
        Series3.Append("];");

        Response.Write("<script type='text/javascript' language='javascript'>\r\n");
        Response.Write(Series);
        if (IncludeSeries2)
            Response.Write(Series2);
        Response.Write(Series3);
        Response.Write("\r\n</script>\r\n");

        if (IncludeSeries2)
            PrimarySeriesVars.AppendFormat("Ser{0}A, Ser{0}C, Ser{0}B,", Model.SensorID);
        else
            PrimarySeriesVars.AppendFormat("Ser{0}A, Ser{0}C,", Model.SensorID);
        ZoomSeriesVars.AppendFormat("Ser{0}A,", Model.SensorID);

        if (IncludeSeries2)
            PrimarySeriesLabels.AppendFormat("{{ label: 'Humidity'}},{{ label: 'Temperature'}},{{ label: 'Is Aware', showLine: false, color: 'red', showLabel: false }},", Model.SensorName.Replace("'", "").Replace("\"", ""));
        else
            PrimarySeriesLabels.AppendFormat("{{ label: 'Humidity'}},{{ label: 'Temperature'}},", Model.SensorName.Replace("'", "").Replace("\"", ""));
        ZoomSeriesLabels.AppendFormat("{{ label: '{0}' }},", Model.SensorName.Replace("'", "").Replace("\"", ""));
        
    }
    if (PrimarySeriesVars.Length > 0) PrimarySeriesVars.Remove(PrimarySeriesVars.Length - 1, 1);
    if (ZoomSeriesVars.Length > 0) ZoomSeriesVars.Remove(ZoomSeriesVars.Length - 1, 1);
    if (PrimarySeriesLabels.Length > 0) PrimarySeriesLabels.Remove(PrimarySeriesLabels.Length - 1, 1);
    if (ZoomSeriesLabels.Length > 0) ZoomSeriesLabels.Remove(ZoomSeriesLabels.Length - 1, 1);

    if (IncludeSeries1)
    {
%>
    
	<script type="text/javascript" language="javascript">
	    $(document).ready(function() {
	        $.jqplot.config.enablePlugins = true;

	        //Format Options = http://www.jqplot.com/docs/files/plugins/jqplot-dateAxisRenderer-js.html
	        targetPlot = $.jqplot('chart1', [<%:PrimarySeriesVars.ToString() %>], {
	            seriesDefaults: 
	                { 
	                    neighborThreshold: 0, 
                        markerOptions:
                        {
                            style: 'filledSquare',
                            size: 6
                        }
                    },
	            axes: {
	                xaxis: {
	                    renderer: $.jqplot.DateAxisRenderer,
	                    tickOptions: { formatString: '<center>%m/%d/%Y</br>%I:%M %p</center>' }, 
	                    autoscale: true
	                },
	                yaxis: {
//	                    label: 'Degree',
	                    autoscale: true
	                }
	            },
	            series: [
                    <%:PrimarySeriesLabels.ToString() %>
                ],
	            highlighter: {
	                sizeAdjust: 6,
	                tooltipAxes: 'y',
	                useAxesFormatters: false,
	                tooltipFormatString: '%.5P'
	            },
                cursor: {
	                zoom: true,
	                showTooltip: false
	            },
	            legend: {
	                show: true,
                    location: 'ne',
	                xoffset: 10,
	                yoffset: 10
	            },
	            axesDefaults: { tickOptions: { formatString: "%d" }, autoscale: false, useSeriesColor: false }

	        });

	        controllerPlot = $.jqplot('chart2', [<%:ZoomSeriesVars.ToString() %>], {
	            seriesDefaults: { neighborThreshold: 0, showMarker: false },
	            axes: { 
	                xaxis: { 
	                    renderer: $.jqplot.DateAxisRenderer, 
	                    tickOptions: { formatString: '%m/%d/%Y'},
	                    min:'<%:From.ToString("yyyy-MM-dd HH:mm:ss") %>',
	                    max:'<%:To.ToString("yyyy-MM-dd HH:mm:ss") %>',
	                } 
	            },
	            series: [
                  <%:ZoomSeriesLabels.ToString() %>
                ],
	            highlighter: {
	                show: false
	            },
                cursor: {
	                showTooltip: false,
	                zoom: true,
	                constrainZoomTo: 'x'
	            },
	            axesDefaults: { tickOptions: { formatString: "%d" }, autoscale: false, useSeriesColor: false }
	        });

	        $.jqplot.Cursor.zoomProxy(targetPlot, controllerPlot);
	        
	        
	        $('#print').click(function () {
	            $('#fullForm').replaceWith($('#PrintArea'));
	            window.print();
	            $('#ExitPrint').show();
	            /*if ($.browser.msie) {
	                $('#PrintArea').printElement({
	                    //                    leaveOpen:true,
	                    //                    printMode:'popup',
	                    pageTitle: 'Chart.html'
	                });
	            }
	            else {
	                chartToImage("chart1");
	                chartToImage("chart2");
	                $('#PrintArea').printElement({
	                    //                    leaveOpen:true,
	                    //                    printMode:'popup',
	                    pageTitle: 'Chart.html'
	                });
	                imageToChart("chart1");
	                imageToChart("chart2");
	            }*/
	        });
	    });
	    
	    function chartToImage(canvasContainerID)
    {
        var newCanvas = document.createElement("canvas");
        newCanvas.width = $("#" + canvasContainerID).width();
        newCanvas.height = $("#" + canvasContainerID).height();
        
        $("#" + canvasContainerID + " > canvas").each(function () {
            var position = $(this).position();
            newCanvas.getContext("2d").drawImage(this, position.left, position.top);
        });
        
        $("#" + canvasContainerID + " > canvas").hide();
        $("#" + canvasContainerID).append("<img id='Img" + canvasContainerID + "' src='" + newCanvas.toDataURL() + "' style='margin-top:-8px;'/>");
            
    }
    
    function imageToChart(canvasContainerID)
    {
        $("#" + canvasContainerID + " > canvas").show();
        $("#" + canvasContainerID + " > img").remove();
    }
    
</script>
<%} %>

<div id="PrintArea" style="background-color:White;">
    <div class="formtitle">
        Sensor Readings
        <%if (IncludeSeries1) {%>
        <a href="#" id="print">
            <img src="../../Content/images/print.png" style="max-height:30px; margin: -9px 5px;" />
        </a>
        <a href="/?sensor=<%:Model.SensorID %>&tab=Chart" id="ExitPrint" style="display:none;" >
            Exit Print View
        </a>
        <%} %>
        <%Html.RenderPartial("HistoryDatePicker"); %>
    </div>
    <%if (IncludeSeries1)
      {
    %>
    <div class="formBody">
        <div class="jqPlot" id="chart1" style="height:320px; width:100%; margin-bottom: 20px; margin-top:20px;"></div>
    </div>
    
    <div class="formtitle">Zoom</div>
    <div class="formBody">
        <div class="jqPlot" id="chart2" style="height:100px; width:100%;"></div>
    </div>
    
    <div class="buttons">
        <button value="reset" onclick="controllerPlot.resetZoom();" class="bluebutton">Reset Zoom</button>
        <button value="Legend" id="legends" class="greybutton" style="font-size: 14px;">Legend</button>
        <div class="clear">
    </div>
    <%}
      else
      { %>
        <div class="formBody">
             No data available for time period.
        </div>
    <%} %>
</div>
<script type="text/javascript" language="javascript">
    $(function () {
        
    });
</script>
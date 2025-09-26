<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<VisualMapSensor>>" %>

<div id="filteredSensors" class="hasScroll-sm d-flex " style="flex-wrap: wrap;">
    <%
        foreach (VisualMapSensor item in Model)
        {
            string badgeValue = item.Sensor.ApplicationID == 22 ? Monnit.ZeroToTwentyMilliamp.ScaleBadgeText(item.Sensor.SensorID) : "";
    %>

    <a class="d-flex" onclick="toggleMapSensorStatic(<%:item.Sensor.SensorID%>,<%:item.Sensor.ApplicationID %>, `<%:Html.GetThemedSVG("app" + item.Sensor.ApplicationID).ToString()%>`)">

        <div class="card-w-data" style="min-height: 79px;">
            <div class="icon-color iconMap" style="width: 30px; margin-left: 5px; margin-right: 10px;">
                <%=Html.GetThemedSVG("app" + item.Sensor.ApplicationID) %>
            </div>
            <div class=" check-card-name2">
                <%:System.Web.HttpUtility.HtmlDecode(item.Sensor.SensorName) %>
            </div>
            <div id="sensor_<%:item.Sensor.SensorID%>" class="check-card-check ListBorder<%:item.Selected ? "Active":"NotActive" %>">
                <%=Html.GetThemedSVG("circle-check") %>
            </div>
        </div>
    </a>
    <%} %>
</div>
<script type="text/javascript">

    $(document).ready(function () {
        $('.filteredSensorsCount').html('<%:ViewBag.FilteredSensorsCount%>');
        $('.totalSensorsCount').html('<%:ViewBag.TotalSensorsCount%>');
        $('.selectedSensorsCount').html('<%:ViewBag.SelectedSensorsCount%>');
    });

    function toggleMapSensorStatic(id, appId, image) {
        var sensorID = id;
        var sensorTypeID = appId;
        //var image = '/Content/images/Wit/app' + sensorTypeID + '.png';
        var selectedToggle = $('#sensor_' + sensorID);
        var add = selectedToggle.hasClass('ListBorderNotActive');
        if (add) {
            selectedToggle.removeClass('ListBorderNotActive').addClass('ListBorderActive');
            $('.selectedSensorsCount').html(parseInt($('.selectedSensorsCount').html()) + 1);
            // invoke the SeaDragon!
            startPlacing(sensorID, image, "sensor");

        } else {
            selectedToggle.removeClass('ListBorderActive').addClass('ListBorderNotActive');
            $('.selectedSensorsCount').html(parseInt($('.selectedSensorsCount').html()) - 1);
            // invoke the SeaDragon!
            remove(sensorID, "sensor");
        }
    }

</script>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.MultiChartSensorDataModel>" %>

<%int sensorCount = (int)ViewBag.sensorCount;  %>
<div class="row chart_row">
    <div class="col-12 col-lg-3 chart_row__details" style="text-align: center;">
        <%=Model.GroupSensor.Sensor.SensorName %><br />
        <%=HttpUtility.UrlDecode(Model.GroupSensor.Alias) %>
    </div>
    <div class="col-12 col-lg-9" >
        <div  id="sensorChartDiv_<%=Model.GroupSensor.SensorID%>_<%=Model.GroupSensor.DatumIndex%>" style="height: <%=sensorCount > 5 ? "100px" :"200px" %>; width: 100%">Chart for this data type not available</div>
    </div>
</div>


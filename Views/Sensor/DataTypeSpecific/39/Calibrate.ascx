<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
%>
<div>
    <div class="formtitle">
        Calibrate Sensor
    </div>
    <form action="/Sensor/Calibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
    <input type="hidden" value="/Sensor/Calibrate/<%:Model.SensorID %>" name="returns" id="returns" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%  if (Model.CanUpdate)
        {%>

    <div>
        Place the sensor in the location that it is going to stay at and while there are no cars present press the calibrate button. 
    </div>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveCalibrationButtons.ascx", Model);%>
</form>


    <%}
        else
        {
    %>
    <div>
        Calibration for this sensor is not available for edit until pending transaction
                is complete.
    </div>
    <%
        }
      %>

    


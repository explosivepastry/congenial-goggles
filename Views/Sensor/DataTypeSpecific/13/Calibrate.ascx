<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
%>

    <div class="formtitle">
        Calibrate Sensor
    </div>



    <%  if (Model.CanUpdate)
        {%><form action="/Sensor/Calibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
            <%: Html.ValidationSummary(false)%>
            <%: Html.Hidden("id", Model.SensorID)%>
            <input type="hidden" value="/Sensor/Calibrate/<%:Model.SensorID %>" name="returns" id="returns" />
   
            <div>
                Orient the sensor so that the label is facing up and the securing tabs are flat
                        on a table. When this button is available again the calibration cycle is complete.
            </div>
          <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveCalibrationButtons.ascx", Model);%>
        </form>


<%}
        else
        {
%>
<div class="formBody" style="font-weight:bold">
    Calibration for this sensor is not available for edit until pending transaction
                is complete.
</div>
<div class="buttons">&nbsp; </div>
<%
        }
%>






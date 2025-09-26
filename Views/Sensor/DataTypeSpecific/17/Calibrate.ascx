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

<%if (string.IsNullOrEmpty(Model.CalibrationCertification))
  {%>


<%  if (Model.CanUpdate)
    {%>
<form action="/Sensor/Calibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
    <input type="hidden" value="/Sensor/Calibrate/<%:Model.SensorID %>" name="returns" id="returns" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <div class="formBody">
        <div>
            Calibrate temperature by entering the current known temperature.<br />
            Calibrate 0 PPM by placing sensor in 0 PPM environment.<br />
            Calibrate upper span by entering the current known CO PPM concentration.
        </div>
        <div>
            <select name="CalibrationType" id="CalibrationType">
                <option value="Temperature">Temperature</option>
                <option value="0_PPM">0 PPM</option>
                <option value="Span_PPM">Upper Span</option>
            </select>
        </div>
        <div class="calibrationValue Temperature Span_PPM">
            <input name="CalibrationValue" /><span class="calibrationValue Temperature"> &#176; C</span><span class="calibrationValue Span_PPM"> PPM</span>
        </div>
    </div>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveCalibrationButtons.ascx", Model);%>
</form>

<script type="text/javascript">
    $(document).ready(function () {
        $('#CalibrationType').change(function () {
            $('.calibrationValue').hide();
            $('.' + $(this).val()).show();
        });
        $('.calibrationValue').hide();
        $('.' + $('#CalibrationType').val()).show();
    });

</script>

<%}
    else
    {
%>
<div class="formBody" style="font-weight: bold">
    Calibration for this sensor is not available for edit until pending transaction
                is complete.
</div>
<div class="buttons">&nbsp; </div>
<%
        }
%>



<%}
  else
  {%>

<div class="formBody">
    <div>
        This sensor has been pre-calibrated in a certified gas chamber.
    </div>
    <br />
    <div>
        If you have a certified gas chamber and would like to perform additional calibrations please contact support.
    </div>
</div>
<div class="buttons">&nbsp; </div>
<%}%>
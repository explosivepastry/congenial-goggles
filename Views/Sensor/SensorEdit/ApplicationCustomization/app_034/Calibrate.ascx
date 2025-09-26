<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();
 %>

<%if (!string.IsNullOrEmpty(ViewBag.ErrorMessage))
  { %>
<div class="formBody" style="color: red;"><%:ViewBag.ErrorMessage %></div>
<%} %>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
  {%>

<%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
  { %>

<% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

<%} %>
<%  if (Model.CanUpdate)
    {%>
<form action="/Sensor/Calibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
    <input type="hidden" value="/Sensor/Calibrate/<%:Model.SensorID %>" name="returns" id="returns" />

    <%: Html.Hidden("id", Model.SensorID)%>
    <div class="form-group">
        <div class="col-md-9 col-sm-9 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_034|Calibrate temperature by entering the current known temperature.","Calibrate temperature by entering the current known temperature.")%><br />
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_034|Calibrate 0 PPM by placing sensor in 0 PPM environment.","Calibrate 0 PPM by placing sensor in 0 PPM environment.")%><br />
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_034|Calibrate upper span by entering the current known CO PPM concentration.","Calibrate upper span by entering the current known CO PPM concentration.")%>
        </div>
    </div>
    <div class="clear"></div>
    <br />
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
        </div>
        <div class="col sensorEditFormInput">
            <div class="calibrationValue Temperature Span_PPM d-flex align-items-center">
                <input name="CalibrationValue" class="form-control" /><span class="calibrationValue Temperature"> &#176; C</span><span class="calibrationValue Span_PPM"> PPM</span>
            </div>
        </div>
        <br />
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("","")%>
        </div>
        <div class="col sensorEditFormInput">
            <select name="CalibrationType" id="CalibrationType" class="form-select">
                <option value="Temperature"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_034|Temperature","Temperature")%></option>
                <option value="0_PPM"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_034|0 PPM","0 PPM")%></option>
                <option value="Span_PPM"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_034|Upper Span","Upper Span")%></option>
            </select>
        </div>
        <br />
    </div>
    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
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

<%}%>
<% else
    {%>
<div class="formBody" style="font-weight: bold">
    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Calibration for this sensor is not available for edit until pending transaction is complete.","Calibration for this sensor is not available for edit until pending transaction is complete.")%>
</div>
<div class="buttons">&nbsp; </div>
<%}%>



<%}
  else
  {%>

<div class="formBody">
    <div>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_034|This sensor has been pre-calibrated in a certified gas chamber.","This sensor has been pre-calibrated in a certified gas chamber.")%>
    </div>
    <br />
    <div>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_034|If you have a certified gas chamber and would like to perform additional calibrations please contact support.","If you have a certified gas chamber and would like to perform additional calibrations please contact support.")%>
    </div>
</div>
<div class="buttons">&nbsp; </div>
<%}%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<%if(TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    { %>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="returns" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>

    <div class="form-group">
        <div class="bold col-md-9 col-sm-9 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_026|1 inch Calibration level must be at least 1 inch or higher on tape to calibrate.","1 inch Calibration level must be at least 1 inch or higher on tape to calibrate.")%><br />
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_026|8 Inch Calibration  level must be at least .5 inches or more lower than top of readings on tape to calibrate.","8 Inch Calibration  level must be at least .5 inches or more lower than top of readings on tape to calibrate.")%>
            <hr />
        </div>
    </div>
    <div class="clear"></div>
    <br />

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_026|Calibrate Sensor to ","Calibrate Sensor to ")%>
        </div>
        <div class="col sensorEditFormInput">
            <select name="calibrationStep" class="form-select">
                <option value="Empty">1 inch Calibration</option>
                <option value="Full">8 inch Calibration</option>
            </select>
        </div>
    </div>
    <div class="clear"></div>
    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<script>
    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
    //MobiScroll
    //$(function () {
    //    $('#calibrationStep').mobiscroll().select({
    //        theme: 'ios',
    //        display: popLocation,
    //        minWidth: 200
    //    });
    //});

</script>
<%}%>
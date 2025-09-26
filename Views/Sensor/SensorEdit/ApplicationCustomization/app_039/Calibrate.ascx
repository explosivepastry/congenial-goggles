<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="returns" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>

    <% if(TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
        {%>

    <div class="form-inline">
        <div class="form-group">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_039|Place the sensor in the location that it is going to stay at and while there are no cars present then press the calibrate button.","Place the sensor in the location that it is going to stay at and while there are no cars present then press the calibrate button.")%>
        </div>

    </div>
    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>
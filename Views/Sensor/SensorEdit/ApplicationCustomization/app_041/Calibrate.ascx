<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>

<% if (new Version(Model.FirmwareVersion) <= new Version("2.2.0.0"))
    {
        TempData["CanCalibrate"] = false;%>

    <div class="form-group">
        <div class="bold col-sm-3 col-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_041|Maximum Pressure on Gauge","Maximum Pressure on Gauge")%>
        </div>
        <div class="col-sm-9 col-12 mdBox">
            <input name="maxPressure" id="maxPressure" value="<%: Model.Calibration4 / 10%>" />
            PSI
        </div>
    </div>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
  {%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
      { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>

    <%if(TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
        {%>
        
    <script>

        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        //MobiScroll
        //$(function () {
        //    $('#Cal_Which').mobiscroll().select({
        //        theme: 'ios',
        //        display: popLocation,
        //        minWidth: 200
        //    });
        //});
    </script>


    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_041|Pressure Range","Pressure Range")%>
        </div>
        <div class="col sensorEditFormInput">
            <select name="Cal_Which" id="Cal_Which" class="form-select">
                <option value="high"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_041|Current Operating Pressure (Higher pressures result in more precise calibrations)","Current Operating Pressure (Higher pressures result in more precise calibrations)")%></option>
                <option value="low"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_041|No pressure (reset 0 point)","No pressure (reset 0 point)")%></option>
            </select>
        </div>
    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_041|Pressure (displayed on dial)","Pressure (displayed on dial)")%>
        </div>
        <div class="col sensorEditFormInput">
            <input name="calPressure" id="calPressure" class="form-control" value="" />
            PSI
        </div>
    </div>

    <%} %>

<%
        }
%>

    <div class="clear"></div>
    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>
     
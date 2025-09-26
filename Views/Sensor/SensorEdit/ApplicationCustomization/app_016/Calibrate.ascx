<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
  {%>

 <%  if (TempData["CanCalibrate"].ToBool())
        {
            byte[] bytes = BitConverter.GetBytes(Model.Calibration2);
            string Thresh16 = ((Convert.ToDouble(bytes[1]) - 128) * 0.063).ToString();
    %>

    <form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" class="form-control" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
      { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_016|G-Force to trigger","G-Force to trigger")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" name="gForce" id="gForce" value="<%:  (Thresh16) %>" />
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_016|Calibrate sensor for","Calibrate sensor for")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-check-input me-1" type="radio" name="performance" id="performancePower" value="p" <%:Model.Calibration1 == Convert.ToInt64(0x00120301) ? "checked='checked'" : "" %> /><label for="p"> <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_016|High Performance","High Performance")%></label>
            <input class="form-check-input mx-1" type="radio" name="performance" id="performanceBattery" value="b" <%:Model.Calibration1 != Convert.ToInt64(0x00120301) ? "checked='checked'" : "" %> /><label for="b"> <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_016|Low Power","Low Power")%></label>
        </div>
    </div>


    <div class="clear"></div>
    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>

<%}
  if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil >= MonnitSession.MakeLocal(DateTime.UtcNow))
  {%>
<div class="formBody">
    <div>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|This sensor has been pre-calibrated and certified by","This sensor has been pre-calibrated and certified by")%> <%: CalibrationFacility.Load(Model.CalibrationFacilityID).Name %>.
    </div>
    <br />

    <div>
        <a target="_blank" href="http://74.93.64.170/iportal/iportal_documents/certs/<%: new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "") %>.pdf"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|View Calibration Certificate","View Calibration Certificate")%></a>
    </div>
</div>
<%}%>
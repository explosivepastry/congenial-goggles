<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
    
<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    TempData["CanCalibrate"] = true;

	//Profile Specific Validation that prevents calibration
	if (Model.GenerationType.Contains("Gen2"))
	{
		TempData["CanCalibrate"] = false;%>

        <div class="form-inline">
            <div class="form-group">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_0015|Calibration for this sensor is not available.", "Calibration for this sensor is not available.")%>
            </div>
        </div>
<%  }
	else
	{
		Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
		Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);
	} 

    //if Validation Passed, allow calibration.
    if(TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    { %>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" class="form-control" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />
    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>

    <div class="form-inline">
        <div class="form-group">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_0015|Orient the sensor so that the label is facing up and the securing tabs are flaton a table. When this button is available again the calibration cycle is complete.","Orient the sensor so that the label is facing up and the securing tabs are flat on a table. When this button is available again the calibration cycle is complete.")%>
        </div>
    </div>
    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>
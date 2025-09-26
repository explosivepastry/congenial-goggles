<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

	TempData["CanCalibrate"] = true;

	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
	{%>

<%if (TempData["CanCalibrate"].ToBool())
	{ %>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form2" method="post">
	<input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden2" />

	<%: Html.ValidationSummary(false)%>
	<%: Html.Hidden("id", Model.SensorID)%>
	<%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
		{ %>

	<% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

	<%} %>

	<div class="form-inline">
		<div class="form-group">
			<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_028|First face North Arrow on sensor North and press Calibrate North button.", "First face North Arrow on sensor North and press Calibrate North button.")%><br />
			<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_028|Wait for Sensor to check in. After North has been calibrated face the North Arrow", "Wait for Sensor to check in. After North has been calibrated face the North Arrow")%><br />
			<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_028|East and then press Calibrate East button. wait for sensor to check in.", "East and then press Calibrate East button. wait for sensor to check in.")%><br />
			<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_028|Your sensor will now be calibrated for proper compass reading.", "Your sensor will now be calibrated for proper compass reading.")%>
		</div>
	</div>
	<div class="clear"></div>
	<br />
	<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<% %>
<%}
	else if (CalibrationCertificationValidUntil >= MonnitSession.MakeLocal(DateTime.UtcNow))
	{%>

<div class="formBody">
	<div>
		<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|This sensor has been pre-calibrated and certified by", "This sensor has been pre-calibrated and certified by")%> <%: CalibrationFacility.Load(Model.CalibrationFacilityID).Name %>.
	</div>
	<br />

	<div>
		<a target="_blank" href="http://74.93.64.170/iportal/iportal_documents/certs/<%: new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "") %>.pdf"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|View Calibration Certificate", "View Calibration Certificate")%></a>
	</div>
</div>
<%}
	}%>

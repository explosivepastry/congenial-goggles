<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

	TempData["CanCalibrate"] = true;

	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

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
	<%  if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
		{
			if (Model.LastDataMessage == null)
			{%>
	<div class="formBody" style="font-weight: bold">
		<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Calibration for this sensor is not available until it has checked in at least one time.", "Calibration for this sensor is not available until it has checked in at least one time.")%>
	</div>
	<div class="buttons">&nbsp; </div>

	<% }
		else
		{
			string tempVal = HandheldFoodProbe.Deserialize(Model.FirmwareVersion.ToString(), Model.LastDataMessage.Data.ToString()).Temp.ToString();

			if (Monnit.HandheldFoodProbe.IsFahrenheit(Model.SensorID))
				tempVal = tempVal.ToDouble().ToFahrenheit().ToString("0.00");
	%>


	<div class="">
		<h2><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Temperature", "Temperature")%></h2>
		<div class="form-group">
			<div class="bold col-md-3 col-sm-3 col-xs-12">
				<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Observed Sensor Reading: (From sensor)", "Observed Sensor Reading: (From sensor)")%>
			</div>
			<div class="col-md-9 col-sm-9 col-xs-12 mdBox">
				<input name="observed" id="observed" readonly="readonly" value="<%: Model.LastDataMessage != null ? tempVal.ToDouble().ToString() : "" %>" />
				<%:Monnit.HandheldFoodProbe.IsFahrenheit(Model.SensorID) ? "F" : "C" %>
			</div>
		</div>
		<div style="clear: both;"></div>
		<br />
		<div class="form-group">
			<div class="bold col-md-3 col-sm-3 col-xs-12">
				<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Actual reading is", "Actual reading is")%>
			</div>
			<div class="col-md-9 col-sm-9 col-xs-12 mdBox">
				<input name="actual" id="actual" value="<%: Model.LastDataMessage != null ? tempVal.ToDouble().ToString() : "" %>" required />
				<%:Monnit.HandheldFoodProbe.IsFahrenheit(Model.SensorID) ? "F" : "C" %>
			</div>
		</div>
		<div style="clear: both;"></div>
		<div class=" mdBox">
			<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_086|degrees", "degrees")%>
			<select name="tempScale" id="scaleType" class="tzSelect">
				<option value="F" <%= HandheldFoodProbe.IsFahrenheit(Model.SensorID) ? "selected" : "" %>><%: Html.TranslateTag("Fahrenheit", "Fahrenheit")%></option>
				<option value="C" <%= !HandheldFoodProbe.IsFahrenheit(Model.SensorID) ? "selected" : "" %>><%: Html.TranslateTag("Celsius", "Celsius")%></option>
			</select>
		</div>
	</div>

	<script>
		$(document).ready(function () {

			$('#actual').addClass('editField editFieldMedium');

			$("#scaleType").change(function () {
				if ($("#scaleType").val() == "F") {
					$("#actual").val(parseFloat(parseFloat($("#actual").val()) / 1.8))

				}
				if ($("#scaleType").val() == "C") {
					$("#actual").val(parseFloat(parseFloat($("#actual").val()) * 1.8))
				}
			});

		});
	</script>

	<br />
	<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}
	}%>
<%
	else if (CalibrationCertificationValidUntil >= MonnitSession.MakeLocal(DateTime.UtcNow))
	{%>
<div class="formBody">
	<div>
		<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|This sensor has been pre-calibrated and certified by", "This sensor has been pre-calibrated and certified by")%> <%: CalibrationFacility.Load(Model.CalibrationFacilityID).Name %>.
	</div>
	<br />

	<div>
		<a target="_blank" href="http://74.93.64.170/iportal/iportal_documents/certs/<%= new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "") %>.pdf"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|View Calibration Certificate", "View Calibration Certificate")%></a>
	</div>
</div>
<%}	
	}%>
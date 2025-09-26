<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

	bool isF = Monnit.SootBlower.IsFahrenheit(Model.SensorID);
	double DefaultMin = -40;
	double DefaultMax = 125;

	if (isF)
	{
		DefaultMin = DefaultMin.ToFahrenheit();

		DefaultMax = DefaultMax.ToFahrenheit();
	}
%>

<%
	TempData["CanCalibrate"] = true;

	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
	{%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
	<input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="returns" />

	<%: Html.ValidationSummary(false)%>
	<%: Html.Hidden("id", Model.SensorID)%>
	<%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
	  { %>

	<% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

	<%} %>

	<%
		if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
		{
			if (Model.LastDataMessage != null)
			{

				string tempVal = SootBlower.Deserialize(Model.FirmwareVersion.ToString(), Model.LastDataMessage.Data.ToString()).Temperature.ToString();



				if (isF)
					tempVal = tempVal.ToDouble().ToFahrenheit().ToString("0.00");
	%>



	<h2><%: Html.TranslateTag("Temperature", "Temperature")%></h2>
	<div class="form-group">
		<div class="bold col-md-3 col-sm-3 col-xs-12">
			<%: Html.TranslateTag("Observed Reading: (From sensor)", "Observed Reading: (From sensor)")%> (<%: Html.Label(isF ? "°F" : "°C") %>)
		</div>
		<div class="col-md-9 col-sm-9 col-xs-12 mdBox">
			<input class="aSettings__input_input" type="number" name="observedTemp" id="observedTemp" readonly="readonly" value="<%: Model.LastDataMessage != null ? tempVal.ToDouble().ToString() : "" %>" />
		</div>
	</div>
	<div style="clear: both;"></div>
	<br />
	<div class="form-group">
		<div class="bold col-md-3 col-sm-3 col-xs-12">
			<%: Html.TranslateTag("Actual Reading:", "Actual Reading:")%> (<%: Html.Label(isF ? "°F" : "°C") %>)
		</div>
		<div class="col-md-9 col-sm-9 col-xs-12 mdBox">
			<input class="aSettings__input_input" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="actualTemp" id="actualTemp" value="<%: tempVal.ToDouble().ToString()%>" required />


			<a id="actualTempNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>


		</div>
	</div>
	<div style="clear: both;"></div>
	<br />

	<script>
		var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';


		$(function () {
			var MinThresMinVal = <%=DefaultMin%>;
			var MinThresMaxVal = <%=DefaultMax%>;
			var DefaultTempVal = <%=tempVal%>;

        <% if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
		{ %>

			$('#actualTempNum').mobiscroll().number({
				theme: 'ios',
				circular: true,
				display: popLocation,

				allowLeadingZero: true,
				minWidth: 120,
				step: .1,
				min: MinThresMinVal,
				max: MinThresMaxVal,
				defaultValue: $('#actualTemp').val(),
				onSet: function (event, inst) {
					var minThres = parseFloat(inst.getVal());
					$('#actualTemp').val(minThres);
					$('#actualTemp').change();
				},
				onShow: function (event, inst) {
					inst.setVal($('#actualTemp').val());
				},

			});

         <%}%>

			$("#actualTempc").change(function () {
				if (isANumber($("#actualTemp").val())) {
					if ($("#actualTemp").val() < MinThresMinVal)
						$("#actualTemp").val(MinThresMinVal);
					if ($("#actualTemp").val() > MinThresMaxVal)

						$("#actualTemp").change();

				} else {

					$("#actualTemp").val(DefaultTempVal);
				}
			});

		});

	</script>

	<br />
	<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>

<%
	}
%>

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
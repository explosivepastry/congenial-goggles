<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
	bool isF = Temperature.IsFahrenheit(Model.SensorID);
	if (!Monnit.VersionHelper.IsVersion_1_0(Model))
	{
		string Min = "";
		string Max = "";
		long DefaultMin = 0;
		long DefaultMax = 0;

		MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
		MonnitApplicationBase.DefaultThresholds(Model, out DefaultMin, out DefaultMax);

		DefaultMin = DefaultMin / 10;
		DefaultMax = DefaultMax / 10;
		if (isF)
		{
			DefaultMin = DefaultMin.ToDouble().ToFahrenheit().ToLong();
			DefaultMax = DefaultMax.ToDouble().ToFahrenheit().ToLong();
		}
%>

<div class="row sensorEditForm">
	<div class="col-12 col-md-3">
		<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Above","Above")%>&nbsp;<span id="aboveType">(<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)</span>
	</div>
	<div class="col sensorEditFormInput">
		<input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
		<a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
		<%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
	</div>
</div>

<script>

	//MobiScroll
	$(function () {
		var MaxThresMinVal = <%=DefaultMin%>;
		var MaxThresMaxVal = <%=DefaultMax%>;

        <% if (Model.CanUpdate)
		   { %>
				<% if (isF)
				{ %>
		MaxThresMobiscroll(MaxThresMinVal, MaxThresMaxVal, 'Maximum Threshold °F');
			  <%}
				else
				{ %>
		MaxThresMobiscroll(MaxThresMinVal, MaxThresMaxVal, 'Maximum Threshold °C');
			  <%} %>


		$("#MaximumThreshold_Manual").change(function () {
			if (isANumber($("#MaximumThreshold_Manual").val())) {
				if ($("#MaximumThreshold_Manual").val() < MaxThresMinVal)
					$("#MaximumThreshold_Manual").val(MaxThresMinVal);
				if ($("#MaximumThreshold_Manual").val() > MaxThresMaxVal)
					$("#MaximumThreshold_Manual").val(MaxThresMaxVal);

				if (parseFloat($("#MaximumThreshold_Manual").val()) < parseFloat($("#MinimumThreshold_Manual").val()))
					$("#MaximumThreshold_Manual").val(parseFloat($("#MinimumThreshold_Manual").val()));

				$('#minThreshNum').mobiscroll('option', {
					max: $('#MaximumThreshold_Manual').val()
				});

			} else {

				$("#MaximumThreshold_Manual").val(<%: Max%>);
			}
		});
        <%}%>

		function MaxThresMobiscroll(min, max, headertext) {

            let arrayForSpinner = arrayBuilder(min, max, 10);
            createSpinnerModal("maxThreshNum", "Max Threshold", "MaximumThreshold_Manual", arrayForSpinner);

		}		
	});
</script>
<%} %>
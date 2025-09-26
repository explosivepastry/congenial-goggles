<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    
        string Min = "";
        string Max = "";
        long DefaultMin = 0;
        long DefaultMax = 0;

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
        MonnitApplicationBase.DefaultThresholds(Model, out DefaultMin, out DefaultMax);

        DefaultMin = DefaultMin / 10;
        DefaultMax = DefaultMax / 10;       
%>

<div class="row sensorEditForm">
	<%  bool isFahrenheit = true;
		if (ViewData["TempScale"] != null)
		{
			isFahrenheit = ViewData["TempScale"].ToStringSafe() == "F";
		}
		else
		{
			isFahrenheit = Temperature.IsFahrenheit(Model.SensorID);
		}
	%>
	<div class="col-12 col-md-3">
		<%: Html.TranslateTag("Sensor/ApplicationCustomization|Display Values In","Display Values In")%>
	</div>

	<div class="col sensorEditFormInput" style="width: 250px !important;">
		<select id="TempScale" name="tempscale" class="form-select">
			<option value="<%="on"%>" <%=isFahrenheit ? "selected='selected'" : "" %>>
				<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Fahrenheit","Fahrenheit")%>
			</option>
			<option style="display: flex; justify-content: space-between;" value="<%="off"%>" <%=isFahrenheit ? "" : "selected='selected'" %>>
				<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Celsius","Celsius")%>
			</option>
		</select>
	</div>
</div>

<div class="clearfix"></div>
<br />

<script type="text/javascript">
	var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

	$(document).ready(function () {

		$('#TempScale').change(function () {
			SetScale();
		});


		function SetScale() {
			//"off" = Celcius
			//"on"  = Fahrenheit
			
			if ($('#TempScale').val() == "off") {
				$('#MinimumThreshold_Manual').val(ConvertToCelcius($('#MinimumThreshold_Manual').val()));
				$('#MaximumThreshold_Manual').val(ConvertToCelcius($('#MaximumThreshold_Manual').val()));

				$('#aboveType').html("(<strong>°C</strong>)");
				$('#belowType').html("(<strong>°C</strong>)");
				
				var MinVal = <%=DefaultMin%>;
				var MaxVal = <%=DefaultMax%>;				
				
				MinThresMobiscroll(MinVal, MaxVal, 'Minimum Threshold °C');
				MaxThresMobiscroll(MinVal, MaxVal, 'Maximum Threshold °C');				
			}
			else {
				$('#MinimumThreshold_Manual').val(ConvertToFahrenheit($('#MinimumThreshold_Manual').val()));
				$('#MaximumThreshold_Manual').val(ConvertToFahrenheit($('#MaximumThreshold_Manual').val()));

				$('#aboveType').html("(<strong>°F</strong>)");
				$('#belowType').html("(<strong>°F</strong>)");
				
				var MinVal = ConvertToFahrenheit(<%=DefaultMin%>);
				var MaxVal = ConvertToFahrenheit(<%=DefaultMax%>);				

				MinThresMobiscroll(MinVal, MaxVal, 'Minimum Threshold °F');
				MaxThresMobiscroll(MinVal, MaxVal, 'Maximum Threshold °F');				
			}
		};
				
		function ConvertToCelcius(num) {
			return round((num - 32) * 5 / 9, 1); 
		}

		function ConvertToFahrenheit(num) {
			return round((num * 9 / 5) + 32, 1);
		}

		function round(value, precision) {
			var multiplier = Math.pow(10, precision || 0);
			return Math.round(value * multiplier) / multiplier;
		}

		function MinThresMobiscroll(min, max, headertext) {

            createSpinnerModal("minThreshNum", headertext, "MinimumThreshold_Manual", [-40, -30, -20, -10, 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120,]);

		}

		function MaxThresMobiscroll(min, max, headertext) {

            createSpinnerModal("maxThreshNum", headertext, "MaximumThreshold_Manual", [-40, -30, -20, -10, 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120,]);

		}	

		//SetScale();

	});
</script>


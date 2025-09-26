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

    <div class="col sensorEditFormInput">
        <select id="TempScale" name="tempscale" class="form-select">
            <option value="<%="on"%>" <%=isFahrenheit ? "selected='selected'" : "" %>>
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Fahrenheit","Fahrenheit")%>
            </option>
            <option style="display: flex; justify-content: space-between;" value="<%="off"%>" <%=isFahrenheit ? "" : "selected='selected'" %>>
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Celsius","Celsius")%>
            </option>
        </select>
        <label for="TempScale">
            <svg xmlns="http://www.w3.org/2000/svg" width="12" height="7.41" viewBox="0 0 12 7.41" style="cursor: pointer;">
                <path id="ic_keyboard_arrow_down_24px" d="M7.41,7.84,12,12.42l4.59-4.58L18,9.25l-6,6-6-6Z" transform="translate(-6 -7.84)" />
            </svg>
        </label>
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

            let arrayForSpinner1 = arrayBuilder(min, max, 10);
            createSpinnerModal("minThreshNum", "Below", "MinimumThreshold_Manual", arrayForSpinner1);
        }

        function MaxThresMobiscroll(min, max, headertext) {

            let arrayForSpinner = arrayBuilder(min, max, 10);
            createSpinnerModal("maxThreshNum", "Above", "MaximumThreshold_Manual", arrayForSpinner);
        }

    });
</script>


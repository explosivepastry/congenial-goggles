<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string PressureHyst = "";
    string PressureMin = "";
    string PressureMax = "";
    string TempHyst = "";
    string TempMin = "";
    string TempMax = "";

    MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out PressureHyst, out PressureMin, out PressureMax);

    TempMin = QuartzdynePressure.TemperatureMinThreshForUI(Model).ToString();
    TempMax = QuartzdynePressure.TemperatureMaxThreshForUI(Model).ToString();
    TempHyst = QuartzdynePressure.TemperatureHystForUI(Model).ToString();

    long DefaultMin = 0;
    long DefaultMax = 0;
    QuartzdynePressure.DefaultTempThresholds(Model, out DefaultMin, out DefaultMax);
%>



<h2>Pressure Settings</h2>

<p class="useAwareState"><%: Html.TranslateTag("Use Aware State","Use Aware State")%></p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Below","Below")%>  (PSI)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=PressureMin %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Above","Above")%> (PSI)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=PressureMax %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Aware State Buffer","Aware State Buffer")%>  (PSI)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=PressureHyst %>" />
        <a id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>


<h2><%: Html.TranslateTag("Temperature Settings","Temperature Settings")%></h2>

<h5><%: Html.TranslateTag("Use Aware State","Use Aware State")%></h5>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Below","Below")%>  (<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="TempMinThreshold_Manual" id="TempMinThreshold_Manual" value="<%=TempMin %>" />
        <a id="tempMinThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration1)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Above","Above")%> (<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="TempMaxThreshold_Manual" id="TempMaxThreshold_Manual" value="<%=TempMax %>" />
        <a id="tempMaxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration2)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Aware State Buffer","Aware State Buffer")%>  (<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="TempHysteresis_Manual" id="TempHysteresis_Manual" value="<%=TempHyst %>" />
        <a id="tempHystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration3)%>
    </div>
</div>
<br />
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Warm Up Time","Warm Up Time")%> (Milliseconds)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="warmUpTime_Manual" id="warmUpTime_Manual" value="<%=Model.Calibration4 %>" />
        <a id="warmUpNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration4)%>
    </div>
</div>

<script>





    $(document).ready(function () {

        <% if (Model.CanUpdate)
    { %>

        const arrayForSpinner = arrayBuilder(0, 30000, 1000);
        createSpinnerModal("minThreshNum", "PSI", "MinimumThreshold_Manual", arrayForSpinner);
        createSpinnerModal("maxThreshNum", "PSI", "MaximumThreshold_Manual", arrayForSpinner);

        const arrayForSpinner1 = arrayBuilder(0, 1000, 50);
        createSpinnerModal("hystNum", "PSI", "Hysteresis_Manual", arrayForSpinner1);

        const arrayForSpinner2 = arrayBuilder(<%:DefaultMin%>, <%:DefaultMax%>, 1);
        createSpinnerModal("tempMinThreshNum", "<%:Temperature.IsFahrenheit(Model.SensorID) ? "Fahrenheit":"Celsius" %>", "TempMinThreshold_Manual", arrayForSpinner2);
        createSpinnerModal("tempMaxThreshNum", "<%:Temperature.IsFahrenheit(Model.SensorID) ? "Fahrenheit":"Celsius" %>", "TempMaxThreshold_Manual", arrayForSpinner2);

        const arrayForSpinner3 = arrayBuilder(0, 50, 1);
        createSpinnerModal("tempHystNum", "<%:Temperature.IsFahrenheit(Model.SensorID) ? "Fahrenheit":"Celsius" %>", "TempHysteresis_Manual", arrayForSpinner3);

        const arrayForSpinner4 = arrayBuilder(100, 10000, 100);
        createSpinnerModal("warmUpNum", "<%:Temperature.IsFahrenheit(Model.SensorID) ? "Fahrenheit":"Celsius" %>", "warmUpTime_Manual", arrayForSpinner4);


        <%}%>

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())) {
                if ($("#MinimumThreshold_Manual").val() < 0)
                    $("#MinimumThreshold_Manual").val(0);
                if ($("#MinimumThreshold_Manual").val() > 30000)
                    $("#MinimumThreshold_Manual").val(30000);
                if (parseFloat($("#MinimumThreshold_Manual").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                    $("#MinimumThreshold_Manual").val(parseFloat($("#MaximumThreshold_Manual").val()));
            } else {
                $("#MinimumThreshold_Manual").val(<%: PressureMin%>);
            }
        });

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())) {
                if ($("#MaximumThreshold_Manual").val() < 0)
                    $("#MaximumThreshold_Manual").val(0);
                if ($("#MaximumThreshold_Manual").val() > 30000)
                    $("#MaximumThreshold_Manual").val(30000);

                if (parseFloat($("#MaximumThreshold_Manual").val()) < parseFloat($("#MinimumThreshold_Manual").val()))
                    $("#MaximumThreshold_Manual").val(parseFloat($("#MinimumThreshold_Manual").val()));
            } else {
                $("#MaximumThreshold_Manual").val(<%: PressureMax%>);
            }
        });

        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < 0)
                    $("#Hysteresis_Manual").val(0);
                if ($("#Hysteresis_Manual").val() > 1000)
                    $("#Hysteresis_Manual").val(1000)
            }
            else {
                $('#Hysteresis_Manual').val(<%: PressureHyst%>);
            }
        });

        $("#TempMinThreshold_Manual").change(function () {
            if (isANumber($("#TempMinThreshold_Manual").val())) {
                if ($("#TempMinThreshold_Manual").val() < <%:(DefaultMin)%>)
                    $("#TempMinThreshold_Manual").val(<%:(DefaultMin)%>);
                if ($("#TempMinThreshold_Manual").val() > <%:(DefaultMax)%>)
                    $("#TempMinThreshold_Manual").val(<%:(DefaultMax)%>);
                if (parseFloat($("#TempMinThreshold_Manual").val()) > parseFloat($("#TempMaxThreshold_Manual").val()))
                    $("#TempMinThreshold_Manual").val(parseFloat($("#TempMaxThreshold_Manual").val()));
            } else {
                $("#TempMinThreshold_Manual").val(<%: TempMin%>);
            }
        });

        $("#TempMaxThreshold_Manual").change(function () {
            if (isANumber($("#TempMaxThreshold_Manual").val())) {
                if ($("#TempMaxThreshold_Manual").val() < <%:(DefaultMin)%>)
                    $("#TempMaxThreshold_Manual").val(<%:(DefaultMin)%>);
                if ($("#TempMaxThreshold_Manual").val() > <%:(DefaultMax)%>)
                    $("#TempMaxThreshold_Manual").val(<%:(DefaultMax)%>);
                if (parseFloat($("#TempMaxThreshold_Manual").val()) < parseFloat($("#TempMinThreshold_Manual").val()))
                    $("#TempMaxThreshold_Manual").val(parseFloat($("#TempMinThreshold_Manual").val()));
            } else {

                $("#TempMaxThreshold_Manual").val(<%: TempMax%>);
            }
        });

        $("#TempHysteresis_Manual").change(function () {
            if (isANumber($("#TempHysteresis_Manual").val())) {
                if ($("#TempHysteresis_Manual").val() < 0)
                    $("#TempHysteresis_Manual").val(0);
                if ($("#TempHysteresis_Manual").val() > 50)
                    $("#TempHysteresis_Manual").val(50)
            }
            else {
                $('#TempHysteresis_Manual').val(<%: TempHyst%>);
            }
        });

        $("#warmUpTime_Manual").change(function () {
            if (isANumber($("#warmUpTime_Manual").val())) {
                if ($("#warmUpTime_Manual").val() < 100)
                    $("#warmUpTime_Manual").val(100);
                if ($("#warmUpTime_Manual").val() > 10000)
                    $("#warmUpTime_Manual").val(10000)
            }
            else {
                $('#warmUpTime_Manual').val(<%: Model.Calibration4%>);
            }
        });
    });
</script>

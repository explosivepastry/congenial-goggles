<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    string VibrationAwareThreshold = "";
    string VibrationSensitivityThreshold = "";

    string WindowFunction = "";
    string VibrationMode = "";
    string AccelerometerRange = "";
    string MeasurementInterval = "";
    string VibrationHysteresis = "";

    VibrationAwareThreshold = AdvancedVibration.GetVibrationAwareThreshold(Model).ToString();
    VibrationSensitivityThreshold = AdvancedVibration.GetVibrationSensitivityThreshold(Model).ToString();

    WindowFunction = AdvancedVibration.GetWindowFunction(Model).ToString();
    VibrationMode = AdvancedVibration.GetVibrationMode(Model).ToString();
    AccelerometerRange = AdvancedVibration.GetAccelerometerRange(Model).ToString();
    MeasurementInterval = AdvancedVibration.GetMeasurementInterval(Model).ToString("0.##");
    VibrationHysteresis = AdvancedVibration.GetVibrationHysteresis(Model).ToString();
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_111|Vibration Mode","Vibration Mode")%>
    </div>
    <div class="col sensorEditFormInput">
        <select <%=Model.CanUpdate ? "" : "disabled" %> id="VibrationMode_Manual" name="VibrationMode_Manual" class="form-select">
            <option <%: VibrationMode == "0"? "selected":"" %> value="0">Velocity RMS</option>
            <option <%: VibrationMode == "1"? "selected":"" %> value="1">Acceleration RMS</option>
            <option <%: VibrationMode == "2"? "selected":"" %> value="2">Acceleration Peak</option>
            <option <%: VibrationMode == "4"? "selected":"" %> value="4">Displacement</option>
        </select>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_111|Vibration Aware Threshold","Vibration Aware Threshold")%> (<span class="VATlabel"></span>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled" %> name="VibrationAwareThreshold_Manual" id="VibrationAwareThreshold_Manual" value="<%=VibrationAwareThreshold %>" />
<%--        <a id="vatNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>--%>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_111|Vibration Hysteresis","Vibration Hysteresis")%> (<span class="VATlabel"></span>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled" %> name="VibrationHysteresis_Manual" id="VibrationHysteresis_Manual" value="<%=VibrationHysteresis %>" />
        <a id="vibHystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_111|Minimum Sensitivity","Minimum  Sensitivity")%> (g)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled" %> name="VibrationSensitivityThreshold_Manual" id="VibrationSensitivityThreshold_Manual" value="<%=VibrationSensitivityThreshold %>" />
        <a id="VSTThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration3)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_111|Window Function","Window Function")%>
    </div>
    <div class="col sensorEditFormInput">
        <select <%=Model.CanUpdate ? "" : "disabled" %> id="WindowFunction_Manual" name="WindowFunction_Manual" class="form-select">
            <option <%: WindowFunction == "0"? "selected":"" %> value="0">Rect</option>
            <option <%: WindowFunction == "1"? "selected":"" %> value="1">Flat Top</option>
            <option <%: WindowFunction == "2"? "selected":"" %> value="2">Hanning</option>
        </select>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_111|Accelerometer Range","Accelerometer Range")%>
    </div>
    <div class="col sensorEditFormInput">
        <select <%=Model.CanUpdate ? "" : "disabled" %> id="AccelerometerRange_Manual" name="AccelerometerRange_Manual" class="form-select">
            <option <%: AccelerometerRange == "0"? "selected":"" %> value="0">8 g</option>
            <option <%: AccelerometerRange == "1"? "selected":"" %> value="1">16 g</option>
            <option <%: AccelerometerRange == "2"? "selected":"" %> value="2">32 g</option>
        </select>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Measurement Interval","Measurement Interval")%> (<%: Html.TranslateTag("Seconds","Seconds")%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" id="MeasurementInterval_Manual" <%=Model.CanUpdate ? "" : "disabled" %> name="MeasurementInterval_Manual" type="number" value="<%=MeasurementInterval %>">
        <a id="measIntNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script type="text/javascript">
    var label = "";
    var VatMax = 0;
    var VATStep = 10;
    var VATVal = parseFloat(<%=VibrationAwareThreshold%>);

    var vibHyst_array = [0, 1, 2, 5, 10, 15, 20, 30, 40, 50];
    var measureInt_array = [10, 20, 30, 60, 120, 240, 360, 720];

    $(function () {
        var VAtMode = <%=VibrationMode%>
            setVATlabel(VAtMode);
            <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("VSTThreshNum", "g", "VibrationSensitivityThreshold_Manual", [0, 1, 2], null, [".00", ".10", ".20", ".30", ".40", ".50", ".60", ".70", ".80", ".90"]);
        createSpinnerModal("measIntNum", "Seconds", "MeasurementInterval_Manual", measureInt_array);
        createSpinnerModal("vibHystNum", label, "VibrationHysteresis_Manual", vibHyst_array);

        $('#VibrationSensitivityThreshold_Manual').change(function () {
            if (isANumber($("#VibrationSensitivityThreshold_Manual").val())) {
                if ($("#VibrationSensitivityThreshold_Manual").val() < 0.00)
                    $("#VibrationSensitivityThreshold_Manual").val(0.00);
                if ($("#VibrationSensitivityThreshold_Manual").val() > 2.55)
                    $("#VibrationSensitivityThreshold_Manual").val(2.55);
            }
            else {
                $('#VibrationSensitivityThreshold_Manual').val(<%: VibrationSensitivityThreshold%>);
            }
        });

        $('#VibrationAwareThreshold_Manual').change(function () {
            if (isANumber($("#VibrationAwareThreshold_Manual").val())) {
                if ($("#VibrationAwareThreshold_Manual").val() < 0)
                    $("#VibrationAwareThreshold_Manual").val(0);
                if ($("#VibrationAwareThreshold_Manual").val() > VatMax)
                    $("#VibrationAwareThreshold_Manual").val(VatMax);
            }
            else {
                $('#VibrationSensitivity_Manual').val(<%: VibrationAwareThreshold%>);
            }
        });

        $('#MeasurementInterval_Manual').change(function () {
            if (isANumber($("#MeasurementInterval_Manual").val())) {
                if ($("#MeasurementInterval_Manual").val() < 1)
                    $("#MeasurementInterval_Manual").val(1);
                if ($("#MeasurementInterval_Manual").val() > 43200)
                    $("#MeasurementInterval_Manual").val(43200);

                if (Number($("#MeasurementInterval_Manual").val()) > Number($("#ActiveStateInterval").val()) * 60) {
                    $("#MeasurementInterval_Manual").val(Math.round((Number($("#ActiveStateInterval").val()) * 60) * 100) / 100);
                }
            }
            else {
                $('#MeasurementInterval_Manual').val(<%: MeasurementInterval%>);
                $('#MeasurementInterval_Manual').val(<%: MeasurementInterval%>);
            }
        });

        $('#VibrationHysteresis_Manual').change(function () {
            if (isANumber($("#VibrationHysteresis_Manual").val())) {
                if ($("#VibrationHysteresis_Manual").val() < 0)
                    $("#VibrationHysteresis_Manual").val(0);
                if ($("#VibrationHysteresis_Manual").val() > 50)
                    $("#VibrationHysteresis_Manual").val(50);
            }
            else {
                $('#VibrationHysteresis_Manual').val(<%: VibrationHysteresis%>);
            }
        });
        $('#VibrationMode_Manual').change(function () {
            //if(vibration mode is dispacement change window function to hanning
            if ($('#VibrationMode_Manual').val() == '4') {
                $('#WindowFunction_Manual').val(2);
                $('#WindowFunction_Manual').change();
            }
            setFreqRanges($('#SampleRate_Manual').val());
            setVATSettings(Number($('#VibrationMode_Manual').val()));

            $('#vatNum').mobiscroll('option', {
                theme: 'ios',
                display: popLocation,
                min: 0,
                max: VatMax,
                step: VATStep,
                value: VatMax,
            });
            $('#VibrationAwareThreshold_Manual').change();
        });

         <%}%>
    });

    function setVATSettings(mode) {
        switch (mode) {
            case 0:
                label = 'mm/s';
                VatMax = 655.35;
                VATStep = .01;
                break;

            case 2:
            case 1:
                label = 'mm/s^2';
                VatMax = 65535;
                VATStep = 1000;
                break;

            case 4:
                label = 'mm';
                VatMax = 655.35;
                VATStep = .01;
                break;
        }
        $('.VATlabel').html(label);
        $('#VibrationAwareThreshold_Manual').val(VatMax);
    }

    function setVATlabel(mode) {
        switch (mode) {

            case 0:
                label = 'mm/s';
                VatMax = 655.35;
                VATStep = .01;
                break;

            case 2:
            case 1:

                label = ' mm/s^2';
                VatMax = 65535;
                VATStep = 100;
                break;

            case 4:
                label = 'mm';
                VatMax = 655.35;
                VATStep = .01;
                break;
        }
        $('#VibrationAwareThreshold_Manual').val(VATVal);
        $('.VATlabel').html(label);
    }

</script>


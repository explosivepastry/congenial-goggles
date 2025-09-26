<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string MinFrequency = "";
    string MaxFrequency = "";
    string SampleRate = "";
    string scale = "";

    MinFrequency = AdvancedVibration.GetMinFrequency(Model).ToString();
    MaxFrequency = AdvancedVibration.GetMaxFrequency(Model).ToString();
    SampleRate = AdvancedVibration.GetSampleRate(Model).ToString();

    scale = AdvancedVibration.IsHertz(Model.SensorID) ? "Hz" : "rpm";

    
    
%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_111|Sample Rate","Sample Rate")%>
    </div>
    <div class="col sensorEditFormInput">
       <select  <%=Model.CanUpdate ? "" : "disabled" %> id="SampleRate_Manual" name="SampleRate_Manual" class="form-select" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
         <%--   <option <%: SampleRate == "15"? "selected":"" %> value="15">25600 Hz</option>--%>
            <option <%: SampleRate == "14"? "selected":"" %> value="14">12800 Hz</option>
            <option <%: SampleRate == "13"? "selected":"" %> value="13">6400 Hz</option>
            <option <%: SampleRate == "12"? "selected":"" %> value="12">3200 Hz</option>
            <option <%: SampleRate == "7"? "selected":"" %> value="7">1600 Hz</option>
            <option <%: SampleRate == "6"? "selected":"" %> value="6">800 Hz</option>
            <option <%: SampleRate == "5"? "selected":"" %> value="5">400 Hz</option>
            <option <%: SampleRate == "4"? "selected":"" %> value="4">200 Hz</option>
            <option <%: SampleRate == "3"? "selected":"" %> value="3">100 Hz</option>
            <option <%: SampleRate == "2"? "selected":"" %> value="2">50 Hz</option>
            <option <%: SampleRate == "1"? "selected":"" %> value="1">25 Hz</option>
        </select>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_111|Frequency Range Minimum","Frequency Range Minimum")%> (<%: scale %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled" %> name="MinFrequency_Manual" id="MinFrequency_Manual" value="<%=MinFrequency %>" />
        <a id="FrequencyRangeMinNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration3)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_111|Frequency Range Maximum","Frequency Range Maximum")%> (<%: scale %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled" %> name="MaxFrequency_Manual" id="MaxFrequency_Manual" value="<%=MaxFrequency %>" />
        <a id="FrequencyRangeMaxNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration3)%>
    </div>
</div>


<script type="text/javascript">

    var MinUIVal = 0;
    var MaxUIVal = 0;
    var UIStep = 0;
    var scale = '<%=scale%>';

    $(document).ready(function () {

        setFreqRangesFirst(<%=SampleRate%>);

        <% if (Model.CanUpdate)
                   { %>

        const arrayForSpinner = arrayBuilder(MinUIVal, MaxUIVal, UIStep);
        createSpinnerModal("FrequencyRangeMinNum", "<%: scale %>", "MinFrequency_Manual", arrayForSpinner);
        createSpinnerModal("FrequencyRangeMaxNum", "<%: scale %>", "MaxFrequency_Manual", arrayForSpinner);

        <%}%>

        $("#MinFrequency_Manual").change(function () {
            if (isANumber(Number($("#MinFrequency_Manual").val()))) {
                if (parseFloat($("#MinFrequency_Manual").val()) < MinUIVal) {
                    $("#MinFrequency_Manual").val(MinUIVal);
                }
                if (parseFloat($("#MinFrequency_Manual").val()) > MaxUIVal) {
                    $("#MinFrequency_Manual").val(MaxUIVal);
                }

                if (parseFloat($("#MinFrequency_Manual").val()) > parseFloat($("#MaxFrequency_Manual").val())) {
                    $("#MinFrequency_Manual").val(parseFloat($("#MaxFrequency_Manual").val()));
                }
            }
            else {
                $("#MinFrequency_Manual").val(parseFloat(<%: MinFrequency%>));
            }
        });

        $("#MaxFrequency_Manual").change(function () {
            if (isANumber(Number($("#MaxFrequency_Manual").val()))) {
                if (parseFloat($("#MaxFrequency_Manual").val()) < MinUIVal) {
                    $("#MaxFrequency_Manual").val(MinUIVal);
                }

                if (parseFloat($("#MaxFrequency_Manual").val()) > MaxUIVal) {
                    $("#MaxFrequency_Manual").val(MaxUIVal);
                }

                if (parseFloat($("#MaxFrequency_Manual").val()) < $("#MinFrequency_Manual").val()) {
                    $("#MaxFrequency_Manual").val(parseFloat($("#MinFrequency_Manual").val()));
                }
            }
            else {
                $("#MaxFrequency_Manual").val(<%: MaxFrequency%>);
            }
        });
        
        $('#SampleRate_Manual').change(function (e) { 
            e.preventDefault();
            setFreqRanges($('#SampleRate_Manual').val());
        });
    });

    function setFreqRanges(sampleRate) {

        var mode = $('#VibrationMode_Manual').val();
        sampleRate = Number(sampleRate);

        switch (mode) {
            case '0': // Velocity RMS
                setFreqRangesVelocity(sampleRate);
                break;
            case '1': //Acceleration RMS
            case '2': //Acceleration Peak
                setFreqRangesAcceleration(sampleRate);
                break;
            case '4': //Displacement
                setFreqRangesDisplacement(sampleRate)
                break;
            default:
                break;
        }

        $('#FrequencyRangeMinNum').mobiscroll('option', {
            theme: 'ios',
            display: popLocation,
            min: MinUIVal,
            max: MaxUIVal,
            step: UIStep,
            value: MinUIVal,
        });

        $('#FrequencyRangeMaxNum').mobiscroll('option', {
            theme: 'ios',
            display: popLocation,
            min: MinUIVal,
            max: MaxUIVal,
            step: UIStep,
            value: MaxUIVal,
        });

        $("#MaxFrequency_Manual").val(MaxUIVal);
        $("#MinFrequency_Manual").val(MinUIVal);
    }

    function setFreqRangesFirst(sampleRate) {

        var mode = $('#VibrationMode_Manual').val();
        sampleRate = Number(sampleRate);

        switch (mode) {
            case '0': // Velocity RMS
                setFreqRangesVelocity(sampleRate);
                break;
            case '1': //Acceleration RMS
            case '2': //Acceleration Peak
                setFreqRangesAcceleration(sampleRate);
                break;
            case '4': //Displacement
                setFreqRangesDisplacement(sampleRate)
                break;
            default:
                break;
        }

        $('#FrequencyRangeMinNum').mobiscroll('option', {
            theme: 'ios',
            display: popLocation,
            min: MinUIVal,
            max: MaxUIVal,
            step: UIStep,
            value: <%=MinFrequency%>,
        });

        $('#FrequencyRangeMaxNum').mobiscroll('option', {
            theme: 'ios',
            display: popLocation,
            min: MinUIVal,
            max: MaxUIVal,
            step: UIStep,
            value: <%=MaxFrequency%>,
        });
    }

    function setFreqRangesAcceleration(sampleRate) {

        switch (sampleRate) {
            case 1:
                MinUIVal = 0.390625;
                MaxUIVal = 9.375;
                UIStep = 1;
                break;

            case 2:
                MinUIVal = 0.78125;
                MaxUIVal = 18.75;
                UIStep = 1;
                break;

            case 3:
                MinUIVal = 1.5625;
                MaxUIVal = 37.5;
                UIStep = 1;
                break;

            case 4:
                MinUIVal = 3.125;
                MaxUIVal = 75;
                UIStep = 5;
                break;

            case 5:
                MinUIVal = 6.25;
                MaxUIVal = 150;
                UIStep = 5;
                break;

            case 6:
                MinUIVal = 12.5;
                MaxUIVal = 300;
                UIStep = 10;
                break;

            case 7:
                MinUIVal = 25;
                MaxUIVal = 600;
                UIStep = 50;
                break;

            case 13:
                MinUIVal = 100;
                MaxUIVal = 2400;
                UIStep = 100;
                break;

            case 14:
                MinUIVal = 200;
                MaxUIVal = 4800;
                UIStep = 200;
                break;

            case 15:
                MinUIVal = 400;
                MaxUIVal = 6500;
                UIStep = 500;
                break;

            case 12:
            default:
                MinUIVal = 50;
                MaxUIVal = 1200;
                UIStep = 25;
                break;

        }

        if (scale == 'rpm') {
            MinUIVal = parseFloat(MinUIVal * 60);
            MaxUIVal = parseFloat(MaxUIVal * 60);
        }
    }

    function setFreqRangesVelocity(sampleRate) {

        switch (sampleRate) {
            case 1:
                MinUIVal = 0.5859375;
                MaxUIVal = 9.375;
                UIStep = 1;
                break;

            case 2:
                MinUIVal = 1.171875;
                MaxUIVal = 18.75;
                UIStep = 1;
                break;

            case 3:
                MinUIVal = 2.34375;
                MaxUIVal = 37.5;
                UIStep = 1;
                break;

            case 4:
                MinUIVal = 4.6875;
                MaxUIVal = 75;
                UIStep = 5;
                break;

            case 5:
                MinUIVal = 9.375;
                MaxUIVal = 150;
                UIStep = 5;
                break;

            case 6:
                MinUIVal = 18.75;
                MaxUIVal = 300;
                UIStep = 10;
                break;

            case 7:
                MinUIVal = 37.5;
                MaxUIVal = 600;
                UIStep = 50;
                break;

            case 13:
                MinUIVal = 150;
                MaxUIVal = 2400;
                UIStep = 100;
                break;

            case 14:
                MinUIVal = 300;
                MaxUIVal = 4800;
                UIStep = 200;
                break;

            case 15:
                MinUIVal = 600;
                MaxUIVal = 6500;
                UIStep = 500;
                break;

            case 12:
            default:
                MinUIVal = 75;
                MaxUIVal = 1200;
                UIStep = 25;
                break;
        }

        if (scale == 'rpm') {
            MinUIVal = parseFloat(MinUIVal * 60);
            MaxUIVal = parseFloat(MaxUIVal * 60);
        }
    }

    function setFreqRangesDisplacement(sampleRate) {

        switch (sampleRate) {
            case 1:
                MinUIVal = 0.78125;
                MaxUIVal = 9.375;
                UIStep = 1;
                break;

            case 2:
                MinUIVal = 1.5625;
                MaxUIVal = 18.75;
                UIStep = 1;
                break;

            case 3:
                MinUIVal = 3.125;
                MaxUIVal = 37.5;
                UIStep = 1;
                break;

            case 4:
                MinUIVal = 6.25;
                MaxUIVal = 75;
                UIStep = 5;
                break;

            case 5:
                MinUIVal = 12.5;
                MaxUIVal = 150;
                UIStep = 5;
                break;

            case 6:
                MinUIVal = 25;
                MaxUIVal = 300;
                UIStep = 10;
                break;

            case 7:
                MinUIVal = 50;
                MaxUIVal = 600;
                UIStep = 50;
                break;

            case 13:
                MinUIVal = 200;
                MaxUIVal = 2400;
                UIStep = 100;
                break;

            case 14:
                MinUIVal = 400;
                MaxUIVal = 4800;
                UIStep = 200;
                break;

            case 15:
                MinUIVal = 800;
                MaxUIVal = 6500;
                UIStep = 500;
                break;

            case 12:
            default:
                MinUIVal = 100;
                MaxUIVal = 1200;
                UIStep = 25;
                break;
        }

        if (scale == 'rpm') {
            MinUIVal = parseFloat(MinUIVal * 60);
            MaxUIVal = parseFloat(MaxUIVal * 60);
        }
    }

</script>

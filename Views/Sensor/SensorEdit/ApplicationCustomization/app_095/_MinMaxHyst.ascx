<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    string Hyst = "";
    string Min = "";
    string Max = "";
    string label = "mm/s";

    int windowFunction = VibrationMeter.GetWindowFunction(Model);
    int measMethod = VibrationMeter.GetMeasurementMethod(Model);

    MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst, out Min, out Max);  
%>

<% if (new Version(Model.FirmwareVersion) >= new Version("2.5.4.6"))
    { %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_095|Sensitivity Threshold","Sensitivity Threshold")%>
    </div>
    <div class="col sensorEditFormInput">
        <select class="form-select" name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" <%: Model.CanUpdate ? "" :"disabled" %>>
            <option value="0" <%: Model.MaximumThreshold == 0 ?"selected":"" %>>0 g</option>
            <option value="205" <%: Model.MaximumThreshold == 205 ?"selected":"" %>>.1 g</option>
            <option value="512" <%: Model.MaximumThreshold == 512 ?"selected":"" %>>.25 g</option>
            <option value="1024" <%: Model.MaximumThreshold == 1024 ?"selected":"" %>>.5 g</option>
            <option value="2048" <%: Model.MaximumThreshold == 2048 ?"selected":"" %>>1 g</option>
        </select>
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_095|Measurement Method","Measurement Method")%>
    </div>
    <div class="col sensorEditFormInput">
        <select class="form-select" name="CalVal1_Manual" id="CalVal1_Manual" <%: Model.CanUpdate ? "" :"disabled" %>>
            <option value="0" <%: measMethod == 0 ?"selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_095|Velocity RMS using Peak Acceleration","Velocity RMS using Peak Acceleration")%></option>
            <option value="1" <%: measMethod == 1 ?"selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_095|Velocity RMS using Peak Velocity","Velocity RMS using Peak Velocity")%></option>
            <option value="2" <%: measMethod == 2 ?"selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_095|Velocity True RMS","Velocity True RMS")%></option>
        </select>
    </div>
</div>

<%} %>

<% if (new Version(Model.FirmwareVersion) >= new Version("16.34.22.5") && Model.GenerationType.ToUpper().Contains("GEN2"))
    {
        int enableAveraging = VibrationMeter.GetEnableAveraging(Model);
        double vibrationAwareThreshold = VibrationMeter.GetVibrationAwareThreshold(Model) / 10.0;
        double vibrationAwareBuffer = VibrationMeter.GetVibrationAwareBuffer(Model) / 10.0;

%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Enable Averaging", "Enable Averaging")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="EnableAveragingChk" id="EnableAveragingChk" <%=enableAveraging > 0 ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
        </div>
        <div style="display: none;"><%: Html.TextBox("EnableAveraging",enableAveraging, (Dictionary<string, object>)ViewData["HtmlAttributes"])%></div>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Vibration Aware Threshold","Vibration Aware Threshold")%> <%: label %>
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="VibrationAwareThreshold" id="VibrationAwareThreshold" value="<%=vibrationAwareThreshold %>" />
       
        <a id="VibrationAwareThresholdNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Vibration Aware Buffer","Vibration Aware Buffer")%> <%: label %>
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="VibrationAwareBuffer" id="VibrationAwareBuffer" value="<%=vibrationAwareBuffer %>" />
       
        <a id="VibrationAwareBufferNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>


<script type="text/javascript">

    var VibrationAwareBuffer_array = [0, 2, 5, 10, 20, 50, 100];
    var VibrationAwareThreshold_array = [10, 20, 30, 60, 120, 240, 360, 600];

    $(document).ready(function () {

        var MaxThresMinVal = 0;
        var MaxThresMaxVal = 600;

               <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("VibrationAwareThresholdNum", 'Vibration Aware Threshold ' + '<%=label%>', "VibrationAwareThreshold", VibrationAwareThreshold_array);
        createSpinnerModal("VibrationAwareBufferNum", 'Vibration Aware Buffer ' + '<%=label%>' + '<%=label%>', "VibrationAwareBuffer", VibrationAwareBuffer_array);

        <%}%>

        $("#VibrationAwareThreshold").change(function () {
            if (isANumber($("#VibrationAwareThreshold").val())) {
                if ($("#VibrationAwareThreshold").val() < MaxThresMinVal)
                    $("#VibrationAwareThreshold").val(MaxThresMinVal);
                if ($("#VibrationAwareThreshold").val() > MaxThresMaxVal)
                    $("#VibrationAwareThreshold").val(MaxThresMaxVal);


            } else {

                $("#VibrationAwareThreshold").val(<%: vibrationAwareThreshold%>);
            }
        });

        $("#VibrationAwareBuffer").change(function () {
            if (isANumber($("#VibrationAwareBuffer").val())) {
                if ($("#VibrationAwareBuffer").val() < 0)
                    $("#VibrationAwareBuffer").val(0);
                if ($("#VibrationAwareBuffer").val() > 100)
                    $("#VibrationAwareBuffer").val(100);


            } else {

                $("#VibrationAwareBuffer").val(<%: vibrationAwareBuffer%>);
                    }
                });

        $('#EnableAveragingChk').change(function () {
            if ($('#EnableAveragingChk').prop('checked')) {
                $('#EnableAveraging').val(1);
            } else {
                $('#EnableAveraging').val(0);
            }
        });

    });
</script>

<%} %>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_095|Window Function","Window Function")%>
    </div>
    <div class="col sensorEditFormInput">
        <select class="form-select" name="MinimumThreshold_Manual" id="Select1" <%: Model.CanUpdate ? "" :"disabled" %>>
            <option value="0" <%: windowFunction == 0 ?"selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_095|No windowing","No windowing")%></option>
            <option value="1" <%: windowFunction == 1 ?"selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_095|Hanning window","Hanning window")%></option>
            <option value="2" <%: windowFunction == 2 ?"selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_095|Flat Top window","Flat Top window")%></option>

        </select>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Measurement Interval","Measurement Interval")%> (<%: Html.TranslateTag("Minutes","Minutes")%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class=" form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="CalVal2_Manual" id="CalVal2_Manual" value="<%=Model.Calibration2 / 60 %>" />
        <a id="measIntNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration2)%>
    </div>
</div>

<% if (new Version(Model.FirmwareVersion) < new Version("2.5.4.6"))
    { %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_095|Vibration Intensity Threshold","Vibration Intensity Threshold")%> (g)
    </div>
    <div class="col sensorEditFormInput">
        <input class=" form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="CalVal3_Manual" id="CalVal3_Manual" value="<%=Model.Calibration3 %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration3)%>
    </div>
</div>

<%} %>



<script>

    var intensityThresh_array = [0, 2, 5, 10, 20, 50, 100, 127];
    var measureInt_array = [10, 20, 30, 60, 120, 240, 360, 720];

    $(function () {

                <% if (Model.CanUpdate)
    { %>
        //vibration intensity 
        createSpinnerModal("minThreshNum", 'G Force', "CalVal3_Manual", intensityThresh_array);
        createSpinnerModal("measIntNum", 'Minutes', "CalVal2_Manual", measureInt_array);

        <%}%>

        $('#CalVal2_Manual').change(function () {
            if (isANumber($("#CalVal2_Manual").val())) {
                if ($("#CalVal2_Manual").val() < 1)
                    $("#CalVal2_Manual").val(1);

                if ($("#CalVal2_Manual").val() > $('#ReportInterval').val()) {
                    $("#CalVal2_Manual").val($('#ReportInterval').val());
                }

            }
            else {
                $("#CalVal2_Manual").val(<%: Model.Calibration2 / 60%>);

            }
        });

        $('#CalVal3_Manual').change(function () {
            if (isANumber($("#CalVal3_Manual").val())) {

                if ($('#CalVal3_Manual').val() < 0) {
                    $('#CalVal3_Manual').val(0);

                }
                if ($('#CalVal3_Manual').val() > 127) {
                    $('#CalVal3_Manual').val(127);

                }
            }
            else {
                $('#CalVal3_Manual').val(<%: Model.Calibration3%>);
            }

        });
    });
</script>

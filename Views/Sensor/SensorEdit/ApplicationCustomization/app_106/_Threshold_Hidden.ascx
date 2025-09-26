<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%  
    //CO2 Instantaneous values
    int instaThreshold = CO2Meter.GetInstantaneousThreshold(Model);
    int instaBuffer = CO2Meter.GetInstantaneousBuffer(Model);

    // CO2 TWA(Time Waited Average) values
    int twaThreshold = CO2Meter.GetTWAThreshold(Model);
    int twaBuffer = CO2Meter.GetTWABuffer(Model);

    double measureInterval = CO2Meter.GetMeasurementInterval(Model);
    double autoZeroInterval = CO2Meter.GetAutoZeroInterval(Model);

    int enableAutoCalibration = CO2Meter.GetEnableAutoCalibration(Model);
%>


<h2>CO2 Instantaneous Settings</h2>
<%----insta min----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_106|CO2 Instantaneous Threshold","CO2 Instantaneous Threshold")%> PPM
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="instantaneousThreshold_Manual" id="instantaneousThreshold_Manual" value="<%=instaThreshold %>" />
        <a id="instaMin" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<%----insta buffer----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|CO2 Instantaneous Buffer","CO2 Instantaneous Buffer")%> PPM
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="instantaneousBuffer_Manual" id="instantaneousBuffer_Manual" value="<%=instaBuffer %>" />
        <%--        <a id="instaBuff" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>--%>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>


<h2>CO2 TWA Settings</h2>
<%----twa min----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_106|CO2 Time Weighted Average Threshold","CO2 Time Weighted Average Threshold")%> PPM
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="twaThreshold_Manual" id="twaThreshold_Manual" value="<%=twaThreshold %>" />
        <a id="twaMin" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<%----twa buffer----%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|CO2 Time Weighted Average Buffer","CO2 Time Weighted Average Buffer")%> PPM
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="twaBuffer_Manual" id="twaBuffer_Manual" value="<%=twaBuffer %>" />
        <%--<a id="twaBuff" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>--%>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>


<%--<h2>Interval Settings</h2>--%>
<%----measurement int----%>
<div class="row sensorEditForm" style="display: none;">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Measurement Interval","Measurement Interval")%> Minutes
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="measurementInterval_Manual" id="measurementInterval_Manual" value="<%=measureInterval %>" />
        <a id="cal1" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration1)%>
    </div>
</div>

<% if (new Version(Model.FirmwareVersion).Revision > 23)
   { %>
<h2>Auto Calibration</h2>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_106|Enable Auto Calibration","Enable Auto Calibration")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="enableOn" id="enableOn" <%=enableAutoCalibration > 0 ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
        </div>
        <input type="hidden" id="enableAutoCalibration_Manual" name="enableAutoCalibration_Manual" value="<%=enableAutoCalibration %>" />
    </div>
</div>

<%----Auto Cal interval----%>
<div class="row sensorEditForm" style="display: none;">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_106|Auto Calibration Interval","Auto Calibration Interval")%> Days
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="autoZeroInterval_Manual" id="autoZeroInterval_Manual" value="<%=autoZeroInterval %>" />
        <a id="cal2" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration2)%>
    </div>
</div>
<%} %>
<script>

    var measurementInterval_array = [0.33, 0.5, 0.75, 1, 2, 5, 10];


    $(document).ready(function () {

                <% if (Model.CanUpdate)
                   { %>

        const arrayForSpinner = arrayBuilder(0, 10000, 100);
        createSpinnerModal("instaMin", "PPM", "instantaneousThreshold_Manual", arrayForSpinner);
        createSpinnerModal("twaMin", "PPM", "twaThreshold_Manual", arrayForSpinner);
        createSpinnerModal("cal1", "Minutes", "measurementInterval_Manual", [1, 2, 5, 10, 15, 30]);
        const arrayForSpinnerCal2 = arrayBuilder(0, 30, 1);
        createSpinnerModal("cal2", "Days", "twaBuffer_Manual", arrayForSpinnerCal2);

         <%}%>
        $("#instantaneousThreshold_Manual").change(function () {
            if (isANumber($("#instantaneousThreshold_Manual").val())) {
                if ($("#instantaneousThreshold_Manual").val() < 0)
                    $("#instantaneousThreshold_Manual").val(0);
                if ($("#instantaneousThreshold_Manual").val() > 10000)
                    $("#instantaneousThreshold_Manual").val(10000);
            }
            else
                $("#instantaneousThreshold_Manual").val(<%: instaThreshold%>);
        });
    });

    $(function () {
        $("#instantaneousBuffer_Manual").change(function () {
            var instaThreshold = $("#instantaneousThreshold_Manual").val();
            instaThreshold = FiftyPercentOfThreshold(instaThreshold);

            if (isANumber($("#instantaneousBuffer_Manual").val())) {
                if ($("#instantaneousBuffer_Manual").val() < 0)
                    $("#instantaneousBuffer_Manual").val(0);
                if ($("#instantaneousBuffer_Manual").val() > instaThreshold);
                $("#instantaneousBuffer_Manual").val(instaThreshold);
            }
            else
                $("#instantaneousBuffer_Manual").val(<%: twaBuffer%>);
        });

        $('#twaThreshold_Manual').change(function () {
            if (isANumber($("#twaThreshold_Manual").val())) {
                if ($('#twaThreshold_Manual').val() < 0)
                    $('#twaThreshold_Manual').val(0);
                if ($('#twaThreshold_Manual').val() > 10000)
                    $('#twaThreshold_Manual').val(10000);
            }
            else {
                $('#twaThreshold_Manual').val(<%: CO2Meter.GetTWAThreshold(Model)%>);
            }
        });

        $("#twaBuffer_Manual").change(function () {
            var twaThreshold = $("#twaThreshold_Manual").val();
            twaThreshold = FiftyPercentOfThreshold(twaThreshold);

            if (isANumber($("#twaBuffer_Manual").val())) {
                if ($("#twaBuffer_Manual").val() < 0)
                    $("#twaBuffer_Manual").val(0);
                if ($("#twaBuffer_Manual").val() > twaThreshold)
                    $("#twaBuffer_Manual").val(twaThreshold);
            }
            else
                $("#twaBuffer_Manual").val(<%: twaBuffer%>);
        });

        $("#measurementInterval_Manual").change(function () {
            if (isANumber($("#measurementInterval_Manual").val())) {
                if ($("#measurementInterval_Manual").val() < 0.33)
                    $("#measurementInterval_Manual").val(0.33);
                if ($("#measurementInterval_Manual").val() > 10)
                    $("#measurementInterval_Manual").val(10);
            }
            else {
                $("#measurementInterval_Manual").val(<%: measureInterval%>);
            }
        });

        $('#enableOn').change(function () {
            if (this.checked) {
                $('#enableAutoCalibration_Manual').val("1");
            }
            else {
                $('#enableAutoCalibration_Manual').val("0");
            }
        });

        $("#autoZeroInterval_Manual").change(function () {
            if (isANumber($("#autoZeroInterval_Manual").val())) {
                if ($("#autoZeroInterval_Manual").val() < 1)
                    $("#autoZeroInterval_Manual").val(1);
                if ($("#autoZeroInterval_Manual").val() > 30)
                    $("#autoZeroInterval_Manual").val(30);
            }
            else
                $("#autoZeroInterval_Manual").val(<%: autoZeroInterval%>);
        });
    });

    function FiftyPercentOfThreshold(threshold) {
        var buffer = (threshold == 0) ? 0 : threshold / 2;
        return buffer;
    }

</script>
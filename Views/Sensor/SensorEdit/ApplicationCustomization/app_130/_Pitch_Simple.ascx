<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    double upthresh = TriggeredTilt.GetUpThreshold(Model);
    double downthresh = TriggeredTilt.GetDownThreshold(Model);
    int measurementStability = TriggeredTilt.GetMeasurementStability(Model);
    int timeout = TriggeredTilt.GetTimeout(Model);
    int AxisMode = TriggeredTilt.GetAxisMode(Model);
    double deltaValue = TriggeredTilt.GetDeltaValue(Model);
%>

<h4>Aware State</h4>

<div class="row sensorEditForm ">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|Down Angle Threshold","Down Angle Threshold")%> (Degrees)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="minThreshold_Manual" name="minThreshold_Manual" value="<%=downthresh %>" />
        <a id="minNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>
<div class="row sensorEditForm ">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|Up Angle Threshold","Up Angle Threshold")%> (Degrees)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="maxThreshold_Manual" name="maxThreshold_Manual" value="<%=upthresh %>" />
        <a id="maxNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<div class="row sensorEditForm deltaMode">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|Delta Value","Delta Value")%> (Degrees)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="DeltaValue_Manual" name="DeltaValue_Manual" value="<%=deltaValue %>" />
        <a id="deltaNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<%--- Motion Threshold ---%>
<div class="row sensorEditForm" style="display: none">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|Measurement Stablity","Measurement Stablity")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="measurementStability_Manual" name="measurementStability_Manual" value="<%=measurementStability %>" />
        <a id="motionNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<div class="row sensorEditForm ">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|Stuck Time Out","Stuck Time Out")%> (Seconds)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="timeOut_Manual" name="timeOut_Manual" value="<%=timeout %>" />
        <a id="TONum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<%--- Stuck Poll Interval ---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|Rotational Axis","Rotational Axis")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="AxisMode_Manual" name="AxisMode_Manual" class="form-select" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
            <option value="0" <%: AxisMode == 0 ? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|+X axis","+X axis")%></option>
            <option value="1" <%: AxisMode == 1 ? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|-X axis","-X axis")%></option>
            <option value="2" <%: AxisMode == 2 ? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|+Y axis","+Y axis")%></option>
            <option value="3" <%: AxisMode == 3 ? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|-Y axis","-Y axis")%></option>
            <option value="4" <%: AxisMode == 4 ? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|+Z axis","+Z axis")%></option>
            <option value="5" <%: AxisMode == 5 ? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_130|-Z axis","-Z axis")%></option>
        </select>
    </div>
</div>

<script type="text/javascript">

    $("#measurementStability_Manual").addClass('editField editFieldSmall');
    $("#timeOut_Manual").addClass('editField editFieldSmall');
    $("#minThreshold_Manual").addClass('editField editFieldSmall');
    $("#maxThreshold_Manual").addClass('editField editFieldSmall');

    $(function () {

        var delta_array = [1, 5, 10, 12, 15, 20, 30, 60, 75, 90];

       <% if (Model.CanUpdate) { %>

        createSpinnerModal("deltaNum", "Degrees", "DeltaValue_Manual", delta_array);
        const arrayForSpinner = arrayBuilder(1, 49, 1);
        createSpinnerModal("motionNum", "Measurements", "measurementStability_Manual", arrayForSpinner);
        const arrayForSpinner1 = arrayBuilder(1, 30, 1);
        createSpinnerModal("TONum", "Seconds", "timeOut_Manual", arrayForSpinner1);
        const arrayForSpinner2 = arrayBuilder(-180, 180, 5);
        createSpinnerModal("minNum", "Degrees", "minThreshold_Manual", arrayForSpinner2);
        createSpinnerModal("maxNum", "Degrees", "maxThreshold_Manual", arrayForSpinner2);

        <%}%>

        $("#DataMode_Manual").change(function () {
            setDataModeOptions();
            $('#AwareMode_Manual>option:eq(0)').attr('selected', true);
        });

        $("#DeltaValue_Manual").change(function () {
            if (isANumber($("#DeltaValue_Manual").val())) {
                if ($("#DeltaValue_Manual").val() < 0.5)
                    $("#DeltaValue_Manual").val(0.5);
                if ($("#DeltaValue_Manual").val() > 90)
                    $("#DeltaValue_Manual").val(90)
            }
            else {
                $("#DeltaValue_Manual").val(15);
            }
        });

        $('#measurementStability_Manual').change(function () {
            if (isANumber($("#measurementStability_Manual").val())) {
                if ($('#measurementStability_Manual').val() < 1)
                    $('#measurementStability_Manual').val(1);
                if ($('#measurementStability_Manual').val() > 49)
                    $('#measurementStability_Manual').val(49);
            }
            else {
                $('#measurementStability_Manual').val(<%: measurementStability%>);
            }
        });

        $('#timeOut_Manual').change(function () {
            if (isANumber($("#timeOut_Manual").val())) {
                if ($('#timeOut_Manual').val() < 1)
                    $('#timeOut_Manual').val(1);
                if ($('#timeOut_Manual').val() > 30)
                    $('#timeOut_Manual').val(30);
            }
            else {
                $('#timeOut_Manual').val(<%: timeout%>);
            }
        });

        $("#minThreshold_Manual").change(function () {
            if (isANumber($("#minThreshold_Manual").val())) {
                if ($("#minThreshold_Manual").val() < -180)
                    $("#minThreshold_Manual").val(-180);
                if ($("#minThreshold_Manual").val() > 180)
                    $("#minThreshold_Manual").val(180);


                if (parseFloat($("#minThreshold_Manual").val()) > parseFloat($('#maxThreshold_Manual').val()))
                    $("#minThreshold_Manual").val(parseFloat($('#maxThreshold_Manual').val()) - 1);
            }
            else {
                $("#minThreshold_Manual").val(<%: downthresh%>);
            }
        });

        $("#maxThreshold_Manual").change(function () {
            if (isANumber($("#maxThreshold_Manual").val())) {
                if ($("#maxThreshold_Manual").val() < -180)
                    $("#maxThreshold_Manual").val(-180);
                if ($("#maxThreshold_Manual").val() > 180)
                    $("#maxThreshold_Manual").val(180);

                if (parseFloat($("#maxThreshold_Manual").val()) < parseFloat($('#minThreshold_Manual').val()))
                    $("#maxThreshold_Manual").val(parseFloat($('#minThreshold_Manual').val()) + 1);
            }
            else {
                $("#maxThreshold_Manual").val(<%: upthresh%>);
            }
        });
    });
</script>

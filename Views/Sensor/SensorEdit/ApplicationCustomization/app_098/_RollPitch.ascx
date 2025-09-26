<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
      
    int pitchMin = RiceTilt.GetPitchMin(Model);
    int pitchMax = RiceTilt.GetPitchMax(Model);
    int rollMin = RiceTilt.GetRollMin(Model);
    int rollMax = RiceTilt.GetRollMax(Model);

    string hyst = RiceTilt.HystForUI(Model);
    string min = RiceTilt.MinThreshForUI(Model);
    string max = RiceTilt.MaxThreshForUI(Model);

    int motionThresh = RiceTilt.GetMotionThreshold(Model);
    int timeout = RiceTilt.GetTimeout(Model);
    int measurementFreq = RiceTilt.GetMeasurementFrequency(Model);
    int upthresh = RiceTilt.GetUpThreshold(Model);
    int downthresh = RiceTilt.GetDownThreshold(Model);
    int stableMaxThresh = RiceTilt.GetStableMaxThresh(Model);
    int stableMinThresh = RiceTilt.GetStableMinThresh(Model);
    int NPmotionThresh = RiceTilt.GetNoParentMotionThreshold(Model);
    
%>

<p class="useAwareState">Use Aware State</p>
<%--- Motion Threshold ---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_098|Motion Threshold","Motion Threshold")%>
    </div>
    <div class="col sensorEditFormInput">
        <input <%=Model.CanUpdate ? "" : "disabled" %> class="form-control" name="motionThreshold_Manual" id="motionThreshold_Manual" value="<%=motionThresh %>" />
        <a id="motionNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<%---No Parent Motion Threshold ---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_098|No Parent Motion Threshold","No Parent Motion Threshold")%>
    </div>
    <div class="col sensorEditFormInput">
        <input <%=Model.CanUpdate ? "" : "disabled" %> class="form-control" name="NPmotionThreshold_Manual" id="NPmotionThreshold_Manual" value="<%=NPmotionThresh %>" />
        <a id="npMotionNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<%--- Stuck Poll Interval ---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_098|Stuck Poll Interval","Stuck Poll Interval")%> (Milliseconds)
    </div>
    <div class="col sensorEditFormInput">
        <input <%=Model.CanUpdate ? "" : "disabled" %> class="form-control" name="StuckPollInterval_Manual" id="StuckPollInterval_Manual" value="<%=measurementFreq %>" />
        <a id="stuckNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<%--- Time Out  ---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_098|Time Out","Time Out")%> (Milliseconds)
    </div>
    <div class="col sensorEditFormInput">
        <input <%=Model.CanUpdate ? "" : "disabled" %> class="form-control" name="timeOut_Manual" id="timeOut_Manual" value="<%=timeout %>" />
        <a id="TONum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<%--- Position Threshold  ---%>
<p class="useAwareState">Position Threshold</p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_098|Down","Down")%> (Degrees)
    </div>
    <div class="col sensorEditFormInput">
        <input <%=Model.CanUpdate ? "" : "disabled" %> class="form-control" name="minThreshold_Manual" id="minThreshold_Manual" value="<%=min %>" />
        <a id="minNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_098|Up","Up")%> (Degrees)
    </div>
    <div class="col sensorEditFormInput">
        <input <%=Model.CanUpdate ? "" : "disabled" %> class="form-control" name="maxThreshold_Manual" id="maxThreshold_Manual" value="<%=max %>" />
        <a id="maxNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<script type="text/javascript">

    $("#motionThreshold_Manual").addClass('form-control');
    $("#NPmotionThreshold_Manual").addClass('form-control');
    $("#timeOut_Manual").addClass('form-control');
    $("#StuckPollInterval_Manual").addClass('form-control');
    $("#minThreshold_Manual").addClass('form-control');
    $("#maxThreshold_Manual").addClass('form-control');

    $(function () {
        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner1111 = arrayBuilder(0, 255, 5);
        createSpinnerModal("motionNum", "Motion Threshold", "motionThreshold_Manual", arrayForSpinner1111);

        <%}%>


        $('#motionThreshold_Manual').change(function () {
            if (isANumber($("#motionThreshold_Manual").val())) {
                if ($('#motionThreshold_Manual').val() < 0)
                    $('#motionThreshold_Manual').val(0);
                if ($('#motionThreshold_Manual').val() > 255)
                    $('#motionThreshold_Manual').val(255);
            }
            else {
                $('#motionThreshold_Manual').val(<%: motionThresh%>);
            }
        });

         <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner111 = arrayBuilder(2, 127, 5);
        createSpinnerModal("npMotionNum", "No Parent Motion Threshold", "NPmotionThreshold_Manual", arrayForSpinner111);

        <%}%>


        $('#NPmotionThreshold_Manual').change(function () {
            if (isANumber($("#NPmotionThreshold_Manual").val())) {
                if ($('#NPmotionThreshold_Manual').val() < 2)
                    $('#NPmotionThreshold_Manual').val(2);

                if ($('#NPmotionThreshold_Manual').val() > 127)
                    $('#NPmotionThreshold_Manual').val(127);
            }
            else {
                $('#NPmotionThreshold_Manual').val(<%: NPmotionThresh%>);
           }

         
       });

        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner11 = arrayBuilder(100, 30000, 100);
        createSpinnerModal("stuckNum", "Milliseconds", "StuckPollInterval_Manual", arrayForSpinner11);

        <%}%>

        $('#StuckPollInterval_Manual').change(function () {
            if (isANumber($("#StuckPollInterval_Manual").val())) {
                if ($('#StuckPollInterval_Manual').val() < 100)
                    $('#StuckPollInterval_Manual').val(100);
                if ($('#StuckPollInterval_Manual').val() > 30000)
                    $('#StuckPollInterval_Manual').val(30000);
            }
            else {
                $('#StuckPollInterval_Manual').val(<%: measurementFreq%>);
            }
        });


        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner1 = arrayBuilder(1000, 30000, 100);
        createSpinnerModal("TONum", "Milliseconds", "timeOut_Manual", arrayForSpinner1);

        <%}%>

        $('#timeOut_Manual').change(function () {
            if (isANumber($("#timeOut_Manual").val())) {
                if ($('#timeOut_Manual').val() < 1000)
                    $('#timeOut_Manual').val(1000);
                if ($('#timeOut_Manual').val() > 30000)
                    $('#timeOut_Manual').val(30000);
            }
            else {
                $('#timeOut_Manual').val(<%: timeout%>);
            }
        });


        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner = arrayBuilder(0, 90, 5);
        createSpinnerModal("minNum", "Degrees", "minThreshold_Manual", arrayForSpinner);

        <%}%>

        $("#minThreshold_Manual").change(function () {
            if (isANumber($("#minThreshold_Manual").val())) {
                if ($("#minThreshold_Manual").val() < 0)
                    $("#minThreshold_Manual").val(0);
                if ($("#minThreshold_Manual").val() > 90)
                    $("#minThreshold_Manual").val(90);

                if (parseFloat($("#minThreshold_Manual").val()) > parseFloat($('#maxThreshold_Manual').val()))
                    $("#minThreshold_Manual").val(parseFloat($('#maxThreshold_Manual').val()));
            }
            else {
                $("#minThreshold_Manual").val(<%: min%>);
            }
        });

        <% if (Model.CanUpdate)
           { %>


        createSpinnerModal("maxNum", "Degrees", "maxThreshold_Manual", arrayForSpinner);
        <%}%>

        $("#maxThreshold_Manual").change(function () {
            if (isANumber($("#maxThreshold_Manual").val())) {
                if ($("#maxThreshold_Manual").val() < 0)
                    $("#maxThreshold_Manual").val(0);
                if ($("#maxThreshold_Manual").val() > 90)
                    $("#maxThreshold_Manual").val(90);

                if (parseFloat($("#maxThreshold_Manual").val()) < parseFloat($('#minThreshold_Manual').val()))
                    $("#maxThreshold_Manual").val(parseFloat($('#minThreshold_Manual').val()));
            }
            else {
                $("#maxThreshold_Manual").val(<%: max%>);
            }
        });

    });
</script>


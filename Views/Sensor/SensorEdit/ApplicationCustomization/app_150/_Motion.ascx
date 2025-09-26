<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    bool AwareOnMotion = Motion_RH_WaterDetect.GetAwareOnMotion(Model).ToBool();
    double reArmTime = (Motion_RH_WaterDetect.GetRearmTime(Model) / 60.0);
    int sensitivity = Motion_RH_WaterDetect.GetPIRSensitivity(Model);
   bool isF = Motion_RH_WaterDetect.IsFahrenheit(Model.SensorID);
%>

<h5 style="font-weight:bold;">Motion:</h5>
<h5>&nbsp;Enter Aware State</h5>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3 ">
        &nbsp;&nbsp; <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_150|On Motion")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="motionOff" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="motionOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 y-0 mx-2" onclick="onOffToggleM()" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="ReportImmediatelyOnChk" id="ReportImmediatelyOnChk" <%=AwareOnMotion  ? "checked" : "" %>>
        </div>
        <div style="display: none;"><input id="ReportImmediatelyOn" name="ReportImmediatelyOn" type="text" value="<%=AwareOnMotion.ToInt() %>"></div>
    </div>
</div>

<%--Rearm Time--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
         <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_150|Re-Arm Time")%>  (<%: Html.TranslateTag("Minutes") %>)
    </div>
    <div class="col sensorEditFormInput">

        <input class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="TimeToReArm" name="TimeToReArm" value="<%=reArmTime.ToString("0.##") %>" />
        <a id="reArmNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_150|Sensitivity","Sensitivity")%>
    </div>
    <div class="col sensorEditFormInput">
        <select class="form-select" name="Sensitivity" id="Sensitivity" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="64" <%=sensitivity == 64 ? "selected" : "" %>><%= isF ?  ("9 " + Html.TranslateTag("Feet")) : ("2.7 " + Html.TranslateTag("Meters")) %></option>
            <option value="40" <%=sensitivity == 40 ? "selected" : "" %>><%= isF ?  ("12 " + Html.TranslateTag("Feet")) : ("3.7 " + Html.TranslateTag("Meters")) %></option>
            <option value="25" <%=sensitivity == 25 ? "selected" : "" %>><%= isF ?  ("15 " + Html.TranslateTag("Feet")) : ("4.6 " + Html.TranslateTag("Meters")) %></option>
        </select>
    </div>
</div>



<script type="text/javascript">



    $(document).ready(function () {
                <% if (Model.CanUpdate) { %>


        const arrayForSpinner = arrayBuilder(1, Number($('#ActiveStateInterval').val()), 1);
        createSpinnerModal("reArmNum", "Minutes", "TimeToReArm", arrayForSpinner);

    <%}%>

        $('#ReportImmediatelyOnChk').change(function () {
            if ($('#ReportImmediatelyOnChk').prop('checked')) {
                $('#ReportImmediatelyOn').val(1);
            } else {
                $('#ReportImmediatelyOn').val(0);
            }
        });

        $("#TimeToReArm").change(function () {
            if (isANumber($("#TimeToReArm").val())) {
                if ($("#TimeToReArm").val() < 0.17)
                    $("#TimeToReArm").val(0.17);

                if ($("#TimeToReArm").val() > 720)
                    $("#TimeToReArm").val(720);

                if (Number($("#TimeToReArm").val()) > Number($('#ActiveStateInterval').val()))
                    $("#TimeToReArm").val(Number($('#ActiveStateInterval').val()));
            }
            else {
                $("#TimeToReArm").val(1);
            }
        });
    });
    let Moff = document.getElementById("motionOff");
    let Mon = document.getElementById("motionOn");
    let motionToggle = document.getElementById("ReportImmediatelyOnChk");

    function onOffToggleM() {
        if (motionToggle.checked == true) {
            Moff.style.display = "none";
            Mon.style.display = "block";
        } else {
            Mon.style.display = "none";
            Moff.style.display = "block";
        }
    };
    onOffToggleM()
</script>

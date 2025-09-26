<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();

    TempData["CanCalibrate"] = true;
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);

    double humOffset = Motion_RH_WaterDetect.GetHumidityOffset(Model);
    double tempOffset = Motion_RH_WaterDetect.GetTemperatureOffset(Model);

    var monnitInstance = new Monnit.ResistiveBridgeMeter(Model.SensorID);
    string decimalPrecision = monnitInstance.Precision.Value;// Monnit.ResistiveBridgeMeter.GetPrecision(Model.SensorID).ToString();
    double ratedOutputValue = monnitInstance.ApplyScale(Model.LastDataMessage != null ? ((ResistiveBridgeMeter)Model.LastDataMessage.AppBase).MilliVoltsPerVolts : 0);
    string ratedOutputValueIncludingManualInputs = monnitInstance.ApplyUsersManualAdjustments(ratedOutputValue).ToString("N" + decimalPrecision);
    
    if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    {
        string label = ResistiveBridgeMeter.GetLabel(Model.SensorID);
        %>



<%if (!string.IsNullOrEmpty(ViewBag.ErrorMessage))
    { %>
<div class="formBody" style="color: red;"><%:ViewBag.ErrorMessage %></div>
<%} %>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    {%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post" style="padding-left: 6px;">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="returns" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
        { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>



    <div class="formtitle boldText">
        <%: Html.TranslateTag("Zero Calibration")%>
    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3" style="padding-left: 0">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_150|Observed Sensor Reading: (From sensor)","Observed Sensor Reading: (From sensor)")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" name="Observed" id="observed" readonly="readonly"
                value="<%:ratedOutputValue%>" />
            <%: label %>
        </div>
        <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
    </div>
</form>



    <% 
//if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
//{
    %>
<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="manualCorrection_<%:Model.SensorID %>" method="post" style="padding-left: 6px;">

    <div class="clearfix"></div>
    <div class="ln_solid"></div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3" style="padding-left: 0">
            <%: Html.TranslateTag("Manual Correction")%>
        </div>

        <div class="col sensorEditFormInput">
            <input id="manualCorrection" name="ManualCorrection" type="number" step="any" class="form-control" oninput="handleInputChange()"
                value="<%: monnitInstance.ManualCorrection.Value %>" />
        </div>
    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3" style="padding-left: 0">
            <%: Html.TranslateTag("Manual Offset")%>
        </div>
        <div class="col sensorEditFormInput">
            <input name="ManualOffset" id="manualOffset" type="number" step="any" class="form-control" oninput="handleInputChange()"
                value="<%: monnitInstance.ManualOffset.Value %>" />
            <%: label %>
        </div>
    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3" style="padding-left: 0">
        </div>
        <div class="col sensorEditFormInput" style="flex-direction: column; align-items: flex-start; gap: 4px;">
            <div><%= Html.TranslateTag("Rated Output:") %> <%= ratedOutputValue %> <%: label %></div>
            <div><%: Html.TranslateTag("Corrected Output:")%> <span id="correctedOutDisplay" class="fade-in-ani-ab"></span></div>
        </div>
    </div>

    <%
//}
    %>

    <div style="clear: both;"></div>
    <br />


    <div class="row sensorEditForm">
        <div class="col-12 col-md-3" style="padding-left: 0">
        </div>
        <div class="col sensorEditFormInput" style="justify-content: flex-end;">
            <button class="btn btn-primary" type="button" id="saveCorrection" onclick="" style="width: fit-content">
                <%: Html.TranslateTag("Save","Save")%>
            </button>
        </div>
    </div>

</form>

<script>

    const label = "<%: label %>";
    let decimalPrecision = "<%: decimalPrecision %>";

    $(function () {


        $("#HumOffset").change(function () {
            if (!isANumber($("#HumOffset").val())) {
                $("#HumOffset").val(0);
            } else {

                if ($("#HumOffset").val() > 10)
                    $("#HumOffset").val(10);

                if ($("#HumOffset").val() < -10)
                    $("#HumOffset").val(-10);

            }
        });


    });

</script>



<%}
    else
    {%>
<div class="formBody">
    <div>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_150|This sensor has been pre-calibrated and certified by","This sensor has been pre-calibrated and certified by")%> <%: CalibrationFacility.Load(Model.CalibrationFacilityID).Name %>.
    </div>
    <br />

    <div>
        <a target="_blank" href="http://74.93.64.170/iportal/iportal_documents/certs/<%: new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "") %>.pdf"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|View Calibration Certificate","View Calibration Certificate")%></a>
    </div>
</div>
<%}%>


<%	}%>

<script>

    $(document).ready(function () {

        $('#saveCorrection').click(function () {
            postForm($('#manualCorrection_<%:Model.SensorID %>'));
        });
    });
    const handleInputChange = () => {
        const observedReadingValue = Number(document.querySelector("#observed")?.value);
        const manualCorrectionValue = Number(document.querySelector("#manualCorrection")?.value);
        const manualOffsetValue = Number(document.querySelector("#manualOffset")?.value);

        if (observedReadingValue !== NaN && manualCorrectionValue !== NaN && manualOffsetValue !== NaN) {
            const correctedOutputValue = ((observedReadingValue * manualCorrectionValue) + manualOffsetValue).toFixed(decimalPrecision);
            const correctedOutputElement = document.querySelector("#correctedOutDisplay");

            correctedOutputElement.classList.remove("fade-in-ani-ab");
            correctedOutputElement.classList.add("fade-out-ani-ab");

            setTimeout(function () {
                correctedOutputElement.textContent = `${correctedOutputValue} ${label}`;
                correctedOutputElement.classList.add("fade-in-ani-ab");
                correctedOutputElement.classList.remove("fade-out-ani-ab");
            }, 300);
        }
    }
    handleInputChange()

</script>

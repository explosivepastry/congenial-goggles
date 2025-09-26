<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    TempData["CanCalibrate"] = true;
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);

    if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    {

        //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        Response.Cache.SetValidUntilExpires(false);
        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

%>


<%if (!string.IsNullOrEmpty(ViewBag.ErrorMessage))
    { %>
<div class="formBody" style="color: red;"><%:ViewBag.ErrorMessage %></div>
<%} %>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    {%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="returns" />
    <input type="hidden" value="<%: Model.LastDataMessage != null ? ((LightSensor_PPFD)Model.LastDataMessage.AppBase).Temperature.ToString() : "" %>" name="tempReading" id="tempReading" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
        { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>

    <%  if (Model.CanUpdate && Model.LastDataMessage != null)
        {%>


    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <b><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_139|Important", "Important")%></b>: <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_139|Perform these steps in the order indicated.", "Perform these steps in the order indicated.")%>
        </div>
    </div>

    <div class="row sensorEditForm">
        <div class="col d-flex flex-column">
            <br />
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_139|1. Zero Calibration: Zero's the sensor at the current light level. Sensor Zero should be performed before Calibration for best accuracy.", "1. Zero Calibration: Zero's the sensor at the current light level. Sensor Zero should be performed before Calibration for best accuracy.")%>
            <div class="clear"></div>
            <br />
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_139|2. Span Calibration: Calibrates sensor to current light level reading. The higher the value you can calibrate the more accurate the sensor will be.", "2. Span Calibration: Calibrates sensor to current light level reading. The higher the value you can calibrate the more accurate the sensor will be.")%>
            <div class="clear"></div>
        </div>
    </div>
    <div class="clear"></div>
    <br />
    <br />
    <div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <b><%: Html.TranslateTag("Select Calibration Type","Select Calibration Type")%></b>
    </div>
    <div class="col sensorEditFormInput">
        <select id="CalibrationType" name="CalibrationType" onchange="HideOrShow(this.value);" class="form-select">
            <option style="display: flex; justify-content: space-between;" value="" disabled selected>Select</option>
            <option style="display: flex; justify-content: space-between;" value="0">Zero Calibration</option>
            <option style="display: flex; justify-content: space-between;" value="1">Span Calibration</option>
        </select>
    </div>
    </div>

    <div style="clear: both;"></div>
    <br />

    <div id="CalibrationType_0" style="display: none; margin-top: 15px;">

        <b><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_139|Light Level Zero", "Light Level Zero")%></b>
        <div class="row sensorEditForm">
            <div class="col-sm-3 col-12">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_139|Observed Sensor Reading: (From sensor)", "Observed Sensor Reading: (From sensor)")%>
            </div>
            <div class="col sensorEditFormInput">
                <label><%: Model.LastDataMessage != null ? (Model.LastDataMessage.AppBase.PlotValue.ToDouble()).ToString("0.0") : "" %></label>
                <input type="hidden" name="lastmVReading" id="lastmVReading" value="<%: Model.LastDataMessage != null ? (((LightSensor_PPFD)Model.LastDataMessage.AppBase).RawVoltage * 1000).ToString() : "" %>" />
            </div>
        </div>
        <div style="clear: both;"></div>
        <br />
    </div>

    <div id="CalibrationType_1" style="display: none; margin-top: 15px;">
        <b><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_139|Light Level Calibration", "Light Level Calibration")%></b>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_139|Observed Sensor Reading: (From sensor)", "Observed Sensor Reading: (From sensor)")%>
            </div>
            <div class="col sensorEditFormInput">
                <label><%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.0") : "" %></label>
                <input type="hidden" name="lastReading" id="lastReportedReading" value="<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.0") : "" %>" />
            </div>
        </div>
        <div style="clear: both;"></div>
        <br />
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_139|Expected Reading", "Expected Reading")%>
            </div>
            <div class="col sensorEditFormInput">
                <input class="form-control" name="expectedReading" id="expectedReading" value="<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.0") : "" %>" required="required" />
            </div>
        </div>
        <div style="clear: both;"></div>
        <br />

    </div>
    <br />
    <div id="CalibrationType_btn"></div>
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>

<script>
    $(document).ready(function () {
        HideOrShow();

        $("CalibrationType").on("change", function () {
            HideOrShow();
        });

        $("#expectedReading").change(function () {
            if (!isANumber($("#expectedReading").val())) {
                $("#expectedReading").val(<%: Model.Calibration1 != 0 ? (Model.Calibration1).ToString("0.0") : "" %>);
            } else {

                if ($("#expectedReading").val() > 4000)
                    $("#expectedReading").val(4000);

                if ($("#expectedReading").val() < 100)
                    $("#expectedReading").val(100);

            }
        });

    });

    function HideOrShow() {

        if ($('#CalibrationType').val() == null) {
            $('#CalibrationType_0').hide();
            $('#CalibrationType_1').hide();
            $('#CalibrationType_btn').hide();
        }
        else if ($('#CalibrationType').val() == "0") {
            $('#CalibrationType_0').show();
            $('#CalibrationType_1').hide();
            $('#CalibrationType_btn').show();
        }
        else {
            $('#CalibrationType_0').hide();
            $('#CalibrationType_1').show();
            $('#CalibrationType_btn').show();
        }
    }

</script>

<%}
    else
    {
%>
<div class="formBody" style="font-weight: bold">
    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Calibration for this sensor is not available for edit until pending transaction is complete.", "Calibration for this sensor is not available for edit until pending transaction is complete.")%>
</div>
<div class="buttons">&nbsp; </div>
<%
    }
%>
<%}

    }%>
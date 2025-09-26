<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    string CustomLabel = LN2Level.GetCustomLabel(Model.SensorID);

    TempData["CanCalibrate"] = true;

    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<% if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    {%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
        { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>

    <%if (Model.LastDataMessage != null)
        {%>
    <%if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("AdvancedCalibration")))
        {%>
    <div class="form-group">

        <div class="bold col-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_152|1. Empty Calibration: Sets the most recent capacitive reading reported by sensor to the Empty Level.")%>
            <div class="clear"></div>
            <br />
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_152|2. Span Calibration: Uses the Actual Percent entered to compute the 100% point of the sensor and assumes the sensor is linear between the 0% and 100% points.")%> </br> <%:Html.TranslateTag("NOTE: Always zero the  sensor prior to span calibration. Recommended using a value above 25% for good results and above 50% for best results.")%>
            <div class="clear"></div>
        </div>
    </div>
    <div class="clear"></div>
    <hr />

    <%} %>

    <div class="formBody">
        <input type="radio" name="calType" id="calType1" value="2">
        <label for="calType1"><%:Html.TranslateTag("Empty Calibration") %></label>

        <input type="radio" name="calType" id="calType2" value="1">
        <label for="calType2"><%:Html.TranslateTag("Span Calibration") %></label>

        <div id="cal-1" style="display: none; margin-top: 15px;">

            <%-- Span Calibration--%>

            <%if (MonnitSession.CustomerCan("Support_Advanced"))
                {%>
            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_152|Current Calibration","Current Calibration")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input name="current" id="current" class="form-control" readonly="readonly" value="<%: Model.Calibration2 %>" />pF
                </div>
            </div>
            <%}%>

            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_152|Measured Reading","Measured Reading")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input id="lastReading" name="lastReading" class="form-control" type="text" readonly="readonly" value="<%: Model.LastDataMessage != null ? ((LN2Level)Model.LastDataMessage.AppBase).Level.ToString() : "" %>" /><%:CustomLabel %>
                </div>
            </div>

            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_152|Actual Reading","Actual Reading")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input name="span" id="span" class="form-control" value="<%: Model.LastDataMessage != null ? ((LN2Level)Model.LastDataMessage.AppBase).Level.ToDouble().ToString("0.00") : "" %>" required /><%:CustomLabel %>
                </div>
            </div>
        </div>

        <%-- Empty Calibration--%>

        <div id="emptyCal" class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_152|Measured Reading","Measured Reading")%>
            </div>
            <div class="col sensorEditFormInput">
                <input id="empty" name="empty" class="form-control" type="text" readonly="readonly" value="<%: Model.LastDataMessage != null ? ((LN2Level)Model.LastDataMessage.AppBase).Level.ToString() : "" %>" /><%:CustomLabel %>
            </div>
        </div>
    </div>
    <div class="clearfix"></div>
    </br>
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>

<script type="text/javascript">

    $(document).ready(function () {
        $("input[name=calType]").on("change", function () {

            $('#cal-1').hide();
            $('#cal-2').hide();
            $("#cal-" + $(this).val()).show();

            // Hide emptyCal div when Span Calibration is selected
            if ($(this).val() === "2") {
                $('#emptyCal').show();
            } else {
                $('#emptyCal').hide();
            }
        });
    });

    $('#lastReading').addClass('form-control');

    $("#lastReading").change(function () {
        if (!isANumber($("#lastReading").val()))
            $("#lastReading").val(<%: Model.LastDataMessage != null ? ((LN2Level)Model.LastDataMessage.AppBase).Level.ToString() : "" %>);
    });

<%--    var ZeroSpanSure = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Are you sure you want to set your calibration back to default?")%>";
    $("#ZeroOffset").click(function () {

        confirmCustom(ZeroSpanSure, function (result) {
            if (result == true) {
                $.post("/Overview/SensorCalibrate/<%:Model.SensorID %>", { calType: 2 }, function (data) {
                    $('#Form1').html(data);
                });
            }
        });
    })--%>
</script>
<%}
    }%>
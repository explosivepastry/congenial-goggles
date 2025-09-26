<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    TempData["CanCalibrate"] = true;

    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();
%>

<%: Html.ValidationSummary(false)%>
<%: Html.Hidden("id", Model.SensorID)%>

<%if (new Version(Model.FirmwareVersion) > new Version("2.0.0.0"))
    {
        //TempData["CanCalibrate"] = false;

        if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
        {
            string lastReadingVal = "0.000";
            if (Model.LastDataMessage != null)
                lastReadingVal = Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.000");
%>
<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="returns" />
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%--Observed Sensor Reading: (From sensor)  --%>
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_022|Observed Sensor Reading:", "Observed Sensor Reading:")%>
        </div>
        <div class="row sensorEditForm">
            <input name="observed" id="observed" class="form-control" readonly="readonly" value="<%: Model.LastDataMessage != null ? (lastReadingVal) : "" %>" />
            <%:Monnit.ZeroToTwentyMilliamp.GetLabel(Model.SensorID) %>
        </div>

    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3 calOptions">
            <%--Actual Input: (External reference value)--%>
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_022|Actual Input:","Actual Input:")%>
        </div>
        <div class="row sensorEditForm calOptions">
            <input name="actual" id="actual" class="form-control" <%=(lastReadingVal.ToDouble() == 0) ? "readonly='readonly'" : "" %> value="<%: Model.LastDataMessage != null ? (lastReadingVal) : "" %>" />
            <%:Monnit.ZeroToTwentyMilliamp.GetLabel(Model.SensorID) %>
        </div>

    </div>

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
        { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>

    <div class="row sensorEditForm">
        <div class="bold ">
            <%if (!Model.CanUpdate)
                {%>

            <span>

                <%if (MonnitSession.CustomerCan("Support_Advanced"))
                    { %>
                <span class="pendingEditIconLeft pendingsvg" style="cursor: pointer; vertical-align: sub;" onclick="clearDirtyFlags(<%: Model.SensorID %>);"><%=Html.GetThemedSVG("Pending_Update") %></span>
                <%}
                    else
                    { %>
                <span class="pendingEditIconLeft pendingsvg" style="vertical-align: sub;"><%=Html.GetThemedSVG("Pending_Update") %></span>
                <%} %>
            &nbsp;<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fields waiting to be written to sensor are not available for edit until transaction is complete.","Fields waiting to be written to sensor are not available for edit until transaction is complete.")%>

            </span>
            <%}  %>
        </div>

        <div class="bold dfac">
            <%if (lastReadingVal.ToDouble() > 0)
                {%>
            <button class="btn btn-primary btn-sm" type="button" id="calibrateButton" onclick="postForm($(this).closest('form'));" style="float: none; margin-right: 10px;" value="<%: Html.TranslateTag("Calibrate", "Calibrate")%> ">
                <%: Html.TranslateTag("Calibrate", "Calibrate")%>
            </button>
            <%}
            else
            { %>
            <button class="btn btn-primary btn-sm" type="button" id="dummyButton" onclick="showSimpleMessageModal("<%=Html.TranslateTag("Unable to Calibrate against actual or observed readings of 0 mA")%>"); return false;" style="float: none; margin-right: 10px;" value="<%: Html.TranslateTag("Calibrate", "Calibrate")%> ">
                <%: Html.TranslateTag("Calibrate", "Calibrate")%>
            </button>
            <%} %>
            <button type="button" id="DefaultsCalibrate1" class="btn btn-secondary btn-sm" style="float: none;" value="<%: Html.TranslateTag("Default","Default")%>">
                <%: Html.TranslateTag("Default","Default")%>
            </button>
            <div style="clear: both;"></div>
        </div>
    </div>


    <script>

        var defaultSure = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Are you sure you want to set your calibration back to default?")%>";
        $(function () {
            $('#DefaultsCalibrate1').on("click", function () {
                var SensorID = <%: Model.SensorID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#simpleCalibrate_<%: Model.SensorID%>').parent();

            if (confirm(defaultSure)) {
                $.post("/Overview/SetDefaultCalibration", { id: SensorID, url: returnUrl }, function (data) {
                    pID.html(data);
                });
            }
        });
    });

        function clearDirtyFlags(sensorID) {
            $.post("/Overview/SetSensorActive/" + sensorID, function (data) {
                window.location.href = window.location.href;

            });
        }
    </script>

</form>

<div class="buttons">&nbsp; </div>
<%}
    }%>

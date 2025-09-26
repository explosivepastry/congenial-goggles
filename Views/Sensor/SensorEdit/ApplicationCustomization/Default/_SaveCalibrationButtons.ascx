<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="form-group">
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
            &nbsp;<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons|Fields waiting to be written to sensor are not available for edit until transaction is complete.","Fields waiting to be written to sensor are not available for edit until transaction is complete.")%>
        </span>
        <%}  %>
    </div>

    <div class="text-end">
        <button type="button" id="DefaultsCalibrate1" class="btn btn-secondary" style="float: none;" value="<%: Html.TranslateTag("Default","Default")%>">
            <%: Html.TranslateTag("Default","Default")%>
        </button>

        <button class="btn btn-primary" type="button" id="calibrateButton" onclick="postForm($(this).closest('form'));" style="float: none;" value="<%: Html.TranslateTag("Calibrate","Calibrate")%>">
            <%: Html.TranslateTag("Calibrate","Calibrate")%>
        </button>

        <div style="clear: both;"></div>
    </div>
</div>

<script type="text/javascript">

    var defaultSure = "<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons|Are you sure you want to set your calibration back to default?","Are you sure you want to set your calibration back to default?")%>";

    $(function () {
        $('#DefaultsCalibrate1').on("click", function () {
            var SensorID = <%: Model.SensorID%>;

            let values = {};
            values.url = `/Overview/SetDefaultCalibration/${SensorID}`;
			values.partialTag = $(this).closest('form'); // $('#Form1');
            values.text = defaultSure;
            openConfirm(values);
        });
    });

    function clearDirtyFlags(sensorID) {
        $.post("/Overview/SetSensorActive/" + sensorID, function (data) {
            window.location.href = window.location.href;
        });
    }

</script>

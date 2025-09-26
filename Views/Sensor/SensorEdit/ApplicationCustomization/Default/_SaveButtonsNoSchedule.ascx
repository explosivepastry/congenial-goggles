<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="form-group">
    <div class="bold col-md-9 col-sm-9 col-xs-12" style="padding-bottom: 4px;">
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
            &nbsp;<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtonsNoSchedule|Fields waiting to be written to sensor are not available for edit until transaction is complete.","Fields waiting to be written to sensor are not available for edit until transaction is complete.")%>
        </span>
        <%}  %>
    </div>

    <div class="col-12 dfac justify-content-end">
        <button class="btn btn-secondary" type="button" id="DefaultsCalibrate" <%=Model.CanUpdate ? "" : "disabled" %> value="<%: Html.TranslateTag("Default","Default")%>">
            <%: Html.TranslateTag("Default","Default")%>
        </button>
        &nbsp;

        <button class="btn btn-primary" type="button" <%=Model.CanUpdate ? "" : "disabled" %> onclick="$(this).hide();$('#saving').show();checkForm(<%:Model.SensorID %>, <%:Model.PowerSource.MinimumRecommendedHeartbeat%>, <%:Model.ApplicationID%>);" value="<%: Html.TranslateTag("Save","Save")%>">
            <%: Html.TranslateTag("Save","Save")%>
        </button>

        <button class="btn btn-primary" id="saving" style="display:none;" type="button" disabled >
            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" ></span>
            <%: Html.TranslateTag("Saving...","Saving...")%>
        </button>

        <div style="clear: both;"></div>
    </div>
</div>

<script type="text/javascript">

    var DefaultYouSure = "<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtonsNoSchedule|Are you sure you want to reset this sensor to defaults?","Are you sure you want to reset this sensor to defaults ?")%>";

    $(function () {
        $('#DefaultsCalibrate').on("click", function () {
            var SensorID = <%: Model.SensorID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();
            if (confirm(DefaultYouSure)) {
                $.get('/Overview/SensorDefault/' + SensorID, function (result) {
                    pID.html(result);

                    setTimeout(function () {
                        window.location.href = window.location.href;
                    }, 2000);
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

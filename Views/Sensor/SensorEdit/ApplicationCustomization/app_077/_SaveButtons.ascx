<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row sensorEditForm">
    <div class="col-12">
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
    <div class="d-flex justify-content-end">
        <input class="btn btn-secondary" type="button" value="Reset Count" id="ResetDataLog" />
        <input type="button" id="DefaultsCalibrate" <%=Model.CanUpdate ? "" : "disabled" %> class="btn btn-dark mx-2" value="<%: Html.TranslateTag("Default", "Default")%>" />
        <input class="btn btn-primary" type="button" <%=Model.CanUpdate ? "" : "disabled" %> onclick="$(this).hide();$('#saving').show();checkForm(<%:Model.SensorID %>, <%:Model.PowerSource.MinimumRecommendedHeartbeat%>, <%:Model.ApplicationID%>);" value="<%: Html.TranslateTag("Save","Save")%>" />
        <button class="btn btn-primary" id="saving" style="display:none;" type="button" disabled >
            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" ></span>
            Saving...
        </button>
        <div style="clear: both;"></div>
    </div>
</div>

<script>
    var DefaultYouSure = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Are you sure you want to reset this sensor to defaults?","Are you sure you want to reset this sensor to defaults?")%>";
    var counterSure = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Are you sure you want to reset the counter on this sensor?")%>";
    $(function () {
        $('#DefaultsCalibrate').on("click", function () {

            var SensorID = <%: Model.SensorID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();
            if (confirm(DefaultYouSure)) {

                $.get('/Overview/SensorDefault/' + SensorID, function (result) {

                    setTimeout(function () {
                        window.location.href = window.location.href;
                    }, 2000);
                });
            }
        });

        $('#ResetDataLog').on("click", function () {
            var SensorID = <%: Model.SensorID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();
            if (confirm(counterSure)) {

                $.post('/Overview/SensorCalibrate/', { id: SensorID, url: returnUrl }, function (result) {

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

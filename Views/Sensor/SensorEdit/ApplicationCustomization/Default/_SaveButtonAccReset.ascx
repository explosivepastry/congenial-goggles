<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div class="form-group" id="hideMyDiv">
    <div style="display: none;">
        <input type="number" name="id" value="<%:Model.SensorID %>" />
    </div>

    <div class="accumDefaultSave" style="align-items:center; justify-content:space-between;">
        <%if (!Model.CanUpdate)
            {%>
        <div style="display:flex; align-items:center;">
            <%if (MonnitSession.CustomerCan("Support_Advanced"))
                { %>
            <span class="pendingEditIconLeft pendingsvg" style="cursor: pointer; vertical-align: sub;" onclick="clearDirtyFlags(<%: Model.SensorID %>);"><%=Html.GetThemedSVG("Pending_Update") %></span>
            <%}
                else
                { %>
            <span class="pendingEditIconLeft pendingsvg" style="vertical-align: sub;"><%=Html.GetThemedSVG("Pending_Update") %></span>
            <%} %>
            &nbsp;<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtonAccReset|Fields waiting to be written to sensor are not available for edit until transaction is complete.","Fields waiting to be written to sensor are not available for edit until transaction is complete.")%>
        </div>
        <% } %>
        <%--        <button class="btn btn-secondary " type="button" id="ResetAccumulator" <%=Model.CanUpdate ? "" : "disabled" %> value="<%: Html.TranslateTag("Reset Accumulator","Reset Accumulator")%>" style="margin-right: .5rem;">
            <%: Html.TranslateTag("Reset Accumulator","Reset Accumulator")%>
        </button>--%>
        <div style="clear: both;"></div>
        <button class="btn btn-primary" type="button" <%=Model.CanUpdate ? "" : "disabled" %> onclick="$(this).hide();$('#saving').show();checkForm(<%:Model.SensorID %>, <%:Model.PowerSource.MinimumRecommendedHeartbeat%>, <%:Model.ApplicationID%>);" value="<%: Html.TranslateTag("Save","Save")%>">
            <%: Html.TranslateTag("Save","Save")%>
        </button>
        <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
            <%: Html.TranslateTag("Saving...","Saving...")%>
        </button>
    </div>
</div>

<script type="text/javascript">

    var DefaultYouSure = "<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtonAccReset|Are you sure you want to reset this sensor to defaults?","Are you sure you want to reset this sensor to defaults?")%>";
    var SureCounter = "<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtonAccReset|Are you sure you want to reset the counter on this sensor?","Are you sure you want to reset the counter on this sensor?")%>";
    var SureDefault = "<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtonAccReset|Are you sure you want to reset this sensor to defaults?","Are you sure you want to reset this sensor to defaults?")%>";

    $(function () {
        var SensorID = <%: Model.SensorID%>;
        var returnUrl = $('#returns').val();
        var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();

        $('#ResetAccumulator').on("click", function () {
            var SensorID = <%: Model.SensorID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();
            if (confirm(SureCounter)) {
                $.post('/Overview/SensorResetCounter', { id: SensorID, url: returnUrl, acc: "5" }, function (result) {
                    pID.html(result);
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

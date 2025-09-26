<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="form-group">
    <div style="display: flex; align-items: center; width: 100%; justify-content:<%=!Model.CanUpdate ? "space-between" : "flex-end" %>;">
        <%if (!Model.CanUpdate)
            {%>
        <div style="display: flex; align-items: center;">

            <%if (MonnitSession.CustomerCan("Support_Advanced"))
                { %>
            <span class="pendingEditIconLeft pendingsvg" style="cursor: pointer; vertical-align: sub;" onclick="clearDirtyFlags(<%: Model.SensorID %>);"><%=Html.GetThemedSVG("Pending_Update") %></span>
            <%}
                else
                { %>
            <span class="pendingEditIconLeft pendingsvg" style="vertical-align: sub;"><%=Html.GetThemedSVG("Pending_Update") %></span>
            <%} %>
            &nbsp;<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fields waiting to be written to sensor are not available for edit until transaction is complete.","Fields waiting to be written to sensor are not available for edit until transaction is complete.")%>
        </div>
        <%}  %>
        <button class="btn btn-primary" type="button" <%=Model.CanUpdate ? "" : "disabled" %> onclick="checkForm(<%:Model.SensorID %>, <%:Model.PowerSource.MinimumRecommendedHeartbeat%>, <%:Model.ApplicationID%>);" value="<%: Html.TranslateTag("Save","Save")%>">
            <%: Html.TranslateTag("Save","Save")%>
        </button>
    </div>
</div>

<script>
    var DefaultYouSure = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Are you sure you want to reset this sensor to defaults?","Are you sure you want to reset this sensor to defaults?")%>";

    $(function () {

        $('#DefaultsCalibrate').on("click", function () {

            var SensorID = <%: Model.SensorID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();
            if (confirm(DefaultYouSure)) {

                $.get('/Overview/SensorDefault/' + SensorID, function (result) {
                    pID.html(result);
                    pID.append(`<br /> <div id="loadingGIF" class="text-center" style="display: none;">
                        < div class= "spinner-border text-primary" style = "margin-top: 50px;" role = "status" >
                        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div> `);

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

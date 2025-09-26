<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%--<div id="button" class="buttons">
    
    <%if (!Model.CanUpdate) {%>
        <span>fields waiting to be written to sensor are not available for edit until transaction is complete.</span>
    <%} else {
        if(MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")) && MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Configure_Multiple"))){ %>
            <span style="float:left; margin: 15px 0px 0px 25px;"><a style="margin: -3px;" href="/Sensor/MultiEdit?id=<%:Model.SensorID %>">Use these settings for other sensors</a></span>
    <%  }
      } %>
    <button class="gen-btn" type="button" onclick="checkForm(<%:Model.SensorID %>, <%:Model.PowerSource.MinimumRecommendedHeartbeat%>, <%:Model.ApplicationID%>);" value="Save">Save</button>
    <button type="button" id="DefaultsCalibrate" class="btn btn-secondary" style="float: none;" value="Default">Default</button>--%>
<%--<button type="button" id="ResetAccumulator" class="btn btn-secondary" style="float: none;" value="Reset Accumulator">Reset Accumulator</button>--%>

<button class="btn btn-secondary mx-2" type="button" id="ResetAccumulator" <%=Model.CanUpdate ? "" : "disabled" %> value="<%: Html.TranslateTag("Reset Accumulator", "Reset Accumulator")%>">
    <%: Html.TranslateTag("Reset Accumulator", "Reset Accumulator")%>
</button>
<%--    <div style="clear: both;"></div>
</div>--%>
<script>
    var counterSure = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Are you sure you want to reset the counter on this sensor?")%>";
    $(function () {
<%--        $('#DefaultsCalibrate').on("click", function () {

            var SensorID = <%: Model.SensorID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();
            if (confirm("Are you sure you want to reset this sensor to defaults?")) {

                $.post('/Sensor/Default/' + SensorID, function (result) {
                    pID.html(result);
                });
            }
        });--%>

        $('#ResetAccumulator').on("click", function () {

            var SensorID = <%: Model.SensorID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();
            if (confirm(counterSure)) {

                $.post('/Sensor/resetCounter', { id: SensorID, url: returnUrl, acc: "6" }, function (result) {
                    pID.html(result);
                });
            }
        });
    });
</script>

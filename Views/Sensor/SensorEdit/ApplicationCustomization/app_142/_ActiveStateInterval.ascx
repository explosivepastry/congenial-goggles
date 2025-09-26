<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%if (Model.ApplicationID != 23 || Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) > new Version("2.2.0.0") || Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) >= new Version("1.2.0.3"))
            { %>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Heartbeat","Aware State Heartbeat")%>
        <%}
            else if (Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) < new Version("2.2.0.0") || Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) <= new Version("1.2.0.3"))
            { %>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Time Before No Motion Rearm","Time Before No Motion Rearm")%>
        <%} %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled"  %> name="ActiveStateInterval" id="ActiveStateInterval" value="<%=Model.ActiveStateInterval %>" />
        <a id="activeNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.ActiveStateInterval)%>
    </div>
</div>

<script type="text/javascript">
    var awareHBString = " <%=Html.TranslateTag("Minutes") %>";
    var ActiveStateInterval_array = [120, 240, 360, 720];
    var minReportInterval = <%=MonnitSession.CurrentCustomer.Account.MinHeartBeat()%>;
    var minActiveInterval = minReportInterval;

    if (minReportInterval == 10) {
        var ActiveStateInterval_array = [10, 20, 30, 60, 120, 240, 360, 720];
    }

    if (minReportInterval <= 1) {
        var ActiveStateInterval_array = [1, 10, 20, 30, 60, 120, 240, 360, 720];
    }

     $(function () {
               <% if (Model.CanUpdate)
    { %>

         createSpinnerModal("activeNum", awareHBString, "ActiveStateInterval", ActiveStateInterval_array);

        <%}%>

        $("#ActiveStateInterval").change(function () {
            if (isANumber($("#ActiveStateInterval").val())) {
                //Check if less than min
                if ($("#ActiveStateInterval").val() < minReportInterval)
                    $("#ActiveStateInterval").val(minReportInterval);

                //Check if greater than max
                if ($("#ActiveStateInterval").val() > 720)
                    $("#ActiveStateInterval").val(720);

                if (Number($('#ActiveStateInterval').val()) > Number($('#ReportInterval').val()))
                    $('#ReportInterval').val(Number($('#ActiveStateInterval').val()));

                if (Number($("#ActiveStateInterval").val()) * 60 < Number($("#MeasurementInterval_Manual").val()))
                    $("#MeasurementInterval_Manual").val(Math.round((Number($("#ActiveStateInterval").val()) * 60) * 100) / 100);
            }
        })
    })

    function showAwareCustom(hbVal) {
        $('#ActiveStateInterval').hide();
        if (hbVal == 'custom') {
            $('#ActiveStateInterval').show();
        }
    }

</script>





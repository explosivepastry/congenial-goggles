<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%if (Model.ApplicationID != 23 || Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) > new Version("1.2.0.3"))
            { %>
        <%: Html.TranslateTag("Heartbeat Interval","Heartbeat Interval")%> (<%: Html.TranslateTag("Minutes","Minutes")%>)
        <%}
            else if (Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) < new Version("2.2.0.0") || Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) <= new Version("1.2.0.3"))
            { %>
    Time Before Motion Rearm
    <%} %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled"  %> name="ReportInterval" id="ReportInterval" value="<%=Model.ReportInterval %>" />
        <a id="reportNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.ReportInterval)%>
    </div>
</div>

<script type="text/javascript">

    var reportInterval_array = [120, 240, 360, 720];
    var minReportInterval = <%=MonnitSession.CurrentCustomer.Account.MinHeartBeat()%>;

    if (minReportInterval == 10) {
        var reportInterval_array = [1, 2, 10, 20, 30, 60, 120, 240, 360, 720];
    }

    if (minReportInterval <= 1) {
        var reportInterval_array = [1, 2, 10, 20, 30, 60, 120, 240, 360, 720];
    }

    $(function () {

          <% if (Model.CanUpdate)
    { %>
      createSpinnerModal("reportNum", "Heartbeat (Minutes)", "ReportInterval", reportInterval_array);

        <%}%>

        $("#ReportInterval").change(function () {
            if (isANumber($("#ReportInterval").val())) {
                //Check if less than min
                if ($("#ReportInterval").val() < minReportInterval)
                    $("#ReportInterval").val(minReportInterval);

                //Check if greater than max
                if ($("#ReportInterval").val() > 720)
                    $("#ReportInterval").val(720);

                <%  if (MonnitSession.AccountCan("sensor_advanced_edit"))
    {%>

                if (Number($('#ReportInterval').val()) < Number($('#Hysteresis_Manual').val())) {
                    $('#Hysteresis_Manual').val(Number($('#ReportInterval').val()));

                    //max = Number($('#ReportInterval').val());                    
                }
                setAproxTime();

                <%}%>

            }
            else {
                $("#ReportInterval").val(<%: Model.ReportInterval%>);
            }
        });
    });

</script>

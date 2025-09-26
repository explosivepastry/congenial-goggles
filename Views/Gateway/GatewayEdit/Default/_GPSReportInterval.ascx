<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GPSReportInterval|Location Heartbeat Minutes","Location Heartbeat Minutes")%> (<%:Html.TranslateTag("default","default") %>: <%:Model.GatewayType.DefaultGPSReportInterval%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" id="GPSReportInterval" name="GPSReportInterval" value="<%=  Model.GPSReportInterval%>" style="width:50%;"/>
          <a id="gpsNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.GPSReportInterval)%>
    </div>
</div>

<script type="text/javascript">

    var mobiLabel = '<%: Html.TranslateTag("Minutes","Minutes")%>';

    var gpsInterval_array = [0, 120, 240, 360, 720];

    var minReportInterval = <%=MonnitSession.CurrentCustomer.Account.MinHeartBeat()%>;

    if (minReportInterval == 10) {
        var gpsInterval_array = [0, 10, 20, 30, 60, 120, 240, 360, 720];
    }

    if (minReportInterval <= 1) {
        var gpsInterval_array = [0, 1, 10, 20, 30, 60, 120, 240, 360, 720];
    }

    $(document).ready(function () {

        $('#GPSReportInterval').change(function () {
            if ($('#GPSReportInterval').val() < 0)
                $('#GPSReportInterval').val(0);

            if ($('#GPSReportInterval').val() != 0 && $('#GPSReportInterval').val() < minReportInterval)
                $('#GPSReportInterval').val(minReportInterval);

            if ($('#GPSReportInterval').val() > 720)
                $('#GPSReportInterval').val(720);
        });
        createSpinnerModal("gpsNum", mobiLabel, "GPSReportInterval", gpsInterval_array);
    });

</script>
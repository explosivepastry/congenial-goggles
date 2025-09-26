<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string measurementInterval = "";
    measurementInterval = (ThreePhase500AmpMeter.GetMeasurementInterval(Model)).ToString("#0.###");
%>

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
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled"  %> name="ActiveStateInterval" id="ActiveStateInterval" value="<%=Model.ActiveStateInterval %>" />
        <a id="activeNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.ActiveStateInterval)%>
    </div>
</div>



<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Measurement Interval","Measurement Interval")%> (<%=Html.TranslateTag("Minutes") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MeasurementInterval_Manual" id="MeasurementInterval_Manual" value="<%=measurementInterval %>" />
        <a id="measure" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<script type="text/javascript">
    var awareHBString = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Heartbeat","Aware State Heartbeat")%> (<%=Html.TranslateTag("Minutes") %>)";
    var measurementIntervalString = "<%: Html.TranslateTag("Measurement Interval","Measurement Interval")%> (<%=Html.TranslateTag("Minutes") %>)";
    var minutesString = "<%=Html.TranslateTag("Minutes") %>";
    var ActiveStateInterval_array = [120, 240, 360, 720];
    var minReportInterval = <%=MonnitSession.CurrentCustomer.Account.MinHeartBeat()%>;
    var minActiveInterval = minReportInterval;

    if (minReportInterval == 10) {
        var ActiveStateInterval_array = [10, 20, 30, 60, 120, 240, 360, 720];
    }

    if (minReportInterval <= 1) {
        var ActiveStateInterval_array = [1, 10, 20, 30, 60, 120, 240, 360, 720];
    }
    $('#MeasurementInterval_Manual').addClass("editField editFieldMedium");
    var measurementInterval_array = [1, 2, 5, 10, 20, 30, 60, 120, 240, 360, 720];


    $(function () {

               <% if (Model.CanUpdate)
    { %>
        createSpinnerModal("activeNum", "Minutes", "ActiveStateInterval", ActiveStateInterval_array);
        createSpinnerModal("measure", "Minutes", "MeasurementInterval_Manual", measurementInterval_array);
        <%}%>



        $("#ActiveStateInterval").change(function () {
            if (isANumber($("#ActiveStateInterval").val())) {
                //Check if less than min
                if ($("#ActiveStateInterval").val() < minReportInterval)
                    $("#ActiveStateInterval").val(minReportInterval);

                //Check if greater than max
                if ($("#ActiveStateInterval").val() > 720)
                    $("#ActiveStateInterval").val(720);

                if (Number($('#ActiveStateInterval').val()) > Number($('#ReportInterval').val())) {
                    $('#ReportInterval').val(Number($('#ActiveStateInterval').val()));
                }

                if (Number($("#MeasurementInterval_Manual").val()) > Number($('#ActiveStateInterval').val())) {
                    $("#MeasurementInterval_Manual").val(Number($('#ActiveStateInterval').val()));
                }
            }
            else {
                $("#ActiveStateInterval").val(<%: Model.ActiveStateInterval%>);
            }
        });

        $("#MeasurementInterval_Manual").addClass('editField editFieldMedium');
        $("#MeasurementInterval_Manual").change(function () {
            if (isANumber($("#MeasurementInterval_Manual").val())) {
                //Check if less than min
                if ($("#MeasurementInterval_Manual").val() < 0.017)
                    $("#MeasurementInterval_Manual").val(0.017);

                //Check if greater than max
                if ($("#MeasurementInterval_Manual").val() > 720)
                    $("#MeasurementInterval_Manual").val(720);

                if (Number($("#MeasurementInterval_Manual").val()) > Number($('#ActiveStateInterval').val())) {
                    $("#MeasurementInterval_Manual").val(Number($('#ActiveStateInterval').val()));
                }

            }
            else {
                $("#MeasurementInterval_Manual").val(<%: measurementInterval%>);
            }
        });
    });

</script>





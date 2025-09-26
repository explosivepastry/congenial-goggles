<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">

        <%: Html.TranslateTag("Heartbeat Interval","Heartbeat Interval")%> (<%: Html.Label("Minutes") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input  type="hidden" name="ActiveStateInterval" id="ActiveStateInterval" value="<%=Model.ReportInterval %>" />
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled"  %> name="ReportInterval" id="ReportInterval" value="<%=Model.ReportInterval %>" />
        <a id="reportNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.ReportInterval)%>
    </div>
</div>



<script type="text/javascript">
    var reportInterval_array = [120, 240, 360, 720];
    var minReportInterval = <%=MonnitSession.CurrentCustomer.Account.MinHeartBeat()%>;

    if (minReportInterval == 10) {
        var reportInterval_array = [10, 20, 30, 60, 120, 240, 360, 720];
    }

    if (minReportInterval <= 1) {
        var reportInterval_array = [1, 10, 20, 30, 60, 120, 240, 360, 720];
    }

    $(function () {


          <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("reportNum", 'Minutes', "ReportInterval", reportInterval_array);

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

                if (Number($('#ReportInterval').val()) < (Number($('#Calibration3_Manual').val()) / 60)) {
                    $('#Calibration3_Manual').val((Number($('#ReportInterval').val() * 60)));

                }
                setAproxTime();
                <%}%>

            }
            else {
                $("#ReportInterval").val(<%: Model.ReportInterval%>);
            }

            $('#ActiveStateInterval').val($('#ReportInterval').val());
        });

    });




</script>

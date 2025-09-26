<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Observation Mode Heartbeat Interval","Observation Mode Heartbeat Interval")%> (<%: Html.TranslateTag("Minutes","Minutes")%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled"  %> name="SessionReportInterval" id="SessionReportInterval" value="<%=Model.Calibration4%>" />
        <a id="sessionReportNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration4)%>
    </div>
</div>


<script type="text/javascript">

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
        $('#SessionReportInterval').addClass("editField editFieldMedium");

               <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("sessionReportNum", "Minutes", "SessionReportInterval", ActiveStateInterval_array);

        <%}%>

        $("#SessionReportInterval").change(function () {
            if (isANumber($("#SessionReportInterval").val())) {
                //Check if less than min
                if ($("#SessionReportInterval").val() < 1)
                    $("#SessionReportInterval").val(1);

                //Check if greater than max
                if ($("#SessionReportInterval").val() > 720)
                    $("#SessionReportInterval").val(720);
            }
            else {
                $("#SessionReportInterval").val(<%: Model.Calibration4%>);
            }
        });
    });

    function showAwareCustom(hbVal) {
        $('#ActiveStateInterval').hide();

        if (hbVal == 'custom') {
            $('#ActiveStateInterval').show();
        }
    }

</script>





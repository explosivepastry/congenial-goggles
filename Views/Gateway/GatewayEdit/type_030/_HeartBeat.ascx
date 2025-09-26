<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_HeartBeat|Heartbeat Minutes (default","Heartbeat Minutes (default")%>: <%:Model.GatewayType.DefaultReportInterval%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" name="ReportInterval" id="ReportInterval" value="<%=Model.ReportInterval %>" />
        <a id="reportNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
         <%: Html.ValidationMessageFor(model => model.ReportInterval)%>
    </div>
</div>


<script type="text/javascript">

    var mobiLabel = '<%: Html.TranslateTag("Minutes","Minutes")%>';

    var reportInterval_array = [1, 5, 10, 20, 30, 60];
    $(document).ready(function () {

        $('#ReportInterval').change(function () {
            if ($('#ReportInterval').val() < 0)
                $('#ReportInterval').val(0);

            if ($('#ReportInterval').val() > 60)
                $('#ReportInterval').val(60);
        });

        createSpinnerModal("reportNum", mobiLabel, "ReportInterval", reportInterval_array);

    });

</script>

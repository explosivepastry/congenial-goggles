<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Active Mode Report Interval","Active Mode Report Interval")%>  (<%: Html.Label("Seconds") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="ActiveReportInterval" id="ActiveReportInterval" value="<%=SootBlower2.GetActiveReportInterval(Model) %>" />

        <a id="activeIntervalNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>



<script type="text/javascript">
    var assessment_array = [1, 2, 5, 10, 30, 60, 120];

    $(function () {

        <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("activeIntervalNum", "Seconds", "ActiveReportInterval", assessment_array);

        <%}%>
    });

    $("#ActiveReportInterval").change(function () {
        if (isANumber($("#ActiveReportInterval").val())) {
            if ($("#ActiveReportInterval").val() < 1)
                $("#ActiveReportInterval").val(1);
            if ($("#ActiveReportInterval").val() > 120)
                $("#ActiveReportInterval").val(120)

            if ((Number($('#ActiveReportInterval').val()) / 60) > Number($('#ReportInterval').val())) {
                $('#ReportInterval').val((Number($('#ActiveReportInterval').val()) / 60));
            }
        }
        else
            $("#ActiveReportInterval").val(<%: Model.Calibration3%>);
    });




</script>

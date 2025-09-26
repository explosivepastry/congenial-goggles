<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Active Mode Report Interval","Active Mode Report Interval")%>  (<%: Html.Label("Seconds") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Calibration3_Manual" id="Calibration3_Manual" value="<%=Model.Calibration3 %>" />
        <a  id="activeIntervalNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<script type="text/javascript">
    var assessment_array = [1, 2, 5, 10, 30, 60, 120];

    $(function () {

        <% if (Model.CanUpdate)
           { %>

        createSpinnerModal("activeIntervalNum", "Seconds", "Calibration3_Manual", assessment_array);

        });

        <%}%>

        $("#Calibration3_Manual").change(function () {
            if(isANumber($("#Calibration3_Manual").val())){
                if ($("#Calibration3_Manual").val() < 1)
                    $("#Calibration3_Manual").val(1);
                if ($("#Calibration3_Manual").val() > 120)
                    $("#Calibration3_Manual").val(120)

                if ((Number($('#Calibration3_Manual').val()) / 60) > Number($('#ReportInterval').val())) {
                    $('#ReportInterval').val((Number($('#Calibration3_Manual').val()) / 60));
                }
            }
            else
                $("#Calibration3_Manual").val(<%: Model.Calibration3%>);
        });

</script>

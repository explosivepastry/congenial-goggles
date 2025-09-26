<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_MeasurementsPerTransmission|Assessments per Heartbeat","Assessments per Heartbeat")%>
    </div>

    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" <%=Model.CanUpdate ? "" : "disabled" %> id="MeasurementsPerTransmission" name="MeasurementsPerTransmission" value="<%=Model.MeasurementsPerTransmission %>" />
        <a id="mptNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>&nbsp;<span id="AproxAssessmentTime" style="font-size: 11px;"></span>
        <%: Html.ValidationMessageFor(model => model.MeasurementsPerTransmission)%>
    </div>
</div>

<script type="text/javascript">
    $(function () {
        var assessment_array = [1, 2, 5, 10, 25, 100, 200, 250];

        <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("mptNum", 'Assessments', "MeasurementsPerTransmission", assessment_array);

        <%}%>

        function getAssessmentIndex() {
            var retval = 0;
            $.each(assessment_array, function (index, value) {
                if (value <= $("#MeasurementsPerTransmission").val())
                    retval = index;
            });

            return retval;
        }
        setAproxTime();

        function setVal(number) {
            $("#MeasurementsPerTransmission").val(number);
        }

        $("#MeasurementsPerTransmission").change(function () {
            if (isANumber($("#MeasurementsPerTransmission").val())) {
                if ($("#MeasurementsPerTransmission").val() < 1)
                    $("#MeasurementsPerTransmission").val(1);
                if ($("#MeasurementsPerTransmission").val() > 250)
                    $("#MeasurementsPerTransmission").val(250)

                setAproxTime();
            }
            else {
                $("#MeasurementsPerTransmission").val($("#MeasurementsPerTransmission").val());
                setAproxTime();
            }
        });
    });

</script>


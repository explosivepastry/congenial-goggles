<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<tr>
    <td>Assessments per Heartbeat</td>
    <td>
        <%: Html.TextBoxFor(model => model.MeasurementsPerTransmission, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <span id="AproxAssessmentTime" style="font-size: 11px;"></span>
        <%: Html.ValidationMessageFor(model => model.MeasurementsPerTransmission)%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="How many times between heartbeats a sensor will check its measurements against its thresholds to determine whether it will enter the Aware State." src="<%:Html.GetThemedContent("/images/help.png")%>" />
        <%if (new Version(Model.FirmwareVersion) < new Version("2.5.2.0"))
          { %>
        <img alt="warning" class="helpIcon" title="Setting assessment greater than one will cause this sensor to not self elect follow a gateway to a new channel.  If you reform your gateway you will need to power cycle the sensor for it to link on a new channel." src="<%:Html.GetThemedContent("/images/Warning.png")%>" />
        <%} %>
    </td>
</tr>
<tr>
    <td></td>
    <td colspan="2">
        <div id="Assessment_Slider"></div>
    </td>
</tr>


<script type="text/javascript">
    var assessment_array = [1, 2, 5, 10, 25, 100, 200, 250];
    function getAssessmentIndex() {
        var retval = 0;
        $.each(assessment_array, function (index, value) {
            if (value <= $("#MeasurementsPerTransmission").val())
                retval = index;
        });

        return retval;
    }
    setAproxTime();

    $('#Assessment_Slider').slider({
        value: getAssessmentIndex(),
        min: 0,
        max: assessment_array.length - 1,
                                    <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
        slide: function (event, ui) {
            //update the amount by fetching the value in the value_array at index ui.value
            $('#MeasurementsPerTransmission').val(assessment_array[ui.value]);
            setAproxTime();
        }
    });
    $("#MeasurementsPerTransmission").addClass('editField editFieldSmall');
    $("#MeasurementsPerTransmission").change(function () {
        if (isANumber($("#MeasurementsPerTransmission").val())) {
            if ($("#MeasurementsPerTransmission").val() < 1)
                $("#MeasurementsPerTransmission").val(1);
            if ($("#MeasurementsPerTransmission").val() > 250)
                $("#MeasurementsPerTransmission").val(250)
            $('#Assessment_Slider').slider("value", getAssessmentIndex());
            setAproxTime();
        }
        else {
            $("#MeasurementsPerTransmission").val($("#MeasurementsPerTransmission").val());
            $('#Assessment_Slider').slider("value", getAssessmentIndex());
            setAproxTime();
        }
    });
</script>


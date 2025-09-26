<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<%
    
    var Nm = Gas_CO.MeasrementsPerHeartbeat(Model);
    
       %>
<tr>
    <td>Number of Mesurement Per Sample</td>
    <td>
         <input type="text" name="MeasurementsPerTransmission" id="MeasurementsPerTransmission" />
    </td>
    <td>
        <img alt="help" class="helpIcon" title="How many times between heartbeats a sensor will check its measurements against its thresholds to determine whether it will enter the Aware State." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
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
        else
            $("#MeasurementsPerTransmission").val(1);
    });
</script>



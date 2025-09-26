<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%if (!Model.IsWiFiSensor)
  {%>

<tr>
    <td style="width: 250px;">Number of Mesurements in a sampling window</td>
    <td>
        <%: Html.TextBox("TransmitOffsetChk", (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%--<input type="checkbox" id="TransmitOffsetChk" name="TransmitOffsetChk" <%:Model.TransmitOffset > 0 ? "checked='checked'" : ""%> />--%>
        <div style="display: none;"><%: Html.TextBoxFor(model => model.TransmitOffset, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="In small sensor networks, the sensors can be set to synchronize their communications. The default setting off allows the sensors to randomize their communications therefore maximizing communication robustness.  Setting this will synchronize the communication of the sensors. <br /><br />Example: Turning synchronization on all sensors indicates that they all try to communicate within 5 seconds of each other, recommended to limit this to gateways with 10 or fewer sensors." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<tr>
    <td></td>
    <td colspan="2">
        <div id="Assessment_Slider1"></div>
    </td>
</tr>
<script type="text/javascript">
    var assessment_array1 = [1, 2, 5, 10, 25, 100, 200, 255];
    function getAssessmentIndex1() {
        var retval = 0;
        $.each(assessment_array1, function (index, value) {
            if (value <= $("#TransmitOffsetChk").val())
                retval = index;
        });

        return retval;
    }
    setAproxTime();

    $('#Assessment_Slider1').slider({
        value: getAssessmentIndex1(),
        min: 0,
        max: assessment_array1.length - 1,
                                    <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
        slide: function (event, ui) {
            //update the amount by fetching the value in the value_array at index ui.value
            $('#TransmitOffsetChk').val(assessment_array1[ui.value]);
            setAproxTime();
        }
    });
    $("#TransmitOffsetChk").addClass('editField editFieldSmall');
    $("#TransmitOffsetChk").change(function () {
        if ($("#TransmitOffsetChk").val() < 1)
            $("#TransmitOffsetChk").val(1);
        if ($("#TransmitOffsetChk").val() > 255)
            $("#MeasurementsPerTransmission").val(255)
        $('#Assessment_Slider1').slider("value", getAssessmentIndex1());
        setAproxTime();
    });
</script>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<tr>
    <%if (Model.ApplicationID != 23 || Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) > new Version("2.2.0.0") || Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) >= new Version("1.2.0.3"))
      { %>
    <td><%: Html.TranslateTag("Aware State Heartbeat","Aware State Heartbeat")%></td>
    <%}
      else if (Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) < new Version("2.2.0.0") || Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) <= new Version("1.2.0.3"))
      { %>
    <td><%: Html.TranslateTag("Time Before No Motion Rearm","Time Before No Motion Rearm")%></td>
    <%} %>
    <td>
        <%: Html.TextBoxFor(model => model.ActiveStateInterval, (Dictionary<string,object>)ViewData["HtmlAttributes"])%> Minutes
                        <%: Html.ValidationMessageFor(model => model.ActiveStateInterval)%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="<%: Html.TranslateTag("How often the sensor communicates with the gateway while in the Aware State.","How often the sensor communicates with the gateway while in the Aware State.")%>" src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>

<tr>
    <td></td>
    <td colspan="2">
        <div id="ActiveStateInterval_Slider"></div>
    </td>
</tr>


<script type="text/javascript">
    //Use Report interval and minReport inteval from heartbeat above
    function getActiveStateIntervalIndex() {
        var retval = 0;
        $.each(reportInterval_array, function (index, value) {
            if (value <= $("#ActiveStateInterval").val())
                retval = index;
        });

        return retval;
    }

    $('#ActiveStateInterval_Slider').slider({
        value: getActiveStateIntervalIndex(),
        min: 0,
        max: reportInterval_array.length - 1,
                        <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
        slide: function (event, ui) {
            //update the amount by fetching the value in the value_array at index ui.value
            $('#ActiveStateInterval').val(reportInterval_array[ui.value]);
            if ($('#ReportInterval').val() < reportInterval_array[ui.value]) {
                $('#ReportInterval').val(reportInterval_array[ui.value]);
                $('#ReportInterval').change();
            }
        }
    });

    $("#ActiveStateInterval").addClass('editField editFieldMedium');
    $("#ActiveStateInterval").change(function () {
        if (isANumber($("#ActiveStateInterval").val())) {
            //Check if less than min
            if ($("#ActiveStateInterval").val() < minReportInterval)
                $("#ActiveStateInterval").val(minReportInterval);

            //Check if greater than max
            if ($("#ActiveStateInterval").val() > 720)
                $("#ActiveStateInterval").val(720);

            $('#ActiveStateInterval_Slider').slider("value", getActiveStateIntervalIndex());
        }
        else {
            $("#ActiveStateInterval").val(<%: Model.ActiveStateInterval%>);
            $('#ActiveStateInterval_Slider').slider("value", getActiveStateIntervalIndex());
        }
    });

</script>






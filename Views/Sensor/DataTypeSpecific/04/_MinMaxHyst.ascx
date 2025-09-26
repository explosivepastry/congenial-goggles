<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%              string Hyst = "";
                string Min = "";
                string Max = "";
                MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst, out Min, out Max); %>


<%--Min Threshold--%>
<tr>
    <td style="width: 250px;">Minimum Threshold</td>
    <td>
        <%: Html.TextBox("MinimumThreshold_Manual", Min, (Dictionary<string,object>)ViewData["HtmlAttributes"])%><%: ZeroToTwentyMilliamp.GetLabel(Model.SensorID)  %>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Any assessments below this value will cause the sensor to enter the Aware State." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>



<%--Max Threshold--%>
<tr>
    <td style="width: 250px;">Maximum Threshold</td>
    <td>
        <%: Html.TextBox("MaximumThreshold_Manual", Max, (Dictionary<string,object>)ViewData["HtmlAttributes"])%><%: ZeroToFiveVolts.GetLabel(Model.SensorID) %>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Any assessments above this value will cause the sensor to enter the Aware State." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>

<%--Hysterisis--%>

<tr>
    <td style="width: 250px;">Hysteresis</td>
    <td>
        <%: Html.TextBox("Hysteresis_Manual", Hyst, (Dictionary<string,object>)ViewData["HtmlAttributes"])%><%: ZeroToFiveVolts.GetLabel(Model.SensorID) %>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="A buffer to prevent the sensor from bouncing between Standard Operation and Aware State when the assessments are very close to a threshold.  For example, if a Maximum Threshold is set to 90° and a Hysteresis of 1°, then once the sensor takes an assessment of 90.0° and enters the Aware State it will remain in the Aware State until the temperature readings drop to 89.0°.  Similarly, at the Minimum Threshold the temperature will have to rise a degree above the threshold to return to Standard Operation." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<script>
    $('#Hysteresis_Manual').addClass("editField editFieldMedium");
    $('#MaximumThreshold_Manual').addClass("editField editFieldMedium");
    $('#MinimumThreshold_Manual').addClass("editField editFieldMedium");

    var lowVal = <%: ZeroToTenVolts.GetLowValue(Model.SensorID).ToDouble()%>;
    var highVal = <%: ZeroToTenVolts.GetHighValue(Model.SensorID).ToDouble()%>;

    $('#Hysteresis_Manual').change(function ()
    {
        if (isANumber($("#Hysteresis_Manual").val())){
            if ($('#Hysteresis_Manual').val() < 0)
                $('#Hysteresis_Manual').val(0);

            if($('#Hysteresis_Manual').val() > ((highVal- lowVal) * .25))
                $('#Hysteresis_Manual').val(((highVal - lowVal) * .25));
        }
        else
        {
            $('#Hysteresis_Manual').val(<%: Hyst%>);
       }

    });

    $('#MaximumThreshold_Manual').change(function () {
        

        if (isANumber($("#MaximumThreshold_Manual").val())){
            if ($('#MaximumThreshold_Manual').val() < lowVal)
                $('#MaximumThreshold_Manual').val(lowVal);

            if ($('#MaximumThreshold_Manual').val() > highVal )
                $('#MaximumThreshold_Manual').val(highVal);
        }
        else
            $('#MaximumThreshold_Manual').val(<%: Max%>);

    });

    $('#MinimumThreshold_Manual').change(function () {
        if (isANumber($("#MinimumThreshold_Manual").val())){
            if ($('#MinimumThreshold_Manual').val() < lowVal)
                $('#MinimumThreshold_Manual').val(lowVal);

            if ($('#MinimumThreshold_Manual').val() > highVal )
                $('#MinimumThreshold_Manual').val(highVal);
        }
        else
            $('#MinimumThreshold_Manual').val(<%: Min%>);

    });
</script>
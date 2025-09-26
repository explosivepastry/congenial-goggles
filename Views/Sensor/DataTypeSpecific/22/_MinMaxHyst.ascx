<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%              
                string Min = "";
                string Max = "";
                MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
%>


<%--Max Threshold--%>
<tr>
    <td style="width: 250px;">Max Power Threshold</td>
    <td>
        <%: Html.TextBox("MaximumThreshold_Manual", Max, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>kWh
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="If the sensor is not aware and power accumulation exceeds this value in a single standard heartbeat the sensor will become aware and report immediately." src="/Content/images/help.png" /></td>
</tr>

<%--Min Threshold--%>
<tr>
    <td style="width: 250px;">Aware Max Power Threshold</td>
    <td>
        <%: Html.TextBox("MinimumThreshold_Manual", Min, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>kWh
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="If the sensor is aware and power accumulation is less than this value the sensor will become not aware." src="/Content/images/help.png" /></td>
</tr>

<%--CalVal3 -- CT-Size 
<tr>
    <td style="width: 250px;">CT-Size</td>
    <td>
        <%: Html.TextBox("CalVal3_Manual", Model.Calibration3, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>AMPS
        <%: Html.ValidationMessageFor(model => model.Calibration3)%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Max Input of the Current Transducer" src="/Content/images/help.png" /></td>
</tr>
--%>
<tr>
    <td>Accumulate/Rollover</td>
    <td>
        <%: Html.CheckBox("HysteresisRollOverChk", CTPower.GetHystFirstByte(Model) == 1, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        
        <div style="display: none;"><%: Html.TextBox("HysteresisRollOver_Manual", CTPower.GetHystFirstByte(Model) ,(Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Determines if the sensor triggers on both state changes." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<script type="text/javascript">

    setTimeout('$("#HysteresisRollOverChk").iButton({ labelOn: "Accumulate" , labelOff: "Rollover" });', 500);
    $('#HysteresisRollOverChk').change(function () {
        if ($('#HysteresisRollOverChk').prop('checked')) {
            $('#HysteresisRollOver_Manual').val(1);
        } else {
            $('#HysteresisRollOver_Manual').val(0);
        }
    });

</script>

<%--
    Hysterisis--%>

<tr>
    <td style="width: 250px;">kWh LED Flashes Every</td>
    <td>
        <%: Html.TextBox("HysteresisKWHLED_Manual", CTPower.HysteresisKWHLED(Model)/1000, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>kWh
        
    </td>
    <td>
        <img alt="help" class="helpIcon" title="A buffer to prevent the sensor from bouncing between Standard Operation and Aware State when the assessments are very close to a threshold.  For example, if a Maximum Threshold is set to 90° and a Hysteresis of 1°, then once the sensor takes an assessment of 90.0° and enters the Aware State it will remain in the Aware State until the temperature readings drop to 89.0°.  Similarly, at the Minimum Threshold the temperature will have to rise a degree above the threshold to return to Standard Operation." src="/Content/images/help.png" /></td>
</tr>

<script>
    $('#HysteresisKWHLED_Manual').addClass("editField editFieldMedium");
    $('#MaximumThreshold_Manual').addClass("editField editFieldMedium");
    $('#MinimumThreshold_Manual').addClass("editField editFieldMedium");

  
    
    $('#HysteresisKWHLED_Manual').change(function ()
    {
        if (isANumber($("#HysteresisKWHLED_Manual").val())) {
            if ($('#HysteresisKWHLED_Manual').val() < .125)
                $('#Hysteresis_Manual').val(.125);

            if($('#Hysteresis_Manual').val() > 65.535)
                $('#Hysteresis_Manual').val(65.535);
        }
        else
        {
            $('#Hysteresis_Manual').val(<%: CTPower.HysteresisKWHLED(Model)%>);
        }
    });

    $('#MaximumThreshold_Manual').change(function () {
        
        if (isANumber($("#MaximumThreshold_Manual").val())){
            if ($('#MaximumThreshold_Manual').val() < 1)
                $('#MaximumThreshold_Manual').val(1);

            if ($('#MaximumThreshold_Manual').val() > 4294967.295)
                $('#MaximumThreshold_Manual').val(4294967.295);
        }
        else
            $('#MaximumThreshold_Manual').val(<%: Max%>);

    });

    $('#MinimumThreshold_Manual').change(function () {
        if (isANumber($("#MinimumThreshold_Manual").val())){
            if ($('#MinimumThreshold_Manual').val() < 1)
                $('#MinimumThreshold_Manual').val(1);

            if ($('#MinimumThreshold_Manual').val() > 4294967.295)
                $('#MinimumThreshold_Manual').val(4294967.295);
        }
        else
            $('#MinimumThreshold_Manual').val(<%: Min%>);

    });
</script>

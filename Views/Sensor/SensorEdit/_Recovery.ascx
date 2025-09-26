<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% if (!Model.IsWiFiSensor)
   { %>



<tr>
    <td>Failed transmissions before link mode</td>
    <td>
        <%: Html.TextBoxFor(model => model.Recovery, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.Recovery)%>
    </td>
    <td>
        <%
       int Interval = Model.TransmitIntervalLink;
       try{
		   if ((Model.GenerationType.ToUpper().Contains("GEN1")) && (new Version(Model.FirmwareVersion) <= new Version(2, 3, 0, 0)))
           {
                if(Model.TransmitIntervalLink > 100)
                    Interval = Model.TransmitIntervalLink - 100;
                else
                    Interval = Model.TransmitIntervalLink * 60;
           }
       }
       catch{}
        %>
        <img alt="help" class="helpIcon" title="Default value 2<br/><br/>The number of transmissions the sensor sends without response from a gateway before it goes to battery saving link mode.  In link mode, the sensor will scan for a new gateway and if not found will enter battery saving sleep mode for up to <%: Interval%> minutes before trying to scan again.  Lower number will allow sensors to find new gateways with fewer missed readings.  Higher numbers will enable the sensor to remain with its current gatweay in a noisy RF environment better.  (Zero will cause the sensor to never join another gatweay, to find a new gateway the battery will have to be cycled out of the sensor.)" src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<tr>
    <td></td>
    <td colspan="2">
        <div id="Recovery_Slider"></div>
    </td>
</tr>
<script type="text/javascript">
    var recovery_array = [0, 1, 2, 3, 4, 5, 10];
    function getRecoveryIndex() {
        var retval = 0;
        $.each(recovery_array, function (index, value) {
            if (value <= $("#Recovery").val())
                retval = index;
        });

        return retval;
    }

    $('#Recovery_Slider').slider({
        value: getRecoveryIndex(),
        min: 0,
        max: recovery_array.length - 1,
                        <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
        slide: function (event, ui) {
            //update the amount by fetching the value in the value_array at index ui.value
            $('#Recovery').val(recovery_array[ui.value]);
        }
    });
    $("#Recovery").addClass('editField editFieldSmall');

    $("#Recovery").change(function () {
        if (isANumber($("#Recovery").val())) {
            if ($("#Recovery").val() < 0)
                $("#Recovery").val(0);
            if ($("#Recovery").val() > 250)
                $("#Recovery").val(250)
            $('#Recovery_Slider').slider("value", getRecoveryIndex());
        }
        else {
            $("#Recovery").val(2);
            $('#Recovery_Slider').slider("value", getRecoveryIndex());
        }
    });
</script>

<% } %>
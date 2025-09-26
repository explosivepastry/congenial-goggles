<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>



<tr>
    <td colspan="3">
        <h2>Data Transformation
                    <img alt="help" class="helpIcon" title="Scale reading and apply engineering units" src="<%:Html.GetThemedContent("/images/help.png")%>" /></h2>
    </td>
</tr>

 <!--LowValue-->
        <tr>
            <td style="width: 250px;">4 mA Value</td>
            <td>
                <input class="aSettings__input_input" type="text" id="lowValue" name="lowValue" value="<%:Monnit.ZeroToTwentyMilliamp.Get4maValue(Model.SensorID) %>" />
            </td>
            <td>
                <img alt="help" class="helpIcon" title="Default value 4<br/><br/>Scaled reading that will be shown when sensor measures 4 mA." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
        </tr>
        <!--HighValue-->
        <tr>
            <td style="width: 250px;">20 mA Value</td>
            <td>
                <input class="aSettings__input_input" type="text" id="highValue" name="highValue" value="<%:Monnit.ZeroToTwentyMilliamp.GetHighValue(Model.SensorID) %>" />
            </td>
            <td>
                <img alt="help" class="helpIcon" title="Default value 4<br/><br/>Scaled reading that will be shown when sensor measures 20 mA." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
        </tr>
        <!--Label-->
        <tr>
            <td style="width: 250px;">Label</td>
            <td>
                <input class="aSettings__input_input" type="text" id="label" name="label" value="<%:Monnit.ZeroToTwentyMilliamp.GetLabel(Model.SensorID) %>" />
            </td>
            <td>
                <img alt="help" class="helpIcon" title="Default value mA<br/><br/>Unit to display with the scaled reading." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
        </tr>
<script>
    $('#lowValue').addClass("editField editFieldMedium");
    $('#highValue').addClass("editField editFieldMedium");
    $('#label').addClass("editField editFieldMedium");
</script>

   


<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<tr>
    <td style="width: 250px;">Sensor Mode</td>
    <td>
        <div class="editor-field">
            <select name="Mode" id="Mode" <%:ViewData["disabled"].ToBool() ? "disabled = disabled" : "" %>>
                <option value="0" <%:(((UInt32)Model.Calibration2) & 0x0000FFFF) == 0 ? "selected='selected'" : "" %>>Volt AC RMS</option>
                <option value="1" <%:(((UInt32)Model.Calibration2) & 0x0000FFFF) == 1 ? "selected='selected'" : "" %>>Volt AC Peak to Peak</option>
                <option value="3" <%:(((UInt32)Model.Calibration2) & 0x0000FFFF) == 3 ? "selected='selected'" : "" %>>Volt DC US(60 Hz Sample)</option>
                <option value="4" <%:(((UInt32)Model.Calibration2) & 0x0000FFFF) == 4 ? "selected='selected'" : "" %>>Volt DC Europe(50 Hz Sample)</option>
            </select>
        </div>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="If AC RMS is selected, you can calibrate. Otherwise no calibration is available for the other options." src="<%:Html.GetThemedContent("/images/help.png")%>" />
        <script>

            $(function () {
                $('.helpIcon').tipTip();
            });

        </script>
    </td>
</tr>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sensitivity Threshold","Sensitivity Threshold")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Meter - Sensitivity Threshold","The sensor only measures vibration magnitudes at or above this setting. If the vibration is below this setting, the vibration will be ignored, and the vibration indicated will be 0. If this setting is 0, all vibrations will be measured and analyzed, even when the sensor is relatively still. This also determines what vibration levels contribute to the Duty Cycle. If set to 0, the Duty Cycle will generally be 100 percent.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Measurement Method","Measurement Method")%>
    </div>
    <div class="word-def">
       <div><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Meter - Measurement Method","Determines the method of measurement used to calcultate Velocity RMS.")%></div>
        <br />
        <div><b><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Meter - Measurement Method","Peak Acceleration: ")%></b><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Meter - Measurement Method","Velocity RMS = (Peak Acceleration / (2 * PI * Frequency)) * .70711.")%></div>
        <br />
        <div><b><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Meter - Measurement Method","Peak Velocity: ")%></b><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Meter - Measurement Method","Velocity RMS = (Peak Velocity * .70711).")%></div>
        <br />
        <div><b><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Meter - Measurement Method","True RMS: ")%></b><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Meter - Measurement Method","Velocity RMS = sqrt(x1^2 + x2^2 + x3^2.../N).")%></div>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Window Function","Window Function")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Meter - Window Function","Determines the type of filter applied to the measurement: No Window (best frequency resolution), Hanning Window (good frequency resolution and amplitude accuracy), and Flattop Window (best amplitude accuracy).")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Measurement Interval","Measurement Interval")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Meter - Measurement Interval","Determines how often the sensor takes a measurement. The Measurement Interval is used with Sensitivity Threshold to calculate the Duty Cycle. It’s the Duty Cycle percent minus the number of measurements above the Sensitivity Threshold / total number of measurements within a single Heartbeat Interval. We recommend setting the Heartbeat to a multiple of the Measurement Interval.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Intensity Threshold","Vibration Intensity Threshold")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|This value determines the g-forces required  to wake up the sensor to take a measurement. If the g-forces are not greater than this, the sensor will remain asleep. Values range from 1 to 127, each unit is .063 g-forces.","This value determines the g-forces required  to wake up the sensor to take a measurement. If the g-forces are not greater than this, the sensor will remain asleep. Values range from 1 to 127, each unit is .063 g-forces.")%>
        <hr />
    </div>
</div>


<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Detection Type","Detection Type")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Configures if pulses are recorded at the positive edge, the negative edge, or both edges of the pulse.", "Configures if pulses are recorded at the positive edge, the negative edge, or both edges of the pulse.")%>
        <div class="d-flex" style="justify-content: center;">
            <%: Html.GetThemedSVG("pulseDiagram")%>
        </div>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Noise Filter ","Noise Filter ")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Dual Pulse - Filter Frequency","Used to filter out false detections caused by electrical noise in the line.")%>
        <br />
        <br />
        <b style="text-decoration:underline"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Dual Pulse - Recommendations:","Recommendations:")%></b>
        <br />
        <br />
        <b><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Dual Pulse - Strong","Strong")%></b> - <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Can be used when maximum expected pulse rate is less than 4 times per second and the pulse width is greater than 125 ms.","Can be used when maximum expected pulse rate is less than 4 times per second and the pulse width is greater than 125 ms.")%>
        <br />
        <br />
        <b><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Dual Pulse - Medium","Medium")%></b> - <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Can be used when maximum expected pulse rate is less than 20 times per second and the pulse width is greater than 25 ms.","Can be used when maximum expected pulse rate is less than 20 times per second and the pulse width is greater than 25 ms.")%>
        <br />
        <br />
        <b><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Dual Pulse - Weak","Weak")%></b> - <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Can be used when maximum expected pulse rate is less than 80 times per second and the pulse width is greater than 6.2 ms.","Can be used when maximum expected pulse rate is less than 80 times per second and the pulse width is greater than 6.2 ms.")%>
        <br />
        <br />
        <b><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Dual Pulse - None","None")%></b> - <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Will not filter any noise.","Will not filter any noise.")%>
        <br />
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>
    <div class="word-def">

        <span style="font-weight: bold"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default: ","Default: ")%> </span><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Off","Off")%>
        <br />
        <br />
        <span style="font-weight: bold"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Standard Operation: ","Standard Operation: ")%> </span><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Reports the number of pulses detected between heartbeats.","Reports the number of pulses detected between heartbeats.")%>
        <br />
        <br />
        <span style="font-weight: bold"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Accumulate On: ","Accumulate On: ")%> </span><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Will accumulate the readings heartbeat after heartbeat.","Will accumulate the readings heartbeat after heartbeat.")%>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Accumulation rolls over at 18446744073709551615 (64 bit).","Accumulation rolls over at 18446744073709551615 (64 bit).")%>
        <br />
        <br />
        <span style="font-weight: bold"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Reset: ","Reset: ")%> </span>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Will set the accumulated value to 0. This command is only available when accumulate is turned on and saved.","Will set the accumulated value to 0. This command is only available when accumulate is turned on and saved.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Overflow Count","Overflow Count")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Dual Pulse - Overflow Count","When this number of pulses occurs within the heartbeat, the sensor will report data with the aware flag set immediately.")%> 
        <br />
        <br />
        <span><b>Note:</b> <%:Html.TranslateTag("If the aware flag was set on the previous data point, the sensor waits for the Overflow Limit (seconds) to report.") %> </span>
        <hr />
    </div>
</div>

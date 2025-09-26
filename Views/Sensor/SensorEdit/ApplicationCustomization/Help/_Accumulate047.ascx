<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">

    <div class="word-choice">
        <%: Html.TranslateTag("Overflow Count (pulses)","Overflow Count (pulses)")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Single Input pulse counter - Overflow count.","The value at which the accumulation of pulses will roll over. Default: 4294967295")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Aware State Overflow count (pulses)","Aware State Overflow count (pulses)")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Single Input Pulse Counter - Aware State Overflow Count","Assessments above this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Accumulate","Accumulate")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Single Input Pulse Counter - Accumulate ","Standard operation repots the number of pulses detected between Heartbeats.")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Single Input Pulse Counter - Accumulate ","Accumulate on will accumulate the readings Heartbeat after Heartbeat.")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Single Input Pulse Counter - Accumulate ","Accumulation rolls over at 4294967295 (2^64 or 0xFFFFFFFF).")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Pulse Edge Detection","Pulse Edge Detection")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Single Input Pulse Counter - Pulse Edge Detection ","Determines whether pulses are recorded at the positive edge of the pulse or the negative edge of the pulse.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Stabilization Delay","Stabilization Delay")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Miliseconds to wait for confirming positive reading after pulse initially detected. (Debouncing) Recommended 50 percent of minimum pulse width of input signal.","Miliseconds to wait for confirming positive reading after pulse initially detected. (Debouncing) Recommended 50 percent of minimum pulse width of input signal.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Filter","Filter")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Single Input Pulse Counter - Filter ","Determines if there is a filter and at what Hz the filter is set.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Scale","Scale")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Single Input Pulse Counter - Scale ","You can set the label and how many pulses are counted as one pulse.")%>
        <hr />
    </div>
</div>



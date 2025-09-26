<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Exit Aware Threshold","Exit Aware Threshold")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|When the number of pulses since the last transmition fail to exceed this threshold, the sensor will revert to the standard (non aware) operating state.  This threshold is only evaluated while the sensor is in the aware operating state.  ", "When the number of pulses since the last transmition fail to exceed this threshold, the sensor will revert to the standard (non aware) operating state.  This threshold is only evaluated while the sensor is in the aware operating state.  ")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Enter Aware Threshold","Enter Aware Threshold")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|When the number of pulses since the last transmition exceed this threshold, the sensor will enter the aware state and trigger a report.  This threshold is only evaluated while the sensor is in the standard (non aware) operating state.", "When the number of pulses since the last transmition exceed this threshold, the sensor will enter the aware state and trigger a report.  This threshold is only evaluated while the sensor is in the standard (non aware) operating state.")%> 
        <hr />
    </div>
</div>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_ActiveStateInterval.ascx", Model);%>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default: On.   Standard operation reports the number of pulses detected between heartbeats. Accumulate on will accumulate the readings heartbeat after heartbeat.Accumulation rolls over at 4294967295 (2^64 or 0xFFFFFFFF)", "Default: On.   Standard operation reports the number of pulses detected between heartbeats. Accumulate on will accumulate the readings heartbeat after heartbeat.Accumulation rolls over at 4294967295 (2^64 or 0xFFFFFFFF)")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|LED Power Mode","LED Power Mode")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default: On.  If this is turned on it will allow the LED to blink green showing that it is connected. if it was unable to connect it will blink red. if this is off it will only show upon starting the sensor. Accumulate on will accumulate the readings heartbeat after heartbeat.  Accumulation rolls over at 4294967295 (2^64 or 0xFFFFFFFF)", "Default: On.  If this is turned on it will allow the LED to blink green showing that it is connected. if it was unable to connect it will blink red. if this is off it will only show upon starting the sensor. Accumulate on will accumulate the readings heartbeat after heartbeat.  Accumulation rolls over at 4294967295 (2^64 or 0xFFFFFFFF)")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Detection Type","Detection Type")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Configures if pulses are recorded at the positive edge of the pulse or the negative edge of the pulse.", "Configures if pulses are recorded at the positive edge of the pulse or the negative edge of the pulse.")%> 
         <div class="d-flex" style="justify-content: center;">
             <%: Html.GetThemedSVG("pulseDiagram")%>
         </div>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Inactivity Timer","Inactivity Timer")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Seconds to wait for confirming positive reading after pulse initially detected. (Debouncing) Recommended 50% of minimum pulse width of input signal.", "Seconds to wait for confirming positive reading after pulse initially detected. (Debouncing) Recommended 50% of minimum pulse width of input signal.")%> 
        <hr />
    </div>
</div>



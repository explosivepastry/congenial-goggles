<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Apparent Power Usage Threshold","Apparent Power Usage Threshold")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|If the sensor is not aware and power accumulation exceeds this value in a single standard heartbeat the sensor will become aware and report immediately.","If the sensor is not aware and power accumulation exceeds this value in a single standard heartbeat the sensor will become aware and report immediately.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|RMS Voltage Threshold","RMS Voltage Threshold")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|RMS voltages above this value will cause the sensor to become aware.","RMS voltages above this value will cause the sensor to become aware.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|RMS Current Threshold","RMS Current Threshold")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|RMS currents above this value will cause the sensor to become aware","RMS currents above this value will cause the sensor to become aware")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CT-Size","CT-Size")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Max Input of the Current Transducer","Max Input of the Current Transducer")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default: Off Standard opperation reports the number of pulses detected between heartbeats. Accumulate on will accumulate the readings heartbeat after heartbeat. Accumulation rolls over at 65535 (2^16 or 0xFFFF)","Default: Off Standard opperation reports the number of pulses detected between heartbeats. Accumulate on will accumulate the readings heartbeat after heartbeat. Accumulation rolls over at 65535 (2^16 or 0xFFFF)")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default Frequency","Default Frequency")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default frequency of the power system.","Default frequency of the power system.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Active Channel","Active Channel")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Check box for active power channels. Channel 0 is always active because the sensor is also power through this channel.","Check box for active power channels. Channel 0 is always active because the sensor is also power through this channel.")%>
        <hr />
    </div>
</div>
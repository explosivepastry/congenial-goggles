<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Observation Mode Heartbeat Interval (minutes)","Observation Mode Heartbeat Interval (minutes)")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Food Probe - Observation Mode Heartbeat Interval ", "The time in minutes the device records and sends a temperature reading to the gateway while in Observation Mode.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Observation Mode Minimum Temp Threshold (°F)","Observation Mode Minimum Temp Threshold (°F)")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Food Probe - Observation Mode Minimum Temp Threshold", "Assessments below this value will cause the sensor to enter the Aware State.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Observation Mode Maximum Temp Threshold (°F)","Observation Mode Maximum Temp Threshold (°F)")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Food Probe - Observation Mode Maximum Temp Threshold", "Assessments above this value will cause the sensor to enter the Aware State.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Observation Mode Time Threshold (Minutes)","Observation Mode Time Threshold (Minutes)")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Food Probe - Observation Mode Time Threshold", "The maximum time the probe is in Observation Mode before the device ends the session and goes to sleep.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("LCD Display Timeout (seconds)","LCD Display Timeout (seconds)")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Food Probe - LCD Display Timeout", "The time in seconds when the LCD Screen will go to sleep if there is no activity.")%> 
        <hr />
    </div>
</div>

<%--<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Scale","Scale")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Food Probe - Scale", "This sensor supports changing the unit of measurement from Fahrenheit to Celsius or vice versa.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Calibration","Calibration")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Food Probe - Calibration", "This sensor supports modifying the temperature calibration.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Calibration Certificate","Calibration Certificate")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Food Probe - Calibration Certificate", "If a certificate is assigned to a sensor, the calibration features are disabled and hidden from view.")%> 
        <hr />
    </div>
</div>--%>




<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Aware State Heartbeat","Aware State Heartbeat")%>
    </div>
     <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|The expected time between communication from the device to the gateway while in the Aware State.","The expected time between communication from the device to the gateway while in the Aware State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Mode","Vibration Mode")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|This determines whether the sensor outputs Velocity, Acceleration, or Displacement data.","This determines whether the sensor outputs Velocity, Acceleration, or Displacement data.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Aware Threshold","Vibration Aware Threshold")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|The sensor will become aware when Vibration goes above this value.","The sensor will become aware when Vibration goes above this value.")%>
        <hr />
    </div>
</div>
<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Hysteresis","Vibration Hysteresis")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|A buffer to prevent the sensor from bouncing between Standard Operation and Aware State when the assessments are very close to a threshold.","A buffer to prevent the sensor from bouncing between Standard Operation and Aware State when the assessments are very close to a threshold.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Sensitivity Threshold","Vibration Sensitivity Threshold")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Values below this threshold are ignored (no analyisis will occur and will not count towards duty cycle). Setting this to 0 will force the sensor to analyze every time, including noise, and the duty cycle will always be 100 percent.","Values below this threshold are ignored (no analyisis will occur and will not count towards duty cycle). Setting this to 0 will force the sensor to analyze every time, including noise, and the duty cycle will always be 100 percent.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Window Function","Window Function")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|This setting determines which window to use to filter the FFT results. If the sample rate and window function for velocity and acceleration are the same one set of samples will be taken instead of two saving power and reducing measurement time.","This setting determines which window to use to filter the FFT results. If the sample rate and window function for velocity and acceleration are the same one set of samples will be taken instead of two saving power and reducing measurement time.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Accelerometer Range","Accelerometer Range")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Max observable g-force","Max observable g-force")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Measurement Interval","Measurement Interval")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Interval in minutes between measurements","Interval in minutes between measurements")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sample Rate","Sample Rate")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|This sets the sample rate of the accelerometer. As the sample rate decreases the amount of time it takes to sample increases. As an example 25 Hz sample time is 10.24 seconds and at 6.25 Hz it is 40.96 seconds. Keep this in mind when setting the measurment interval.","This sets the sample rate of the accelerometer. As the sample rate decreases the amount of time it takes to sample increases. As an example 25 Hz sample time is 10.24 seconds and at 6.25 Hz it is 40.96 seconds. Keep this in mind when setting the measurment interval.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Frequency Range","Frequency Range")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|The frequency range to consider when measuring velocity.","The frequency range to consider when measuring velocity.")%>
        <hr />
    </div>
</div>
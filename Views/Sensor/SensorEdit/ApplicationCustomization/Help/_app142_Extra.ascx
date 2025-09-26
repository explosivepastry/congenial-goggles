<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Mode","Vibration Mode")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Advanced Vibration 2 - Vibration Mode","Option to select what information the meter will generate: Velocity RMS, Acceleration RMS, Acceleration Peak, or Displacement Peak-to-Peak.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Aware Threshold","Vibration Aware Threshold")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Advanced Vibration 2 - Vibration Aware Threshold","The measured value above which the device will immediately report and enter its Aware State.")%>
        <hr />
    </div>
</div>
<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Hysteresis","Vibration Hysteresis")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Advanced Vibration 2 - Vibration Hysteresis","A buffer to prevent the sensor from bouncing between Standard and Aware States when the vibrations are very close to a threshold.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vibration Sensitivity Threshold","Vibration Sensitivity Threshold")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Advanced Vibration 2 - Vibration Sensitivity Threshold","Measured values below this limit are ignored and will not count toward the Duty Cycle. Setting this to 0 will force the sensor to analyze every measurement (including noise) fully, and the Duty Cycle will always be 100 percent.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Window Function","Window Function")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Advanced Vibration 2 - Window Function","Determines which filter to apply to the FFT results. Rectangular means no filtering. Hanning minimizes spectral leakage.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Accelerometer Range","Accelerometer Range")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Advanced Vibration 2 - Accelerometer Range ","Determines the accelerometer's maximum observable g-force. If the actual measured values are above this setting, the peaks of the vibration waveform may be clipped, distorting the data and typically resulting in a Crest Factor below 1.41.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Measurement Interval","Measurement Interval")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Advanced Vibration 2 - Measurement Interval","In minutes between vibration measurements, each vibration measurement consists of 256 digital samples collected at the configured Sample Rate.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sample Rate","Sample Rate")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Advanced Vibration 2 - Sample Rate","Determines the digital sampling freqency of the accelerometer.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Frequency Range Minimum","Frequency Range Minimum")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Advanced Vibration 2 - Frequency Range Minimum","Vibrations below this limit are filtered/ignored and may be set as low as Sample Rate * 2 / 256")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Frequency Range Minimum","Frequency Range Maximum")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Advanced Vibration 2 - Frequency Range Maximum","Vibrations above this limit are filtered/ignored and may be set as low as Sample Rate / 2")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Power Mode","Power Mode")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Advanced Vibration 2 - Power Mode","Control the power consumption of the accelerometer during measurements by changing the amount of oversampling performed. Low Power averages one reading per sample. Medium Power averages 16 readings per sample. High Performance averages up to 128 readings per sample and consumes the most power.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Data Mode","Data Mode")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Advanced Vibration 2 - Data Mode","Determines what data to display on each Heartbeat. Most Recent (Recently captured data), Average (Average of data captured over the Heartbeat), and Maximum (Maximum data captured over the Heartbeat).")%>
        <hr />
    </div>
</div>

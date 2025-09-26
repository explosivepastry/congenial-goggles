<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Vibration Sensitivity Threshold","Vibration Sensitivity Threshold")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Values below this threshold are ignored (no analyisis will occur)","Values below this threshold are ignored (no analyisis will occur)")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Axis Measurement Mode","Axis Measurement Mode")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|This setting determines which axis or set of axes are used to calculate the fundamental and harmonic frequencies.","This setting determines which axis or set of axes are used to calculate the fundamental and harmonic frequencies.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fundamental Frequency Resolution","Fundamental Frequency Resolution")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|This setting determines the FFT length used to calculated the fundamental frequency. Low setting = 6 Hz resolution (best battery life),  Medium setting = 3 Hz resolution, High setting 1.5 Hz resolution","This setting determines the FFT length used to calculated the fundamental frequency. Low setting = 6 Hz resolution (best battery life),  Medium setting = 3 Hz resolution, High setting 1.5 Hz resolution")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fundamental Windowing","Fundamental Windowing")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|This setting determines which window to use to filter the FFT results.","This setting determines which window to use to filter the FFT results.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Harmonic Frequency Resolution","Harmonic Frequency Resolution")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|This setting determines which window to use to filter the FFT results. Low setting = 100 Hz resolution (best battery life),  Medium setting = 50 Hz resolution, High setting 25 Hz resolution","This setting determines which window to use to filter the FFT results. Low setting = 100 Hz resolution (best battery life),  Medium setting = 50 Hz resolution, High setting 25 Hz resolution")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Harmonic Windowing","Harmonic Windowing")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|This setting determines which window to use to filter the FFT results.","This setting determines which window to use to filter the FFT results.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Harmonic Analysis","Harmonic Analysis")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Interval between harmonic measurements.","Interval between harmonic measurements.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Show Full Data Value","Show Full Data Value")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Default: False, only shows Fundamental Frequency","Default: False, only shows Fundamental Frequency")%>
        <hr />
    </div>
</div>

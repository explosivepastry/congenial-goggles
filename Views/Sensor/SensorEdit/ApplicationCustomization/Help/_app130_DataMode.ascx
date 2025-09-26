<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tilt Detection - Down Angle Threshold","Down Angle Threshold")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tilt Detection - Down Angle Threshold","Angles at or below this point are considered down.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tilt Detection - Up Angle Threshold","Up Angle Threshold")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tilt Detection - Up Angle Threshold","Angles at or below this point are considered up. The Up Angle Threshold should always be higher than your Down Angle Threshold.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tilt Detection - Delta Value","Delta Value")%>
    </div>
    <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tilt Detection - Delta Value","The change permitted before triggering an Aware State and communicating immediately.")%>
        <hr />
    </div>
</div>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Measurement Stablity","Measurement Stablity")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tilt Detection - Measurement Stability","The Number of measurements after entering a valid Up or Down region to wait until reporting the current pitch and status")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Stuck Time Out","Stuck Time Out")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|How long the sensor will measure to determine the finger is stable enough to measure after an event.","How long the sensor will measure to determine the finger is stable enough to measure after an event.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tilt Detection - Rotational Axis","Rotational Axis")%>
    </div>
    <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tilt Detection - Rotational Axis","Used to select the axis of measurement. While the ALTA Tilt Detection Sensor can measre tilt on all three axes, it can only report readings from one with positive or negative polarity.")%>
        <hr />
    </div>
</div>

<%--<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tilt Detection - Delta Mode","Delta Mode")%>
    </div>
    <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tilt Detection - Delta Mode","Select if the sensor is in Up/Down or Delta Mode.")%>
        <hr />
    </div>
</div>



<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tilt Detection - Data Mode","Data Mode")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tilt Detection - Data Mode","Select if the sensor is in Up/Down or Delta Mode. If in Up/Down Mode:")%>
           <hr />
    </div>
</div>--%>


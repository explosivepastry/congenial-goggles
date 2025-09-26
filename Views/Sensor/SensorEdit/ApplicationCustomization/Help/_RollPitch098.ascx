<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Threshold","Motion Threshold")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|1 motion threshold equals .63 g-force.","1 motion threshold equals .63 g-force.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Stuck Poll Interval","Stuck Poll Interval")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Stuck Poll Interval: How often the sensor will take a measurement after it has detected an event","Stuck Poll Interval: How often the sensor will take a measurement after it has detected an event")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Time Out","Time Out")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|How long the sensor will measure to determine the finger is stable enough to measure after an event.","How long the sensor will measure to determine the finger is stable enough to measure after an event.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Position Threshold","Position Threshold")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Any assessments outside of these values will cause the sensor to enter the Aware State.","Any assessments outside of these values will cause the sensor to enter the Aware State.")%> 
        <hr />
    </div>
</div>



<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Position Threshold","Position Threshold")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Any assessments outside of these values will cause the sensor to enter the Aware State.","Any assessments outside of these values will cause the sensor to enter the Aware State.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Time to Re-Arm (Seconds)","Time to Re-Arm (Seconds)")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tilt Detection - Re-Arm Time","The time in seconds after a triggering event that the sensor will wait before reacting to additional triggers.")%> 
        <hr />
    </div>
</div>
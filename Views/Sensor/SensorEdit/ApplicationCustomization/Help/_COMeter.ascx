<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Concentration Threshold","Concentration Threshold")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Any assessments outside of these values will cause the sensor to enter the Aware State.","Any assessments outside of these values will cause the sensor to enter the Aware State.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Concentration Buffer","Concentration Buffer")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|A buffer to prevent the sensor from bouncing between Standard Operation and Aware State when the assessments are very close to a threshold.  For example, if a Maximum Threshold is set to 900 pmm and a Hysteresis of 10ppm, then once the sensor takes an assessment of 900 and enters the Aware State it will remain in the Aware State until the temperature readings drop to 890.  Similarly, at the Minimum Threshold the temperature will have to rise 10 ppm above the threshold to return to Standard Operation.","A buffer to prevent the sensor from bouncing between Standard Operation and Aware State when the assessments are very close to a threshold.  For example, if a Maximum Threshold is set to 900 pmm and a Hysteresis of 10ppm, then once the sensor takes an assessment of 900 and enters the Aware State it will remain in the Aware State until the temperature readings drop to 890.  Similarly, at the Minimum Threshold the temperature will have to rise 10 ppm above the threshold to return to Standard Operation.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Time Weighted Average Threshold","Time Weighted Average Threshold")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Any assessments outside of these values will cause the sensor to enter the Aware State.","Any assessments outside of these values will cause the sensor to enter the Aware State.")%> 
        <hr />
    </div>
</div>


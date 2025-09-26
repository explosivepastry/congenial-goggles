<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Aware State Buffer","Aware State Buffer")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|A buffer to prevent the sensor from bouncing between Standard Operation and Aware State when the assessments are very close to a threshold.  For example, if a Maximum Threshold is set to 90° and a Aware State Buffer of 1°, then once the sensor takes an assessment of 90.0° and enters the Aware State it will remain in the Aware State until the temperature readings drop to 89.0°.  Similarly, at the Minimum Threshold the temperature will have to rise a degree above the threshold to return to Standard Operation.","A buffer to prevent the sensor from bouncing between Standard Operation and Aware State when the assessments are very close to a threshold.  For example, if a Maximum Threshold is set to 90° and a Aware State Buffer of 1°, then once the sensor takes an assessment of 90.0° and enters the Aware State it will remain in the Aware State until the temperature readings drop to 89.0°.  Similarly, at the Minimum Threshold the temperature will have to rise a degree above the threshold to return to Standard Operation.")%>
        <hr />
    </div>
</div>



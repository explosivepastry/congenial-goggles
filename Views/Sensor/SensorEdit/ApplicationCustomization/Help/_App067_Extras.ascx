<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Below","Below")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Commercial Ultrasonic - Aware Below","Assessments below this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Above","Above")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Commercial Ultrasonic - Aware Above","Assessments above this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Aware State Buffer","Aware State Buffer")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Commercial Ultrasonic - Aware Buffer","A buffer to prevent the sensor from bouncing between Standard Operation and the Aware State when the assessments are very close to a thresehold. For example, if you set a Maximum Thresholds to 200cm, the Aware State Buffer is set to 10cm. Once the sensor takes an assessment of 200cm and enters the Aware State, it will remain in the Aware State until the readings drop to 190cm. Similarly, the reading must rise 10cm above the Minimum Threshold to return to Standard Operation.")%>
        <hr />
    </div>
</div>
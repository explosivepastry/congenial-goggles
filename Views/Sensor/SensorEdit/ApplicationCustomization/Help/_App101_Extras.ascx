<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Enter Aware State when there is","Enter Aware State when there is")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Plus - Enter Aware State when there is","Determines if the sensor enters Aware State or not when detecting motion.")%>
        <hr />
    </div>
</div>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sensitivity","Sensitivity")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Plus - Motion Sensitivity","Sensitivity dictates the distance at which the sensor registers motion. Your options are 9 feet, 12 feet, or 15 feet.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Re-Arm Time","Re-Arm Time")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Plus - Re-Arm Time","The time in seconds after a triggering event that the sensor will wait before recognizing additional triggering events.")%>
        <hr />
    </div>
</div>



<%--<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Temperature Enter Aware State Below (°F)","Temperature Enter Aware State Below (°F)")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Plus - Temperature Enter Aware State Below","Temperature Assessments below this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Temperature Enter Aware State Above (°F)","Temperature Enter Aware State Above (°F)")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Plus - Temperature Enter Aware State Above","Temperature Assessments above this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Humidity Enter Aware State Below(%)","Humidity Enter Aware State Below(%)")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Plus - Humidity Enter Aware State Below(%)","Humidity Assessments below this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Humidity Enter Aware State Above(%)","Humidity Enter Aware State Above(%)")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Plus - Humidity Enter Aware State Above(%)","Humidity Assessments above this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Scale","Scale")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Plus - Scale","This sensor supports changing the unit of measurement from Fahrenheit to Celsius or vice versa.")%>
        <hr />
    </div>
</div>--%>
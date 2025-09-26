<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Detection Magnitude","Detection Magnitude")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Any assessments above this value will cause the sensor to detect vehicle status changed.", "Any assessments above this value will cause the sensor to detect vehicle status changed.")%> 
        <hr />
    </div>
</div>



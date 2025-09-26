<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Below (centibars)","Below (centibars)")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Soil Moisture - Below","Assessments below this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Above (centibars)","Above (centibars)")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Soil Moisture - Above","Assessments above this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>


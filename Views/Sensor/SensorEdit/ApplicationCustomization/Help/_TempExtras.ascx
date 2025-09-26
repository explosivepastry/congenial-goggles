<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Aware Below Threshold","Below")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Temperature Sensor - Aware Below Threshold","Temperatures measured below this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Aware Above Threshold","Above")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Temperature Sensor - Aware Above Threshold","Temperatures measured above this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>


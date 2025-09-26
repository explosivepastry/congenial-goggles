<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Above","Above")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Assessments above this value will cause the sensor to enter the Aware State.","Assessments above this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>



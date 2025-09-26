<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Minimum Threshold (unit of measure)","Minimum Threshold (unit of measure)")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Resistance - Min Ohms","Assessments below this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Maximum Threshold (unit of measure)","Maximum Threshold (unit of measure)")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Resistance - Max Ohms","Assessments above this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>

</div>



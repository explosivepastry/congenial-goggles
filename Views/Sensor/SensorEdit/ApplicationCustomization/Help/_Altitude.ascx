<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below (unit of measure)","Below (unit of measure)")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Air Velocity - Below","Assessments below this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Above (unit of measure)","Above (unit of measure)")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Air Velocity - Above","Assessments above this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>



    <%if (Model.ApplicationID == 114)
        { %>
    <%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Hysteresis.ascx", Model);%>
    <% } %>

    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Altitude","Altitude")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Air Velocity - Altitude","Enter the sensor's altitude in meters above sea level for greater accuracy-default 0 meters.")%>
        <hr />
    </div>

    <%if (Model.ApplicationID == 114)
        { %>
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Show Temperature","Show Temperature")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Air Velocity - Show Temperature","Adds the neasurement to the pressure reading.")%>
        <hr />
    </div>
    <% } %>
</div>

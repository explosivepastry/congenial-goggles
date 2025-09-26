<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Below","Below")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Thermocouple - Minimum Threshold","Assessments below this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>



<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Above","Above")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Thermocouple - Maximum Threshold","Assessments above this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_app086_AwareBuffer.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SyncronizeOffset.ascx", Model);%>

<%--<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Scale","Scale")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Thermocouple - Scale","This sensor supports changing the unit of measurement from Fahrenheit to Celsius or vice versa.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Calibration","Calirbration")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Thermocouple - Calibration","This sensor supports modifying the temperature calibration.")%>
        <hr />
    </div>
</div>--%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Below Aware Threshold (Pascal)","Below Aware Threshold (Pascal)")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Differential Air Pressure - Below Aware Threshold (Pascal)","Assessments below this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Above Aware Threshold (Pascal)","Above Aware Threshold (Pascal)")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Differential Air Pressure - Above Aware Threshold (Pascal)","Assessments above this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>

<%  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Hysteresis.ascx", Model);%>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Show Temperature","Show Temperature")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Differential Air Pressure - Show Temp","Adds this measurement to the pressure reading.")%>
        <hr />
    </div>
</div>

<%  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SyncronizeOffset.ascx", Model);%>

<%--<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Scale","Scale")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Differential Air Pressure - Scale","This sensor supports changing the unit of measurement from Fahrenheit to Celsius or vice versa. It also allows changing the pressure scale to Pascal, Torr, PSI inH2O, inHG, mmHg, mm Water Column, and Custom.")%>
        <hr />
    </div>
</div>--%>


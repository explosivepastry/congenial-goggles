<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Pressure - Below","Assessments below this value will cause the meter to enter the Aware State.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Above","Above")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Pressure - Above","Assessments above this value will cause the meter to enter the Aware State.")%>
        <hr />
    </div>

    <%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Hysteresis.ascx", Model);%>

    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Stabilization Delay","Stabilization Delay")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Pressure - Stabilization Delay","Determines the number of milliseconds the pressure meter allows the pressure transducer to stabilize before taking a measurement. The default is 750 milliseconds.")%>
        <hr />
    </div>

<%--    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Scale","Scale")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Pressure - Scale","This meter supports changing the unit of measurement to any of the following: PSI, atm, bar, kPA, TORR, or Custom.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Calibration","Calibration")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Pressure - Calibration","This meter supports modifying the pressure calibration.")%>
        <hr />
    </div>--%>

</div>

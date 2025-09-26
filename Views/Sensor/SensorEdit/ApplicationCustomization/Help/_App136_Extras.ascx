<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|LCD Display Timeout (seconds)","LCD Display Timeout (seconds)")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|LCD Temp - LCD Display Timeout", "The time in seconds when the LCD screen will remain active before going to sleep.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Use Aware State","Use Aware State")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|LCD Temp - Use Aware State", "A switch to toggle if you want the Aware State to trigger based on a set temperature threshold or any registered change.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Below","Below")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|LCD Temp - Minimum Threshold", "When in Threshold Mode, Assessments below this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Above","Above")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|LCD Temp - Maximum Threshold", "When in Threshold Mode, Assessments above this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>

<%--<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Delta Threshold","Delta Threshold")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|LCD Temp - Delta Threshold", "The Delta Threshold is the numerical difference between the last recorded temperature and the current recorded temperature. If the difference between these readings exceeds the Delta Threshold, the sensor will enter the Aware State.")%>
        <hr />
    </div>
</div>--%>


<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Hysteresis.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SyncronizeOffset.ascx", Model);%>

<%--<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Scale","Scale")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|LCD Temp - Scale", "This sensor supports changing the unit of measurement from Fahrenheit to Celsius or vice versa.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Calibration","Calibration")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|LCD Temp - Calibration", "This sensor supports modifying the temperature calibration.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Calibration Certificates","Calibration Certificates")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|LCD Temp - Calibration Certificates", "If a certificate is assigned to a sensor, the calibration features are disabled and hidden from view.")%>
        <hr />
    </div>
</div>--%>



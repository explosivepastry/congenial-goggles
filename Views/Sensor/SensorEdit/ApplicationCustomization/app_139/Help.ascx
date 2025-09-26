<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SensorName.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_UseWithRepeater.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_HeartBeat.ascx", Model); %>


<% Account acc = Account.Load(Model.AccountID);
    if (acc.CurrentSubscription.AccountSubscriptionType.Can("sensor_advanced_edit") && MonnitSession.CustomerCan("Sensor_Advanced_Configuration"))
   {%>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_ActiveStateInterval.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_TimeOfDay.ascx", Model);%>

<%if (new Version(Model.FirmwareVersion) < new Version("25.44.39.5")) { %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_MeasurementsPerTransmission.ascx", Model);%>
<%} %>


<%--Low Light Threshold (Min)--%>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Low Light Threshold","Low Light Threshold")%>
    </div>
    <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|PAR Light Meter - Low Light Threshold","The device will report No Light if the light level falls below this threshold. There's no change to the Aware State or behavior. Measured in micromoles per square meter per second (μmol/m²/s). ")%>
        <hr />
    </div>
</div>

<%--Saturated Light Threshold (Max)--%>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Saturated Light Threshold","Saturated Light Threshold")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|PAR Light Meter - Saturated Light Threshold","The meter will report Saturated Light and observe aware behavior if the light level is above this threshold. The device will immediately report when transitioning between different light states. Measured in micromoles per square meter per second. (μmol/m²/s)\nNote: If the meter is Aware for any other configured reason, it will still report Aware. However, it will report immediately if the meter transitions across other configured thresholds or to a different light state.")%>
        <hr />
    </div>
</div>


<%--Light Light Buffer--%>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Light Threshold Buffer","Light Threshold Buffer")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|PAR Light Meter - Light Threshold Buffer","A buffer value between measurements corresponding to a device in an Aware State and those corresponding to a device in Normal State. This stops the device from cycling between states when measuring near the maximum or the minimum limits.")%>
        <hr />
    </div>
</div>


<%if (new Version(Model.FirmwareVersion) >= new Version("25.44.39.5")) { %>


<%--DLI Start Time--%>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|DLI Reset Time","DLI Reset Time")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|PAR Light Meter - DLI Reset Time","The time of day the meter will reset the photosynthetically active radiation (PAR) daily light integral (PAR DLI) accumulation to zero. The meter will report a total 24-hour PAR accumulation at the reset time indicated. PAR DLI will reset to zero, and on the next Heartbeat, the sensor will report the PAR DLI since the reset. This configuration will not automatically adjust for daylight savings. That will require a manual adjustment if desired.\nNote: When this configuration is sent to the meter, it will reset PAR DLI.")%>
        <hr />
    </div>
</div>

<%--MeasurementInterval--%>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Measurement Interval","Measurement Interval")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|PAR Light Meter - Measurement Interval","A frequency of photosynthetic photon flux density (PPFD) measurements used to calculate PAR DLI.")%>
        <hr />
    </div>
</div>

<%--DLIThreshold--%>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|DLI Threshold","DLI Threshold")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|PAR Light Meter - DLI Threshold","Assessments above this value will cause the meter to enter the Aware State.")%>
        <hr />
    </div>
</div>

<%} %>

<%--Enable Temperature Compensation--%>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Enable Temperature Compensation","Enable Temperature Compensation")%>
    </div>
    <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization|139-Temp-Comp-HelpText","When enabled, the meter will apply temperature compensation adjustments to the transducer readings.")%>
        <br /><%: Html.TranslateTag("Note","Note")%>:
        <%: Html.TranslateTag("Sensor/ApplicationCustomization|139-Temp-Comp-HelpText2","The temperature element is in the meter’s base, not the light-sensing element on the end of the lead. If the sensor base is not close to the same temperature environment as the element on the lead’s end, we recommend turning off temperature compensation. Temperature compensation has a minimal effect at 20°C. If the environment is kept at 20°C +/- 5°C, compensation will have less than a 1% impact on the meter reading. If fast temperature swings occurring in less than 30 minutes are common in your application, we recommend that temperature compensation be turned off.")%>
        <br /><b><%: Html.TranslateTag("Note","Note")%>:</b>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization|139-DLI-Reset-HelpText","When this configuration is sent to the meter,it will reset PAR DLI.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Calibration","Calibration")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|PAR Light Meter - Calibration","This sensor supports modifying the light level calibration.")%>
        <hr />
    </div>
</div>


<%} %>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_WifiSensor.ascx", Model); %>



<div class="clearfix"></div>

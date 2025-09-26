<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Scale","Scale")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Temperature Sensor - Scale","This sensor supports changing the unit of measurement from Fahrenheit to Celsius or vice versa.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Calibration","Calibration")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Temperature Sensor - Calibration","This sensor supports modifying the temperature calibration.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Calibration Certificate","Calibration Certificate")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Temperature Sensor - Calibration Certificate","If a certificate is assigned to a sensor, the calibration features are disabled and hidden from view.")%>
        <hr />
    </div>
</div>

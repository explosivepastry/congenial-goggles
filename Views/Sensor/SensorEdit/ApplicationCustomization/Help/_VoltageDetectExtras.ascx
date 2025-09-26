<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Enter Aware State when voltage is","Enter Aware State when voltage is")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Voltage Detects - Aware State","Sets which detected activity will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Time to Re-Arm","Time to Re-Arm")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Voltage Detects - Time to Re-Arm","The time in seconds after a triggering event that the sensor will wait before recognizing additional triggering events.")%>
        <hr />
    </div>
</div>
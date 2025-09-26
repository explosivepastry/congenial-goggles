<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Enter Aware State when there is","Enter Aware State when there is")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Detection - Aware State", "Sets the device to detect when there is motion, no motion, or a state change.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Report Immediately on","Report Immediately on")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Detection - Report Immediately on", "Toggles a switch between All Stage Changes or Aware State.")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Detection - Report Immediately on", "If Aware State is selected, the time between the messasge detecting motion and the message saying no motion is (Re-Arm time + Aware State Heartbeat).")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Detection - Report Immediately on", "If set to All State Changes, the time between the two messages is (Re-Arm time + Re-Arm time). This example assumes the standard Aware on Motion State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Time to Re-Arm","Time to Re-Arm")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Detection - Time to Re-Arm", "The time in seconds after a triggering event that the sensor will wait before recognizing additional triggering events.")%>
        <hr />
    </div>
</div>



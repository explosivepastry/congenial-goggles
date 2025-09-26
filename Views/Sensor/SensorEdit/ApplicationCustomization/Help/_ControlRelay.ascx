<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Relay Title","Relay Title")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help/_ControlRelay|The display name of Relay. The default name will be Relay 1 or Relay 2.","The display name of Relay. The default name will be Relay 1 or Relay 2.")%>
        <hr />
    </div>
</div>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help/_ControlRelay|Relay Default State","Relay Default State")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Control - Default State", "Assigns the open or closed state as the default setting for the Relay so it can be adjusted in the Control tab. Off is open. On is closed. The control device will listen for messages from iMonnit via a gateway. If a sensor reading meets the Aware State conditions, the Relay automatically switches to the non-default state. When the sensor reports a reading outside the Aware State conditions, the Relay will switch back to its default state.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Relay 2 Visible","Relay 2 Visible")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help/_ControlRelay|Control - Relay 2 Visible","Dertermines whether to show or hide the state of Relay 2.")%>
        <hr />
    </div>
</div>

<%if (Model.ApplicationID != 158)
    {%>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help/_ControlRelay|Paired Sensor ID", "Paired Sensor ID")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help/_ControlRelay|Control - Paired Sensor ID", "The Sensor ID of the device that will toggle the Relay to a non-default state when the sensor is in the Aware State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help/_ControlRelay|Control", "Control")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help/_ControlRelay|Control - Control", "List of relay commands for the control unit to send.")%>
        <hr />
    </div>
</div>
<%} %>

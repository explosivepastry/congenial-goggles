<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Behavior Mode","Behavior Mode")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Data is only delivered and or logged while sensor is in Study Mode.","Data is only delivered and or logged while sensor is in Study Mode.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Backlog Delivery","Backlog Delivery")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Only 5 devices per gateway are permitted to be in Priority mode.","Only 5 devices per gateway are permitted to be in Priority mode.")%> 
        <br />
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|In standard mode sensor will deliver up to 3 messages for each heartbeat.","In standard mode sensor will deliver up to 3 messages for each heartbeat.")%>
        <br />
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|In priority mode sensor will deliver up to 3 messages every 15 seconds.","In priority mode sensor will deliver up to 3 messages every 15 seconds.")%>
        <hr />
    </div>
</div>




<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Aware State Heartbeat (Minutes)","Aware State Heartbeat (Minutes)")%>
    </div>
     <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|The expected time between communication from the device to the gateway while in the Aware State.","The expected time between communication from the device to the gateway while in the Aware State.")%>
        <hr />
    </div>
</div>

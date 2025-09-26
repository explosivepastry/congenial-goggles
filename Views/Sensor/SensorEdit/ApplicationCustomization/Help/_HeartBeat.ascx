<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Heartbeat Interval (Minutes)","Heartbeat Interval (Minutes)")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|The expected time between communication from the device to the gateway while in the Normal State and the absence of any data-triggering events.","The expected time between communication from the device to the gateway while in the Normal State and the absence of any data-triggering events.")%>
        <hr />
    </div>
</div>



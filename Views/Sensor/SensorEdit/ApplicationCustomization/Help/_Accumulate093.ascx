<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>
     <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|With Accumulate off, only the Amp Hours/Watt Hours consumed during a single heartbeat will be reported. With accumulate on, the energy consumed during the current heartbeat will be added to the energy consumed during the previous heartbeats.",
        "With accumulate off, only the Amp Hours/Watt Hours consumed during a single heartbeat will be reported. With accumulate on, the energy consumed during the current heartbeat will be added to the energy consumed during the previous heartbeats.")%>
        <hr />
    </div>
</div>



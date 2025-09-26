<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_OnAwareMessages|On Aware Messages","On Aware Messages")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Gateway/_OnAwareMessages|Configuration indicates if the Aware Message arrival event will Trigger Heartbeat (default) or Wait for Heartbeat. When the switch is toggled to Trigger Heartbeat, the gateway is configured to immediately report to the server. When toggled to Wait for Heartbeat, messages are stored until the gateway is scheduled to communicate before connection with the Primary Server.","Configuration indicates if the Aware Message arrival event will Trigger Heartbeat (default) or Wait for Heartbeat. When the switch is toggled to Trigger Heartbeat, the gateway is configured to immediately report to the server. When toggled to Wait for Heartbeat, messages are stored until the gateway is scheduled to communicate before connection with the Primary Server.")%>
        <hr />
    </div>
</div>




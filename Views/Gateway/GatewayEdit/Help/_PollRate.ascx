<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Poll Rate","Poll Rate")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Gateway/_PollRate|Configures the interval that the gateway periodically checks in with the server. If the server has urgent commands or notification for wireless sensors, the Primary Server will signal the gateway for a full data dialog (Heartbeat). The default is 0 minutes, meaning the gateway poll feature is disabled.","Configures the interval that the gateway periodically checks in with the server. If the server has urgent commands or notification for wireless sensors, the Primary Server will signal the gateway for a full data dialog (Heartbeat). The default is 0 minutes, meaning the gateway poll feature is disabled.")%>
        <hr />
    </div>
</div>



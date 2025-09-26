<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_OnServerLoss|On Server Loss","On Server Loss")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Gateway/_OnServerLoss|Configuration indicates if the wireless network on the gateway will stay active and Log Sensor Data (default) or if the gateway will Disable Wireless network. In networks with multiple gateways, forcing the sensors to switch to an active gateway will enable more timely delivery of data to the server.","Configuration indicates if the wireless network on the gateway will stay active and Log Sensor Data (default) or if the gateway will Disable Wireless network. In networks with multiple gateways, forcing the sensors to switch to an active gateway will enable more timely delivery of data to the server.")%>
        <hr />
    </div>
</div>




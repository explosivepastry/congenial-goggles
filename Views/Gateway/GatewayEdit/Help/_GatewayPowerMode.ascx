<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_GatewayPowerMode|Gateway Power Mode","Gateway Power Mode")%>
    </div>
     <div class="word-def" >
    <%: Html.TranslateTag("Gateway/_GatewayPowerMode|Enables the selection between Standard power (default), Force Low Power, and Force High Power, from a drop-down menu. Standard means that the gateway will keep lights and cellular transmission active when plugged into an outlet. On battery power, the gateway will power down lights and the cellular connection between communications. Force Low Power means the gateway will always power down the lights and the cellular connection when not talking to the server. Force High Power means the gateway will keep the lights and cellular module active, regardless of whether or not the gateway is plugged in.", "Enables the selection between Standard power (default), Force Low Power, and Force High Power, from a drop-down menu. Standard means that the gateway will keep lights and cellular transmission active when plugged into an outlet. On battery power, the gateway will power down lights and the cellular connection between communications. Force Low Power means the gateway will always power down the lights and the cellular connection when not talking to the server. Force High Power means the gateway will keep the lights and cellular module active, regardless of whether or not the gateway is plugged in.")%>
        <hr />
    </div>
</div>



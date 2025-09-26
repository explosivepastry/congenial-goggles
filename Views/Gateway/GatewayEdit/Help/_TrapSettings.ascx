<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row">

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|Traps","Traps")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|Choosing 'Enable' brings up selections for on Authentication Failure, on New Sensor Data, and on Sensor Alarms. Your Trap Address is the IP Address for the SNMP Server where the trap will be sent. Your Trap Port is the server port where the trap alert state is sent when active.", "Choosing 'Enable' brings up selections for on Authentication Failure, on New Sensor Data, and on Sensor Alarms. Your Trap Address is the IP Address for the SNMP Server where the trap will be sent. Your Trap Port is the server port where the trap alert state is sent when active.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|Trap IP Address","Trap IP Address")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|The IP Address for the SNMP Server where the trap will be sent.", "The IP Address for the SNMP Server where the trap will be sent.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|Trap Port","Trap Port")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|The server port where the trap alert state is sent when active.", "The server port where the trap alert state is sent when active.")%>
        <hr />
    </div>

</div>




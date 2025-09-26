<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|Interface Settings","Interface Settings")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|SNMP Interface","SNMP Interface")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|Simple Network Management Protocol is an Internet application protocol that manages and monitors network device functionality. Monnit uses SNMP version 1. These settings can both be configured on iMonnit and the local interface.", "Simple Network Management Protocol is an Internet application protocol that manages and monitors network device functionality. Monnit uses SNMP version 1. These settings can both be configured on iMonnit and the local interface.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|Modbus Interface","Modbus Interface")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|Modbus TCP (Transmission Control Protocol) is the Modbus RTU protocol with a TCP interface that runs on Ethernet. Monnit provides the Modbus TCP interface for you to pull gateway and sensor data. You can use Modbus without the server interface active. The data will not be sent to a server, but you can continue to poll for new data as it is received by the gateway.", "Modbus TCP (Transmission Control Protocol) is the Modbus RTU protocol with a TCP interface that runs on Ethernet. Monnit provides the Modbus TCP interface for you to pull gateway and sensor data. You can use Modbus without the server interface active. The data will not be sent to a server, but you can continue to poll for new data as it is received by the gateway.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|SNTP Interface","SNTP Interface")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|SNTP is a synchronized computer clock on a network. An SNTP server can be set up on the same LAN as the gateway, such as on a router or a Linux computer. The gateway should be configured to retrieve time from only trusted servers, such as ones maintained by your ISP. Incorrect time can affect the delivery or sensor traffic. If the Monnit Server is active, it will be utilized for time synchronization in ordinary operation. So SNTP will be used as a backup. If you disable the default server interface, you must configure the SNTP Interface.", "SNTP is a synchronized computer clock on a network. An SNTP server can be set up on the same LAN as the gateway, such as on a router or a Linux computer. The gateway should be configured to retrieve time from only trusted servers, such as ones maintained by your ISP. Incorrect time can affect the delivery or sensor traffic. If the Monnit Server is active, it will be utilized for time synchronization in ordinary operation. So SNTP will be used as a backup. If you disable the default server interface, you must configure the SNTP Interface.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|Inbound IP Range","Inbound IP Range")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|This is the IP address for the SNMP client. If you have one device to communicate with, the start and end IP addresses will be the same. Exchanging information with multiple machines will require a set of different start and end IP addresses.", "This is the IP address for the SNMP client. If you have one device to communicate with, the start and end IP addresses will be the same. Exchanging information with multiple machines will require a set of different start and end IP addresses.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|Inbound Port","Inbound Port")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|This is the number for where specifically in the server data from the gateway is received.", "This is the number for where specifically in the server data from the gateway is received.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|Community String","Community String")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|This is used as a configurable password for clients within the accepted IP Range. Communication will not be allowed if the Community String does not match. The default will be set to 'public'.", "This is used as a configurable password for clients within the accepted IP Range. Communication will not be allowed if the Community String does not match. The default will be set to 'public'.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|HTTP Interface","HTTP Interface")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|Allows you to set how long you wish the local interface to be active before being automatically disabled. For increased Security, you may configure the local HTTP interface to remain Read Only, or to be disabled after 1 minute, 5 minutes, 30 minutes, or always active.", "Allows you to set how long you wish the local interface to be active before being automatically disabled. For increased Security, you may configure the local HTTP interface to remain Read Only, or to be disabled after 1 minute, 5 minutes, 30 minutes, or always active.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|Configuration Timeout","Configuration Timeout")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Gateway/_InterfaceSettings|Sets the amount of time the HTTP Interface may be used to change settings on the IoT Gateway after startup or a utility button press. Options are 'Read Only' (default), 1 minute, 5 minutes, 30 minutes, or Always Available. After this time, the HTTP Interface is only available to display status and settings information.", "Sets the amount of time the HTTP Interface may be used to change settings on the IoT Gateway after startup or a utility button press. Options are 'Read Only' (default), 1 minute, 5 minutes, 30 minutes, or Always Available. After this time, the HTTP Interface is only available to display status and settings information.")%>
        <hr />
    </div>

</div>

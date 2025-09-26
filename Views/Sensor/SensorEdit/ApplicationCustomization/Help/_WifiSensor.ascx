<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% var WifiGateway = Gateway.LoadBySensorID(Model.SensorID);

    if (Model.IsWiFiSensor && WifiGateway != null && WifiGateway.GatewayType != null)
    { %>

<%if (Model.SensorTypeID != 8)
    {%>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Data Logging","Data Logging")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Data logging will reduce battery life.  The last 50 readings are logged even when this is off if there is a disruption in internet service.  This is for extended periods without connectivity, such as monitoring during shipping or other.","Data logging will reduce battery life.  The last 50 readings are logged even when this is off if there is a disruption in internet service.  This is for extended periods without connectivity, such as monitoring during shipping or other.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Wi-Fi Configuration","Wi-Fi Configuration")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|By clicking the clear Wi-Fi Settings button your sensor will reset the current configuration back to the default configuration.")%>
        <hr />
    </div>
</div>

<%} %>

<%if (Model.SensorTypeID != 8)
    { %>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Always Flash LED","Always Flash LED")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Turning the LED on will significantly reduce battery life (by up to 1/2).","Turning the LED on will significantly reduce battery life (by up to 1/2).")%>
        <hr />
    </div>
</div>
<%}%>


<% if (WifiGateway.GatewayType.SupportsHostAddress)
    { %>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Server Host Address","Server Host Address")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default value: sensorsgateway.com","Default value: sensorsgateway.com")%>
        <br />
        <br />
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|the location of the monitoring software that the sensor communicates with.","the location of the monitoring software that the sensor communicates with.")%>
        <hr />
    </div>
</div>
<%} %>

<% if (WifiGateway.GatewayType.SupportsHostPort)
    { %>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Server Communication Port","Server Communication Port")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default value: 3000","Default value: 3000")%>
        <br />
        <br />
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|the port used for the communication to the monitoring software.","the port used for the communication to the monitoring software.")%>
        <hr />
    </div>
</div>
<%} %>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sensor IP Address","Sensor IP Address")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Determines if the sensor uses DHCP to obtain an IP address.","Determines if the sensor uses DHCP to obtain an IP address.")%>
        <hr />
    </div>
</div>

<div class="row dhcpSettings">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Static IP Address","IP Address")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|IP address the sensor will use on the network it joins.","IP address the sensor will use on the network it joins.")%>
        <hr />
    </div>
</div>

<div class="row dhcpSettings">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Network Mask","Subnet Mask")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Subnet mask the of the network the sensor will belong to.","Subnet mask the of the network the sensor will belong to.")%>
        <hr />
    </div>
</div>

<div class="row dhcpSettings">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default Gateway","Default Gateway")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|IP address of the device serving as the gateway to the internet. Typically your Wi-Fi enabled router.","IP address of the device serving as the gateway to the internet. Typically your Wi-Fi enabled router.")%>
        <hr />
    </div>
</div>

<div class="row dhcpSettings">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default DNS Server","DNS Server")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|IP address of the device that resolves DNS queries for the network. Often your Wi-Fi enabled router.","IP address of the device that resolves DNS queries for the network. Often your Wi-Fi enabled router.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Primary Wi-Fi Network","Primary Wi-Fi Network")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Primary Wi-Fi Network","The network your device will always seek to connect to first.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Secondary/Tertiary Networks","Secondary/Tertiary Networks")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Secondary/Tertiary Networks","Allows the device to connect to other networks when the primary network is not available.")%>
        <hr />
    </div>
</div>

<%} %>



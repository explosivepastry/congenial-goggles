<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: GatewayAssignedToNotificaiton</b><br />
    Returns the list of Gateways that belongs to user based on the notification they are assigned to.<br />

    <h4>Parameters</h4>
    <table>
        <tr>
            <td>notificationID:</td>
            <td>Long</td>
            <td>Filters list to Gateways that belong to this notification id</td>
        </tr>
    </table>

    <h4>Example</h4>
<%--    <a href="/xml/GatewayAssignedToNotificaiton/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3" target="_blank">https://<%:Request.Url.Host %>/xml/GatewayAssignedToNotificaiton/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3</a>--%>
    <br />
    Only gateways that contain a notification id of 3 are returned
    <br />

<input type="button" id="btn_TryAPI_GatewayAssignedToNotificaiton" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_GatewayAssignedToNotificaiton').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "GatewayAssignedToNotificaiton",
					"params": [
                        { "name": "notificationID", "type": "Long", "description": "Filters list to Gateways that belong to this notification id", "optional": false },
					]
				};								
				APITest(json);
			});
		});
    </script>    
                
      <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto; max-width: 835px;">
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;GatewayAssignedToNotificaiton&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIGatewayList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIGateway GatewayID="2599" NetworkID="100" Name="Building2 (103) gateway" GatewayType="Ethernet_Gateway" Heartbeat="5" IsDirty="False" LastCommunicationDate="1/11/2011 2:57:36 PM" LastInboundIPAddress="" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIGateway GatewayID="100" NetworkID="100" Name="Building3 (Gateway) 100" GatewayType="usb" Heartbeat="5" IsDirty="False" LastCommunicationDate="2/6/2011 4:30:44 PM" LastInboundIPAddress="" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIGateway GatewayID="901" NetworkID="100" Name="WiFi Gateway(WIFI ActiveID) Building1" GatewayType="Wifi_Sensor" Heartbeat="15" IsDirty="True" LastCommunicationDate="2/6/2011 4:18:40 PM" LastInboundIPAddress="" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIGatewayList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>

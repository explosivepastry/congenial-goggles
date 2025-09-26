<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationAssignGateway</b><br />
    Asssigns a gateway to a specific notification.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>NotificationID:</td>
            <td>Long</td>
            <td>Unique identifier of the Notification</td>
        </tr>
        <tr>
            <td>GatewayID:</td>
            <td>Long</td>
            <td>Identifier of gateway to assign</td>
        </tr>
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/NotificationAssignGateway/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&gatewayID=123" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationAssignGateway/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&amp;gatewayID=123</a>--%>

<input type="button" id="btn_TryAPI_NotificationAssignGateway" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationAssignGateway').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationAssignGateway",
					"params": [
                        { "name": "NotificationID", "type": "Long", "description": "Unique identifier of the Notification", "optional": false },
                        { "name": "toDate", "type": "Long", "description": "Identifier of gateway to assign", "optional": false }						
					]
				};								
				APITest(json);
			});
		});
    </script>    
              
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;NotificationAssignGateway&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIGatewayList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIGateway GatewayID="123" NetworkID="100" Name="Building2 (103) gateway" GatewayType="Ethernet_Gateway" Heartbeat="5" IsDirty="False" LastCommunicationDate="1/11/2011 2:57:36 PM" LastInboundIPAddress="" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIGatewayList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
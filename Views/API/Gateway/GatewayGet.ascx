<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<h3>
    GatewayGet</h3>
<div class="methodDiv">
    <b>Method: GatewayGet</b><br />
    Returns the gateway object.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>gatewayID:</td>
            <td>Integer</td>
            <td>Unique identifier of the gateway</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/GatewayGet/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=1234" target="_blank">https://<%:Request.Url.Host %>/xml/GatewayGet/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=1234</a>--%>
    <input type="button" id="btn_TryAPI_GatewayGet" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_GatewayGet').click(function () {
				var json =
				{
					"auth": true,					
					"api": "GatewayGet",
					"params": [
						{ "name": "gatewayID", "type": "Integer", "description": "Unique identifier of the gateway", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;GatewayGet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&lt;APIGateway GatewayID="1234" NetworkID="100" Name="(103) gateway" GatewayType="Ethernet_Gateway" Heartbeat="5" IsDirty="False" LastCommunicationDate="1/11/2011 2:57:36 PM" LastInboundIPAddress="" MacAddress="" IsUnlocked="False" CheckDigit="XXXX" AccountID="123"  SignalStrength="31" BatteryLevel="84" ResetInterval="168" GatwayPowerMode="Force_High_Power"/> /&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
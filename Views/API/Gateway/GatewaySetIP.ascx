<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<div class="methodDiv">
	<b>Method: GatewaySetIP</b><br />
	<br />
	Set the IP address, Subnet Mask, Default Gateway, and DNS Server of a gateway.<br />

	<h4>Parameters</h4>
	<table>
		<tr>
			<td>GatewayID:</td>
			<td>Integer</td>
			<td>Unique identifier of the gateway.</td>
		</tr>
		<tr>
			<td>IPAddress:</td>
			<td>String</td>
			<td>This is the IP address assigned to the gateway. Setting the IP address to 0.0.0.0 will force the gateway to use DHCP.</td>
		</tr>
		<tr>
			<td>NetworkMask:</td>
			<td>String</td>
			<td>This is the Subnet ID of the network.</td>
		</tr>
		<tr>
			<td>DefaultGateway:</td>
			<td>String</td>
			<td>IP address of the default router.</td>
		</tr>
		<tr>
			<td>DNSServer:</td>
			<td>String</td>
			<td>This is the IP address of the DNS server to use.</td>
		</tr>
	</table>

	<h4>Example</h4>
	<%--<a href="/xml/GatewaySetIP/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=12345&ipAddress=192.168.100.10&networkMask=255.255.255.0&defaultGateway=192.168.100.1&dnsServer=75.75.75.0" target="_blank">https://<%:Request.Url.Host %>/xml/GatewaySetIP/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=12345&amp;ipAddress=192.168.100.10&amp;networkMask=255.255.255.0&amp;defaultGateway=192.168.100.1&amp;dnsServer=75.75.75.0"</a>--%>
	<input type="button" id="btn_TryAPI_GatewaySetIP" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_GatewaySetIP').click(function () {
				var json =
				{
					"auth": true,					
					"api": "GatewaySetIP",
					"params": [
						{ "name": "GatewayID", "type": "Integer", "description": "Unique identifier of the gateway", "optional": false },
						{ "name": "IPAddress", "type": "String", "description": "This is the IP address assigned to the gateway. Setting the IP address to 0.0.0.0 will force the gateway to use DHCP", "optional": false },
						{ "name": "NetworkMask", "type": "String", "description": "This is the Subnet ID of the network", "optional": false },
						{ "name": "DefaultGateway", "type": "String", "description": "IP address of the default router", "optional": false },
						{ "name": "DNSServer", "type": "String", "description": "This is the IP address of the DNS server to use", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;GatewaySetIP&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;New host set after pending command.&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>

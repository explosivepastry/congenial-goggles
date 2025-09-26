<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<h3>GatewayList</h3>
<div class="methodDiv">
    <b>Method: GatewayList</b><br />
    Returns the list of gateways that belongs to user.<br />

    <h4>Parameters</h4>
    <table>

        <tr>
            <td>Name:</td>
            <td>String (optional)</td>
            <td>Filters list to gateways with names containing this string. (case-insensitive)</td>
        </tr>
        <tr>
            <td>NetworkID:</td>
            <td>Integer (optional)</td>
            <td>Filters list to gateway that belong to this network id</td>
        </tr>  
        <%--<%if (ViewBag.ShowResellerParameters == true)
          {%>--%>
        <tr>
            <td>accountID: </td>
            <td>Integer (optional)</td>
            <td>Unique identifier of SubAccount. If omitted,  Your default account will be used.</td>
        </tr>
        <%--<%} %>--%>
    </table>

    <h4>Example</h4>
    <%--<a href="/xml/GatewayList/Z3Vlc3Q6cGFzc3dvcmQ=?name=Building" target="_blank">https://<%:Request.Url.Host %>/xml/GatewayList/Z3Vlc3Q6cGFzc3dvcmQ=?name=Building</a>
    <br />
    Only gateways that contain "Building" in the name are returned--%>
    <input type="button" id="btn_TryAPI_GatewayList" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_GatewayList').click(function () {
				var json =
				{
					"auth": true,					
					"api": "GatewayList",
					"params": [
                        { "name": "Name", "type": "String", "description": "Filters list to gateways with names containing this string. (case-insensitive)", "optional": true },
						{ "name": "NetworkID", "type": "Integer", "description": "Filters list to gateway that belong to this network id", "optional": true },
						{ "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount. If omitted,  Your default account will be used.", "optional": true }
					]
				};								
				APITest(json);
			});
		});
	</script>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto;max-width:835px;">
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;GatewayList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIGatewayList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIGateway GatewayID="2599" NetworkID="100" Name="Building2 (103) gateway" GatewayType="Ethernet_Gateway" Heartbeat="5" IsDirty="False" LastCommunicationDate="1/11/2011 2:57:36 PM" LastInboundIPAddress="" MacAddress="123456789|3123456789" IsUnlocked="False" CheckDigit="XXXX" AccountID="123" SignalStrength="31" BatteryLevel="84" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIGateway GatewayID="100" NetworkID="100" Name="Building3 (Gateway) 100" GatewayType="usb" Heartbeat="5" IsDirty="False" LastCommunicationDate="2/6/2011 4:30:44 PM" LastInboundIPAddress=""  MacAddress="123456789|3123456789" IsUnlocked="False" CheckDigit="XXXX" AccountID="123" SignalStrength="31" BatteryLevel="84"  /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIGateway GatewayID="901" NetworkID="100" Name="WiFi Gateway(WIFI ActiveID) Building1" GatewayType="Wifi_Sensor" Heartbeat="15" IsDirty="True" LastCommunicationDate="2/6/2011 4:18:40 PM" LastInboundIPAddress=""  MacAddress="123456789|3123456789" IsUnlocked="False" CheckDigit="XXXX" AccountID="123" SignalStrength="31" BatteryLevel="84"  /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIGatewayList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>

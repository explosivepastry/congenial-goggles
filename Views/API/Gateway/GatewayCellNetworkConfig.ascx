<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<h3>
    GatewayCellNetworkConfig</h3>
<div class="methodDiv">
    <b>Method: GatewayCellNetworkConfig</b><br />
    Sets APN settings of a gateway. You may include or omit arguments as desired, except for the GatewayID.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>GatewayID:</td>
            <td>Integer</td>
            <td>Unique identifier of the gateway</td>
        </tr>
        <tr>
            <td>CellAPNName:</td>
            <td>String</td>
            <td>APN Name for the Gateway.</td>
        </tr>
        <tr>
            <td>Username:</td>
            <td>String</td>
            <td>APN username for the Gateway.</td>
        </tr>
        <tr>
            <td>Password:</td>
            <td>String</td>
            <td>APN password for the Gateway.</td>
        </tr>
        <tr>
            <td>GatewayDNS:</td>
            <td>String</td>
            <td>Primary DNS.</td>
        </tr>
        <tr>
            <td>SecondaryDNS:</td>
            <td>String</td>
            <td>Secondary DNS.</td>
        </tr>
        <tr>
            <td>PollInterval:</td>
            <td>Double</td>
            <td>Interval for gateway communication to keep connection open.</td>
        </tr>
        <tr>
            <td>GPSReportInterval:</td>
            <td>Double</td>
            <td>Interval for gateway to send its location</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/GatewayCellNetworkConfig/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=12345&PollInterval=2.1&CellAPNName=mynetwork&username=user&password=password" target="_blank">https://<%:Request.Url.Host %>/xml/GatewayCellNetworkConfig/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=12345&PollInterval=2.1&CellAPNName=mynetwork&username=user&password=password</a>--%>
    <input type="button" id="btn_TryAPI_GatewayCellNetworkConfig" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_GatewayCellNetworkConfig').click(function () {
				var json =
				{
					"auth": true,					
					"api": "GatewayCellNetworkConfig",
					"params": [
                        { "name": "GatewayID", "type": "Integer", "description": "Unique identifier of the gateway", "optional": false },
						{ "name": "CellAPNName", "type": "String", "description": "APN Name for the Gateway", "optional": false },
						{ "name": "Username", "type": "String", "description": "APN username for the Gateway", "optional": false },
						{ "name": "Password", "type": "String", "description": "APN password for the Gateway", "optional": false },
						{ "name": "GatewayDNS", "type": "String", "description": "Primary DNS", "optional": false },
						{ "name": "SecondaryDNS", "type": "String", "description": "Secondary DNS", "optional": false },
						{ "name": "PollInterval", "type": "Double", "description": "Interval for gateway communication to keep connection open", "optional": false },
						{ "name": "GPSReportInterval", "type": "Double", "description": "Interval for gateway to send its location", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;GatewayCellNetworkConfig&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<h3>
    GatewayPoint</h3>
<div class="methodDiv">
    <b>Method: GatewayPoint</b><br />
    Sets the host address and port of a gateway.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>GatewayID:</td>
            <td>Integer</td>
            <td>Unique identifier of the gateway</td>
        </tr>
        <tr>
            <td>HostAddress:</td>
            <td>String</td>
            <td>Address of the host server.</td>
        </tr>
        <tr>
            <td>Port:</td>
            <td>Integer</td>
            <td>Port of the host server.</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/GatewayPoint/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=12345&HostAddress=192.168.1.150&port=3000" target="_blank">https://<%:Request.Url.Host %>/xml/GatewayPoint/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=12345&HostAddress=192.168.1.150&port=3000</a>--%>
    <input type="button" id="btn_TryAPI_GatewayPoint" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_GatewayPoint').click(function () {
				var json =
				{
					"auth": true,					
					"api": "GatewayPoint",
					"params": [
                        { "name": "GatewayID", "type": "Integer", "description": "Unique identifier of the gateway", "optional": false },
						{ "name": "HostAddress", "type": "String", "description": "Address of the host server", "optional": false },
						{ "name": "Port", "type": "Integer", "description": "Port of the host server", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;GatewayPoint&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Set pending command.&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
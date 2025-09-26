<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<div class="methodDiv">
    <b>Method: GatewaySetHost</b><br />
    <b>*Special Case: No Authorization Token Required*</b><br />
    Set the host address and port of a gateway.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>GatewayID:</td>
            <td>Integer</td>
            <td>Unique identifier of the gateway</td>
        </tr>
        <tr>
            <td>CheckDigit:</td>
            <td>String</td>
            <td>Check digit to prevent unauthorized movement of gateways</td>
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
        <tr>
            <td>LockDirty:</td>
            <td>Bool</td>
            <td>Force the gateway to always send config when talking to "this" server.</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/GatewaySetHost?gatewayID=12345&CheckDigit=XXXXX&HostAddress=192.168.1.123&port=3000" target="_blank">https://<%:Request.Url.Host %>/xml/GatewaySetHost?gatewayID=12345&amp;CheckDigit=XXXXX&amp;HostAddress=192.168.1.123&amp;port=3000</a>--%>
    <input type="button" id="btn_TryAPI_GatewaySetHost" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_GatewaySetHost').click(function () {
				var json =
				{
					"auth": true,					
					"api": "GatewaySetHost",
					"params": [
						{ "name": "GatewayID", "type": "Integer", "description": "Unique identifier of the gateway", "optional": false },						
						{ "name": "CheckDigit", "type": "String", "description": "Check digit to prevent unauthorized movement of gateways", "optional": false },
						{ "name": "HostAddress", "type": "String", "description": "Address of the host server", "optional": false },
                        { "name": "Port", "type": "Integer", "description": "Port of the host server", "optional": false },
                        { "name": "LockDirty", "type": "Boolean", "description": "Force the gateway to always send config when talking to this server.", "optional": true }
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
&nbsp;&nbsp;&lt;Method&gt;GatewaySetHost&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;New host set after pending command.&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
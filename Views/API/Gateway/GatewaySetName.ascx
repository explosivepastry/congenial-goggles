<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: GatewaySetName</b><br />
    Sets the display name of the gateway<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>gatewayID:</td>
            <td>Integer</td>
            <td>Unique identifier of the gateway</td>
        </tr>
        <tr>
            <td>gatewayName:</td>
            <td>String</td>
            <td>Name to give the gateway</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/GatewaySetName/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=101&gatewayName=MyGateway" target="_blank">https://<%:Request.Url.Host %>/xml/GatewaySetName/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=101&amp;gatewayName=MyGateway</a>--%>
    <input type="button" id="btn_TryAPI_GatewaySetName" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_GatewaySetName').click(function () {
				var json =
				{
					"auth": true,					
					"api": "GatewaySetName",
					"params": [
                        { "name": "gatewayID", "type": "Integer", "description": "Unique identifier of the gateway", "optional": false },
						{ "name": "gatewayName", "type": "String", "description": "Name to give the gateway", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;GatewaySetName&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
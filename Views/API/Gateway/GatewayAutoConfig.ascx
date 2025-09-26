<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<h3>
    GatewayAutoConfig</h3>
<div class="methodDiv">
    <b>Method: GatewayAutoConfig</b><br />
     Sets a pending command for the gateway to go into AutoConfig Mode for the number of minutes specifies. If the number of minutes is not provided, the gateway's stored value will be used.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>gatewayID:</td>
            <td>Integer</td>
            <td>Unique identifier of the gateway</td>
        </tr>
        <tr>
            <td>AutoConfigTime:</td>
            <td>Integer</td>
            <td>Number of minutes the gateway will be in AutoConfig mode.</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/GatewayAutoConfig/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=001234&AutoConfigTime=5" target="_blank">https://<%:Request.Url.Host %>/xml/GatewayAutoConfig/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=001234&AutoConfigTime=5</a>--%>
    <input type="button" id="btn_TryAPI_GatewayAutoConfig" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_GatewayAutoConfig').click(function () {
				var json =
				{
					"auth": true,					
					"api": "GatewayAutoConfig",
					"params": [
                        { "name": "gatewayID", "type": "Integer", "description": "Unique identifier of the gateway", "optional": false },
						{ "name": "AutoConfigTime", "type": "Integer", "description": ">Number of minutes the gateway will be in AutoConfig mode.", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;GatewayAutoConfig&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
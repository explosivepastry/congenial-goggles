<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<h3>
    GatewaySetResetInterval</h3>
<div class="methodDiv">
    <b>Method: GatewaySetResetInterval</b><br />
     Sets a pending command for the gateway to go into AutoConfig Mode for the number of minutes specifies. If the number of minutes is not provided, the gateway's stored value will be used.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>gatewayID:</td>
            <td>Integer</td>
            <td>Unique identifier of the gateway</td>
        </tr>
        <tr>
            <td>ResetInterval:</td>
            <td>Integer</td>
            <td>Number of hours the gateway will wait before resetting</td>
        </tr>
           <tr>
            <td>GatewayPowerMode:</td>
            <td>string</td>
            <td>Gateway power mode when on backup battery (force_high_power, force_low_power , standard)</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/GatewayAutoConfig/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=001234&AutoConfigTime=5" target="_blank">https://<%:Request.Url.Host %>/xml/GatewayAutoConfig/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=001234&AutoConfigTime=5</a>--%>
    <input type="button" id="btn_TryAPI_GatewaySetResetInterval" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
            $('#btn_TryAPI_GatewaySetResetInterval').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "GatewaySetResetInterval",
					"params": [
                        { "name": "gatewayID", "type": "Integer", "description": "Unique identifier of the gateway", "optional": false },
                        { "name": "ResetInterval", "type": "Integer", "description": ">Number of hours the gateway will wait before resetting", "optional": true }
                        { "name": "GatewayPowerMode", "type": "string", "description": ">Gateway power mode when on backup battery (force_high_power, force_low_power , standard)", "optional": true }
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
&nbsp;&nbsp;&lt;Method&gt;GatewaySetResetInterval&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
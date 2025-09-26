<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<h3>GatewayClearHost</h3>
<div class="methodDiv">
    <b>Method: GatewayClearHost</b><br />
    <b>*Special Case: No Authorization Token Required*</b><br />
    Resets the host address and port of a gateway to factory defaults.<br />
    
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
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/GatewayClearHost?gatewayID=12345&CheckDigit=XXXXX" target="_blank">https://<%:Request.Url.Host %>/xml/GatewayClearHost?gatewayID=12345&amp;CheckDigit=XXXXX</a>--%>
    <input type="button" id="btn_TryAPI_GatewayClearHost" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_GatewayClearHost').click(function () {
				var json =
				{
					"auth": false,					
					"api": "GatewayClearHost",
					"params": [
                        { "name": "GatewayID", "type": "Integer", "description": "Unique identifier of the gateway", "optional": false },
						{ "name": "CheckDigit", "type": "String", "description": "Check digit to prevent unauthorized movement of gateways", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;GatewayClearHost&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Host reset to default after pending command.&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
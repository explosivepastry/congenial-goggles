<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: GatewayLookUp</b><br />
    Returns the Gateway meta data.<br />

    <h4>Parameters</h4>
    <table>
        <tr>
            <td>GatewayID:</td>
            <td>Integer</td>
            <td>Unique identifier of the Gateway</td>
        </tr>
        <tr>
            <td>CheckDigit:</td>
            <td>String</td>
            <td>Check digit to prevent unauthorized access to Gateway</td>
        </tr>
    </table>

    <h4>Example</h4>
    <%--<a href="/xml/GatewayLookUp?gatewayID=1234&CheckDigit=XXXXXX" target="_blank">https://<%:Request.Url.Host %>/xml/GatewayLookUp?gatewayID=1234&amp;CheckDigit=XXXXXX</a>--%>
      <input type="button" id="btn_TryAPI_GatewayLookUp" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_GatewayLookUp').click(function () {
				var json =
				{
					"auth": false,					
					"api": "GatewayLookUp",
					"params": [
						{ "name": "GatewayID", "type": "Integer", "description": "Unique identifier of the Gateway", "optional": false },
						{ "name": "CheckDigit", "type": "String", "description": "Check digit to prevent unauthorized access to Gateway", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;GatewayLookUp&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIGatewayMetaData  GatewayID="1234"  GatewayTypeID="2" RadioBand="900 MHz" APNFirmwareVersion="2.5.1.2" GatewayFirmwareVersion="3.3.0.2" GenerationType="Gen1" /&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>

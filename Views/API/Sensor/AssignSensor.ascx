<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: AssignSensor</b><br />
    Assigns sensor to the specified network<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>NetworkID:</td>
            <td>Integer</td>
            <td>Identifier of network on your account</td>
        </tr>
        <tr>
            <td>SensorID:</td>
            <td>Integer</td>
            <td>Identifier of sensor to move</td>
        </tr>
        <tr>
            <td>CheckDigit:</td>
            <td>String</td>
            <td>Check digit to prevent unauthorized movement of sensors</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/AssignSensor/Z3Vlc3Q6cGFzc3dvcmQ=?networkID=2&sensorID=101&checkDigit=AAAAAA" target="_blank">https://<%:Request.Url.Host %>/xml/AssignSensor/Z3Vlc3Q6cGFzc3dvcmQ=?networkID=2&amp;sensorID=101&amp;checkDigit=AAAAAA</a>--%>
     <input type="button" id="btn_TryAPI_AssignSensor" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_AssignSensor').click(function () {
				var json =
				{
					"auth": true,					
					"api": "AssignSensor",
                    "params": [
						{ "name": "NetworkID", "type": "Integer", "description": "Identifier of network on your account", "optional": false },
                        { "name": "SensorID", "type": "Integer", "description": "Unique identifier of the sensor", "optional": false },
						{ "name": "CheckDigit", "type": "String", "description": "Check Digit required if sensor not already on your account", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;AssignSensor&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>

</div>
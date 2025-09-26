<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorSetThreshold</b><br />
    Sets the thresholds of the sensor that activate Aware State<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
        <tr>
            <td>measurementsPerTransmission:</td>
            <td>Integer</td>
            <td>Number of times per heartbeat the thresholds are checked.</td>
        </tr>
        <tr>
            <td>minimumThreshold:</td>
            <td>Integer</td>
            <td>Minimum Threshold</td>
        </tr>
        <tr>
            <td>maximumThreshold:</td>
            <td>Integer</td>
            <td>Maximum Threshold</td>
        </tr>
        <tr>
            <td>hysteresis:</td>
            <td>Integer</td>
            <td>Maximum Threshold</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/SensorSetThreshold/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&measurementsPerTransmission=10&minimumThreshold=320&maximumThreshold=2120&hysteresis=20" target="_blank">https://<%:Request.Url.Host %>/xml/SensorSetThreshold/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&amp;measurementsPerTransmission=10&amp;minimumThreshold=320&amp;maximumThreshold=2120&amp;hysteresis=20</a>--%>
    <input type="button" id="btn_TryAPI_SensorSetThreshold" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SensorSetThreshold').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SensorSetThreshold",
					"params": [
                        { "name": "sensorID", "type": "Integer", "description": "Unique identifier of the sensor", "optional": false },
						{ "name": "measurementsPerTransmission", "type": "Integer", "description": "Number of times per heartbeat the thresholds are checked", "optional": false },
						{ "name": "minimumThreshold", "type": "Integer", "description": "Minimum Threshold", "optional": false },
						{ "name": "maximumThreshold", "type": "Integer", "description": "Maximum Threshold", "optional": false },
						{ "name": "hysteresis", "type": "Integer", "description": "Maximum Threshold", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;SensorSetThreshold&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
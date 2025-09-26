<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorSetHeartbeat</b><br />
    Sets the heartbeat intervals of the sensor. Heartbeats cannot be set to lower than 10 minutes<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
        <tr>
            <td>reportInterval:</td>
            <td>Numeric</td>
            <td>Standard state heart beat (Minimum 10 minutes)</td>
        </tr>
        <tr>
            <td>activeStateInterval:</td>
            <td>Numeric</td>
            <td>Aware state heart beat (Minimum of 10 minutes. Must be equal to or less than reportInterval)</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/SensorSetHeartbeat/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&reportInterval=120&activeStateInterval=120" target="_blank">https://<%:Request.Url.Host %>/xml/SensorSetHeartbeat/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&amp;reportInterval=120&amp;activeStateInterval=120</a>--%>
     <input type="button" id="btn_TryAPI_SensorSetHeartbeat" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SensorSetHeartbeat').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SensorSetHeartbeat",
					"params": [
                        { "name": "sensorID", "type": "Integer", "description": "Unique identifier of the sensor", "optional": false },
                        { "name": "reportInterval", "type": "Numeric", "description": "Standard state heart beat (Minimum 10 minutes)", "optional": false },
						{ "name": "activeStateInterval", "type": "Numeric", "description": "Aware state heart beat (Minimum of 10 minutes. Must be equal to or less than reportInterval)", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;SensorSetHeartbeat&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorSetName</b><br />
    Sets the display name of the sensor<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
        <tr>
            <td>sensorName:</td>
            <td>String</td>
            <td>Name to give the sensor</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/SensorSetName/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&sensorName=MySensor" target="_blank">https://<%:Request.Url.Host %>/xml/SensorSetName/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&amp;sensorName=MySensor</a>--%>
    <input type="button" id="btn_TryAPI_SensorSetName" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SensorSetName').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SensorSetName",
					"params": [
						{ "name": "sensorID", "type": "Integer", "description": "Unique identifier of the sensor", "optional": false },
						{ "name": "sensorName", "type": "String", "description": "Name to give the sensor", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;SensorSetName&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
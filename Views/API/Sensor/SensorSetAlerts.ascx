<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorSetAlerts</b><br />
    Sets alerts active/inactive for network containing sensor<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
        <tr>
            <td>active:</td>
            <td>Boolean</td>
            <td>state to set the notifications. [true | false]</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/SensorSetAlerts/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&active=false" target="_blank">https://<%:Request.Url.Host %>/xml/SensorSetAlerts/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&amp;active=false</a>--%>
     <input type="button" id="btn_TryAPI_SensorSetAlerts" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SensorSetAlerts').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SensorSetAlerts",
					"params": [
                        { "name": "sensorID", "type": "Integer", "description": "Unique identifier of the sensor", "optional": false },
						{ "name": "active", "type": "Boolean", "description": "state to set the notifications. [true | false]", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;SensorSetAlerts&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
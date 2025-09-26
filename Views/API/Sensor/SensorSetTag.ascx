<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<div class="methodDiv">
    <b>Method: SensorSetTag()</b><br />
     Sets a tag field for a sensor (No longer used)<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Long</td>
            <td>Unique identifier of the Sensor</td>
        </tr>
        <tr>
            <td>tag:</td>
            <td>String</td>
            <td>This is a pipe deliminated string that gives a way to group sensors</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/SensorSetTag/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=250&tag=Room23|Temp" target="_blank">https://<%:Request.Url.Host %>/xml/SensorSetTag/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=250&tag=Room23|Temp</a>--%>
     <input type="button" id="btn_TryAPI_SensorSetTag" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SensorSetTag').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SensorSetTag",
					"params": [
                        { "name": "sensorID", "type": "Integer", "description": "Unique identifier of the sensor", "optional": false },
						{ "name": "tag", "type": "String", "description": "This is a pipe deliminated string that gives a way to group sensors", "optional": true }
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
&nbsp;&nbsp;&lt;Method&gt;SensorSetTag&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
